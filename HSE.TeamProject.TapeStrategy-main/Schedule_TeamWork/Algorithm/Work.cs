using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Schedule_TeamWork.Algorithm
{
    /// <summary>
    /// Класс для каждой работы
    /// </summary>
    class Work
    {
        private char Name;      // Название работы
        private int Priority;       // Приоритет работы
        private int numberParents;       //Кол-во предков
        private char nextWork;        //Следующая работа
        private List<Work> parentsWorks;      //Предыдущие работы
        private static int temp=0;        //Для назначения приоритетов

        #region Constructions

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Work() {
            Name = default(char);
            Priority = default(int);
            numberParents = default(int);
            nextWork = 'a';
            parentsWorks = new List<Work>();
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="_name">Имя работы</param>
        public Work(char _name) {
            Name = _name;
            Priority = default(int);
            numberParents = default(int);
            nextWork = 'a';
            parentsWorks = new List<Work>();
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="_nextWork">Следующая работа</param>
        /// <param name="kostil">Костыль</param>
        public Work(char _nextWork, bool kostil) {
            Name = default(char);
            Priority = default(int);
            numberParents = default(int);
            nextWork = _nextWork;
        }


        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="_name">Имя работы</param>
        /// <param name="_priority">Приоритет работы</param>
        /// <param name="_numberParents">Количество предков для работы</param>
        /// <param name="_nextWork">Следующая работа</param>
        public Work(char _name, int _priority, int _numberParents, char _nextWork, List<Work> list) {
            Name = _name;
            Priority = _priority;
            numberParents = _numberParents;
            nextWork = _nextWork;
            parentsWorks = list;
        }

        #endregion

        #region GetSet

        /// <summary>
        /// Получить потомственный лист
        /// </summary>
        public List<Work> GetparentList {
            get { return parentsWorks; }
        }

        /// <summary>
        /// Получить имя работы
        /// </summary>
        public char GetName {
            get { return Name;}
            private set { Name = value; }
        }

        /// <summary>
        /// Получить приоритет для работы
        /// </summary>
        public int GetPriority {
            get { return Priority;}
            set { Priority = value; }
        }

        /// <summary>
        /// Получить количество потомков
        /// </summary>
        public int GetnumberParents {
            get { return numberParents;}
            set { numberParents = value; }
        }

        /// <summary>
        /// Получить следующую для выполнения работу
        /// </summary>
        public char GetnextWork {
            get { return nextWork;}
            private set { nextWork = value; }
        }

        #endregion

        /// <summary>
        /// Добавить предка
        /// </summary>
        /// <param name="parent">Элемент предок</param>
        public void AddParent(Work parent) {
            parentsWorks.Add(parent);
        }

        /// <summary>
        /// Перегрузка сравнения работ по имени
        /// </summary>
        /// <param name="obj">Работа для сравнения</param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            Work work1 = (Work) obj;
            return Name == work1.Name;
        }

        /// <summary>
        /// Выставление приоритетов
        /// </summary>
        /// <param name="_priority">Приоритет</param>
        public void FillPriority(int _priority, int gap, ref List<Work> list) {
            Priority = _priority+1;
            _priority += gap + 1;
            int index = list.BinarySearch(new Work(Name), new ComparerName());
            list[index].GetPriority = Priority;
            int i = 0;
            foreach (Work parent in parentsWorks) {
                parent.FillPriority(_priority+i,parentsWorks.Count-(i+1),ref list);
                i++;
            }
        }
    }

    /// <summary>
    /// Класс для сравнения по имени
    /// </summary>
    class ComparerName : IComparer<Work>
    {
        public int Compare(Work x, Work y) {
            if (x.GetName > y.GetName) return 1;
            if (x.GetName < y.GetName) return -1;
            return 0;
        }
    }

    /// <summary>
    /// Класс для сравнения по приоритету
    /// </summary>
    class ComparerPriority : IComparer<Work>
    {
        public int Compare(Work x, Work y) {
            if (x.GetPriority < y.GetPriority) return 1;
            if (x.GetPriority > y.GetPriority) return -1;
            return 0;
        }
    }

    /// <summary>
    /// Класс для сравнения по следующему элементу
    /// </summary>
    class ComparerNext : IComparer<Work>
    {
        public int Compare(Work x, Work y) {
            if (x.GetnextWork > y.GetnextWork) return 1;
            if (x.GetnextWork < y.GetnextWork) return -1;
            return 0;
        }
    }
}
