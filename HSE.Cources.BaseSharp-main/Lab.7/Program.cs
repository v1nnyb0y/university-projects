using System;
using ReadLib;

namespace Lab._7
{
    //Класс для однонаправленного списка
    internal class PointOne
    {
        public int Data; //информационное поле
        public PointOne Next; //адресное поле

        public PointOne() //конструктор без параметров
        {
            Data = 0;
            Next = null;
        }

        public PointOne(int d) //конструктор с параметрами
        {
            Data = d;
            Next = null;
        }

        public override string ToString() {
            return Data + " ";
        }
    }

    //Класс для двунаправленного списка
    internal class PointTwo
    {
        public double Data;

        public PointTwo Next, //адрес следующего элемента
            Pred; //адрес предыдущего элемента

        public PointTwo() {
            Data = 0;
            Next = null;
            Pred = null;
        }

        public PointTwo(double d) {
            Data = d;
            Next = null;
            Pred = null;
        }

        public override string ToString() {
            return Data + " ";
        }
    }

    //Класс для бинарного дерева
    internal class PointTree
    {
        public string Data;

        public PointTree Left, //адрес левого поддерева 
            Right; //адрес правого поддерева

        public PointTree() {
            Data = "";
            Left = null;
            Right = null;
        }

        public PointTree(string d) {
            Data = d;
            Left = null;
            Right = null;
        }

        public override string ToString() {
            return Data + " ";
        }
    }


    internal class Program
    {
        private static readonly string[] Index =
            {
                "Задача 1. Работа с однонаправленными списками", "Задача 2. Работа с двунаправленными списками",
                "Задача 3. Работа с бинарными деревьями",
                "Выход", "Назад", "Заполнение списка",
                "Удаление элементов с четным информационным полем(только первый)",
                "Показать список",
                "Для продолжения нажмите Enter...", "Добавить элемент после элемента с заданным информационным полем",
                "Ввод списка ДСЧ", "Ввод списка с клавиатуры",
                "Добавление элемента с ДСЧ", "Добавление элемента с клавиатуры",
                "Печать дерева(текущего, после определенного действия)",
                "Формирование идеально сбалансированного дерева",
                "Подсчет кол-ва листьев", "Преобразование идеально сбалансированного дерева в дерево поиска"
            };

        private static void Main() {
            var exit = false;

            while (!exit) {
                var sw = Print.Menu(0, Index[0], Index[1], Index[2], Index[3]);
                switch (sw) {
                    case 1:
                        DeleteElements();
                        break;


                    case 2:
                        AddElement();
                        break;


                    case 3:
                        NumberLeaves();
                        break;


                    case 4:
                        exit = true; //Выход
                        break;
                }
            }

            Console.WriteLine("Для продолжения нажмите Enter...");
        }

        //Задание 1. Удаление первого элемента из списка с четным информационным полем

        #region taskOne

        //создание однонаправленного списка
        private static PointOne MakePoint(int d) {
            var p = new PointOne(d);
            return p;
        }

        //добавление в начало однонаправленного списка(рандом)
        private static PointOne MakeListOneRandom(int size) {
            var rnd = new Random();
            var info = rnd.Next(-100, 101);
            Console.WriteLine("The element {0} is adding...", info);
            var beg = MakePoint(info); //создаем первый элемент
            for (var i = 1; i < size; i++) {
                info = rnd.Next(-100, 101);
                Console.WriteLine("The element {0} is adding...", info);
                //создаем элемент и добавляем в начало списка
                var p = MakePoint(info);
                p.Next = beg;
                beg = p;
            }

            return beg;
        }

        //добавление в начало однонаправленного списка(клавиатура)
        private static PointOne MakeListOneKeyboard(int size) {
            Console.Write("Read element: ");
            var info = ReadLib.ReadLib.ReadInt();
            Console.WriteLine("The element {0} is adding...", info);
            var beg = MakePoint(info); //создаем первый элемент
            for (var i = 1; i < size; i++) {
                Console.Write("Read element: ");
                info = ReadLib.ReadLib.ReadInt();
                Console.WriteLine("The element {0} is adding...", info);
                //создаем элемент и добавляем в начало списка
                var p = MakePoint(info);
                p.Next = beg;
                beg = p;
            }

            return beg;
        }

