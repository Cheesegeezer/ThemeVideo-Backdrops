using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using MediaBrowser.Library.Logging;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace ThemeVideoBackdrops.Configuration
{
    /// <summary>
    /// Interaction logic for PluginConfigurator.xaml
    /// </summary>
    public partial class PluginConfigurator
    {
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        public static bool? Configure(Plugin plugin)
        {
            var ui = new PluginConfigurator
            {
                Title = "Configure " + plugin.Name,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            PluginConfig config = PluginConfig.FromFile(plugin.ConfigFile);

            ui.FillControlsFromObject(config);

            var result = ui.ShowDialog();

            if (result != true) return result;
            ui.UpdateObjectFromControls(config);

            config.Save();

            return true;

        }

        public PluginConfigurator()
        {
            Logger.ReportInfo("ThemeVideo Backdrops is Initializing the Plugin Configuration.");
            try
            {
                
                
                InitializeComponent();

                btnOK.Click += btnOK_Click;
                btnCancel.Click += btnCancel_Click;

                for (int i = 1; i <= 20; i++)
                {
                    lstPlayCount.Items.Add(new ComboBoxItem() { Content = i.ToString(CultureInfo.InvariantCulture) });
                }

                lstSources.SelectionChanged += lstSources_SelectionChanged;
            }
            catch (Exception ex)
            {
                Logger.ReportException("Error initializing ThemeVideo Backdrops Plugin Configuration - The EXCEPTION thown is {0}", ex);
            }
            
        }

        void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = lstSources.SelectedIndex;

            pnlSearchMode.Visibility = selectedIndex == 1 || selectedIndex == 3 ? Visibility.Visible : Visibility.Collapsed;

            //pnlShuffle.Visibility = selectedIndex == 4 ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            pnlPlayCount.Visibility = selectedIndex == 4 ? Visibility.Collapsed : Visibility.Visible;
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateUserInput())
            {
                this.DialogResult = true;
                this.Close();

            }
        }

        public void FillControlsFromObject(PluginConfig config)
        {
            //chkShuffle.IsChecked = config.Shuffle;
            lstSearchMode.SelectedIndex = config.StopSearchingAfterFirstFoundSource ? 1 : 0;

            lstPlayCount.SelectedIndex = config.PlayCount - 1;

            bool hasBackdrops = config.BackdropSources.Any(b => b == BackdropSource.MediaBackdrops);
            bool hasTrailers = config.BackdropSources.Any(b => b == BackdropSource.Trailers);

            if (hasBackdrops && hasTrailers)
            {
                lstSources.SelectedIndex = config.BackdropSources.First() == BackdropSource.MediaBackdrops ? 3 : 1;
            }
            else if (hasBackdrops)
            {
                lstSources.SelectedIndex = 2;
            }
            else if (hasTrailers)
            {
                lstSources.SelectedIndex = 0;
            }
            else
            {
                lstSources.SelectedIndex = 4;
            }
        }

        public void UpdateObjectFromControls(PluginConfig config)
        {
            config.StopSearchingAfterFirstFoundSource = lstSearchMode.SelectedIndex == 1;
            config.PlayCount = lstPlayCount.SelectedIndex + 1;

            switch (lstSources.SelectedIndex)
            {
                case 0:
                    config.BackdropSources = new[] { BackdropSource.Trailers }.ToList();
                    break;
                case 1:
                    config.BackdropSources = new[] { BackdropSource.Trailers, BackdropSource.MediaBackdrops }.ToList();
                    break;
                case 2:
                    config.BackdropSources = new[] { BackdropSource.MediaBackdrops }.ToList();
                    break;
                case 3:
                    config.BackdropSources = new[] { BackdropSource.MediaBackdrops, BackdropSource.Trailers }.ToList();
                    break;
                case 4:
                    config.BackdropSources = new BackdropSource[] { }.ToList();
                    break;
            }
        }

        private static bool ValidateUserInput()
        {
            return true;
        }


        //ALLOW US TO LOG IN DURING CONFIGURATION ACTIVITY
        private const string PluginName = "ThemeVideo Backdrops";

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
