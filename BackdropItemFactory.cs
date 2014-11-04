using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaBrowser.Library.Factories;
using MediaBrowser.Library.Logging;
using MediaBrowser.Library.Playables;
using ThemeVideoBackdrops.Configuration;
using MediaBrowser.Library.Entities;
using MediaBrowser.Library.Interfaces;
using MediaBrowser.LibraryManagement;

namespace ThemeVideoBackdrops
{
    class BackdropItemFactory
    {
        public static BackdropItemFactory Instance = new BackdropItemFactory();

        private readonly Dictionary<Guid, BackdropItem> CachedItems = new Dictionary<Guid, BackdropItem>();

        public BackdropItem Create(BaseItem item)
        {
            BackdropItem backdropItem = CreateInternal(item);

            while (!backdropItem.Files.Any() && item.Parent != null)
            {
                item = item.Parent;
                backdropItem = CreateInternal(item);
            }
            
            return backdropItem;
        }

        private BackdropItem CreateInternal(BaseItem item)
        {
            if (!CachedItems.ContainsKey(item.Id))
            {
                CachedItems[item.Id] = new BackdropItem()
                {
                    Files = GetBackdropFiles(item),
                    ItemId = item.Id
                };
            }

            return CachedItems[item.Id];
        }

        private IEnumerable<string> GetBackdropFiles(BaseItem item)
        {
            var files = new List<string>();

            foreach (BackdropSource source in PluginConfig.Instance.BackdropSources)
            {
                if (source == BackdropSource.MediaBackdrops)
                {
                    files.AddRange(GetMediaBackdropFiles(item));
                }
                else if (source == BackdropSource.Trailers)
                {
                    files.AddRange(GetTrailerFiles(item));
                }

                if (PluginConfig.Instance.StopSearchingAfterFirstFoundSource && files.Any())
                {
                    break;
                }
            }

            return files;
        }

        private IEnumerable<string> GetTrailerFiles(BaseItem item)
        {
            ISupportsTrailers media = item as ISupportsTrailers;

            if (media != null)
            {
                return media.TrailerFiles;
            }

            return new string[] { };
        }

        private IEnumerable<string> GetMediaBackdropFiles(BaseItem item)
        {
            var path = item.Path;

            var files = new List<string>();
            

            if (Path.HasExtension(path))
            {
                path = Path.GetDirectoryName(path);
            }

            path = Path.Combine(path, "backdrops");
            //Check this the backdrops dir exists and find all videos and add to the files list
            //Logger.ReportInfo("######################################backdrops" + path);
            if (Directory.Exists(path))
            {
                //Logger.ReportInfo("######################################backdrops" + path);
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    if (Helper.IsVideo(file))
                    {
                        files.Add(file);
                    }         
                }
                
            }
            //Fall back to theme music if no theme videos are available
            else
            {
                //strip back off the backdrops from the path and perform a lookup in the root of the Item
                path = Path.GetDirectoryName(path);
                //playPlayableItem.GoFullScreen = false;
                //Logger.ReportInfo("######################################rootmusic" + path);
                files.AddRange(GetMediaBackdropMusicFiles(path));
                
                //If still nothing found, apend theme-music onto the path and look in there
                if (files.Count <= 0)
                {
                    //Logger.ReportInfo("######################################theme-music" + path);
                    //playPlayableItem.GoFullScreen = false;
                    path = Path.Combine(path, "theme-music");
                    files.AddRange(GetMediaBackdropMusicFiles(path));
                    
                }
                
            }
            //in any case, return the files list.
            return files; 
        }

        //the new function to check if theme music is available in the path passed. 
        private IEnumerable<string> GetMediaBackdropMusicFiles(String path)
        {
            var files = new List<string>();
         
            if (Directory.Exists(path))
            {

                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    var extension = Path.GetExtension(file);
                    if (extension != null)
                    {
                        string ext = extension.ToLower();

                        if (ext.EndsWith("mp3") || ext.EndsWith("flac") || ext.EndsWith("wma"))
                        {
                            files.Add(file);
                        }
                    }
                }
            }
            return files;
        }
    }
}
