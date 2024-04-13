namespace Курсач
{
    partial class СозданиеСобытия
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(СозданиеСобытия));
            this.LLНазвание = new System.Windows.Forms.Label();
            this.Название = new System.Windows.Forms.TextBox();
            this.ДатаОт = new System.Windows.Forms.DateTimePicker();
            this.LLОт = new System.Windows.Forms.Label();
            this.LLДо = new System.Windows.Forms.Label();
            this.ВесьДень = new System.Windows.Forms.CheckBox();
            this.Добавить = new System.Windows.Forms.Button();
            this.Отменить = new System.Windows.Forms.Button();
            this.Заметки = new System.Windows.Forms.RichTextBox();
            this.Напоминание = new System.Windows.Forms.ComboBox();
            this.LLНапоминания = new System.Windows.Forms.Label();
            this.ДатаДо = new System.Windows.Forms.DateTimePicker();
            this.LLВажность = new System.Windows.Forms.Label();
            this.Важность = new System.Windows.Forms.ComboBox();
            this.LLПовторы = new System.Windows.Forms.Label();
            this.Повторы = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // LLНазвание
            // 
            this.LLНазвание.AutoSize = true;
            this.LLНазвание.ForeColor = System.Drawing.Color.DarkRed;
            this.LLНазвание.Location = new System.Drawing.Point(16, 30);
            this.LLНазвание.Name = "LLНазвание";
            this.LLНазвание.Size = new System.Drawing.Size(72, 17);
            this.LLНазвание.TabIndex = 0;
            this.LLНазвание.Text = "Название";
            // 
            // Название
            // 
            this.Название.Location = new System.Drawing.Point(92, 26);
            this.Название.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Название.Name = "Название";
            this.Название.Size = new System.Drawing.Size(124, 22);
            this.Название.TabIndex = 1;
            // 
            // ДатаОт
            // 
            this.ДатаОт.CustomFormat = "";
            this.ДатаОт.Location = new System.Drawing.Point(92, 89);
            this.ДатаОт.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ДатаОт.Name = "ДатаОт";
            this.ДатаОт.Size = new System.Drawing.Size(200, 22);
            this.ДатаОт.TabIndex = 2;
            this.ДатаОт.ValueChanged += new System.EventHandler(this.ДатаОт_ValueChanged);
            // 
            // LLОт
            // 
            this.LLОт.AutoSize = true;
            this.LLОт.ForeColor = System.Drawing.Color.DarkRed;
            this.LLОт.Location = new System.Drawing.Point(16, 94);
            this.LLОт.Name = "LLОт";
            this.LLОт.Size = new System.Drawing.Size(58, 17);
            this.LLОт.TabIndex = 3;
            this.LLОт.Text = "Начало";
            // 
            // LLДо
            // 
            this.LLДо.AutoSize = true;
            this.LLДо.ForeColor = System.Drawing.Color.DarkRed;
            this.LLДо.Location = new System.Drawing.Point(16, 121);
            this.LLДо.Name = "LLДо";
            this.LLДо.Size = new System.Drawing.Size(49, 17);
            this.LLДо.TabIndex = 4;
            this.LLДо.Text = "Конец";
            // 
            // ВесьДень
            // 
            this.ВесьДень.AutoSize = true;
            this.ВесьДень.Location = new System.Drawing.Point(19, 62);
            this.ВесьДень.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ВесьДень.Name = "ВесьДень";
            this.ВесьДень.Size = new System.Drawing.Size(96, 21);
            this.ВесьДень.TabIndex = 6;
            this.ВесьДень.Text = "Весь день";
            this.ВесьДень.UseVisualStyleBackColor = true;
            this.ВесьДень.CheckedChanged += new System.EventHandler(this.ВесьДень_CheckedChanged);
            // 
            // Добавить
            // 
            this.Добавить.AccessibleName = "";
            this.Добавить.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Добавить.Location = new System.Drawing.Point(103, 250);
            this.Добавить.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Добавить.Name = "Добавить";
            this.Добавить.Size = new System.Drawing.Size(93, 53);
            this.Добавить.TabIndex = 7;
            this.Добавить.Text = "Добавить";
            this.Добавить.UseVisualStyleBackColor = true;
            this.Добавить.Click += new System.EventHandler(this.Добавить_Click);
            // 
            // Отменить
            // 
            this.Отменить.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Отменить.Location = new System.Drawing.Point(409, 250);
            this.Отменить.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Отменить.Name = "Отменить";
            this.Отменить.Size = new System.Drawing.Size(93, 53);
            this.Отменить.TabIndex = 8;
            this.Отменить.Text = "Отмена";
            this.Отменить.UseVisualStyleBackColor = true;
            // 
            // Заметки
            // 
            this.Заметки.Location = new System.Drawing.Point(312, 12);
            this.Заметки.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Заметки.Name = "Заметки";
            this.Заметки.Size = new System.Drawing.Size(265, 232);
            this.Заметки.TabIndex = 9;
            this.Заметки.Text = "Заметки";
            this.Заметки.Enter += new System.EventHandler(this.Заметки_Enter);
            this.Заметки.Leave += new System.EventHandler(this.Заметки_Leave);
            // 
            // Напоминание
            // 
            this.Напоминание.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Напоминание.FormattingEnabled = true;
            this.Напоминание.Items.AddRange(new object[] {
            "(нет)",
            "За 10 минут",
            "За 30 минут",
            "За 1 час",
            "За 1 день"});
            this.Напоминание.Location = new System.Drawing.Point(121, 149);
            this.Напоминание.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Напоминание.Name = "Напоминание";
            this.Напоминание.Size = new System.Drawing.Size(139, 24);
            this.Напоминание.TabIndex = 10;
            // 
            // LLНапоминания
            // 
            this.LLНапоминания.AutoSize = true;
            this.LLНапоминания.Location = new System.Drawing.Point(16, 153);
            this.LLНапоминания.Name = "LLНапоминания";
            this.LLНапоминания.Size = new System.Drawing.Size(99, 17);
            this.LLНапоминания.TabIndex = 11;
            this.LLНапоминания.Text = "Напоминания";
            // 
            // ДатаДо
            // 
            this.ДатаДо.CustomFormat = "";
            this.ДатаДо.Location = new System.Drawing.Point(92, 117);
            this.ДатаДо.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ДатаДо.Name = "ДатаДо";
            this.ДатаДо.Size = new System.Drawing.Size(200, 22);
            this.ДатаДо.TabIndex = 12;
            this.ДатаДо.ValueChanged += new System.EventHandler(this.ДатаДо_ValueChanged);
            // 
            // LLВажность
            // 
            this.LLВажность.AutoSize = true;
            this.LLВажность.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LLВажность.Location = new System.Drawing.Point(16, 190);
            this.LLВажность.Name = "LLВажность";
            this.LLВажность.Size = new System.Drawing.Size(71, 17);
            this.LLВажность.TabIndex = 13;
            this.LLВажность.Text = "Важность";
            // 
            // Важность
            // 
            this.Важность.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Важность.FormattingEnabled = true;
            this.Важность.Items.AddRange(new object[] {
            "Важное",
            "Обычное",
            "Необязательное"});
            this.Важность.Location = new System.Drawing.Point(121, 186);
            this.Важность.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Важность.Name = "Важность";
            this.Важность.Size = new System.Drawing.Size(139, 24);
            this.Важность.TabIndex = 14;
            // 
            // LLПовторы
            // 
            this.LLПовторы.AutoSize = true;
            this.LLПовторы.Location = new System.Drawing.Point(16, 223);
            this.LLПовторы.Name = "LLПовторы";
            this.LLПовторы.Size = new System.Drawing.Size(66, 17);
            this.LLПовторы.TabIndex = 15;
            this.LLПовторы.Text = "Повторы";
            // 
            // Повторы
            // 
            this.Повторы.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Повторы.FormattingEnabled = true;
            this.Повторы.Items.AddRange(new object[] {
            "Никогда",
            "Каждый день",
            "Каждую неделю",
            "Каждый год"});
            this.Повторы.Location = new System.Drawing.Point(121, 220);
            this.Повторы.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Повторы.Name = "Повторы";
            this.Повторы.Size = new System.Drawing.Size(139, 24);
            this.Повторы.TabIndex = 16;
            // 
            // СозданиеСобытия
            // 
            this.AcceptButton = this.Добавить;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Отменить;
            this.ClientSize = new System.Drawing.Size(589, 309);
            this.Controls.Add(this.Повторы);
            this.Controls.Add(this.LLПовторы);
            this.Controls.Add(this.Важность);
            this.Controls.Add(this.LLВажность);
            this.Controls.Add(this.ДатаДо);
            this.Controls.Add(this.LLНапоминания);
            this.Controls.Add(this.Напоминание);
            this.Controls.Add(this.Заметки);
            this.Controls.Add(this.Отменить);
            this.Controls.Add(this.Добавить);
            this.Controls.Add(this.ВесьДень);
            this.Controls.Add(this.LLДо);
            this.Controls.Add(this.LLОт);
            this.Controls.Add(this.ДатаОт);
            this.Controls.Add(this.Название);
            this.Controls.Add(this.LLНазвание);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "СозданиеСобытия";
            this.Text = "Создать событие";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.СозданиеСобытия_FormClosing);
            this.Load += new System.EventHandler(this.СозданиеСобытия_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LLНазвание;
        private System.Windows.Forms.TextBox Название;
        private System.Windows.Forms.DateTimePicker ДатаОт;
        private System.Windows.Forms.Label LLОт;
        private System.Windows.Forms.Label LLДо;
        private System.Windows.Forms.CheckBox ВесьДень;
        private System.Windows.Forms.Button Добавить;
        private System.Windows.Forms.Button Отменить;
        private System.Windows.Forms.RichTextBox Заметки;
        private System.Windows.Forms.ComboBox Напоминание;
        private System.Windows.Forms.Label LLНапоминания;
        private System.Windows.Forms.DateTimePicker ДатаДо;
        private System.Windows.Forms.Label LLВажность;
        private System.Windows.Forms.ComboBox Важность;
        private System.Windows.Forms.Label LLПовторы;
        private System.Windows.Forms.ComboBox Повторы;
    }
}