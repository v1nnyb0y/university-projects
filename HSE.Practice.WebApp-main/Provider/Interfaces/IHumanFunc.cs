using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Provider.Handlers;

namespace Provider.Interfaces
{
    /// <summary>
    ///     Interface for communication with User/Person classes
    /// </summary>
    public interface IHumanFunc
    {
        #region Authentication

        /// <summary>
        ///     Check level of the access to the application
        /// </summary>
        /// <param name="args">Login and Password</param>
        /// <returns></returns>
        Tuple<object, bool, CurrentProvider> CheckAccess
        (
            AccessHandler args
        );

        #endregion

        #region Updating information

        /// <summary>
        ///     Update information
        /// </summary>
        /// <param name="args">Arguments</param>
        void UpdateAuthorizedHuman
        (
            NewPersonalInfoHandler args
        );

        #endregion

        #region Delete user from db

        /// <summary>
        ///     Delete user from the db
        /// </summary>
        /// <param name="addressId">Address Id</param>
        void DeleteUserAsync
        (
            int addressId
        );

        #endregion

        #region Add new user

        /// <summary>
        ///     Add new user to db
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <param name="roomId"></param>
        /// <param name="fio">FIO of the user</param>
        List<object> AddNewUserAsync
        (
            string          login,
            string          password,
            int             roomId,
            params string[] fio
        );

        /// <summary>
        ///     Check user on exist
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Exist user</returns>
        bool IsUserExist
        (
            string login,
            string password
        );

        /// <summary>
        ///     Check user on exist
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">Password</param>
        /// <returns>Exist user</returns>
        bool IsUserExist
        (
            int userId,
            string password
        );

        #endregion
    }
}