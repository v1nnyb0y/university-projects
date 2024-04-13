using System;
using System.Windows.Forms;

namespace Курсач
{
    public partial class СозданиеСобытия : Form
    {
        private readonly int      Alarm;
        private readonly bool     AllDay;
        private readonly string   Description;
        private readonly string   eventName;
        private readonly DateTime From;
        private readonly int      Importance;
        private readonly bool     Is;
        private readonly int      Repeat;
        private readonly DateTime To;

        public СозданиеСобытия() {
            InitializeComponent();
        }

        public СозданиеСобытия(
            bool   @is,         int  importance, int alarm, int repeat, DateTime from, DateTime to, string name,
            string description, bool allDay) {
            InitializeComponent();
            Is = @is;
            Alarm = alarm;
            From = from;
            To = to;
            Repeat = repeat;
            Importance = importance;
            eventName = name;
            Description = description;
            AllDay = allDay;
        }

        //Загрузка формы
        private void СозданиеСобытия_Load(object sender, EventArgs e) {
            ДатаОт.Format = DateTimePickerFormat.Custom;
            ДатаДо.Format = DateTimePickerFormat.Custom;
            ДатаОт.CustomFormat = "HH:mm dd.MM.yyyy";
            ДатаДо.CustomFormat = "HH:mm dd.MM.yyyy";

            if (!Is) {
                Важность.SelectedIndex = 0;
                Напоминание.SelectedIndex = 0;
                Повторы.SelectedIndex = 0;


                ДатаДо.Value = ДатаДо.Value.AddHours(1);
            }
            else {
                Название.Text = eventName;
                Заметки.Text = Description;
                Важность.SelectedIndex = Importance;
                Напоминание.SelectedIndex = Alarm;
                Повторы.SelectedIndex = Repeat;
                ДатаОт.Value = From;
                ДатаДо.Value = To;
                ВесьДень.Checked = AllDay;
                ControlBox = false;

                Отменить.Text = "Удалить";
                Добавить.Text = "Обновить";
            }

            ДатаОт.MinDate = ДатаОт.Value.AddHours(-ДатаОт.Value.Hour);
            ДатаОт.MinDate = ДатаОт.MinDate.AddMinutes(-ДатаОт.Value.Minute);
            ДатаОт.MinDate = ДатаОт.MinDate.AddSeconds(-ДатаОт.Value.Second);

            ДатаДо.MinDate = ДатаОт.MinDate.AddHours(-ДатаОт.Value.Hour);
            ДатаДо.MinDate = ДатаОт.MinDate.AddMinutes(-ДатаОт.Value.Minute);
            ДатаДо.MinDate = ДатаОт.MinDate.AddSeconds(-ДатаОт.Value.Second);
        }

        //Создание отметки "Событие на весь день"
        private void ВесьДень_CheckedChanged(object sender, EventArgs e) {
            if (ВесьДень.Checked) {
                var add_hour = ДатаОт.Value.Hour;
                ДатаОт.Value = ДатаОт.Value.AddHours(-add_hour);

                var add_minute = ДатаОт.Value.Minute;
                ДатаОт.Value = ДатаОт.Value.AddMinutes(-add_minute);

                add_hour = 23 - ДатаДо.Value.Hour;
                ДатаДо.Value = ДатаДо.Value.AddHours(add_hour);

                add_minute = 59 - ДатаДо.Value.Minute;
                ДатаДо.Value = ДатаДо.Value.AddMinutes(add_minute);
            }
            else {
                var add_hour = DateTime.Now.Hour;
                ДатаОт.Value = ДатаОт.Value.AddHours(add_hour);

                var add_minute = DateTime.Now.Minute;
                ДатаОт.Value = ДатаОт.Value.AddMinutes(add_minute);

                add_hour = 22 - DateTime.Now.Hour;
                ДатаДо.Value = ДатаДо.Value.AddHours(-add_hour);

                add_minute = 59 - DateTime.Now.Minute;
                ДатаДо.Value = ДатаДо.Value.AddMinutes(-add_minute);
            }
        }

