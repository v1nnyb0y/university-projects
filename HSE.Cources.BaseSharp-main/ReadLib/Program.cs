using System;

namespace ReadLib
{
    public class ReadLib
    {
        /// <summary>
        ///     Перевод строки в вещественное число
        /// </summary>
        /// <param return="Веществененное число"></param>
        public static double ReadDouble() {
            bool ok;
            double chislo;
            do {
                var buf = Console.ReadLine();
                ok = double.TryParse(buf, out chislo);
                if (!ok) Console.WriteLine("Ошибка ввода, введено не вещественное число");
            } while (!ok);

            return chislo;
        }

        /// <summary>
        ///     Перевод строки в целое число
        /// </summary>
        /// <param return="Целое число"></param>
        public static int ReadInt() {
            bool ok;
            int chislo;
            do {
                var buf = Console.ReadLine();
                ok = int.TryParse(buf, out chislo);
                if (!ok) Console.WriteLine("Ошибка ввода, введено не целое число");
            } while (!ok);

            return chislo;
        }

        /// <summary>
        ///     Перевод строки в целое число
        /// </summary>
        /// <param return="Вещественное число"></param>
        public static float ReadFloat() {
            bool ok;
            float chislo;
            do {
                var buf = Console.ReadLine();
                ok = float.TryParse(buf, out chislo);
                if (!ok) Console.WriteLine("Ошибка ввода, введено не вещественное число");
            } while (!ok);

            return chislo;
        }

        /// <summary>
        ///     Проверка на границы int от числа min до числа max
        /// </summary>
        /// <returns></returns>
        public static int ReadVGran(int min, int max) {
            int chislo;
            do {
                chislo = ReadInt();
                if (chislo < min || chislo > max)
                    Console.WriteLine("Ошибка ввода, повторите ввод в числа. {0}<= число <={1}", min, max);
            } while (chislo < min || chislo > max);

            return chislo;
        }

        /// <summary>
        ///     Проверка на границу int от числа min
        /// </summary>
        /// <param Минимум="min"></param>
        /// <returns></returns>
        public static int ReadVGran(int min) {
            int chislo;
            do {
                chislo = ReadInt();
                if (chislo < min) Console.WriteLine("Ошибка ввода, повторите ввод в числа. {0}<=", min);
            } while (chislo < min);

            return chislo;
        }

        /// <summary>
        ///     Проверка на символ
        /// </summary>
        /// <returns></returns>
        public static char ReadChar() {
            bool ok;
            char symbol;
            do {
                var buf = Console.ReadLine();
                ok = char.TryParse(buf, out symbol);
                if (!ok || !(symbol >= 'а' && symbol <= 'я' || symbol >= 'А' && symbol <= 'Я')) {
                    ok = false;
                    Console.WriteLine("Ошибка ввода, введен не символ");
                }
            } while (!ok);

            return symbol;
        }

        /// <summary>
        ///     Проверка на границу char от символа min
        /// </summary>
        /// <param Символ минимум="min"></param>
        /// <returns></returns>
        public static char ReadVGran(char min) {
            char symbol;
            do {
                symbol = ReadChar();
                if (symbol < min || !(symbol >= 'а' && symbol <= 'я' || symbol >= 'А' && symbol <= 'Я'))
                    Console.WriteLine("Ошибка ввода, повторите ввод в символа. После {0} символа в русском алфавите",
                        min);
            } while (symbol < min);

            return symbol;
        }

        /// <summary>
        ///     Проверка на границу char от символа min до символа max
        /// </summary>
        /// <param Символ минимум="min"></param>
        /// <param Символ максимум="max"></param>
        /// <returns></returns>
        public static char ReadVGran(char min, char max) {
            char symbol;
            do {
                symbol = ReadChar();
                if (symbol < min || symbol > max || !(symbol >= 'а' && symbol <= 'я' || symbol >= 'А' && symbol <= 'Я'))
                    Console.WriteLine(
                        "Ошибка ввода, повторите ввод в символа. После {0} символа в русском алфавите и до {1} символа",
                        min);
            } while (symbol < min || symbol > max);

            return symbol;
        }
    }

