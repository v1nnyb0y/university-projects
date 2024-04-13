using System;
using Lab._11;

namespace Lab._14
{
    internal class Program
    {
        private static void Main() {
            MyNewCollection myQueue_1 = new MyNewCollection("Первая коллекция");
            MyNewCollection myQueue_2 = new MyNewCollection("Вторая коллекция");

            Journal j_1 = new Journal("Изменение в первой коллекции");
            Journal jReference = new Journal("Изменение состояния в обеих коллекциях");

            myQueue_1.CollectionCountChanged += new CollectionHandler(j_1.CollectionCountChanged);
            myQueue_1.CollectionReferenceChanged+=new CollectionHandler(j_1.CollectionReferenceChanged);

            myQueue_1.CollectionReferenceChanged += new CollectionHandler(jReference.CollectionReferenceChanged);
            myQueue_2.CollectionReferenceChanged += new CollectionHandler(jReference.CollectionReferenceChanged);

            myQueue_1.Filling();
            myQueue_2.Filling();

            string[] menu =
                {
                    "Добавить элемент в первую очередь.", "Добавить элемент во вторую очередь.",
                    "Удалить элемент из первой очереди.", "Удалить элемент из второй очерели.",
                    "Присвоить новые значение элементам первой очереди.", "Присвоить новые значения элементам второй очереди.",
                    "Выход."
                };

            while (true) {
                var idMenu = ReadLib.Print.Menu(0, menu);
                switch (idMenu) {
                    case 1: {
                        myQueue_1.AddDefault();
                        break;
                    }
                    case 2: {
                        myQueue_2.AddDefault();
                        break;
                    }
                    case 3: {
                        Console.Write("Введите индекс для удаления элемента из очереди: ");
                        int id = ReadLib.ReadLib.ReadVGran(0);
                        myQueue_1.Remove(id);
                        break;
                    }
                    case 4: {
                        Console.Write("Введите индекс для удаления элемента из очереди: ");
                        int id = ReadLib.ReadLib.ReadVGran(0);
                        myQueue_2.Remove(id);
                        break;
                    }
                    case 5:
                    {
                        Console.Write("Введите индекс для изменения элемента из очереди: ");
                        int id = ReadLib.ReadLib.ReadVGran(0);
                        myQueue_1[id] = CreateIPerson.CreateElement<Student>();
                        break;
                    }
                    case 6:
                    {
                        Console.Write("Введите индекс для изменения элемента из очереди: ");
                        int id = ReadLib.ReadLib.ReadVGran(0);
                        myQueue_2[id] = CreateIPerson.CreateElement<Associate>();
                        break;
                    }
                    case 7: {
                        return;
                    }
                }
            }
        }
    }
}