using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCSharp
{
    class Program
    {
        static void example_sbyte(sbyte digit) {
            sbyte some_digit = 0;

            some_digit = 23;
            //Check out of type
            try {
                some_digit = digit;
            }
            catch (Exception e) {
                Console.WriteLine($"{e} - ошибка\n");
            }


            //Check divided by zero
            try {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования sbyte = 5/0\n");
            }
            catch(Exception e) {
                Console.WriteLine($"{e} - ошибка\n");
            }

            int a = 10;
            //Exception: some_digit = a;
            some_digit = (sbyte) a;

            unchecked {
                for(int i = 0; i<200;i++)
                    some_digit ++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_byte(byte digit) {
            byte some_digit = 0;

            some_digit = 23;
            //Check out of type
            try
            {
                some_digit = digit;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }


            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования byte = 5/0\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }

            int a = 10;
            //Exception: some_digit = a;
            some_digit = (byte)a;

            unchecked
            {
                for (int i = 0; i < 500; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_short(short digit) {
            short some_digit = 0;

            some_digit = 23;
            //Check out of type
            try
            {
                some_digit = digit;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }


            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования short = 5/0\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }

            int a = 10;
            //Exception: some_digit = a;
            some_digit = (short)a;

            unchecked
            {
                for (int i = 0; i < 40000; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_int(int digit) {
            int some_digit = 0;

            some_digit = 23;
            //Check out of type
            try
            {
                some_digit = digit;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }


            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования int = 5/0\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }

            string a = "10";
            //Exception: some_digit = a;
            some_digit = Int32.Parse(a);

            unchecked
            {
                for (long i = 0; i < 30000000000; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_ushort(ushort digit) {
            ushort some_digit = 0;

            some_digit = 23;
            //Check out of type
            try
            {
                some_digit = digit;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }


            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования ushort = 5/0\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }

            int a = 10;
            //Exception: some_digit = a;
            some_digit = (ushort)a;

            unchecked
            {
                for (int i = 0; i < 400000; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_uint(uint digit) {
            uint some_digit = 0;

            some_digit = 23;
            //Check out of type
            try
            {
                some_digit = digit;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }


            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования uint = 5/0\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e} - ошибка\n");
            }

            short a = 10;
            //Exception: some_digit = a;
            some_digit = (uint)a;

            unchecked
            {
                for (long i = 0; i < 40000000000; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_float(float se, float e, float x)
        {
            var ne = 1;
            var ae = se;
            do
            {
                ae *= (float) (Math.Pow(-1, ne) * Math.Pow(x, 2) / (2 * ne + 1));
                se += ae;
                ne++;
            } while (ae > e);
            Console.WriteLine($"{se} - отличная точность\n");

            float some_digit = x;
            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования float = 5/0\n");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception} - ошибка\n");
            }

            double a = 10.1;
            //Exception: some_digit = a;
            some_digit = (float)a;

            unchecked
            {
                for (long i = 0; i < 4000000000000000000; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_double(double se, double e, double x)
        {
            var ne = 1;
            var ae = se;
            do
            {
                ae *= (double)(Math.Pow(-1, ne) * Math.Pow(x, 2) / (2 * ne + 1));
                se += ae;
                ne++;
            } while (ae > e);
            Console.WriteLine($"{se} - отличная точность\n");

            double some_digit = x;
            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования float = 5/0\n");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception} - ошибка\n");
            }

            int a = 10;
            //Exception: some_digit = a;
            some_digit = (double)a;

            unchecked
            {
                for (long i = 0; i < 4000000000000000000; i++)
                    some_digit++;
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_decimal(decimal se, decimal e, decimal x)
        {
            var ne = 1;
            var ae = se;
            do
            {
                ae *= (decimal)(Math.Pow(-1, ne) * Math.Pow((double)x, 2) / (2 * ne + 1));
                se += ae;
                ne++;
            } while (ae > e);
            Console.WriteLine($"{se} - отличная точность\n");

            decimal some_digit = x;
            //Check divided by zero
            try
            {
                some_digit /= 0;
                Console.WriteLine($"{some_digit} - пример использования float = 5/0\n");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception} - ошибка\n");
            }

            double a = 10.1;
            //Exception: some_digit = a;
            some_digit = (decimal)a;

            unchecked
            {
                for (long i = 0; i < 4000000000000000000; i++)
                    some_digit= (decimal)((double)some_digit * 0.1);
                Console.WriteLine($"{some_digit} - пример использования переполнения\n");
            }
        }

        static void example_bool(bool ok)
        {

        }

        static void example_char() {
            char a = 'G';
            char b = 'GG';

            try {
                //Nothing
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
        static void Main() {
            int sw = 3;
            switch (sw) {
                case 1:
                    example_sbyte(1);
                    break;
                case 2: 
                    example_byte(5);
                    break;
                case 3:
                    example_short(156);
                    break;
                case 4:
                    example_int(131312);
                    break;
                case 5:
                    example_ushort(15);
                    break;
                case 6:
                    example_uint(12121);
                    break;
                case 7:
                    example_float((float)1.1, (float)0.0001, (float)1.1);
                    break;
                case 8:
                    example_double(1.1, 0.0001, 1.1);
                    break;
                case 9:
                    example_decimal((decimal)1.1, (decimal)0.0001, (decimal)1.1);
                    break;
            }
        }
    }
}