    public class ArrayLib
    {
        public class OneDimationalArr
        {
            public class ReadArr
            {
                /// <summary>
                ///     Заполнение одномерного массива от min до max
                /// </summary>
                /// <param Массив="arr"></param>
                public static void ReadODRandom(int[] arr) {
                    var rnd = new Random();
                    Console.WriteLine("От какого числа заполнять значения массива рандомно");
                    var min = ReadLib.ReadInt();
                    Console.WriteLine("До какого числа заполнять значение массива рандомно");
                    var max = ReadLib.ReadVGran(min);
                    for (var i = 0; i < arr.Length; i++)
                        arr[i] = rnd.Next(min, max + 1);
                }

                /// <summary>
                ///     Заполнение одномерного массива с клавиатуры
                /// </summary>
                /// <param Массив="arr"></param>
                public static void ReadODKeyboard(int[] arr) {
                    for (var i = 0; i < arr.Length; i++) {
                        Console.WriteLine("Введите {0}-ый элемент", i + 1);
                        arr[i] = ReadLib.ReadInt();
                    }
                }
            }

            public class WriteArr
            {
                /// <summary>
                ///     Вывод одномерного массива
                /// </summary>
                /// <param Массив="arr"></param>
                public static void WriteOD(int[] arr) {
                    for (var i = 0; i < arr.Length; i++)
                        Console.Write(arr[i] + " ");
                }
            }

            public class DeleteArr
            {
                /// <summary>
                ///     Удаление элемента с заданным номером
                /// </summary>
                /// <param Массив="arr"></param>
                /// <param Значение элемента="znach"></param>
                /// <returns></returns>
                public static int[] DeleteElem(int[] arr, int znach) {
                    for (var i = znach; i < arr.Length; i++)
                        arr[i] = arr[i + 1];
                    Array.Resize(ref arr, arr.Length - 1);
                    return arr;
                }

                /// <summary>
                ///     Удаление элементов по значению из одномерного массива
                /// </summary>
                /// <param Массив="arr"></param>
                /// <param Значение элемента="chislo"></param>
                /// <returns></returns>
                public static int[] YdalElPoZnach(int[] arr, int chislo) {
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

                /// <summary>
                ///     Удаление элемента по значению
                /// </summary>
                /// <param Массив="arr"></param>
                /// <param Элемент="value"></param>
                /// <returns></returns>
                public static string[] YdalElPoZnach(string[] arr, string value) {
                    var i = 0;
                    do {
                        if (arr[i].Contains(value)) {
                            var tmp = arr[i];
                            for (var j = i; j < arr.Length - 1; j++)
                                arr[j] = arr[j + 1];
                            arr[arr.Length - 1] = tmp;
                            Array.Resize(ref arr, arr.Length - 1);
                            break;
                        }

                        i++;
                    } while (i < arr.Length);

                    return arr;
                }
            }

            public class AddArr
            {
                /// <summary>
                ///     Вставка элементов одномерного массива от min до max
                /// </summary>
                /// <param Первый элемент="firstznach"></param>
                /// <param Сколько элементов="kolvoznach"></param>
                /// <param Массив="arr"></param>
                /// <returns></returns>
                public static int[] AddElRandom(int firstznach, int kolvoznach, int[] arr) {
                    var arr_tmp = new int[arr.Length + kolvoznach];
                    var rnd = new Random();
                    Console.WriteLine(
                        "Введите от какого числа будут заполняться вставляемые элементы одномерного массива");
                    var min = ReadLib.ReadInt();
                    Console.WriteLine(
                        "Введите до какого числа будут заполняться вставляемые элементы одномерного массива");
                    var max = ReadLib.ReadVGran(min);
                    for (var i = 0; i < firstznach - 1; i++)
                        arr_tmp[i] = arr[i];
                    for (var i = firstznach - 1; i < kolvoznach + firstznach - 1; i++)
                        arr_tmp[i] = rnd.Next(min, max + 1);
                    for (var i = firstznach + kolvoznach - 1; i < arr.Length + kolvoznach; i++)
                        arr_tmp[i] = arr[i - kolvoznach];
                    return arr_tmp;
                }

