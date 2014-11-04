using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Library.Persistance;
using MediaBrowser.Library.Plugins;

namespace ThemeVideoBackdrops.Configuration
{
    public class PluginConfig : PluginConfigurationOptions
    {

        public static PluginConfig FromFile(string filename)
        {
            var config = new PluginConfig {File = filename};

            config._settings = XmlSettings<PluginConfig>.Bind(config, filename);

            return config;
        }

        public static PluginConfig Instance { get; set; }

        //public bool Shuffle = false;
        public int PlayCount = 1; 
        public bool StopSearchingAfterFirstFoundSource = false;

        public List<BackdropSource> BackdropSources { get; set; }

        public PluginConfig()
        {
            BackdropSources = new[] { BackdropSource.MediaBackdrops }.ToList();
        }

        [SkipField]
        public string File;

        [SkipField]
        XmlSettings<PluginConfig> _settings;

        public void Save()
        {
            _settings.Write();
        }
    }

    public enum BackdropSource
    {
        MediaBackdrops = 1,
        Trailers = 2
    }
}
