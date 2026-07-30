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
        private System.Windows.Forms.Button btnSelectArea;
        private System.Windows.Forms.Button btnClearArea;

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
            this.btnSelectArea = new System.Windows.Forms.Button();
            this.btnClearArea = new System.Windows.Forms.Button();
            this.trackBarQuality = new System.Windows.Forms.TrackBar();
            this.lblQualityValue = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.Button();
            this.hideBtn = new System.Windows.Forms.Button();
            this.appLogo = new System.Windows.Forms.PictureBox();
            this.appName = new System.Windows.Forms.Label();
            this.cbRecordSystemAudio = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.recordingTimer = new System.Windows.Forms.Timer(this.components);
            this.comboBoxFps = new System.Windows.Forms.ComboBox();
            this.fpsLbl = new System.Windows.Forms.Label();
            this.appverLbl = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.hideonrecordChkBox = new System.Windows.Forms.CheckBox();
            this.countdownChkBox = new System.Windows.Forms.CheckBox();
            this.dspLbl = new System.Windows.Forms.Label();
            this.savePathSection = new System.Windows.Forms.GroupBox();
            this.showPath = new System.Windows.Forms.Button();
            this.videoOptionsSection = new System.Windows.Forms.GroupBox();
            this.areaStatusLabel = new System.Windows.Forms.Label();
            this.comboBoxMonitor = new System.Windows.Forms.ComboBox();
            this.cbrCheck = new System.Windows.Forms.CheckBox();
            this.vbrCheck = new System.Windows.Forms.CheckBox();
            this.comboBoxCodec = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.audioOptionsSection = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showCtxMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showRecordingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLastRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitCtxMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.topBar = new System.Windows.Forms.Panel();
            this.lblRecordingTimer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).BeginInit();
            this.savePathSection.SuspendLayout();
            this.videoOptionsSection.SuspendLayout();
            this.audioOptionsSection.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.topBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Crimson;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.LightGray;
            this.btnStart.Location = new System.Drawing.Point(409, 389);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(166, 36);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "⚫ Start Recording (F9)";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.SlateBlue;
            this.btnStop.Enabled = false;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.LightGray;
            this.btnStop.Location = new System.Drawing.Point(25, 389);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(170, 36);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "■ Stop Recording (F10)";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // comboBoxMic
            // 
            this.comboBoxMic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMic.ForeColor = System.Drawing.Color.Black;
            this.comboBoxMic.Location = new System.Drawing.Point(96, 24);
            this.comboBoxMic.Name = "comboBoxMic";
            this.comboBoxMic.Size = new System.Drawing.Size(317, 21);
            this.comboBoxMic.TabIndex = 1;
            // 
            // labelMic
            // 
            this.labelMic.AutoSize = true;
            this.labelMic.ForeColor = System.Drawing.Color.LightGray;
            this.labelMic.Location = new System.Drawing.Point(17, 28);
            this.labelMic.Name = "labelMic";
            this.labelMic.Size = new System.Drawing.Size(73, 13);
            this.labelMic.TabIndex = 0;
            this.labelMic.Text = "Microphone:";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.ForeColor = System.Drawing.Color.Black;
            this.txtPath.Location = new System.Drawing.Point(17, 25);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(338, 23);
            this.txtPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.White;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(365, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(78, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "📂 Select";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(42)))));
            this.lblStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatus.Location = new System.Drawing.Point(25, 448);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(369, 30);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status: Idle";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // btnSelectArea
            // 
            this.btnSelectArea.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSelectArea.FlatAppearance.BorderSize = 0;
            this.btnSelectArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectArea.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectArea.ForeColor = System.Drawing.Color.LightGray;
            this.btnSelectArea.Location = new System.Drawing.Point(238, 88);
            this.btnSelectArea.Name = "btnSelectArea";
            this.btnSelectArea.Size = new System.Drawing.Size(108, 23);
            this.btnSelectArea.TabIndex = 27;
            this.btnSelectArea.Text = "⛶ Custom area";
            this.btnSelectArea.UseVisualStyleBackColor = false;
            this.btnSelectArea.Click += new System.EventHandler(this.BtnSelectArea_Click);
            // 
            // btnClearArea
            // 
            this.btnClearArea.BackColor = System.Drawing.Color.Crimson;
            this.btnClearArea.Enabled = false;
            this.btnClearArea.FlatAppearance.BorderSize = 0;
            this.btnClearArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearArea.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearArea.ForeColor = System.Drawing.Color.LightGray;
            this.btnClearArea.Location = new System.Drawing.Point(352, 88);
            this.btnClearArea.Name = "btnClearArea";
            this.btnClearArea.Size = new System.Drawing.Size(61, 23);
            this.btnClearArea.TabIndex = 28;
            this.btnClearArea.Text = "🗑 Clear";
            this.btnClearArea.UseVisualStyleBackColor = false;
            this.btnClearArea.Click += new System.EventHandler(this.BtnClearArea_Click);
            // 
            // trackBarQuality
            // 
            this.trackBarQuality.LargeChange = 1;
            this.trackBarQuality.Location = new System.Drawing.Point(17, 57);
            this.trackBarQuality.Minimum = 1;
            this.trackBarQuality.Name = "trackBarQuality";
            this.trackBarQuality.Size = new System.Drawing.Size(153, 45);
            this.trackBarQuality.TabIndex = 8;
            this.trackBarQuality.Value = 6;
            this.trackBarQuality.Scroll += new System.EventHandler(this.TrackBarQuality_Scroll);
            // 
            // lblQualityValue
            // 
            this.lblQualityValue.ForeColor = System.Drawing.Color.LightGray;
            this.lblQualityValue.Location = new System.Drawing.Point(15, 90);
            this.lblQualityValue.Name = "lblQualityValue";
            this.lblQualityValue.Size = new System.Drawing.Size(157, 20);
            this.lblQualityValue.TabIndex = 9;
            this.lblQualityValue.Text = "Quality: Better (16 Mbps)";
            this.lblQualityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(24)))));
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.ForeColor = System.Drawing.Color.White;
            this.exitBtn.Location = new System.Drawing.Point(529, 1);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(70, 65);
            this.exitBtn.TabIndex = 12;
            this.exitBtn.Text = "🗙";
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // hideBtn
            // 
            this.hideBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(24)))));
            this.hideBtn.FlatAppearance.BorderSize = 0;
            this.hideBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.hideBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideBtn.ForeColor = System.Drawing.Color.White;
            this.hideBtn.Location = new System.Drawing.Point(459, 1);
            this.hideBtn.Name = "hideBtn";
            this.hideBtn.Size = new System.Drawing.Size(70, 65);
            this.hideBtn.TabIndex = 13;
            this.hideBtn.Text = "🗕";
            this.hideBtn.UseVisualStyleBackColor = false;
            this.hideBtn.Click += new System.EventHandler(this.HideBtn_Click);
            // 
            // appLogo
            // 
            this.appLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(24)))));
            this.appLogo.Image = global::SimpleScreenRecorder.Properties.Resources.screenrec;
            this.appLogo.Location = new System.Drawing.Point(25, 16);
            this.appLogo.Name = "appLogo";
            this.appLogo.Size = new System.Drawing.Size(35, 35);
            this.appLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.appLogo.TabIndex = 14;
            this.appLogo.TabStop = false;
            this.appLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // appName
            // 
            this.appName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(24)))));
            this.appName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appName.ForeColor = System.Drawing.Color.Silver;
            this.appName.Location = new System.Drawing.Point(63, 13);
            this.appName.Name = "appName";
            this.appName.Size = new System.Drawing.Size(390, 27);
            this.appName.TabIndex = 15;
            this.appName.Text = "Simple Screen Recorder";
            this.appName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // cbRecordSystemAudio
            // 
            this.cbRecordSystemAudio.AutoSize = true;
            this.cbRecordSystemAudio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRecordSystemAudio.Checked = true;
            this.cbRecordSystemAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRecordSystemAudio.ForeColor = System.Drawing.Color.LightGray;
            this.cbRecordSystemAudio.Location = new System.Drawing.Point(424, 26);
            this.cbRecordSystemAudio.Name = "cbRecordSystemAudio";
            this.cbRecordSystemAudio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbRecordSystemAudio.Size = new System.Drawing.Size(109, 17);
            this.cbRecordSystemAudio.TabIndex = 16;
            this.cbRecordSystemAudio.Text = "Windows Audio";
            this.cbRecordSystemAudio.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // recordingTimer
            // 
            this.recordingTimer.Interval = 1000;
            this.recordingTimer.Tick += new System.EventHandler(this.RecordingTimer_Tick);
            // 
            // comboBoxFps
            // 
            this.comboBoxFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFps.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFps.ForeColor = System.Drawing.Color.Black;
            this.comboBoxFps.FormattingEnabled = true;
            this.comboBoxFps.Items.AddRange(new object[] {
            "30 FPS - Standard",
            "60 FPS - Smooth",
            "120 FPS - Very Smooth"});
            this.comboBoxFps.Location = new System.Drawing.Point(238, 24);
            this.comboBoxFps.Name = "comboBoxFps";
            this.comboBoxFps.Size = new System.Drawing.Size(175, 21);
            this.comboBoxFps.TabIndex = 17;
            // 
            // fpsLbl
            // 
            this.fpsLbl.AutoSize = true;
            this.fpsLbl.ForeColor = System.Drawing.Color.LightGray;
            this.fpsLbl.Location = new System.Drawing.Point(203, 27);
            this.fpsLbl.Name = "fpsLbl";
            this.fpsLbl.Size = new System.Drawing.Size(28, 13);
            this.fpsLbl.TabIndex = 18;
            this.fpsLbl.Text = "FPS:";
            // 
            // appverLbl
            // 
            this.appverLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(24)))));
            this.appverLbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appverLbl.ForeColor = System.Drawing.Color.Gray;
            this.appverLbl.Location = new System.Drawing.Point(64, 38);
            this.appverLbl.Name = "appverLbl";
            this.appverLbl.Size = new System.Drawing.Size(388, 13);
            this.appverLbl.TabIndex = 19;
            this.appverLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.appverLbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Simple Screen Recorder";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // hideonrecordChkBox
            // 
            this.hideonrecordChkBox.AutoSize = true;
            this.hideonrecordChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hideonrecordChkBox.ForeColor = System.Drawing.Color.LightGray;
            this.hideonrecordChkBox.Location = new System.Drawing.Point(315, 399);
            this.hideonrecordChkBox.Name = "hideonrecordChkBox";
            this.hideonrecordChkBox.Size = new System.Drawing.Size(79, 17);
            this.hideonrecordChkBox.TabIndex = 20;
            this.hideonrecordChkBox.Text = "Auto-Hide";
            this.hideonrecordChkBox.UseVisualStyleBackColor = true;
            // 
            // countdownChkBox
            // 
            this.countdownChkBox.AutoSize = true;
            this.countdownChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.countdownChkBox.ForeColor = System.Drawing.Color.LightGray;
            this.countdownChkBox.Location = new System.Drawing.Point(218, 399);
            this.countdownChkBox.Name = "countdownChkBox";
            this.countdownChkBox.Size = new System.Drawing.Size(88, 17);
            this.countdownChkBox.TabIndex = 21;
            this.countdownChkBox.Text = "Countdown";
            this.countdownChkBox.UseVisualStyleBackColor = true;
            // 
            // dspLbl
            // 
            this.dspLbl.AutoSize = true;
            this.dspLbl.ForeColor = System.Drawing.Color.LightGray;
            this.dspLbl.Location = new System.Drawing.Point(184, 60);
            this.dspLbl.Name = "dspLbl";
            this.dspLbl.Size = new System.Drawing.Size(47, 13);
            this.dspLbl.TabIndex = 22;
            this.dspLbl.Text = "Display:";
            // 
            // savePathSection
            // 
            this.savePathSection.Controls.Add(this.showPath);
            this.savePathSection.Controls.Add(this.txtPath);
            this.savePathSection.Controls.Add(this.btnBrowse);
            this.savePathSection.ForeColor = System.Drawing.Color.LightGray;
            this.savePathSection.Location = new System.Drawing.Point(25, 304);
            this.savePathSection.Name = "savePathSection";
            this.savePathSection.Size = new System.Drawing.Size(550, 64);
            this.savePathSection.TabIndex = 23;
            this.savePathSection.TabStop = false;
            this.savePathSection.Text = "Save Path";
            // 
            // showPath
            // 
            this.showPath.BackColor = System.Drawing.Color.White;
            this.showPath.FlatAppearance.BorderSize = 0;
            this.showPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.showPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.showPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPath.ForeColor = System.Drawing.Color.Black;
            this.showPath.Location = new System.Drawing.Point(454, 25);
            this.showPath.Name = "showPath";
            this.showPath.Size = new System.Drawing.Size(78, 23);
            this.showPath.TabIndex = 5;
            this.showPath.Text = "👁 Open";
            this.showPath.UseVisualStyleBackColor = false;
            this.showPath.Click += new System.EventHandler(this.ShowPath_Click);
            // 
            // videoOptionsSection
            // 
            this.videoOptionsSection.Controls.Add(this.areaStatusLabel);
            this.videoOptionsSection.Controls.Add(this.lblQualityValue);
            this.videoOptionsSection.Controls.Add(this.comboBoxMonitor);
            this.videoOptionsSection.Controls.Add(this.cbrCheck);
            this.videoOptionsSection.Controls.Add(this.btnClearArea);
            this.videoOptionsSection.Controls.Add(this.btnSelectArea);
            this.videoOptionsSection.Controls.Add(this.vbrCheck);
            this.videoOptionsSection.Controls.Add(this.comboBoxCodec);
            this.videoOptionsSection.Controls.Add(this.label2);
            this.videoOptionsSection.Controls.Add(this.trackBarQuality);
            this.videoOptionsSection.Controls.Add(this.dspLbl);
            this.videoOptionsSection.Controls.Add(this.fpsLbl);
            this.videoOptionsSection.Controls.Add(this.comboBoxFps);
            this.videoOptionsSection.ForeColor = System.Drawing.Color.LightGray;
            this.videoOptionsSection.Location = new System.Drawing.Point(25, 163);
            this.videoOptionsSection.Name = "videoOptionsSection";
            this.videoOptionsSection.Size = new System.Drawing.Size(550, 128);
            this.videoOptionsSection.TabIndex = 24;
            this.videoOptionsSection.TabStop = false;
            this.videoOptionsSection.Text = "Video Options";
            // 
            // areaStatusLabel
            // 
            this.areaStatusLabel.AutoSize = true;
            this.areaStatusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.areaStatusLabel.Location = new System.Drawing.Point(424, 93);
            this.areaStatusLabel.Name = "areaStatusLabel";
            this.areaStatusLabel.Size = new System.Drawing.Size(95, 13);
            this.areaStatusLabel.TabIndex = 29;
            this.areaStatusLabel.Text = "Area: Full Display";
            // 
            // comboBoxMonitor
            // 
            this.comboBoxMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonitor.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMonitor.ForeColor = System.Drawing.Color.Black;
            this.comboBoxMonitor.Location = new System.Drawing.Point(238, 56);
            this.comboBoxMonitor.Name = "comboBoxMonitor";
            this.comboBoxMonitor.Size = new System.Drawing.Size(175, 21);
            this.comboBoxMonitor.TabIndex = 27;
            // 
            // cbrCheck
            // 
            this.cbrCheck.AutoSize = true;
            this.cbrCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbrCheck.ForeColor = System.Drawing.Color.LightGray;
            this.cbrCheck.Location = new System.Drawing.Point(424, 58);
            this.cbrCheck.Name = "cbrCheck";
            this.cbrCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbrCheck.Size = new System.Drawing.Size(110, 17);
            this.cbrCheck.TabIndex = 26;
            this.cbrCheck.Text = "Constant Bitrate";
            this.cbrCheck.UseVisualStyleBackColor = true;
            this.cbrCheck.CheckedChanged += new System.EventHandler(this.CbrCheck_CheckedChanged);
            // 
            // vbrCheck
            // 
            this.vbrCheck.AutoSize = true;
            this.vbrCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vbrCheck.Checked = true;
            this.vbrCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vbrCheck.ForeColor = System.Drawing.Color.LightGray;
            this.vbrCheck.Location = new System.Drawing.Point(430, 26);
            this.vbrCheck.Name = "vbrCheck";
            this.vbrCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.vbrCheck.Size = new System.Drawing.Size(104, 17);
            this.vbrCheck.TabIndex = 17;
            this.vbrCheck.Text = "Variable Bitrate";
            this.vbrCheck.UseVisualStyleBackColor = true;
            this.vbrCheck.CheckedChanged += new System.EventHandler(this.VbrCheck_CheckedChanged);
            // 
            // comboBoxCodec
            // 
            this.comboBoxCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodec.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCodec.ForeColor = System.Drawing.Color.Black;
            this.comboBoxCodec.Items.AddRange(new object[] {
            "H.264 (AVC)",
            "H.265 (HEVC)"});
            this.comboBoxCodec.Location = new System.Drawing.Point(65, 24);
            this.comboBoxCodec.Name = "comboBoxCodec";
            this.comboBoxCodec.Size = new System.Drawing.Size(109, 21);
            this.comboBoxCodec.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(17, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Codec:";
            // 
            // audioOptionsSection
            // 
            this.audioOptionsSection.Controls.Add(this.cbRecordSystemAudio);
            this.audioOptionsSection.Controls.Add(this.labelMic);
            this.audioOptionsSection.Controls.Add(this.comboBoxMic);
            this.audioOptionsSection.ForeColor = System.Drawing.Color.LightGray;
            this.audioOptionsSection.Location = new System.Drawing.Point(25, 84);
            this.audioOptionsSection.Name = "audioOptionsSection";
            this.audioOptionsSection.Size = new System.Drawing.Size(550, 64);
            this.audioOptionsSection.TabIndex = 24;
            this.audioOptionsSection.TabStop = false;
            this.audioOptionsSection.Text = "Audio Options";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCtxMenu,
            this.showRecordingsToolStripMenuItem,
            this.openLastRecordingToolStripMenuItem,
            this.startRecordingToolStripMenuItem,
            this.stopRecordingToolStripMenuItem,
            this.exitCtxMenu});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(202, 136);
            // 
            // showCtxMenu
            // 
            this.showCtxMenu.Name = "showCtxMenu";
            this.showCtxMenu.Size = new System.Drawing.Size(201, 22);
            this.showCtxMenu.Text = "Show";
            this.showCtxMenu.Click += new System.EventHandler(this.ShowCtxMenu_Click);
            // 
            // showRecordingsToolStripMenuItem
            // 
            this.showRecordingsToolStripMenuItem.Name = "showRecordingsToolStripMenuItem";
            this.showRecordingsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showRecordingsToolStripMenuItem.Text = "Open Recordings Folder";
            this.showRecordingsToolStripMenuItem.Click += new System.EventHandler(this.ShowRecordingsToolStripMenuItem_Click);
            // 
            // openLastRecordingToolStripMenuItem
            // 
            this.openLastRecordingToolStripMenuItem.Enabled = false;
            this.openLastRecordingToolStripMenuItem.Name = "openLastRecordingToolStripMenuItem";
            this.openLastRecordingToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openLastRecordingToolStripMenuItem.Text = "Open Last Recording";
            this.openLastRecordingToolStripMenuItem.Click += new System.EventHandler(this.OpenLastRecordingToolStripMenuItem_Click);
            // 
            // startRecordingToolStripMenuItem
            // 
            this.startRecordingToolStripMenuItem.Name = "startRecordingToolStripMenuItem";
            this.startRecordingToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.startRecordingToolStripMenuItem.Text = "Start Recording";
            this.startRecordingToolStripMenuItem.Click += new System.EventHandler(this.StartRecordingToolStripMenuItem_Click);
            // 
            // stopRecordingToolStripMenuItem
            // 
            this.stopRecordingToolStripMenuItem.Enabled = false;
            this.stopRecordingToolStripMenuItem.Name = "stopRecordingToolStripMenuItem";
            this.stopRecordingToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.stopRecordingToolStripMenuItem.Text = "Stop Recording";
            this.stopRecordingToolStripMenuItem.Click += new System.EventHandler(this.StopRecordingToolStripMenuItem_Click);
            // 
            // exitCtxMenu
            // 
            this.exitCtxMenu.Name = "exitCtxMenu";
            this.exitCtxMenu.Size = new System.Drawing.Size(201, 22);
            this.exitCtxMenu.Text = "Exit";
            this.exitCtxMenu.Click += new System.EventHandler(this.ExitCtxMenu_Click);
            // 
            // topBar
            // 
            this.topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(24)))));
            this.topBar.Controls.Add(this.appverLbl);
            this.topBar.Location = new System.Drawing.Point(1, 1);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(598, 65);
            this.topBar.TabIndex = 25;
            this.topBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            // 
            // lblRecordingTimer
            // 
            this.lblRecordingTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(42)))));
            this.lblRecordingTimer.ForeColor = System.Drawing.Color.DarkGray;
            this.lblRecordingTimer.Location = new System.Drawing.Point(409, 448);
            this.lblRecordingTimer.Name = "lblRecordingTimer";
            this.lblRecordingTimer.Size = new System.Drawing.Size(166, 30);
            this.lblRecordingTimer.TabIndex = 26;
            this.lblRecordingTimer.Text = "00:00:00";
            this.lblRecordingTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScreenRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));
            this.BackgroundImage = global::SimpleScreenRecorder.Properties.Resources.border;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.hideBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.appLogo);
            this.Controls.Add(this.videoOptionsSection);
            this.Controls.Add(this.savePathSection);
            this.Controls.Add(this.countdownChkBox);
            this.Controls.Add(this.hideonrecordChkBox);
            this.Controls.Add(this.lblRecordingTimer);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.audioOptionsSection);
            this.Controls.Add(this.appName);
            this.Controls.Add(this.topBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ScreenRecorder";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Recorder by NaeTech";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenRecorder_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Handle_Window_Drag);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).EndInit();
            this.savePathSection.ResumeLayout(false);
            this.savePathSection.PerformLayout();
            this.videoOptionsSection.ResumeLayout(false);
            this.videoOptionsSection.PerformLayout();
            this.audioOptionsSection.ResumeLayout(false);
            this.audioOptionsSection.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.topBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TrackBar trackBarQuality;
        private System.Windows.Forms.Label lblQualityValue;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button hideBtn;
        private System.Windows.Forms.PictureBox appLogo;
        private System.Windows.Forms.Label appName;
        private System.Windows.Forms.CheckBox cbRecordSystemAudio;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox comboBoxFps;
        private System.Windows.Forms.Label fpsLbl;
        private System.Windows.Forms.Label appverLbl;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox hideonrecordChkBox;
        private System.Windows.Forms.CheckBox countdownChkBox;
        private System.Windows.Forms.Label dspLbl;
        private System.Windows.Forms.GroupBox savePathSection;
        private System.Windows.Forms.GroupBox videoOptionsSection;
        private System.Windows.Forms.ComboBox comboBoxCodec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox audioOptionsSection;
        private System.Windows.Forms.CheckBox vbrCheck;
        private System.Windows.Forms.CheckBox cbrCheck;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showCtxMenu;
        private System.Windows.Forms.ToolStripMenuItem exitCtxMenu;
        private System.Windows.Forms.ToolStripMenuItem startRecordingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRecordingToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxMonitor;
        private System.Windows.Forms.ToolStripMenuItem showRecordingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLastRecordingToolStripMenuItem;
        private System.Windows.Forms.Button showPath;
        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Timer recordingTimer;
        private System.Windows.Forms.Label lblRecordingTimer;
        private System.Windows.Forms.Label areaStatusLabel;
    }
}
