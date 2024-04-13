namespace Lab._2.Paint.UI
{
    partial class CanvasForm
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
            this.ShowedPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ShowedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ShowedPictureBox
            // 
            this.ShowedPictureBox.BackColor = System.Drawing.Color.White;
            this.ShowedPictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ShowedPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowedPictureBox.Location = new System.Drawing.Point(0, 0);
            this.ShowedPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.ShowedPictureBox.Name = "ShowedPictureBox";
            this.ShowedPictureBox.Size = new System.Drawing.Size(482, 360);
            this.ShowedPictureBox.TabIndex = 0;
            this.ShowedPictureBox.TabStop = false;
            this.ShowedPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PreShowFigure);
            this.ShowedPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownAction);
            this.ShowedPictureBox.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.ShowedPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            this.ShowedPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseMoveUp);
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 360);
            this.Controls.Add(this.ShowedPictureBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CanvasForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "CanvasForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.savingProject);
            ((System.ComponentModel.ISupportInitialize)(this.ShowedPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ShowedPictureBox;
    }
}