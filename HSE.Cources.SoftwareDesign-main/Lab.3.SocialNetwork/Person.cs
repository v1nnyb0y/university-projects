using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Lab._3.SocialNetwork
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class CollectionHandlerEventArgs : EventArgs
    {
        public CollectionHandlerEventArgs(string source_fio, string changes, DateTime curr_moment, string  additional_info) {
            Source_FIO = source_fio;
            Changes = changes;
            Time = curr_moment;
            INFO = additional_info;
        }

        public string Source_FIO { get; }

        public string Changes { get; }

        public DateTime Time { get; }

        public string INFO { get; }

        public override string ToString() {
            string outp = "";
            outp += "Сообщение о событии у " + Source_FIO + "\n";
            outp += "        Тип события: " + Changes + "\n";
            outp += "        INFO: " + INFO + "\n";
            outp += "        Время: " + Time.ToShortTimeString() + " " + Time.ToShortDateString() + "\n";
            return outp;
        }
    }

    public class Person
    {
        private string FIO;
        private DateTime BP;
        private string gender;
        private string school;
        private string university;
        private string family;

        public event CollectionHandler CollectionHandlerEvent;

        public virtual void OnCollectionHandlerEvent(object source, CollectionHandlerEventArgs args) {
            if (CollectionHandlerEvent != null) {
                CollectionHandlerEvent(source, args);
            }
        }

        public string get_fio {
            get => FIO;
            set {
                OnCollectionHandlerEvent(this,
                    new CollectionHandlerEventArgs(FIO + $"({value})", "Смена личной информаци, ФИО", DateTime.Now, $"ФИО изменено с {FIO} на {value}"));
                FIO = value;
                check_bp(this);
            }
        }

        public DateTime get_bp {
            get => BP;
            set {
                OnCollectionHandlerEvent(this,
                    new CollectionHandlerEventArgs(FIO, "Смена личной информаци, Дата Рождения", DateTime.Now, $"Дата рождения изменена с {BP.Date} на {value.Date}"));
                BP = value;
                check_bp(this);
            }
        }

        public string get_gender {
            get => gender;
            set { gender = value; }
        }

        public string get_school {
            get => school;
            set {
                check_bp(this);
                OnCollectionHandlerEvent(this, 
                    new CollectionHandlerEventArgs(FIO, "Смена личной информаци, Место учебы (начальное)", DateTime.Now, $"Место учебы (школа) изменена с {school} на {value}"));
                school = value;
            }
        }

        public string get_university {
            get => university;
            set {
                check_bp(this);
                OnCollectionHandlerEvent(this, 
                    new CollectionHandlerEventArgs(FIO, "Смена личной информаци, Место учебы (высшее)", DateTime.Now, $"Место учебы (ВУЗ) изменен с {university} на {value}"));
                university = value;
            }
        }

        public string get_family {
            get => family;
            set {
                check_bp(this);
                OnCollectionHandlerEvent(this,
                    new CollectionHandlerEventArgs(FIO, "Смена личной информаци, Семейное положение", DateTime.Now, $"Семейное положение изменено с {family} на {value}"));
                family = value;
            }
        }

        public List<Person> friends;
        public List<string> news_on_wall;
        public List<string> pictures;
        private Journal journal;

        public bool add_friend(ref Person new_friend) {
            friends.Add(new_friend);
            new_friend.CollectionHandlerEvent += journal.CollectionHandlerEvent;
            new_friend.journal.add(new Message(FIO, "Новости, Добавление друга", DateTime.Now, $"{FIO} подписался на вас"));
            OnCollectionHandlerEvent(this, 
               new CollectionHandlerEventArgs(FIO, "Новости, Добавление друга", DateTime.Now, $"{FIO} подписался на {new_friend.FIO}"));
            check_bp(this);
            check_bp(new_friend);

            return true;
        }

        public bool remove_friend(ref Person removing_friend) {
            friends.Remove(removing_friend);
            removing_friend.CollectionHandlerEvent -= journal.CollectionHandlerEvent;
            OnCollectionHandlerEvent(this,
                new CollectionHandlerEventArgs(FIO, "Новости, Удалил друга", DateTime.Now, $"{FIO} удалил друга {removing_friend.FIO}"));
            check_bp(this);

            return true;
        }

        public bool add_news(string new_news) {
            news_on_wall.Add(new_news);
            OnCollectionHandlerEvent(this, 
                new CollectionHandlerEventArgs(FIO, "Новости, Добавление новости", DateTime.Now, $"{new_news}"));
            check_bp(this);

            return true;
        }

        public bool remove_news(string removing_news) {
            news_on_wall.Remove(removing_news);
            OnCollectionHandlerEvent(this,
                new CollectionHandlerEventArgs(FIO, "Новости, Удаление новости", DateTime.Now, $"{FIO} удалил новость {removing_news}"));
            check_bp(this);

            return true;
        }

        public bool add_picture(string name_new_picture) {
            pictures.Add(name_new_picture);
            OnCollectionHandlerEvent(this,
                new CollectionHandlerEventArgs(FIO, "Новости, Добавлена новая фотография", DateTime.Now, $"{name_new_picture}"));
            check_bp(this);

            return true;
        }

        public bool remove_picture(string name_removing_picture) {
            pictures.Remove(name_removing_picture);
            OnCollectionHandlerEvent(this,
                new CollectionHandlerEventArgs(FIO, "Новости, Удалена фотография", DateTime.Now, $"{FIO} удалил фотографию {name_removing_picture}"));
            check_bp(this);

            return true;
        }

        public Person(string _FIO, DateTime _BP, string _gender, string _school, string _university, string _family) {
            FIO = _FIO;
            gender = _gender;
            BP = _BP;
            school = _school;
            university = _university;
            family = _family;
            journal = new Journal(FIO);
            friends = new List<Person>();
            news_on_wall = new List<string>();
            pictures = new List<string>();
        }

        public string Show() {
            return journal.ToString();
        }

        public void check_bp(Person person) {
            if (DateTime.Now.Day == person.BP.Day && DateTime.Now.Month == person.BP.Month) {
                person.OnCollectionHandlerEvent(person,
                    new CollectionHandlerEventArgs(person.FIO, "Новости, День рождения", DateTime.Now, "Сегодня День Рождения!"));
            }
        }
    }
}
