using System;
using System.Text.RegularExpressions;
using ReadLib;

namespace Lab._8
{
    public class Person
    {
        public string Name, SeName, FatherName, Account, SumMoney;

        public Person(string seName, string name, string fatherName, string bankAccount, string money) {
            Name = name;
            SeName = seName;
            FatherName = fatherName;
            Account = bankAccount;
            SumMoney = money;
        }

        public override string ToString() {
            return SeName + " " + Name + " " + FatherName + " " + Account + " " + SumMoney;
        }
    }

    public class LPoint
    {
        public readonly string Value;
        public int Key;

        public LPoint(string s) {
            Value = s;
            Key = GetHashCode();
        }

        public override string ToString() {
            return Key + ": " + Value;
        }

        public sealed override int GetHashCode() {
            var code = 0;
            foreach (var c in Value)
                code += c;
            return code;
        }
    }

    public class HTable
    {
        public int Size;
        public LPoint[] Table;

        public HTable(int size) {
            Size = size;
            Table = new LPoint[Size];
        }

        public bool Add(string key, string s, out int count, out bool flag, out int size) {
            flag = true;
            var keyIndex = new LPoint(key);
            var point = new LPoint(s);
            size = Table.Length;
            count = 0;
            if (s == null)
                return false;
            var index = keyIndex.GetHashCode() % Size;
            if (Table[index] == null) {
                Table[index] = point;
            }
            else {
                count++;
                var firstIndex = index;
                while (Table[index] != null) {
                    index++;
                    if (index >= Table.Length)
                        index = 0;
                    if (index == firstIndex) {
                        size = Table.Length + 1;
                        flag = false;
                        break;
                    }
                }

                Table[index] = point;
            }

            return true;
        }

        public void PrintHt() {
            for (var i = 0; i < Table.Length; i++)
                if (Table[i] != null) {
                    var point = Table[i];
                    Console.WriteLine(point.ToString());
                }
        }

        public LPoint SearchPerson(string searchedPerson) {
            var point = new LPoint(searchedPerson);
            var index = point.GetHashCode() % Size;
            var firstIndex = index;
            while (Table[index] != null)
                if (Table[index].ToString().Contains(searchedPerson)) {
                    return Table[index];
                }
                else {
                    index++;
                    if (index >= Table.Length)
                        index = 0;
                    if (index == firstIndex) break;
                }

            return null;
        }

        public void FillingHashTable(string[] key, string[] array, int size, out int count) {
            Table = new LPoint[size];
            count = 0;
            var flag = true;
            for (var i = 0; i < array.Length; i++) {
                Add(key[i], array[i], out var s, out flag, out size);
                if (!flag)
                    break;
                count += s;
            }

            if (!flag)
                FillingHashTable(key, array, size, out count);
        }

        public void DeletePerson(ref string[] array, ref string[] keyIndex) {
            string deletedPerson;
            var rgx = new Regex(@"[0-9]");
            Console.WriteLine("Write second name, name, father name of person, whose you want to delete:");
            do {
                deletedPerson = Console.ReadLine();
                if (deletedPerson.Replace(" ", "") == "")
                    Console.WriteLine("Error! Write second name, name and father name of the person:");
                if (rgx.IsMatch(deletedPerson))
                    Console.WriteLine("Error! Written the number...");
            } while (deletedPerson == "" || rgx.IsMatch(deletedPerson));

            var point = SearchPerson(deletedPerson);
            if (point == null) {
                Console.WriteLine("The given person isn't in the hash-table...");
            }
            else {
                array = ArrayLib.OneDimationalArr.DeleteArr.YdalElPoZnach(array, deletedPerson);
                keyIndex = ArrayLib.OneDimationalArr.DeleteArr.YdalElPoZnach(array, deletedPerson.Replace(" ", ""));
                FillingHashTable(keyIndex, array, Table.Length - 1, out _);
                if (Size == 0)
                    Console.WriteLine("The last person was deleted from the hash-table...");
                else
                    PrintHt();
            }
        }
    }

