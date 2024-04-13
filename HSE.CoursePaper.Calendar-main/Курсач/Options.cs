using System;
using System.Windows.Forms;

namespace Курсач
{
    public partial class Options : Form
    {
        public Options() {
            InitializeComponent();
        }

        private void AcceptOptions_Click(object sender, EventArgs e) {
            Options_Control.NewOption(ВыборДняНедели.SelectedIndex, NoAlarm.Checked, NumberWeeks.Checked);
        }

        private void Options_Load(object sender, EventArgs e) {
            ClassOptions classOptions = Options_Control.GetClassOptions;
            ВыборДняНедели.SelectedIndex = classOptions.FirstDay;
            NoAlarm.Checked = classOptions.NotAlarm;
            NumberWeeks.Checked = classOptions.NumberWeeks;
        }
    }
}