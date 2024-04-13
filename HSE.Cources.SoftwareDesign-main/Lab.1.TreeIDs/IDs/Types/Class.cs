using System;
using System.Xml.Serialization;
using Lab._1.TreeIDs.IDs.Enum;

namespace Lab._1.TreeIDs.IDs.Types
{
    /// <summary>
    ///     Идентификатор класса
    /// </summary>
    [Serializable]
    public class Class : ID
    {
        #region Landscape

        private readonly VarType Type = VarType.class_type;

        #endregion

        public override string ToString() {
            return base.ToString() + " | " + Type;
        }

        #region Constructions

        /// <summary>
        ///     Конструктор без параметра
        /// </summary>
        public Class() : base(ID_Type.CLASSES) { }

        /// <summary>
        ///     Конструктор без парметра
        /// </summary>
        /// <param name="name">Имя идентификтора: класс</param>
        public Class(string name) : base(name, ID_Type.CLASSES) { }

        #endregion
    }
}