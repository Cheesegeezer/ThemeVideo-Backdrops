using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ThemeVideoBackdrops.Configuration
{
    public partial class PluginConfigurator2 : Form
    {
        private int _selectedIndex;
        private string _text;

        public PluginConfigurator2()
        {
            InitializeComponent();
        }

        private void lstSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstSourceCombo item1 = new LstSourceCombo();
            item1.Text = "Backdrops Only";
            item1.Value = "1";

            LstSourceCombo item2 = new LstSourceCombo();
            item2.Text = "Trailers Only";
            item2.Value = "2";

            LstSourceCombo item3 = new LstSourceCombo();
            item3.Text = "Play Backdrops then Trailers";
            item3.Value = "3";

            LstSourceCombo item4 = new LstSourceCombo();
            item4.Text = "Play Trailers then Backdrops";
            item4.Value = "4";

            LstSourceCombo item5 = new LstSourceCombo();
            item5.Text = "None";
            item5.Value = "5";

            _selectedIndex = lstSources.SelectedIndex;
            Display();

        }

        private void lstSearchMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstPlayCount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void Display()
        {
            Text = string.Format("Text: {0}; SelectedIndex: {1}",_text,_selectedIndex);
        }
    }
}

public class LstSourceCombo
{
    public string Text { get; set; }
    public string Value { get; set; }
    public override string ToString() { return Text; } 
}

public class LstSearchCombo
{
    
}
