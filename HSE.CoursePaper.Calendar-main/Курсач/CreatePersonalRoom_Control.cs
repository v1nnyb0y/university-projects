using System;

namespace Курсач
{
    internal class CreatePersonalRoom
    {
        public string[] name_sport;

        //Конструктор
        public CreatePersonalRoom(string[] sport) {
            name_sport = sport;
        }

        //Создание рекомендаций
        public void CreateTextRichText(ref string richBox, DateTime age, params string[] s) {
            if (s[0].Length == 0 || age == null || s[1].Length == 0) {
                richBox =
                    "Заполните поля Вес, Рост, Возраст; или Город, Больницы, чтобы узнать рекомендации для себя. Заполните все поля, чтобы информация была" +
                    " полной.";
            }
            else {
                var IMT = Math.Round(int.Parse(s[0]) / Math.Pow(double.Parse(s[1]) / 100, 2), 2);
                richBox = "Ваш ИМТ: " + IMT;
                var pulse_low = (int) Math.Round((220  - (DateTime.Now.Year - age.Year)) * 0.65);
                var pulse_high = (int) Math.Round((220 - (DateTime.Now.Year - age.Year)) * 0.85);

                if (IMT > 25)
                    richBox += "\nВам необходимо сбросить лишний вес.\n"    +
                               "Рекомендуем поддерживать пульс в пределах " + pulse_low  +
                               " - "                                        + pulse_high +
                               " во время выполнения упражнений.";
                else if (IMT < 18.5)
                    richBox += "\nВам необходимо набрать вес.\n" + "Рекомендуем пойти в тренажерный зал.";
                else
                    richBox += "\nВаш вес находится в норме.\n" +
                               "Поддерживайте данный вес, чтобы оставаться в хорошем расположении духа.";
            }
        }

        //Кнопка принятия изменений в личном кабинете
        public bool button_accept(
            bool man, bool woman, string[] checked_items, out string richBox, DateTime age, params string[] s) {
            richBox = null;
            var ok = false;
            if (s[0].Length == 0 || s[1].Length == 0) {
                ok = true;
            }
            else {
            #region change_array

                info.name = s[0];
                info.se_name = s[1];
                info.father_name = s[2];
                info.weight = s[3];
                info.age = age;
                info.growth = s[4];

            #endregion

                info.name_sport = checked_items;

                if (man || woman)
                    if (man)
                        info.sex = "M";
                    else
                        info.sex = "W";

                CreateTextRichText(ref richBox, age, s[3], s[4]);

                info.recomendation = richBox;
            }

            return ok;
        }
    }
}