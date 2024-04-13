using System;

namespace Lab._6.SOLID.Journal
{
    internal class Message
    {
        private readonly string _changes;

        private readonly string _info;

        private readonly string _sourceFIO;

        private readonly DateTime _time;

        public Message
        (
            string   sourceFIO,
            string   changes,
            DateTime currMoment,
            string   additionalInfo
        ) {
            _sourceFIO = sourceFIO;
            _changes   = changes;
            _time      = currMoment;
            _info      = additionalInfo;
        }

        public override string ToString() {
            var outp = "";
            outp += "Сообщение о событии у " + _sourceFIO                                           + "\n";
            outp += "        Тип события: " + _changes                                              + "\n";
            outp += "        INFO: " + _info                                                        + "\n";
            outp += "        Время: " + _time.ToShortTimeString() + " " + _time.ToShortDateString() + "\n";
            return outp;
        }
    }
}