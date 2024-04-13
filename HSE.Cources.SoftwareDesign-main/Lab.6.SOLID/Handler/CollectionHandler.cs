using System;

namespace Lab._6.SOLID.Handler
{
    public delegate void CollectionHandler
    (
        object                     source,
        CollectionHandlerEventArgs args
    );

    public class CollectionHandlerEventArgs : EventArgs
    {
        public CollectionHandlerEventArgs
        (
            string   sourceFIO,
            string   changes,
            DateTime currMoment,
            string   additionalInfo
        ) {
            SourceFIO = sourceFIO;
            Changes   = changes;
            Time      = currMoment;
            Info      = additionalInfo;
        }

        public string SourceFIO { get; }

        public string Changes { get; }

        public DateTime Time { get; }

        public string Info { get; }

        public override string ToString() {
            var outp = "";
            outp += "Сообщение о событии у " + SourceFIO                                          + "\n";
            outp += "        Тип события: " + Changes                                             + "\n";
            outp += "        INFO: " + Info                                                       + "\n";
            outp += "        Время: " + Time.ToShortTimeString() + " " + Time.ToShortDateString() + "\n";
            return outp;
        }
    }
}