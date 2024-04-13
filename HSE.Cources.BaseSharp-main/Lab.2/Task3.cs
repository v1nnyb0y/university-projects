using System;

namespace Lab._2
{
    internal class ProgramTask3
    {
        private static void DoTask() {
            bool ok1, ok2, ok3;
            int n, sum = 0, i = 1;
            Console.WriteLine("Введите кол-во слагаемых выражения");
            do {
                var buf = Console.ReadLine();
                ok1 = int.TryParse(buf, out n);
                ok2 = false;
                ok3 = false;
                if (!ok1) {
                    Console.WriteLine("Ошибка ввода, введите целое число");
                }
                else {
                    if (n < 0) Console.WriteLine("Ошибка ввода, введите положительное число");
                    else ok2 = true;
                    if (n == 0) Console.WriteLine("Ошибка ввода, введена пустая последовательность");
                    else ok3 = true;
                }
            } while (!ok1 || !ok2 || !ok3);

            while (i <= n)
                if (i % 3 == 0)
                    sum -= i++;
                else
                    sum += i++;

            Console.WriteLine("Выражение равно {0}", sum);
            Console.ReadLine();
        }
    }
}