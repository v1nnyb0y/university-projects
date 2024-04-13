namespace Lab._7.Threads
{
    partial class StartWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartWindow));
            this.playGameBttn = new System.Windows.Forms.Button();
            this.ChoosePriority = new System.Windows.Forms.ComboBox();
            this.infoBoxForPriority = new System.Windows.Forms.TextBox();
            this.infoBoxStatistic = new System.Windows.Forms.TextBox();
            this.StatisticTxtBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // playGameBttn
            // 
            this.playGameBttn.BackColor = System.Drawing.Color.Red;
            this.playGameBttn.Font = new System.Drawing.Font("Wide Latin", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playGameBttn.ForeColor = System.Drawing.Color.White;
            this.playGameBttn.Location = new System.Drawing.Point(84, 227);
            this.playGameBttn.Name = "playGameBttn";
            this.playGameBttn.Size = new System.Drawing.Size(139, 49);
            this.playGameBttn.TabIndex = 0;
            this.playGameBttn.Text = "В БОЙ!";
            this.playGameBttn.UseVisualStyleBackColor = false;
            this.playGameBttn.Click += new System.EventHandler(this.PlayGameClickEvent);
            // 
            // ChoosePriority
            // 
            this.ChoosePriority.FormattingEnabled = true;
            this.ChoosePriority.Items.AddRange(new object[] {
            "Lowest",
            "BelowNormal",
            "Normal",
            "AboveNormal",
            "Highest"});
            this.ChoosePriority.Location = new System.Drawing.Point(12, 31);
            this.ChoosePriority.Name = "ChoosePriority";
            this.ChoosePriority.Size = new System.Drawing.Size(191, 21);
            this.ChoosePriority.TabIndex = 1;
            // 
            // infoBoxForPriority
            // 
            this.infoBoxForPriority.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoBoxForPriority.Location = new System.Drawing.Point(12, 12);
            this.infoBoxForPriority.Name = "infoBoxForPriority";
            this.infoBoxForPriority.Size = new System.Drawing.Size(191, 13);
            this.infoBoxForPriority.TabIndex = 2;
            this.infoBoxForPriority.Text = "Выбрать приоритет \r\nпотоков врага:";
            // 
            // infoBoxStatistic
            // 
            this.infoBoxStatistic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoBoxStatistic.Location = new System.Drawing.Point(116, 58);
            this.infoBoxStatistic.Name = "infoBoxStatistic";
            this.infoBoxStatistic.Size = new System.Drawing.Size(191, 13);
            this.infoBoxStatistic.TabIndex = 3;
            this.infoBoxStatistic.Text = "Статистика игр";
            // 
            // StatisticTxtBox
            // 
            this.StatisticTxtBox.Location = new System.Drawing.Point(12, 77);
            this.StatisticTxtBox.Name = "StatisticTxtBox";
            this.StatisticTxtBox.ReadOnly = true;
            this.StatisticTxtBox.Size = new System.Drawing.Size(283, 144);
            this.StatisticTxtBox.TabIndex = 4;
            this.StatisticTxtBox.Text = "";
            // 
            // StartWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 288);
            this.Controls.Add(this.StatisticTxtBox);
            this.Controls.Add(this.infoBoxStatistic);
            this.Controls.Add(this.infoBoxForPriority);
            this.Controls.Add(this.ChoosePriority);
            this.Controls.Add(this.playGameBttn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartWindow";
            this.Text = "Главное меню";
            this.Load += new System.EventHandler(this.StartForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playGameBttn;
        private System.Windows.Forms.ComboBox ChoosePriority;
        private System.Windows.Forms.TextBox infoBoxForPriority;
        private System.Windows.Forms.TextBox infoBoxStatistic;
        private System.Windows.Forms.RichTextBox StatisticTxtBox;
    }
}