using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaBrowser.Library.Plugins;
using MediaBrowser.Library.Plugins.Configuration;
using ThemeVideoBackdrops.Configuration;

namespace ThemeVideoBackdrops
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("Menu Name:")]
        public string MenuName = "ThemeVideo Backdrops";

        [Label("Backdrop Source:")]
        [Items("Local Trailers, Local Trailers then Theme Backdrops, Theme Backdrops, Theme Backdrops then Local Trailers, None(disabled)")]
        public string ThemeSource = "Local Trailers";

        [Label("Search Mode:")]
        [Items("Combine all sources for each item, Limit to first found source for each item")]
        public string SearchMode = "Combine all sources for each item";

        [Label("Play Count:")]
        public string PlayCount = "1";
    }
}
