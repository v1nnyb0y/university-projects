using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workes
{
    /// <summary>
    /// Класс для работ
    /// </summary>
    class Work
    {
        private char letter;        //Имя работы
        private int firstWork;      //Первая работа
        private int secondWork;     //Вторая работа

        /// <summary>
        /// Инициализация класса Work c параметрами
        /// </summary>
        /// <param name="_first">Начальная работа(длина)</param>
        /// <param name="_second">Конечная работа(длина)</param>
        /// <param name="name">Имя работы</param>
        public Work(char name,int _first, int _second) {
            firstWork = _first;
            secondWork = _second;
            letter = name;
        }

        /// <summary>
        /// Получить длину начальной работы
        /// </summary>
        public int GetStartWork {
            get { return firstWork; }
        }

        /// <summary>
        /// Получить длину конечной работы
        /// </summary>
        public int GetLastWork { 
            get { return secondWork; }
        }

        /// <summary>
        /// Получить название работы
        /// </summary>
        public char GetName {
            get { return letter; }
        }
    }

    class Comparer : IComparer
    {
        public int Compare(object obj1, object obj2) {
            Work firstWork = obj1 as Work;
            Work secondWork = obj2 as Work;
            int min_1 = Math.Min(firstWork.GetStartWork, secondWork.GetLastWork);
            int min_2 = Math.Min(firstWork.GetLastWork, secondWork.GetStartWork);
            if (min_1 >= min_2) return 1;
            return -1;
        }
    }
}
