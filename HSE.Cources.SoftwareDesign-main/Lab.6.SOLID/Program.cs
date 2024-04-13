using System;
using Lab._6.SOLID.PersonActions;
using Lab._6.SOLID.PersonActions.Write;

namespace Lab._6.SOLID
{
    internal class Program
    {
        private static void Start() {
            while (true) {
                Console.Write
                    (
                     "Possible commands: start and exit: "
                    );
                var read = Console.ReadLine();
                switch (read) {
                    case "start":
                        {
                            #region Add users

                            var starkova = new Person
                                (
                                 "Старкова Татьяна Сергеевна",
                                 new DateTime
                                     (
                                      2000,
                                      2,
                                      24
                                     ),
                                 "Женский",
                                 "Гимназия 17",
                                 "ПНИПУ",
                                 "В отношениях"
                                );
                            var betc = new Person
                                (
                                 "Бетц Карина Львовна",
                                 new DateTime
                                     (
                                      1984,
                                      1,
                                      11
                                     ),
                                 "Женский",
                                 "Школа 1",
                                 "ПГНИУ",
                                 "Замужем"
                                );
                            var shmidt = new Person
                                (
                                 "Шмидт Николай Александрович",
                                 new DateTime
                                     (
                                      1972,
                                      9,
                                      16
                                     ),
                                 "Мужской",
                                 "Школа 8",
                                 "МГУ",
                                 "Женат"
                                );
                            var ivanov = new Person
                                (
                                 "Иванов Кирилл Викторович",
                                 new DateTime
                                     (
                                      1997,
                                      2,
                                      22
                                     ),
                                 "Мужской",
                                 "Лицей 1",
                                 "ПГНИУ",
                                 "Свободен"
                                );

                            #endregion

                            #region Actions

                            betc.AddFriend
                                (
                                 ref starkova
                                );
                            shmidt.AddPicture
                                (
                                 "добавила фото с одноклассниками"
                                );
                            betc.AddFriend
                                (
                                 ref shmidt
                                );
                            starkova.AddPicture
                                (
                                 "фото тульпанов"
                                );
                            starkova.AddFriend
                                (
                                 ref shmidt
                                );
                            starkova.AddFriend
                                (
                                 ref betc
                                );
                            betc.AddNews
                                (
                                 "песня эминема очень крута"
                                );
                            shmidt.AddFriend
                                (
                                 ref betc
                                );
                            ivanov.AddFriend
                                (
                                 ref starkova
                                );
                            betc.University = "ПНИПУ";
                            ivanov.AddNews
                                (
                                 "продаю котят: рыжего и черного"
                                );
                            betc.AddFriend
                                (
                                 ref ivanov
                                );
                            ivanov.AddFriend
                                (
                                 ref betc
                                );
                            starkova.RemoveFriend
                                (
                                 ref betc
                                );
                            ivanov.Family = "Встречаюсь";
                            shmidt.AddNews
                                (
                                 "ищу прогера в команду"
                                );
                            betc.AddPicture
                                (
                                 "фото с концерта"
                                );
                            betc.RemoveFriend
                                (
                                 ref starkova
                                );
                            starkova.Family = "Помолвлена";
                            shmidt.AddNews
                                (
                                 "новый альбом алегровой супер"
                                );
                            shmidt.AddFriend
                                (
                                 ref ivanov
                                );
                            starkova.AddNews
                                (
                                 "скоро весна"
                                );
                            ivanov.AddNews
                                (
                                 "котята куплены"
                                );
                            shmidt.RemoveNews
                                (
                                 "новый альбом алегровой супер"
                                );
                            betc.RemovePicture
                                (
                                 "фото с концерта"
                                );
                            betc.RemoveFriend
                                (
                                 ref shmidt
                                );
                            betc.RemoveFriend
                                (
                                 ref ivanov
                                );
                            ivanov.Birthday = new DateTime
                                (
                                 1972,
                                 3,
                                 22
                                );
                            shmidt.Family = "Разведен";
                            betc.FIO      = "Смирнова Карина Львовна";
                            betc.AddPicture
                                (
                                 "фото с питомцем"
                                );
                            ivanov.AddPicture
                                (
                                 "фото котят с новыми хозяевами"
                                );

                            #endregion

                            IPrinter printer = new ConsolePrinter();

                            starkova.Show
                                (
                                 printer
                                );
                            betc.Show
                                (
                                 printer
                                );
                            shmidt.Show
                                (
                                 printer
                                );
                            ivanov.Show
                                (
                                 printer
                                );
                            break;
                        }
                    case "exit": return;
                }
            }
        }

        private static void Main() {
            Start();
        }
    }
}