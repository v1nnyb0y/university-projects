using System;

namespace Lab._3
{
    internal class Program
    {
        public static void PodchetSn(int n, ref double sn, double x) {
            var an = sn;
            for (var i = 1; i <= n; i++) {
                an *= Math.Pow(-1, i) * Math.Pow(x, 2) / (2 * i + 1);
                sn += an;
            }
        }

        public static void PodchetSe(ref double se, double e, double x) {
            var ne = 1;
            var ae = se;
            do {
                ae *= Math.Pow(-1, ne) * Math.Pow(x, 2) / (2 * ne + 1);
                se += ae;
                ne++;
            } while (ae > e);
        }

        private static void Main() {
            double shag = (1 - 0.1) / 10, x = 0.01, se, sn;
            for (var k = 0; k <= 10; k++) {
                x += shag;
                var toh = Math.Sin(x);
                sn = x;
                se = x;
                PodchetSn(40, ref sn, x);
                PodchetSe(ref se, 0.0001, x);
                Console.WriteLine("X = {0:f5}; SN = {1:f5}; SE = {2:f5}; Y = {3:f5}", x, sn, se, toh);
            }
        }
    }
}