using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab._7.Threads
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayGameClickEvent(object sender, EventArgs e) {
            Priority priority = Priority.Lowest;
            switch (ChoosePriority.SelectedIndex) {
                case 0: priority = Priority.Lowest;
                    break;
                case 1: priority = Priority.BelowNormal;
                    break;
                case 2: priority = Priority.Normal;
                    break;
                case 3: priority = Priority.AboveNormal;
                    break;
                case 4: priority = Priority.Highest;
                    break;
            }
            var plg = new Playground(priority)
                                 {
                                     Owner = this
                                 };
            plg.Show();
        }

        public void AddInfoStat
        (
            string newGameInfo
        ) {
            StatisticTxtBox.Text += newGameInfo + "\n";
        }

        private void StartForm(object sender, EventArgs e) {
            ChoosePriority.SelectedIndex = 2;
        }
    }
}
