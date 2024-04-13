using System;
using System.Globalization;
using Generator;
using Graph;

namespace AaDS.MaxFlow
{
    internal class MainProgram
    {
        /// <summary>
        ///     Print menu
        /// </summary>
        /// <param name="pechat">Array</param>
        /// <returns></returns>
        private static int _print_menu(int k, params string[] pechat) {
            Console.Clear();
            Console.CursorVisible = false;
            var y = 1;
            int tek = 0, tekold = 0;
            var x = 1;
            var ok = false;
            for (var i = 0; i < pechat.Length; i++) {
                Console.SetCursorPosition(x, y + i);
                if (i % (k + 1) == 0) {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(pechat[i]);
            }

            ;
            do {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x, y + tekold);
                Console.Write(pechat[tekold] + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + tek);
                Console.Write(pechat[tek]);
                tekold = tek;
                var key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.DownArrow:
                        tek += k + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        tek -= k + 1;
                        break;
                    case ConsoleKey.Enter:
                        ok = true;
                        break;
                }

                if (tek >= pechat.Length)
                    tek = 0;
                else if (tek < 0)
                    tek = pechat.Length - 1;
            } while (!ok);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return tek + 1;
        }

        /// <summary>
        ///     Input int digit
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Int digit</returns>
        private static int _int_input(string message) {
            var digit = -1;
            while (true) {
                Console.Write(message);
                var ok = int.TryParse(Console.ReadLine(), out digit);
                if (ok) return digit;
            }
        }

        /// <summary>
        /// Check zero value
        /// </summary>
        /// <returns>Graph size</returns>
        private static int _check_zero_one_value() {
            var Size = _int_input("Write size of the graph: ");
            while (true)
            {
                if (Size == 0 || Size == 1)
                {
                    Console.WriteLine("Wrong input! Size cannot be a zero|one value... Repeat please");
                    Size = _int_input("Write size of the graph: ");
                    continue;
                }

                return Size;
            }
        }

        private static void Main() {
            string[] menu =
                {
                    "Create graph (using generator) and find max flow.",
                    "Create graph (using keyboard) and find max flow.",
                    "Close"
                };
            while (true) {
                var sw = _print_menu(0, menu);
                switch (sw) {
                    case 1: {
                        int size = _check_zero_one_value();
                        var _generator = new GraphGenerator(size, 0);
                        byte[,] graph = _generator.GetMatrix;
                        int[,] ribs_capacity = _generator.GetRibsCapacity;
                        GraphAction action = new GraphAction(graph, ribs_capacity);
                        action.Output();
                        Console.WriteLine("Generator answer: {1}\nMax flow in the graph: {0}",
                            action.GetMaxFlow,
                            _generator.GetSum);
                        Console.ReadKey(true);
                        break;
                    }
                    case 2: {
                        var _generator = new GraphGenerator(0, 1);
                        byte[,] graph = _generator.GetMatrix;
                        int[,] ribs_capacity = _generator.GetRibsCapacity;
                        GraphAction action = new GraphAction(graph, ribs_capacity);
                        action.Output();
                        Console.WriteLine("Max flow in the graph: {0}",
                            action.GetMaxFlow);
                            Console.ReadKey(true);
                        break;
                    }
                    case 3: {
                        return;
                    }
                }
            }
        }
    }
}