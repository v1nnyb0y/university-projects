using System;
using ReadLib;

namespace Lab._11
{
    internal class Program
    {
        private static readonly string[] text_MainMenu =
            {
                "Часть 1. Проверка на вирутальные классы. Составление иерархии классов.",
                "Часть 2. Выполнение запросов.",
                "Часть 3. Использовение интерфейсов/абстрактных классов",
                "Выход."
            };

        private static readonly string text_end = "Для продолжения нажмите Enter...";

        private static readonly string[] text_Part2 =
            {
                "Вывести ФИ студентов заданного курса.",
                "Вывести ФИ и должность преподавателей заданной кафедры.",
                "Вывести кол-во студентов, сдавших экзамен на отлично.",
                "Вывести кол-во сотрудников указанной кафедры.",
                "Вывод массива.",
                "Назад."
            };

        private static readonly string[] text_Part3 =
            {
                "Сортировать массив.",
                "Поиск элемента в массиве.",
                "Клонирование массива.",
                "Печать массива.",
                "Назад."
            };

        private static readonly string[] text_Part3_1 =
            {
                "Найти студента.",
                "Найти сотрудника.",
                "Найти преподавателя.",
                "Назад."
            };

        //Рандомное имя
        private static readonly string[] random_name =
            ("Харлам Аким Марьян Давыд Аникей Аврамий Демьян Егорий Софрон Кирсан Мина Лука Филат Гурий Игнат Пимен Леонтий " +
             "Пётр Аврамий Иларион Власий Захар Фаддей Влас Увар Мина Фаддей Ануфрий Лонгин Автоном").Split(' ');

        //Рандомная фамилия
        private static readonly string[] random_se_name =
            ("Панин Качалов Жилин Кисель Туманский Янковский Кондратьев Теряев Несвицкий Калугин Дьяченко Сомов Власовский " +
             "Баташев Перхуров Савелов Троекуров Ашанин Бурунов Чернопятов Боярский Шеховцов Глебов Висленев Назимов Булдаков " +
             "Гарюшкин Головачёв Мокеев Кирпонос Аверченко Рахманинов").Split(' ');

        //Рандомная кафедра
        private static readonly string[] random_department =
            {
                "Военная кафедра", "Кафедра высшей математики",
                "Кафедра физического воспитания", "Школа бизнеса и делового администрирования",
                "Школа логистики", "Школа бизнес-информатики", "Институс менеджмента инноваций",
                "Международный центр подготовки кадров в области логистики",
                "Высшая школа управления проектами", "Институт коммуникационного менеджмента",
                "Центр корпоративного управления", "Высшая школа маркетинга и развития бизнеса",
                "Международный институт управления и бизнеса"
            };

        //Рандомная должность
        private static readonly string[] random_office =
            {
                "Аспирант", "Ассистент", "Ведущий научный сотрудник", "Главный научный сотрудник",
                "Докторант", "Доцент", "Младший научный сотрудник", "Научный сотрудник", "Преподаватель",
                "Профессор", "Старший преподаватель", "Стажер", "Старший научный сотрудник"
            };

        //Переменная для рандома
        private static readonly Random rnd = new Random();

        //Вывод массива
        private static void OutputArray(IPerson[] personArr) {
            foreach (var person1 in personArr) {
                var person = (Person) person1;
                person.Show();
            }
        }

        //Создать массив
        private static IPerson[] CreateArray() {
            IPerson[] person = new Person[100];
            for (var i = 0; i < person.Length; i++) {
                var check = rnd.Next(1, 4);
                if (check == 1) {
                    person[i] = new Student(random_name[rnd.Next(0, random_name.Length)],
                        random_se_name[rnd.Next(0, random_se_name.Length)],
                        rnd.Next(1, 5), rnd.Next(1, 11));
                }
                else {
                    if (check == 2)
                        person[i] = new Associate(random_name[rnd.Next(0, random_name.Length)],
                            random_se_name[rnd.Next(0, random_se_name.Length)],
                            random_department[rnd.Next(0, random_department.Length)]);
                    else
                        person[i] = new Teacher(random_name[rnd.Next(0, random_name.Length)],
                            random_se_name[rnd.Next(0, random_se_name.Length)],
                            random_department[rnd.Next(0, random_department.Length)],
                            random_office[rnd.Next(0, random_office.Length)]);
                }
            }

            return person;
        }

        //Из Person в Associate
        private static Associate[] ToAssociate(IPerson[] person) {
            Associate[] associates = null;
            foreach (var person1 in person) {
                var man = (Person) person1;
                if (man is Associate associate) {
                    if (associates == null)
                        associates = new Associate[1];
                    else
                        Array.Resize(ref associates, associates.Length + 1);

                    associates[associates.Length - 1] = associate;
                }
            }

            return associates;
        }

        #region Задание_1

        //Часть 1
        private static void Case_1() {
            var persons = CreateArray();

            OutputArray(persons);
        }

        #endregion

