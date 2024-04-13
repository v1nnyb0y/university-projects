using System;

namespace Lab._1
{
    internal class ProgramTask3
    {
        private static void DoTask() {
            #region double 

            double a = 10000, b = 0.00001;
            var c = Math.Pow(a + b, 3);
            var d = Math.Pow(a, 3);
            var e = 3 * a * Math.Pow(b, 2);
            var f = 3 * b * Math.Pow(a, 2);
            var g = Math.Pow(b, 3);
            var res1 = (c - (d + e)) / (f + g);
            Console.WriteLine("Результат выражения при типе double = {0:f5}", res1);

            #endregion double 

            #region float 

            float a1 = 10000, b1 = 0.00001f;
            var c1 = (float) Math.Pow(a1 + b1, 3);
            var d1 = (float) Math.Pow(a1, 3);
            var e1 = (float) Math.Pow(b1, 2);
            var f1 = (float) Math.Pow(a1, 2);
            var g1 = (float) Math.Pow(b1, 3);
            var res2 = (c1 - (d1 + e1 * 3 * a1)) / (f1 * 3 * b1 + g1);
            Console.WriteLine("Результат выражения при типе float = {0:f5}", res2);

            #endregion float 

            Console.ReadLine();
        }
    }
}