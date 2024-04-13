using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._3.SocialNetwork
{
    class Message
    {
        public Message(string source_fio, string changes, DateTime curr_moment, string additional_info)
        {
            Source_FIO = source_fio;
            Changes = changes;
            Time = curr_moment;
            INFO = additional_info;
        }

        private readonly string Source_FIO;

        private readonly string Changes;

        private readonly DateTime Time;

        private readonly string INFO;

        public override string ToString()
        {
            string outp = "";
            outp += "Сообщение о событии у " + Source_FIO + "\n";
            outp += "        Тип события: " + Changes + "\n";
            outp += "        INFO: " + INFO + "\n";
            outp += "        Время: " + Time.ToShortTimeString() + " " + Time.ToShortDateString() + "\n";
            return outp;
        }
    }
}
