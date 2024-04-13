using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Курсач.Properties;

namespace Курсач
{
    internal class MainMenu_Control
    {
        //Сохранение настроек
        public static void Save() {
            Settings.Default.Name = info.name;
            Settings.Default.Se_name = info.se_name;
            Settings.Default.Father_name = info.father_name;
            Settings.Default.Growth = info.growth;
            Settings.Default.Age = info.age;
            Settings.Default.Weight = info.weight;
            Settings.Default.Recomendation = info.recomendation;
            Settings.Default.City = info.city;
            Settings.Default.Sex = info.sex;


            File.WriteAllText("Personal_Room\\sportInfo.bin", string.Empty);
            if (info.name_sport != null) {
                Settings.Default.LoadNamesSport = true;
                var bf = new BinaryFormatter();
                using (var fs = new FileStream("Personal_Room\\sportInfo.bin", FileMode.Open)) {
                    bf.Serialize(fs, info.name_sport);
                }
            }
            else {
                Settings.Default.LoadNamesSport = false;
            }
        }

        public static void Load() {
            var fileInfo = new FileInfo("Personal_Room\\sportInfo.bin");
            if (fileInfo.Length != 0)
                try {
                    var bf = new BinaryFormatter();
                    using (var fs = new FileStream("Personal_Room\\sportInfo.bin", FileMode.Open)) {
                        info.name_sport = (string[]) bf.Deserialize(fs);
                    }
                }
                catch {
                    info.name_sport = null;
                }
        }

        //Определение дней в месяце данного года
        public static bool DaysMonthes(int day, int month, int year) {
            var ok = false;
            if (day > 28 + (month + Math.Truncate((double) month / 8)) % 2 + 2 %
                month    + Math.Truncate((double) (1 +
                                                   (1 - (year % 4 + 2) % (year % 4 + 1)) *
                                                   ((year % 100 + 2) % (year % 100 + 1)) +
                                                   (1 - (year % 400 + 2) % (year % 400 + 1))) / month)
                + Math.Truncate((double) 1 / month) - Math.Truncate((double) ((1 - (year % 4 + 2) % (year % 4 + 1)) *
                                                                              ((year % 100 + 2) % (year % 100 + 1)) +
                                                                              (1 - (year % 400 + 2) % (year % 400 + 1))
                                                                    ) / month))
                ok = true;
            return ok;
        }

        //Создание двумерного массива для ДатыГрид
        public static string[,] EventsToMatrix(DateTime date, int importance, out string[] names) {
            date = date.AddHours(-date.Hour);
            date = date.AddSeconds(-date.Second);
            date = date.AddMinutes(-date.Minute);
            date = date.AddMilliseconds(-date.Millisecond);


            var matrix = new string[24, 1];
            names = new string[1];
            names[0] = "Время";
            for (var i = 0; i < 24; i++) matrix[i, 0] = i + ":" + "00";


            for (var repeat = 0; repeat < 4; repeat++) {
                Dictionaries Dictionaries = WorkWithFiles_Control.ReturnMeanings(repeat);

                switch (repeat) {
                    case 0:
                        NeverRepeat(date, importance, ref names, Dictionaries, ref matrix);
                        break;
                    case 1:
                        OneDayRepeat(date, importance, ref names, Dictionaries, ref matrix);
                        break;
                    case 2:
                        WeekRepeat(date, importance, ref names, Dictionaries, ref matrix);
                        break;
                    case 3:
                        YearRepeat(date, importance, ref names, Dictionaries, ref matrix);
                        break;
                }
            }

            return matrix;
        }