        //Вывести список
        private static void ShowList(PointOne beg) {
            //проверка наличия элементов в списке
            if (beg == null) {
                Console.WriteLine("The List is empty");
                return;
            }

            var p = beg;
            while (p != null) {
                Console.Write(p);
                p = p.Next; //переход к следующему элементу
            }

            Console.WriteLine();
        }

        //Удалляем элемент с заданным номером
        private static PointOne DelElement(PointOne beg, ref int sizeOne) {
            if (beg == null) //пустой список
            {
                Console.WriteLine("Error! The List is empty");
                return null;
            }


            var p = beg;
            if (p.Data % 2 == 0) {
                beg = beg.Next;
                sizeOne--;
                return beg;
            }

            for (var i = 0; i < sizeOne - 1 && p.Next != null; i++)
                if (p.Next.Data % 2 != 0)
                    p = p.Next;
                else
                    break;

            if (p.Next == null) {
                Console.WriteLine("Error! Even number isn't in list");
                return beg;
            } //если элемент не найден

            //исключаем элемент из списка
            p.Next = p.Next.Next;
            sizeOne--;
            return beg;
        }

        //Создание списка
        public static PointOne CreatePoint(out int k, out int sizeOne) {
            Console.WriteLine("Введите количество элементов в однонаправленном списке: ");
            sizeOne = ReadLib.ReadLib.ReadVGran(0);
            k = 2;
            if (sizeOne == 0) {
                Console.WriteLine("Error! The list is empty");
            }
            else {
                var sw = Print.Menu(0, Index[10], Index[11], Index[4]);
                switch (sw) {
                    case 1:
                        k = 0;
                        return MakeListOneRandom(sizeOne);
                    case 2:
                        k = 0;
                        return MakeListOneKeyboard(sizeOne);
                    case 3:
                        return null;
                }
            }

            return null;
        }

        //Функция объединения
        public static void DeleteElements() {
            var exit = false;
            PointOne beg = null;
            var sizeOne = 0;
            var k = 2;


            while (!exit) {
                var sw = Print.Menu(k, Index[5], Index[6], Index[7], Index[4]);
                switch (sw) {
                    case 1:
                        beg = CreatePoint(out k, out sizeOne);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 2:
                        Console.WriteLine("Строка до изменений: ");
                        ShowList(beg);
                        var tmp = sizeOne;
                        beg = DelElement(beg, ref sizeOne);
                        if (sizeOne != tmp) {
                            Console.WriteLine("Строка после изменений: ");
                            ShowList(beg);
                        }

                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 3:
                        ShowList(beg);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 4:
                        exit = true; //выход
                        break;
                }
            } //Конец While
        }

        #endregion

        //Задание 2.Добавление элемента после элемента с заданным информационным полем 

        #region taskTwo

        //Создание ДСЧ
        private static PointTwo MakeListTwoRandom(int size) {
            var rnd = new Random();
            var info = Math.Round(rnd.Next(-100, 101) + rnd.NextDouble(), 3);
            Console.WriteLine("The element {0} is adding...", info);
            var beg = MakePointTwo(info);
            for (var i = 1; i < size; i++) {
                info = Math.Round(rnd.Next(-100, 101) + rnd.NextDouble(), 3);
                Console.WriteLine("The element {0} is adding...", info);
                var p = MakePointTwo(info);
                p.Next = beg;
                beg.Pred = p;
                beg = p;
            }

            return beg;
        }

        //Создание с класиатуры
        private static PointTwo MakeListTwoKeyboard(int size) {
            Console.Write("Read element: ");
            var info = ReadLib.ReadLib.ReadDouble();
            Console.WriteLine("The element {0} is adding...", info);
            var beg = MakePointTwo(info);
            for (var i = 1; i < size; i++) {
                Console.Write("Read element: ");
                info = ReadLib.ReadLib.ReadDouble();
                Console.WriteLine("The element {0} is adding...", info);
                var p = MakePointTwo(info);
                p.Next = beg;
                beg.Pred = p;
                beg = p;
            }

            return beg;
        }

