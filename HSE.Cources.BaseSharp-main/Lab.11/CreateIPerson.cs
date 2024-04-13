using System;

namespace Lab._11
{
    public static class CreateIPerson
    {
        //Рандомное имя
        private static readonly string[] RandomName =
            ("Харлам Аким Марьян Давыд Аникей Аврамий Демьян Егорий Софрон Кирсан Мина Лука Филат Гурий Игнат Пимен Леонтий " +
             "Пётр Аврамий Иларион Власий Захар Фаддей Влас Увар Мина Фаддей Ануфрий Лонгин Автоном").Split(' ');

        //Рандомная фамилия
        private static readonly string[] RandomSeName =
            ("Панин Качалов Жилин Кисель Туманский Янковский Кондратьев Теряев Несвицкий Калугин Дьяченко Сомов Власовский " +
             "Баташев Перхуров Савелов Троекуров Ашанин Бурунов Чернопятов Боярский Шеховцов Глебов Висленев Назимов Булдаков " +
             "Гарюшкин Головачёв Мокеев Кирпонос Аверченко Рахманинов").Split(' ');

        //Рандомная кафедра
        private static readonly string[] RandomDepartment =
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
        private static readonly string[] RandomOffice =
            {
                "Аспирант", "Ассистент", "Ведущий научный сотрудник", "Главный научный сотрудник",
                "Докторант", "Доцент", "Младший научный сотрудник", "Научный сотрудник", "Преподаватель",
                "Профессор", "Старший преподаватель", "Стажер", "Старший научный сотрудник"
            };

        //Переменная для рандома
        private static readonly Random Rnd = new Random();

        //Вывод массива
        static CreateIPerson() { }

        public static void OutputArray(Person[] personArr) {
            foreach (var person in personArr) person.Show();
        }

        //Создать массив
        public static IPerson[] CreateArray(int size) {
            var person = new IPerson[size];
            var i = 0;
            while (i < person.Length) {
                var check = Rnd.Next(1, 4);
                IPerson persona;
                if (check == 1) {
                    persona = new Student(RandomName[Rnd.Next(0, RandomName.Length)],
                        RandomSeName[Rnd.Next(0, RandomSeName.Length)],
                        Rnd.Next(1, 5), Rnd.Next(1, 11));
                }
                else {
                    if (check == 2)
                        persona = new Associate(RandomName[Rnd.Next(0, RandomName.Length)],
                            RandomSeName[Rnd.Next(0, RandomSeName.Length)],
                            RandomDepartment[Rnd.Next(0, RandomDepartment.Length)]);
                    else
                        persona = new Teacher(RandomName[Rnd.Next(0, RandomName.Length)],
                            RandomSeName[Rnd.Next(0, RandomSeName.Length)],
                            RandomDepartment[Rnd.Next(0, RandomDepartment.Length)],
                            RandomOffice[Rnd.Next(0, RandomOffice.Length)]);
                }

                if (Contains(persona, person)) { }
                else {
                    person[i] = persona;
                    i++;
                }
            }

            return person;
        }

        public static bool Contains(IPerson element, IPerson[] array) {
            foreach (var person in array) {
                if (person == null) return false;
                if (string.Compare(person.Return_SeName() + " " + person.Return_Name(), element.Return_SeName() + " " +
                                                                                        element.Return_Name(),
                        StringComparison.Ordinal) == 0)
                    return true;
            }

            return false;
        }

        public static IPerson CreateElement<T>() {
            if (typeof(T) == typeof(Teacher))
                return new Teacher(RandomName[Rnd.Next(0, RandomName.Length)],
                    RandomSeName[Rnd.Next(0, RandomSeName.Length)],
                    RandomDepartment[Rnd.Next(0, RandomDepartment.Length)],
                    RandomOffice[Rnd.Next(0, RandomOffice.Length)]);

            if (typeof(T) == typeof(Associate))
                return new Associate(RandomName[Rnd.Next(0, RandomName.Length)],
                    RandomSeName[Rnd.Next(0, RandomSeName.Length)],
                    RandomDepartment[Rnd.Next(0, RandomDepartment.Length)]);
            return new Student(RandomName[Rnd.Next(0, RandomName.Length)],
                RandomSeName[Rnd.Next(0, RandomSeName.Length)],
                Rnd.Next(1, 5), Rnd.Next(1, 11));
        }

        public static void Show<T>(T element) {
            if (typeof(T) == typeof(Teacher)) {
                var teacher = element as Teacher;
                teacher.Show();
            }
            else {
                if (typeof(T) == typeof(Associate)) {
                    var associate = element as Associate;
                    associate.Show();
                }
                else {
                    var student = element as Student;
                    student.Show();
                }
            }
        }
    }
}