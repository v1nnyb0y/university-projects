using System;
using System.Runtime.Remoting.Messaging;
namespace Lab._3.SocialNetwork
{
    internal class Program
    {
        private static void start() {
            while (true) {
                Console.Write("Possible commands: start and exit: ");
                var read = Console.ReadLine();
                if (read == "start") {
                    Person person_1 = new Person("Старкова Татьяна Сергеевна", new DateTime(2000, 2, 24), "Женский", "Гимназия 17", "ПНИПУ", "В отношениях");
                    Person person_2 = new Person("Бетц Карина Львовна", new DateTime(1984, 1, 11), "Женский", "Школа 1", "ПГНИУ", "Замужем");
                    Person person_3 = new Person("Шмидт Николай Александрович", new DateTime(1972, 9,16), "Мужской", "Школа 8", "МГУ", "Женат");
                    Person person_4 = new Person("Иванов Кирилл Викторович", new DateTime(1997,2,22), "Мужской", "Лицей 1", "ПГНИУ", "Свободен");
                    person_2.add_friend(ref person_1);
                    person_3.add_picture("добавила фото с одноклассниками");
                    person_2.add_friend(ref person_3);
                    person_1.add_picture("фото тульпанов");
                    person_1.add_friend(ref person_3);
                    person_1.add_friend(ref person_2);
                    person_2.add_news("песня эминема очень крута");
                    person_3.add_friend(ref person_2);
                    person_4.add_friend(ref person_1);
                    person_2.get_university = "ПНИПУ";
                    person_4.add_news("продаю котят: рыжего и черного");
                    person_2.add_friend(ref person_4);
                    person_4.add_friend(ref person_2);
                    person_1.remove_friend(ref person_2);
                    person_4.get_family = "Встречаюсь";
                    person_3.add_news("ищу прогера в команду");
                    person_2.add_picture("фото с концерта");
                    person_2.remove_friend(ref person_1);
                    person_1.get_family = "Помолвлена";
                    person_3.add_news("новый альбом алегровой супер");
                    person_3.add_friend(ref person_4);
                    person_1.add_news("скоро весна");
                    person_4.add_news("котята куплены");
                    person_3.remove_news("новый альбом алегровой супер");
                    person_2.remove_picture("фото с концерта");
                    person_2.remove_friend(ref person_3);
                    person_2.remove_friend(ref person_4);
                    person_4.get_bp = new DateTime(1972, 3, 22);
                    person_3.get_family = "Разведен";
                    person_2.get_fio = "Смирнова Карина Львовна";
                    person_2.add_picture("фото с питомцем");
                    person_4.add_picture("фото котят с новыми хозяевами");
                    Console.WriteLine(person_1.Show());
                    Console.WriteLine(person_2.Show());
                    Console.WriteLine(person_3.Show());
                    Console.WriteLine(person_4.Show());
                }
                if (read == "exit") return;
            }
        }
        private static void Main() {
            start();
        }
    }
}