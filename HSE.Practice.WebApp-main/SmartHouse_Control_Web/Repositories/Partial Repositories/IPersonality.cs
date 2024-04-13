using System;
using System.Web;
using Provider;

namespace SmartHouse_Control_Web.Repositories.Partial_Repositories
{
    internal interface IPersonality
    {
        #region Authentication

        CurrentProvider Authentication
        (
            string login,
            string password
        );

        #endregion
    }
}