        //Функция объединения
        private static void AddElement() {
            var exit = false;
            PointTwo beg = null;
            var sizeTwo = 0;
            var k = 2;


            while (!exit) {
                var sw = Print.Menu(k, Index[5], Index[9], Index[7], Index[4]);
                switch (sw) {
                    case 1:
                        beg = CreatePointTwo(out k, out sizeTwo);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 2:
                        Console.WriteLine("Список до изменений: ");
                        ShowList(beg);
                        var tmp = sizeTwo;
                        CreateExtraElement(beg, ref sizeTwo);
                        if (sizeTwo != tmp) {
                            Console.WriteLine("Cписок после изменений: ");
                            ShowList(beg);
                        }

                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 3:
                        ShowList(beg);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 4:
                        exit = true; //выход
                        break;
                }
            } //Конец While
        } //Функция объединения

        //создание элемента списка
        private static PointTwo MakePointTwo(double d) {
            var p = new PointTwo(d);
            return p;
        }

        //Добавление элемента с заданным номером c ДСЧ
        private static PointTwo AddPointRandom(PointTwo beg, PointTwo point, ref int sizeTwo) {
            var rnd = new Random();


            if (beg == null) //список пустой
            {
                Console.WriteLine("Error! The List is empty...");
                return beg;
            }

            var p = beg;
            for (var i = 0; i < sizeTwo && p != null; i++)
                if (p.Data != point.Data)
                    p = p.Next;
                else
                    break;

            if (p == null) {
                Console.WriteLine("Error! The given number isn't in the the List...");
                return beg;
            }

            var info = Math.Round(rnd.Next(-100, 101) + rnd.NextDouble(), 3);
            Console.WriteLine("The element {0} is adding...", info);
            var newPoint = MakePointTwo(info);

            newPoint.Next = p.Next;
            newPoint.Pred = p;
            p.Next = newPoint;
            sizeTwo++;
            if (newPoint.Next != null) //не последний
                newPoint.Next.Pred = newPoint;
            return beg;
        }

        //Добавление элемента с заданным номером с класиатуры
        private static PointTwo AddPointKeyboard(PointTwo beg, PointTwo point, ref int sizeTwo) {
            if (beg == null) //список пустой
            {
                Console.WriteLine("Error! The List is empty...");
                return beg;
            }

            var p = beg;
            for (var i = 0; i < sizeTwo && p != null; i++)
                if (p.Data != point.Data)
                    p = p.Next;
                else
                    break;
            if (p == null) {
                Console.WriteLine("Error! The given number isn't in the the List...");
                return beg;
            }

            Console.WriteLine("Read element: ");
            var info = ReadLib.ReadLib.ReadDouble();
            Console.WriteLine("The element {0} is adding...", info);
            var newPoint = MakePointTwo(info);

            newPoint.Next = p.Next;
            newPoint.Pred = p;
            p.Next = newPoint;
            if (newPoint.Next != null) //не последний
                newPoint.Next.Pred = newPoint;
            sizeTwo++;
            return beg;
        }

        //Выбор ввода доп. элемента
        private static void CreateExtraElement(PointTwo beg, ref int sizeTwo) {
            Console.WriteLine("Введите элемент, после которого ввести элемент: ");
            var addElement = MakePointTwo(ReadLib.ReadLib.ReadDouble());
            var sw = Print.Menu(0, Index[12], Index[13], Index[4]);
            switch (sw) {
                case 1:
                    beg = AddPointRandom(beg, addElement, ref sizeTwo);
                    break;
                case 2:
                    beg = AddPointKeyboard(beg, addElement, ref sizeTwo);
                    break;
                case 3:
                    break;
            }
        }

        //Создание списка
        public static PointTwo CreatePointTwo(out int k, out int sizeTwo) {
            Console.WriteLine("Введите количество элементов в двунаправленном списке: ");
            sizeTwo = ReadLib.ReadLib.ReadVGran(0);
            k = 2;
            if (sizeTwo == 0) {
                Console.WriteLine("Error! The list is empty");
            }
            else {
                var sw = Print.Menu(0, Index[10], Index[11], Index[4]);
                switch (sw) {
                    case 1:
                        k = 0;
                        return MakeListTwoRandom(sizeTwo);
                    case 2:
                        k = 0;
                        return MakeListTwoKeyboard(sizeTwo);
                    case 3:
                        return null;
                }
            }

            return null;
        }

