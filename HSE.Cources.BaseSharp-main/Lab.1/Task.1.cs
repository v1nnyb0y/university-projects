using System;

namespace Lab._1
{
    internal class ProgramTask1
    {
        private static void DoTask() {
            int res1, n, m;
            double x, res4;
            bool res2, res3, ok, kk;
            do {
                var ko = false;
                kk = false;
                do {
                    Console.WriteLine("Введите начало отрезка");
                    ok = int.TryParse(Console.ReadLine(), out n);
                    if (!ok) Console.WriteLine("Было введено не целое число, попробуйте еще раз");
                    if (n == 0 || n == 1)
                        Console.WriteLine("Было введено n=0 или n=1, что недопустимо, попробуйте еще раз \n");
                    else ko = true;
                } while (!ok || !ko);

                do {
                    Console.WriteLine("Введите конец отрезка");
                    ok = int.TryParse(Console.ReadLine(), out m);
                    if (!ok) Console.WriteLine("Было введено не целое число, попробуйте еще раз");
                } while (!ok);

                if (n >= m)
                    Console.WriteLine(
                        "Введены некорректные данные. Число, обозначающее конец отрезка должно быть больше числа, обозначающего конец \n");
                else kk = true;
            } while (!kk);

            res1 = m / --n;
            n++;
            Console.WriteLine("m/--n++ = {0}, n = {1}, m = {2}", res1, n, m);
            res2 = false;
            if (m / n < n--) res2 = true;
            Console.WriteLine("m/n<n-- = {0}, n = {1}, m = {2}", res2, n, m);
            res3 = false;
            if (m + n++ > n + m) res3 = true;
            Console.WriteLine("m + n++ > n + m = {0}, n = {1}, m = {2}", res3, n, m);
            do {
                Console.WriteLine("Введите х");
                ok = double.TryParse(Console.ReadLine(), out x);
                if (!ok) Console.WriteLine("Было введено не вещественное число, попробуйте еще раз");
            } while (!ok);

            res4 = Math.Pow(x, 5) * Math.Sqrt(Math.Abs(x - 1)) + Math.Abs(25 - Math.Pow(x, 5));
            Console.WriteLine(
                "Math.Pow(x, 5) * Math.Sqrt(Math.Abs(x - 1)) + Math.Abs(25 - Math.Pow(x, 5)) = {0:f3},x = {1}", res4,
                x);

            Console.ReadLine();
        }
    }
}