    internal class Program
    {
        //Массив текста меню
        private static readonly string[] Index =
            {
                "Create an array of elements, using RNG.",
                "Perform an element search on a given key, using a hash-table.",
                "Perform an element delete on a given key, using a hash-table.",
                "Perform an element add on a geven key, using a hash-table.",
                "Exit.", "Press ENTER to continue...", "Write an hash-table.",
                "Calculate the number of collisions with the size of the hash table 40, 75 and 90 elements"
            };

        //Рандомное имя
        private static readonly string[] RandomName =
            ("Харлам Аким Марьян Давыд Аникей Аврамий Демьян Егорий Софрон Кирсан Мина Лука Филат Гурий Игнат Пимен Леонтий " +
             "Пётр Аврамий Иларион Власий Захар Фаддей Влас Увар Мина Фаддей Ануфрий Лонгин Автоном").Split(' ');

        //Рандомная фамилия
        private static readonly string[] RandomSeName =
            ("Панин Качалов Жилин Кисель Туманский Янковский Кондратьев Теряев Несвицкий Калугин Дьяченко Сомов Власовский " +
             "Баташев Перхуров Савелов Троекуров Ашанин Бурунов Чернопятов Боярский Шеховцов Глебов Висленев Назимов Булдаков " +
             "Гарюшкин Головачёв Мокеев Кирпонос Аверченко Рахманинов").Split(' ');

        //Рандомное отчество
        private static readonly string[] RandomFatherName =
            ("Фектистович Порфирьевич Павлович Пахомович Арсеньевич Автономович Аксёнович Мануйлович Артемиевич Азарьевич " +
             "Марьянович Никифорович Данилович Аверьянович Парменович Иванович Титович Денисович Потапович Абрамович Лаврович " +
             "Гаврилович Яковлевич Ипатович Филимонович Минаевич Никодимович Емельянович Ефремович Прокофьевич Пантелеевич " +
             "Митрофанович").Split(' ');


        //Выделение памяти под массив и задание его размерности
        private static string[] InputSizeOfArray(out int size) {
            Console.WriteLine("Input size of array(no less than 100):");
            size = ReadLib.ReadLib.ReadVGran(100);
            return new string[size];
        }

        //Создание рандомного счета
        private static string CreateAccount(Random rnd) {
            string buf = null;
            for (var i = 0; i < 20; i++) buf = string.Concat(buf, rnd.Next(0, 10));
            return buf;
        }

        //Заполнение массива с ДСЧ
        private static string[] FillingAnArray(string[] array, out string[] k) {
            var rnd = new Random();
            k = new string[array.Length];
            for (var i = 0; i < array.Length; i++) {
                var seName = RandomSeName[rnd.Next(0, RandomSeName.Length)];
                var name = RandomName[rnd.Next(0, RandomName.Length)];
                var fatherName = RandomFatherName[rnd.Next(0, RandomFatherName.Length)];
                var person = new Person(seName, name, fatherName, CreateAccount(rnd),
                    rnd.Next(0, 1000000).ToString());
                array[i] = person.ToString();
                k[i] = string.Concat(seName, name, fatherName);
            }

            return array;
        }

        //Создание хэш-таблицы
        private static HTable CreateHashTable(string[] array, string[] key, out int k, out int count) {
            HTable ht;
            k = 0;
            count = 0;
            Console.WriteLine("Write the size of hash-table");
            var size = ReadLib.ReadLib.ReadVGran(0);
            if (size == 0) {
                Console.WriteLine("Error! The hash-table is empty...");
                return null;
            }

            ht = new HTable(size);
            ht.FillingHashTable(key, array, size, out count);
            return ht;
        }

        //case 1
        private static HTable Case1(out int pass, out string[] array, out string[] k) {
            array = InputSizeOfArray(out _);
            array = FillingAnArray(array, out k);
            Console.Clear();
            var hTable = CreateHashTable(array, k, out pass, out var count);
            if (hTable != null) {
                hTable.PrintHt();
                Console.WriteLine("The number of collisions: {0}", count);
            }

            Console.WriteLine(Index[5]);
            Console.ReadKey();
            return hTable;
        }

