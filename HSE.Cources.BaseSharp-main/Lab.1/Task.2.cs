using System;

namespace Lab._1
{
    internal class ProgramTask2
    {
        private static void DoTask() {
            double x, y;
            bool ok;
            Console.WriteLine("Введите коорднаты точки");
            Console.WriteLine("Введите коорднаты точки x");
            do {
                var buf = Console.ReadLine();
                ok = double.TryParse(buf, out x);
                if (!ok) Console.WriteLine("Ошибка ввода, введите действительное число х снова");
            } while (!ok);

            Console.WriteLine("Введите коорднаты точки y");
            do {
                var buf = Console.ReadLine();
                ok = double.TryParse(buf, out y);
                if (!ok) Console.WriteLine("Ошибка ввода, введите действительное число y снова");
            } while (!ok);

            var t1 = x >= 7 && x <= 0 || y >= -1 && y <= 0 || y <= -x / 7 - 1;
            Console.WriteLine(t1);
            Console.ReadLine();
        }
    }
}