                /// <summary>
                ///     Вставка элементов одномерного массива с клавиатуры
                /// </summary>
                /// <param Первый элемент="firstznach"></param>
                /// <param Сколько элементов="kolvoznach"></param>
                /// <param Массив="arr"></param>
                /// <returns></returns>
                public static int[] AddElKeyboard(int firstznach, int kolvoznach, int[] arr) {
                    var k = 1;
                    var arr_tmp = new int[arr.Length + kolvoznach];
                    for (var i = 0; i < firstznach - 1; i++)
                        arr_tmp[i] = arr[i];
                    for (var i = firstznach - 1; i < kolvoznach + firstznach - 1; i++) {
                        Console.WriteLine("Введите элемент {0} (вставляемый элемент)", k++);
                        arr_tmp[i] = ReadLib.ReadInt();
                    }

                    ;
                    for (var i = firstznach + kolvoznach - 1; i < arr.Length + kolvoznach; i++)
                        arr_tmp[i] = arr[i - kolvoznach];
                    return arr_tmp;
                }
            }

            public class WorkArr
            {
                /// <summary>
                ///     Циклический сдвиг элементов массива влево
                /// </summary>
                /// <param Кол-во сдвигов="kolvotimes"></param>
                /// <param Массив="arr"></param>
                /// <returns></returns>
                public static int[] MoveNTimesLeft(int kolvotimes, int[] arr) {
                    for (var i = 0; i < kolvotimes; i++) {
                        var tmp = arr[0];
                        for (var j = 1; j < arr.Length; j++)
                            arr[j - 1] = arr[j];
                        arr[arr.Length - 1] = tmp;
                    }

                    return arr;
                }

                /// <summary>
                ///     Циклический сдвиг элементов массива вправо
                /// </summary>
                /// <param Кол-во сдвигов="kolvotimes"></param>
                /// <param Массив="arr"></param>
                /// <returns></returns>
                public static int[] MoveNTimesRight(int kolvotimes, int[] arr) {
                    for (var i = 0; i < kolvotimes; i++) {
                        var tmp = arr[arr.Length - 1];
                        for (var j = arr.Length - 1; j > 0; j++)
                            arr[j] = arr[j - 1];
                        arr[0] = tmp;
                    }

                    return arr;
                }

                /// <summary>
                ///     Поиск элемента в массиве
                /// </summary>
                /// <param Массив="arr"></param>
                /// <param Элемент="el"></param>
                /// <param Кол-во сравнений="k"></param>
                public static bool FindEl(int[] arr, int el) {
                    SortArr.SortShell(arr);
                    int left = 0, right = arr.Length - 1, sred;
                    do {
                        sred = (right + left) / 2;
                        if (arr[sred] < el)
                            left = sred + 1;
                        else
                            right = sred;
                    } while (left != right);

                    if (arr[left] == el)
                        return true;
                    return false;
                }
            }

            public class SortArr
            {
                /// <summary>
                ///     Сортировка простым включением
                /// </summary>
                /// <param Массив="arr"></param>
                public static void SortEasyVkl(int[] arr) {
                    int j, el;
                    for (var i = 1; i < arr.Length; i++) {
                        el = arr[i];
                        j = i - 1;
                        while (j >= 0 && el < arr[j]) {
                            arr[j + 1] = arr[j];
                            j--;
                        }

                        ;
                        arr[j + 1] = el;
                    }
                }

                /// <summary>
                ///     Сортировка методом Шелла
                /// </summary>
                /// <param Массив="arr"></param>
                public static void SortShell(int[] arr) {
                    var t = 4;
                    int[] step = {9, 5, 3, 1};
                    int j;
                    for (var m = 0; m < t; m++) {
                        var k = step[m];
                        for (var i = k; i < arr.Length; i += k) {
                            var buf = arr[i];
                            j = i - k;
                            while (j >= 0 && buf < arr[j]) {
                                arr[j + k] = arr[j];
                                j -= k;
                            }

                            arr[j + k] = buf;
                        }
                    }
                }

