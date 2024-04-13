using System;

namespace Lab._4
{
    internal class Program
    {
        public static void ZapolnArr(int[] arr, int dlinaArr) {
            var rnd = new Random();
            for (var i = 0; i < dlinaArr; i++)
                arr[i] = rnd.Next(-61, 60);
        }

        public static void VivodArr(int[] arr, int dlinaArr) {
            for (var i = 0; i < dlinaArr; i++)
                Console.Write(arr[i] + " ");
        }

        public static void YdalElem(int a, int[] arr, ref int dlinaArr) {
            for (var i = a; i < dlinaArr - 1; i++)
                arr[i] = arr[i + 1];
            dlinaArr--;
        }

        public static void ZapolnArrSKeyBoard(int[] arr, int dlinaarr) {
            for (var i = 0; i < dlinaarr; i++) {
                Console.WriteLine("Введите {0}-ый элемент", i + 1);
                arr[i] = ReadLib.ReadLib.ReadInt();
            }
        }

        public static void VstavkaEl(int a, int b, ref int[] arr, ref int dlinaArr) {
            var arr1 = new int[dlinaArr + b];
            var rnd = new Random();
            for (var i = 0; i < a - 1; i++)
                arr1[i] = arr[i];
            for (var i = a - 1; i < b + a - 1; i++)
                arr1[i] = rnd.Next(-61, 60);
            for (var i = a + b - 1; i < dlinaArr + b; i++)
                arr1[i] = arr[i - b];
            dlinaArr += b;
            arr = arr1;
            VivodArr(arr, dlinaArr);
        }

        public static void SdvigNTimes(int k, int[] arr, int dlinaArr) {
            for (var i = 0; i < k; i++) {
                var tmp = arr[0];
                for (var j = 1; j < dlinaArr; j++)
                    arr[j - 1] = arr[j];
                arr[dlinaArr - 1] = tmp;
            }

            VivodArr(arr, dlinaArr);
        }

        public static void PoiskElBezSort(int el, int[] arr, int dlinaArr) {
            var k = 0;
            for (var i = 0; i < dlinaArr; i++)
                if (arr[i] == el) {
                    k++;
                    Console.WriteLine("Данный елемент {0} встречается в неотсортированном массиве на {1} позиции.\n" +
                                      "Чтобы его найти выполнили {2} сравнений", el, i + 1, k);
                    break;
                }
                else {
                    k++;
                }

            if (k == dlinaArr)
                Console.WriteLine("\nЗаданного элемента {0} в массиве нет. Выполнили {1} сравнений", el, k);
        }

        public static void SortEasyVkl(int[] arr, int dlinaArr) {
            int j, el;
            for (var i = 1; i < dlinaArr; i++) {
                el = arr[i];
                j = i - 1;
                while (j >= 0 && el < arr[j]) {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = el;
            }

            VivodArr(arr, dlinaArr);
        }

        public static void PoiskElPlusSort(int el, int[] arr, int dlinaArr) {
            int left = 0, right = dlinaArr - 1, sred, k = 0;
            do {
                sred = (right + left) / 2;
                if (arr[sred] < el) {
                    left = sred + 1;
                    k++;
                }
                else {
                    right = sred;
                    k++;
                }
            } while (left != right);

            if (arr[left] == el)
                Console.WriteLine("Данный елемент {0} встречается в отсортированном массиве на {1} позиции.\n" +
                                  "Чтобы его найти выполнили {2} сравнений", el, left + 1, k);
            else
                Console.WriteLine("\nЗаданного элемента {0} в массиве нет. Выполнили {1} сравнений", el, k);
        }

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

            VivodArr(arr, arr.Length);
        }

