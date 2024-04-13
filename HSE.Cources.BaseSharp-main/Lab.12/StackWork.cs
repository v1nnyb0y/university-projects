using System;
using System.Collections.Generic;
using Lab._11;
using ReadLib;
using Action = ReadLib.Action;

namespace Lab._12
{
    public class StackWork
    {
        private Stack<IPerson> _stack;

        public StackWork() { }

        public StackWork(Stack<IPerson> stack) {
            _stack = stack;
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
                        _stack.Push(person);

                        Console.WriteLine("Объект успешно добавлен.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("Введите сотрудника для добавления:");
                        person = new Associate();
                        person.Input();
                        _stack.Push(person);

                        Console.WriteLine("Объект успешно добавлен.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("Введите учителя для добавления:");
                        person = new Teacher();
                        person.Input();
                        _stack.Push(person);

                        Console.WriteLine("Объект успешно добавлен.\n\n\nДля продолженния нажать на любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case 4:
                        return;
                }
            }
        }

        #endregion

        #region Sort

        //Сортировка стэка
        private void Sort() {
            var array = _stack.ToArray();
            Array.Sort(array);
            CreateStack(array);
            Console.WriteLine("Отсортированный стэк: ");
            Output();

            Console.WriteLine("\n\n\nДля продолженния нажать на любую клавишу...");
            Console.ReadKey(true);
        }

        #endregion

        #region Output

        //Вывод
        public void Output() {
            foreach (var element in _stack) element.Show();

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
                        CreateStack();
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
                        Sort();
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

        #region CreateStack

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

        //Создать Stack
        private void CreateStack() {
            int size;
            while (true) {
                size = Input("Введите размер стэка: ");
                if (size <= 0)
                    Console.WriteLine("Введена пустая/отрицательная последовательность! Повторите ввод...");
                else
                    break;
            }

            _stack = new Stack<IPerson>(size);
            var array = CreateIPerson.CreateArray(size);
            for (var i = 0; i < size; i++) _stack.Push(array[i]);
        }

        //Создать Stack
        private void CreateStack(IPerson[] array) {
            _stack = new Stack<IPerson>(array.Length);
            foreach (var element in array)
                _stack.Push(element);
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

                        var tmp = _stack.ToArray();
                        var preSize = tmp.Length;
                        RemoveFromArray(ref tmp, person);
                        CreateStack(tmp);
                        if (preSize == tmp.Length) {
                            Console.WriteLine(
                                "Объект для удаления отсутсвует в стэке.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }
                        else {
                            Console.WriteLine(
                                "Объект успешно удален.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }

                        if (_stack.Count == 0) {
                            k = 6;
                            return;
                        }

                        break;
                    case 2:
                        Console.WriteLine("Введите сотрудника для удаления:");
                        person = new Associate();
                        person.Input();
                        tmp = _stack.ToArray();
                        preSize = tmp.Length;
                        RemoveFromArray(ref tmp, person);
                        CreateStack(tmp);
                        if (preSize == tmp.Length) {
                            Console.WriteLine(
                                "Объект для удаления отсутсвует в стэке.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }
                        else {
                            Console.WriteLine(
                                "Объект успешно удален.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }

                        if (_stack.Count == 0) {
                            k = 6;
                            return;
                        }

                        break;
                    case 3:
                        Console.WriteLine("Введите учителя для удаления:");
                        person = new Teacher();
                        person.Input();
                        tmp = _stack.ToArray();
                        preSize = tmp.Length;
                        RemoveFromArray(ref tmp, person);
                        CreateStack(tmp);
                        if (preSize == tmp.Length) {
                            Console.WriteLine(
                                "Объект для удаления отсутсвует в стэке.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }
                        else {
                            Console.WriteLine(
                                "Объект успешно удален.\n\n\nДля продолженния нажать на любую клавишу...");
                            Console.ReadKey(true);
                        }

                        if (_stack.Count == 0) {
                            k = 6;
                            return;
                        }

                        break;
                    case 4:
                        return;
                }
            }
        }

        //Удаление из массива IPerson[]
        private static void RemoveFromArray(ref IPerson[] array, IPerson element) {
            var ok = false;
            for (var i = 0; i < array.Length - 1; i++)
                if (array[i].CompareTo(element) == 0) {
                    ok = true;
                    Action.Swap(ref array[i], ref array[i + 1]);
                }

            if (ok)
                Array.Resize(ref array, array.Length - 1);
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
                        foreach (var person in _stack)
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
                        foreach (var person in _stack)
                            try {
                                var element = (T) person;
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
                        var array = _stack.ToArray();
                        foreach (var person in _stack)
                            try {
                                var element = (T) person;
                                RemoveFromArray(ref array, (IPerson) element);
                                count++;
                            }
                            catch {
                                // ignored
                            }

                        CreateStack(array);
                        for (var i = 0; i < count; i++) _stack.Push(CreateIPerson.CreateElement<T>());
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
            Console.WriteLine("Исходный стэк: ");
            Output();
            Console.WriteLine("\n\n\nСклонированный стэк: ");
            var clone = new StackWork(CloneStack());
            clone.Output();
        }

        //Клон
        private Stack<IPerson> CloneStack() {
            var newStack = new Stack<IPerson>(_stack.Count);
            foreach (var element in _stack) newStack.Push(element);

            return newStack;
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
                        var student = new Student();
                        student.Input();
                        var number = Find(student);
                        if (number == 0)
                            Console.WriteLine(
                                "Заданный объект не был найден в стэке.\n\n\nДля продолженния нажать на любую клавишу...");
                        else
                            Console.WriteLine(
                                "Номер объекта в отсортированном массиве - {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                                number);
                        Console.ReadKey(true);
                        break;
                    case 2:
                        var associate = new Associate();
                        associate.Input();
                        number = Find(associate);
                        if (number == 0)
                            Console.WriteLine(
                                "Заданный объект не был найден в стэке.\n\n\nДля продолженния нажать на любую клавишу...");
                        else
                            Console.WriteLine(
                                "Номер объекта в отсортированном массиве - {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                                number);
                        Console.ReadKey(true);
                        break;
                    case 3:
                        var teacher = new Teacher();
                        teacher.Input();
                        number = Find(teacher);
                        if (number == 0)
                            Console.WriteLine(
                                "Заданный объект не был найден в стэке.\n\n\nДля продолженния нажать на любую клавишу...");
                        else
                            Console.WriteLine(
                                "Номер объекта в отсортированном массиве - {0}.\n\n\nДля продолженния нажать на любую клавишу...",
                                number);
                        Console.ReadKey(true);
                        break;
                    case 4:
                        return;
                }
            }
        }

        //Поиск объекта
        private int Find(IPerson element) {
            var array = _stack.ToArray();
            return Array.BinarySearch(array, element) + 1;
        }

        #endregion
    }
}