using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Lab._1.TreeIDs.IDs.Enum;

namespace Lab._1.TreeIDs.IDs.Types
{
    /// <summary>
    ///     Идентификатор метод
    /// </summary>
    [Serializable]
    public class Method : ID
    {
        public List<Vars> GetList { get; set; } //Получить/изменить лист

        public VarType GeType { get; set; } //Получить/изменить тип идентификатора

        public override string ToString() {
            var str = "";
            foreach (var element in GetList) str = string.Concat(str, element.ToString());
            return base.ToString() + " | " + GeType + " | " + str;
        }

        #region Landscape

        #endregion

        #region Construction

        /// <summary>
        ///     Создание экземпляра класса без параметра
        /// </summary>
        public Method() : base(ID_Type.METHODS) { }

        /// <summary>
        ///     Создание экземпляра класса с параметрами
        /// </summary>
        /// <param name="name">Имя идентификатора</param>
        /// <param name="_type">Тип идентификатора</param>
        /// <param name="_list">Лист идентификаторов</param>
        public Method(string name, VarType _type, List<Vars> _list) : base(name, ID_Type.METHODS) {
            GeType = _type;
            GetList = _list;
        }

        #endregion
    }
}