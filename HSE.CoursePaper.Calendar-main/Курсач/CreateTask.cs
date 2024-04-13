using System;
using System.Windows.Forms;

namespace Курсач
{
    public partial class СозданиеЗадания : Form
    {
        private readonly bool   Is;
        private readonly string taskDescription;
        private readonly string taskName;

        public СозданиеЗадания() {
            InitializeComponent();
        }

        public СозданиеЗадания(string TN, string TD, bool @is) {
            InitializeComponent();
            taskName = TN;
            taskDescription = TD;
            Is = @is;
        }

        //Закрытие формы
        private void СозданиеЗадания_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult == DialogResult.Cancel) {
                e.Cancel = false;
            }
            else {
                if (CheckRightInput()) {
                    WorkWithFiles_Control.AddTask(Название.Text, УсловиеВыполнения.Text);
                    Send.GetTasks = new Tasks();
                    Send.GetTasks.AddTask(Название.Text, УсловиеВыполнения.Text);

                    e.Cancel = false;
                }
                else {
                    e.Cancel = true;
                }
            }
        }

        //Проверка на правильность ввода
        private bool CheckRightInput() {
            if (Название.TextLength == 0 || УсловиеВыполнения.TextLength == 0) {
                MessageBox.Show("Заполните все обязальные поля", "Создание задачи",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void СозданиеЗадания_Load(object sender, EventArgs e) {
            if (Is) {
                Название.Text = taskName;
                УсловиеВыполнения.Text = taskDescription;
                Создать.Text = "Обновить";
                Отмена.Text = "Удалить задание";
                ControlBox = false;
            }
        }
    }
}