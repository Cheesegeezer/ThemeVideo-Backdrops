using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaBrowser;
using MediaBrowser.Library;
using MediaBrowser.Library.Playables.VLC2;
using MediaBrowser.LibraryManagement;
using MediaBrowser.Library.Entities;
using MediaBrowser.Library.Events;
using MediaBrowser.Library.Factories;
using MediaBrowser.Library.Logging;
using MediaBrowser.Library.Playables;
using ThemeVideoBackdrops.Configuration;

namespace ThemeVideoBackdrops.Playables
{
    public class ThemeVideoPlayables
    {
        private static BackdropItem CurrentBackdropItem { get; set; }

        private static PlayableItem PlayableItem { get; set; }

        private const string PluginName = "ThemeVideo Backdrops";
        private static IEnumerable<string> files;

        /// <summary>
        /// Bool - Is the current items themebackdrop playing
        /// </summary>
        static bool IsPlaying
        {
            get
            {
                return CurrentBackdropItem != null;
            }
        }

        /// <summary>
        /// Bool - Is there a trailer being played
        /// </summary>
        private static bool IsCurrentlyPlayingLocalTrailer
        {
            get
            {
                if (!IsPlaying)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(PlayableItem.CurrentFile))
                {
                    return PlayableItem.CurrentFile.ToLower().Contains("\\trailers\\");
                }

                return PlayableItem.Files.Any(f => f.ToLower().Contains("\\trailers\\"));
            }
        }

        /// <summary>
        ///The Main Method for the Plugin - Do not change unless broken
        /// </summary>
        public static void kernel_ApplicationInitialized()
        {
            try
            {
                if (PluginConfig.Instance.BackdropSources.Any())
                {
                    Application.CurrentInstance.PrePlayback += Application_PrePlayback;
                    Application.CurrentInstance.PlaybackFinished += Application_PlaybackFinished;
                    AttachEventHandlers();
                }
            }
            catch (Exception ex)
            {

                Logger.ReportException("kernal_ApplicationInitialized Method NOT Working", ex);
            }

        }


        public static void Application_PlaybackFinished(object sender, GenericEventArgs<PlayableItem> e)
        {
            AttachEventHandlers();
        }

        public static void Application_PrePlayback(object sender, GenericEventArgs<PlayableItem> e)
        {
            DetachEventHandlers();
        }

        private static void AttachEventHandlers()
        {
            DetachEventHandlers();

            Application.CurrentInstance.NavigationInto += Item_NavigationInto;
            Application.CurrentInstance.CurrentItemChanged += CurrentItemChanged;

        }

        private static void DetachEventHandlers()
        {
            Application.CurrentInstance.NavigationInto -= Item_NavigationInto;
            Application.CurrentInstance.CurrentItemChanged -= CurrentItemChanged;
        }

        /// <summary>
        /// Logic when Navigating into the current item.
        /// </summary>
        static void Item_NavigationInto(object sender, GenericEventArgs<Item> e)
        {
            BackdropItem backdropItem = BackdropItemFactory.Instance.Create(e.Item.BaseItem);

            if (IsCurrentlyPlaying(backdropItem))
            {
                return;
            }

            if (IsCurrentlyPlayingLocalTrailer)
            {
                return;
            }

            if (backdropItem.Files.Any())
            {
                Play(backdropItem);
            }
        }

        /// <summary>
        /// Is the Logic for when the user changes the item
        /// </summary>
        static void CurrentItemChanged(object sender, GenericEventArgs<Item> e)
        {
            if (!IsPlaying) return;

            var type = e.Item.ItemTypeString;
            //Play thru without stopping in TV shows, From Series=>Season>Episode and back until Exiting Season View
            if (type == "Season" | type == "episode")
            {
                if (!IsCurrentlyPlaying(BackdropItemFactory.Instance.Create(e.Item.BaseItem)))
                {
                    // Need to stop if we've left the currently playing backdrop
                    Stop();
                }
            }
                //To ensure it stops when indexing in TV series.
            else if (type == "Series")
            {
                if (IsCurrentlyPlaying(BackdropItemFactory.Instance.Create(e.Item.BaseItem)))
                {
                    // Need to stop if we've left the currently playing backdrop
                    Stop();
                }
            }
            //Ensure movies still works
            else
            {
                if (!IsCurrentlyPlaying(BackdropItemFactory.Instance.Create(e.Item.BaseItem.Parent)))
                {
                    // Need to stop if we've left the currently playing backdrop
                    Stop();
                }
            }

        }