        //case 2
        private static void Case2(HTable hTable) {
            if (hTable != null) {
                string searchedPerson;
                var rgx = new Regex(@"[0-9]");
                Console.WriteLine("Write second name, name, father name of person, whose you want to find:");
                do {
                    searchedPerson = Console.ReadLine();
                    if (searchedPerson != null && searchedPerson.Replace(" ", "") == "")
                        Console.WriteLine("Write second name, name and father name of teh person:");
                    if (rgx.IsMatch(searchedPerson))
                        Console.WriteLine("Error! Written the number...");
                } while (searchedPerson == "" || rgx.IsMatch(searchedPerson));

                var searched = hTable.SearchPerson(searchedPerson);
                if (searched == null)
                    Console.WriteLine("The given person wasn't in the hash-table...");
                else
                    Console.WriteLine(searched);
            }
            else {
                Console.WriteLine("Error! The hash-table is empty...");
            }

            Console.WriteLine(Index[5]);
            Console.ReadKey();
        }

        //case 3
        private static HTable Case3(HTable hTable, ref string[] array, ref string[] keyIndex) {
            if (hTable != null)
                hTable.DeletePerson(ref array, ref keyIndex);
            else
                Console.WriteLine("Error! The hash-table is empty...");
            Console.WriteLine(Index[5]);
            Console.ReadKey();
            return hTable;
        }

        //case 4
        private static HTable Case4(HTable hTable, ref string[] array, ref string[] keyIndex) {
            Array.Resize(ref array, array.Length + 1);
            Array.Resize(ref keyIndex, keyIndex.Length + 1);
            var rgx = new Regex(@"[0-9]");
            string addPerson;
            string[] add;
            Console.WriteLine("Write second name, name and father name of the person, whose you want to add:");
            do {
                addPerson = Console.ReadLine();
                if (rgx.IsMatch(addPerson))
                    Console.WriteLine("Error! Written the number...");
                var separator = " ".ToCharArray();
                add = addPerson.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (add.Length != 3)
                    Console.WriteLine(
                        "Error! The wrong input...\nWrite second name, name and father name of teh person:");
            } while (addPerson == "" || rgx.IsMatch(addPerson) || add.Length != 3);

            var rnd = new Random();
            var person = new Person(add[0], add[1], add[2], CreateAccount(rnd), rnd.Next(0, 1000000).ToString());
            array[array.Length - 1] = person.ToString();
            keyIndex[keyIndex.Length - 1] = string.Concat(add[0], add[1], add[2]);
            if (hTable.SearchPerson(addPerson) == null) {
                hTable.FillingHashTable(keyIndex, array, hTable.Size + 1, out _);
                Console.WriteLine("{0} is added to the hash-table:", add[0] + " " + add[1] + " " + add[2]);
                hTable.PrintHt();
            }
            else {
                Console.WriteLine("The person is already added to the hash-table...");
            }

            Console.WriteLine(Index[5]);
            Console.ReadKey();
            return hTable;
        }


        //Печать меню для функции Main()
        private static bool PrintMenu(ref int pass, ref HTable hTable, ref string[] array, ref string[] keyIndex) {
            var ok = true;
            var sw = Print.Menu(pass, Index[0], Index[1], Index[2], Index[3], Index[6], Index[4]);
            switch (sw) {
                case 1:
                    hTable = Case1(out pass, out array, out keyIndex);
                    break;
                case 2:
                    Case2(hTable);
                    break;
                case 3:
                    hTable = Case3(hTable, ref array, ref keyIndex);
                    break;
                case 4:
                    hTable = Case4(hTable, ref array, ref keyIndex);
                    break;
                case 5:
                    hTable.PrintHt();
                    Console.WriteLine(Index[5]);
                    Console.ReadKey();
                    break;
                case 6:
                    ok = false;
                    break;
            }

            return ok;
        }

        //Функция Main()
        private static void Main() {
            var pass = 4;
            var hTable = new HTable(0);
            string[] array = null;
            string[] keyIndex = null;
            while (true)
                if (!PrintMenu(ref pass, ref hTable, ref array, ref keyIndex))
                    break;
            Console.WriteLine(Index[5]);
        }
    }
}