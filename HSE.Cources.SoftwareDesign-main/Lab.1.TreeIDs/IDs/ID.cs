using System;
using System.Xml.Serialization;
using Lab._1.TreeIDs.IDs.Enum;

namespace Lab._1.TreeIDs.IDs
{
    [Serializable]
    public abstract class ID
    {
        public string GetName { get; set; } //Получить/изменить имя идентификатора

        [XmlIgnore]
        public ID_Type GetType { get; set; } //Получить/изменить тип идентификатора

        /// <summary>
        ///     Вычисление хэш-кода
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return GetName.GetHashCode();
        }

        public override string ToString() {
            return GetName + " | " + GetType;
        }

        #region Landscapes

        #endregion

        #region Construction

        /// <summary>
        ///     Конструктор без параметров
        /// </summary>
        public ID() {
            GetName = "Id";
            GetType = ID_Type.VARS;
        }

        /// <summary>
        ///     Конструктор с параметром(тип)
        /// </summary>
        /// <param name="idClassif">Тип идентификатора</param>
        public ID(ID_Type idClassif) {
            GetName = "Id";
            GetType = idClassif;
        }

        /// <summary>
        /// </summary>
        /// <param name="idName">Название идентификатора</param>
        /// <param name="idClassif">Тип идентификатора</param>
        public ID(string idName, ID_Type idClassif) {
            GetName = idName;
            GetType = idClassif;
        }

        #endregion
    }
}