namespace Lab._7.Threads
{
    partial class Playground
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Playground));
            this.TimerForRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TimerForRefresh
            // 
            this.TimerForRefresh.Enabled = true;
            this.TimerForRefresh.Interval = 250;
            this.TimerForRefresh.Tick += new System.EventHandler(this.TickMovement);
            // 
            // Playground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab._7.Threads.Properties.Resources.BG;
            this.ClientSize = new System.Drawing.Size(979, 561);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Playground";
            this.ShowInTaskbar = false;
            this.Text = "Игровое поле";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndGame);
            this.Load += new System.EventHandler(this.StartGame);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RefreshWindow);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangeWay);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TimerForRefresh;
    }
}

