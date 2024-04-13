using System;
using Lab._11;
using Lab._12;

namespace Lab._14
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class CollectionHandlerEventArgs : EventArgs
    {
        public CollectionHandlerEventArgs(string name, string changes, IPerson _object) {
            NameCollection = name;
            Changes = changes;
            ChangesPerson = _object;
        }

        public string NameCollection { get; }

        public string Changes { get; }

        public IPerson ChangesPerson { get; }

        public override string ToString() {
            return "/nНазвание коллекции: " + NameCollection + "/nИзменение: " + Changes + "/nИзмененный объект: " +
                   ChangesPerson;
        }
    }

    public class MyNewCollection : MyQueue<IPerson>
    {
        public MyNewCollection(string name) {
            NameCollection = name;
        }

        public string NameCollection { get; }

        public new IPerson this[int index] {
            get => base[index].Data;
            set {
                base[index].Data = value;
                OnCollectionReferenceChanged(this,
                    new CollectionHandlerEventArgs(NameCollection, "Изменение ссылки.", base[index].Data));
            }
        }

        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public void Filling() {
            var count = Capacity;
            for (var i = 0; i < count; i++) {
                switch (i % 3) {
                    case 0:
                        Enqueue(Student.GetSelfPerson);
                        break;
                    case 1:

                        Enqueue(Associate.GetSelfPerson);
                        break;
                    case 2:
                        Enqueue(Teacher.GetSelfPerson);
                        break;
                }

                if (CollectionCountChanged != null)
                    OnCollectionCountChanged(this,
                        new CollectionHandlerEventArgs(NameCollection, "Добавление.", base[i].Data));
            }
        }

        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args) {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }

        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args) {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }

        public void AddDefault() {
            Console.WriteLine("Текущая очередь:");
            Show();
            Console.WriteLine("\n");

            var rnd = new Random();
            var index = rnd.Next(1, Capacity - 7);
            if (base.Add(Student.GetSelfPerson, index))
                OnCollectionCountChanged(this,
                    new CollectionHandlerEventArgs(NameCollection, "Добавление.", base[index - 1].Data));
            else
                Console.WriteLine("Индекс неверен!");
            Console.WriteLine("\nОчередь после изменений:");
            Show();
        }

        public void Add(IPerson[] array) {
            Console.WriteLine("Текущая очередь:");
            Show();
            Console.WriteLine("\n");

            foreach (var person in array) {
                Enqueue(person);
                OnCollectionCountChanged(this,
                    new CollectionHandlerEventArgs(NameCollection, "Добавление.", base[Count - 1].Data));
            }

            Console.WriteLine("\nОчередь после изменений:");
            Show();
        }

        public new void Remove(int index) {
            Console.WriteLine("Текущая очередь:");
            Show();
            Console.WriteLine("\n");

            var clone = Clone();

            if (base.Remove(index))
                OnCollectionCountChanged(this,
                    new CollectionHandlerEventArgs(NameCollection, "Удаление.", clone[index - 1].Data));
            else
                Console.WriteLine("Индекс неверен!");

            if (Count == 0) {
                Console.WriteLine("Очередь опустела!");
            }
            else {
                Console.WriteLine("\nОчередь после изменений:");
                Show();
            }
        }

        public void Indexes(int index) {
            Console.WriteLine("Текущая очередь:");
            Show();
            Console.WriteLine("\n");

            base[index].Data = CreateIPerson.CreateElement<Teacher>();

            Console.WriteLine("\nОчередь после изменений:");
            Show();
        }

        public void Show() {
            Console.WriteLine("\nНазвание коллекции: " + NameCollection + "\n");
            foreach (QueueElement<IPerson> person in this) person.Data.Show();
        }
    }
}