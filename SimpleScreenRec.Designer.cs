namespace SimpleScreenRecorder
{
    partial class ScreenRecorder
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox comboBoxMic;
        private System.Windows.Forms.Label labelMic;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenRecorder));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.comboBoxMic = new System.Windows.Forms.ComboBox();
            this.labelMic = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.trackBarQuality = new System.Windows.Forms.TrackBar();
            this.lblQualityValue = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.Button();
            this.hideBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRecordSystemAudio = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fpsSelect = new System.Windows.Forms.NumericUpDown();
            this.fpsLbl = new System.Windows.Forms.Label();
            this.appverLbl = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.hideonrecordChkBox = new System.Windows.Forms.CheckBox();
            this.dspLbl = new System.Windows.Forms.Label();
            this.numericUpDownMonitor = new System.Windows.Forms.NumericUpDown();
            this.savePathSection = new System.Windows.Forms.GroupBox();
            this.videoOptionsSection = new System.Windows.Forms.GroupBox();
            this.cbrCheck = new System.Windows.Forms.CheckBox();
            this.vbrCheck = new System.Windows.Forms.CheckBox();
            this.comboBoxCodec = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonitor)).BeginInit();
            this.savePathSection.SuspendLayout();
            this.videoOptionsSection.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(75)))), ((int)(((byte)(141)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MidnightBlue;
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SteelBlue;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(317, 327);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(168, 36);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start Recording";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Sienna;
            this.btnStop.Enabled = false;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(15, 327);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(170, 36);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop Recording";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // comboBoxMic
            // 
            this.comboBoxMic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMic.Location = new System.Drawing.Point(94, 93);
            this.comboBoxMic.Name = "comboBoxMic";
            this.comboBoxMic.Size = new System.Drawing.Size(270, 22);
            this.comboBoxMic.TabIndex = 1;
            // 
            // labelMic
            // 
            this.labelMic.AutoSize = true;
            this.labelMic.ForeColor = System.Drawing.Color.White;
            this.labelMic.Location = new System.Drawing.Point(22, 97);
            this.labelMic.Name = "labelMic";
            this.labelMic.Size = new System.Drawing.Size(66, 14);
            this.labelMic.TabIndex = 0;
            this.labelMic.Text = "Microphone:";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(10, 24);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(339, 21);
            this.txtPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(357, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 21);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(15, 379);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(470, 24);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status: Idle";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // trackBarQuality
            // 
            this.trackBarQuality.LargeChange = 1;
            this.trackBarQuality.Location = new System.Drawing.Point(3, 44);
            this.trackBarQuality.Maximum = 5;
            this.trackBarQuality.Minimum = 1;
            this.trackBarQuality.Name = "trackBarQuality";
            this.trackBarQuality.Size = new System.Drawing.Size(185, 45);
            this.trackBarQuality.TabIndex = 8;
            this.trackBarQuality.Value = 2;
            this.trackBarQuality.Scroll += new System.EventHandler(this.TrackBarQuality_Scroll);
            // 
            // lblQualityValue
            // 
            this.lblQualityValue.ForeColor = System.Drawing.Color.White;
            this.lblQualityValue.Location = new System.Drawing.Point(10, 21);
            this.lblQualityValue.Name = "lblQualityValue";
            this.lblQualityValue.Size = new System.Drawing.Size(178, 20);
            this.lblQualityValue.TabIndex = 9;
            this.lblQualityValue.Text = "Quality: Medium (10 Mbps)";
            this.lblQualityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.Chocolate;
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Firebrick;
            this.exitBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.ForeColor = System.Drawing.Color.White;
            this.exitBtn.Location = new System.Drawing.Point(426, 15);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(59, 23);
            this.exitBtn.TabIndex = 12;
            this.exitBtn.Text = "EXIT";
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // hideBtn
            // 
            this.hideBtn.BackColor = System.Drawing.Color.SeaGreen;
            this.hideBtn.FlatAppearance.BorderSize = 0;
            this.hideBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.hideBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSeaGreen;
            this.hideBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtn.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideBtn.ForeColor = System.Drawing.Color.White;
            this.hideBtn.Location = new System.Drawing.Point(361, 15);
            this.hideBtn.Name = "hideBtn";
            this.hideBtn.Size = new System.Drawing.Size(59, 23);
            this.hideBtn.TabIndex = 13;
            this.hideBtn.Text = "HIDE";
            this.hideBtn.UseVisualStyleBackColor = false;
            this.hideBtn.Click += new System.EventHandler(this.HideBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SimpleScreenRecorder.Properties.Resources.icon_naetech_small;
            this.pictureBox1.Location = new System.Drawing.Point(15, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(170, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Simple Screen Recorder";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // cbRecordSystemAudio
            // 
            this.cbRecordSystemAudio.AutoSize = true;
            this.cbRecordSystemAudio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRecordSystemAudio.Checked = true;
            this.cbRecordSystemAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRecordSystemAudio.ForeColor = System.Drawing.Color.White;
            this.cbRecordSystemAudio.Location = new System.Drawing.Point(357, 26);
            this.cbRecordSystemAudio.Name = "cbRecordSystemAudio";
            this.cbRecordSystemAudio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbRecordSystemAudio.Size = new System.Drawing.Size(102, 18);
            this.cbRecordSystemAudio.TabIndex = 16;
            this.cbRecordSystemAudio.Text = "Windows Audio";
            this.cbRecordSystemAudio.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // fpsSelect
            // 
            this.fpsSelect.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpsSelect.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.fpsSelect.Location = new System.Drawing.Point(399, 57);
            this.fpsSelect.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.fpsSelect.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.fpsSelect.Name = "fpsSelect";
            this.fpsSelect.Size = new System.Drawing.Size(60, 21);
            this.fpsSelect.TabIndex = 17;
            this.fpsSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fpsSelect.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // fpsLbl
            // 
            this.fpsLbl.AutoSize = true;
            this.fpsLbl.ForeColor = System.Drawing.Color.White;
            this.fpsLbl.Location = new System.Drawing.Point(337, 60);
            this.fpsLbl.Name = "fpsLbl";
            this.fpsLbl.Size = new System.Drawing.Size(59, 14);
            this.fpsLbl.TabIndex = 18;
            this.fpsLbl.Text = "Framerate:";
            // 
            // appverLbl
            // 
            this.appverLbl.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appverLbl.ForeColor = System.Drawing.Color.Gray;
            this.appverLbl.Location = new System.Drawing.Point(172, 37);
            this.appverLbl.Name = "appverLbl";
            this.appverLbl.Size = new System.Drawing.Size(156, 13);
            this.appverLbl.TabIndex = 19;
            this.appverLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.appverLbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
            // 
            // hideonrecordChkBox
            // 
            this.hideonrecordChkBox.AutoSize = true;
            this.hideonrecordChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hideonrecordChkBox.ForeColor = System.Drawing.Color.White;
            this.hideonrecordChkBox.Location = new System.Drawing.Point(263, 336);
            this.hideonrecordChkBox.Name = "hideonrecordChkBox";
            this.hideonrecordChkBox.Size = new System.Drawing.Size(47, 18);
            this.hideonrecordChkBox.TabIndex = 20;
            this.hideonrecordChkBox.Text = "Hide";
            this.hideonrecordChkBox.UseVisualStyleBackColor = true;
            // 
            // dspLbl
            // 
            this.dspLbl.AutoSize = true;
            this.dspLbl.ForeColor = System.Drawing.Color.White;
            this.dspLbl.Location = new System.Drawing.Point(194, 60);
            this.dspLbl.Name = "dspLbl";
            this.dspLbl.Size = new System.Drawing.Size(45, 14);
            this.dspLbl.TabIndex = 22;
            this.dspLbl.Text = "Display:";
            // 
            // numericUpDownMonitor
            // 
            this.numericUpDownMonitor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMonitor.Location = new System.Drawing.Point(243, 57);
            this.numericUpDownMonitor.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMonitor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMonitor.Name = "numericUpDownMonitor";
            this.numericUpDownMonitor.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownMonitor.TabIndex = 21;
            this.numericUpDownMonitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownMonitor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // savePathSection
            // 
            this.savePathSection.Controls.Add(this.txtPath);
            this.savePathSection.Controls.Add(this.btnBrowse);
            this.savePathSection.ForeColor = System.Drawing.Color.White;
            this.savePathSection.Location = new System.Drawing.Point(15, 248);
            this.savePathSection.Name = "savePathSection";
            this.savePathSection.Size = new System.Drawing.Size(470, 64);
            this.savePathSection.TabIndex = 23;
            this.savePathSection.TabStop = false;
            this.savePathSection.Text = "Save Path";
            // 
            // videoOptionsSection
            // 
            this.videoOptionsSection.Controls.Add(this.cbrCheck);
            this.videoOptionsSection.Controls.Add(this.vbrCheck);
            this.videoOptionsSection.Controls.Add(this.comboBoxCodec);
            this.videoOptionsSection.Controls.Add(this.label2);
            this.videoOptionsSection.Controls.Add(this.trackBarQuality);
            this.videoOptionsSection.Controls.Add(this.numericUpDownMonitor);
            this.videoOptionsSection.Controls.Add(this.dspLbl);
            this.videoOptionsSection.Controls.Add(this.fpsLbl);
            this.videoOptionsSection.Controls.Add(this.fpsSelect);
            this.videoOptionsSection.Controls.Add(this.lblQualityValue);
            this.videoOptionsSection.ForeColor = System.Drawing.Color.White;
            this.videoOptionsSection.Location = new System.Drawing.Point(15, 145);
            this.videoOptionsSection.Name = "videoOptionsSection";
            this.videoOptionsSection.Size = new System.Drawing.Size(470, 94);
            this.videoOptionsSection.TabIndex = 24;
            this.videoOptionsSection.TabStop = false;
            this.videoOptionsSection.Text = "Video Options";
            // 
            // cbrCheck
            // 
            this.cbrCheck.AutoSize = true;
            this.cbrCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbrCheck.ForeColor = System.Drawing.Color.White;
            this.cbrCheck.Location = new System.Drawing.Point(359, 25);
            this.cbrCheck.Name = "cbrCheck";
            this.cbrCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbrCheck.Size = new System.Drawing.Size(47, 18);
            this.cbrCheck.TabIndex = 26;
            this.cbrCheck.Text = "CBR";
            this.cbrCheck.UseVisualStyleBackColor = true;
            this.cbrCheck.CheckedChanged += new System.EventHandler(this.CbrCheck_CheckedChanged);
            // 
            // vbrCheck
            // 
            this.vbrCheck.AutoSize = true;
            this.vbrCheck.Checked = true;
            this.vbrCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vbrCheck.ForeColor = System.Drawing.Color.White;
            this.vbrCheck.Location = new System.Drawing.Point(412, 25);
            this.vbrCheck.Name = "vbrCheck";
            this.vbrCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.vbrCheck.Size = new System.Drawing.Size(48, 18);
            this.vbrCheck.TabIndex = 17;
            this.vbrCheck.Text = "VBR";
            this.vbrCheck.UseVisualStyleBackColor = true;
            this.vbrCheck.CheckedChanged += new System.EventHandler(this.VbrCheck_CheckedChanged);
            // 
            // comboBoxCodec
            // 
            this.comboBoxCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodec.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCodec.Items.AddRange(new object[] {
            "H.264 (AVC)",
            "H.265 (HEVC)"});
            this.comboBoxCodec.Location = new System.Drawing.Point(243, 23);
            this.comboBoxCodec.Name = "comboBoxCodec";
            this.comboBoxCodec.Size = new System.Drawing.Size(106, 22);
            this.comboBoxCodec.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(194, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 23;
            this.label2.Text = "Codec:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRecordSystemAudio);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 64);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Options";
            // 
            // ScreenRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(500, 420);
            this.Controls.Add(this.videoOptionsSection);
            this.Controls.Add(this.savePathSection);
            this.Controls.Add(this.hideonrecordChkBox);
            this.Controls.Add(this.appverLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.hideBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.labelMic);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.comboBoxMic);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScreenRecorder";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Recorder by NaeTech";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenRecorder_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonitor)).EndInit();
            this.savePathSection.ResumeLayout(false);
            this.savePathSection.PerformLayout();
            this.videoOptionsSection.ResumeLayout(false);
            this.videoOptionsSection.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TrackBar trackBarQuality;
        private System.Windows.Forms.Label lblQualityValue;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button hideBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbRecordSystemAudio;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown fpsSelect;
        private System.Windows.Forms.Label fpsLbl;
        private System.Windows.Forms.Label appverLbl;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox hideonrecordChkBox;
        private System.Windows.Forms.Label dspLbl;
        private System.Windows.Forms.NumericUpDown numericUpDownMonitor;
        private System.Windows.Forms.GroupBox savePathSection;
        private System.Windows.Forms.GroupBox videoOptionsSection;
        private System.Windows.Forms.ComboBox comboBoxCodec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox vbrCheck;
        private System.Windows.Forms.CheckBox cbrCheck;
    }
}
