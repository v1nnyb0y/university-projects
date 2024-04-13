using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lab._11;
using ReadLib;

namespace Lab._13
{
    public class TestCollection
    {
        private readonly Dictionary<Person, IPerson> _dictionaryPerson;
        private readonly Dictionary<string, IPerson> _dictionaryString;
        private readonly List<Person> _listKeys;
        private readonly List<string> _listString;
        private readonly Random _rnd = new Random();

        public TestCollection() {
            _listKeys = new List<Person>();
            _dictionaryPerson = new Dictionary<Person, IPerson>();
            _listString = new List<string>();
            _dictionaryString = new Dictionary<string, IPerson>();
        }

        private void Continue() {
            Console.WriteLine("\n\nДля продолжения нажмите на любую клавишу...");
            Console.ReadKey(true);
        }

        #region Input

        //Ввести человека
        private IPerson Input() {
            string[] inputMenu =
                    {"Ввести студента.", "Ввести сотрудника.", "Ввести учителя.", "Назад."};
            while (true) {
                var sw = Print.Menu(0, inputMenu);
                switch (sw) {
                    case 1:
                        Console.WriteLine("Введите студента для удаления:");
                        return Student.GetSelfPerson;
                    case 2:
                        Console.WriteLine("Введите сотрудника для удаления:");
                        return Associate.GetSelfPerson;
                    case 3:
                        Console.WriteLine("Введите учителя для удаления:");
                        return Teacher.GetSelfPerson;
                    case 4:
                        return null;
                }
            }
        }

        #endregion

        #region Delete

        //Удалить человека
        private void Delete(ref int k) {
            var person = Input();
            if (_dictionaryPerson.ContainsKey(person.BasePerson)) {
                _listKeys.Remove(person.BasePerson);
                _dictionaryPerson.Remove(person.BasePerson);
                _listString.Remove(person.ToString());
                _dictionaryString.Remove(person.ToString());
                Console.WriteLine("Объект был удален из коллекции!");
                if (_listKeys.Count == 0) {
                    Console.WriteLine("После удаления объекта коллекции опустели!");
                    k = 4;
                }
            }
            else {
                throw new ExeptionClass("Заданного объекта в коллекции не было!");
            }

            Continue();
        }

        #endregion

        #region Add

        //Добавить элемент
        private void Add() {
            var person = Input();
            if (_listKeys.Contains(person.BasePerson)) {
                _listKeys.Add(person.BasePerson);
                _listString.Add(person.ToString());
                _dictionaryPerson.Add(person.BasePerson, person);
                _dictionaryString.Add(person.ToString(), person);
                Console.WriteLine("Заданный объект был добавлен в коллекции!");
            }
            else {
                throw new ExeptionClass("Заданный объект уже присутствует в колекции!");
            }

            Continue();
        }

        #endregion

        #region Output

        //Вывести элементы коллекции
        private void Output() {
            foreach (var person in _dictionaryPerson.Values) person.Show();
            Continue();
        }

        #endregion

        public void Start() {
            string[] menu =
                {
                    "Создать TestCollection.", "Удаление элементов.", "Добавление элементов.",
                    "Время поиска элемента для каждой коллекции.",
                    "Печать коллекции (элементов из которых составлены коллекции).", "Выход."
                };
            var k = 4;
            while (true) {
                var sw = Print.Menu(k, menu);
                switch (sw) {
                    case 1:
                        CreateCollection(_rnd.Next(0, 1000), ref k);
                        break;
                    case 2:
                        Delete(ref k);
                        break;
                    case 3:
                        Add();
                        break;
                    case 4:
                        Search();
                        break;
                    case 5:
                        Output();
                        break;
                    case 6:
                        return;
                }
            }
        }

        #region CreateCollection

        private void CreateElement(int type) {
            IPerson person = null;
            while (true) {
                switch (type) {
                    case 1:
                        person = Student.GetSelfPerson;
                        break;
                    case 2:
                        person = Associate.GetSelfPerson;
                        break;
                    case 3:
                        person = Teacher.GetSelfPerson;
                        break;
                }

                if (!_listKeys.Contains(person.BasePerson) &&
                    !_dictionaryPerson.ContainsKey(person.BasePerson) &&
                    !_listString.Contains(person.ToString()) &&
                    !_dictionaryString.ContainsKey(person.ToString()))
                    break;
            }

            _listKeys.Add(person.BasePerson);
            _dictionaryPerson.Add(person.BasePerson, person);
            _listString.Add(person.ToString());
            _dictionaryString.Add(person.ToString(), person);
        } // Создать элемент

        private void CreateCollection(int size, ref int k) {
            if (size == 0) throw new ExeptionClass("Ошибка! Размер коллекции не может быть равен нулю!");

            _listKeys.Clear();
            _dictionaryPerson.Clear();
            _listString.Clear();
            _dictionaryString.Clear();
            k = 0;
            for (var count = 0; count < size; count++)
                CreateElement(_rnd.Next(1, 4));
            Continue();
        } // Создать коллекцию

        #endregion

        #region TimeOfSearch

