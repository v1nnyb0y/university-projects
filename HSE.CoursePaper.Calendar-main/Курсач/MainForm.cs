using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Курсач.Properties;

namespace Курсач
{
    public class ГлавнаяФорма : Form
    {
        private Thread streamAlarm;
        private Thread streamLoad;


        public ГлавнаяФорма() {
            InitializeComponent();
        }

        //Перевод элементов отмеченных в спорте в массив стринг
        private string[] CheckedItemsToStringArray() {
            var items = new string[ВидСпорта.CheckedItems.Count];
            for (var i = 0; i < ВидСпорта.CheckedItems.Count; i++)
                items[i] = ВидСпорта.CheckedItems[i].ToString();
            return items;
        }

        //Загрузка формы
        private void ГлавнаяФорма_Load(object sender, EventArgs e) {
            //Потом убрать. Только для быстроты работы (тестирования)
            WorkWithFiles_Control.LoadEvents();
            WorkWithFiles_Control.LoadTasks();
            MainMenu_Control.Load();
            Options_Control.LoadOptions();

            for (var j = 0; j < 3; j++) {
                for (var i = 0; i < 4; i++) {
                    Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(i);
                    switch (i) {
                        case 0:
                            foreach (var element in valuePairs.ReturnDictionary_Events_From[j].Keys) {
                                Send.Date = new DateTime[2];
                                Send.Date[0] = element.GetDateTime;
                                Send.Date[1] = MainMenu_Control.
                                    GetKeyByValue(valuePairs.ReturnDictionary_Events_From[j][element],
                                                  valuePairs.ReturnDictionary_Events_To[j]).GetDateTime;

                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            break;
                        case 1:
                            foreach (var element in valuePairs.ReturnDictionary_Events_From[j].Keys) {
                                Send.Date = new DateTime[2];
                                Send.Date[0] = element.GetDateTime;
                                Send.Date[1] = MainMenu_Control.
                                    GetKeyByValue(valuePairs.ReturnDictionary_Events_From[j][element],
                                                  valuePairs.ReturnDictionary_Events_To[j]).GetDateTime;

                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            break;
                        case 2:
                            foreach (var element in valuePairs.ReturnDictionary_Events_From[j].Keys) {
                                Send.Date = new DateTime[2];
                                Send.Date[0] = element.GetDateTime;
                                Send.Date[1] = MainMenu_Control.
                                    GetKeyByValue(valuePairs.ReturnDictionary_Events_From[j][element],
                                                  valuePairs.ReturnDictionary_Events_To[j]).GetDateTime;

                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            break;
                        case 3:
                            foreach (var element in valuePairs.ReturnDictionary_Events_From[j].Keys) {
                                Send.Date = new DateTime[2];
                                Send.Date[0] = element.GetDateTime;
                                Send.Date[1] = MainMenu_Control.
                                    GetKeyByValue(valuePairs.ReturnDictionary_Events_From[j][element],
                                                  valuePairs.ReturnDictionary_Events_To[j]).GetDateTime;

                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            break;
                    }
                }
            }


            ClassOptions classOptions = Options_Control.GetClassOptions;
            switch (classOptions.FirstDay) {
                case 0:
                    Календарь.FirstDayOfWeek = Day.Monday;
                    break;
                case 1:
                    Календарь.FirstDayOfWeek = Day.Tuesday;
                    break;
                case 2:
                    Календарь.FirstDayOfWeek = Day.Wednesday;
                    break;
                case 3:
                    Календарь.FirstDayOfWeek = Day.Thursday;
                    break;
                case 4:
                    Календарь.FirstDayOfWeek = Day.Friday;
                    break;
                case 5:
                    Календарь.FirstDayOfWeek = Day.Saturday;
                    break;
                case 6:
                    Календарь.FirstDayOfWeek = Day.Sunday;
                    break;
            }

            if (classOptions.NotAlarm) {
                if (streamAlarm != null)
                    streamAlarm.Abort();
            }
            else {
                StartAlarms();
            }

            Календарь.ShowWeekNumbers = classOptions.NumberWeeks;

            Календарь.Refresh();

            ОтображениеВажных.Columns.Add("Время", "Время");
            ОтображениеНеобязательных.Columns.Add("Время", "Время");
            ОтображениеОбычных.Columns.Add("Время", "Время");

            ОтображениеВажных.Rows.Add(24);
            ОтображениеНеобязательных.Rows.Add(24);
            ОтображениеОбычных.Rows.Add(24);

            ОтображениеВажных.RowHeadersVisible = false;
            ОтображениеНеобязательных.RowHeadersVisible = false;
            ОтображениеОбычных.RowHeadersVisible = false;

            SportEvents.Font = new Font("Microsoft Sans Serif", 8);
            SportEvents.RowHeadersVisible = false;

            for (var i = 0; i < 24; i++) {
                ОтображениеВажных[0, i].Value = i + ":"         + "00";
                ОтображениеНеобязательных[0, i].Value = i + ":" + "00";
                ОтображениеОбычных[0, i].Value = i + ":"        + "00";
            }

            Рекомендации.Text = info.recomendation;

        #region ChangeTextBox

            Фамилия.Text = info.name;
            Имя.Text = info.se_name;
            Отчество.Text = info.father_name;
            Вес.Text = info.weight;
            if (info.age.Year != 1) {
                if (info.age.Year == DateTime.Now.Year) {
                    ДатаРождения.Text = null;
                    info.age = DateTime.Now;
                }
                else {
                    ДатаРождения.Text = info.age.ToString();
                }
            }
            else {
                info.age = DateTime.Now;
                ДатаРождения.Text = null;
            }

            Рост.Text = info.growth;
            Город.Text = info.city;
            if (info.sex == "M") {
                МужскойПол.Checked = true;
            }
            else {
                if (info.sex == "W") ЖенскийПол.Checked = true;
            }

        #endregion

            ВидСпорта.Items.Clear();
            WorkWithFiles_Control.LoadOpenSource("OpenSource\\Перечень мероприятий по физической куль.xml");
            if (info.name_sport != null)
                foreach (string key in WorkWithFiles_Control.Sport.keyValues.Keys)
                    if (info.name_sport.Contains(key))
                        ВидСпорта.Items.Add(key, true);
                    else
                        ВидСпорта.Items.Add(key, false);
            else
                foreach (string key in WorkWithFiles_Control.Sport.keyValues.Keys)
                    ВидСпорта.Items.Add(key);
            if (Settings.Default.AllSport) {
                var i = 0;
                foreach (string key in WorkWithFiles_Control.Sport.keyValues.Keys) {
                    SportArray sportArray = WorkWithFiles_Control.Sport.keyValues[key];
                    foreach (Sport sport in sportArray) {
                        SportEvents.Rows.Add();
                        SportEvents[1, i].Value = sport.Name;
                        SportEvents[2, i].Value = sport.Data;
                        i++;
                    }
                }
            }
            else {
                if (info.name_sport != null) {
                    var i = 0;
                    foreach (var key in info.name_sport) {
                        SportArray sportArray = WorkWithFiles_Control.Sport.keyValues[key];
                        foreach (Sport sport in sportArray) {
                            SportEvents.Rows.Add();
                            SportEvents[1, i].Value = sport.Name;
                            SportEvents[2, i].Value = sport.Data;
                            i++;
                        }
                    }
                }
            }


            ИМТ.ReadOnly = true;
            Рекомендации.ReadOnly = true;
            try {
                Аватарка.Image = new Bitmap("Resources\\main_avatar.png");
            }
            catch {
                Аватарка.Image = new Bitmap("Resources\\avatar.png");
            }

            ПоискДаты.Text = DateTime.Now.ToString();
            VisiblePersonalRoom(false);
            Трэй.Icon = Icon.ExtractAssociatedIcon("Icons\\Calendar.ico");
            Трэй.Visible = true;
            streamAlarm = new Thread(Alarm);

            SportEvents.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            SportEvents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            SportEvents.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            LoadToInter();
            WriteEvents();
            StartAlarms();
            Календарь.UpdateBoldedDates();
            contextMenuStrip1.Enabled = false;
        }

        private void LoadToInter() {
            string[] name = WorkWithFiles_Control.GetTasks;
            int[] indexes = WorkWithFiles_Control.GetIndexes;
            if (name == null) {
                LLНетЗадач.Visible = true;
            }
            else {
                if (name.Length != 0) {
                    LLНетЗадач.Visible = false;

                    Задачи.Items.AddRange(name);
                    for (var i = 0; i < indexes.Length; i++)
                        Задачи.SetItemChecked(indexes[i], true);
                    //Задачи.Items[i] = Brushes.Green;
                }
                else {
                    LLНетЗадач.Visible = true;
                }
            }
        }

        //Функция, которая позволяет сделать видимой или не видимой окно "Личный кабинет"
        private void VisiblePersonalRoom(bool ok) {
            if (ok) Icon = Icon.ExtractAssociatedIcon("Icons\\Person.ico");
            ВозвращениеГлавногоМеню.Visible = ok;
            ЗагрузкаАватара.Visible = ok;
            Применить.Visible = ok;
            Отменить.Visible = ok;
            УдалитьАватар.Visible = ok;
            Аватарка.Visible = ok;
            Рекомендации.Visible = ok;
            ВидСпорта.Visible = ok;
            Фамилия.Visible = ok;
            Имя.Visible = ok;
            Отчество.Visible = ok;
            Вес.Visible = ok;
            ДатаРождения.Visible = ok;
            Рост.Visible = ok;
            ИМТ.Visible = ok;
            Город.Visible = ok;
            LLИмя.Visible = ok;
            LLОтчество.Visible = ok;
            LLВозраст.Visible = ok;
            LLРост.Visible = ok;
            LLВес.Visible = ok;
            LLИМТ.Visible = ok;
            LLРекомендации.Visible = ok;
            LLСпорт.Visible = ok;
            LLИнфо.Visible = ok;
            LLПол.Visible = ok;
            LLГород.Visible = ok;
            LLФамилия.Visible = ok;
            МужскойПол.Visible = ok;
            ЖенскийПол.Visible = ok;
            if (!ok)
                Size = new Size(960, 715); //695
            else
                Size = new Size(530, 630);
        }

        //Функция, которая позволяет сделать видимой или не видимой окно "Главное меню"
        private void VisibleMainForm(bool ok) {
            if (ok) Icon = Icon.ExtractAssociatedIcon("Icons\\Calendar.ico");
            LLСобытия.Visible = ok;
            СобытияТаб.Visible = ok;
            Календарь.Visible = ok;
            LLПоиск.Visible = ok;
            ПоискДаты.Visible = ok;
        }

        //Проверка, что в личный кабинет все необходимые поля были заполнены и все, что заполнены правильно
        private bool CheckRightInput(DateTime age, params string[] text) {
            var ok = false;
            var regex_name = new Regex(@"^[А-Я][а-я]{1,}"); //для имени/фамилии/отчеству
            if (!(regex_name.IsMatch(text[0]) && regex_name.IsMatch(text[1]))) {
                MessageBox.Show("Неверный формат ввода имени/фамилии/ или города", "Личный кабинет",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                if (text[2].Length != 0 && text[5].Length != 0)
                    if (!(regex_name.IsMatch(text[2]) && regex_name.IsMatch(text[5]))) {
                        MessageBox.Show("Неверный формат ввода имени/фамилии/отчеста или города", "Личный кабинет",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return ok;
                    }

                if (text[3].Length != 0)
                    if (int.Parse(text[3]) > 250 || int.Parse(text[3]) < 30) {
                        MessageBox.Show("Введен несуществующий вес человека", "Личный кабинет",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                if (text[4].Length != 0)
                    if (int.Parse(text[4]) > 570 || int.Parse(text[4]) < 2) {
                        MessageBox.Show("Введен несуществующий рост человека", "Личный кабинет",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }


                if (DateTime.Now.Year - age.Year > 100 || DateTime.Now.Year - age.Year < 6) {
                    MessageBox.Show("Введен неправильный возраст человека", "Личный кабинет",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!MainMenu_Control.DaysMonthes(age.Day, age.Month, age.Year))
                    ok = true;
                else
                    MessageBox.Show("Введена несуществующая дата, как возраст человека", "Личный кабинет",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ok;
        }

        //Раскрывать приложение из трэя
        private void Трэй_Click(object sender, EventArgs e) {
            WindowState = FormWindowState.Normal;
            VisibleMainForm(true);
            VisiblePersonalRoom(false);
            ShowInTaskbar = true;
        }

        //Запустить ли поток?
        private void StartAlarms() {
            streamAlarm = new Thread(Alarm);
            if (!WorkWithFiles_Control.EmptyAlarm()) {
                streamAlarm.Start();
                streamAlarm.IsBackground = true;
            }
        }

        //Выход из потока
        private void EndStream() {
            streamAlarm.Abort();
        }

        //Поток для уведомления
        private void Alarm() {
            Dictionary<OverrideDateTime, NameArr>[] alarmArr = WorkWithFiles_Control.Alarm;
            var date_check = DateTime.Now;
            date_check = date_check.AddSeconds(-date_check.Second);
            var k = 0;
            while (true) {
                var j = 0;
                foreach (var alarm in alarmArr) {
                    var ok = true;
                    var date = DateTime.Now;
                    date = date.AddSeconds(-date.Second);
                    if (j == 0 && k != 0)
                        if ((date_check - date).Minutes == 0) {
                            ok = false;
                        }
                        else {
                            date_check = date;
                            k = 0;
                        }

                    if (!ok)
                        break;
                    if (j != 0) {
                        if (j == 1) {
                            foreach (var key in alarm.Keys) {
                                var time = date - key.GetDateTime;
                                if (time.Minutes >= 0 &&
                                    (time.Hours % 24 == 0 && time.Minutes == 0
                                     || new OverrideDateTime(date).Equals(key)))
                                    Message(key, alarm, j);
                            }
                        }
                        else {
                            if (j == 2) {
                                foreach (var key in alarm.Keys) {
                                    var time = date - key.GetDateTime;
                                    if (time.Days % 7 == 0 &&
                                        (time.Hours % 24 == 0 && time.Minutes == 0
                                         || new OverrideDateTime(date).Equals(key)))
                                        Message(key, alarm, j);
                                }
                            }
                            else {
                                if (j == 3)
                                    foreach (var key in alarm.Keys) {
                                        var time = date - key.GetDateTime;
                                        if (DateTime.IsLeapYear(date.Year)) {
                                            if (time.Days % 366 == 0 &&
                                                (time.Hours % 24 == 0 && time.Minutes == 0
                                                 || new OverrideDateTime(date).Equals(key)))
                                                Message(key, alarm, j);
                                        }
                                        else {
                                            if (time.Days % 365 == 0 &&
                                                (time.Hours % 24 == 0 && time.Minutes == 0
                                                 || new OverrideDateTime(date).Equals(key)))
                                                Message(key, alarm, j);
                                        }
                                    }
                            }
                        }
                    }
                    else {
                        if (alarm.ContainsKey(new OverrideDateTime(date)) && alarm.Count != 0) {
                            Message(new OverrideDateTime(date), alarm, j);
                            alarmArr[0].Remove(new OverrideDateTime(date));
                        }

                        k = 1;
                    }

                    j++;
                }
            }
        }

        //Уведомление
        private void Message(OverrideDateTime key, Dictionary<OverrideDateTime, NameArr> alarm, int j) {
            for (var i = 0; i < alarm[key].Return_size; i++) {
                var name = alarm[key][i].EventName;
                var description = alarm[key][i].EventDescription;
                var time = "";
                for (var imp = 0; imp < 3; imp++) {
                    var overrideDate = MainMenu_Control.GetKeyByValue(alarm[key],
                                                                      WorkWithFiles_Control.ReturnMeanings(j).
                                                                          ReturnDictionary_Events_From[imp]);
                    if (!overrideDate.Equals(new OverrideDateTime(DateTime.Parse("1.1.1")))) {
                        var timeSpan = overrideDate.GetDateTime - key.GetDateTime;
                        if (timeSpan.Minutes == 10) {
                            time = "10 минут";
                        }
                        else {
                            if (timeSpan.Minutes == 30) {
                                time = "30 минут";
                            }
                            else {
                                if (timeSpan.Hours == 1)
                                    time = "1 час";
                                else
                                    time = "1 день";
                            }
                        }
                    }
                }

                Трэй.ShowBalloonTip(5, name + ": событие через " + time, description, ToolTipIcon.Info);
            }
        }

        private void Вес_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private bool CheckForCancel() {
            if (info.name_sport != null) {
                if (info.name_sport.Length == ВидСпорта.SelectedItems.Count)
                    for (var i = 0; i < info.name_sport.Length; i++)
                        if (info.name_sport[i].CompareTo(ВидСпорта.CheckedItems[i]) == 0)
                            return true;
                else
                    return false;
            }
            else {
                if (ВидСпорта.SelectedItems.Count != 0)
                    return false;
                return true;
            }

            return false;
        }

        private void ВидСпорта_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Город_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void ДатаРождения_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void ЖенскийПол_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Имя_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void МужскойПол_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Рост_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Фамилия_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Отчество_Leave(object sender, EventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Город_KeyPress(object sender, KeyPressEventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void ДатаРождения_KeyPress(object sender, KeyPressEventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Имя_KeyPress(object sender, KeyPressEventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Отчество_KeyPress(object sender, KeyPressEventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void Фамилия_KeyPress(object sender, KeyPressEventArgs e) {
            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

        private void LoadEvents() {
            var load = new Load();
            load.ShowDialog();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

    #region MainForm

    #region Work_With_Context_Menu

        //Выход из приложения
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        //Отображение окна "Личный кабинет"
        private void войтиВЛичныйКабинетToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!СобытияТаб.Visible)
                SystemSounds.Beep.Play();
            Отменить.Enabled = false;
            VisibleMainForm(false);
            VisiblePersonalRoom(true);
        }

        //Удаление личного кабинета
        private void удалитьЛичныйКабинетToolStripMenuItem_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("Вы уверены, что хотите удалить существующий аккаунт?",
                                         "Мой здоровый календарь",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                info.name = info.se_name = info.father_name = info.city = info.growth =
                    info.weight = info.recomendation = info.sex = "";
                info.age = DateTime.Now;
                info.name_sport = null;
                SportEvents.Rows.Clear();
                Settings.Default.LoadSport = false;
                Settings.Default.AllSport = false;

                CancelForUserInfo();
                CancelForSport();
                CancelSex();
                Рекомендации.Text = info.recomendation;

                VisibleMainForm(true);
                VisiblePersonalRoom(false);

                MessageBox.Show("Существующий аккаунт успешно удален.",
                                "Мой здоровый календарь",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        //Добавить событие (отображение диалогового окна)
        private void добавитьСобытиеToolStripMenuItem_Click(object sender, EventArgs e) {
            var dlg = new СозданиеСобытия();
            dlg.ShowDialog();


            if (DialogResult.OK == dlg.DialogResult) {
                DateTime tmp_min = Календарь.MinDate;
                Календарь.MinDate = DateTime.Now;

                if (Send.repeat == 0)
                    for (var day = Send.Date[0].Date; day.CompareTo(Send.Date[1].Date) <= 0; day = day.AddDays(1))
                        Календарь.AddBoldedDate(day);

                if (Send.repeat == 1) {
                    Send.Date[1] = Send.Date[1].AddYears(1);
                    for (var day = Send.Date[0].Date; day.CompareTo(Send.Date[1].Date) <= 0; day = day.AddDays(1))
                        Календарь.AddBoldedDate(day);
                }

                if (Send.repeat == 2) {
                    var year = Send.Date[1].AddYears(7).Year;
                    while (Send.Date[1].Year <= year) {
                        for (var day = Send.Date[0].Date; day.CompareTo(Send.Date[1].Date) <= 0; day = day.AddDays(1))
                            Календарь.AddBoldedDate(day);
                        Send.Date[0] = Send.Date[0].AddDays(7);
                        Send.Date[1] = Send.Date[1].AddDays(7);
                    }
                }

                if (Send.repeat == 3) {
                    var year = Send.Date[1].AddYears(365).Year;

                    if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                    while (Send.Date[1].Year <= year) {
                        for (var day = Send.Date[0].Date; day.CompareTo(Send.Date[1].Date) <= 0; day = day.AddDays(1))
                            Календарь.AddBoldedDate(day);
                        Send.Date[0] = Send.Date[0].AddYears(1);
                        Send.Date[1] = Send.Date[1].AddYears(1);
                    }
                }

                WriteEvents();
                Календарь.MinDate = tmp_min;
            }

            ClassOptions classOptions = Options_Control.GetClassOptions;
            Календарь.UpdateBoldedDates();
            if (classOptions.NotAlarm) {
                if (!streamAlarm.IsAlive)
                    StartAlarms();
            }
            else {
                streamAlarm.Abort();
                streamAlarm = new Thread(Alarm);
                StartAlarms();
            }
        }

        //Отображение информации о пользовании
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) {
            var dlg = new AboutProgramm();
            dlg.ShowDialog();
        }

        //Настройки приложения
        private void персонализацияПриложенияToolStripMenuItem_Click(object sender, EventArgs e) {
            var options = new Options();
            var dlg = options.ShowDialog();

            if (dlg == DialogResult.OK) {
                ClassOptions classOptions = Options_Control.GetClassOptions;
                switch (classOptions.FirstDay) {
                    case 0:
                        Календарь.FirstDayOfWeek = Day.Monday;
                        break;
                    case 1:
                        Календарь.FirstDayOfWeek = Day.Tuesday;
                        break;
                    case 2:
                        Календарь.FirstDayOfWeek = Day.Wednesday;
                        break;
                    case 3:
                        Календарь.FirstDayOfWeek = Day.Thursday;
                        break;
                    case 4:
                        Календарь.FirstDayOfWeek = Day.Friday;
                        break;
                    case 5:
                        Календарь.FirstDayOfWeek = Day.Saturday;
                        break;
                    case 6:
                        Календарь.FirstDayOfWeek = Day.Sunday;
                        break;
                }

                if (classOptions.NotAlarm) {
                    streamAlarm.Abort();
                }
                else {
                    streamAlarm = new Thread(Alarm);
                    StartAlarms();
                }

                Календарь.ShowWeekNumbers = classOptions.NumberWeeks;

                Календарь.Refresh();
            }
        }

        //Добавить задачу
        private void добавитьЗадачуToolStripMenuItem_Click(object sender, EventArgs e) {
            var dlg = new СозданиеЗадания();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK) {
                LLНетЗадач.Visible = false;
                Задачи.Items.Add(Send.GetTasks.GetName[0]);
            }
        }

    #endregion

    #region Work_With_Dates

        //На удаление
        private void ОтображениеВажных_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            var column = e.ColumnIndex;
            var row = e.RowIndex;
            if (row != -1)
                if (ОтображениеВажных[column, row].Value as string != null && column != 0) {
                    var str = ОтображениеВажных[column, row].Value as string;
                    if (str.Contains("Начало: ")) {
                        var tmp = str.Split(' ');
                        var arr = tmp[1].Split(':');
                        var from = new DateTime(Календарь.SelectionStart.Year, Календарь.SelectionStart.Month,
                                                Календарь.SelectionStart.Day, int.Parse(arr[0]), int.Parse(arr[1]), 0);

                        MainMenu_Control.FixDeleteEvent_Start(tmp, ref str, ref from, out var timeSpan, out var i,
                                                              out var to, out var description, 0, out var AllDay);

                        var dlg = new СозданиеСобытия();
                        if (timeSpan.Days == 1) {
                            dlg = new СозданиеСобытия(true, 0, 4, i, from, to, str, description, AllDay);
                        }
                        else {
                            if (timeSpan.Hours == 1) {
                                dlg = new СозданиеСобытия(true, 0, 3, i, from, to, str, description, AllDay);
                            }
                            else {
                                if (timeSpan.Minutes == 30) {
                                    dlg = new СозданиеСобытия(true, 0, 2, i, from, to, str, description, AllDay);
                                }
                                else {
                                    if (timeSpan.Minutes == 10)
                                        dlg = new СозданиеСобытия(true, 0, 1, i, from, to, str, description, AllDay);
                                    else
                                        dlg = new СозданиеСобытия(true, 0, 0, i, from, to, str, description, AllDay);
                                }
                            }
                        }

                        dlg.ShowDialog();

                        Календарь.RemoveAllBoldedDates();
                        for (var imp = 0; imp < 4; imp++) {
                            Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(imp);
                            switch (imp) {
                                case 0:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 1:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        Send.Date[1] = Send.Date[1].AddYears(1);
                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 2:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        var year = Send.Date[1].AddYears(7).Year;
                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddDays(7);
                                            Send.Date[1] = Send.Date[1].AddDays(7);
                                        }
                                    }

                                    break;
                                case 3:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        var year = Send.Date[1].AddYears(365).Year;

                                        if (DateTime.IsLeapYear(Send.Date[1].Year))
                                            year = Send.Date[1].AddYears(1).Year;


                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddYears(1);
                                            Send.Date[1] = Send.Date[1].AddYears(1);
                                        }
                                    }

                                    break;
                            }
                        }

                        if (DialogResult.OK == dlg.DialogResult) {
                            DateTime tmp_min = Календарь.MinDate;
                            Календарь.MinDate = DateTime.Now;

                            if (Send.repeat == 0)
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1))
                                    Календарь.AddBoldedDate(day);

                            if (Send.repeat == 1) {
                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            if (Send.repeat == 2) {
                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            if (Send.repeat == 3) {
                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            Календарь.MinDate = tmp_min;
                        }
                    }
                    else {
                        var tmp = str.Split(' ');
                        var arr = tmp[1].Split(':');
                        var to = new DateTime(Календарь.SelectionStart.Year, Календарь.SelectionStart.Month,
                                              Календарь.SelectionStart.Day, int.Parse(arr[0]), int.Parse(arr[1]), 0);
                        MainMenu_Control.FixDeleteEvent_End(tmp, ref str, ref to, out var timeSpan, out var i,
                                                            out var from, out var description, 0, out var AllDay);
                        var dlg = new СозданиеСобытия();
                        if (timeSpan.Days == 1) {
                            dlg = new СозданиеСобытия(true, 0, 4, i, from, to, str, description, AllDay);
                        }
                        else {
                            if (timeSpan.Hours == 1) {
                                dlg = new СозданиеСобытия(true, 0, 3, i, from, to, str, description, AllDay);
                            }
                            else {
                                if (timeSpan.Minutes == 30) {
                                    dlg = new СозданиеСобытия(true, 0, 2, i, from, to, str, description, AllDay);
                                }
                                else {
                                    if (timeSpan.Minutes == 10)
                                        dlg = new СозданиеСобытия(true, 0, 1, i, from, to, str, description, AllDay);
                                    else
                                        dlg = new СозданиеСобытия(true, 0, 0, i, from, to, str, description, AllDay);
                                }
                            }
                        }

                        dlg.ShowDialog();

                        Календарь.RemoveAllBoldedDates();

                        for (var imp = 0; imp < 4; imp++) {
                            Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(imp);
                            switch (imp) {
                                case 0:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 1:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        Send.Date[1] = Send.Date[1].AddYears(1);
                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 2:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        var year = Send.Date[1].AddYears(7).Year;
                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddDays(7);
                                            Send.Date[1] = Send.Date[1].AddDays(7);
                                        }
                                    }

                                    break;
                                case 3:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[0].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[0][element],
                                                          valuePairs.ReturnDictionary_Events_To[0]).GetDateTime;

                                        var year = Send.Date[1].AddYears(365).Year;

                                        if (DateTime.IsLeapYear(Send.Date[1].Year))
                                            year = Send.Date[1].AddYears(1).Year;


                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddYears(1);
                                            Send.Date[1] = Send.Date[1].AddYears(1);
                                        }
                                    }

                                    break;
                            }
                        }

                        if (DialogResult.OK == dlg.DialogResult) {
                            DateTime tmp_min = Календарь.MinDate;
                            Календарь.MinDate = DateTime.Now;

                            if (Send.repeat == 0)
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1))
                                    Календарь.AddBoldedDate(day);

                            if (Send.repeat == 1) {
                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            if (Send.repeat == 2) {
                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            if (Send.repeat == 3) {
                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            Календарь.MinDate = tmp_min;
                        }
                    }

                    ClassOptions classOptions = Options_Control.GetClassOptions;
                    Календарь.UpdateBoldedDates();
                    if (classOptions.NotAlarm)
                        if (!streamAlarm.IsAlive) {
                            StartAlarms();
                        }
                        else {
                            streamAlarm.Abort();
                            streamAlarm = new Thread(Alarm);
                            StartAlarms();
                        }
                }

            WriteEvents();
        }

        //На удаление
        private void ОтображениеОбычных_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            var column = e.ColumnIndex;
            var row = e.RowIndex;
            if (row != -1)
                if (ОтображениеОбычных[column, row].Value as string != null && column != 0) {
                    var str = ОтображениеОбычных[column, row].Value as string;
                    if (str.Contains("Начало: ")) {
                        var tmp = str.Split(' ');
                        var arr = tmp[1].Split(':');
                        var from = new DateTime(Календарь.SelectionStart.Year, Календарь.SelectionStart.Month,
                                                Календарь.SelectionStart.Day, int.Parse(arr[0]), int.Parse(arr[1]), 0);

                        MainMenu_Control.FixDeleteEvent_Start(tmp, ref str, ref from, out var timeSpan, out var i,
                                                              out var to, out var description, 1, out var AllDay);

                        var dlg = new СозданиеСобытия();
                        if (timeSpan.Days == 1) {
                            dlg = new СозданиеСобытия(true, 1, 4, i, from, to, str, description, AllDay);
                        }
                        else {
                            if (timeSpan.Hours == 1) {
                                dlg = new СозданиеСобытия(true, 1, 3, i, from, to, str, description, AllDay);
                            }
                            else {
                                if (timeSpan.Minutes == 30) {
                                    dlg = new СозданиеСобытия(true, 1, 2, i, from, to, str, description, AllDay);
                                }
                                else {
                                    if (timeSpan.Minutes == 10)
                                        dlg = new СозданиеСобытия(true, 1, 1, i, from, to, str, description, AllDay);
                                    else
                                        dlg = new СозданиеСобытия(true, 1, 0, i, from, to, str, description, AllDay);
                                }
                            }
                        }

                        dlg.ShowDialog();

                        Календарь.RemoveAllBoldedDates();
                        for (var imp = 0; imp < 4; imp++) {
                            Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(imp);
                            switch (imp) {
                                case 0:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 1:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        Send.Date[1] = Send.Date[1].AddYears(1);
                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 2:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        var year = Send.Date[1].AddYears(7).Year;
                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddDays(7);
                                            Send.Date[1] = Send.Date[1].AddDays(7);
                                        }
                                    }

                                    break;
                                case 3:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        var year = Send.Date[1].AddYears(365).Year;

                                        if (DateTime.IsLeapYear(Send.Date[1].Year))
                                            year = Send.Date[1].AddYears(1).Year;


                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddYears(1);
                                            Send.Date[1] = Send.Date[1].AddYears(1);
                                        }
                                    }

                                    break;
                            }
                        }

                        if (DialogResult.OK == dlg.DialogResult) {
                            DateTime tmp_min = Календарь.MinDate;
                            Календарь.MinDate = DateTime.Now;

                            if (Send.repeat == 0)
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1))
                                    Календарь.AddBoldedDate(day);

                            if (Send.repeat == 1) {
                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            if (Send.repeat == 2) {
                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            if (Send.repeat == 3) {
                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            Календарь.MinDate = tmp_min;
                        }
                    }
                    else {
                        var tmp = str.Split(' ');
                        var arr = tmp[1].Split(':');
                        var from = new DateTime(Календарь.SelectionStart.Year, Календарь.SelectionStart.Month,
                                                Календарь.SelectionStart.Day, int.Parse(arr[0]), int.Parse(arr[1]), 0);

                        MainMenu_Control.FixDeleteEvent_End(tmp, ref str, ref from, out var timeSpan, out var i,
                                                            out var to, out var description, 1, out var AllDay);

                        var dlg = new СозданиеСобытия();
                        if (timeSpan.Days == 1) {
                            dlg = new СозданиеСобытия(true, 1, 4, i, from, to, str, description, AllDay);
                        }
                        else {
                            if (timeSpan.Hours == 1) {
                                dlg = new СозданиеСобытия(true, 1, 3, i, from, to, str, description, AllDay);
                            }
                            else {
                                if (timeSpan.Minutes == 30) {
                                    dlg = new СозданиеСобытия(true, 1, 2, i, from, to, str, description, AllDay);
                                }
                                else {
                                    if (timeSpan.Minutes == 10)
                                        dlg = new СозданиеСобытия(true, 1, 1, i, from, to, str, description, AllDay);
                                    else
                                        dlg = new СозданиеСобытия(true, 1, 0, i, from, to, str, description, AllDay);
                                }
                            }
                        }

                        dlg.ShowDialog();

                        Календарь.RemoveAllBoldedDates();
                        for (var imp = 0; imp < 4; imp++) {
                            Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(imp);
                            switch (imp) {
                                case 0:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 1:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        Send.Date[1] = Send.Date[1].AddYears(1);
                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 2:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        var year = Send.Date[1].AddYears(7).Year;
                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddDays(7);
                                            Send.Date[1] = Send.Date[1].AddDays(7);
                                        }
                                    }

                                    break;
                                case 3:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[1].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[1][element],
                                                          valuePairs.ReturnDictionary_Events_To[1]).GetDateTime;

                                        var year = Send.Date[1].AddYears(365).Year;

                                        if (DateTime.IsLeapYear(Send.Date[1].Year))
                                            year = Send.Date[1].AddYears(1).Year;


                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddYears(1);
                                            Send.Date[1] = Send.Date[1].AddYears(1);
                                        }
                                    }

                                    break;
                            }
                        }

                        if (DialogResult.OK == dlg.DialogResult) {
                            DateTime tmp_min = Календарь.MinDate;
                            Календарь.MinDate = DateTime.Now;

                            if (Send.repeat == 0)
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1))
                                    Календарь.AddBoldedDate(day);

                            if (Send.repeat == 1) {
                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            if (Send.repeat == 2) {
                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            if (Send.repeat == 3) {
                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            Календарь.MinDate = tmp_min;
                        }
                    }

                    ClassOptions classOptions = Options_Control.GetClassOptions;
                    Календарь.UpdateBoldedDates();
                    if (classOptions.NotAlarm)
                        if (!streamAlarm.IsAlive) {
                            StartAlarms();
                        }
                        else {
                            streamAlarm.Abort();
                            streamAlarm = new Thread(Alarm);
                            StartAlarms();
                        }
                }

            WriteEvents();
        }

        //На удаление
        private void ОтображениеНеобязательных_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            var column = e.ColumnIndex;
            var row = e.RowIndex;
            if (row != -1)
                if (ОтображениеНеобязательных[column, row].Value as string != null && column != 0) {
                    var str = ОтображениеНеобязательных[column, row].Value as string;
                    if (str.Contains("Начало: ")) {
                        var tmp = str.Split(' ');
                        var arr = tmp[1].Split(':');
                        var from = new DateTime(Календарь.SelectionStart.Year, Календарь.SelectionStart.Month,
                                                Календарь.SelectionStart.Day, int.Parse(arr[0]), int.Parse(arr[1]), 0);

                        MainMenu_Control.FixDeleteEvent_Start(tmp, ref str, ref from, out var timeSpan, out var i,
                                                              out var to, out var description, 2, out var AllDay);

                        var dlg = new СозданиеСобытия();
                        if (timeSpan.Days == 1) {
                            dlg = new СозданиеСобытия(true, 2, 4, i, from, to, str, description, AllDay);
                        }
                        else {
                            if (timeSpan.Hours == 1) {
                                dlg = new СозданиеСобытия(true, 2, 3, i, from, to, str, description, AllDay);
                            }
                            else {
                                if (timeSpan.Minutes == 30) {
                                    dlg = new СозданиеСобытия(true, 2, 2, i, from, to, str, description, AllDay);
                                }
                                else {
                                    if (timeSpan.Minutes == 10)
                                        dlg = new СозданиеСобытия(true, 2, 1, i, from, to, str, description, AllDay);
                                    else
                                        dlg = new СозданиеСобытия(true, 2, 0, i, from, to, str, description, AllDay);
                                }
                            }
                        }

                        dlg.ShowDialog();

                        Календарь.RemoveAllBoldedDates();
                        for (var imp = 0; imp < 4; imp++) {
                            Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(imp);
                            switch (imp) {
                                case 0:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 1:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        Send.Date[1] = Send.Date[1].AddYears(1);
                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 2:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        var year = Send.Date[1].AddYears(7).Year;
                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddDays(7);
                                            Send.Date[1] = Send.Date[1].AddDays(7);
                                        }
                                    }

                                    break;
                                case 3:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        var year = Send.Date[1].AddYears(365).Year;

                                        if (DateTime.IsLeapYear(Send.Date[1].Year))
                                            year = Send.Date[1].AddYears(1).Year;


                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddYears(1);
                                            Send.Date[1] = Send.Date[1].AddYears(1);
                                        }
                                    }

                                    break;
                            }
                        }

