using System;
using System.IO;
using ThemeVideoBackdrops.Configuration;
using MediaBrowser.Library;
using MediaBrowser.Library.Configuration;
using MediaBrowser.Library.Logging;
using MediaBrowser.Library.Plugins;
using ThemeVideoBackdrops.Playables;

namespace ThemeVideoBackdrops
{
    public class Plugin : BasePlugin
    {
        private static readonly Guid ThemeVideoBackdropsGuid = new Guid("2CACB8F2-3111-4946-A1D5-1218D843D391");
        
        private const string PluginName = "ThemeVideo Backdrops";

        /// <summary>
        /// Name of the Plugin
        /// </summary>
        /*public override string Name
        {
            get { return PluginName; }
        }*/

        /// <summary>
        /// Description of Plugin
        /// </summary>
        public override string Description
        {
            get { return "Video & Audio Backdrops for your Movies & TV Series."; }
        }
        
        /// <summary>
        /// Allow MBC Configurator to know whether the plugin is configurable and allows use of the configure button
        /// </summary>
        public override bool IsConfigurable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the config file
        /// </summary>
        public string ConfigFile
        {
            get
            {
                return Path.Combine(ApplicationPaths.PluginConfigPath, "ThemeVideoBackdrops.xml");
            }
        }

        public override bool InstallGlobally
        {
            get
            {
                return true;
            }
        }

        public override string Name
        {
            get
            {
                return "ThemeVideo Backdrops";
            }
        }

        public override string PluginClass
        {
            get
            {
                return "Radeons Tools";
            }
        }

        /// <summary>
        /// Initializes the entire plugin when MBC Loads.  This happens only once.
        /// </summary>
        public override void Init(Kernel kernel)
        {
            if (File.Exists("C:\\ProgramData\\MediaBrowser-Classic\\Plugins\\Backup\\ThemeVideoBackdrops.dll"))
            {
                File.Delete("C:\\ProgramData\\MediaBrowser-Classic\\Plugins\\Backup\\ThemeVideoBackdrops.dll");
            }
            try
            {
                if (AppDomain.CurrentDomain.FriendlyName.Contains("ehExtHost"))
                {
                    
                    ReloadConfig();
                    ThemeVideoPlayables.kernel_ApplicationInitialized();

                }
                Logger.ReportInfo("ThemeVideo Backdrops (version " + this.Version + ") Plug-in Loaded.");
            }
            catch (Exception exception)
            {
                Logger.ReportException("Error initializing ThemeVideo Backdrops - probably incompatable MB version", exception);
            }
        }

        /// <summary>
        /// Reloads (Forces) the Config file
        /// </summary>
        private void ReloadConfig()
        {
            PluginConfig.Instance = PluginConfig.FromFile(ConfigFile);
        }


        public override void Configure()
        {
            if (PluginConfigurator.Configure(this) == true)
            {
                ReloadConfig();
            }
        }

        
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
