using System;

namespace Lab._14
{
    internal class Journal
    {
        private readonly string _nameColl;
        private JournalEntry[] _journal = new JournalEntry[0];

        public Journal(string name) {
            _nameColl = name;
        }

        public void Add(JournalEntry je) {
            Array.Resize(ref _journal, _journal.Length + 1);
            _journal[_journal.Length - 1] = je;
        }

        public override string ToString() {
            var str = "\nЖурнал для коллекции " + _nameColl + ":\n";
            var count = 0;
            foreach (var element in _journal) str = string.Concat(str, ++count, ". ", element.ToString(), "\n");

            return str;
        }

        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs e) {
            var je = new JournalEntry(e.NameCollection, e.Changes, e.ChangesPerson);
            Add(je);
        }

        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs e) {
            var je = new JournalEntry(e.NameCollection, e.Changes, e.ChangesPerson);
            Add(je);
        }
    }
}