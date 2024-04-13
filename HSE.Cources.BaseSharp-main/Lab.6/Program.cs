using System;
using System.Text.RegularExpressions;
using ReadLib;

namespace Lab._6
{
    internal class Program
    {
        private static readonly string[] index =
            {
                "Выход", "Заполнение рваного массива", "Заполнение ДСC", "Заполнение с клавиатуры",
                "Задание 1. Удаление первой строки с >=3 гласных букв",
                "Задание 2. Работа с идентификаторами", "Печать массива", "Удаление строки",
                "Нажмите Enter для продолжения...", "Работа с первым фиксированным текстом",
                "Работа со вторым фиксированным текстом", "Задать массив самомму и работать с ним",
                "Дополнительное задание. Работа с регулярными выражениями"
            };

        private static readonly string str =
            "Привет,как дела._Все нормально,ответил 1я.Таков _б1ыл наш___23 раз!!!говор???";

        private static readonly string str_1 =
            "Пр%%ивет,к%5ак дел^#2а._Вс124$е нормаль***65но отве%3???т%ил 1>>я.Т<<аков _б1|||ыл н$аш___23 р%аз!!!го%вор???";

        public static int Menu(bool exit, params string[] pechat) {
            Console.Clear();
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
                        if (exit)
                            tek += 3;
                        else
                            tek++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (exit)
                            tek -= 3;
                        else
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
            Console.Clear();
            return tek + 1;
        }

        #region extratask

        public static void ExtraTaskFunction() {
            bool ok;
            Console.WriteLine("Введите строку, чтобы проверить, является ли она URL адресом");
            var str = Console.ReadLine();
            if (str == "" || str == " ") {
                Console.WriteLine("Строка пуста");
            }
            else {
                ok = Regex.IsMatch(str,
                    @"(^(ht|f)tp(s?)\:\/\/)?([0-9a-zA-Z]{2}([-.\w]*[0-9a-zA-Z])?){1}\.[a-zA-Z]{2,3}(:\d*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%=\$#_]*)?$");
                Console.WriteLine(ok);
            }

            Console.WriteLine("\n{0}", index[8]);
            Console.ReadLine();
        }

        #endregion

        private static void Main() {
            var exit = false;
            while (!exit) {
                var sw = Print.Menu(0, index[4], index[5], index[12], index[0]);
                switch (sw) {
                    case 1:
                        DeleteString();
                        break;
                    case 2:
                        WriteIdentifier();
                        break;
                    case 3:
                        ExtraTaskFunction();
                        break;
                    case 4:
                        exit = true;
                        break;
                }
            }

            Console.WriteLine("Для закрытия нажмите Enter");
        }

        #region firsttask

        public static void MemoryAllocation(out int strings, out bool exit) {
            exit = false;
            Console.WriteLine("Введите размерность рваного массива\n" +
                              "Ввдети кол-во строк в массиве");
            strings = ReadLib.ReadLib.ReadVGran(0);
            if (strings == 0) {
                Console.WriteLine("Введен пустой массив");
                Console.WriteLine("\n{0}", index[8]);
                Console.ReadLine();
                exit = true;
            }
        }

        public static char[][] ReadRaggedForFirstTask(char[][] ragged) {
            var sw = Print.Menu(0, index[2], index[3]);
            switch (sw) {
                case 1:
                    ArrayLib.RaggedArr.ReadRagged.ReadRaggedRandom(ragged);
                    break;
                case 2:
                    ArrayLib.RaggedArr.ReadRagged.ReadRaggedKeyboard(ragged);
                    break;
            }

            return ragged;
        }

        public static int NumberVowels(char[][] ragged, int strings) {
            var vowelsCount = 0;
            char[] alhabet = {'у', 'е', 'э', 'о', 'а', 'ы', 'я', 'и', 'ю', 'У', 'Е', 'Э', 'О', 'А', 'Ы', 'Я', 'И', 'Ю'};
            for (var i = 0; i < ragged[strings].Length; i++)
                foreach (var cha in alhabet)
                    if (ragged[strings][i] == cha)
                        vowelsCount++;
            return vowelsCount;
        }