        private static void Main() {
            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, text_MainMenu);
                switch (sw) {
                    case 1:
                        Case_1();

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 2:
                        Case_2();
                        break;
                    case 3:
                        Case_3();
                        break;
                    case 4:
                        ok = false;
                        break;
                }
            }
        }

        #region Задание_2

        //ФИ студентов заданного курса
        private static void DegreeStudents(IPerson[] person) {
            var count = 0;
            Console.Write("Ввести курс: ");
            var inputDegree = ReadLib.ReadLib.ReadVGran(1, 4);

            foreach (var person1 in person) {
                var man = (Person) person1;
                if (man is Student student)
                    if (student.GetDegree == inputDegree) {
                        man.Show();
                        count++;
                    }
            }

            if (count == 0) Console.WriteLine("Студентов заданного курса нет.");
        }

        //ФИ преподов и их должность указанной кафедры
        private static void DepartmentTeacher(IPerson[] person) {
            var count = 0;
            Console.Write("Введите наименование кафедры: ");
            var inputDepartment = Console.ReadLine();
            foreach (var person1 in person) {
                var man = (Person) person1;
                if (man is Teacher teacher)
                    if (teacher.GetDepartment == inputDepartment) {
                        count++;
                        man.Show();
                    }
            }

            if (count == 0) Console.WriteLine("На заданной кафедре не числиться ни одного преподавателя.");
        }

        //Кол-во студентов, сдавших экзамен на отлично
        private static void GoodStudents(IPerson[] person) {
            var count = 0;

            foreach (var person1 in person) {
                var man = (Person) person1;
                if (man is Student student)
                    if (student.GetMark >= 8)
                        count++;
            }

            Console.WriteLine("Кол-во студентов, сдавших экзамен на отлично: " + count);
        }

        //Преподавателей указанной кафедры
        private static void NumberAssociaters(IPerson[] person) {
            var count = 0;
            Console.Write("Введите наименование кафедры: ");
            var inputDepartment = Console.ReadLine();

            foreach (var person1 in person) {
                var man = (Person) person1;
                if (man is Associate associate)
                    if (associate.GetDepartment == inputDepartment)
                        count++;
            }

            Console.WriteLine("Кол-во преподавателей указанной кафедры: " + count);
        }

        //Часть 2
        private static void Case_2() {
            var person = CreateArray();

            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, text_Part2);
                switch (sw) {
                    case 1:
                        DegreeStudents(person);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 2:
                        DepartmentTeacher(person);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 3:
                        GoodStudents(person);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 4:
                        NumberAssociaters(person);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 5:
                        OutputArray(person);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 6:
                        ok = false;
                        break;
                }
            }
        }

        #endregion

        #region Задание_3

        //Сортировка массива
        private static void SortArray(IPerson[] person) {
            Console.WriteLine("Сортировка массива по фамилии студента: \n");
            Array.Sort(person);
            foreach (var man in person) man.Show();
        }

        //Поиск студента
        private static void FindStudent(IPerson[] person) {
            Array.Sort(person);

            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, text_Part3_1);
                switch (sw) {
                    case 1:
                        var student = new Student();
                        student.Input();

                        var index = Array.BinarySearch(person, student);
                        try {
                            var tmp = person[index] as Student;
                            Console.Write("Студент: ");
                            tmp.Show();
                            Console.Write("Номер стеднта в массиве: " + index);
                        }
                        catch {
                            Console.Write("Студент: ");
                            student.Show();
                            Console.Write("Отсутствует в массиве.");
                        }

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 2:
                        var associate = new Associate();
                        associate.Input();

                        index = Array.BinarySearch(person, associate);
                        try {
                            var tmp = person[index] as Associate;
                            Console.Write("Работник: ");
                            tmp.Show();
                            Console.Write("Номер работника в массиве: " + index);
                        }
                        catch {
                            Console.Write("Работник: ");
                            associate.Show();
                            Console.Write("Отсутствует в массиве.");
                        }

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 3:
                        var teacher = new Teacher();
                        teacher.Input();

                        index = Array.BinarySearch(person, teacher);
                        try {
                            var tmp = person[index] as Teacher;
                            Console.Write("Преподаватель: ");
                            tmp.Show();
                            Console.Write("Номер преподавателя в массиве: " + index);
                        }
                        catch {
                            Console.Write("Преподаватель: ");
                            teacher.Show();
                            Console.Write("Отсутствует в массиве.");
                        }

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 4:
                        ok = false;
                        break;
                }
            }
        }

        //Клонирование
        private static void Clone(IPerson[] person) {
            var associate = ToAssociate(person);

            Associate clone;

            Console.Write("Для поверхностного копирования элемент: ");
            associate[0].Show();
            Console.WriteLine("");

            Console.Write("Для глубокого копирования элемент: ");
            associate[1].Show();
            Console.WriteLine("");

            Console.Write("Поверхностное копирование: ");
            clone = associate[0].SurfaceCopying();
            clone.Show();
            Console.WriteLine("");

            Console.Write("Глубокое копирование: ");
            clone = (Associate) associate[1].Clone();
            clone.Show();
            Console.WriteLine("");
        }

        //Часть 3
        private static void Case_3() {
            var person = CreateArray();

            var ok = true;
            while (ok) {
                var sw = Print.Menu(0, text_Part3);
                switch (sw) {
                    case 1:
                        SortArray(person);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 2:
                        FindStudent(person);
                        break;
                    case 3:
                        var mans = CreateArray();
                        Clone(mans);

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 4:
                        foreach (var man in person) man.Show();

                        Console.WriteLine("\n" + text_end);
                        Console.ReadLine();
                        break;
                    case 5:
                        ok = false;
                        break;
                }
            }
        }

        #endregion
    }
}