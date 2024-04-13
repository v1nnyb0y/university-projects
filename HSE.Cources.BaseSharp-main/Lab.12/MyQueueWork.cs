using System;
using Lab._11;
using ReadLib;

namespace Lab._12
{
    public class MyQueueWork
    {
        private MyQueue<IPerson> _queue;

        public MyQueueWork() { }

        public MyQueueWork(MyQueue<IPerson> queue) {
            _queue = queue;
        }

        private void Continue() {
            Console.WriteLine("\n\n\nДля продолжения нажать любую клавишу...");
            Console.ReadKey(true);
        }

        public void Start() {
            string[] mainMenu =
                {
                    "Создать очередь.", "Проверка св-в коллекции.", "Проверка метода Contains.",
                    "Проверка метода Clear.",
                    "Проверка метода Dequeue.", "Проверка метода Enqueue.", "Проверка метода Peek.",
                    "Проверка метода ToArray.",
                    "Проверка метода Clone.", "Проверка метода CopyTo.", "Выход."
                };
            var k = 9;
            while (true) {
                var sw = Print.Menu(k, mainMenu);
                switch (sw) {
                    case 1:
                        k = Create();
                        break;
                    case 2:
                        CurrentQueue();
                        Console.WriteLine("\nОбъем коллекции: {0}", _queue.Capacity);
                        Console.WriteLine("\nКол-во элементов в последовательности: {0}", _queue.Count);
                        Continue();
                        break;
                    case 3:
                        CurrentQueue();
                        IPerson person1 = new Student("Адам", "Арсанукаев", 1, 10);
                        var person2 = _queue.Peek();
                        Console.WriteLine(
                            "\nЧеловек " + person1.Return_SeName() + " " + person1.Return_Name() +
                            " содержиться ли в очереди?\n{0}", _queue.Contains(person1));
                        Console.WriteLine(
                            "\nЧеловек " + person2.Return_SeName() + " " + person2.Return_Name() +
                            " содержиться ли в очереди?\n{0}", _queue.Contains(person2));
                        Continue();
                        break;
                    case 4:
                        CurrentQueue();
                        _queue.Clear();
                        Console.WriteLine("Очередь очищена...");
                        k = 0;
                        Continue();
                        break;
                    case 5:
                        CurrentQueue();
                        var person = _queue.Dequeue();
                        Console.WriteLine("\nВывели человека: ");
                        person.Show();
                        Console.WriteLine("\nОчередь после изменения: ");
                        CurrentQueue();
                        Continue();
                        break;
                    case 6:
                        CurrentQueue();
                        var add = CreateIPerson.CreateElement<Associate>();
                        Console.WriteLine("\nДобавим в конец списка: ");
                        add.Show();
                        _queue.Enqueue(add);
                        Console.WriteLine("\nОчередь после изменения: ");
                        CurrentQueue();
                        Continue();
                        break;
                    case 7:
                        CurrentQueue();
                        person = _queue.Peek();
                        Console.WriteLine("\nВывели человека: ");
                        person.Show();
                        Console.WriteLine("\nОчередь не изменена: ");
                        CurrentQueue();
                        Continue();
                        break;
                    case 8:
                        CurrentQueue();
                        var array = _queue.ToArray();
                        Console.WriteLine("Вывод массива: ");
                        Output(array);
                        Continue();
                        break;
                    case 9:
                        CurrentQueue();
                        var clone = new MyQueue<IPerson>(_queue);
                        Console.WriteLine("Вывод копии: ");
                        var cloneMyQueueWork = new MyQueueWork(clone);
                        cloneMyQueueWork.Output();
                        break;
                    case 10:
                        CurrentQueue();
                        _queue.CopyTo(out array, _queue.Count / 2);
                        Console.WriteLine("Вывод массива: ");
                        Output(array);
                        Continue();
                        break;
                    case 11:
                        return;
                }
            }
        }

        #region Create

        //Создать очередь
        private int Create() {
            var k = 9;
            string[] createMenu =
                {
                    "Создать очередь с фиксированным объемом.(заполнится автоматически)",
                    "Создать очередь с заданным объемом.(заполнится автоматически)",
                    "Создать очередь (скопировать из существующей).", "Назад."
                };
            while (true) {
                var sw = Print.Menu(0, createMenu);
                switch (sw) {
                    case 1:
                        CreateFixCapacity();
                        return 0;
                    case 2:
                        CreateFromCapacity();
                        return 0;
                    case 3:
                        CreateFromReady();
                        return 0;
                    case 4:
                        return k;
                }
            }
        }

        //Создать очередь из фиксированной Capacity
        private void CreateFixCapacity() {
            _queue = new MyQueue<IPerson>();
            var array = CreateIPerson.CreateArray(_queue.Capacity * 2);
            foreach (var arrayElement in array) _queue.Enqueue(arrayElement);
        }

        //Создать очередь из готовой
        private void CreateFromReady() {
            var tmpQueue = new MyQueue<IPerson>();
            var add = CreateIPerson.CreateArray(10);
            foreach (var addElement in add) tmpQueue.Enqueue(addElement);
            _queue = new MyQueue<IPerson>(tmpQueue);
        }

        //Создать очередь из заданной Capacity
        private void CreateFromCapacity() {
            _queue = new MyQueue<IPerson>(Input("Введите объем очереди: "));
            var array = CreateIPerson.CreateArray(_queue.Capacity * 2);
            foreach (var arrayElement in array) _queue.Enqueue(arrayElement);
        }

        //Ввод int
        private int Input(string str) {
            var ok = true;
            var digit = 0;
            while (ok) {
                Console.Write(str);
                ok = int.TryParse(Console.ReadLine(), out digit);
                if (!ok) {
                    Console.WriteLine("Ошибка ввода! Повторите ввод...");
                    ok = true;
                }
                else {
                    ok = false;
                }
            }

            return digit;
        }

        #endregion

        #region Output

        //Вывод коллекции
        private void Output() {
            foreach (IPerson element in _queue) element.Show();
        }

        //Вывод текущей очереди
        private void CurrentQueue() {
            Console.WriteLine("Текущая очередь: ");
            Output();
        }

        //Вывод массива
        private static void Output(IPerson[] array) {
            foreach (var element in array) element.Show();
        }

        #endregion
    }
}