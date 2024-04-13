using System;
using System.Collections.Generic;
using Lab._6.SOLID.Handler;
using Lab._6.SOLID.Journal;
using Lab._6.SOLID.PersonActions.PersonActionFields;
using Lab._6.SOLID.PersonActions.Write;

namespace Lab._6.SOLID.PersonActions
{
    public class Person
    {
        #region Initialize

        public Person
        (
            string   fio,
            DateTime bp,
            string   gender,
            string   school,
            string   university,
            string   family
        ) {
            _fio        = fio;
            Gender      = gender;
            _bp         = bp;
            _school     = school;
            _university = university;
            _family     = family;
            _journal = new Journal.Journal
                (
                 _fio
                );
            Friends    = new List<Friend>();
            NewsOnWall = new List<News>();
            Pictures   = new List<Picture>();
        }

        #endregion

        #region Printers

        public void Show
        (
            IPrinter printer
        ) {
            printer.Print
                (
                 _journal.ToString()
                );
        }

        #endregion

        #region Checks

        private void CheckBirthday
        (
            Person person
        ) {
            if (DateTime.Now.Day   == person._bp.Day &&
                DateTime.Now.Month == person._bp.Month)
                person.OnCollectionHandlerEvent
                    (
                     person,
                     new CollectionHandlerEventArgs
                         (
                          person._fio,
                          "Новости, День рождения",
                          DateTime.Now,
                          "Сегодня День Рождения!"
                         )
                    );
        }

        #endregion

        #region Fields

        private string   _fio;
        private DateTime _bp;
        private string   _school;
        private string   _university;
        private string   _family;


        public           List<Friend>    Friends;
        public           List<News>      NewsOnWall;
        public           List<Picture>   Pictures;
        private readonly Journal.Journal _journal;

        #endregion

        #region Event OnChange

        public event CollectionHandler CollectionHandlerEvent;

        public virtual void OnCollectionHandlerEvent
        (
            object                     source,
            CollectionHandlerEventArgs args
        ) {
            CollectionHandlerEvent?.Invoke
                (
                 source,
                 args
                );
        }

        #endregion

        #region Methods

        public string FIO
            {
                get => _fio;
                set {
                    OnCollectionHandlerEvent
                        (
                         this,
                         new CollectionHandlerEventArgs
                             (
                              _fio + $"({value})",
                              "Смена личной информаци, ФИО",
                              DateTime.Now,
                              $"ФИО изменено с {_fio} на {value}"
                             )
                        );
                    _fio = value;
                    CheckBirthday
                        (
                         this
                        );
                }
            }

        public DateTime Birthday
            {
                get => _bp;
                set {
                    OnCollectionHandlerEvent
                        (
                         this,
                         new CollectionHandlerEventArgs
                             (
                              _fio,
                              "Смена личной информаци, Дата Рождения",
                              DateTime.Now,
                              $"Дата рождения изменена с {_bp.Date} на {value.Date}"
                             )
                        );
                    _bp = value;
                    CheckBirthday
                        (
                         this
                        );
                }
            }

        public string Gender { get; set; }

        public string School
            {
                get => _school;
                set {
                    CheckBirthday
                        (
                         this
                        );
                    OnCollectionHandlerEvent
                        (
                         this,
                         new CollectionHandlerEventArgs
                             (
                              _fio,
                              "Смена личной информаци, Место учебы (начальное)",
                              DateTime.Now,
                              $"Место учебы (школа) изменена с {_school} на {value}"
                             )
                        );
                    _school = value;
                }
            }

        public string University
            {
                get => _university;
                set {
                    CheckBirthday
                        (
                         this
                        );
                    OnCollectionHandlerEvent
                        (
                         this,
                         new CollectionHandlerEventArgs
                             (
                              _fio,
                              "Смена личной информаци, Место учебы (высшее)",
                              DateTime.Now,
                              $"Место учебы (ВУЗ) изменен с {_university} на {value}"
                             )
                        );
                    _university = value;
                }
            }

        public string Family
            {
                get => _family;
                set {
                    CheckBirthday
                        (
                         this
                        );
                    OnCollectionHandlerEvent
                        (
                         this,
                         new CollectionHandlerEventArgs
                             (
                              _fio,
                              "Смена личной информаци, Семейное положение",
                              DateTime.Now,
                              $"Семейное положение изменено с {_family} на {value}"
                             )
                        );
                    _family = value;
                }
            }

        #endregion

        #region Friend Action

        public bool AddFriend
        (
            ref Person newSourceFriend
        ) {
            var newFriend = new Friend
                (
                 newSourceFriend
                );

            Friends.Add
                (
                 newFriend
                );

            newSourceFriend.CollectionHandlerEvent += _journal.CollectionHandlerEvent;

            newSourceFriend._journal.Add
                (
                 new Message
                     (
                      _fio,
                      "Новости, Добавление друга",
                      DateTime.Now,
                      $"{_fio} подписался на вас"
                     )
                );

            newFriend.Add
                (
                 this
                );

            CheckBirthday
                (
                 this
                );

            CheckBirthday
                (
                 newFriend
                );

            return true;
        }

        public bool RemoveFriend
        (
            ref Person removingSourceFriend
        ) {
            var removingFriend = new Friend
                (
                 removingSourceFriend
                );

            Friends.Remove
                (
                 removingFriend
                );

            removingSourceFriend.CollectionHandlerEvent -= _journal.CollectionHandlerEvent;

            removingFriend.Remove
                (
                 this
                );

            CheckBirthday
                (
                 this
                );

            return true;
        }

        #endregion

        #region News Action

        public bool AddNews
        (
            string newNewsContent
        ) {
            var newNews = new News
                (
                 newNewsContent
                );

            NewsOnWall.Add
                (
                 newNews
                );

            newNews.Add
                (
                 this
                );

            CheckBirthday
                (
                 this
                );

            return true;
        }

        public bool RemoveNews
        (
            string removingNewsContent
        ) {
            var removingNews = new News
                (
                 removingNewsContent
                );

            NewsOnWall.Remove
                (
                 removingNews
                );

            removingNews.Remove
                (
                 this
                );

            CheckBirthday
                (
                 this
                );

            return true;
        }

        #endregion

        #region Picture Action

        public bool AddPicture
        (
            string newPictureContent
        ) {
            var newPicture = new Picture
                (
                 newPictureContent
                );

            Pictures.Add
                (
                 newPicture
                );

            newPicture.Add
                (
                 this
                );

            CheckBirthday
                (
                 this
                );

            return true;
        }

        public bool RemovePicture
        (
            string removingPictureContent
        ) {
            var removingPicture = new Picture
                (
                 removingPictureContent
                );

            Pictures.Remove
                (
                 removingPicture
                );

            removingPicture.Remove
                (
                 this
                );

            CheckBirthday
                (
                 this
                );

            return true;
        }

        #endregion
    }
}