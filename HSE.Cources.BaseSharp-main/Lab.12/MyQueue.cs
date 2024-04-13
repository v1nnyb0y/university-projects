using System;
using System.Collections;

namespace Lab._12
{
    public class MyQueue<T> : IEnumerable
    {
        internal QueueElement<T> QueueElement;
        public int Capacity { get; private set; }

        public int Count { get; private set; }

        public QueueElement<T> this[int index] {
            get {
                if (index < Count) {
                    var count = 0;
                    foreach (var element in this) {
                        if (count == index) return (QueueElement<T>) element;

                        count++;
                    }
                }
                else {
                    Console.WriteLine("Индекс неверен!");
                }

                return null;
            }
            set {
                if (index < Count) {
                    var root = QueueElement;
                    for (var count = 0; count <= Count; count++)
                        if (count == index)
                            break;
                        else
                            root = root.Next;
                }
                else {
                    Console.WriteLine("Индекс неверен!");
                }
            }
        }

        public IEnumerator GetEnumerator() {
            return new ClassEnumerator<T>(this);
        }

        public bool Contains(object queueElement) {
            foreach (var queue in this)
                if (queue.Equals(queueElement))
                    return true;

            return false;
        }

        public void Clear() {
            Count = 0;
            QueueElement = null;
        }

        public T Dequeue() {
            Count--;
            var data = QueueElement.Data;
            QueueElement = QueueElement.Next;
            return data;
        }

        public bool Add(T person, int index) {
            if (index <= Count) {
                if (Count == index) {
                    Enqueue(person);
                }
                else {
                    var nextElement = this[index];
                    this[index - 1].Next = new QueueElement<T>(person);
                    this[index].Next = nextElement;
                }

                if (++Count == Capacity) Capacity *= 2;

                return true;
            }

            return false;
        }

        public bool Remove(int index) {
            if (index <= Count) {
                if (index == Count)
                    this[index] = null;
                else
                    this[index - 1].Next = this[index].Next;

                Count--;
                return true;
            }

            return false;
        }

        public void Enqueue(T addElement) {
            Count++;
            Capacity *= Count == Capacity ? 2 : 1;
            var add = new QueueElement<T>(addElement);
            var beg = QueueElement;
            if (beg != null) {
                while (beg.Next != null) beg = beg.Next;
                add.Next = beg.Next;
                beg.Next = add;
            }
            else {
                QueueElement = add;
            }
        }

        public T Peek() {
            return QueueElement.Data;
        }

        public T[] ToArray() {
            var array = new T[0];
            foreach (T add in this) {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = add;
            }

            return array;
        }

        public MyQueue<T> Clone() {
            var newQueue = new MyQueue<T>(Capacity);
            foreach (T cloneElement in this) newQueue.Enqueue(cloneElement);

            return newQueue;
        }

        public void CopyTo(out T[] array, int arrayIndex) {
            array = new T[arrayIndex];
            var i = 0;
            foreach (T addElement in this) {
                array[i] = addElement;
                i++;
                if (i == arrayIndex) return;
            }
        }

        #region Constractions

        //Пустой конструктор
        public MyQueue() {
            Capacity = 10;
        }

        //С заданным разрешением(ёмкостью)
        public MyQueue(int capacity) {
            Capacity = capacity;
        }

        //Элементы и емкости другой последовательность(not ready)
        public MyQueue(MyQueue<T> queue) {
            Capacity = queue.Capacity;
            Count = queue.Count;
            QueueElement = queue.QueueElement;
        }

        #endregion
    }

    public class QueueElement<T>
    {
        public T Data; //информационное поле 
        public QueueElement<T> Next; //адресное поле 

        public QueueElement() //конструктор без параметров 
        {
            Data = default(T);
            Next = null;
        }

        public QueueElement(T d) //конструктор с параметрами 
        {
            Data = d;
            Next = null;
        }

        public override bool Equals(object obj) {
            var queue = (QueueElement<T>) obj;
            return Data.Equals(queue.Data);
        }

        public override string ToString() {
            return Data + " ";
        }
    }

    internal class ClassEnumerator<T> : IEnumerator
    {
        private QueueElement<T> _currElement;
        private int _position = -1;

        public ClassEnumerator(MyQueue<T> t) {
            _currElement = t.QueueElement;
        }

        // The IEnumerator interface requires a MoveNext method. 
        public bool MoveNext() {
            if (_position == -1) {
                _position++;
                return true;
            }

            if (_currElement.Next != null) {
                _position++;
                _currElement = _currElement.Next;
                return true;
            }

            return false;
        }

        // The IEnumerator interface requires a Reset method. 
        public void Reset() {
            _position = -1;
        }

        // The IEnumerator interface requires a Current method. 
        public object Current => _currElement.Data;
    }
}