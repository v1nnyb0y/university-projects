using System;
using Lab._6.SOLID.Handler;

namespace Lab._6.SOLID.Journal
{
    internal class Journal
    {
        private readonly string    _fio;
        private          Message[] _messages = new Message[0];

        public Journal
        (
            string fio
        ) {
            _fio = fio;
        }

        public void Add
        (
            Message message
        ) {
            Array.Resize
                (
                 ref _messages,
                 _messages.Length + 1
                );
            _messages[_messages.Length - 1] = message;
        }

        public override string ToString() {
            var title = $"\nНовости для {_fio}:\n";
            var count = 0;
            foreach (var mssg in _messages)
                title = string.Concat
                    (
                     title,
                     ++count,
                     ". ",
                     mssg.ToString(),
                     "\n"
                    );

            return title;
        }

        public void CollectionHandlerEvent
        (
            object                     source,
            CollectionHandlerEventArgs args
        ) {
            var newMssg = new Message
                (
                 args.SourceFIO,
                 args.Changes,
                 args.Time,
                 args.Info
                );
            Add
                (
                 newMssg
                );
        }
    }
}