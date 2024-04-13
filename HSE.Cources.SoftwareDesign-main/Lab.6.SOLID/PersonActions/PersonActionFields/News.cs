using System;
using Lab._6.SOLID.Handler;

namespace Lab._6.SOLID.PersonActions.PersonActionFields
{
    public class News : IField
    {
        #region Fields

        public string NewsContentMessage;

        #endregion

        #region Initialize

        public News
        (
            string newsContentMessage
        ) {
            NewsContentMessage = newsContentMessage;
        }

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
                      "Новости, Добавление новости",
                      DateTime.Now,
                      $"{NewsContentMessage}"
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
                      "Новости, Удаление новости",
                      DateTime.Now,
                      $"{person.FIO} удалил новость {NewsContentMessage}"
                     )
                );
        }

        #endregion
    }
}