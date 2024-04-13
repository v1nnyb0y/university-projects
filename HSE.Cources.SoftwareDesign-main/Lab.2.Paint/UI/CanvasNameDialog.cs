using System;
using System.Windows.Forms;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.UI
{
    public partial class CanvasNameDialog : Form
    {
        private readonly IWorkspace Parent;

        public CanvasNameDialog(IWorkspace _Parent) {
            InitializeComponent();
            Parent = _Parent;
        }

        private void AcceptButtonClickAction(object sender, EventArgs e) {
            //If new name was input
            if (ProjectName.Text.Length != 0) {
                var temp = ProjectName.Text;
                if (Parent.Names.Contains(temp)) {
                    var id = 1;
                    while (true) {
                        temp = ProjectName.Text;
                        temp = string.Concat(temp, " (", id, ")");
                        id++;
                        if (!Parent.Names.Contains(temp)) break;
                    }
                }

                Parent.Names.Add(temp);
                new CanvasForm(temp, Parent as IDescriptionable)
                    {
                        MdiParent = Parent as MainForm
                    }.Show();
                Close();
            }
            else {
                //If was not
                MessageBox.Show("Ошибка ввода!\nПожалуйста введи название нового проекта...",
                    "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void KeyPressAction(object sender, KeyPressEventArgs e) {
            //If press enter
            if (e.KeyChar == 13) AcceptButtonClickAction(sender, e);

            //Do not press except digits and words
            if (!char.IsLetterOrDigit(e.KeyChar)) {
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        private void ClosingFormAction(object sender, FormClosingEventArgs e) {
            //If no project name
            if (ProjectName.Text.Length == 0)
                e.Cancel = true;

            //return false
            e.Cancel = false;
        }
    }
}