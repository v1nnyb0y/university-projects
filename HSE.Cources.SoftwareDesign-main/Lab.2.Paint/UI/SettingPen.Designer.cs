namespace Lab._2.Paint.UI
{
    partial class SettingPen
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
            this.bttnAccept = new System.Windows.Forms.Button();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.lWidth = new System.Windows.Forms.Label();
            this.tsWidth = new System.Windows.Forms.DomainUpDown();
            this.cbSmoothing = new System.Windows.Forms.CheckBox();
            this.cbDotted = new System.Windows.Forms.CheckBox();
            this.preShowPen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.preShowPen)).BeginInit();
            this.SuspendLayout();
            // 
            // bttnAccept
            // 
            this.bttnAccept.Location = new System.Drawing.Point(13, 197);
            this.bttnAccept.Name = "bttnAccept";
            this.bttnAccept.Size = new System.Drawing.Size(92, 31);
            this.bttnAccept.TabIndex = 1;
            this.bttnAccept.Text = "Принять";
            this.bttnAccept.UseVisualStyleBackColor = true;
            this.bttnAccept.Click += new System.EventHandler(this.AcceptChanges);
            // 
            // bttnCancel
            // 
            this.bttnCancel.Location = new System.Drawing.Point(257, 197);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(92, 31);
            this.bttnCancel.TabIndex = 2;
            this.bttnCancel.Text = "Отмена";
            this.bttnCancel.UseVisualStyleBackColor = true;
            this.bttnCancel.Click += new System.EventHandler(this.Cancel);
            // 
            // lWidth
            // 
            this.lWidth.AutoSize = true;
            this.lWidth.Location = new System.Drawing.Point(247, 13);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(115, 13);
            this.lWidth.TabIndex = 3;
            this.lWidth.Text = "Толщина карандаша:";
            // 
            // tsWidth
            // 
            this.tsWidth.Location = new System.Drawing.Point(308, 30);
            this.tsWidth.Name = "tsWidth";
            this.tsWidth.ReadOnly = true;
            this.tsWidth.Size = new System.Drawing.Size(54, 20);
            this.tsWidth.TabIndex = 4;
            this.tsWidth.Text = "Толщина";
            this.tsWidth.SelectedItemChanged += new System.EventHandler(this.ChangeWidth);
            // 
            // cbSmoothing
            // 
            this.cbSmoothing.AutoSize = true;
            this.cbSmoothing.Location = new System.Drawing.Point(268, 56);
            this.cbSmoothing.Name = "cbSmoothing";
            this.cbSmoothing.Size = new System.Drawing.Size(94, 17);
            this.cbSmoothing.TabIndex = 5;
            this.cbSmoothing.Text = "Сглаживание";
            this.cbSmoothing.UseVisualStyleBackColor = true;
            this.cbSmoothing.CheckedChanged += new System.EventHandler(this.ChangeSmoothing);
            // 
            // cbDotted
            // 
            this.cbDotted.AutoSize = true;
            this.cbDotted.Location = new System.Drawing.Point(268, 79);
            this.cbDotted.Name = "cbDotted";
            this.cbDotted.Size = new System.Drawing.Size(83, 30);
            this.cbDotted.TabIndex = 6;
            this.cbDotted.Text = "Пунктир\r\n(для фигур)\r\n";
            this.cbDotted.UseVisualStyleBackColor = true;
            this.cbDotted.CheckedChanged += new System.EventHandler(this.ChangeDock);
            // 
            // preShowPen
            // 
            this.preShowPen.BackColor = System.Drawing.Color.White;
            this.preShowPen.Location = new System.Drawing.Point(13, 13);
            this.preShowPen.Name = "preShowPen";
            this.preShowPen.Size = new System.Drawing.Size(245, 178);
            this.preShowPen.TabIndex = 0;
            this.preShowPen.TabStop = false;
            // 
            // SettingPen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(367, 240);
            this.Controls.Add(this.cbDotted);
            this.Controls.Add(this.cbSmoothing);
            this.Controls.Add(this.tsWidth);
            this.Controls.Add(this.lWidth);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.bttnAccept);
            this.Controls.Add(this.preShowPen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingPen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка карандаша";
            this.Load += new System.EventHandler(this.LoadForm);
            ((System.ComponentModel.ISupportInitialize)(this.preShowPen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox preShowPen;
        private System.Windows.Forms.Button bttnAccept;
        private System.Windows.Forms.Button bttnCancel;
        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.DomainUpDown tsWidth;
        private System.Windows.Forms.CheckBox cbSmoothing;
        private System.Windows.Forms.CheckBox cbDotted;
    }
}