        public static char[][] DeleteStringRagged(char[][] ragged) {
            for (var i = 0; i < ragged.Length; i++) {
                var vowelsCount = NumberVowels(ragged, i);
                if (vowelsCount >= 3) {
                    ragged = ArrayLib.RaggedArr.DeleteRagged.DeleteStringRagged(ragged, i);
                    break;
                }
            }

            return ragged;
        }

        private static void DeleteStringWithThreeVowels(int strings, ref char[][] ragged) {
            if (ragged.Length == 0) {
                Console.WriteLine("Массив пуст");
            }
            else {
                ragged = DeleteStringRagged(ragged);
                if (ragged.Length == strings) {
                    Console.WriteLine("В массиве не было строк с 3 и более гласными");
                    ArrayLib.RaggedArr.WriteRagged.WriteTorn(ragged);
                }
                else if (ragged.Length == 0) {
                    Console.WriteLine("После удаление строки с 3 и более гласными массив опустел");
                }
                else {
                    ArrayLib.RaggedArr.WriteRagged.WriteTorn(ragged);
                }
            }

            Console.WriteLine("\n{0}", index[8]);
            Console.ReadLine();
        }

        public static void DeleteString() {
            bool exit, check = true;
            int strings;
            MemoryAllocation(out strings, out exit);
            var ragged = new char[strings][];
            while (!exit) {
                var sw = Menu(check, index[1], index[7], index[6], index[0]);
                switch (sw) {
                    case 1:
                        ragged = ReadRaggedForFirstTask(ragged);
                        check = false;
                        Console.WriteLine();
                        ArrayLib.RaggedArr.WriteRagged.WriteTorn(ragged);
                        Console.WriteLine("\n{0}", index[8]);
                        Console.ReadLine();
                        break;
                    case 2:
                        DeleteStringWithThreeVowels(strings, ref ragged);
                        break;
                    case 3:
                        if (ragged.Length == 0)
                            Console.WriteLine("Массив пуст");
                        else
                            ArrayLib.RaggedArr.WriteRagged.WriteTorn(ragged);
                        Console.WriteLine("\n{0}", index[8]);
                        Console.ReadLine();
                        break;
                    case 4:
                        exit = true;
                        break;
                }
            }
        }

        #endregion

        #region secondtask

        public static void WorkWithString(string str) {
            var max = 0;
            var seperators = "!?.,:; ".ToCharArray();
            var arrTmp = str.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            var tmp = new string[arrTmp.Length];
            var j = 0;
            for (var i = 0; i < arrTmp.Length; i++)
                if (Regex.IsMatch(arrTmp[i], @"^[а-яА-Я_]\w*$")) {
                    tmp[j] = arrTmp[i];
                    j++;
                    if (arrTmp[i].Length > max)
                        max = arrTmp[i].Length;
                }

            if (max == 0) {
                Console.WriteLine("В строке нет идентификаторов");
            }
            else {
                str = null;
                for (var i = 0; i < tmp.Length; i++)
                    if (tmp[i] != null)
                        if (tmp[i].Length == max)
                            str = string.Concat(str, " " + tmp[i]);
                Console.WriteLine("Самый длинный идентификатор(-ы): " + str);
            }
        }

        public static void WriteIdentifier() {
            var exit = false;
            while (!exit) {
                var sw = Menu(false, index[9], index[10], index[11], index[0]);
                switch (sw) {
                    case 1:
                        Console.WriteLine("Cтрока с которой будем работать:\n{0}", str);
                        WorkWithString(str);
                        Console.WriteLine("\n{0}", index[8]);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Cтрока с которой будем работать:\n{0}", str_1);
                        WorkWithString(str_1);
                        Console.WriteLine("\n{0}", index[8]);
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Введите строку, с которой хотите работать.");
                        var str2 = Console.ReadLine();
                        if (str2 == "" || str2 == " ")
                            Console.WriteLine("Введена пустая строка");
                        else
                            WorkWithString(str2);
                        Console.WriteLine("\n{0}", index[8]);
                        Console.ReadLine();
                        break;
                    case 4:
                        exit = true;
                        break;
                }
            }
        }

        #endregion
    }
}