        private static void Main() {
            //Введение кол-во членов массива
            Console.WriteLine("Введите размерность массива в промежутке от 0 до 10000");
            var dlina = ReadLib.ReadLib.ReadVGran(0, 10000);
            if (dlina == 0) {
                Console.WriteLine("Введена пустая последовательность");
            }
            else {
                Console.WriteLine("\nВведите способ заданий массива.\n" +
                                  "Способ 1. Задание массива с клавиатуры.\n" +
                                  "Способ 2. Задание массива рандомом от -60 до 60.\n");
                var arr = new int[dlina];
                var sw = ReadLib.ReadLib.ReadVGran(1, 2);
                switch (sw) {
                    case 1:
                        //Заполнение массива элеменнтами с класиватуры и вывод массива
                        ZapolnArrSKeyBoard(arr, dlina);
                        VivodArr(arr, dlina);
                        break;
                    case 2:
                        //Заполнение массива элементами рандом и вывод массива
                        ZapolnArr(arr, dlina);
                        VivodArr(arr, dlina);
                        break;
                }

                var prodolzenie = 1;
                while (prodolzenie == 1) {
                    //Выполнение заданий
                    Console.WriteLine("\nВведите число, эквивалентное выполняемому заданию.\n" +
                                      "Задание 1. Удалить элементы массива.\n" +
                                      "Задание 2. Добавить элементы массива.\n" +
                                      "Задание 3. Перестановка массива.\n" +
                                      "Задание 4. Поиск элементов массива.\n" +
                                      "Задание 5. Сортировка массива и поиск заданного элемента в сортированном массиве\n" +
                                      "Задание 6. Сортировка массива сложным методом\n" +
                                      "Задание 7. Печать массива");
                    sw = ReadLib.ReadLib.ReadVGran(1, 7);
                    switch (sw) {
                        //Первая задача
                        case 1:
                            if (dlina == 0) {
                                Console.WriteLine("Пустой массив, выполните действие 2");
                            }
                            else {
                                //Введение нужного элемента, который надо удалить
                                Console.WriteLine("Введите номер элемента, который необходимо удалить");
                                var el = ReadLib.ReadLib.ReadVGran(1, dlina);
                                //Удаление эллементов массива и его вывод
                                YdalElem(el - 1, arr, ref dlina);
                                if (dlina == 0)
                                    Console.WriteLine("После удаление элемента массив стал пустой");
                                else
                                    VivodArr(arr, dlina);
                            }

                            break;
                        case 2:
                            //Введение нужного элемента с которого и кол-ва элементов, которые необходимо довбавить
                            Console.WriteLine("Введите номер, с которого необходимо добавить элементы");
                            var n = ReadLib.ReadLib.ReadVGran(1, dlina + 1);
                            Console.WriteLine("Введите кол-во элементов, которые необходимо добавить");
                            var k = ReadLib.ReadLib.ReadInt();
                            //Вставка элементов массива и его вывод
                            VstavkaEl(n, k, ref arr, ref dlina);
                            break;
                        case 3:
                            //Введение кол-ва элементов, на которое надо сдвинуть массив
                            Console.WriteLine("Введите на сколько элементов необходимо сдвинуть массив влево");
                            var m = ReadLib.ReadLib.ReadInt();
                            //Сдвиг элементов массива и его вывод
                            SdvigNTimes(m, arr, dlina);
                            break;
                        case 4:
                            //Введите значение элемента, который надо найти
                            Console.WriteLine("Введите значение элемента, который надо найти");
                            var poisk = ReadLib.ReadLib.ReadInt();
                            //Поиск заданного элемента массива
                            PoiskElBezSort(poisk, arr, dlina);
                            break;
                        case 5:
                            //Сортировка методом простого включения
                            SortEasyVkl(arr, dlina);
                            //Введите значение элемента, который надо найти
                            Console.WriteLine("\nВведите значение элемента, который надо найти");
                            var poisk2 = ReadLib.ReadLib.ReadInt();
                            PoiskElPlusSort(poisk2, arr, dlina);
                            break;
                        case 6:
                            SortShell(arr);
                            break;
                        case 7:
                            VivodArr(arr, dlina);
                            break;
                    }

                    if (prodolzenie != 0) {
                        Console.WriteLine(
                            "\nВведите 0, если хотите остановить программу. Введите 1, если хотите продолжить работу программы");
                        prodolzenie = ReadLib.ReadLib.ReadVGran(0, 1);
                    }
                }
            }

            Console.WriteLine("\nНажмите Enter, чтобы выйти из программы");
        }
    }
}