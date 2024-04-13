using System;
using ReadLib;

namespace Lab._5
{
    internal class Program
    {
        private static readonly string[] index =
            {
                "Работа с одномерными массивами", "Работа с двумерными массивами(матрицы)",
                "Работа с рваными массивами", "Выход", "Задание массива с клавиатуры", "Задание массива рандомом",
                "Назад",
                "Чтобы вернутся назад, нажмите Enter", "Продолжить работу",
                "Заполнение дополнительной строки с клавиатуры",
                "Заполнение дополнительной строки рандомом"
            };

        public static int Menu(params string[] pechat) {
            Console.CursorVisible = false;
            var y = 1;
            int tek = 0, tekold = 0;
            var x = 1;
            var ok = false;
            for (var i = 0; i < pechat.Length; i++) {
                Console.SetCursorPosition(x, y + i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(pechat[i]);
            }

            do {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x, y + tekold);
                Console.Write(pechat[tekold] + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + tek);
                Console.Write(pechat[tek]);
                tekold = tek;
                var key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.DownArrow:
                        tek++;
                        break;
                    case ConsoleKey.UpArrow:
                        tek--;
                        break;
                    case ConsoleKey.Enter:
                        ok = true;
                        break;
                }

                if (tek >= pechat.Length)
                    tek = 0;
                else if (tek < 0)
                    tek = pechat.Length - 1;
            } while (!ok);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            return tek + 1;
        }

        public static int[] YdalElPoZnach(int[] arr) {
            Console.WriteLine("\nВведите значение элемента, который хотите удалить");
            var chislo = ReadLib.ReadLib.ReadInt();
            var i = 0;
            do {
                if (arr[i] == chislo) {
                    var tmp = arr[i];
                    for (var j = i; j < arr.Length - 1; j++)
                        arr[j] = arr[j + 1];
                    arr[arr.Length - 1] = tmp;
                    Array.Resize(ref arr, arr.Length - 1);
                }
                else {
                    i++;
                }
            } while (i < arr.Length);

            return arr;
        }

        public static void ZapolnMatrSKeyboard(int[,] arr) {
            for (var i = 0; i < arr.GetLength(0); i++)
            for (var j = 0; j < arr.GetLength(1); j++) {
                Console.WriteLine("Введиет элемент, стоящий на {0}-ой строки и {1}-го столбца", i + 1, j + 1);
                arr[i, j] = ReadLib.ReadLib.ReadInt();
            }
        }

        public static void ZapolnMatrRandom(int[,] arr) {
            var rnd = new Random();
            bool ok;
            int max, min;
            do {
                ok = true;
                Console.WriteLine("От какого числа заполнять значения массива рандомно");
                min = ReadLib.ReadLib.ReadInt();
                Console.WriteLine("До какого числа заполнять значение массива рандомно");
                max = ReadLib.ReadLib.ReadInt();
                if (min > max)
                    Console.WriteLine("Ошибка, введите первое число меньшее, чем второе\n");
                else
                    ok = false;
            } while (ok);

            for (var i = 0; i < arr.GetLength(0); i++)
            for (var j = 0; j < arr.GetLength(1); j++)
                arr[i, j] = rnd.Next(min, max + 1);
        }

        public static void VivodMatr(int[,] arr) {
            for (var j = 0; j < arr.GetLength(0); j++) {
                for (var i = 0; i < arr.GetLength(1); i++)
                    Console.Write("{0}\t", arr[j, i]);
                Console.WriteLine();
            }
        }

        public static int[,] AddStringsSKeyBoard(int[,] arr) {
            var matr = new int[arr.GetLength(0) + 1, arr.GetLength(1)];
            for (var i = 0; i < arr.GetLength(0) + 1; i++)
            for (var j = 0; j < arr.GetLength(1); j++)
                if (i != arr.GetLength(0)) {
                    matr[i, j] = arr[i, j];
                }
                else {
                    Console.WriteLine("Введиет элемент, стоящий на {0}-ой строки и {1}-го столбца",
                        arr.GetLength(0) + 1, j + 1);
                    matr[arr.GetLength(0), j] = ReadLib.ReadLib.ReadInt();
                }

            return matr;
        }

