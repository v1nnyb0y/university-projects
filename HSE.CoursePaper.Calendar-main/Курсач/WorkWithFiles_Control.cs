using System;
using System.Collections.Generic;
using System.IO;
using Курсач.Properties;

namespace Курсач
{
    internal static class WorkWithFiles_Control
    {
        private static readonly Event            @event = new Event();
        private static          Tasks            Tasks  = new Tasks();
        public static           OpenSource_Sport Sport { get; set; }

        //Вывод name
        public static string[] GetTasks => Tasks.GetName;

        //вывод Indexes
        public static int[] GetIndexes => Tasks.GetIndexes;

        //Вывод уведомлений
        public static Dictionary<OverrideDateTime, NameArr>[] Alarm {
            get => @event.ReturnDictionary_Alarm;
            set => @event.ReturnDictionary_Alarm = value;
        }

        public static void DeleteFromClass(int repeat, int importance, Name name, DateTime from, DateTime to) {
            var tmp = @event.ReturnDictionaries;
            var tmp_Dictionaries = tmp[repeat];
            tmp.Remove(repeat);

            var nameArr = tmp_Dictionaries.ReturnDictionary_Events_From[importance][new OverrideDateTime(from)];

            var tmp_alarm = @event.ReturnDictionary_Alarm;
            var overrideDateTime = MainMenu_Control.GetKeyByValue(nameArr, tmp_alarm[repeat]);
            nameArr.DeleteName(name);

            tmp_alarm[repeat].Remove(overrideDateTime);

            tmp_Dictionaries.ReturnDictionary_Events_From[importance].Remove(new OverrideDateTime(from));
            tmp_Dictionaries.ReturnDictionary_Events_To[importance].Remove(new OverrideDateTime(to));

            if (nameArr.Return_size != 0) {
                tmp_Dictionaries.ReturnDictionary_Events_From[importance].Add(new OverrideDateTime(from), nameArr);
                tmp_Dictionaries.ReturnDictionary_Events_To[importance].Add(new OverrideDateTime(to), nameArr);

                tmp_alarm[repeat].Add(overrideDateTime, nameArr);
            }

            tmp.Add(repeat, tmp_Dictionaries);
            @event.ReturnDictionaries = tmp;
        }

        //Загружаем элементы из файлов
        public static void LoadEvents() {
            var alarm = new FileInfo("Resources\\AlarmForEvents.bin");
            var events = new FileInfo("Resources\\EventsTo.bin");

            if (events.Exists && events.Length != 0)
                @event.LoadEvents();

            if (alarm.Exists && alarm.Length != 0)
                @event.LoadAlarms();
        }

        //Сохраняем в файл 2 списка (уведомления/сами события)
        public static void SaveEvents() {
            if (!@event.Empty()) {
                Settings.Default.LoadEvents = true;

                @event.AddNamesDatesToXML();
                @event.AddEventsToXML();

                if (!@event.EmptyAlarm()) {
                    @event.AddAlarmToXML();
                    Settings.Default.LoadAlarms = true;
                }
                else {
                    File.WriteAllText("Resources\\AlarmForEvents.bin", string.Empty);
                    Settings.Default.LoadAlarms = false;
                }
            }
            else {
                File.WriteAllText("Resources\\Names.bin", string.Empty);
                File.WriteAllText("Resources\\EventsTo.bin", string.Empty);
                File.WriteAllText("Resources\\AlarmForEvents.bin", string.Empty);
                Settings.Default.LoadEvents = false;
                Settings.Default.LoadAlarms = false;
            }
        }

        //Добавляем к классу элементы
        public static void AddToClass(
            DateTime From, DateTime To, int indexAlarm, int indexВажность, string Name, string Notes, int repeat) {
            var date = new Date(From, To);
            var name = new Name(Name, Notes);
            switch (indexAlarm) {
                case 0:
                    @event.AddEvent(date, date, name, indexВажность, repeat);
                    break;
                case 1:
                    var alarm = new Date(From = From.AddMinutes(-10));
                    @event.AddEvent(date, alarm, name, indexВажность, repeat);
                    break;
                case 2:
                    alarm = new Date(From = From.AddMinutes(-30));
                    @event.AddEvent(date, alarm, name, indexВажность, repeat);
                    break;
                case 3:
                    alarm = new Date(From = From.AddHours(-1));
                    @event.AddEvent(date, alarm, name, indexВажность, repeat);
                    break;
                case 4:
                    alarm = new Date(From = From.AddDays(-1));
                    @event.AddEvent(date, alarm, name, indexВажность, repeat);
                    break;
            }
        }

        //Вернуть значения для вывода в Дату
        public static Dictionaries ReturnMeanings(int repeat) {
            return @event.ReturnDictionaries[repeat];
        }

        //Добавляем события
        public static void AddTask(string Name, string Conditions) {
            Tasks.AddTask(Name, Conditions);
        }

        //Сохранение задач
        public static void SaveTasks() {
            if (!Tasks.Empty()) {
                Tasks.SaveTasksToXML();

                Settings.Default.LoadTasks = true;
            }
            else {
                File.WriteAllText("Resources\\Tasks.bin", string.Empty);
                Settings.Default.LoadTasks = false;
            }
        }

        //Загрузка задач
        public static void LoadTasks() {
            var fileInfo = new FileInfo("Resources\\Tasks.bin");

            if (fileInfo.Exists && fileInfo.Length != 0) Tasks.LoadTasksFromXML(ref Tasks);
        }

        //изменяем индекс
        public static void ChangeIndex(int index, bool ok) {
            if (ok)
                Tasks.CheckElement(index);
            else
                Tasks.OutCheckIndex(index);
        }

        public static bool EmptyAlarm() {
            return @event.EmptyAlarm();
        }

        public static void LoadOpenSource(string source) {
            Sport = new OpenSource_Sport();
            Sport.LoadXml(source);
        }

        public static string DeterminationDescription(string name) {
            var names = Tasks.GetName;
            var condition = Tasks.GetConditions;
            var i = 0;
            for (i = 0; i < names.Length; i++)
                if (names[i].CompareTo(name) == 0)
                    break;
            return condition[i];
        }

        public static void DeleteFromClass(string name, string description, bool Checked, int index) {
            Tasks.Delete(name, description, Checked, index);
        }

        public static OverrideDateTime FindElementFrom(string name, int repeat, int importance) {
            var outTMP = new OverrideDateTime();
            foreach (var overrideDateTime in ReturnMeanings(repeat).ReturnDictionary_Events_From[importance].Keys)
                if (ReturnMeanings(repeat).ReturnDictionary_Events_From[importance][overrideDateTime].IsInArray(name)) {
                    outTMP = overrideDateTime;
                    break;
                }

            return outTMP;
        }

        public static OverrideDateTime FindElementTo(string name, int repeat, int importance) {
            var outTMP = new OverrideDateTime();
            foreach (var overrideDateTime in ReturnMeanings(repeat).ReturnDictionary_Events_To[importance].Keys)
                if (ReturnMeanings(repeat).ReturnDictionary_Events_To[importance][overrideDateTime].IsInArray(name)) {
                    outTMP = overrideDateTime;
                    break;
                }

            return outTMP;
        }
    }
}