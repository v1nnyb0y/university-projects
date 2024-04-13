namespace Lab._2.Paint.UI
{
    partial class CanvasNameDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.ProjectName = new System.Windows.Forms.TextBox();
            this.Accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пожалуйста, введите название нового проекта:";
            // 
            // ProjectName
            // 
            this.ProjectName.Location = new System.Drawing.Point(16, 29);
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Size = new System.Drawing.Size(199, 20);
            this.ProjectName.TabIndex = 1;
            this.ProjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressAction);
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(16, 55);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(87, 23);
            this.Accept.TabIndex = 2;
            this.Accept.Text = "Принять";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.AcceptButtonClickAction);
            // 
            // CanvasNameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 82);
            this.ControlBox = false;
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.ProjectName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CanvasNameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Имя нового проекта";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingFormAction);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProjectName;
        private System.Windows.Forms.Button Accept;
    }
}