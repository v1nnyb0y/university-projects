using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workes;

namespace Schedule
{
    /// <summary>
    /// Класс для составления расписания
    /// </summary>
    class Schedule
    {
        private Work[] arrWork;     //Массив длин работ

        /// <summary>
        /// Инициализация класса Schedule без параметров
        /// </summary>
        public Schedule()
        {
            arrWork = default(Work[]);
        }

        /// <summary>
        /// Инициализация класса Schedule c параметрами
        /// </summary>
        /// <param name="lines">Массив длительностей работ</param>
        public Schedule(string[] lines)
        {
            char letter = 'A';
            arrWork = new Work[lines.Length];
            int index = 0;
            foreach (var el in lines)
            {
                string[] tmp = el.Split(' ');
                arrWork[index++] = new Work(letter++, Int32.Parse(tmp[0]), Int32.Parse(tmp[1]));
            }
        }

        public List<char>[] CreateSchedule()
        {
            Array.Sort(arrWork, new Comparer());
            List<char>[] personWork = new List<char>[2];
            List<char> firstPerson = new List<char>();
            List<char> secondPeron = new List<char>();

            int index = 0;
            for (int day = 0; day < arrWork.Length; day++) {
                for (int i = 0; i < arrWork[day].GetStartWork; i++) {
                    firstPerson.Add(arrWork[day].GetName);
                    if (day == 0) {
                        index++;
                        secondPeron.Add('-');
                    }
                }
            }

            for (int day = 0; day < arrWork.Length; day++) {
                try {
                    while (firstPerson[index] == arrWork[day].GetName && firstPerson[index] == arrWork[day+1].GetName)
                    {
                        secondPeron.Add('-');
                        index++;
                    }
                }
                catch { }

                for (int i = 0; i < arrWork[day].GetLastWork; i++)
                {
                    secondPeron.Add(arrWork[day].GetName);
                }
            }

            personWork[0] = firstPerson;
            personWork[1] = secondPeron;
            return personWork;
        }
    }
}