                /// <summary>
                ///     Сортировка Хоара
                /// </summary>
                /// <param Массив="arr"></param>
                /// <param Левая граница="left"></param>
                /// <param Правая граница="right"></param>
                public static void SortHoara(int[] arr, int left, int right) {
                    int i = left, j = right;
                    var buf = arr[(left + right) / 2];
                    do {
                        while (arr[i] < buf)
                            i++;
                        while (arr[j] > buf)
                            j++;
                        if (i <= j)
                            Action.Swap(ref arr[i], ref arr[j]);
                    } while (i <= j);

                    if (left < j)
                        SortHoara(arr, left, j);
                    if (i < right)
                        SortHoara(arr, i, right);
                }
            }
        }

        public class MatrArr
        {
            public class ReadMatr
            {
                /// <summary>
                ///     Заполнение матрицы от min до max
                /// </summary>
                /// <param Массив="matr"></param>
                private static void ReadMatrRandom(int[,] matr) {
                    var rnd = new Random();
                    Console.WriteLine("От какого числа заполнять значения массива рандомно");
                    var min = ReadLib.ReadInt();
                    Console.WriteLine("До какого числа заполнять значение массива рандомно");
                    var max = ReadLib.ReadVGran(min);
                    for (var i = 0; i < matr.GetLength(0); i++)
                    for (var j = 0; j < matr.GetLength(1); j++)
                        matr[i, j] = rnd.Next(min, max + 1);
                }

                /// <summary>
                ///     Заполнение матрицы с клавиатуры
                /// </summary>
                /// <param Массив="matr"></param>
                private static void ReadMatrKeyboard(int[,] matr) {
                    for (var i = 0; i < matr.GetLength(0); i++)
                    for (var j = 0; j < matr.GetLength(1); j++) {
                        Console.WriteLine("Введиет элемент, стоящий на {0}-ой строки и {1}-го столбца", i + 1,
                            j + 1);
                        matr[i, j] = ReadLib.ReadInt();
                    }
                }
            }

            public class WriteMatr
            {
                /// <summary>
                ///     Вывод массива
                /// </summary>
                /// <param Массив="matr"></param>
                private static void WriteMatrix(int[,] matr) {
                    for (var i = 0; i < matr.GetLength(0); i++) {
                        for (var j = 0; j < matr.GetLength(1); j++)
                            Console.Write("{0}\t", matr[i, j]);
                        Console.WriteLine();
                    }
                }
            }

            public class AddMatr
            {
                /// <summary>
                ///     Добавление строки в конец матрицы c клавиатуры
                /// </summary>
                /// <param Массив="matr"></param>
                /// <returns></returns>
                public static int[,] AddStringsKeyboard(int[,] matr) {
                    var matr_tmp = new int[matr.GetLength(0) + 1, matr.GetLength(1)];
                    for (var i = 0; i < matr.GetLength(0) + 1; i++)
                    for (var j = 0; j < matr.GetLength(1); j++) {
                        if (i != matr.GetLength(0)) {
                            matr_tmp[i, j] = matr[i, j];
                        }
                        else {
                            Console.WriteLine("Введиет элемент, стоящий на {0}-ой строки и {1}-го столбца",
                                matr.GetLength(0) + 1, j + 1);
                            matr_tmp[matr.GetLength(0), j] = ReadLib.ReadInt();
                        }

                        ;
                    }

                    return matr_tmp;
                }

