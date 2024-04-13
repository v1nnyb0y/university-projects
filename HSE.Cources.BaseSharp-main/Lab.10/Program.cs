using System;
using ReadLib;

namespace Lab._10
{
    internal class MainCode
    {
        private static readonly string[] mainMenu =
            {
                "Проверка 1 части. Создание класса и подсчет расстояние между точками.",
                "Проверка 2 части. Проверка перегруженых операций.",
                "Проверка 3 части. Создание массива и вычисление самой удаленной точки от начала координат.",
                "Отобразить кол-во используемых переменных типа \"Point\"", "Выход."
            };

        private static readonly string endPhrase = "Для продолжения нажмите любую клавишу. . .";

        private static readonly string[] part1Text = {"Введите координаты точки.", "Отмена."};

        private static readonly string[] part2Text =
            {
                "Введите координаты точки.", "Унарные операции.", "Операции приведения типа.", "Бинарные операции",
                "Показать координаты точки", "Отмена."
            };

        private static readonly string[] part2Text_Uno =
            {
                "Проверка работы ++(добавление 1 к координате) к X.",
                "Проверка работы --(отнять 1 от координаты) из X.", "Отмена."
            };

        private static readonly string[] part2Text_Type =
            {
                "Проверка работы явного приведения(целая часть от координаты X).",
                "Проверка работы неявного приведения(координата Y).", "Отмена."
            };

        private static readonly string[] part2Text_Bin =
            {
                "Проверка работы \"+ Point p\"(вычисление расстояние до точки p).",
                "Проверка работы \"+ целое число\"(увеличить координату X на целое число).", "Отмена."
            };

        private static readonly string[] part3Text =
            {
                "Задать массив.", "Найти самую удаленную(-ые) точку(-и).", "Проверка индексатора.",
                "Печать множества точек.", "Отмена."
            };

        private static readonly string[] part3Text_Create =
                {"Задать массив вручную.", "Задать массив с помощью ДСЧ.", "Отмена."};

        //Ввод точки
        public static ClassPoint.Point ReadCoordinate(int i) {
            Console.WriteLine("Введите координаты {0} точки:", i);
            Console.Write("Координата X = ");
            var x = ReadLib.ReadLib.ReadDouble();
            Console.Write("Координата Y = ");
            var y = ReadLib.ReadLib.ReadDouble();
            Console.WriteLine();
            return new ClassPoint.Point(x, y);
        }

