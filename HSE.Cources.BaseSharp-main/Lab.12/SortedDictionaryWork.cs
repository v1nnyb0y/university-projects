using System;
using System.Collections.Generic;
using Lab._11;
using ReadLib;

namespace Lab._12
{
    public class SortedDictionaryWork
    {
        private SortedDictionary<string, IPerson> _persons;

        public SortedDictionaryWork() { }

        public SortedDictionaryWork(SortedDictionary<string, IPerson> persons) {
            _persons = persons;
        }

        #region AddElement

        //Добавить объект
        private void Add() {
            string[] addMenu =
                    {"Добавить студента.", "Добавить сотрудника.", "Добавить учителя.", "Назад."};
            while (true) {
                var sw = Print.Menu(0, addMenu);
                IPerson person;
                switch (sw) {
                    case 1:
                        Console.WriteLine("Введите студента для добавления:");
                        person = new Student();
                        person.Input();
                        _persons.Add(person.Return_SeName() + " " + person.Return_Name(), person);

                        Console.WriteLine("Объект успешно добавлен.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("Введите сотрудника для добавления:");
                        person = new Associate();
                        person.Input();
                        _persons.Add(person.Return_SeName() + " " + person.Return_Name(), person);

                        Console.WriteLine("Объект успешно добавлен.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("Введите учителя для добавления:");
                        person = new Teacher();
                        person.Input();
                        _persons.Add(person.Return_SeName() + " " + person.Return_Name(), person);

                        Console.WriteLine("Объект успешно добавлен.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 4:
                        return;
                }
            }
        }

        #endregion

        #region DeleteElement

        //Удалить объект
        private void Delete(out int k) {
            string[] addMenu =
                    {"Удалить студента.", "Удалить сотрудника.", "Удалить учителя.", "Назад."};
            k = 0;
            while (true) {
                var sw = Print.Menu(0, addMenu);
                IPerson person;
                switch (sw) {
                    case 1:
                        Console.WriteLine("Введите студента для удаления:");
                        person = new Student();
                        person.Input();

                        if (!_persons.ContainsKey(person.Return_SeName() + " " + person.Return_Name())) {
                            Console.WriteLine(
                                "Объект для удаления отсутсвует в словаре.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }
                        else {
                            _persons.Remove(person.Return_SeName() + " " + person.Return_Name());
                            Console.WriteLine(
                                "Объект успешно удален.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }

                        if (_persons.Count == 0) {
                            k = 6;
                            return;
                        }

                        break;
                    case 2:
                        Console.WriteLine("Введите сотрудника для удаления:");
                        person = new Associate();
                        person.Input();
                        if (!_persons.ContainsKey(person.Return_SeName() + " " + person.Return_Name())) {
                            Console.WriteLine(
                                "Объект для удаления отсутсвует в словаре.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }
                        else {
                            _persons.Remove(person.Return_SeName() + " " + person.Return_Name());
                            Console.WriteLine(
                                "Объект успешно удален.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }

                        if (_persons.Count == 0) {
                            k = 6;
                            return;
                        }

                        break;
                    case 3:
                        Console.WriteLine("Введите учителя для удаления:");
                        person = new Teacher();
                        person.Input();
                        if (!_persons.ContainsKey(person.Return_SeName() + " " + person.Return_Name())) {
                            Console.WriteLine(
                                "Объект для удаления отсутсвует в словаре.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }
                        else {
                            _persons.Remove(person.Return_SeName() + " " + person.Return_Name());
                            Console.WriteLine(
                                "Объект успешно удален.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }

                        if (_persons.Count == 0) {
                            k = 6;
                            return;
                        }

                        break;
                    case 4:
                        return;
                }
            }
        }

        #endregion

        #region Find

        //Тип поиска
        private void TypeFind() {
            string[] queriesMenu =
                    {"Поиск Student.", "Поиск Associate.", "Поиск Teacher.", "Назад."};
            while (true) {
                var sw = Print.Menu(0, queriesMenu);
                switch (sw) {
                    case 1:
                        IPerson person = new Student();
                        person.Input();
                        if (_persons.ContainsKey(person.Return_SeName() + " " + person.Return_Name()))
                            Console.WriteLine(
                                "Введенный элемент - {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                                _persons[person.Return_SeName() + " " + person.Return_Name()]);
                        else
                            Console.WriteLine(
                                "Заданный объект не был найден в стэке.\n\n\nДля продолженния нажать на любую клавишу...");

                        Console.ReadKey(true);
                        break;
                    case 2:
                        person = new Associate();
                        person.Input();
                        if (_persons.ContainsKey(person.Return_SeName() + " " + person.Return_Name()))
                            Console.WriteLine(
                                "Введенный элемент - {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                                _persons[person.Return_SeName() + " " + person.Return_Name()]);
                        else
                            Console.WriteLine(
                                "Заданный объект не был найден в стэке.\n\n\nДля продолженния нажать на любую клавишу...");

                        Console.ReadKey(true);
                        break;
                    case 3:
                        person = new Teacher();
                        person.Input();
                        if (_persons.ContainsKey(person.Return_SeName() + " " + person.Return_Name()))
                            Console.WriteLine(
                                "Введенный элемент - {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                                _persons[person.Return_SeName() + " " + person.Return_Name()]);
                        else
                            Console.WriteLine(
                                "Заданный объект не был найден в стэке.\n\n\nДля продолженния нажать на любую клавишу...");

                        Console.ReadKey(true);
                        break;
                    case 4:
                        return;
                }
            }
        }

        #endregion

        #region Output

        //Вывод
        public void Output() {
            foreach (var key in _persons.Keys) _persons[key].Show();

            Console.WriteLine("\n\n\nДля продолженния нажать на любую клавишу...");
            Console.ReadKey(true);
        }

        #endregion

        public void Start() {
            string[] stackMenu =
                {
                    "Создать коллекцию.", "Добавить элемент.", "Удалить элемент.", "Выполнение запросов.",
                    "Клонирование коллекции.", "Сортировка коллекции (+ поиск элемента).",
                    "Вывод коллекции (используя foreach).", "Назад."
                };
            var k = 6;
            while (true) {
                var sw = Print.Menu(k, stackMenu);
                switch (sw) {
                    case 1:
                        CreateDictionary();
                        k = 0;
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Delete(out k);
                        break;
                    case 4:
                        TypeQueries();
                        break;
                    case 5:
                        Clone();
                        break;
                    case 6:
                        TypeFind();
                        break;
                    case 7:
                        Output();
                        break;
                    case 8:
                        return;
                }
            }
        }

        #region CreateDictionary

        //Ввод int
        private static int Input(string str) {
            var ok = true;
            var digit = 0;
            while (ok) {
                Console.Write(str);
                ok = int.TryParse(Console.ReadLine(), out digit);
                if (!ok) {
                    Console.WriteLine("Ошибка ввода! Повтире ввода...");
                    ok = true;
                }
                else {
                    ok = false;
                }
            }

            return digit;
        }

        //Создать коллекцию
        private void CreateDictionary() {
            _persons = new SortedDictionary<string, IPerson>(new ComparerForDictionary());
            int size;
            while (true) {
                size = Input("Введите размер коллекции: ");
                if (size == 0)
                    Console.WriteLine("Введена пустая коллекция. Повторите ввод...");
                else
                    break;
            }

            var array = CreateIPerson.CreateArray(size);
            foreach (var element in array) _persons.Add(element.Return_SeName() + " " + element.Return_Name(), element);
        }

        #endregion

        #region Queries

        //Меню
        private void TypeQueries() {
            string[] queriesMenu =
                    {"Запросы к типу Student.", "Запросы к типу Associate.", "Запросы к типу Teacher.", "Назад."};
            while (true) {
                var sw = Print.Menu(0, queriesMenu);
                switch (sw) {
                    case 1:
                        Queries<Student>();
                        break;
                    case 2:
                        Queries<Associate>();
                        break;
                    case 3:
                        Queries<Teacher>();
                        break;
                    case 4:
                        return;
                }
            }
        }

        //Запросы
        private void Queries<T>() {
            string[] queriesMenu = {"Кол-во объектов.", "Печать объектов.", "Перегенерировать объекты", "Назад."};
            while (true) {
                var sw = Print.Menu(0, queriesMenu);
                switch (sw) {
                    case 1:
                        var count = 0;
                        foreach (var key in _persons.Keys)
                            try {
                                count++;
                            }
                            catch {
                                // ignored
                            }

                        Console.WriteLine(
                            "Кол-во объектов выбранного типа = {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                            count);
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("Объекты выбранного типа: ");
                        foreach (var key in _persons.Keys)
                            try {
                                var element = (T) _persons[key];
                                CreateIPerson.Show(element);
                            }
                            catch {
                                // ignored
                            }

                        Console.WriteLine("\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 3:
                        count = 0;
                        var tmp = new SortedDictionary<string, IPerson>(ClonaDictionary());
                        foreach (var key in tmp.Keys)
                            try {
                                _persons.Remove(key);
                                count++;
                            }
                            catch {
                                // ignored
                            }

                        for (var i = 0; i < count; i++) {
                            var add = CreateIPerson.CreateElement<T>();
                            _persons.Add(add.Return_SeName() + " " + add.Return_Name(), add);
                        }

                        Console.WriteLine(
                            "Объекты выбранного типы были перезаписаны.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 4:
                        return;
                }
            }
        }

        #endregion

        #region Clone

        //Клон
        private void Clone() {
            Console.WriteLine("Исходный словарь: ");
            Output();
            Console.WriteLine("\n\n\nСклонированный стэк: ");
            var clone = new SortedDictionaryWork(ClonaDictionary());
            clone.Output();
        }

        //Клон
        private SortedDictionary<string, IPerson> ClonaDictionary() {
            var newDictionary = new SortedDictionary<string, IPerson>(new ComparerForDictionary());
            foreach (var keys in _persons.Keys) newDictionary.Add(keys, _persons[keys]);

            return newDictionary;
        }

        #endregion
    }

    internal class ComparerForDictionary : IComparer<string>
    {
        int IComparer<string>.Compare(string obj1, string obj2) {
            var person1 = obj1.Split(' ');
            var person2 = obj2.Split(' ');

            if (string.CompareOrdinal(person1[0], person2[0]) >= 1) return 1;

            if (string.CompareOrdinal(person1[0], person2[0]) <= -1) return -1;

            if (string.CompareOrdinal(person1[1], person2[1]) >= 1) return 1;

            if (string.CompareOrdinal(person1[1], person2[1]) <= -1) return -1;

            return 0;
        }
    }
}