                /// <summary>
                ///     Добавление строки в конец матрицы от min до max
                /// </summary>
                /// <param Массив="matr"></param>
                /// <returns></returns>
                public static int[,] AddStringsRandom(int[,] matr) {
                    var rnd = new Random();
                    Console.WriteLine("От какого числа заполнять значения в дополнительной строке массива рандомно");
                    var min = ReadLib.ReadInt();
                    Console.WriteLine("До какого числа заполнять значения в дополнительной строке массива рандомно");
                    var max = ReadLib.ReadVGran(min);
                    var matr_tmp = new int[matr.GetLength(0) + 1, matr.GetLength(1)];
                    for (var i = 0; i < matr.GetLength(0) + 1; i++)
                    for (var j = 0; j < matr.GetLength(1); j++)
                        if (i != matr.GetLength(0))
                            matr_tmp[i, j] = matr[i, j];
                        else
                            matr_tmp[matr.GetLength(0), j] = rnd.Next(min, max + 1);
                    return matr_tmp;
                }
            }
        }

        public class RaggedArr
        {
            public class ReadRagged
            {
                /// <summary>
                ///     Заполнение рваного массива с клавиатуры
                /// </summary>
                /// <param Массив="ragged"></param>
                public static void ReadRaggedKeyboard(char[][] ragged) {
                    {
                        int colums;
                        for (var i = 0; i < ragged.Length; i++) {
                            var ok = false;
                            Console.WriteLine("Введите кол-во столбцов в {0}-ой строке", i + 1);
                            do {
                                colums = ReadLib.ReadVGran(0);
                                if (colums == 0)
                                    Console.WriteLine("Ошибка ввода, повторите ввод");
                                else
                                    ok = true;
                            } while (!ok);

                            ragged[i] = new char[colums];
                            for (var j = 0; j < colums; j++) {
                                Console.WriteLine("Введиет символ, стоящий на {0}-ой строки и {1}-го столбца", i + 1,
                                    j + 1);
                                do {
                                    ragged[i][j] = ReadLib.ReadChar();
                                } while (!(ragged[i][j] >= 'а' && ragged[i][j] <= 'я' ||
                                           ragged[i][j] >= 'А' && ragged[i][j] <= 'Я'));
                            }
                        }
                    }
                }

                /// <summary>
                ///     Заполнение рваного массива от min до max
                /// </summary>
                /// <param Массив="ragged"></param>
                public static void ReadRaggedRandom(char[][] ragged) {
                    var rnd = new Random();
                    int colums;
                    Console.WriteLine("От какого символа русского алфавита заполнять массив");
                    var min = ReadLib.ReadChar();
                    Console.WriteLine("До какого символа[не включая его] русского алфовита заполнять массив");
                    var max = ReadLib.ReadVGran(min);
                    for (var i = 0; i < ragged.Length; i++) {
                        var ok = false;
                        Console.WriteLine("Введите кол-во столбцов в {0}-ой строке", i + 1);
                        do {
                            colums = ReadLib.ReadVGran(0);
                            if (colums == 0)
                                Console.WriteLine("Ошибка ввода, повторите ввод");
                            else
                                ok = true;
                        } while (!ok);

                        ragged[i] = new char[colums];
                        for (var j = 0; j < colums; j++)
                            do {
                                ragged[i][j] = Convert.ToChar(rnd.Next(min, max));
                            } while (!(ragged[i][j] >= 'а' && ragged[i][j] <= 'я' ||
                                       ragged[i][j] >= 'А' && ragged[i][j] <= 'Я'));
                    }
                }

                /// <summary>
                ///     Заполнение рваного массива с клавиатуры
                /// </summary>
                /// <param Массив="ragged"></param>
                public static void ReadRaggedKeyboard(int[][] ragged) {
                    int colums;
                    for (var i = 0; i < ragged.Length; i++) {
                        var ok = false;
                        Console.WriteLine("Введите кол-во столбцов в {0}-ой строке", i + 1);
                        do {
                            colums = ReadLib.ReadVGran(0);
                            if (colums == 0)
                                Console.WriteLine("Ошибка ввода, повторите ввод");
                            else
                                ok = true;
                        } while (!ok);

                        ragged[i] = new int[colums];
                        for (var j = 0; j < colums; j++) {
                            Console.WriteLine("Введиет элемент, стоящий на {0}-ой строки и {1}-го столбца", i + 1,
                                j + 1);
                            ragged[i][j] = ReadLib.ReadInt();
                        }
                    }
                }

