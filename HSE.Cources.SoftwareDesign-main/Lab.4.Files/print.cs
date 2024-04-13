using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._4.Files
{
    public static class print
    {
        public static int Menu(int escape, params string[] items_of_menu)
        {
            Console.Clear();
            Console.CursorVisible = false;
            var y = 1;
            int tek = 0, tekold = 0;
            var x = 1;
            var ok = false;
            for (var i = 0; i < items_of_menu.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                if (i % (escape + 1) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(items_of_menu[i]);
            }

            ;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x, y + tekold);
                Console.Write(items_of_menu[tekold] + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + tek);
                Console.Write(items_of_menu[tek]);
                tekold = tek;
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        tek += escape + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        tek -= escape + 1;
                        break;
                    case ConsoleKey.Enter:
                        ok = true;
                        break;
                }

                if (tek >= items_of_menu.Length)
                    tek = 0;
                else if (tek < 0)
                    tek = items_of_menu.Length - 1;
            } while (!ok);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return tek + 1;
        }
    }
}
