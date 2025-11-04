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
        private System.Windows.Forms.Label labelPath;
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
            this.labelPath = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonitor)).BeginInit();
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
            this.btnStart.Location = new System.Drawing.Point(322, 214);
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
            this.btnStop.Location = new System.Drawing.Point(10, 214);
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
            this.comboBoxMic.Location = new System.Drawing.Point(82, 70);
            this.comboBoxMic.Name = "comboBoxMic";
            this.comboBoxMic.Size = new System.Drawing.Size(300, 22);
            this.comboBoxMic.TabIndex = 1;
            // 
            // labelMic
            // 
            this.labelMic.AutoSize = true;
            this.labelMic.ForeColor = System.Drawing.Color.White;
            this.labelMic.Location = new System.Drawing.Point(10, 74);
            this.labelMic.Name = "labelMic";
            this.labelMic.Size = new System.Drawing.Size(66, 14);
            this.labelMic.TabIndex = 0;
            this.labelMic.Text = "Microphone:";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(82, 175);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(300, 20);
            this.txtPath.TabIndex = 3;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.ForeColor = System.Drawing.Color.White;
            this.labelPath.Location = new System.Drawing.Point(10, 178);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(59, 14);
            this.labelPath.TabIndex = 2;
            this.labelPath.Text = "Save Path:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.White;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.btnBrowse.Location = new System.Drawing.Point(388, 175);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 20);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(10, 266);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(480, 24);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status: Idle";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblStatus_MouseDown);
            // 
            // trackBarQuality
            // 
            this.trackBarQuality.LargeChange = 1;
            this.trackBarQuality.Location = new System.Drawing.Point(10, 128);
            this.trackBarQuality.Maximum = 5;
            this.trackBarQuality.Minimum = 1;
            this.trackBarQuality.Name = "trackBarQuality";
            this.trackBarQuality.Size = new System.Drawing.Size(244, 45);
            this.trackBarQuality.TabIndex = 8;
            this.trackBarQuality.Value = 2;
            this.trackBarQuality.Scroll += new System.EventHandler(this.TrackBarQuality_Scroll);
            // 
            // lblQualityValue
            // 
            this.lblQualityValue.ForeColor = System.Drawing.Color.White;
            this.lblQualityValue.Location = new System.Drawing.Point(13, 105);
            this.lblQualityValue.Name = "lblQualityValue";
            this.lblQualityValue.Size = new System.Drawing.Size(241, 20);
            this.lblQualityValue.TabIndex = 9;
            this.lblQualityValue.Text = "Video Quality: Medium (10 Mbps)";
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
            this.exitBtn.Location = new System.Drawing.Point(431, 10);
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
            this.hideBtn.Location = new System.Drawing.Point(366, 10);
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
            this.pictureBox1.Location = new System.Drawing.Point(10, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(170, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Simple Screen Recorder";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label1_MouseDown);
            // 
            // cbRecordSystemAudio
            // 
            this.cbRecordSystemAudio.AutoSize = true;
            this.cbRecordSystemAudio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRecordSystemAudio.Checked = true;
            this.cbRecordSystemAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRecordSystemAudio.ForeColor = System.Drawing.Color.White;
            this.cbRecordSystemAudio.Location = new System.Drawing.Point(388, 72);
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
            this.fpsSelect.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.fpsSelect.Location = new System.Drawing.Point(440, 128);
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
            this.fpsSelect.Size = new System.Drawing.Size(50, 20);
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
            this.fpsLbl.Location = new System.Drawing.Point(378, 131);
            this.fpsLbl.Name = "fpsLbl";
            this.fpsLbl.Size = new System.Drawing.Size(59, 14);
            this.fpsLbl.TabIndex = 18;
            this.fpsLbl.Text = "Framerate:";
            // 
            // appverLbl
            // 
            this.appverLbl.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appverLbl.ForeColor = System.Drawing.Color.Gray;
            this.appverLbl.Location = new System.Drawing.Point(172, 32);
            this.appverLbl.Name = "appverLbl";
            this.appverLbl.Size = new System.Drawing.Size(156, 13);
            this.appverLbl.TabIndex = 19;
            this.appverLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.appverLbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AppverLbl_MouseDown);
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
            this.hideonrecordChkBox.Location = new System.Drawing.Point(269, 223);
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
            this.dspLbl.Location = new System.Drawing.Point(266, 131);
            this.dspLbl.Name = "dspLbl";
            this.dspLbl.Size = new System.Drawing.Size(45, 14);
            this.dspLbl.TabIndex = 22;
            this.dspLbl.Text = "Display:";
            // 
            // numericUpDownMonitor
            // 
            this.numericUpDownMonitor.Location = new System.Drawing.Point(314, 128);
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
            this.numericUpDownMonitor.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownMonitor.TabIndex = 21;
            this.numericUpDownMonitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownMonitor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ScreenRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.dspLbl);
            this.Controls.Add(this.numericUpDownMonitor);
            this.Controls.Add(this.hideonrecordChkBox);
            this.Controls.Add(this.appverLbl);
            this.Controls.Add(this.fpsLbl);
            this.Controls.Add(this.fpsSelect);
            this.Controls.Add(this.cbRecordSystemAudio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.hideBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.lblQualityValue);
            this.Controls.Add(this.trackBarQuality);
            this.Controls.Add(this.labelMic);
            this.Controls.Add(this.comboBoxMic);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScreenRecorder";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Recorder by NaeTech";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenRecorder_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScreenRecorder_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonitor)).EndInit();
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
    }
}
