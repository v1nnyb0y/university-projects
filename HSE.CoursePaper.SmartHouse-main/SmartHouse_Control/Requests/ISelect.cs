using System;
using System.Collections.Generic;
using System.Data;

namespace SmartHouse_Control.Requests
{
    /// <summary>
    ///     Interface for the functions with SELECT commands
    /// </summary>
    internal interface ISelect
    {
        #region Getting info for change

        /// <summary>
        ///     Get all users in the room
        /// </summary>
        /// <param name="id_room">ID room</param>
        /// <returns></returns>
        DataTable get_all_users(int id_room);

        #endregion

        #region Get QBE

        /// <summary>
        ///     Do new select
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        DataTable do_select(string select);

        #endregion

        #region Check existed user

        /// <summary>
        ///     Is user exist
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        bool is_existed(string login, string password);

        #endregion

        #region Getting Start info

        /// <summary>
        ///     Get access
        /// </summary>
        /// <param name="id_user">User ID</param>
        /// <returns></returns>
        List<object[]> get_access(int id_user);

        /// <summary>
        ///     Check access to the application
        /// </summary>
        /// <param name="login">Entered login</param>
        /// <param name="password">Entered password</param>
        /// <returns></returns>
        object[] is_access(string login, string password);

        /// <summary>
        ///     Get the address
        /// </summary>
        /// <param name="id_address">Address ID</param>
        /// <returns></returns>
        object[] get_address(int id_address);

        #endregion

        #region Get info for room menu

        /// <summary>
        ///     get room sensors
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <returns></returns>
        List<object[]> get_room_sensors(int id_room);

        /// <summary>
        ///     Get room model
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <param name="settings">Settings of the model</param>
        /// <returns></returns>
        string get_model(int id_room);


        /// <summary>
        ///     Get room model
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <param name="settings">settings</param>
        /// <returns></returns>
        Tuple<string, string> get_curr_model(int id_room);

        #endregion

        #region Get Conrol and data menu info

        /// <summary>
        ///     Get groups
        /// </summary>
        string[] get_sensor_group();

        /// <summary>
        ///     Get controllers
        /// </summary>
        /// <param name="id_room">Room id</param>
        /// <returns></returns>
        List<object[]> get_room_sensors_controller(int id_room);

        #endregion
    }
}