namespace Курсач
{
    partial class СозданиеЗадания
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(СозданиеЗадания));
            this.LLНазвание = new System.Windows.Forms.Label();
            this.LLУсловиеВыполнения = new System.Windows.Forms.Label();
            this.Название = new System.Windows.Forms.TextBox();
            this.УсловиеВыполнения = new System.Windows.Forms.RichTextBox();
            this.Создать = new System.Windows.Forms.Button();
            this.Отмена = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LLНазвание
            // 
            this.LLНазвание.AutoSize = true;
            this.LLНазвание.ForeColor = System.Drawing.Color.DarkRed;
            this.LLНазвание.Location = new System.Drawing.Point(7, 19);
            this.LLНазвание.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LLНазвание.Name = "LLНазвание";
            this.LLНазвание.Size = new System.Drawing.Size(57, 13);
            this.LLНазвание.TabIndex = 0;
            this.LLНазвание.Text = "Название";
            // 
            // LLУсловиеВыполнения
            // 
            this.LLУсловиеВыполнения.AutoSize = true;
            this.LLУсловиеВыполнения.ForeColor = System.Drawing.Color.DarkRed;
            this.LLУсловиеВыполнения.Location = new System.Drawing.Point(7, 50);
            this.LLУсловиеВыполнения.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LLУсловиеВыполнения.Name = "LLУсловиеВыполнения";
            this.LLУсловиеВыполнения.Size = new System.Drawing.Size(116, 13);
            this.LLУсловиеВыполнения.TabIndex = 1;
            this.LLУсловиеВыполнения.Text = "Условие выполнения";
            // 
            // Название
            // 
            this.Название.Location = new System.Drawing.Point(68, 16);
            this.Название.Margin = new System.Windows.Forms.Padding(2);
            this.Название.Name = "Название";
            this.Название.Size = new System.Drawing.Size(84, 20);
            this.Название.TabIndex = 2;
            // 
            // УсловиеВыполнения
            // 
            this.УсловиеВыполнения.Location = new System.Drawing.Point(9, 66);
            this.УсловиеВыполнения.Margin = new System.Windows.Forms.Padding(2);
            this.УсловиеВыполнения.Name = "УсловиеВыполнения";
            this.УсловиеВыполнения.Size = new System.Drawing.Size(242, 118);
            this.УсловиеВыполнения.TabIndex = 3;
            this.УсловиеВыполнения.Text = "";
            // 
            // Создать
            // 
            this.Создать.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Создать.Location = new System.Drawing.Point(9, 199);
            this.Создать.Margin = new System.Windows.Forms.Padding(2);
            this.Создать.Name = "Создать";
            this.Создать.Size = new System.Drawing.Size(82, 28);
            this.Создать.TabIndex = 4;
            this.Создать.Text = "Создать";
            this.Создать.UseVisualStyleBackColor = true;
            // 
            // Отмена
            // 
            this.Отмена.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Отмена.Location = new System.Drawing.Point(169, 199);
            this.Отмена.Margin = new System.Windows.Forms.Padding(2);
            this.Отмена.Name = "Отмена";
            this.Отмена.Size = new System.Drawing.Size(82, 28);
            this.Отмена.TabIndex = 5;
            this.Отмена.Text = "Отмена";
            this.Отмена.UseVisualStyleBackColor = true;
            // 
            // СозданиеЗадания
            // 
            this.AcceptButton = this.Создать;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Отмена;
            this.ClientSize = new System.Drawing.Size(260, 237);
            this.Controls.Add(this.Отмена);
            this.Controls.Add(this.Создать);
            this.Controls.Add(this.УсловиеВыполнения);
            this.Controls.Add(this.Название);
            this.Controls.Add(this.LLУсловиеВыполнения);
            this.Controls.Add(this.LLНазвание);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "СозданиеЗадания";
            this.Text = "Создание задания";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.СозданиеЗадания_FormClosing);
            this.Load += new System.EventHandler(this.СозданиеЗадания_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LLНазвание;
        private System.Windows.Forms.Label LLУсловиеВыполнения;
        private System.Windows.Forms.TextBox Название;
        private System.Windows.Forms.RichTextBox УсловиеВыполнения;
        private System.Windows.Forms.Button Создать;
        private System.Windows.Forms.Button Отмена;
    }
}