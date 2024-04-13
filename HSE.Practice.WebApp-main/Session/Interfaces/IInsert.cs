using System;
using System.Collections.Generic;

namespace Requests.Interfaces
{
    /// <summary>
    ///     Interface for the insert LINQs
    /// </summary>
    public interface IInsert : IDisposable
    {
        #region Add new user

        /// <summary>
        ///     Add new user
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <param name="roomId">Room Id</param>
        /// <param name="fio">FIO of the added user</param>
        List<object> AddNewUser
        (
            string login,
            string password,
            int roomId,
            params string[] fio
        );

        #endregion

        #region Insert new data of the sensor

        void InsertNewValue
        (
            int    optionId,
            string newValue
        );

        #endregion
    }
}