        /// <summary>
        /// Gets the current playing state for the current item's backdrop.
        /// </summary>

        private static bool IsCurrentlyPlaying(BackdropItem item)
        {
            return IsPlaying && CurrentBackdropItem.ItemId.Equals(item.ItemId);
        }


        /// <summary>
        /// Stops playback of the current items themebackdrop
        /// </summary>
        private static void Stop()
        {
            if (IsPlaying)
            {
                CurrentBackdropItem = null;
                PlayableItem.StopPlayback();
                Application.CurrentInstance.StopAllPlayback(true);
                PlayableItem.ShowNowPlayingView = false;
                PlayableItem = null;
                
            }
        }

        

        /// <summary>
        /// Starts play of the current items themebackdrop
        /// </summary>
        private static void Play(BackdropItem backdropItem)
        {
            PlayableItem = GetPlayableItem(backdropItem);
            Application.CurrentInstance.Play(PlayableItem);
            CurrentBackdropItem = backdropItem;
            //PlayableItem.ShowNowPlayingView = true; - this doesn't work

        }

        /// <summary>
        /// Gets the list of playable theme files from the currently selected item
        /// </summary>
        private static PlayableItem GetPlayableItem(BackdropItem backdropItem)
        {
            var filesToPlay = new List<string>();

            if (backdropItem.Files.Count() == 1)
            {
                filesToPlay.Add(backdropItem.Files.First());
            }
            /*else if (PluginConfig.Instance.Shuffle)
            {
                filesToPlay.AddRange(ShuffleFiles(backdropItem.Files));
            }*/
            else
            {
                filesToPlay.AddRange(backdropItem.Files);
            }

            if (PluginConfig.Instance.PlayCount > 1)
            {
                filesToPlay = RepeatList(filesToPlay, PluginConfig.Instance.PlayCount);
            }

            PlayableItem playable = PlayableItemFactory.Instance.CreateForInternalPlayer(filesToPlay);

            playable.GoFullScreen = false;
            playable.RaiseGlobalPlaybackEvents = false;
            playable.ShowNowPlayingView = Helper.IsVideo(filesToPlay.First());
            playable.UseCustomPlayer = false;
            playable.PlayInBackground = true;

            return playable;
        }

        /// <summary>
        /// Config Option - Allows us to repeat the playlist of themes a fixed number of times - User selection
        /// </summary>
        private static List<string> RepeatList(IEnumerable<string> items, int count)
        {
            var newList = new List<string>();

            for (int i = 0; i < count; i++)
            {
                newList.AddRange(items);
            }

            return newList;
        }

        /*private IEnumerable<string> ShuffleFiles(IEnumerable<string> files)
        {
            var rnd = new Random();

            return files.OrderBy(i => rnd.Next()).ToList();
        }*/




        /// <summary>
        /// Convenience log method to help standardize messages from plugins
        /// </summary>
        public static void LogInfo(string message, params object[] paramList)
        {
            Logger.ReportInfo(PluginName + ": " + message, paramList);
        }

        /// <summary>
        /// Convenience log method to help standardize messages from plugins
        /// </summary>
        public static void LogVerbose(string message, params object[] paramList)
        {
            Logger.ReportVerbose(PluginName + ": " + message, paramList);
        }

        /// <summary>
        /// Convenience log method to help standardize messages from plugins
        /// </summary>
        public static void LogWarning(string message, params object[] paramList)
        {
            Logger.ReportWarning(PluginName + ": " + message, paramList);
        }

        /// <summary>
        /// Convenience log method to help standardize messages from plugins
        /// </summary>
        public static void LogError(string message, params object[] paramList)
        {
            Logger.ReportError(PluginName + ": " + message, paramList);
        }

        /// <summary>
        /// Convenience log method to help standardize messages from plugins
        /// </summary>
        public static void LogException(string message, Exception ex, params object[] paramList)
        {
            Logger.ReportException(PluginName + ": " + message, ex, paramList);
        }
    }
}
