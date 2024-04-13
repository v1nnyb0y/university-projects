namespace Курсач
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.NumberWeeks = new System.Windows.Forms.CheckBox();
            this.NoAlarm = new System.Windows.Forms.CheckBox();
            this.ВыборДняНедели = new System.Windows.Forms.ComboBox();
            this.LLВыборДняНедели = new System.Windows.Forms.Label();
            this.AcceptOptions = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NumberWeeks
            // 
            this.NumberWeeks.AutoSize = true;
            this.NumberWeeks.Location = new System.Drawing.Point(9, 58);
            this.NumberWeeks.Margin = new System.Windows.Forms.Padding(2);
            this.NumberWeeks.Name = "NumberWeeks";
            this.NumberWeeks.Size = new System.Drawing.Size(169, 17);
            this.NumberWeeks.TabIndex = 8;
            this.NumberWeeks.Text = "Показывать номера недель";
            this.NumberWeeks.UseVisualStyleBackColor = true;
            // 
            // NoAlarm
            // 
            this.NoAlarm.AutoSize = true;
            this.NoAlarm.Location = new System.Drawing.Point(9, 37);
            this.NoAlarm.Margin = new System.Windows.Forms.Padding(2);
            this.NoAlarm.Name = "NoAlarm";
            this.NoAlarm.Size = new System.Drawing.Size(102, 17);
            this.NoAlarm.TabIndex = 7;
            this.NoAlarm.Text = "Не беспокоить";
            this.NoAlarm.UseVisualStyleBackColor = true;
            // 
            // ВыборДняНедели
            // 
            this.ВыборДняНедели.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ВыборДняНедели.FormattingEnabled = true;
            this.ВыборДняНедели.Items.AddRange(new object[] {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота",
            "Воскресенье"});
            this.ВыборДняНедели.Location = new System.Drawing.Point(130, 5);
            this.ВыборДняНедели.Margin = new System.Windows.Forms.Padding(2);
            this.ВыборДняНедели.Name = "ВыборДняНедели";
            this.ВыборДняНедели.Size = new System.Drawing.Size(117, 21);
            this.ВыборДняНедели.TabIndex = 6;
            // 
            // LLВыборДняНедели
            // 
            this.LLВыборДняНедели.AutoSize = true;
            this.LLВыборДняНедели.Location = new System.Drawing.Point(6, 8);
            this.LLВыборДняНедели.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LLВыборДняНедели.Name = "LLВыборДняНедели";
            this.LLВыборДняНедели.Size = new System.Drawing.Size(113, 13);
            this.LLВыборДняНедели.TabIndex = 5;
            this.LLВыборДняНедели.Text = "Первый день недели";
            // 
            // AcceptOptions
            // 
            this.AcceptOptions.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptOptions.Location = new System.Drawing.Point(9, 86);
            this.AcceptOptions.Name = "AcceptOptions";
            this.AcceptOptions.Size = new System.Drawing.Size(87, 23);
            this.AcceptOptions.TabIndex = 9;
            this.AcceptOptions.Text = "Обновить";
            this.AcceptOptions.UseVisualStyleBackColor = true;
            this.AcceptOptions.Click += new System.EventHandler(this.AcceptOptions_Click);
            // 
            // Back
            // 
            this.Back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Back.Location = new System.Drawing.Point(160, 86);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(87, 23);
            this.Back.TabIndex = 10;
            this.Back.Text = "Назад";
            this.Back.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AcceptButton = this.AcceptOptions;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Back;
            this.ClientSize = new System.Drawing.Size(251, 113);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.AcceptOptions);
            this.Controls.Add(this.NumberWeeks);
            this.Controls.Add(this.NoAlarm);
            this.Controls.Add(this.ВыборДняНедели);
            this.Controls.Add(this.LLВыборДняНедели);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "Настройка";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox NumberWeeks;
        private System.Windows.Forms.CheckBox NoAlarm;
        private System.Windows.Forms.ComboBox ВыборДняНедели;
        private System.Windows.Forms.Label LLВыборДняНедели;
        private System.Windows.Forms.Button AcceptOptions;
        private System.Windows.Forms.Button Back;
    }
}