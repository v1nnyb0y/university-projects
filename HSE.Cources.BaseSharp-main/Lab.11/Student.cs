using System;
using System.Text.RegularExpressions;

namespace Lab._11
{
    //Производный класс от персоны
    public class Student : Person
    {
        protected int Degree, Mark;

        //Конструктор без параметра
        public Student() {
            Degree = 0;
            Mark = 0;
        }

        //Конструктор с параметрами
        public Student(string name, string seName, int degree, int mark)
            : base(name, seName) {
            Degree = degree;
            Mark = mark;
        }

        public Student(Student student)
            : base(student.BasePerson) {
            Degree = student.Degree;
            Mark = student.Mark;
        }

        //Получить/задать курс студента(/протект)
        public int GetDegree => Degree;

        //Получить/задать рэйтинг студента(/протект)
        public int GetMark {
            get => Mark;
            protected set => Mark = value;
        }

        public new Person BasePerson => new Person(Name, SeName);

        public new static IPerson GetSelfPerson => CreateIPerson.CreateElement<Student>();

        //Вывод студента
        public override void Show() {
            Console.WriteLine(SeName + " " + Name + " Курс: " + Degree + " Оценка за экзамен: " + Mark);
        }

        public IPerson Create(IPerson person) {
            return new Student((Student) person);
        }

        public void Input() {
            string[] input;
            var inputFi = "";
            var regex = new Regex(@"^[А-Я][а-я]{1,}[ ][А-Я]{1}[а-я]{1,}$");
            var ok = true;
            while (ok) {
                Console.Write("Введите ФИ студента, которого необходимо найти: ");
                inputFi = Console.ReadLine();
                if (regex.IsMatch(inputFi))
                    ok = false;
                else
                    Console.WriteLine("Введите фамилию и имя в правильном формате (Фффф Ииии)");
            }

            Console.WriteLine("Введите курс студента: ");
            var degree = ReadLib.ReadLib.ReadVGran(1, 5);

            Console.WriteLine("Введите оценку студента за экзамен: ");
            var mark = ReadLib.ReadLib.ReadVGran(1, 11);

            input = inputFi.Split(' ');

            Name = input[1];
            SeName = input[0];
            Degree = degree;
            Mark = mark;
        }
    }
}