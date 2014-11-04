namespace ThemeVideoBackdrops.Configuration
{
    partial class PluginConfigurator2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BDSourcesLbl = new System.Windows.Forms.Label();
            this.SearchModeLbl = new System.Windows.Forms.Label();
            this.PlayCountLbl = new System.Windows.Forms.Label();
            this.lstSources = new System.Windows.Forms.ComboBox();
            this.lstSearchMode = new System.Windows.Forms.ComboBox();
            this.lstPlayCount = new System.Windows.Forms.ComboBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BDSourcesLbl
            // 
            this.BDSourcesLbl.AutoSize = true;
            this.BDSourcesLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BDSourcesLbl.Location = new System.Drawing.Point(12, 22);
            this.BDSourcesLbl.Name = "BDSourcesLbl";
            this.BDSourcesLbl.Size = new System.Drawing.Size(116, 17);
            this.BDSourcesLbl.TabIndex = 0;
            this.BDSourcesLbl.Text = "Backdrop Sources";
            // 
            // SearchModeLbl
            // 
            this.SearchModeLbl.AutoSize = true;
            this.SearchModeLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchModeLbl.Location = new System.Drawing.Point(12, 68);
            this.SearchModeLbl.Name = "SearchModeLbl";
            this.SearchModeLbl.Size = new System.Drawing.Size(87, 17);
            this.SearchModeLbl.TabIndex = 1;
            this.SearchModeLbl.Text = "Search Mode";
            // 
            // PlayCountLbl
            // 
            this.PlayCountLbl.AutoSize = true;
            this.PlayCountLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayCountLbl.Location = new System.Drawing.Point(12, 113);
            this.PlayCountLbl.Name = "PlayCountLbl";
            this.PlayCountLbl.Size = new System.Drawing.Size(75, 17);
            this.PlayCountLbl.TabIndex = 2;
            this.PlayCountLbl.Text = "Play Count";
            // 
            // lstSources
            // 
            this.lstSources.FormattingEnabled = true;
            this.lstSources.Location = new System.Drawing.Point(138, 18);
            this.lstSources.Name = "lstSources";
            this.lstSources.Size = new System.Drawing.Size(178, 21);
            this.lstSources.TabIndex = 3;
            this.lstSources.SelectedIndexChanged += new System.EventHandler(this.lstSources_SelectedIndexChanged);
            // 
            // lstSearchMode
            // 
            this.lstSearchMode.FormattingEnabled = true;
            this.lstSearchMode.Location = new System.Drawing.Point(138, 64);
            this.lstSearchMode.Name = "lstSearchMode";
            this.lstSearchMode.Size = new System.Drawing.Size(178, 21);
            this.lstSearchMode.TabIndex = 4;
            this.lstSearchMode.SelectedIndexChanged += new System.EventHandler(this.lstSearchMode_SelectedIndexChanged);
            // 
            // lstPlayCount
            // 
            this.lstPlayCount.FormattingEnabled = true;
            this.lstPlayCount.Location = new System.Drawing.Point(138, 109);
            this.lstPlayCount.Name = "lstPlayCount";
            this.lstPlayCount.Size = new System.Drawing.Size(56, 21);
            this.lstPlayCount.TabIndex = 5;
            this.lstPlayCount.SelectedIndexChanged += new System.EventHandler(this.lstPlayCount_SelectedIndexChanged);
            // 
            // BtnOK
            // 
            this.BtnOK.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOK.Location = new System.Drawing.Point(141, 254);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(68, 33);
            this.BtnOK.TabIndex = 6;
            this.BtnOK.Text = "SAVE";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.Location = new System.Drawing.Point(226, 254);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(80, 33);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "CANCEL";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // PluginConfigurator2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(451, 299);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.lstPlayCount);
            this.Controls.Add(this.lstSearchMode);
            this.Controls.Add(this.lstSources);
            this.Controls.Add(this.PlayCountLbl);
            this.Controls.Add(this.SearchModeLbl);
            this.Controls.Add(this.BDSourcesLbl);
            this.MaximizeBox = false;
            this.Name = "PluginConfigurator2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThemeVideo Backdrops Configurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BDSourcesLbl;
        private System.Windows.Forms.Label SearchModeLbl;
        private System.Windows.Forms.Label PlayCountLbl;
        private System.Windows.Forms.ComboBox lstSources;
        private System.Windows.Forms.ComboBox lstSearchMode;
        private System.Windows.Forms.ComboBox lstPlayCount;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button CancelBtn;
    }
}