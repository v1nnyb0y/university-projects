namespace SmartHouse_Control.Session
{
    /// <summary>
    ///     Class fot room name
    /// </summary>
    public class room
    {
        #region Fields

        #endregion

        #region Initialize

        /// <summary>
        ///     Initialize class
        /// </summary>
        public room() { }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="_name">Room name</param>
        /// <param name="_access_lvl">Lvl of access in this room</param>
        public room(string _name,
            string _access_lvl,
            int room_id,
            string _settings) {
            get_name = _name;
            get_access = _access_lvl;
            get_room_id = room_id;
            Settings = _settings;
        }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="_copy">Copy element</param>
        public room(room _copy) {
            get_name = _copy.get_name;
            get_access = _copy.get_access;
            get_room_id = _copy.get_room_id;
            Settings = _copy.Settings;
        }

        #endregion

        #region Usual methods

        /// <summary>
        ///     Get room name
        /// </summary>
        public string get_name { get; }

        /// <summary>
        ///     Get access level
        /// </summary>
        public string get_access { get; }

        /// <summary>
        ///     Get room id
        /// </summary>
        public int get_room_id { get; }

        /// <summary>
        ///     Get or set settings
        /// </summary>
        public string Settings { get; set; }

        #endregion
    }
}