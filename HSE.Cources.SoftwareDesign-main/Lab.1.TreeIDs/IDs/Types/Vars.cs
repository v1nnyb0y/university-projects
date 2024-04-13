using System;
using System.Xml.Serialization;
using Lab._1.TreeIDs.IDs.Enum;

namespace Lab._1.TreeIDs.IDs.Types
{
    /// <summary>
    ///     Идентификатор var
    /// </summary>
    [Serializable]
    public class Vars : ID
    {
        public VarType GetType { get; set; } //Получить/изменить тип идентификатора var

        public VarPrefixes GetPrefix { get; set; } //Получить/изменить префик идентификатора var

        public override string ToString() {
            if (GetPrefix == VarPrefixes.none)
                return base.ToString() + " | " + GetType;
            return GetType + "/" + GetPrefix + " ";
        }

        #region Landscape

        #endregion

        #region Construction

        /// <summary>
        ///     Создание экземпляра класса без параметров
        /// </summary>
        public Vars() : base(ID_Type.VARS) { }

        /// <summary>
        ///     Создание экземпляра класса для элемента дерева
        /// </summary>
        /// <param name="name">Имя идентификатора</param>
        /// <param name="_type">Тип идентификатора</param>
        public Vars(string name, VarType _type) : base(name, ID_Type.VARS) {
            GetType = _type;
            GetPrefix = VarPrefixes.none;
        }

        /// <summary>
        ///     Создание экземпляра класса для элемента списка
        /// </summary>
        /// <param name="name">Имя идентификатора</param>
        /// <param name="_type">Тип идентификатора</param>
        /// <param name="_prefix">Префикс идентификатора</param>
        public Vars(string name, VarType _type, VarPrefixes _prefix) : base(name, ID_Type.VARS) {
            GetType = _type;
            GetPrefix = _prefix;
        }

        #endregion
    }
}