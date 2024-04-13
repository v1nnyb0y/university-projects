using System;
using System.Collections.Generic;

namespace Requests.Interfaces
{
    /// <summary>
    ///     Interface for the select LINQs
    /// </summary>
    public interface ISelect : IDisposable
    {
        #region Authentication

        /// <summary>
        ///     Check access level
        /// </summary>
        /// <param name="login">Checking login</param>
        /// <param name="password">Checking password</param>
        /// <returns></returns>
        List<object>[] IsAccess
        (
            string login,
            string password
        );

        #endregion

        #region Get Rooms Info

        #region Rooms and Users

        /// <summary>
        ///     Get users in one room with accesses
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns>List of the users and access</returns>
        List<object[]> GetUsersInRoom
        (
            int roomId
        );

        #endregion

        #region Get room model

        /// <summary>
        /// Get room model
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        byte[] GetRoomModel
        (
            int roomId
        );

        /// <summary>
        /// Get controllers in the room
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        List<object[]> GetRoomSensors
        (
            int roomId
        );

        List<object[]> GetManipulatedRooms
        (
            int roomId
        );

        #endregion

        #endregion

        #region Add new user

        /// <summary>
        ///     Check user exist
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Exist user?</returns>
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