using System;
using System.Text.RegularExpressions;

namespace Lab._11
{
    //Базовый класс
    public class Person : IPerson
    {
        protected string Name, SeName;

        //Конструктор без параметров
        public Person() {
            Name = "";
            SeName = "";
        }

        //Конструктор с параметрами
        public Person(string name, string seName) {
            Name = name;
            SeName = seName;
        }

        public Person(Person person) {
            Name = person.Name;
            SeName = person.SeName;
        }

        //Получить фамилии
        public string GetSeName => SeName;

        //Получить имена
        public string GetName => Name;

        //Вывод персоны
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

            input = inputFi.Split(' ');

            Name = input[1];
            SeName = input[0];
        }

        public virtual void Show() {
            Console.WriteLine(SeName + " " + Name);
        }

        //Ввод персоны
        public string Return_SeName() {
            return SeName;
        }

        public string Return_Name() {
            return Name;
        }

        public IPerson GetSelfPerson => new Person();

        public Person BasePerson => this;

        public IPerson Create(IPerson person) {
            return new Person((Person) person);
        }

        public int CompareTo(object other) {
            var person = other as Person;
            return string.Compare(GetSeName + " " + GetName, person.GetSeName + " " + person.GetName);
        }
    }
}