        public static int[,] AddStringsRandom(int[,] arr) {
            var rnd = new Random();
            Console.WriteLine("От какого числа заполнять значения в дополнительной строке массива рандомно");
            var min = ReadLib.ReadLib.ReadInt();
            Console.WriteLine("До какого числа заполнять значения в дополнительной строке массива рандомно");
            var max = ReadLib.ReadLib.ReadInt();
            var matr = new int[arr.GetLength(0) + 1, arr.GetLength(1)];
            for (var i = 0; i < arr.GetLength(0) + 1; i++)
            for (var j = 0; j < arr.GetLength(1); j++)
                if (i != arr.GetLength(0))
                    matr[i, j] = arr[i, j];
                else
                    matr[arr.GetLength(0), j] = rnd.Next(min, max + 1);
            return matr;
        }

        public static void ZapolnRaggedSKeyboard(int[][] ragged) {
            int colums;
            for (var i = 0; i < ragged.Length; i++) {
                var ok = false;
                Console.WriteLine("Введите кол-во столбцов в {0}-ой строке", i + 1);
                do {
                    colums = ReadLib.ReadLib.ReadVGran(0);
                    if (colums == 0)
                        Console.WriteLine("Ошибка ввода, повторите ввод");
                    else
                        ok = true;
                } while (!ok);

                ragged[i] = new int[colums];
                for (var j = 0; j < colums; j++) {
                    Console.WriteLine("Введиет элемент, стоящий на {0}-ой строки и {1}-го столбца", i + 1, j + 1);
                    ragged[i][j] = ReadLib.ReadLib.ReadInt();
                }
            }
        }

        public static void ZapolnRaggedRandom(int[][] ragged) {
            var rnd = new Random();
            int colums;
            bool ok1;
            int max, min;
            do {
                ok1 = true;
                Console.WriteLine("От какого числа заполнять значения массива рандомно");
                min = ReadLib.ReadLib.ReadInt();
                Console.WriteLine("До какого числа заполнять значение массива рандомно");
                max = ReadLib.ReadLib.ReadInt();
                if (min > max)
                    Console.WriteLine("Ошибка, введите первое число меньшее, чем второе\n");
                else
                    ok1 = false;
            } while (ok1);

            for (var i = 0; i < ragged.Length; i++) {
                var ok = false;
                Console.WriteLine("Введите кол-во столбцов в {0}-ой строке", i + 1);
                do {
                    colums = ReadLib.ReadLib.ReadVGran(0);
                    if (colums == 0)
                        Console.WriteLine("Ошибка ввода, повторите ввод");
                    else
                        ok = true;
                } while (!ok);

                ragged[i] = new int[colums];
                for (var j = 0; j < colums; j++)
                    ragged[i][j] = rnd.Next(min, max + 1);
            }
        }

        public static void VivodRagged(int[][] ragged) {
            for (var i = 0; i < ragged.Length; i++) {
                for (var j = 0; j < ragged[i].Length; j++)
                    Console.Write(ragged[i][j] + "  ");
                Console.WriteLine();
            }
        }

        public static bool IsNull(int[][] ragged) {
            var ok = false;
            var chet = 0;
            for (var i = 0; i < ragged.Length; i++)
            for (var j = 0; j < ragged[i].Length; j++)
                if (ragged[i][j] == 0) {
                    chet++;
                    break;
                }

            if (chet == ragged.Length)
                ok = true;
            return ok;
        }

        public static int[][] YdalStringRagged(int[][] ragged, int deletestrings) {
            var tmp = new int[ragged.Length - 1][];
            var j = 0;
            for (var i = 0; i < ragged.Length; i++)
                if (i != deletestrings)
                    tmp[j++] = ragged[i];

            return tmp;
        }

        public static int[][] DelStringSZeroRagged(int[][] ragged, out bool check, out bool ok) {
            ok = false;
            check = false;
            var i = 0;
            do {
                int j = 0, a;
                a = 0;
                do {
                    if (ragged[i][j] == 0) {
                        a = 1;
                        ragged = YdalStringRagged(ragged, i);
                        break;
                    }

                    j++;
                } while (j < ragged[i].Length);

                if (a == 0)
                    i++;
            } while (i < ragged.Length);

            check = true;
            ok = IsNull(ragged);
            return ragged;
        }

