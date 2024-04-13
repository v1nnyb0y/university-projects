namespace Lab._2.Paint.UI
{
    partial class SaveCanvasesDialog
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
            this.lPrintF = new System.Windows.Forms.Label();
            this.cblistProjects = new System.Windows.Forms.CheckedListBox();
            this.bttnAccept = new System.Windows.Forms.Button();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lPrintF
            // 
            this.lPrintF.AutoSize = true;
            this.lPrintF.Location = new System.Drawing.Point(12, 9);
            this.lPrintF.Name = "lPrintF";
            this.lPrintF.Size = new System.Drawing.Size(246, 13);
            this.lPrintF.TabIndex = 0;
            this.lPrintF.Text = "Выберите проекты, которые хотите сохранить:";
            // 
            // cblistProjects
            // 
            this.cblistProjects.CheckOnClick = true;
            this.cblistProjects.FormattingEnabled = true;
            this.cblistProjects.Location = new System.Drawing.Point(15, 26);
            this.cblistProjects.Name = "cblistProjects";
            this.cblistProjects.Size = new System.Drawing.Size(157, 289);
            this.cblistProjects.TabIndex = 1;
            // 
            // bttnAccept
            // 
            this.bttnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bttnAccept.Location = new System.Drawing.Point(15, 321);
            this.bttnAccept.Name = "bttnAccept";
            this.bttnAccept.Size = new System.Drawing.Size(90, 26);
            this.bttnAccept.TabIndex = 2;
            this.bttnAccept.Text = "Сохранить...";
            this.bttnAccept.UseVisualStyleBackColor = true;
            this.bttnAccept.Click += new System.EventHandler(this.AcceptAction);
            // 
            // bttnCancel
            // 
            this.bttnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnCancel.Location = new System.Drawing.Point(168, 321);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(90, 26);
            this.bttnCancel.TabIndex = 3;
            this.bttnCancel.Text = "Отмена";
            this.bttnCancel.UseVisualStyleBackColor = true;
            this.bttnCancel.Click += new System.EventHandler(this.CancelAction);
            // 
            // SaveCanvasesDialog
            // 
            this.AcceptButton = this.bttnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.bttnCancel;
            this.ClientSize = new System.Drawing.Size(260, 359);
            this.ControlBox = false;
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.bttnAccept);
            this.Controls.Add(this.cblistProjects);
            this.Controls.Add(this.lPrintF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SaveCanvasesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сохранить как...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lPrintF;
        private System.Windows.Forms.CheckedListBox cblistProjects;
        private System.Windows.Forms.Button bttnAccept;
        private System.Windows.Forms.Button bttnCancel;
    }
}