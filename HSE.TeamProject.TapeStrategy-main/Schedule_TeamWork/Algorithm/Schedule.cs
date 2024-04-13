using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_TeamWork.Algorithm
{
    /// <summary>
    /// Класс для составления оптимального расписания
    /// </summary>
    class Schedule
    {
        private string[] Lines;      //строки для работ
        private int numberWorkers;      //кол-во работников
        private int numberWorks;        //кол-во работ
        private List<Work> listWorks = new List<Work>();        //Лист всех работ
        


        #region Construction

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Schedule() {
            Lines = default(string[]);
            numberWorkers = default(int);
            numberWorks = default(int);
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="pairWork">Массив пар работ разделенных пробелом</param>
        /// <param name="_numberWorks">Количество работ</param>
        /// <param name="_numberWorkers">Количество работников</param>
        public Schedule(string[] pairWork, int _numberWorks, int _numberWorkers) {
            Lines = pairWork;
            numberWorks = _numberWorks;
            numberWorkers = _numberWorkers;
        }

        #endregion

        /// <summary>
        /// Создание начального листа(заполнения буковками алфавита)
        /// </summary>
        private void CreateList() {
            char name = 'A';
            for (int i = 0; i < numberWorks; i++) {
                listWorks.Add(new Work(name++));
            }
        }

        /// <summary>
        /// Дополнение информации о каждой работе
        /// </summary>
        private void CreateListAddon() {
            int i = 0;
            while (i < Lines.Length)
            {
                char[] works = new[] { Lines[i][0], Lines[i][2] };
                i++;
                int index = listWorks.BinarySearch(new Work(works[1]), new ComparerName());
                Work work = listWorks[index];
                work = new Work(work.GetName, default(int), work.GetnumberParents + 1, work.GetnextWork, work.GetparentList);
                int index_2 = listWorks.BinarySearch(new Work(works[0]), new ComparerName());
                listWorks[index_2] = new Work(listWorks[index_2].GetName, default(int), listWorks[index_2].GetnumberParents, works[1], listWorks[index_2].GetparentList);
                work.AddParent(listWorks[index_2]);
                listWorks[index] = work;
            }
        }

        /// <summary>
        /// Выставление приоритетов
        /// </summary>
        private void CreatePriority() {
            List<Work> tmp = new List<Work>(listWorks);
            tmp.Sort(new ComparerNext());
            int index = tmp.BinarySearch(new Work('a', true), new ComparerNext());
            tmp[index].FillPriority(0,0,ref listWorks);
        }

        /// <summary>
        /// Заполнения листа
        /// </summary>
        private void FillingArr() {
            CreateList();
            CreateListAddon();
            CreatePriority();
        }

        /// <summary>
        /// Создание оптимального расписания
        /// </summary>
        /// <returns>Работы в порядке их выполнения</returns>
        public List<char>[] CreateSchedule() {
            List<char>[] schedule = new List<char>[numberWorkers];
            for (int i = 0; i < numberWorkers; i++) {
                schedule[i] = new List<char>();
            }
            FillingArr();
            listWorks.Sort(new ComparerPriority());
            while (listWorks.Count != 0) {
                if (listWorks.Count == 1) {
                    schedule[0].Add(listWorks[0].GetName);
                    schedule[1].Add('-');
                    break;
                }
                int worker = 0;
                int index = 0;
                List<int> toParent = new List<int>();
                List<Work> toDelete = new List<Work>();
                while (index < listWorks.Count) {
                    if (listWorks[index].GetnumberParents == 0) {
                        schedule[worker].Add(listWorks[index].GetName);
                        int tmp = listWorks.IndexOf(new Work(listWorks[index].GetnextWork));
                        toParent.Add(tmp);
                        toDelete.Add(listWorks[index]);
                        worker++;
                    }

                    if (worker == numberWorkers) {
                        break;
                    }

                    index++;
                }

                for (int i = 0; i < toParent.Count; i++) {
                    listWorks[toParent[i]].GetnumberParents--;
                }

                for (int i = 0; i < toDelete.Count; i++) {
                    listWorks.Remove(toDelete[i]);
                }

                for (int i = worker; i < numberWorkers; i++) {
                    schedule[i].Add('-');
                }
            }
            return schedule;
        }
    }
}
