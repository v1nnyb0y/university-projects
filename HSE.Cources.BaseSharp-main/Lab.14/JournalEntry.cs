using Lab._11;

namespace Lab._14
{
    public class JournalEntry
    {
        private readonly string Changes;
        private readonly IPerson ChangesPerson;
        private readonly string NameCollection;

        public JournalEntry(string nameCollection, string changes, IPerson person) {
            Changes = changes;
            ChangesPerson = person;
            NameCollection = nameCollection;
        }

        public override string ToString() {
            return "Название коллекции: " + NameCollection + " Изменение: " + Changes + " Измененный объект: " +
                   ChangesPerson;
        }
    }
}