                /// <summary>
                ///     Заполнение рваного массива от min до max
                /// </summary>
                /// <param Массив="ragged"></param>
                public static void ReadRaggedRandom(int[][] ragged) {
                    var rnd = new Random();
                    int colums;
                    int max, min;
                    Console.WriteLine("От какого числа заполнять значения массива рандомно");
                    min = ReadLib.ReadInt();
                    Console.WriteLine("До какого числа заполнять значение массива рандомно");
                    max = ReadLib.ReadVGran(min);
                    for (var i = 0; i < ragged.Length; i++) {
                        var ok = false;
                        Console.WriteLine("Введите кол-во столбцов в {0}-ой строке", i + 1);
                        do {
                            colums = ReadLib.ReadVGran(0);
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
            }

            public class WriteRagged
            {
                /// <summary>
                ///     Вывод рваного массива
                /// </summary>
                /// <param Массив="ragged"></param>
                public static void WriteTorn(int[][] ragged) {
                    for (var i = 0; i < ragged.Length; i++) {
                        for (var j = 0; j < ragged[i].Length; j++)
                            Console.Write(ragged[i][j] + "  ");
                        Console.WriteLine();
                    }
                }

                /// <summary>
                ///     Вывод рваного массива
                /// </summary>
                /// <param Массив="ragged"></param>
                public static void WriteTorn(char[][] ragged) {
                    for (var i = 0; i < ragged.Length; i++) {
                        for (var j = 0; j < ragged[i].Length; j++)
                            Console.Write(ragged[i][j] + "  ");
                        Console.WriteLine();
                    }
                }
            }

            public class DeleteRagged
            {
                /// <summary>
                ///     Удалени строки в рваном массиве
                /// </summary>
                /// <param Массив="ragged"></param>
                /// <param Номер строки="deletestrings"></param>
                /// <returns></returns>
                public static int[][] DeleteStringRagged(int[][] ragged, int deletestrings) {
                    var tmp = new int[ragged.Length - 1][];
                    var j = 0;
                    for (var i = 0; i < ragged.Length; i++)
                        if (i != deletestrings)
                            tmp[j++] = ragged[i];

                    return tmp;
                }

                /// <summary>
                ///     Удаление строки в рваном массиве
                /// </summary>
                /// <param Массив="ragged"></param>
                /// <param Номер строки="deletestrings"></param>
                /// <returns></returns>
                public static char[][] DeleteStringRagged(char[][] ragged, int deletestrings) {
                    var tmp = new char[ragged.Length - 1][];
                    var j = 0;
                    for (var i = 0; i < ragged.Length; i++)
                        if (i != deletestrings)
                            tmp[j++] = ragged[i];

                    return tmp;
                }
            }
        }
    }

    public class Print
    {
        /// <summary>
        ///     Печать меню
        /// </summary>
        /// <param Массив элементов="pechat"></param>
        /// <returns></returns>
        public static int Menu(int k, params string[] pechat) {
            Console.Clear();
            Console.CursorVisible = false;
            var y = 1;
            int tek = 0, tekold = 0;
            var x = 1;
            var ok = false;
            for (var i = 0; i < pechat.Length; i++) {
                Console.SetCursorPosition(x, y + i);
                if (i % (k + 1) == 0) {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(pechat[i]);
            }

            ;
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
                        tek += k + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        tek -= k + 1;
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
            Console.Clear();
            return tek + 1;
        }
    }

    public class Action
    {
        /// <summary>
        ///     Смена мест
        /// </summary>
        /// <param Первая переменная="a"></param>
        /// <param Вторая переменная="b"></param>
        public static void Swap<T>(ref T a, ref T b) {
            var tmp = a;
            a = b;
            b = tmp;
        }


        public class Movement
        {
            /// <summary>
            ///     Факториал числа
            /// </summary>
            /// <param Число="chislo"></param>
            /// <returns></returns>
            public static int Fact(int chislo) {
                if (chislo == 0)
                    return 1;
                return chislo * Fact(chislo - 1);
            }
        }
    }
}