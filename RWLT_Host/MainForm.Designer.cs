namespace RWLT_Host
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.connectionBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.trackBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.trackExportBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.trackClearBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.logBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.logViewCurrentBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.logAnalyzeCurrentBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.logAnalyzeBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.logClearAllEntriesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.infoBtn = new System.Windows.Forms.ToolStripButton();
            this.settingsBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.isScreenShotsBtn = new System.Windows.Forms.ToolStripButton();
            this.emulatorBtn = new System.Windows.Forms.ToolStripButton();
            this.markCurrentBtn = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainPortStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.auxGNSSStatusCaptionLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.auxGNSSStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.targetToolStrip = new System.Windows.Forms.ToolStrip();
            this.auxCapLbl = new System.Windows.Forms.ToolStripLabel();
            this.auxCrsLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.auxMiscLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbaLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.hdopLbl = new System.Windows.Forms.ToolStripLabel();
            this.auxToolStrip = new System.Windows.Forms.ToolStrip();
            this.targetCapLbl = new System.Windows.Forms.ToolStripLabel();
            this.targetCrsLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.targetMiscLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.plotToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tracksToFitCbx = new System.Windows.Forms.ToolStripComboBox();
            this.geoPlot = new UCNLUI.Controls.uOSMGeoPlot();
            this.isStatisticsBtn = new System.Windows.Forms.ToolStripButton();
            this.mainToolStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.targetToolStrip.SuspendLayout();
            this.auxToolStrip.SuspendLayout();
            this.plotToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionBtn,
            this.toolStripSeparator1,
            this.trackBtn,
            this.logBtn,
            this.toolStripSeparator2,
            this.infoBtn,
            this.settingsBtn,
            this.toolStripSeparator6,
            this.isScreenShotsBtn,
            this.emulatorBtn,
            this.markCurrentBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(877, 30);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // connectionBtn
            // 
            this.connectionBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.connectionBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionBtn.Image = ((System.Drawing.Image)(resources.GetObject("connectionBtn.Image")));
            this.connectionBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(125, 27);
            this.connectionBtn.Text = "CONNECTION";
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // trackBtn
            // 
            this.trackBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.trackBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackExportBtn,
            this.toolStripSeparator3,
            this.trackClearBtn});
            this.trackBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trackBtn.Image = ((System.Drawing.Image)(resources.GetObject("trackBtn.Image")));
            this.trackBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.trackBtn.Name = "trackBtn";
            this.trackBtn.Size = new System.Drawing.Size(78, 27);
            this.trackBtn.Text = "TRACK";
            // 
            // trackExportBtn
            // 
            this.trackExportBtn.Name = "trackExportBtn";
            this.trackExportBtn.Size = new System.Drawing.Size(157, 28);
            this.trackExportBtn.Text = "EXPORT...";
            this.trackExportBtn.Click += new System.EventHandler(this.trackExportBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // trackClearBtn
            // 
            this.trackClearBtn.Name = "trackClearBtn";
            this.trackClearBtn.Size = new System.Drawing.Size(157, 28);
            this.trackClearBtn.Text = "CLEAR";
            this.trackClearBtn.Click += new System.EventHandler(this.trackClearBtn_Click);
            // 
            // logBtn
            // 
            this.logBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.logBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logViewCurrentBtn,
            this.logAnalyzeCurrentBtn,
            this.logAnalyzeBtn,
            this.toolStripSeparator4,
            this.logClearAllEntriesBtn});
            this.logBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logBtn.Image = ((System.Drawing.Image)(resources.GetObject("logBtn.Image")));
            this.logBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logBtn.Name = "logBtn";
            this.logBtn.Size = new System.Drawing.Size(57, 27);
            this.logBtn.Text = "LOG";
            // 
            // logViewCurrentBtn
            // 
            this.logViewCurrentBtn.Name = "logViewCurrentBtn";
            this.logViewCurrentBtn.Size = new System.Drawing.Size(247, 28);
            this.logViewCurrentBtn.Text = "VIEW CURRENT";
            this.logViewCurrentBtn.Click += new System.EventHandler(this.logViewCurrentBtn_Click);
            // 
            // logAnalyzeCurrentBtn
            // 
            this.logAnalyzeCurrentBtn.Name = "logAnalyzeCurrentBtn";
            this.logAnalyzeCurrentBtn.Size = new System.Drawing.Size(247, 28);
            this.logAnalyzeCurrentBtn.Text = "PLAYBACK CURRENT";
            this.logAnalyzeCurrentBtn.Click += new System.EventHandler(this.logAnalyzeCurrentBtn_Click);
            // 
            // logAnalyzeBtn
            // 
            this.logAnalyzeBtn.Name = "logAnalyzeBtn";
            this.logAnalyzeBtn.Size = new System.Drawing.Size(247, 28);
            this.logAnalyzeBtn.Text = "PLAYBACK..";
            this.logAnalyzeBtn.Click += new System.EventHandler(this.logAnalyzeBtn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(244, 6);
            // 
            // logClearAllEntriesBtn
            // 
            this.logClearAllEntriesBtn.Name = "logClearAllEntriesBtn";
            this.logClearAllEntriesBtn.Size = new System.Drawing.Size(247, 28);
            this.logClearAllEntriesBtn.Text = "CLEAR ALL ENTRIES";
            this.logClearAllEntriesBtn.Click += new System.EventHandler(this.logClearAllEntriesBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // infoBtn
            // 
            this.infoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(54, 27);
            this.infoBtn.Text = "INFO";
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(93, 27);
            this.settingsBtn.Text = "SETTINGS";
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 30);
            // 
            // isScreenShotsBtn
            // 
            this.isScreenShotsBtn.CheckOnClick = true;
            this.isScreenShotsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isScreenShotsBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isScreenShotsBtn.Image = ((System.Drawing.Image)(resources.GetObject("isScreenShotsBtn.Image")));
            this.isScreenShotsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isScreenShotsBtn.Name = "isScreenShotsBtn";
            this.isScreenShotsBtn.Size = new System.Drawing.Size(178, 27);
            this.isScreenShotsBtn.Text = "AUTOSCREENSHOTS";
            this.isScreenShotsBtn.Click += new System.EventHandler(this.isScreenShotsBtn_Click);
            // 
            // emulatorBtn
            // 
            this.emulatorBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.emulatorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.emulatorBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.emulatorBtn.Image = ((System.Drawing.Image)(resources.GetObject("emulatorBtn.Image")));
            this.emulatorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.emulatorBtn.Name = "emulatorBtn";
            this.emulatorBtn.Size = new System.Drawing.Size(50, 27);
            this.emulatorBtn.Text = "EMU";
            this.emulatorBtn.Click += new System.EventHandler(this.emulatorBtn_Click);
            // 
            // markCurrentBtn
            // 
            this.markCurrentBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.markCurrentBtn.Enabled = false;
            this.markCurrentBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markCurrentBtn.Image = ((System.Drawing.Image)(resources.GetObject("markCurrentBtn.Image")));
            this.markCurrentBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markCurrentBtn.Name = "markCurrentBtn";
            this.markCurrentBtn.Size = new System.Drawing.Size(120, 27);
            this.markCurrentBtn.Text = "MARK POINT";
            this.markCurrentBtn.Click += new System.EventHandler(this.markCurrentBtn_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.mainPortStatusLbl,
            this.auxGNSSStatusCaptionLbl,
            this.auxGNSSStatusLbl});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 540);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.mainStatusStrip.Size = new System.Drawing.Size(877, 28);
            this.mainStatusStrip.TabIndex = 1;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(89, 23);
            this.toolStripStatusLabel1.Text = "Main port:";
            // 
            // mainPortStatusLbl
            // 
            this.mainPortStatusLbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainPortStatusLbl.Name = "mainPortStatusLbl";
            this.mainPortStatusLbl.Size = new System.Drawing.Size(75, 23);
            this.mainPortStatusLbl.Text = "CLOSED";
            // 
            // auxGNSSStatusCaptionLbl
            // 
            this.auxGNSSStatusCaptionLbl.Name = "auxGNSSStatusCaptionLbl";
            this.auxGNSSStatusCaptionLbl.Size = new System.Drawing.Size(95, 23);
            this.auxGNSSStatusCaptionLbl.Text = "AUX GNSS:";
            this.auxGNSSStatusCaptionLbl.Visible = false;
            // 
            // auxGNSSStatusLbl
            // 
            this.auxGNSSStatusLbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.auxGNSSStatusLbl.Name = "auxGNSSStatusLbl";
            this.auxGNSSStatusLbl.Size = new System.Drawing.Size(75, 23);
            this.auxGNSSStatusLbl.Text = "CLOSED";
            this.auxGNSSStatusLbl.Visible = false;
            // 
            // targetToolStrip
            // 
            this.targetToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.auxCapLbl,
            this.auxCrsLbl,
            this.toolStripSeparator8,
            this.auxMiscLbl,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.tbaLbl,
            this.toolStripLabel2,
            this.hdopLbl});
            this.targetToolStrip.Location = new System.Drawing.Point(0, 30);
            this.targetToolStrip.Name = "targetToolStrip";
            this.targetToolStrip.Size = new System.Drawing.Size(877, 41);
            this.targetToolStrip.TabIndex = 3;
            this.targetToolStrip.Text = "toolStrip1";
            // 
            // auxCapLbl
            // 
            this.auxCapLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.auxCapLbl.Name = "auxCapLbl";
            this.auxCapLbl.Size = new System.Drawing.Size(55, 38);
            this.auxCapLbl.Text = "AUX:";
            // 
            // auxCrsLbl
            // 
            this.auxCrsLbl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.auxCrsLbl.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.auxCrsLbl.Name = "auxCrsLbl";
            this.auxCrsLbl.Size = new System.Drawing.Size(66, 38);
            this.auxCrsLbl.Text = "- - -";
            this.auxCrsLbl.ToolTipText = "⎈ - AUX GNSS track\r\n🡽 - azimuth to the target";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 41);
            // 
            // auxMiscLbl
            // 
            this.auxMiscLbl.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.auxMiscLbl.Name = "auxMiscLbl";
            this.auxMiscLbl.Size = new System.Drawing.Size(66, 38);
            this.auxMiscLbl.Text = "- - -";
            this.auxMiscLbl.ToolTipText = "🡺-Distance to the target\r\n🐌 - AUX GNSS Speed";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 41);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(50, 38);
            this.toolStripLabel1.Text = "TBA:";
            this.toolStripLabel1.ToolTipText = "Target-to-base arrangement quality.\r\nThe best arrangement is when the target is i" +
    "nside a polygon formed by the base stations";
            // 
            // tbaLbl
            // 
            this.tbaLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbaLbl.Name = "tbaLbl";
            this.tbaLbl.Size = new System.Drawing.Size(48, 38);
            this.tbaLbl.Text = "- - -";
            this.tbaLbl.ToolTipText = "Target-to-base arrangement quality";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(70, 38);
            this.toolStripLabel2.Text = "HDOP:";
            this.toolStripLabel2.ToolTipText = "Horizontal dilution of precision";
            // 
            // hdopLbl
            // 
            this.hdopLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hdopLbl.Name = "hdopLbl";
            this.hdopLbl.Size = new System.Drawing.Size(48, 38);
            this.hdopLbl.Text = "- - -";
            this.hdopLbl.ToolTipText = "Better when the target is inside navigation base";
            // 
            // auxToolStrip
            // 
            this.auxToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.targetCapLbl,
            this.targetCrsLbl,
            this.toolStripSeparator9,
            this.targetMiscLbl,
            this.toolStripSeparator7});
            this.auxToolStrip.Location = new System.Drawing.Point(0, 71);
            this.auxToolStrip.Name = "auxToolStrip";
            this.auxToolStrip.Size = new System.Drawing.Size(877, 41);
            this.auxToolStrip.TabIndex = 4;
            this.auxToolStrip.Text = "toolStrip1";
            // 
            // targetCapLbl
            // 
            this.targetCapLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.targetCapLbl.Name = "targetCapLbl";
            this.targetCapLbl.Size = new System.Drawing.Size(49, 38);
            this.targetCapLbl.Text = "TGT:";
            // 
            // targetCrsLbl
            // 
            this.targetCrsLbl.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.targetCrsLbl.Name = "targetCrsLbl";
            this.targetCrsLbl.Size = new System.Drawing.Size(66, 38);
            this.targetCrsLbl.Text = "- - -";
            this.targetCrsLbl.ToolTipText = "⎈ - Target track\r\n🡽 - azimuth to AUX GNSS\r\n";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 41);
            // 
            // targetMiscLbl
            // 
            this.targetMiscLbl.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.targetMiscLbl.Name = "targetMiscLbl";
            this.targetMiscLbl.Size = new System.Drawing.Size(66, 38);
            this.targetMiscLbl.Text = "- - -";
            this.targetMiscLbl.ToolTipText = "🡻 - Depth of the target\r\n⚡ - Battery voltage\r\n🌡 - Water temperature";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 41);
            // 
            // plotToolStrip
            // 
            this.plotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tracksToFitCbx,
            this.isStatisticsBtn});
            this.plotToolStrip.Location = new System.Drawing.Point(0, 112);
            this.plotToolStrip.Name = "plotToolStrip";
            this.plotToolStrip.Size = new System.Drawing.Size(877, 28);
            this.plotToolStrip.TabIndex = 5;
            this.plotToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(115, 25);
            this.toolStripLabel3.Text = "TRACKS TO FIT";
            // 
            // tracksToFitCbx
            // 
            this.tracksToFitCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tracksToFitCbx.DropDownWidth = 200;
            this.tracksToFitCbx.Name = "tracksToFitCbx";
            this.tracksToFitCbx.Size = new System.Drawing.Size(200, 28);
            this.tracksToFitCbx.SelectedIndexChanged += new System.EventHandler(this.tracksToFitCbx_SelectedIndexChanged);
            // 
            // geoPlot
            // 
            this.geoPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.geoPlot.BackColor = System.Drawing.Color.Beige;
            this.geoPlot.HistoryLinesNumber = 5;
            this.geoPlot.HistoryTextColor = System.Drawing.Color.ForestGreen;
            this.geoPlot.HistoryTextFont = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.HistoryVisible = true;
            this.geoPlot.LeftUpperText = null;
            this.geoPlot.LeftUpperTextColor = System.Drawing.Color.DarkBlue;
            this.geoPlot.LeftUpperTextFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.LegendFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.Location = new System.Drawing.Point(13, 145);
            this.geoPlot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.geoPlot.MaxHistoryLineLength = 127;
            this.geoPlot.MeasurementLineColor = System.Drawing.Color.Black;
            this.geoPlot.MeasurementTextBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.geoPlot.MeasurementTextColor = System.Drawing.Color.Black;
            this.geoPlot.MeasurementTextFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.Name = "geoPlot";
            this.geoPlot.Padding = new System.Windows.Forms.Padding(14, 18, 14, 18);
            this.geoPlot.ScaleLineColor = System.Drawing.SystemColors.ControlText;
            this.geoPlot.ScaleLineFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.Size = new System.Drawing.Size(851, 390);
            this.geoPlot.TabIndex = 6;
            this.geoPlot.TextBackgroundColor = System.Drawing.Color.Wheat;
            // 
            // isStatisticsBtn
            // 
            this.isStatisticsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isStatisticsBtn.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isStatisticsBtn.Image = ((System.Drawing.Image)(resources.GetObject("isStatisticsBtn.Image")));
            this.isStatisticsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isStatisticsBtn.Name = "isStatisticsBtn";
            this.isStatisticsBtn.Size = new System.Drawing.Size(92, 25);
            this.isStatisticsBtn.Text = "STATISTICS";
            this.isStatisticsBtn.Click += new System.EventHandler(this.isStatisticsBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 568);
            this.Controls.Add(this.geoPlot);
            this.Controls.Add(this.plotToolStrip);
            this.Controls.Add(this.auxToolStrip);
            this.Controls.Add(this.targetToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "RWLT Host";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.targetToolStrip.ResumeLayout(false);
            this.targetToolStrip.PerformLayout();
            this.auxToolStrip.ResumeLayout(false);
            this.auxToolStrip.PerformLayout();
            this.plotToolStrip.ResumeLayout(false);
            this.plotToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripButton connectionBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton settingsBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton infoBtn;
        private System.Windows.Forms.ToolStripDropDownButton trackBtn;
        private System.Windows.Forms.ToolStripDropDownButton logBtn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel mainPortStatusLbl;
        private System.Windows.Forms.ToolStripMenuItem trackExportBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem trackClearBtn;
        private System.Windows.Forms.ToolStripMenuItem logViewCurrentBtn;
        private System.Windows.Forms.ToolStripMenuItem logAnalyzeCurrentBtn;
        private System.Windows.Forms.ToolStripMenuItem logAnalyzeBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem logClearAllEntriesBtn;
        private System.Windows.Forms.ToolStrip targetToolStrip;
        private System.Windows.Forms.ToolStripStatusLabel auxGNSSStatusCaptionLbl;
        private System.Windows.Forms.ToolStripStatusLabel auxGNSSStatusLbl;
        private System.Windows.Forms.ToolStripLabel auxCapLbl;
        private System.Windows.Forms.ToolStripLabel auxCrsLbl;
        private System.Windows.Forms.ToolStripButton emulatorBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tbaLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton isScreenShotsBtn;
        private System.Windows.Forms.ToolStripButton markCurrentBtn;
        private System.Windows.Forms.ToolStrip auxToolStrip;
        private System.Windows.Forms.ToolStripLabel targetCapLbl;
        private System.Windows.Forms.ToolStripLabel targetCrsLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel auxMiscLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel targetMiscLbl;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel hdopLbl;
        private System.Windows.Forms.ToolStrip plotToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox tracksToFitCbx;
        private UCNLUI.Controls.uOSMGeoPlot geoPlot;
        private System.Windows.Forms.ToolStripButton isStatisticsBtn;
    }
}

