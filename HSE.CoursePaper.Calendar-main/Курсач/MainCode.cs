using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Курсач.Properties;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Курсач
{
    //При наличие времени переписать под свой класс Персона
    internal static class info
    {
        public static string name { get; set; } = Settings.Default.Name;

        public static string se_name { get; set; } = Settings.Default.Se_name;

        public static string father_name { get; set; } = Settings.Default.Father_name;

        public static string city { get; set; } = Settings.Default.City;

        public static string weight { get; set; } = Settings.Default.Weight;

        public static DateTime age { get; set; } = Settings.Default.Age;

        public static string growth { get; set; } = Settings.Default.Growth;

        public static string[] name_sport { get; set; }

        public static string recomendation { get; set; } = Settings.Default.Recomendation;

        public static string sex { get; set; } = Settings.Default.Sex;
    }

    //Класс для передачи информации (определения жирных дат)
    internal static class Send
    {
        public static DateTime[] Date { get; set; }

        public static int repeat { get; set; }

        public static Tasks GetTasks { get; set; }
    }

    //Настройки
    [Serializable]
    public class ClassOptions
    {
        private static int count;

        public ClassOptions() {
            FirstDay = 0;
            NotAlarm = false;
            NumberWeeks = false;
        }

        public ClassOptions(int firstday, bool notalarm, bool numberweeks) {
            FirstDay = firstday;
            NotAlarm = notalarm;
            NumberWeeks = numberweeks;
            count = 1;
        }

        public int  FirstDay    { get; set; }
        public bool NotAlarm    { get; set; }
        public bool NumberWeeks { get; set; }

        public int GetCount => count;

        public void Load(ref ClassOptions classOptions) {
            try {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream("config.bin", FileMode.Open)) {
                    classOptions = (ClassOptions) formatter.Deserialize(fs);
                }
            }
            catch {
                classOptions = new ClassOptions();
            }
        }

        public void Save() {
            File.WriteAllText("config.bin", string.Empty);
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("config.bin", FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, this);
            }
        }
    }

    //Задачи
    [Serializable]
    public class Tasks
    {
        private string[] Conditions;
        private int[]    Indexes;
        private string[] Name;

        //Конструктор
        public Tasks() {
            Name = null;
            Conditions = null;
        }

        //Получаем индексы отмеченных задач
        public int[] GetIndexes => Indexes;

        //Получение имен
        public string[] GetName => Name;

        //Получение условий
        public string[] GetConditions => Conditions;

        public void Delete(string name, string description, bool Checked, int index) {
            var i = 0;
            for (i = 0; i < Name.Length - 1; i++)
                if (Name[i].CompareTo(name) == 0) {
                    var tmp = Name[i + 1];
                    Name[i + 1] = Name[i];
                    Name[i] = tmp;

                    tmp = Conditions[i + 1];
                    Conditions[i       + 1] = Conditions[i];
                    Conditions[i] = tmp;
                }

            if (Checked) {
                for (i = 0; i < Indexes.Length - 1; i++)
                    if (Indexes[i] == index) {
                        var tmp = Indexes[i + 1];
                        Indexes[i + 1] = Indexes[i];
                        Indexes[i] = tmp;
                    }

                Array.Resize(ref Indexes, Indexes.Length - 1);
            }

            Array.Resize(ref Name, Name.Length             - 1);
            Array.Resize(ref Conditions, Conditions.Length - 1);
        }

        //Добавляем задачу
        public void AddTask(string name, string conditions) {
            if (Name == null) {
                Name = new string[1];
                Conditions = new string[1];
            }
            else {
                Array.Resize(ref Name, Name.Length             + 1);
                Array.Resize(ref Conditions, Conditions.Length + 1);
            }

            Name[Name.Length             - 1] = name;
            Conditions[Conditions.Length - 1] = conditions;
        }

        //Отмечаем индекс
        public void CheckElement(int index) {
            if (Indexes == null)
                Indexes = new int[1];
            else
                Array.Resize(ref Indexes, Indexes.Length + 1);

            Indexes[Indexes.Length - 1] = index;
        }

        //Убираем индекс
        public void OutCheckIndex(int index_delete) {
            var index = 0;
            for (var i = 0; i < Indexes.Length; i++)
                if (Indexes[i] == index_delete) {
                    index = i;
                    break;
                }

            for (var i = index; i < Indexes.Length - 1; i++) Indexes[i] = Indexes[i + 1];

            Array.Resize(ref Indexes, Indexes.Length - 1);
        }

        //Сохраняем задачи
        public void SaveTasksToXML() {
            File.WriteAllText("Resources\\Tasks.bin", string.Empty);
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("Resources\\Tasks.bin", FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, this);
            }
        }

        //Загружаем задачи
        public static void LoadTasksFromXML(ref Tasks tasks) {
            try {
                var formatter = new BinaryFormatter();

                using (var fs = new FileStream("Resources\\Tasks.bin", FileMode.Open)) {
                    tasks = (Tasks) formatter.Deserialize(fs);
                }
            }
            catch {
                tasks = new Tasks();
            }
        }

        //Пустой ли класс
        public bool Empty() {
            if (Name == null)
                return true;
            return false;
        }
    }

    [Serializable]
    //Класс для ключей в словарь
    public struct OverrideDateTime
    {
        //Получаем ключ
        public DateTime GetDateTime { get; }

        //Конструктор
        public OverrideDateTime(DateTime DT) {
            //DateTime = DT;
            GetDateTime = new DateTime(DT.Year, DT.Month, DT.Day, DT.Hour, DT.Minute, 0);
        }

        //Перегрузка метода равно
        public override bool Equals(object obj) {
            var @override = (OverrideDateTime) obj;

            var tmp = @override.GetDateTime;

            return tmp.Day    == GetDateTime.Day    && tmp.Month == GetDateTime.Month && tmp.Year == GetDateTime.Year &&
                   tmp.Minute == GetDateTime.Minute && tmp.Hour  == GetDateTime.Hour;
        }

        //Перегрузка метора получение хэша
        public override int GetHashCode() {
            return GetDateTime.Day * GetDateTime.Month * GetDateTime.Minute * GetDateTime.Hour / GetDateTime.Year;
        }

        //Перегрузка оператора
        public static bool operator <=(OverrideDateTime dateTime1, OverrideDateTime dateTime2) {
            var date1 = dateTime1.GetDateTime;
            var date2 = dateTime2.GetDateTime;

            if (date1.Date <= date2.Date)
                if (date1.Hour <= date2.Hour)
                    if (date1.Minute <= date2.Minute)
                        return true;
                    else
                        return false;
                else
                    return false;
            return false;
        }

        //Перегрузка оператора
        public static bool operator >=(OverrideDateTime dateTime1, OverrideDateTime dateTime2) {
            var date1 = dateTime1.GetDateTime;
            var date2 = dateTime2.GetDateTime;

            if (date1.Date >= date2.Date)
                if (date1.Hour >= date2.Hour)
                    if (date1.Minute >= date2.Minute)
                        return true;
                    else
                        return false;
                else
                    return false;
            return false;
        }
    }

    [Serializable]
    //Класс событий(запись и чтение в файл)
    public class Event
    {
        private static NameArr[]                               nameArr;
        private static Dictionary<int, Dictionaries>           events;
        private static Dictionary<OverrideDateTime, NameArr>[] alarm;

        //Конструктор
        public Event() {
            nameArr = null;
            events = new Dictionary<int, Dictionaries>();
            for (var i = 0; i < 4; i++) {
                var dictionaries = new Dictionaries();
                events.Add(i, dictionaries);
            }

            alarm = new Dictionary<OverrideDateTime, NameArr>[4];
            for (var i = 0; i < 4; i++) alarm[i] = new Dictionary<OverrideDateTime, NameArr>();
        }

        //Возвращаем словарь ивентов(от)
        public Dictionary<int, Dictionaries> ReturnDictionaries {
            get => events;
            set => events = value;
        }

        //Возвращаем словарь уведомления
        public Dictionary<OverrideDateTime, NameArr>[] ReturnDictionary_Alarm {
            get => alarm;
            set => alarm = value;
        }

        //Пуст ли словарь
        public bool Empty() {
            if (events.Count != 0)
                return false;
            return true;
        }

        //пусть ли словарь
        public bool EmptyAlarm() {
            var ok = true;
            for (var i = 0; i < 4; i++)
                if (alarm[i].Any()) {
                    ok = false;
                    break;
                }

            return ok;
        }

        //Добавление событе к списку (добавить сортировку СЛОВАРЕЙ)
        public void AddEvent(Date Date, Date Alarm, Name Name, int importance, int repeat) {
            var tmp = new NameArr();
            var Dictionaries = events[repeat];

            if (Dictionaries.ReturnDictionary_Events_From[importance].Count != 0) {
                if (Dictionaries.ReturnDictionary_Events_From[importance].
                    ContainsKey(new OverrideDateTime(Date.Return_From_Real))) {
                    tmp = Dictionaries.ReturnDictionary_Events_From[importance]
                        [new OverrideDateTime(Date.Return_From_Real)];

                    Dictionaries.ReturnDictionary_Events_From[importance].
                        Remove(new OverrideDateTime(Date.Return_From_Real));
                    Dictionaries.ReturnDictionary_Events_To[importance].
                        Remove(new OverrideDateTime(Date.Return_To_Real));

                    tmp = tmp.DeterminationIndex(nameArr);
                }
                else {
                    Array.Resize(ref nameArr, nameArr.Length + 1);
                    nameArr[nameArr.Length       - 1] = new NameArr();
                    tmp = nameArr[nameArr.Length - 1];
                }
            }
            else {
                nameArr = new NameArr[1];
                nameArr[nameArr.Length - 1] = tmp;
            }

            tmp.AddName(Name);
            Dictionaries.AddDictionaryElement(tmp, importance, Date);

            if (!Alarm.Equals(Date))
                try {
                    alarm[repeat].Add(new OverrideDateTime(Alarm.Return_From_Real), tmp);
                }
                catch {
                    alarm[repeat].Remove(new OverrideDateTime(Alarm.Return_From_Real));
                    alarm[repeat].Add(new OverrideDateTime(Alarm.Return_From_Real), tmp);
                }
        }

    #region Save

        //Добавляем даты и имена в файл
        public void AddNamesDatesToXML() {
            try {
                File.WriteAllText("Resources\\Names.bin", string.Empty);

                var formatter = new BinaryFormatter();
                using (var fs = new FileStream("Resources\\Names.bin", FileMode.OpenOrCreate)) {
                    formatter.Serialize(fs, nameArr);
                }
            }
            catch {
            }
        }

        //Сохраняем события в файл
        public void AddEventsToXML() {
            File.WriteAllText("Resources\\EventsTo.bin", string.Empty);
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("Resources\\EventsTo.bin", FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, events);
            }
        }

        //Сохраняем уведомления в файл
        public void AddAlarmToXML() {
            File.WriteAllText("Resources\\AlarmForEvents.bin", string.Empty);
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("Resources\\AlarmForEvents.bin", FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, alarm);
            }
        }

    #endregion

    #region Load

        //Считывание файлов при запуске программы
        public void LoadEvents() {
            var formatterFind = new BinaryFormatter();
            var formatter = new BinaryFormatter();

            //Загрузка ивентов
            using (var fs = new FileStream("Resources\\EventsTo.bin", FileMode.Open)) {
                events = (Dictionary<int, Dictionaries>) formatter.Deserialize(fs);
            }

            try {
                //Загрузка названий ивентов
                using (var fs = new FileStream("Resources\\Names.bin", FileMode.Open)) {
                    nameArr = (NameArr[]) formatterFind.Deserialize(fs);
                }
            }
            catch {
            }
        }

        //Загрузка файла задач
        public void LoadAlarms() {
            var formatter = new BinaryFormatter();
            //Загрузка уведомлений  
            using (var fs = new FileStream("Resources\\AlarmForEvents.bin", FileMode.Open)) {
                alarm = (Dictionary<OverrideDateTime, NameArr>[]) formatter.Deserialize(fs);
            }
        }

    #endregion
    }

    [Serializable]
    //Класс для словарей
    public class Dictionaries
    {
        private Dictionary<OverrideDateTime, NameArr>[] events_from;
        private Dictionary<OverrideDateTime, NameArr>[] events_to;

        //Конструктор
        public Dictionaries() {
            events_from = new Dictionary<OverrideDateTime, NameArr>[3];
            events_to = new Dictionary<OverrideDateTime, NameArr>[3];
            for (var i = 0; i < 3; i++) {
                events_from[i] = new Dictionary<OverrideDateTime, NameArr>();
                events_to[i] = new Dictionary<OverrideDateTime, NameArr>();
            }
        }

        //Возвращаем словарь ивентов(от)
        public Dictionary<OverrideDateTime, NameArr>[] ReturnDictionary_Events_From {
            get => events_from;
            set => events_from = value;
        }

        //Возвращаем словарь ивентов(до)
        public Dictionary<OverrideDateTime, NameArr>[] ReturnDictionary_Events_To {
            get => events_to;
            set => events_to = value;
        }

        //Добавить к словарям значение
        public void AddDictionaryElement(NameArr tmp, int importance, Date date) {
            events_from[importance].Add(new OverrideDateTime(date.Return_From_Real), tmp);
            events_to[importance].Add(new OverrideDateTime(date.Return_To_Real), tmp);
        }
    }

    [Serializable]
    //Класс массив для Name
    public class NameArr
    {
        private Name[] names;

        //Конструктор без параметра
        public NameArr() {
            names = null;
            Return_size = 0;
        }

        public int Return_size { get; private set; }

        private Name[] GetName => names;

        public Name this[int index] {
            get {
                if (index >= 0 && index < Return_size)
                    return names[index];
                return new Name("", "");
            }
            set {
                if (index >= 0 && index < Return_size)
                    names[index] = value;
            }
        }

        //Добавить имя к классу
        public void AddName(Name Name) {
            Return_size++;
            if (names != null)
                Array.Resize(ref names, names.Length + 1);
            else
                names = new Name[1];
            names[names.Length - 1] = Name;
        }

        public void DeleteName(Name name) {
            for (var i = 0; i < Return_size - 1; i++)
                if (names[i].Equals(name)) {
                    var tmp = names[i + 1];
                    names[i + 1] = names[i];
                    names[i] = tmp;
                }

            Return_size--;
            Array.Resize(ref names, names.Length - 1);
        }

        //поиск индекса по значению
        public NameArr DeterminationIndex(NameArr[] nameArr) {
            for (var i = 0; i < nameArr.Length; i++)
                if (Sravn(nameArr[i].GetName))
                    return nameArr[i];
            return null;
        }

        private bool Sravn(Name[] names_compare) {
            if (names_compare.Length != names.Length) return false;

            for (var i = 0; i < names.Length; i++)
                if (!names[i].Equals(names_compare[i]))
                    return false;
            return true;
        }

        public bool IsInArray(string name) {
            var ok = false;
            foreach (var eventName in names)
                if (eventName.EventName.CompareTo(name) == 0) {
                    ok = true;
                    break;
                }

            return ok;
        }

        public static NameArr operator +(NameArr nameArr1, NameArr nameArr2) {
            for (var i = 0; i < nameArr2.Return_size; i++) nameArr1.AddName(nameArr2[i]);
            return nameArr1;
        }

        public override bool Equals(object obj) {
            var ok = false;
            var arr = (NameArr) obj;
            if (arr.Return_size == Return_size) {
                ok = true;
                for (var i = 0; i < arr.Return_size; i++)
                    if (!arr[i].Equals(this[i])) {
                        ok = false;
                        break;
                    }
            }

            return ok;
        }
    }

    [Serializable]
    //Класс для даты/уведомления
    public class Date
    {
        private DateTime eventFrom_real;
        private DateTime eventTo_real;

        //Конструктор для уведомлений
        public Date(DateTime dateAlarm) {
            //eventFrom_virtual = eventTo_virtual = dateAlarm;
            eventFrom_real = eventTo_real = dateAlarm;
        }

        //Конструктор для событий
        public Date(DateTime EF, DateTime ET) {
            EF = EF.AddSeconds(-EF.Second);
            EF = EF.AddMilliseconds(-EF.Millisecond);

            ET = ET.AddSeconds(-ET.Second);
            ET = ET.AddMilliseconds(-ET.Millisecond);

            eventFrom_real = EF;
            eventTo_real = ET;
            //eventFrom_virtual = EF;
            //eventTo_virtual = ET;
        }

        public DateTime Return_From_Real {
            get => eventFrom_real;
            set => eventFrom_real = value;
        }

        public DateTime Return_To_Real {
            get => eventTo_real;
            set => eventTo_real = value;
        }
    }

    [Serializable]
    //Класс для имени файла
    public class Name
    {
        private string eventDescription;
        private string eventName;

        public Name() {
            eventName = null;
            eventDescription = null;
        }

        //Конструктор
        public Name(string name, string description) {
            eventName = name;
            eventDescription = description;
        }

        public string EventName {
            get => eventName;
            set => eventName = value;
        }

        public string EventDescription {
            get => eventDescription;
            set => eventDescription = value;
        }

        public override bool Equals(object obj) {
            var name = (Name) obj;
            return eventName.CompareTo(name.EventName) == 0 && eventDescription.CompareTo(name.EventDescription) == 0;
        }
    }

    [Serializable]
    internal class OpenSource_Sport
    {
        public OpenSource_Sport() {
            keyValues = new Dictionary<string, SportArray>();
        }

        public Dictionary<string, SportArray> keyValues { get; set; }

        public void LoadXml(string xml) {
            var html = File.ReadAllText(xml);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var nodes = htmlDoc.DocumentNode.SelectNodes("descendant::item");

            var newSport = new Sport();
            var Type = "";
            foreach (var node in nodes) {
                var newHtmlDoc = new HtmlDocument();
                newHtmlDoc.LoadHtml(node.InnerHtml);

                var n = newHtmlDoc.DocumentNode.SelectSingleNode("caption");
                if (n != null)
                    if (n.InnerText == "Наименование" || n.InnerText == "Дата проведения" ||
                        n.InnerText == "Вид мероприятия") {
                        var n11 = newHtmlDoc.DocumentNode.SelectSingleNode("dopattrval");


                        if (n.InnerText == "Наименование") {
                            newSport.Name = n11.InnerText;
                        }
                        else if (n.InnerText == "Дата проведения") {
                            newSport.Data = n11.InnerText;
                        }
                        else if (n.InnerText == "Вид мероприятия") {
                            Type = n11.InnerText;
                            if (!keyValues.ContainsKey(Type)) {
                                var sportArray = new SportArray();
                                sportArray.Add(newSport);
                                keyValues.Add(Type, sportArray);
                            }
                            else {
                                var sportArray = keyValues[Type];
                                keyValues.Remove(Type);
                                sportArray.Add(newSport);
                                keyValues.Add(Type, sportArray);
                            }


                            Type = "";
                            newSport = new Sport();
                        }
                    }
            }
        }
    }

    [Serializable]
    internal class SportArray : IEnumerable
    {
        public  Sport[] array;
        private int     index = -1;

        public SportArray() {
            array = new Sport[0];
        }

        // Реализуем интерфейс IEnumerable
        public IEnumerator GetEnumerator() {
            return new SportEnumerator(this);
        }

        public void Add(Sport sport) {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = sport;
        }
    }

    [Serializable]
    internal class SportEnumerator : IEnumerator
    {
        private          int        position = -1;
        private readonly SportArray t;

        public SportEnumerator(SportArray t) {
            this.t = t;
        }

        // The IEnumerator interface requires a MoveNext method.
        public bool MoveNext() {
            if (position < t.array.Length - 1) {
                position++;
                return true;
            }

            return false;
        }

        // The IEnumerator interface requires a Reset method.
        public void Reset() {
            position = -1;
        }

        // The IEnumerator interface requires a Current method.
        public object Current => t.array[position];
    }

    [Serializable]
    internal class Sport
    {
        public string Data;
        public string Name;
    }


    internal static class MainCode
    {
        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ГлавнаяФорма());
        }
    }
}