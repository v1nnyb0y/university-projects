using System;
using System.Text.RegularExpressions;

namespace Lab._11
{
    public class Associate : Person, ICloneable
    {
        protected string Department;

        //Конструктор без параметра
        public Associate() {
            Department = "";
        }

        //Конструктор с параметром
        public Associate(string name, string seName, string department)
            : base(name, seName) {
            Department = department;
        }

        public Associate(Associate associate)
            : base(associate.BasePerson) {
            Department = associate.Department;
        }

        //Получить/задать кафедру(/протект)
        public string GetDepartment {
            get => Department;
            protected set => Department = value;
        }

        public new Person BasePerson => new Person(Name, SeName);

        public new static IPerson GetSelfPerson => CreateIPerson.CreateElement<Associate>();

        //Глубокое копирование
        public object Clone() {
            return new Associate(Name, SeName, Department);
        }

        //Вывод сотрудника
        public override void Show() {
            Console.WriteLine(SeName + " " + Name + " Кафедра: " + Department);
        }

        //Поверхностное копирование
        public Associate SurfaceCopying() {
            return (Associate) MemberwiseClone();
        }

        public IPerson Create(IPerson person) {
            return new Associate((Associate) person);
        }

        public void Input() {
            string[] input;
            var inputDepartment = "";
            string inputFI;
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

            input = inputDepartment.Split(' ');
            Name = input[1];
            SeName = input[0];
            Department = inputDepartment;
        }
    }
}