namespace SmartHouse_Control.Requests
{
    /// <summary>
    ///     Interface for insert functions
    /// </summary>
    internal interface IInsert
    {
        /// <summary>
        ///     Set new value to the time series
        /// </summary>
        /// <param name="id"></param>
        /// <param name="new_value"></param>
        void set_new_value(int id, string new_value);

        /// <summary>
        ///     Create address
        /// </summary>
        /// <returns>id of the address</returns>
        int new_address();

        /// <summary>
        ///     Create new person and user (user because i need id to encrypt password)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="second_name">Second name</param>
        /// <param name="last_name">Last name</param>
        /// <param name="id_address">Address id</param>
        /// <returns>id_user</returns>
        int new_person_user(string name, string second_name, string last_name, int id_address);

        /// <summary>
        ///     Insert new string to roommodel
        /// </summary>
        /// <param name="user_id">user id</param>
        /// <param name="room_id">room id</param>
        /// <returns></returns>
        int insert_new_user_to_room(int user_id, int room_id);

        /// <summary>
        ///     Insert new access
        /// </summary>
        /// <param name="room_model_id"></param>
        /// <returns></returns>
        void insert_new_access(int room_model_id);
    }
}