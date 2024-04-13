using System;
using System.Text.RegularExpressions;

namespace Lab._11
{
    public class Teacher : Associate
    {
        protected string Office;

        //Конструкт без параметра
        public Teacher() {
            Office = "";
        }

        //Конструктор с параметрои
        public Teacher(string name, string seName, string department, string office)
            : base(name, seName, department) {
            Office = office;
        }

        public Teacher(Teacher teacher)
            : base(teacher.Name, teacher.SeName, teacher.Department) {
            Office = teacher.Office;
        }

        //Получить/задать должность(/протект)
        public string GetOffice {
            get => Office;
            protected set => Office = value;
        }

        public new Person BasePerson => new Person(Name, SeName);

        public new static IPerson GetSelfPerson => CreateIPerson.CreateElement<Teacher>();

        //Вывод препода
        public override void Show() {
            Console.WriteLine(SeName + " " + Name + " Кафедра: " + Department + " Должность: " + Office);
        }

        public IPerson Create(IPerson person) {
            return new Teacher((Teacher) person);
        }

        public void Input() {
            string[] input;
            var inputDepartment = "";
            string inputFI;
            var inputOffice = "";
            var regex = new Regex(@"^[А-Я][а-я]{1,}[ ][А-Я]{1}[а-я]{1,}$");
            var ok = true;
            while (ok) {
                Console.Write("Введите ФИ сотрудника, которого необходимо найти: ");
                inputFI = Console.ReadLine();
                if (regex.IsMatch(inputFI))
                    ok = false;
                else
                    Console.WriteLine("Введите фамилию и имя в правильном формате (Фффф Ииии)");
            }

            regex = new Regex(@"[А-Я]{1}[а-я]{1,}[ ]([а-я]{1,}[ ]){1,}[а-я]{1,}");
            ok = true;
            while (ok) {
                Console.Write("Введите кафедру сотрудника, которого необходимо найти: ");
                inputDepartment = Console.ReadLine();
                if (regex.IsMatch(inputDepartment))
                    ok = false;
                else
                    Console.WriteLine("Введите кафедру в правильном формате (Название)");
            }

            regex = new Regex(@"[А-Я]{1}[а-я]{1,}[ ]{0,1}([а-я]{1,}[ ]){0,}[а-я]{0,}");
            ok = true;
            while (ok) {
                Console.Write("Введите должность сотрудника, которого необходимо найти: ");
                inputOffice = Console.ReadLine();
                if (regex.IsMatch(inputOffice))
                    ok = false;
                else
                    Console.WriteLine("Введите должность в правильном формате (Название)");
            }

            input = inputDepartment.Split(' ');

            Name = input[1];
            SeName = input[0];
            Department = inputDepartment;
            Office = inputOffice;
        }
    }
}