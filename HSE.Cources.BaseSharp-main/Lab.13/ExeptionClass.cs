using System;

namespace Lab._13
{
    public class ExeptionClass : ApplicationException
    {
        public ExeptionClass() { }

        public ExeptionClass(string str)
            : base(str) { }

        public override string ToString() {
            return Message;
        }
    }
}