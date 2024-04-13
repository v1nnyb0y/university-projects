using System;

namespace Lab._2
{
    internal class ProgramTask2
    {
        private static void DoTask() {
            int crat, chislo, chet = 0;
            bool ok, kk;
            Console.WriteLine("Введите первый член последовательности");
            do {
                kk = false;
                var buf = Console.ReadLine();
                ok = int.TryParse(buf, out crat);
                if (!ok) Console.WriteLine("Ошибка ввода, введено не целое число\n");
                else if (crat == 0) Console.WriteLine("Ошибка ввода, введена пустя последовательность");
                else
                    kk = true;
            } while (!ok || !kk);

            Console.WriteLine("Вводите члены последовательности через Enter. Когда закончите ввод, введите 0");
            do {
                do {
                    Console.WriteLine("Введите остальную последовательность");
                    var buf = Console.ReadLine();
                    ok = int.TryParse(buf, out chislo);
                    if (!ok) Console.WriteLine("Ошибка ввода, введено не целое число");
                } while (!ok);

                if (chislo % crat == 0 && chislo != 0) chet++;
            } while (chislo != 0);

            Console.WriteLine("Кол-во членов последновательности, кратных {0}, равно {1}", crat, chet);
        }
    }
}