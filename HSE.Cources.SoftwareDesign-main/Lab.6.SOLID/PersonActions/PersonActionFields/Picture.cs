using System;
using Lab._6.SOLID.Handler;

namespace Lab._6.SOLID.PersonActions.PersonActionFields
{
    public class Picture : IField
    {
        #region Fields

        public string PictureContentMessage;

        #endregion

        #region Initialize

        public Picture
        (
            string pictureContentMessage
        ) {
            PictureContentMessage = pictureContentMessage;
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
                      "Новости, Добавлена новая фотография",
                      DateTime.Now,
                      $"{PictureContentMessage}"
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
                      "Новости, Удалена фотография",
                      DateTime.Now,
                      $"{person.FIO} удалил фотографию {PictureContentMessage}"
                     )
                );
        }

        #endregion
    }
}