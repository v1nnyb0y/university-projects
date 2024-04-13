using System;
using System.Xml.Serialization;
using Lab._1.TreeIDs.IDs.Enum;

namespace Lab._1.TreeIDs.IDs.Types
{
    /// <summary>
    ///     Идентификатор const
    /// </summary>
    [Serializable]
    public class Constant : ID
    {
        public string GetValue { get; set; } //Получить/изменить значение const

        public VarType GetVarType { get; set; } //Получить/изменить тип идентификатора: const

        public override string ToString() {
            return base.ToString() + " | " + GetVarType + " | " + GetValue;
        }

        #region Landscape

        #endregion

        #region Constructions

        /// <summary>
        ///     Создание экземпляр класса без параметров
        /// </summary>
        public Constant() : base(ID_Type.CONSTS) {
            GetValue = "";
            GetVarType = VarType.Default;
        }

        /// <summary>
        ///     Создание экземпляра класса с параметрами
        /// </summary>
        /// <param name="name">Имя идентификатора</param>
        /// <param name="type">Тип переменной идентификатора</param>
        /// <param name="_value">Значение идентификатора</param>
        public Constant(string name, VarType type, string _value) : base(name, ID_Type.CONSTS) {
            GetValue = _value;
            GetVarType = type;
        }

        #endregion
    }
}