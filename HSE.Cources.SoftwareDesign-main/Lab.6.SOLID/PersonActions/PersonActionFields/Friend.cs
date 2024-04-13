using System;
using Lab._6.SOLID.Handler;

namespace Lab._6.SOLID.PersonActions.PersonActionFields
{
    public class Friend : Person,
                          IField
    {
        #region Fields

        #endregion

        #region Initialize

        public Friend
        (
            Person sourcePerson
        )
            : base
                (
                 sourcePerson.FIO,
                 sourcePerson.Birthday,
                 sourcePerson.Gender,
                 sourcePerson.School,
                 sourcePerson.University,
                 sourcePerson.Family
                ) { }

        public Friend
        (
            string   fio,
            DateTime bp,
            string   gender,
            string   school,
            string   university,
            string   family
        )
            : base
                (
                 fio,
                 bp,
                 gender,
                 school,
                 university,
                 family
                ) { }

        #endregion

        #region Actions

        public void Add
        (
            object sourcePerson
        ) {
            var person = (Person) sourcePerson;
            person.OnCollectionHandlerEvent
                (
                 sourcePerson,
                 new CollectionHandlerEventArgs
                     (
                      person.FIO,
                      "Новости, Добавление друга",
                      DateTime.Now,
                      $"{person.FIO} подписался на {FIO}"
                     )
                );
        }

        public void Remove
        (
            object sourcePerson
        ) {
            var person = (Person) sourcePerson;
            person.OnCollectionHandlerEvent
                (
                 sourcePerson,
                 new CollectionHandlerEventArgs
                     (
                      person.FIO,
                      "Новости, Удалил друга",
                      DateTime.Now,
                      $"{person.FIO} удалил друга {FIO}"
                     )
                );
        }

        #endregion
    }
}