                        if (DialogResult.OK == dlg.DialogResult) {
                            DateTime tmp_min = Календарь.MinDate;
                            Календарь.MinDate = DateTime.Now;

                            if (Send.repeat == 0)
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1))
                                    Календарь.AddBoldedDate(day);

                            if (Send.repeat == 1) {
                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            if (Send.repeat == 2) {
                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            if (Send.repeat == 3) {
                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            Календарь.MinDate = tmp_min;
                        }
                    }
                    else {
                        var tmp = str.Split(' ');
                        var arr = tmp[1].Split(':');
                        var from = new DateTime(Календарь.SelectionStart.Year, Календарь.SelectionStart.Month,
                                                Календарь.SelectionStart.Day, int.Parse(arr[0]), int.Parse(arr[1]), 0);

                        MainMenu_Control.FixDeleteEvent_End(tmp, ref str, ref from, out var timeSpan, out var i,
                                                            out var to, out var description, 2, out var AllDay);

                        var dlg = new СозданиеСобытия();
                        if (timeSpan.Days == 1) {
                            dlg = new СозданиеСобытия(true, 2, 4, i, from, to, str, description, AllDay);
                        }
                        else {
                            if (timeSpan.Hours == 1) {
                                dlg = new СозданиеСобытия(true, 2, 3, i, from, to, str, description, AllDay);
                            }
                            else {
                                if (timeSpan.Minutes == 30) {
                                    dlg = new СозданиеСобытия(true, 2, 2, i, from, to, str, description, AllDay);
                                }
                                else {
                                    if (timeSpan.Minutes == 10)
                                        dlg = new СозданиеСобытия(true, 2, 1, i, from, to, str, description, AllDay);
                                    else
                                        dlg = new СозданиеСобытия(true, 2, 0, i, from, to, str, description, AllDay);
                                }
                            }
                        }

                        dlg.ShowDialog();

                        Календарь.RemoveAllBoldedDates();
                        for (var imp = 0; imp < 4; imp++) {
                            Dictionaries valuePairs = WorkWithFiles_Control.ReturnMeanings(imp);
                            switch (imp) {
                                case 0:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 1:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        Send.Date[1] = Send.Date[1].AddYears(1);
                                        for (var day = Send.Date[0].Date;
                                             day.CompareTo(Send.Date[1].Date) <= 0;
                                             day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    }

                                    break;
                                case 2:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        var year = Send.Date[1].AddYears(7).Year;
                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddDays(7);
                                            Send.Date[1] = Send.Date[1].AddDays(7);
                                        }
                                    }

                                    break;
                                case 3:
                                    foreach (var element in valuePairs.ReturnDictionary_Events_From[2].Keys) {
                                        Send.Date = new DateTime[2];
                                        Send.Date[0] = element.GetDateTime;
                                        Send.Date[1] = MainMenu_Control.
                                            GetKeyByValue(valuePairs.ReturnDictionary_Events_From[2][element],
                                                          valuePairs.ReturnDictionary_Events_To[2]).GetDateTime;

                                        var year = Send.Date[1].AddYears(365).Year;

                                        if (DateTime.IsLeapYear(Send.Date[1].Year))
                                            year = Send.Date[1].AddYears(1).Year;


                                        while (Send.Date[1].Year <= year) {
                                            for (var day = Send.Date[0].Date;
                                                 day.CompareTo(Send.Date[1].Date) <= 0;
                                                 day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                            Send.Date[0] = Send.Date[0].AddYears(1);
                                            Send.Date[1] = Send.Date[1].AddYears(1);
                                        }
                                    }

                                    break;
                            }
                        }

                        if (DialogResult.OK == dlg.DialogResult) {
                            DateTime tmp_min = Календарь.MinDate;
                            Календарь.MinDate = DateTime.Now;

                            if (Send.repeat == 0)
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1))
                                    Календарь.AddBoldedDate(day);

                            if (Send.repeat == 1) {
                                Send.Date[1] = Send.Date[1].AddYears(1);
                                for (var day = Send.Date[0].Date;
                                     day.CompareTo(Send.Date[1].Date) <= 0;
                                     day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                            }

                            if (Send.repeat == 2) {
                                var year = Send.Date[1].AddYears(7).Year;
                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddDays(7);
                                    Send.Date[1] = Send.Date[1].AddDays(7);
                                }
                            }

                            if (Send.repeat == 3) {
                                var year = Send.Date[1].AddYears(365).Year;

                                if (DateTime.IsLeapYear(Send.Date[1].Year)) year = Send.Date[1].AddYears(1).Year;


                                while (Send.Date[1].Year <= year) {
                                    for (var day = Send.Date[0].Date;
                                         day.CompareTo(Send.Date[1].Date) <= 0;
                                         day = day.AddDays(1)) Календарь.AddBoldedDate(day);
                                    Send.Date[0] = Send.Date[0].AddYears(1);
                                    Send.Date[1] = Send.Date[1].AddYears(1);
                                }
                            }

                            Календарь.MinDate = tmp_min;
                        }
                    }
                }

            WriteEvents();
        }

        //Отметить событие выполеннным
        private void SportEvents_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            var i = e.RowIndex;
            if (i != -1) {
                var dlg = MessageBox.Show("Отметить событие выполненным?", "Мой здоровый календарь.",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                    SportEvents[0, i].Value = true;
                else
                    try {
                        if ((bool) SportEvents[0, i].Value) {
                            dlg = MessageBox.Show("Отметить событие невыполненным?", "Мой здоровый календарь.",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlg == DialogResult.Yes) SportEvents[0, i].Value = false;
                        }
                    }
                    catch {
                    }
            }
        }

        //дописать вывод планов в DataGridView
        private void Календарь_DateSelected(object sender, DateRangeEventArgs e) {
            WriteEvents();
        }

        //Вывод ивентов в гриды
        private void WriteEvents() {
            ОтображениеВажных.Rows.Clear();
            ОтображениеВажных.Columns.Clear();

            ОтображениеНеобязательных.Rows.Clear();
            ОтображениеНеобязательных.Columns.Clear();

            ОтображениеОбычных.Rows.Clear();
            ОтображениеОбычных.Columns.Clear();

            for (var i = 0; i < 3; i++) {
                var value = MainMenu_Control.EventsToMatrix(Календарь.SelectionStart, i, out var columnNames);
                switch (i) {
                    case 0:
                        EventsToGridВажные(value, columnNames);
                        break;
                    case 1:
                        EventsToGridОбычные(value, columnNames);
                        break;
                    case 2:
                        EventsToGridнеобязательные(value, columnNames);
                        break;
                }
            }
        }

        //Заполнение грида для важныхь событий
        private void EventsToGridВажные(string[,] values, string[] columnNames) {
            ОтображениеВажных.Columns.Add(columnNames[0], columnNames[0]);
            ОтображениеВажных.Rows.Add(24);

            for (var i = 0; i < values.GetLength(0); i++) ОтображениеВажных[0, i].Value = values[i, 0];

            for (var j = 1; j < columnNames.Length; j++) {
                ОтображениеВажных.Columns.Add(columnNames[j], columnNames[j]);
                for (var i = 0; i < values.GetLength(0); i++) ОтображениеВажных[j, i].Value = values[i, j];
            }
        }

        //Заполенение грида для обычных событий
        private void EventsToGridОбычные(string[,] values, string[] columnNames) {
            ОтображениеОбычных.Columns.Add(columnNames[0], columnNames[0]);
            ОтображениеОбычных.Rows.Add(24);

            for (var i = 0; i < values.GetLength(0); i++) ОтображениеОбычных[0, i].Value = values[i, 0];

            for (var j = 1; j < columnNames.Length; j++) {
                ОтображениеОбычных.Columns.Add(columnNames[j], columnNames[j]);
                for (var i = 0; i < values.GetLength(0); i++) ОтображениеОбычных[j, i].Value = values[i, j];
            }
        }

        //Заполенняи Грида для необязательный событий
        private void EventsToGridнеобязательные(string[,] values, string[] columnNames) {
            ОтображениеНеобязательных.Columns.Add(columnNames[0], columnNames[0]);
            ОтображениеНеобязательных.Rows.Add(24);

            for (var i = 0; i < values.GetLength(0); i++) ОтображениеНеобязательных[0, i].Value = values[i, 0];

            for (var j = 1; j < columnNames.Length; j++) {
                ОтображениеНеобязательных.Columns.Add(columnNames[j], columnNames[j]);
                for (var i = 0; i < values.GetLength(0); i++) ОтображениеНеобязательных[j, i].Value = values[i, j];
            }
        }

        //дописать вывод планов в DataGridView
        private void ПоискДаты_Leave(object sender, EventArgs e) {
            if (Календарь.Visible) {
                string[] text = ПоискДаты.Text.Split('.');
                var day = Convert.ToInt32(text[0]);
                var month = Convert.ToInt32(text[1]);
                var year = Convert.ToInt32(text[2]);
                if (year < 999) {
                    MessageBox.Show("Ошибка! Год введен неверно.", "Мой здоровый календарь",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ПоискДаты.Text = DateTime.Now.ToString();
                }
                else if (MainMenu_Control.DaysMonthes(day, month, year) || day > 31 || month > 12) {
                    MessageBox.Show("Ошибка! Введена не существующая дата.", "Мой здоровый календарь",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ПоискДаты.Text = DateTime.Now.ToString();
                }
                else {
                    var dlg = MessageBox.Show("Вывести введенную дату?.", "Мой здоровый календарь",
                                              MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (DialogResult.OK == dlg) {
                        Календарь.SetDate(DateTime.Parse(ПоискДаты.Text));
                        WriteEvents();
                    }
                    else {
                        ПоискДаты.Text = DateTime.Now.ToString();
                    }
                }
            }
        }

        //дописать вывод планов в DataGridView
        private void ПоискДаты_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char) Keys.Enter || e.KeyChar == (char) Keys.Tab)
                if (Календарь.Visible) {
                    string[] text = ПоискДаты.Text.Split('.');
                    var day = Convert.ToInt32(text[0]);
                    var month = Convert.ToInt32(text[1]);
                    var year = Convert.ToInt32(text[2]);
                    if (year < 999) {
                        MessageBox.Show("Ошибка! Год введен неверно.", "Мой здоровый календарь",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ПоискДаты.Text = DateTime.Now.ToString();
                    }
                    else if (MainMenu_Control.DaysMonthes(day, month, year) || day > 31 || month > 12) {
                        MessageBox.Show("Ошибка! Введена не существующая дата.", "Мой здоровый календарь",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ПоискДаты.Text = DateTime.Now.ToString();
                    }
                    else {
                        Календарь.SetDate(DateTime.Parse(ПоискДаты.Text));
                        WriteEvents();
                    }
                }
        }

    #endregion

        private void Задачи_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (Задачи.GetItemChecked(Задачи.SelectedIndex)) {
                    var dlg =
                        MessageBox.
                            Show("Вы точно выполнили условие:\n \"" + WorkWithFiles_Control.DeterminationDescription(Задачи.Items[Задачи.SelectedIndex] as string) + "\"",
                                 "Задачи",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlg == DialogResult.Yes)
                        WorkWithFiles_Control.ChangeIndex(Задачи.SelectedIndex,
                                                          Задачи.GetItemChecked(Задачи.SelectedIndex));
                    else
                        Задачи.SetItemChecked(Задачи.SelectedIndex, false);
                }
                else {
                    WorkWithFiles_Control.ChangeIndex(Задачи.SelectedIndex,
                                                      Задачи.GetItemChecked(Задачи.SelectedIndex));
                }
            }
            catch {
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            var name = Задачи.Items[Задачи.SelectedIndex] as string;
            string condition =
                WorkWithFiles_Control.DeterminationDescription(Задачи.Items[Задачи.SelectedIndex] as string);
            int index = Задачи.SelectedIndex;
            WorkWithFiles_Control.DeleteFromClass(name, condition, Задачи.GetItemChecked(Задачи.SelectedIndex),
                                                  Задачи.SelectedIndex);

            var dlg = new СозданиеЗадания(name, condition, true);
            dlg.ShowDialog();
            if (dlg.DialogResult != DialogResult.OK) {
                Задачи.Items.RemoveAt(Задачи.SelectedIndex);
                LLНетЗадач.Visible = true;
            }
            else {
                Задачи.Items.RemoveAt(Задачи.SelectedIndex);
                Задачи.Items.Insert(index, Send.GetTasks.GetName[0]);
            }

            if (Задачи.Items == null) LLНетЗадач.Visible = true;
        }

        private void Задачи_Click(object sender, EventArgs e) {
            if (Задачи.SelectedIndex != -1)
                contextMenuStrip1.Enabled = true;
            else
                contextMenuStrip1.Enabled = false;
        }

        //Происходит призакрытие формы(запоминаем настройки)
        private void ГлавнаяФорма_FormClosing(object sender, FormClosingEventArgs e) {
            var dialog = new DialogResult();
            dialog = MessageBox.Show("Закрыть приложение?", "Мой здоровый календарь",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) {
                Options_Control.SaveOptions();
                MainMenu_Control.Save();
                WorkWithFiles_Control.SaveTasks();
                WorkWithFiles_Control.SaveEvents();
                Settings.Default.Save();
            }
            else {
                e.Cancel = true;
                dialog = MessageBox.Show("Свернуть приложение?", "Мой здоровый календарь",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes) {
                    WindowState = FormWindowState.Minimized;
                    ShowInTaskbar = false;
                }
            }
        }

    #endregion

    #region PersonalRoom

    #region button_cancel

        //Заполнить личную информацию
        private void CancelForUserInfo() {
            Фамилия.Text = info.name;
            Имя.Text = info.se_name;
            Отчество.Text = info.father_name;
            Вес.Text = info.weight;
            if ((DateTime.Now.Date - info.age.Date).Days < 2191)
                ДатаРождения.Text = null;
            else
                ДатаРождения.Text = info.age.ToShortDateString();
            Рост.Text = info.growth;
            Город.Text = info.city;
        }

        //Заполнить информацию о спорте
        private void CancelForSport() {
            if (info.name_sport == null)
                for (var i = 0; i < ВидСпорта.Items.Count; i++)
                    ВидСпорта.SetItemChecked(i, false);
            else
                for (var i = 0; i < ВидСпорта.Items.Count; i++)
                    if (info.name_sport.Contains(ВидСпорта.Items[i].ToString()))
                        ВидСпорта.SetItemChecked(i, true);
                    else
                        ВидСпорта.SetItemChecked(i, false);
        }

        //Запомнить пол
        private void CancelSex() {
            МужскойПол.Checked = false;
            ЖенскийПол.Checked = false;
            if (info.sex != "")
                if (info.sex == "M")
                    МужскойПол.Checked = true;
                else
                    ЖенскийПол.Checked = true;
        }

    #endregion

        //возвращения на главную форму
        private void ВозвращениеГлавногоМеню_Click(object sender, EventArgs e) {
            CancelForUserInfo();
            CancelForSport();
            CancelSex();


            Рекомендации.Text = info.recomendation;
            VisiblePersonalRoom(false);
            VisibleMainForm(true);
        }

    #region Info_About_User

        //Ограничения ввода на рост/вес/возраст
        private void РостВозрастВес_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char) Keys.Back && e.KeyChar != (char) Keys.Delete)
                e.Handled = true;

            if (Вес.Text.CompareTo(info.weight)           != 0 || Фамилия.Text.CompareTo(info.name) != 0 ||
                Имя.Text.CompareTo(info.se_name)          != 0 ||
                Отчество.Text.CompareTo(info.father_name) != 0 || МужскойПол.Checked && info.sex == "W" ||
                ЖенскийПол.Checked                                                   && info.sex == "M" ||
                Город.Text.CompareTo(info.city)  != 0                                                   ||
                Рост.Text.CompareTo(info.growth) != 0                                                   ||
                !CheckForCancel())
                if (info.age.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
                    if (ДатаРождения.Text.CompareTo(info.age.ToShortDateString()) != 0)
                        Отменить.Enabled = true;
                    else
                        Отменить.Enabled = false;
                else
                    Отменить.Enabled = true;
            else
                Отменить.Enabled = false;
        }

    #endregion

    #region AcceptChanges_or_not

        //Принять
        private void Принять_Click(object sender, EventArgs e) {
            var age = DateTime.Now;
            try {
                age = DateTime.Parse(ДатаРождения.Text);
            }
            catch {
                age = DateTime.Now;
            }

            if (CheckRightInput(age, Фамилия.Text, Имя.Text, Отчество.Text, Вес.Text, Рост.Text, Город.Text)) {
                string richtext;
                var sportInterest = CheckedItemsToStringArray();
                var CPR = new CreatePersonalRoom(info.name_sport);
                if (!CPR.button_accept(МужскойПол.Checked, ЖенскийПол.Checked,
                                       sportInterest,
                                       out richtext, age,
                                       Фамилия.Text, Имя.Text, Отчество.Text, Вес.Text,
                                       Рост.Text, ИМТ.Text)) {
                    Рекомендации.Text = richtext;
                    info.city = Город.Text;

                    if (!Settings.Default.LoadSport && sportInterest.Length != 0) {
                        var dlg =
                            MessageBox.Show("Выводить события по всем видам спорта? Прогрмма будет работать долго.",
                                            "Личный кабинет",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlg == DialogResult.Yes) {
                            streamLoad = new Thread(LoadEvents);
                            streamLoad.IsBackground = true;
                            streamLoad.Start();
                            Settings.Default.AllSport = true;
                            var i = 0;
                            foreach (string key in WorkWithFiles_Control.Sport.keyValues.Keys) {
                                SportArray sportArray = WorkWithFiles_Control.Sport.keyValues[key];
                                foreach (Sport sport in sportArray) {
                                    SportEvents.Rows.Add();
                                    SportEvents[0, i].Value = false;
                                    SportEvents[1, i].Value = sport.Name;
                                    SportEvents[2, i].Value = sport.Data;
                                    i++;
                                }
                            }

                            streamLoad.Abort();
                        }
                        else {
                            //DialogResult dialog = MessageBox.Show("Подождите. Идет загрузка событий...");
                            streamLoad = new Thread(LoadEvents);
                            streamLoad.IsBackground = true;
                            streamLoad.Start();
                            var i = 0;
                            foreach (var key in sportInterest) {
                                SportArray sportArray = WorkWithFiles_Control.Sport.keyValues[key];
                                foreach (Sport sport in sportArray) {
                                    SportEvents.Rows.Add();
                                    SportEvents[0, i].Value = false;
                                    SportEvents[1, i].Value = sport.Name;
                                    SportEvents[2, i].Value = sport.Data;
                                    i++;
                                }
                            }

                            streamLoad.Abort();
                        }

                        Settings.Default.LoadSport = true;
                    }
                    else {
                        Settings.Default.LoadSport = true;
                    }

                    if (!Settings.Default.AllSport) {
                        streamLoad = new Thread(LoadEvents);
                        streamLoad.IsBackground = true;
                        streamLoad.Start();
                        SportEvents.Rows.Clear();
                        var i = 0;
                        foreach (var key in sportInterest) {
                            SportArray sportArray = WorkWithFiles_Control.Sport.keyValues[key];
                            foreach (Sport sport in sportArray) {
                                SportEvents.Rows.Add();
                                SportEvents[1, i].Value = sport.Name;
                                SportEvents[2, i].Value = sport.Data;
                                i++;
                            }
                        }

                        streamLoad.Abort();
                    }

                    Отменить.Enabled = false;
                    MessageBox.Show("Личный кабинет успешно создан!", "Мой здоровый календарь",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("Ошибка! Не все обязательные данные заполнены.", "Мой здоровый календарь",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Отменить
        private void Отменить_Click(object sender, EventArgs e) {
            CancelForUserInfo();
            CancelForSport();
            CancelSex();


            Рекомендации.Text = info.recomendation;

            Отменить.Enabled = false;
        }

    #endregion

    #region pictureBox

        //Загрузка аватарки
        private void ЗагрузкаАватара_Click(object sender, EventArgs e) {
            var open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
                try {
                    Аватарка.Image = new Bitmap(open_dialog.FileName);
                    Аватарка.Image.Save("Resources\\main_avatar.png");
                }
                catch {
                    MessageBox.Show("Ошибка!\n" + "Невозможно открыть выбранный файл.", "Мой здоровый календарь",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        //Удалить аватар
        private void УдалитьАватар_Click(object sender, EventArgs e) {
            var dlg = MessageBox.Show("Вы уверены, что хотите удалить аватар?", "Мой здоровый календарь",
                                      MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.OK) {
                Аватарка.Image.Dispose();
                Аватарка.Image = new Bitmap("Resources\\avatar.png");
                File.Delete("Resources\\main_avatar.png");
            }
        }

    #endregion

    #region Sex

        //Выбор мужской полспаси
        private void МужскойПол_CheckedChanged(object sender, EventArgs e) {
            if (МужскойПол.Checked)
                if (ЖенскийПол.Checked)
                    ЖенскийПол.Checked = false;
        }

        //Выбор женский пол
        private void ЖенскийПол_CheckedChanged(object sender, EventArgs e) {
            if (ЖенскийПол.Checked)
                if (МужскойПол.Checked)
                    МужскойПол.Checked = false;
        }

    #endregion

    #endregion
    }
}