        public static void OdinMassiv() {
            Console.Clear();
            bool exit;
            Console.WriteLine("Введите размерность одномерного массива");
            var dlina = ReadLib.ReadLib.ReadVGran(0);
            if (dlina == 0) {
                Console.WriteLine("Введен пустой массив");
                Console.WriteLine("\n{0}", index[7]);
                Console.ReadLine();
                exit = true;
            }
            else {
                exit = false;
            }

            var arr = new int[dlina];
            int sw;
            while (!exit) {
                Console.Clear();
                sw = Menu(index[4], index[5], index[8], index[6]);
                Console.Clear();
                switch (sw) {
                    case 1:
                        if (arr.Length == 0) {
                            Console.WriteLine("Из массива удалили все числа");
                        }
                        else {
                            ArrayLib.OneDimationalArr.ReadArr.ReadODKeyboard(arr);
                            ArrayLib.OneDimationalArr.WriteArr.WriteOD(arr);
                            arr = YdalElPoZnach(arr);
                            if (arr.Length == dlina)
                                Console.WriteLine("Данного элемента в массиве нету, массив не изменится");
                            else if (arr.Length == 0)
                                Console.WriteLine("Из массива удалили все числа");
                            else
                                ArrayLib.OneDimationalArr.WriteArr.WriteOD(arr);
                        }

                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 2:
                        if (arr.Length == 0) {
                            Console.WriteLine("Из массива удалили все числа");
                        }
                        else {
                            ArrayLib.OneDimationalArr.ReadArr.ReadODRandom(arr);
                            ArrayLib.OneDimationalArr.WriteArr.WriteOD(arr);
                            arr = YdalElPoZnach(arr);
                            if (arr.Length == dlina)
                                Console.WriteLine("Данного элемента в массиве нету, массив не изменится");
                            else if (arr.Length == 0)
                                Console.WriteLine("Из массива удалили все числа");
                            else
                                ArrayLib.OneDimationalArr.WriteArr.WriteOD(arr);
                        }

                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 3:
                        if (arr.Length == 0) {
                            Console.WriteLine("Из массива удалили все числа");
                        }
                        else {
                            ArrayLib.OneDimationalArr.WriteArr.WriteOD(arr);
                            arr = YdalElPoZnach(arr);
                            if (arr.Length == 0)
                                Console.WriteLine("Из массива удалили все числа");
                            else
                                ArrayLib.OneDimationalArr.WriteArr.WriteOD(arr);
                        }

                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 4:
                        exit = true;
                        break;
                }
            }
        }

