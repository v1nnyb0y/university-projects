using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule = Schedule.Schedule;

namespace SchedulePair
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void btnEnter_Click(object sender, EventArgs e) {
            dgvOutput.Columns.Add("1", "День 1");
            dgvOutput.RowHeadersVisible = false;
            global::Schedule.Schedule schedule = new global::Schedule.Schedule(rtbInput.Lines);
            List<char>[] arr = schedule.CreateSchedule();
            for (int i = 0; i < 2; i++) {
                dgvOutput.Rows.Add();
                for (int j = 0; j < arr[i].Count; j++) {
                    try {
                        if (j != 0) {
                            if (i == 1) {
                                if (!dgvOutput.Columns.Contains((j + 1).ToString())) {
                                    dgvOutput.Columns.Add((j + 1).ToString(), "День " + (j + 1));
                                }
                            }
                            else {
                                dgvOutput.Columns.Add((j + 1).ToString(), "День " + (j + 1));
                            }
                        }
                    }
                    catch { }

                    dgvOutput[j, i].Value = arr[i][j];
                }
            }
            dgvOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
