namespace RWLT_Host
{
    partial class SettingsEditor
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.basesPortGoup = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inportBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inportNameCbx = new System.Windows.Forms.ComboBox();
            this.outputPortGroup = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.outportBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.outportNameCbx = new System.Windows.Forms.ComboBox();
            this.isUseOutputPortChb = new System.Windows.Forms.CheckBox();
            this.wpGroup = new System.Windows.Forms.GroupBox();
            this.styDialogLnk = new System.Windows.Forms.LinkLabel();
            this.soundSpeedEdit = new System.Windows.Forms.NumericUpDown();
            this.soundSpeedEditTitleLbl = new System.Windows.Forms.Label();
            this.isAutoSoundSpeedChb = new System.Windows.Forms.CheckBox();
            this.salinityEdit = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rerrEdit = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.trackFIFOSizeEdit = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.isUseAUXGNSSPortChb = new System.Windows.Forms.CheckBox();
            this.auxGNSSPortGroup = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.auxGNSSPortBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.auxGNSSPortNameCbx = new System.Windows.Forms.ComboBox();
            this.basesPortGoup.SuspendLayout();
            this.outputPortGroup.SuspendLayout();
            this.wpGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.soundSpeedEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rerrEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFIFOSizeEdit)).BeginInit();
            this.auxGNSSPortGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.okBtn.Location = new System.Drawing.Point(440, 551);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(113, 43);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelBtn.Location = new System.Drawing.Point(587, 551);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(113, 43);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "CANCEL";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // basesPortGoup
            // 
            this.basesPortGoup.Controls.Add(this.label2);
            this.basesPortGoup.Controls.Add(this.inportBaudrateCbx);
            this.basesPortGoup.Controls.Add(this.label1);
            this.basesPortGoup.Controls.Add(this.inportNameCbx);
            this.basesPortGoup.Location = new System.Drawing.Point(12, 62);
            this.basesPortGoup.Name = "basesPortGoup";
            this.basesPortGoup.Size = new System.Drawing.Size(225, 207);
            this.basesPortGoup.TabIndex = 2;
            this.basesPortGoup.TabStop = false;
            this.basesPortGoup.Text = "Input port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port baudrate";
            // 
            // inportBaudrateCbx
            // 
            this.inportBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inportBaudrateCbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inportBaudrateCbx.FormattingEnabled = true;
            this.inportBaudrateCbx.Location = new System.Drawing.Point(6, 156);
            this.inportBaudrateCbx.Name = "inportBaudrateCbx";
            this.inportBaudrateCbx.Size = new System.Drawing.Size(213, 36);
            this.inportBaudrateCbx.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port name";
            // 
            // inportNameCbx
            // 
            this.inportNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inportNameCbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inportNameCbx.FormattingEnabled = true;
            this.inportNameCbx.Location = new System.Drawing.Point(6, 71);
            this.inportNameCbx.Name = "inportNameCbx";
            this.inportNameCbx.Size = new System.Drawing.Size(213, 36);
            this.inportNameCbx.TabIndex = 0;
            this.inportNameCbx.SelectedIndexChanged += new System.EventHandler(this.inportNameCbx_SelectedIndexChanged);
            // 
            // outputPortGroup
            // 
            this.outputPortGroup.Controls.Add(this.label3);
            this.outputPortGroup.Controls.Add(this.outportBaudrateCbx);
            this.outputPortGroup.Controls.Add(this.label4);
            this.outputPortGroup.Controls.Add(this.outportNameCbx);
            this.outputPortGroup.Enabled = false;
            this.outputPortGroup.Location = new System.Drawing.Point(243, 62);
            this.outputPortGroup.Name = "outputPortGroup";
            this.outputPortGroup.Size = new System.Drawing.Size(225, 207);
            this.outputPortGroup.TabIndex = 3;
            this.outputPortGroup.TabStop = false;
            this.outputPortGroup.Text = "Output port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 28);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port baudrate";
            // 
            // outportBaudrateCbx
            // 
            this.outportBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outportBaudrateCbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outportBaudrateCbx.FormattingEnabled = true;
            this.outportBaudrateCbx.Location = new System.Drawing.Point(6, 156);
            this.outportBaudrateCbx.Name = "outportBaudrateCbx";
            this.outportBaudrateCbx.Size = new System.Drawing.Size(213, 36);
            this.outportBaudrateCbx.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 28);
            this.label4.TabIndex = 5;
            this.label4.Text = "Port name";
            // 
            // outportNameCbx
            // 
            this.outportNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outportNameCbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outportNameCbx.FormattingEnabled = true;
            this.outportNameCbx.Location = new System.Drawing.Point(6, 71);
            this.outportNameCbx.Name = "outportNameCbx";
            this.outportNameCbx.Size = new System.Drawing.Size(213, 36);
            this.outportNameCbx.TabIndex = 4;
            this.outportNameCbx.SelectedIndexChanged += new System.EventHandler(this.outportNameCbx_SelectedIndexChanged);
            // 
            // isUseOutputPortChb
            // 
            this.isUseOutputPortChb.AutoSize = true;
            this.isUseOutputPortChb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isUseOutputPortChb.Location = new System.Drawing.Point(243, 24);
            this.isUseOutputPortChb.Name = "isUseOutputPortChb";
            this.isUseOutputPortChb.Size = new System.Drawing.Size(185, 32);
            this.isUseOutputPortChb.TabIndex = 4;
            this.isUseOutputPortChb.Text = "Use output port";
            this.isUseOutputPortChb.UseVisualStyleBackColor = true;
            this.isUseOutputPortChb.CheckedChanged += new System.EventHandler(this.isUseOutputPortChb_CheckedChanged);
            // 
            // wpGroup
            // 
            this.wpGroup.Controls.Add(this.styDialogLnk);
            this.wpGroup.Controls.Add(this.soundSpeedEdit);
            this.wpGroup.Controls.Add(this.soundSpeedEditTitleLbl);
            this.wpGroup.Controls.Add(this.isAutoSoundSpeedChb);
            this.wpGroup.Controls.Add(this.salinityEdit);
            this.wpGroup.Controls.Add(this.label5);
            this.wpGroup.Location = new System.Drawing.Point(12, 275);
            this.wpGroup.Name = "wpGroup";
            this.wpGroup.Size = new System.Drawing.Size(225, 250);
            this.wpGroup.TabIndex = 4;
            this.wpGroup.TabStop = false;
            this.wpGroup.Text = "Water properties";
            // 
            // styDialogLnk
            // 
            this.styDialogLnk.AutoSize = true;
            this.styDialogLnk.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styDialogLnk.Location = new System.Drawing.Point(137, 65);
            this.styDialogLnk.Name = "styDialogLnk";
            this.styDialogLnk.Size = new System.Drawing.Size(58, 41);
            this.styDialogLnk.TabIndex = 11;
            this.styDialogLnk.TabStop = true;
            this.styDialogLnk.Text = ". . .";
            this.styDialogLnk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.styDialogLnk_LinkClicked);
            // 
            // soundSpeedEdit
            // 
            this.soundSpeedEdit.DecimalPlaces = 1;
            this.soundSpeedEdit.Enabled = false;
            this.soundSpeedEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.soundSpeedEdit.Location = new System.Drawing.Point(24, 191);
            this.soundSpeedEdit.Maximum = new decimal(new int[] {
            1750,
            0,
            0,
            0});
            this.soundSpeedEdit.Minimum = new decimal(new int[] {
            1350,
            0,
            0,
            0});
            this.soundSpeedEdit.Name = "soundSpeedEdit";
            this.soundSpeedEdit.Size = new System.Drawing.Size(125, 34);
            this.soundSpeedEdit.TabIndex = 10;
            this.soundSpeedEdit.Value = new decimal(new int[] {
            1450,
            0,
            0,
            0});
            // 
            // soundSpeedEditTitleLbl
            // 
            this.soundSpeedEditTitleLbl.AutoSize = true;
            this.soundSpeedEditTitleLbl.Enabled = false;
            this.soundSpeedEditTitleLbl.Location = new System.Drawing.Point(19, 160);
            this.soundSpeedEditTitleLbl.Name = "soundSpeedEditTitleLbl";
            this.soundSpeedEditTitleLbl.Size = new System.Drawing.Size(168, 28);
            this.soundSpeedEditTitleLbl.TabIndex = 9;
            this.soundSpeedEditTitleLbl.Text = "Sound speed, m/s";
            // 
            // isAutoSoundSpeedChb
            // 
            this.isAutoSoundSpeedChb.AutoSize = true;
            this.isAutoSoundSpeedChb.Checked = true;
            this.isAutoSoundSpeedChb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isAutoSoundSpeedChb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isAutoSoundSpeedChb.Location = new System.Drawing.Point(6, 125);
            this.isAutoSoundSpeedChb.Name = "isAutoSoundSpeedChb";
            this.isAutoSoundSpeedChb.Size = new System.Drawing.Size(204, 32);
            this.isAutoSoundSpeedChb.TabIndex = 8;
            this.isAutoSoundSpeedChb.Text = "Auto sound speed";
            this.isAutoSoundSpeedChb.UseVisualStyleBackColor = true;
            this.isAutoSoundSpeedChb.CheckedChanged += new System.EventHandler(this.isAutoSoundSpeedChb_CheckedChanged);
            // 
            // salinityEdit
            // 
            this.salinityEdit.DecimalPlaces = 1;
            this.salinityEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.salinityEdit.Location = new System.Drawing.Point(6, 73);
            this.salinityEdit.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.salinityEdit.Name = "salinityEdit";
            this.salinityEdit.Size = new System.Drawing.Size(125, 34);
            this.salinityEdit.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 28);
            this.label5.TabIndex = 6;
            this.label5.Text = "Salinity, PSU";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rerrEdit);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.trackFIFOSizeEdit);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(243, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 250);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Misc.";
            // 
            // rerrEdit
            // 
            this.rerrEdit.DecimalPlaces = 1;
            this.rerrEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rerrEdit.Location = new System.Drawing.Point(22, 191);
            this.rerrEdit.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.rerrEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.rerrEdit.Name = "rerrEdit";
            this.rerrEdit.Size = new System.Drawing.Size(125, 34);
            this.rerrEdit.TabIndex = 12;
            this.rerrEdit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(228, 28);
            this.label10.TabIndex = 11;
            this.label10.Text = "Radial error threshold, m";
            // 
            // trackFIFOSizeEdit
            // 
            this.trackFIFOSizeEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trackFIFOSizeEdit.Location = new System.Drawing.Point(22, 73);
            this.trackFIFOSizeEdit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.trackFIFOSizeEdit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.trackFIFOSizeEdit.Name = "trackFIFOSizeEdit";
            this.trackFIFOSizeEdit.Size = new System.Drawing.Size(103, 34);
            this.trackFIFOSizeEdit.TabIndex = 10;
            this.trackFIFOSizeEdit.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(190, 28);
            this.label7.TabIndex = 9;
            this.label7.Text = "Track points to show";
            // 
            // isUseAUXGNSSPortChb
            // 
            this.isUseAUXGNSSPortChb.AutoSize = true;
            this.isUseAUXGNSSPortChb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isUseAUXGNSSPortChb.Location = new System.Drawing.Point(474, 24);
            this.isUseAUXGNSSPortChb.Name = "isUseAUXGNSSPortChb";
            this.isUseAUXGNSSPortChb.Size = new System.Drawing.Size(219, 32);
            this.isUseAUXGNSSPortChb.TabIndex = 13;
            this.isUseAUXGNSSPortChb.Text = "Use AUX GNSS Port";
            this.isUseAUXGNSSPortChb.UseVisualStyleBackColor = true;
            this.isUseAUXGNSSPortChb.CheckedChanged += new System.EventHandler(this.isUseAUXGNSSPortChb_CheckedChanged);
            // 
            // auxGNSSPortGroup
            // 
            this.auxGNSSPortGroup.Controls.Add(this.label8);
            this.auxGNSSPortGroup.Controls.Add(this.auxGNSSPortBaudrateCbx);
            this.auxGNSSPortGroup.Controls.Add(this.label9);
            this.auxGNSSPortGroup.Controls.Add(this.auxGNSSPortNameCbx);
            this.auxGNSSPortGroup.Enabled = false;
            this.auxGNSSPortGroup.Location = new System.Drawing.Point(474, 62);
            this.auxGNSSPortGroup.Name = "auxGNSSPortGroup";
            this.auxGNSSPortGroup.Size = new System.Drawing.Size(225, 207);
            this.auxGNSSPortGroup.TabIndex = 12;
            this.auxGNSSPortGroup.TabStop = false;
            this.auxGNSSPortGroup.Text = "AUX GNSS Port";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 28);
            this.label8.TabIndex = 7;
            this.label8.Text = "Port baudrate";
            // 
            // auxGNSSPortBaudrateCbx
            // 
            this.auxGNSSPortBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auxGNSSPortBaudrateCbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.auxGNSSPortBaudrateCbx.FormattingEnabled = true;
            this.auxGNSSPortBaudrateCbx.Location = new System.Drawing.Point(6, 156);
            this.auxGNSSPortBaudrateCbx.Name = "auxGNSSPortBaudrateCbx";
            this.auxGNSSPortBaudrateCbx.Size = new System.Drawing.Size(213, 36);
            this.auxGNSSPortBaudrateCbx.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 28);
            this.label9.TabIndex = 5;
            this.label9.Text = "Port name";
            // 
            // auxGNSSPortNameCbx
            // 
            this.auxGNSSPortNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auxGNSSPortNameCbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.auxGNSSPortNameCbx.FormattingEnabled = true;
            this.auxGNSSPortNameCbx.Location = new System.Drawing.Point(6, 71);
            this.auxGNSSPortNameCbx.Name = "auxGNSSPortNameCbx";
            this.auxGNSSPortNameCbx.Size = new System.Drawing.Size(213, 36);
            this.auxGNSSPortNameCbx.TabIndex = 4;
            this.auxGNSSPortNameCbx.SelectedIndexChanged += new System.EventHandler(this.auxGNSSPortNameCbx_SelectedIndexChanged);
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 606);
            this.Controls.Add(this.isUseAUXGNSSPortChb);
            this.Controls.Add(this.auxGNSSPortGroup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.wpGroup);
            this.Controls.Add(this.isUseOutputPortChb);
            this.Controls.Add(this.outputPortGroup);
            this.Controls.Add(this.basesPortGoup);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsEditor";
            this.Text = "RWLT Host - Settings";
            this.basesPortGoup.ResumeLayout(false);
            this.basesPortGoup.PerformLayout();
            this.outputPortGroup.ResumeLayout(false);
            this.outputPortGroup.PerformLayout();
            this.wpGroup.ResumeLayout(false);
            this.wpGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.soundSpeedEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rerrEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFIFOSizeEdit)).EndInit();
            this.auxGNSSPortGroup.ResumeLayout(false);
            this.auxGNSSPortGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox basesPortGoup;
        private System.Windows.Forms.GroupBox outputPortGroup;
        private System.Windows.Forms.CheckBox isUseOutputPortChb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox inportBaudrateCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox inportNameCbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox outportBaudrateCbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox outportNameCbx;
        private System.Windows.Forms.GroupBox wpGroup;
        private System.Windows.Forms.NumericUpDown salinityEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown soundSpeedEdit;
        private System.Windows.Forms.Label soundSpeedEditTitleLbl;
        private System.Windows.Forms.CheckBox isAutoSoundSpeedChb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown trackFIFOSizeEdit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox isUseAUXGNSSPortChb;
        private System.Windows.Forms.GroupBox auxGNSSPortGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox auxGNSSPortBaudrateCbx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox auxGNSSPortNameCbx;
        private System.Windows.Forms.NumericUpDown rerrEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel styDialogLnk;
    }
}