        //Функция получения матрицы для формы
        private static string[,] ResizeColumns(string[,] matrix) {
            var tmp = new string[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (var i = 0; i < matrix.GetLength(0); i++) {
                for (var j = 0; j < matrix.GetLength(1); j++) tmp[i, j] = matrix[i, j];
            }

            return tmp;
        }


        //Получить ключ по значению
        public static OverrideDateTime GetKeyByValue(NameArr value, Dictionary<OverrideDateTime, NameArr> dictionary) {
            foreach (var recordOfDictionary in dictionary)
                if (recordOfDictionary.Value.Equals(value))
                    return recordOfDictionary.Key;
            return new OverrideDateTime(DateTime.Parse("1.1.1"));
        }

        //Содержит ли матрица данную строку
        private static bool ContainsMatrix(string[,] matrix, string str) {
            var ok = false;
            foreach (var tmp in matrix)
                if (tmp != null)
                    if (tmp.CompareTo(str) == 0) {
                        ok = true;
                        break;
                    }

            return ok;
        }

        //Ежеразовые события
        private static void NeverRepeat(
            DateTime      date, int importance, ref string[] names, Dictionaries Dictionaries,
            ref string[,] matrix) {
            for (var i = 0; i < 24; i++) {
                if (i != 0)
                    date = date.AddHours(1);
                for (var min = 0; min < 60; min++) {
                    if (min != 0)
                        date = date.AddMinutes(1);

                    var dateTime = new OverrideDateTime(date);
                    if (Dictionaries.ReturnDictionary_Events_From[importance].ContainsKey(dateTime)) {
                        for (var j = 0;
                             j < Dictionaries.ReturnDictionary_Events_From[importance][dateTime].Return_size;
                             j++) {
                            matrix = ResizeColumns(matrix);

                            Array.Resize(ref names, names.Length + 1);

                            names[names.Length - 1] =
                                Dictionaries.ReturnDictionary_Events_From[importance][dateTime][j].EventName;
                            matrix[i, matrix.GetLength(1) - 1] +=
                                "Начало: " + date.ToShortTimeString() + " " +
                                Dictionaries.ReturnDictionary_Events_From[importance][dateTime][j].EventName;


                            var endOver = GetKeyByValue(Dictionaries.ReturnDictionary_Events_From[importance][dateTime],
                                                        Dictionaries.ReturnDictionary_Events_To[importance]);
                            var end = endOver.GetDateTime;


                            if (end.Date == date.Date)
                                matrix[end.Hour, matrix.GetLength(1) - 1] +=
                                    "Конец: " + end.ToShortTimeString() + " " +
                                    Dictionaries.ReturnDictionary_Events_To[importance][endOver][j].EventName;
                        }
                    }
                    else {
                        if (Dictionaries.ReturnDictionary_Events_To[importance].ContainsKey(dateTime))
                            for (var j = 0;
                                 j < Dictionaries.ReturnDictionary_Events_To[importance][dateTime].Return_size;
                                 j++)
                                if (!ContainsMatrix(matrix,
                                                    "Конец: " + date.ToShortTimeString() + " " +
                                                    Dictionaries.ReturnDictionary_Events_To[importance][dateTime][j].
                                                        EventName)) {
                                    Array.Resize(ref names, names.Length + 1);


                                    names[names.Length - 1] =
                                        Dictionaries.ReturnDictionary_Events_To[importance][dateTime][j].EventName;

                                    matrix = ResizeColumns(matrix);
                                    matrix[i, matrix.GetLength(1) - 1] +=
                                        "Конец: " + date.ToShortTimeString() + " " +
                                        Dictionaries.ReturnDictionary_Events_To[importance][dateTime][j].EventName;
                                }
                    }

                    if (date.Minute == 59) date = date.AddMinutes(-59);
                }
            }
        }


        //вывод матрицы до (события)
        private static void OneDayRepeatFrom(
            DateTime      date, int importance, ref string[] names, Dictionaries Dictionaries,
            ref string[,] matrix) {
            var dateTime = new OverrideDateTime(date);
            var endOver = GetKeyByValue(Dictionaries.ReturnDictionary_Events_From[importance][dateTime],
                                        Dictionaries.ReturnDictionary_Events_To[importance]);
            var end = endOver.GetDateTime;


            for (var j = 0; j < Dictionaries.ReturnDictionary_Events_From[importance][dateTime].Return_size; j++) {
                matrix = ResizeColumns(matrix);

                Array.Resize(ref names, names.Length + 1);

                names[names.Length - 1] = Dictionaries.ReturnDictionary_Events_From[importance][dateTime][j].EventName;

                matrix[date.Hour, matrix.GetLength(1) - 1] += "Начало: " + date.ToShortTimeString() + " " +
                                                              Dictionaries.ReturnDictionary_Events_From[importance]
                                                                  [dateTime][j].EventName;

                if (end.Date == date.Date)
                    matrix[end.Hour, matrix.GetLength(1) - 1] += "Конец: " + end.ToShortTimeString() + " " +
                                                                 Dictionaries.ReturnDictionary_Events_To[importance]
                                                                     [endOver][j].EventName;
            }
        }

        //вывод матрицы от (события)
        private static void OneDayRepeatTo(
            DateTime      date, int importance, ref string[] names, Dictionaries Dictionaries,
            ref string[,] matrix) {
            var dateTime = new OverrideDateTime(date);


            if (Dictionaries.ReturnDictionary_Events_To[importance].ContainsKey(dateTime))
                for (var j = 0; j < Dictionaries.ReturnDictionary_Events_To[importance][dateTime].Return_size; j++) {
                    var element = "Конец: " + date.ToShortTimeString() + " " +
                                  Dictionaries.ReturnDictionary_Events_To[importance][dateTime][j].EventName;


                    if (!ContainsMatrix(matrix, element)) {
                        matrix = ResizeColumns(matrix);

                        Array.Resize(ref names, names.Length + 1);

                        names[names.Length - 1] =
                            Dictionaries.ReturnDictionary_Events_To[importance][dateTime][j].EventName;

                        matrix[date.Hour, matrix.GetLength(1) - 1] +=
                            "Конец: " + date.ToShortTimeString() + " " +
                            Dictionaries.ReturnDictionary_Events_To[importance][dateTime][j].EventName;
                    }
                }
        }

        //Каждодневные события
        private static void OneDayRepeat(
            DateTime      date, int importance, ref string[] names, Dictionaries Dictionaries,
            ref string[,] matrix) {
            if (Dictionaries.ReturnDictionary_Events_From[0].Any() ||
                Dictionaries.ReturnDictionary_Events_From[1].Any() ||
                Dictionaries.ReturnDictionary_Events_From[2].Any()) {
                foreach (var DateTime in Dictionaries.ReturnDictionary_Events_From[importance].Keys) {
                    var dateTime = DateTime.GetDateTime;

                    if (new OverrideDateTime(dateTime.Date) <= new OverrideDateTime(date))
                        OneDayRepeatFrom(dateTime, importance, ref names, Dictionaries, ref matrix);
                }

                foreach (var DateTime in Dictionaries.ReturnDictionary_Events_To[importance].Keys) {
                    var dateTime = DateTime.GetDateTime;

                    var tmp = dateTime;
                    tmp = tmp.AddHours(-tmp.Hour);
                    tmp = tmp.AddSeconds(-tmp.Second);
                    tmp = tmp.AddMinutes(-tmp.Minute);
                    tmp = tmp.AddMilliseconds(-tmp.Millisecond);

                    if (new OverrideDateTime(tmp) <= new OverrideDateTime(date))
                        OneDayRepeatTo(dateTime, importance, ref names, Dictionaries, ref matrix);
                }
            }
        }

        //Каждонедельные события
        private static void WeekRepeat(
            DateTime      date, int importance, ref string[] names, Dictionaries Dictionaries,
            ref string[,] matrix) {
            if (Dictionaries.ReturnDictionary_Events_From[0].Any() ||
                Dictionaries.ReturnDictionary_Events_From[1].Any() ||
                Dictionaries.ReturnDictionary_Events_From[2].Any()) {
                foreach (var DateTime in Dictionaries.ReturnDictionary_Events_From[importance].Keys) {
                    var dateTime = DateTime.GetDateTime;
                    var tmp = dateTime;
                    tmp = tmp.AddHours(-tmp.Hour);
                    tmp = tmp.AddSeconds(-tmp.Second);
                    tmp = tmp.AddMinutes(-tmp.Minute);
                    tmp = tmp.AddMilliseconds(-tmp.Millisecond);

                    var span = date - tmp;
                    var timeSpan_Fix =
                        GetKeyByValue(Dictionaries.ReturnDictionary_Events_From[importance][DateTime],
                                      Dictionaries.ReturnDictionary_Events_To[importance]).GetDateTime.Date - tmp;


                    var days = span.Days + 1;
                    if (days >= 0 && days % 7 <= timeSpan_Fix.Days + 1)
                        OneDayRepeatFrom(dateTime, importance, ref names, Dictionaries, ref matrix);
                }

                foreach (var DateTime in Dictionaries.ReturnDictionary_Events_To[importance].Keys) {
                    var dateTime = DateTime.GetDateTime;
                    var tmp = dateTime;
                    tmp = tmp.AddHours(-tmp.Hour);
                    tmp = tmp.AddSeconds(-tmp.Second);
                    tmp = tmp.AddMinutes(-tmp.Minute);
                    tmp = tmp.AddMilliseconds(-tmp.Millisecond);

                    var span = date - tmp;
                    var check = GetKeyByValue(Dictionaries.ReturnDictionary_Events_To[importance][DateTime],
                                              Dictionaries.ReturnDictionary_Events_From[importance]).GetDateTime;
                    check = check.AddHours(-check.Hour);
                    check = check.AddMinutes(-check.Minute);
                    var timeSpan_Fix = tmp - check;


                    var days = span.Days + 1;
                    if (days >= 0 && days % 7 <= timeSpan_Fix.Days + 1)
                        OneDayRepeatTo(dateTime, importance, ref names, Dictionaries, ref matrix);
                }
            }
        }

        //Годовые посторяющиеся события
        private static void YearRepeat(
            DateTime      date, int importance, ref string[] names, Dictionaries Dictionaries,
            ref string[,] matrix) {
            if (Dictionaries.ReturnDictionary_Events_From[0].Any() ||
                Dictionaries.ReturnDictionary_Events_From[1].Any() ||
                Dictionaries.ReturnDictionary_Events_From[2].Any()) {
                foreach (var Date_Time in Dictionaries.ReturnDictionary_Events_From[importance].Keys) {
                    var dateTime = Date_Time.GetDateTime;
                    var tmp = dateTime;
                    tmp = tmp.AddHours(-tmp.Hour);
                    tmp = tmp.AddSeconds(-tmp.Second);
                    tmp = tmp.AddMinutes(-tmp.Minute);
                    tmp = tmp.AddMilliseconds(-tmp.Millisecond);

                    var span = date - tmp;

                    var days = span.Days;

                    if (DateTime.IsLeapYear(date.Year - 1)) {
                        if (days >= 0 && days % 366 == 0)
                            OneDayRepeatFrom(dateTime, importance, ref names, Dictionaries, ref matrix);
                    }
                    else {
                        if (days >= 0 && days % 365 == 0)
                            OneDayRepeatFrom(dateTime, importance, ref names, Dictionaries, ref matrix);
                    }
                }

                foreach (var Date_Time in Dictionaries.ReturnDictionary_Events_To[importance].Keys) {
                    var dateTime = Date_Time.GetDateTime;
                    var tmp = dateTime;
                    tmp = tmp.AddHours(-tmp.Hour);
                    tmp = tmp.AddSeconds(-tmp.Second);
                    tmp = tmp.AddMinutes(-tmp.Minute);
                    tmp = tmp.AddMilliseconds(-tmp.Millisecond);

                    var span = date - tmp;
                    var check = GetKeyByValue(Dictionaries.ReturnDictionary_Events_To[importance][Date_Time],
                                              Dictionaries.ReturnDictionary_Events_From[importance]).GetDateTime;
                    check = check.AddHours(-check.Hour);
                    check = check.AddMinutes(-check.Minute);
                    var timeSpan = tmp - check;


                    var days = span.Days;

                    if (DateTime.IsLeapYear(date.Year - 1)) {
                        if (days >= 0 && days % 366 == 0)
                            if (timeSpan.Days != 366)
                                OneDayRepeatTo(dateTime, importance, ref names, Dictionaries, ref matrix);
                            else
                                OneDayRepeatTo(dateTime, importance, ref names, Dictionaries, ref matrix);
                    }
                    else {
                        if (days >= 0 && days % 365 == 0)
                            if (timeSpan.Days != 365)
                                OneDayRepeatTo(dateTime, importance, ref names, Dictionaries, ref matrix);
                            else
                                OneDayRepeatTo(dateTime, importance, ref names, Dictionaries, ref matrix);
                    }
                }
            }
        }

        public static void FixDeleteEvent_Start(
            string[]     tmp, ref string str, ref DateTime from, out TimeSpan timeSpan, out int i,
            out DateTime to,
            out string   description, int importance, out bool AllDay) {
            str = "";
            for (var j = 2; j < tmp.Length; j++) {
                str = string.Concat(str, tmp[j]);
                if (j + 1 != tmp.Length) str = string.Concat(str, " ");
            }

            var dictionaries = new Dictionaries();
            to = new DateTime();
            var tmp_i = 0;
            for (i = 0; i < 4; i++) {
                dictionaries = WorkWithFiles_Control.ReturnMeanings(i);
                try {
                    if (i == 0) {
                        to =
                            GetKeyByValue(dictionaries.ReturnDictionary_Events_From[importance][new OverrideDateTime(from)],
                                          WorkWithFiles_Control.ReturnMeanings(i).
                                              ReturnDictionary_Events_To[importance]).GetDateTime;
                        if (to.Year != 1) break;
                        tmp_i = i;
                    }
                    else {
                        if (from == new DateTime(1, 1, 1) || to == new DateTime(1, 1, 1)) {
                            from = WorkWithFiles_Control.FindElementFrom(str, i, importance).GetDateTime;
                            to = WorkWithFiles_Control.FindElementTo(str, i, importance).GetDateTime;
                            tmp_i = i;
                        }
                    }
                }
                catch {
                }
            }

            i = tmp_i;
            dictionaries = WorkWithFiles_Control.ReturnMeanings(i);

            var nameArr = dictionaries.ReturnDictionary_Events_From[importance][new OverrideDateTime(from)];
            description = null;

            for (var j = 0; j < nameArr.Return_size; j++)
                if (nameArr[j].EventName.CompareTo(str) == 0)
                    description = nameArr[j].EventDescription;

            timeSpan = to - from;
            AllDay = false;
            if (timeSpan.Days == 0 && timeSpan.Hours == 23 && timeSpan.Minutes == 59) AllDay = true;

            var alarmDate = new DateTime();
            Dictionary<OverrideDateTime, NameArr>[] alarm = WorkWithFiles_Control.Alarm;
            foreach (var key in alarm[i].Keys)
                if (nameArr.Equals(alarm[i][key])) {
                    alarmDate = key.GetDateTime;
                    break;
                }

            timeSpan = from - alarmDate;


            WorkWithFiles_Control.DeleteFromClass(i, importance, new Name(str, description), from, to);
        }

        public static void FixDeleteEvent_End(
            string[]     tmp, ref string str, ref DateTime to, out TimeSpan timeSpan, out int i,
            out DateTime from,
            out string   description, int importance, out bool AllDay) {
            str = "";
            for (var j = 2; j < tmp.Length; j++) {
                str = string.Concat(str, tmp[j]);
                if (j + 1 != tmp.Length) str = string.Concat(str, " ");
            }

            var tmp_i = 0;

            var dictionaries = new Dictionaries();
            from = new DateTime();
            for (i = 0; i < 4; i++) {
                dictionaries = WorkWithFiles_Control.ReturnMeanings(i);
                try {
                    if (i == 0) {
                        from =
                            GetKeyByValue(dictionaries.ReturnDictionary_Events_To[importance][new OverrideDateTime(to)],
                                          WorkWithFiles_Control.ReturnMeanings(i).
                                              ReturnDictionary_Events_From[importance]).GetDateTime;
                        if (from.Year != 1) break;
                        tmp_i = i;
                    }
                    else {
                        if (from == new DateTime(1, 1, 1) || to == new DateTime(1, 1, 1)) {
                            from = WorkWithFiles_Control.FindElementFrom(str, i, importance).GetDateTime;
                            to = WorkWithFiles_Control.FindElementTo(str, i, importance).GetDateTime;
                            tmp_i = i;
                        }
                    }
                }
                catch {
                }
            }

            i = tmp_i;
            dictionaries = WorkWithFiles_Control.ReturnMeanings(i);

            var nameArr = dictionaries.ReturnDictionary_Events_To[importance][new OverrideDateTime(to)];
            description = null;

            for (var j = 0; j < nameArr.Return_size; j++)
                if (nameArr[j].EventName.CompareTo(str) == 0)
                    description = nameArr[j].EventDescription;

            timeSpan = to - from;
            AllDay = false;
            if (timeSpan.Days == 0 && timeSpan.Hours == 23 && timeSpan.Minutes == 59) AllDay = true;

            var alarmDate = new DateTime();
            Dictionary<OverrideDateTime, NameArr>[] alarm = WorkWithFiles_Control.Alarm;
            foreach (var key in alarm[i].Keys)
                if (nameArr.Equals(alarm[i][key])) {
                    alarmDate = key.GetDateTime;
                    break;
                }

            timeSpan = from - alarmDate;


            WorkWithFiles_Control.DeleteFromClass(i - 1, importance, new Name(str, description), from, to);
        }
    }
}