        //Если выбрали день конца раньше, чем день начала
        private void ДатаДо_ValueChanged(object sender, EventArgs e) {
            if (ДатаДо.Value.CompareTo(ДатаОт.Value) == -1 && !ВесьДень.Checked) {
                ДатаОт.Value = ДатаДо.Value;
                ДатаДо.Value = ДатаДо.Value.AddHours(1);
            }
            else {
                if (ДатаДо.Value.CompareTo(ДатаОт.Value) == -1) ДатаОт.Value = ДатаДо.Value;
            }

            if ((ДатаДо.Value - ДатаОт.Value).Days != 0) {
                ВесьДень.Enabled = false;
                ВесьДень.Checked = false;
            }
            else {
                ВесьДень.Enabled = true;
            }

            if (ВесьДень.Checked) {
                var add_hour = ДатаОт.Value.Hour;
                ДатаОт.Value = ДатаОт.Value.AddHours(-add_hour);

                var add_minute = ДатаОт.Value.Minute;
                ДатаОт.Value = ДатаОт.Value.AddMinutes(-add_minute);

                add_hour = 23 - ДатаДо.Value.Hour;
                ДатаДо.Value = ДатаДо.Value.AddHours(add_hour);

                add_minute = 59 - ДатаДо.Value.Minute;
                ДатаДо.Value = ДатаДо.Value.AddMinutes(add_minute);
            }
        }

        //Выделяем весь текст в текст боксе, если его не редили
        private void Заметки_Enter(object sender, EventArgs e) {
            if (string.Compare("Заметки", Заметки.Text) == 0) Заметки.SelectAll();
        }

        //Пишем "Заметки", если текст пустой или он остался без изменений
        private void Заметки_Leave(object sender, EventArgs e) {
            if (Заметки.Text == "" && string.Compare(Заметки.Text, "Заметки") != 0) Заметки.Text = "Заметки";
        }

        //Добавить событие
        private void Добавить_Click(object sender, EventArgs e) {
            Send.Date = new DateTime[2];
            Send.Date[0] = ДатаОт.Value.Date;
            Send.Date[1] = ДатаДо.Value.Date;
            Send.repeat = Повторы.SelectedIndex;
        }

        //Проверка на ввод всех обязательных окон
        private bool CheckRigthInput() {
            var ok = true;
            if (Название.TextLength == 0) {
                ok = false;
                MessageBox.Show("Заполните все обязальные поля", "Создание события",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ok;
        }

        //При закрытие формы
        private void СозданиеСобытия_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult == DialogResult.Cancel) {
                e.Cancel = false;
            }
            else {
                if (CheckRigthInput()) {
                    WorkWithFiles_Control.AddToClass(ДатаОт.Value, ДатаДо.Value, Напоминание.SelectedIndex,
                                                     Важность.SelectedIndex,
                                                     Название.Text, Заметки.Text, Повторы.SelectedIndex);
                    e.Cancel = false;
                }
                else {
                    e.Cancel = true;
                }
            }
        }

        //Если выбрали день конца раньше, чем день начала
        private void ДатаОт_ValueChanged(object sender, EventArgs e) {
            if (ДатаДо.Value.CompareTo(ДатаОт.Value) == -1 && !ВесьДень.Checked) {
                ДатаДо.Value = ДатаОт.Value;
                ДатаДо.Value = ДатаДо.Value.AddHours(1);
            }
            else {
                if (ДатаДо.Value.CompareTo(ДатаОт.Value) == -1) ДатаДо.Value = ДатаОт.Value;
            }

            if ((ДатаДо.Value - ДатаОт.Value).Days != 0) {
                ВесьДень.Enabled = false;
                ВесьДень.Checked = false;
            }
            else {
                ВесьДень.Enabled = true;
            }

            if (ВесьДень.Checked) {
                var add_hour = ДатаОт.Value.Hour;
                ДатаОт.Value = ДатаОт.Value.AddHours(-add_hour);

                var add_minute = ДатаОт.Value.Minute;
                ДатаОт.Value = ДатаОт.Value.AddMinutes(-add_minute);

                add_hour = 23 - ДатаДо.Value.Hour;
                ДатаДо.Value = ДатаДо.Value.AddHours(add_hour);

                add_minute = 59 - ДатаДо.Value.Minute;
                ДатаДо.Value = ДатаДо.Value.AddMinutes(add_minute);
            }
        }
    }
}