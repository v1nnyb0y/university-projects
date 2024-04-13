using System;

namespace Lab._6.SOLID.PersonActions.Write
{
    internal class ConsolePrinter : IPrinter
    {
        public void Print
        (
            string text
        ) {
            Console.WriteLine
                (
                 text
                );
        }
    }
}