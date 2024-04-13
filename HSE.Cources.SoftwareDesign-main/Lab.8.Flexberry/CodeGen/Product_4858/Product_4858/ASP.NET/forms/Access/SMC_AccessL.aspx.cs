﻿/*flexberryautogenerated="true"*/
namespace IIS.Product_4858
{
    using System;
    using ICSSoft.STORMNET.Web.Controls;

    using Resources;

    public partial class SMC_AccessL : BaseListForm<Access>
    {
        /// <summary>
        /// Конструктор без параметров,
        /// инициализирует свойства, соответствующие конкретной форме.
        /// </summary>
        public SMC_AccessL() : base(Access.Views.SMC_AccessL)
        {
            EditPage = SMC_AccessE.FormPath;
        }
                
        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/Access/SMC_AccessL.aspx"; }
        }

        /// <summary>
        /// Вызывается самым первым в Page_Load.
        /// </summary>
        protected override void Preload()
        {
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
        }
    }
}