        public static void MatrMassiv() {
            Console.Clear();
            bool exit;
            Console.WriteLine("Введите размерность двумерного массива\n" +
                              "Ввдети кол-во строк в матрице");
            var strings = ReadLib.ReadLib.ReadVGran(0);
            Console.WriteLine("Введите кол-во столбцов в матрице");
            var colums = ReadLib.ReadLib.ReadVGran(0);
            if (strings == 0 || colums == 0) {
                Console.WriteLine("Введен пустой массив");
                Console.WriteLine("\n{0}", index[7]);
                Console.ReadLine();
                exit = true;
            }
            else {
                exit = false;
            }

            var matr = new int[strings, colums];
            int sw;
            while (!exit) {
                Console.Clear();
                sw = Menu(index[4], index[5], index[6]);
                Console.Clear();
                switch (sw) {
                    case 1:
                        ZapolnMatrSKeyboard(matr);
                        VivodMatr(matr);
                        var exit1 = false;
                        int sw1;
                        while (!exit1) {
                            Console.Clear();
                            sw1 = Menu(index[9], index[10], index[6]);
                            Console.Clear();
                            switch (sw1) {
                                case 1:
                                    matr = AddStringsSKeyBoard(matr);
                                    VivodMatr(matr);
                                    Console.WriteLine("Нажмите Enter, чтобы перейти к следующему шагу");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    matr = AddStringsRandom(matr);
                                    VivodMatr(matr);
                                    Console.WriteLine("Нажмите Enter, чтобы перейти к следующему шагу");
                                    Console.ReadLine();
                                    break;
                                case 3:
                                    exit1 = true;
                                    exit = true;
                                    break;
                            }
                        }

                        VivodMatr(matr);
                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 2:
                        ZapolnMatrRandom(matr);
                        VivodMatr(matr);
                        Console.WriteLine("Нажмите Enter, чтобы перейти к следующему шагу");
                        Console.ReadLine();
                        int sw2;
                        var exit2 = false;
                        while (!exit2) {
                            Console.Clear();
                            sw2 = Menu(index[9], index[10], index[6]);
                            Console.Clear();
                            switch (sw2) {
                                case 1:

                                    matr = AddStringsSKeyBoard(matr);
                                    VivodMatr(matr);
                                    Console.WriteLine("Нажмите Enter, чтобы перейти к следующему шагу");
                                    Console.ReadLine();
                                    break;
                                case 2:

                                    matr = AddStringsRandom(matr);
                                    VivodMatr(matr);
                                    Console.WriteLine("Нажмите Enter, чтобы перейти к следующему шагу");
                                    Console.ReadLine();
                                    break;
                                case 3:
                                    exit2 = true;
                                    exit = true;
                                    break;
                            }
                        }

                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 3:
                        exit = true;
                        break;
                }
            }
        }

        public static void RaggedMassiv() {
            Console.Clear();
            bool exit;
            Console.WriteLine("Введите размерность рваного массива\n" +
                              "Ввдети кол-во строк в массиве");
            var strings = ReadLib.ReadLib.ReadVGran(0);
            if (strings == 0) {
                Console.WriteLine("Введен пустой массив");
                Console.WriteLine("\n{0}", index[7]);
                Console.ReadLine();
                exit = true;
            }
            else {
                exit = false;
            }

            var ragged = new int[strings][];
            int sw;
            var check = false;
            while (!exit) {
                Console.Clear();
                sw = Menu(index[4], index[5], index[6]);
                Console.Clear();
                switch (sw) {
                    case 1:
                        if (check == false) {
                            ZapolnRaggedSKeyboard(ragged);
                            VivodRagged(ragged);
                            Console.WriteLine(
                                "Нажмите любую клавишу, чтобы удалить строки массива, в которых есть элемент 0");
                            Console.ReadLine();
                            bool ok;
                            ragged = DelStringSZeroRagged(ragged, out check, out ok);
                            if (ok)
                                Console.WriteLine("После удаления строк с нулевыми элементами массив опустел");
                            else
                                VivodRagged(ragged);
                        }
                        else {
                            Console.WriteLine("Действие над данным массивом уже произведены");
                        }

                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 2:
                        if (check == false) {
                            ZapolnRaggedRandom(ragged);
                            VivodRagged(ragged);
                            Console.WriteLine(
                                "Нажмите любую клавишу, чтобы удалить строки массива, в которых есть элемент 0");
                            Console.ReadLine();
                            bool ok;
                            ragged = DelStringSZeroRagged(ragged, out check, out ok);
                            if (ok)
                                Console.WriteLine("После удаления строк с нулевыми элементами массив опустел");
                            else
                                VivodRagged(ragged);
                        }
                        else {
                            Console.WriteLine("Действие над данным массивом уже произведены");
                        }

                        Console.WriteLine("\n{0}", index[7]);
                        Console.ReadLine();
                        break;
                    case 3:
                        exit = true;
                        break;
                }
            }
        }

        private static void Main() {
            var exit = false;
            while (!exit) {
                Console.Clear();
                switch (Menu(index[0], index[1], index[2], index[3])) {
                    case 1:
                        OdinMassiv();
                        break;
                    case 2:
                        MatrMassiv();
                        break;
                    case 3:
                        RaggedMassiv();
                        break;
                    case 4:
                        exit = true;
                        Console.Clear();
                        break;
                }
            }
        }
    }
}