namespace SimpleScreenRecorder
{
    partial class RegionSelectorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCurrentSize;
        private System.Windows.Forms.Label lblInstructions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCurrentSize = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCurrentSize
            // 
            this.lblCurrentSize.AutoSize = true;
            this.lblCurrentSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.lblCurrentSize.ForeColor = System.Drawing.Color.White;
            this.lblCurrentSize.Location = new System.Drawing.Point(8, 8);
            this.lblCurrentSize.Name = "lblCurrentSize";
            this.lblCurrentSize.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.lblCurrentSize.Size = new System.Drawing.Size(68, 21);
            this.lblCurrentSize.TabIndex = 0;
            this.lblCurrentSize.Text = "1280 x 720";
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.lblInstructions.ForeColor = System.Drawing.Color.White;
            this.lblInstructions.Location = new System.Drawing.Point(8, 35);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.lblInstructions.Size = new System.Drawing.Size(240, 21);
            this.lblInstructions.TabIndex = 1;
            this.lblInstructions.Text = "Enter: Confirm  |  Esc: Cancel  |  Double-click: Confirm";
            // 
            // RegionSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblCurrentSize);
            this.Name = "RegionSelectorForm";
            this.Text = "Select Recording Area";
            this.DoubleClick += new System.EventHandler(this.RegionSelectorForm_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegionSelectorForm_KeyDown);
            this.Move += new System.EventHandler(this.RegionSelectorForm_Move);
            this.Resize += new System.EventHandler(this.RegionSelectorForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
