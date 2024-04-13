using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._3.SocialNetwork
{
    class Journal
    {
        private readonly string FIO;
        private Message[] messages = new Message[0];

        public Journal(string _FIO) {
            FIO = _FIO;
        }

        public void add(Message message) {
            Array.Resize(ref messages, messages.Length + 1);
            messages[messages.Length - 1] = message;
        }

        public override string ToString() {
            var title = $"\nНовости для {FIO}:\n";
            var count = 0;
            foreach (var mssg in messages) {
                title = string.Concat(title, ++count, ". ", mssg.ToString(), "\n");
            }

            return title;
        }

        public void CollectionHandlerEvent(object source, CollectionHandlerEventArgs args) {
            var new_mssg = new Message(args.Source_FIO, args.Changes, args.Time, args.INFO);
            add(new_mssg);
        }
    }
}
