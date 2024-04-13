using System;

namespace Lab._2
{
    internal class ProgramTask1
    {
        private static void DoTask() {
            bool ok1, ok2, ok3;
            double a, sum;
            int n, i;
            Console.WriteLine("Введите кол-во членов последовательности");
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

            sum = 0;


            for (i = 1; i <= n; i++) {
                Console.WriteLine("Введите {0} член последоательности", i);
                do {
                    var buf = Console.ReadLine();
                    ok1 = double.TryParse(buf, out a);
                    if (!ok1) Console.WriteLine("Ошибка ввода, введено не вещественное число");
                } while (!ok1);

                if (i % 2 == 0) sum += a;
            }

            if (n == 1) Console.WriteLine("Четных членов в последовательности нет");
            else Console.WriteLine("Сумма всех четных членов последовательности равна {0}", sum);

            Console.ReadLine();
        }
    }
}