        //Поиск в коллекции list<Person>
        private void SearchInListPerson(IPerson element, string str, ref Stopwatch sw) {
            sw.Start();
            Console.WriteLine(str);
            if (_listKeys.Contains(element.BasePerson)) {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
            else {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он не содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
        }

        //Поиск в коллекции list<string>
        private void SearchInListString(IPerson element, string str, ref Stopwatch sw) {
            sw.Start();
            Console.WriteLine(str);
            if (_listString.Contains(element.ToString())) {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
            else {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он не содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
        }

        //Поиск в коллекции dictionary<Person,IPerson>(по ключам)
        private void SearchInDictionaryPersonKey(IPerson element, string str, ref Stopwatch sw) {
            sw.Start();
            Console.WriteLine(str);
            if (_dictionaryPerson.ContainsKey(element.BasePerson)) {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
            else {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он не содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
        }

        //Поиск в коллекции dictionary<Person,IPerson>(по элементам)
        private void SearchInDictionaryPersonValue(IPerson element, string str, ref Stopwatch sw) {
            sw.Start();
            Console.WriteLine(str);
            if (_dictionaryPerson.ContainsValue(element)) {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
            else {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он не содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
        }

        //Поиск в коллекции dictionary<string,IPerson>
        private void SearchInDictionaryPerson(IPerson element, string str, ref Stopwatch sw) {
            sw.Start();
            Console.WriteLine(str);
            if (_dictionaryString.ContainsKey(element.ToString())) {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
            else {
                sw.Stop();
                Console.WriteLine("Поиск этого элемента (он не содержится в коллекции) потрачено {0} тиков\n",
                    sw.ElapsedTicks);
                sw.Reset();
            }
        }

        //Время поиска первого элемента
        private void SearchFirstElement(IPerson person) {
            var sw = new Stopwatch();

            SearchInListString(person, "Поиск первого элемента в коллекции list<string>:", ref sw);
            SearchInListPerson(person, "Поиск первого элемента в коллекции list<Person>:", ref sw);
            SearchInDictionaryPerson(person, "Поиск первого элемента в коллекции dictionary<string,IPerson>:", ref sw);
            SearchInDictionaryPersonKey(person,
                "Поиск первого элемента в коллекции dictionary<Person,IPerson>(по ключам):",
                ref sw);
            SearchInDictionaryPersonValue(person,
                "Поиск первого элемента в коллекции dictionary<Person,IPerson>(по элементам):",
                ref sw);
        }

        //Время поиска центрального элемента
        private void SearchCentralElement(IPerson person) {
            {
                var sw = new Stopwatch();

                SearchInListString(person, "Поиск центрального элемента в коллекции list<string>:", ref sw);
                SearchInListPerson(person, "Поиск центрального элемента в коллекции list<Person>:", ref sw);
                SearchInDictionaryPerson(person, "Поиск центрального элемента в коллекции dictionary<string,IPerson>:",
                    ref sw);
                SearchInDictionaryPersonKey(person,
                    "Поиск центрального элемента в коллекции dictionary<Person,IPerson>(по ключам):",
                    ref sw);
                SearchInDictionaryPersonValue(person,
                    "Поиск центрального элемента в коллекции dictionary<Person,IPerson>(по элементам):",
                    ref sw);
            }
        }

        //Время поиска последнего элемента
        private void SearchLastElement(IPerson person) {
            {
                var sw = new Stopwatch();

                SearchInListString(person, "Поиск последнего элемента в коллекции list<string>:", ref sw);
                SearchInListPerson(person, "Поиск последнего элемента в коллекции list<Person>:", ref sw);
                SearchInDictionaryPerson(person, "Поиск последнего элемента в коллекции dictionary<string,IPerson>:",
                    ref sw);
                SearchInDictionaryPersonKey(person,
                    "Поиск последнего элемента в коллекции dictionary<Person,IPerson>(по ключам):",
                    ref sw);
                SearchInDictionaryPersonValue(person,
                    "Поиск последнего элемента в коллекции dictionary<Person,IPerson>(по элементам):",
                    ref sw);
            }
        }

        //Время поиска элемента, находящегося вне последовательности
        private void SearchRandomElement(IPerson person) {
            {
                var sw = new Stopwatch();

                SearchInListString(person, "Поиск элемента вне коллекции list<string>:", ref sw);
                SearchInListPerson(person, "Поиск элемента вне коллекции list<Person>:", ref sw);
                SearchInDictionaryPerson(person, "Поиск элемента вне коллекции dictionary<string,IPerson>:", ref sw);
                SearchInDictionaryPersonKey(person,
                    "Поиск элемента вне коллекции dictionary<Person,IPerson>(по ключам):",
                    ref sw);
                SearchInDictionaryPersonValue(person,
                    "Поиск элемента вне коллекции dictionary<Person,IPerson>(по элементам):",
                    ref sw);
            }
        }

        //Обобщенная функция 
        private void Search() {
            var array = _listKeys.ToArray();
            IPerson person = new Person("Баба", "Яга");
            switch (array.Length) {
                case 1:
                    person = person.Create(_dictionaryPerson[array[0]]);
                    SearchFirstElement(person);
                    SearchRandomElement(new Student("Иван", "Кукушкин", 1, 6));
                    Console.WriteLine("Прочие проверки для массива размером 1 бессмысленны");
                    break;
                case 2:
                    person = person.Create(_dictionaryPerson[array[0]]);
                    SearchFirstElement(person);
                    person = person.Create(_dictionaryPerson[array[array.Length - 1]]);
                    SearchLastElement(person);
                    SearchRandomElement(new Student("Иван", "Кукушкин", 1, 6));
                    Console.WriteLine("Прочие проверки для массива размером 2 бессмысленны");
                    break;
                default:
                    person = person.Create(_dictionaryPerson[array[0]]);
                    SearchFirstElement(person);
                    person = person.Create(_dictionaryPerson[array[array.Length - 2]]);
                    SearchCentralElement(person);
                    person = person.Create(_dictionaryPerson[array[array.Length - 1]]);
                    SearchLastElement(person);
                    SearchRandomElement(new Student("Иван", "Кукушкин", 1, 6));
                    break;
            }

            Continue();
        }

        #endregion
    }
}