        //Вывести список
        private static void ShowList(PointTwo beg) {
            if (beg == null) {
                Console.WriteLine("The List is empty");
                return;
            }

            var p = beg;
            while (p != null) {
                Console.Write(p);
                p = p.Next;
            }

            Console.WriteLine();
        }

        #endregion

        //Задание 3.Найти кол-во листьев в дереве

        #region taskThree

        //Функция объединения
        private static void NumberLeaves() {
            var exit = false;
            var root = new PointTree();
            var rootSorttree = new PointTree();
            int sizeTree;
            var k = 3;

            while (!exit) {
                var sw = Print.Menu(k, Index[15], Index[16], Index[17], Index[14], Index[3]);
                switch (sw) {
                    case 1:

                        Console.WriteLine("Read size of ideal tree: ");
                        sizeTree = ReadLib.ReadLib.ReadVGran(0);
                        if (sizeTree == 0) {
                            Console.WriteLine("Error! The tree is empty");
                        }
                        else {
                            root = IdealTree(sizeTree, root);
                            k = 0;
                        }

                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 2:
                        var count = 0;
                        LeavesCount(root, ref count);
                        Console.WriteLine("Number of leaves:" + count);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 3:
                        root = CreateSortTree(root, rootSorttree);
                        ShowTree(root, 3);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 4:
                        ShowTree(root, 3);
                        Console.WriteLine(Index[8]);
                        Console.ReadLine();
                        break;


                    case 5:
                        exit = true;
                        break;
                }
            }
        }

        //построение идеально сбалансированного дерева с клавиатуры
        private static PointTree IdealTree(int size, PointTree p) {
            PointTree r;
            int nl, nr;
            if (size == 0) {
                p = null;
                return p;
            }

            nl = size / 2;
            nr = size - nl - 1;
            Console.WriteLine("Read string Tree element: ");
            var d = Console.ReadLine();
            r = new PointTree(d);
            r.Left = IdealTree(nl, r.Left);
            r.Right = IdealTree(nr, r.Right);
            p = r;
            return p;
        }

        //печать дерева по уровням
        private static void ShowTree(PointTree p, int l) {
            if (p != null) {
                ShowTree(p.Right, l + 3); //переход к левому поддереву
                for (var i = 0; i < l; i++)
                    Console.Write(" ");
                Console.WriteLine(p.Data);
                ShowTree(p.Left, l + 3); //переход к правому поддереву
            }
        }

        //Подсчет кол-ва листьев
        public static void LeavesCount(PointTree p, ref int rez) {
            if (p != null) {
                LeavesCount(p.Left, ref rez);
                if (p.Left == null && p.Right == null)
                    rez++;
                LeavesCount(p.Right, ref rez);
            }
        }

        //Добавление элемента к дереву
        private static PointTree Add(PointTree root, string d) {
            var p = root; //корень дерева
            PointTree r = null;
            //флаг для проверки существования элемента d в дереве
            var ok = false;
            while (p != null && !ok) {
                r = p;
                if (d.CompareTo(p.Data) == 0)
                    ok = true; //элемент уже существует
                else if (d.CompareTo(p.Data) == -1)
                    p = p.Left; //пойти в левое поддерево
                else
                    p = p.Right; //пойти в правое поддерево
            }

            if (ok)
                return root; //найдено, не добавляем
            //создаем узел
            var newPoint = new PointTree(d); //выделили память

            // если d<r->key, то добавляем его в левое поддерево
            if (d.CompareTo(r.Data) == -1)
                r.Left = newPoint;
            // если d>r->key, то добавляем его в правое поддерево
            else
                r.Right = newPoint;
            return root;
        }

        //Преобразование идеального дерева в дерево поиска
        private static PointTree CreateSortTree(PointTree root, PointTree sortTree) {
            if (root != null) {
                CreateSortTree(root.Left, sortTree);
                if (root.Data != " ")
                    sortTree = Add(sortTree, root.Data);
                CreateSortTree(root.Right, sortTree);
            }

            return sortTree;
        }

        #endregion
    }
}