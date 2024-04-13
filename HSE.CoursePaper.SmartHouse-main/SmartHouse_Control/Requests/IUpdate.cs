namespace SmartHouse_Control.Requests
{
    /// <summary>
    ///     Interface with functions for updating information on data base
    /// </summary>
    internal interface IUpdate
    {
        #region NewAccesses

        /// <summary>
        ///     Update accesses
        /// </summary>
        /// <param name="accesses">New accesses</param>
        void update_accesses(string[] accesses, int id_room);

        #endregion

        #region Set new settings

        /// <summary>
        ///     Set new cfg
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="new_cfg">New cfg</param>
        void update_cfg(int id, string new_cfg);

        #endregion

        #region Update status

        /// <summary>
        ///     Update status of the sensor
        /// </summary>
        /// <param name="id_sensor">Sensor id</param>
        /// <param name="new_status">New status</param>
        void update_status(int id_sensor, string new_status);

        #endregion

        #region Update for the insert

        /// <summary>
        ///     Insert password and login
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        void insert_user(string login, string password, int id_user);

        #endregion

        #region Personal/User info

        /// <summary>
        ///     Update name
        /// </summary>
        /// <param name="new_name">New name</param>
        /// <param name="id_person">Person ID</param>
        void update_name(string new_name, int id_person);

        /// <summary>
        ///     Update second name
        /// </summary>
        /// <param name="new_second_name">New second name</param>
        /// <param name="id_person">Person ID</param>
        void update_second_name(string new_second_name, int id_person);

        /// <summary>
        ///     Update last name
        /// </summary>
        /// <param name="new_last_name">New last name</param>
        /// <param name="id_person">Person ID</param>
        void update_last_name(string new_last_name, int id_person);

        /// <summary>
        ///     Update address
        /// </summary>
        /// <param name="new_address">New address</param>
        /// <param name="id_address">ID address</param>
        void update_address(string new_address, int id_address);

        /// <summary>
        ///     Update phone number
        /// </summary>
        /// <param name="new_phone">New phone number</param>
        /// <param name="id_person">Person ID</param>
        void update_phone(string new_phone, int id_person);

        /// <summary>
        ///     Update mail
        /// </summary>
        /// <param name="new_mail">New e-mail</param>
        /// <param name="id_user">Person ID</param>
        void update_mail(string new_mail, int id_user);

        /// <summary>
        ///     Update organisation
        /// </summary>
        /// <param name="new_organisation">New organisation</param>
        /// <param name="id_user">Person ID</param>
        void update_organisation(string new_organisation, int id_user);

        /// <summary>
        ///     Update function
        /// </summary>
        /// <param name="new_function">New function</param>
        /// <param name="id_user">Person ID</param>
        void update_function(string new_function, int id_user);

        /// <summary>
        ///     Update avatar
        /// </summary>
        /// <param name="new_avatar">New avatar</param>
        /// <param name="id_user">Person ID</param>
        void update_avatar(object new_avatar, int id_user);

        /// <summary>
        ///     Update password
        /// </summary>
        /// <param name="new_password">New password</param>
        /// <param name="id_user">Person ID</param>
        void update_password(string new_password, int id_user);

        #endregion
    }
}