        private static void Main() {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, mainMenu[0], mainMenu[1], mainMenu[2], mainMenu[3], mainMenu[4]);
                switch (sw) {
                    case 1:
                        Case1();
                        break;
                    case 2:
                        Case2();
                        break;
                    case 3:
                        Case3();
                        break;
                    case 4:
                        Console.WriteLine(
                            "Кол-во переменных \"Point\", которые использовались в программе, равно {0}\n",
                            ClassPoint.Point.Count);
                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 5:
                        ok = false;
                        break;
                }
            }
        }

        #region Task_1

        //Дистанция между 2-мя координатами 
        private static void Distance() {
            var coordinate1 = ReadCoordinate(1);
            var coordinate2 = ReadCoordinate(2);
            Console.Clear();
            if (coordinate1.Distance(coordinate2) == 0) {
                Console.WriteLine("Точки с координатами: ");
                coordinate1.Show();
                Console.Write(" и ");
                coordinate2.Show();
                Console.Write(" совпадают.\n");
            }
            else {
                Console.WriteLine("Расстояние между точками: ");
                coordinate1.Show();
                Console.Write(" и ");
                coordinate2.Show();
                Console.Write(" равно {0}\n", coordinate1.Distance(coordinate2));
            }
        }

        //Фунция меню для части 1.
        private static void Case1() {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, part1Text[0], part1Text[1]);
                switch (sw) {
                    case 1:
                        Distance();

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 2:
                        ok = false;
                        break;
                }
            }
        }

        #endregion

        #region Task_2

        //Проверка перегрузки ++
        private static void CheckPlusPlus(ClassPoint.Point coordinate) {
            Console.WriteLine("Используем ++{что-то}: ");
            Console.WriteLine("Должно получится: ({0};{1})", coordinate.CoordinateX + 1, coordinate.CoordinateY);
            ++coordinate;
            Console.Write("Что получилось: ");
            coordinate.Show();
            Console.WriteLine("\n");

            Console.WriteLine("Используем {что-то}++: ");
            Console.WriteLine("Должно получится: ({0};{1})", coordinate.CoordinateX + 1, coordinate.CoordinateY);
            coordinate++;
            Console.Write("Что получилось: ");
            coordinate.Show();
            Console.WriteLine("\n");
        }

        //Проверка перегрузки --
        private static void CheckMinusMinus(ClassPoint.Point coordinate) {
            Console.WriteLine("Используем --{что-то}: ");
            Console.WriteLine("Должно получится: ({0};{1})", coordinate.CoordinateX - 1, coordinate.CoordinateY);
            --coordinate;
            Console.Write("Что получилось: ");
            coordinate.Show();
            Console.WriteLine("\n");

            Console.WriteLine("Используем {что-то}--: ");
            Console.WriteLine("Должно получится: ({0};{1})", coordinate.CoordinateX - 1, coordinate.CoordinateY);
            coordinate--;
            Console.Write("Что получилось: ");
            coordinate.Show();
            Console.WriteLine("\n");
        }

        //Проверка перегрузки (int)
        private static void CheckInt(ClassPoint.Point coordinate) {
            Console.WriteLine("Используем (int){что-то}: ");
            Console.WriteLine("Должно получится: {0}", (int) coordinate.CoordinateX);
            var x = (int) coordinate;
            Console.WriteLine("Что получилось: {0}", x);
            Console.WriteLine("\n");
        }

        //Проверка перегрузки double
        private static void CheckDouble(ClassPoint.Point coordinate) {
            Console.WriteLine("Используем double {что-то}: ");
            Console.WriteLine("Должно получится: {0}", coordinate.CoordinateY * 0.1 / 0.1);
            double y = coordinate;
            Console.WriteLine("Что получилось: {0}", y);
            Console.WriteLine("\n");
        }

        //Проверка работы перегрузки  + Point p
        private static void CheckPlusPoint(ClassPoint.Point coordinate1) {
            Console.Write("Координаты 1-ой точки: ");
            coordinate1.Show();
            Console.WriteLine("\n");
            var coordinate2 = ReadCoordinate(2);
            Console.WriteLine("Используем \"Point + Point\": ");
            Console.WriteLine("Должно получится: {0}", coordinate1.Distance(coordinate2));
            var distance = coordinate1 + coordinate2;
            Console.WriteLine("Что получилось: {0}", distance);
            Console.WriteLine("\n");
        }

        //Проверка работы парегрузки + целое число
        private static void CheckPlusIntRight(int digit, ref ClassPoint.Point coordinate) {
            Console.WriteLine("Используем Point + {0}: ", digit);
            Console.WriteLine("Должно получится: ({0};{1})", coordinate.CoordinateX + digit, coordinate.CoordinateY);
            coordinate = coordinate + digit;
            Console.Write("Что получилось: ");
            coordinate.Show();
            Console.WriteLine("\n");
        }

        //Проверка работы парегрузки + целое число
        private static void CheckPlusIntLeft(int digit, ref ClassPoint.Point coordinate) {
            Console.WriteLine("Используем {0} + Point: ", digit);
            Console.WriteLine("Должно получится: ({0};{1})", coordinate.CoordinateX + digit, coordinate.CoordinateY);
            coordinate = digit + coordinate;
            Console.Write("Что получилось: ");
            coordinate.Show();
            Console.WriteLine("\n");
        }

        //Функция меню для унарных операций
        private static void Case2_1(ClassPoint.Point coordinate) {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, part2Text_Uno[0], part2Text_Uno[1], part2Text_Uno[2]);
                switch (sw) {
                    case 1:
                        CheckPlusPlus(coordinate);

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 2:
                        CheckMinusMinus(coordinate);

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 3:
                        ok = false;
                        break;
                }
            }
        }

        //Функция меню для операций приведения типа
        private static void Case2_2(ClassPoint.Point coordinate) {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, part2Text_Type[0], part2Text_Type[1], part2Text_Type[2]);
                switch (sw) {
                    case 1:
                        CheckInt(coordinate);

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 2:
                        CheckDouble(coordinate);

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 3:
                        ok = false;
                        break;
                }
            }
        }

        //Функция меню для бинарных операций
        private static void Case2_3(ClassPoint.Point coordinate) {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, part2Text_Bin[0], part2Text_Bin[1], part2Text_Bin[2]);
                switch (sw) {
                    case 1:
                        CheckPlusPoint(coordinate);

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 2:
                        var rnd = new Random();
                        var digit = rnd.Next(-10, 10);
                        CheckPlusIntRight(digit, ref coordinate);
                        CheckPlusIntLeft(-digit, ref coordinate);

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 3:
                        ok = false;
                        break;
                }
            }
        }

        //Функция меню для части 2
        private static void Case2() {
            var k = 4;
            var ok = true;
            ClassPoint.Point coordinate = null;
            while (ok) {
                var sw = Print.Menu(k, part2Text[0], part2Text[1], part2Text[2], part2Text[3], part2Text[4],
                    part2Text[5]);
                switch (sw) {
                    case 1:
                        coordinate = ReadCoordinate(1);
                        k = 0;
                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 2:
                        Case2_1(coordinate);
                        break;
                    case 3:
                        Case2_2(coordinate);
                        break;
                    case 4:
                        Case2_3(coordinate);
                        break;
                    case 5:
                        Console.Write("Координаты точки: ");
                        coordinate.Show();
                        Console.WriteLine();
                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 6:
                        ok = false;
                        break;
                }
            }
        }

        #endregion

        #region Task_3

        //Создание массива ДСЧ
        private static void RandomArray(ref ClassPoint.PointArray pointArray) {
            Console.Write("Введите размер массива: ");
            var size = ReadLib.ReadLib.ReadVGran(0);

            Console.Write("От какого элемента заполнять: ");
            var min = ReadLib.ReadLib.ReadInt();
            Console.Write("До какого элемента заполнять: ");
            var max = ReadLib.ReadLib.ReadVGran(min);

            pointArray = new ClassPoint.PointArray(size, min, max);
        }

        //Создание массива вручную
        private static void KeyboardArray(ref ClassPoint.PointArray pointArray) {
            Console.Write("Введите размер массива: ");
            var size = ReadLib.ReadLib.ReadVGran(0);
            pointArray = new ClassPoint.PointArray(size);
        }

        //Функция меню для создания массива
        private static void Case3_1(ref ClassPoint.PointArray pointArray, ref int k) {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, part3Text_Create[0], part3Text_Create[1], part3Text_Create[2]);
                switch (sw) {
                    case 1:
                        k = 0;
                        KeyboardArray(ref pointArray);
                        ok = false;

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 2:
                        k = 0;
                        RandomArray(ref pointArray);
                        ok = false;

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 3:
                        ok = false;
                        break;
                }
            }
        }

        //Проверка индексатора(вывод)
        private static void Output(ClassPoint.PointArray pointArray) {
            Console.WriteLine("Проверка вывода!");
            Console.WriteLine("Массив размера: {0}", pointArray.GetSize);
            Console.WriteLine("Проверка индексатора: ");
            Console.WriteLine("pointArray[-1]: ");
            pointArray[-1].Show();
            Console.WriteLine("\npointArray[0]: ");
            pointArray[0].Show();
            if (pointArray.GetSize > 1) {
                Console.WriteLine("\npointArray[{0}]: ", pointArray.GetSize - 1);
                pointArray[pointArray.GetSize - 1].Show();
            }

            Console.WriteLine("\npointArray[{0}]: ", pointArray.GetSize);
            pointArray[pointArray.GetSize].Show();
            Console.WriteLine("");
        }

        //Проверка индексатора(ввод)
        private static void Input(ClassPoint.PointArray pointArray) {
            Console.WriteLine("Проверка ввода!");
            Console.WriteLine("Массив размера: {0}", pointArray.GetSize);
            Console.WriteLine("Проверка индексатора: ");
            Console.WriteLine("pointArray[-1] = (0,0): ");
            pointArray[-1] = new ClassPoint.Point(0, 0);
            Console.WriteLine("pointArray[0] = (1,1):");
            pointArray[0] = new ClassPoint.Point(1, 1);
            pointArray[0].Show();
            if (pointArray.GetSize > 1) {
                Console.WriteLine("\npointArray[{0}] = (2,2)", pointArray.GetSize - 1);
                pointArray[pointArray.GetSize - 1] = new ClassPoint.Point(2, 2);
                pointArray[pointArray.GetSize - 1].Show();
            }

            Console.WriteLine("\npointArray[{0}] = (-1,-1)", pointArray.GetSize);
            pointArray[pointArray.GetSize] = new ClassPoint.Point(-1, -1);
            Console.WriteLine("");
        }

        //Самые удаленные точки
        public static ClassPoint.PointArray DeterminationRemoteCoordinate(ClassPoint.PointArray pointArray) {
            var origin = new ClassPoint.Point(0, 0);

            var array = new ClassPoint.PointArray();
            for (var i = 0; i < pointArray.GetSize; i++)
                if (pointArray[i] > origin) {
                    array.Resize(1);
                    array[array.GetSize - 1] = pointArray[i];
                    pointArray[i].Distance(origin);
                }
                else {
                    if (pointArray[i] == origin) {
                        array.Resize(array.GetSize + 1);
                        array[array.GetSize - 1] = pointArray[i];
                    }
                }

            return array;
        }

        //Функция меню для части 3
        private static void Case3() {
            var ok = true;
            var pointArray = new ClassPoint.PointArray();
            var k = 3;
            while (ok) {
                var sw = Print.Menu(k, part3Text[0], part3Text[1], part3Text[2], part3Text[3], part3Text[4]);
                switch (sw) {
                    case 1:
                        Case3_1(ref pointArray, ref k);
                        break;
                    case 2:
                        var array = DeterminationRemoteCoordinate(pointArray);
                        if (array.GetSize == 1) {
                            Console.WriteLine("Наиболее удаленная точка: ");
                            array.Show();
                        }
                        else {
                            Console.WriteLine("Наиболее удаленные точки: ");
                            array.Show();
                        }

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 3:
                        Output(pointArray);

                        Console.WriteLine(
                            "Для проверки ввода значений(индексатор ввода) выполните следующие действия: ");
                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        Console.Clear();

                        Input(pointArray);


                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Множество точек: ");
                        pointArray.Show();

                        Console.WriteLine(endPhrase);
                        Console.ReadLine();
                        break;
                    case 5:
                        ok = false;
                        break;
                }
            }
        }

        #endregion
    }
}