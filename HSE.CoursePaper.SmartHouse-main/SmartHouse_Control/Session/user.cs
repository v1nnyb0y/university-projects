using System.Collections.Generic;
using SmartHouse_Control.Handlers;

namespace SmartHouse_Control.Session
{
    internal delegate void change_info_user(object source, personal_handler args); //Delegate for change info in db 

    /// <summary>
    ///     Class for the current user
    /// </summary>
    public class user : person
    {
        #region Take functions

        /// <summary>
        ///     Set rooms list
        /// </summary>
        /// <param name="arr_rooms">List of the rooms</param>
        public void set_rooms(List<object[]> arr_rooms) {
            room tmp;
            foreach (var var_arr in arr_rooms) {
                tmp = new room(var_arr[1].ToString(),
                    var_arr[2].ToString(),
                    int.Parse(var_arr[0].ToString()),
                    var_arr[3].ToString()); //Create class room
                rooms.Add(tmp); //Add to list
            }
        }

        #endregion

        #region Fields

        private string organisation; //Organization
        private string password; //User password
        private byte[] avatar; //User avatar
        private List<room> rooms = new List<room>(); //List of the rooms

        #endregion

        #region Delegates

        private event change_info_user handler; //Event for change info in db  

        /// <summary>
        ///     Doing subscribed function
        /// </summary>
        /// <param name="args"></param>
        private void OnChange(object source, personal_handler args) {
            handler?.Invoke(source, args);
        }

        #endregion

        #region Initialize

        /// <summary>
        ///     Initialize class
        /// </summary>
        public user() { }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="_id">User ID</param>
        /// <param name="_organisation">Organization</param>
        /// <param name="_access">Access</param>
        /// <param name="_password">Current password</param>
        /// <param name="_avatar">Current avatar</param>
        /// <param name="_name">First name</param>
        /// <param name="_second_name">Second name</param>
        /// <param name="_last_name">Last name</param>
        /// <param name="_phone_number">Phine number</param>
        /// <param name="_e_mail">E-mail</param>
        /// <param name="_address">Address</param>
        public user(int _id,
            string _organisation,
            string _access,
            string _password,
            byte[] _avatar,
            int _ID,
            string _name,
            string _second_name,
            string _last_name,
            string _phone_number,
            string _e_mail,
            string _address,
            int _id_address) : base(_name, _second_name, _last_name, _phone_number, _e_mail, _address, _id_address,
            _ID) {
            get_user_id = _id;
            organisation = _organisation;
            password = _password;
            avatar = _avatar;
            get_access = _access;
            handler += subscribe.update_info;
        }

        /// <summary>
        ///     Initialize class (full copy)
        /// </summary>
        /// <param name="copy">Old user</param>
        public user(user copy)
            : base(copy.set_name, copy.set_second_name, copy.set_last_name, copy.set_phone, copy.set_mail,
                copy.set_address, copy.get_address_id, copy.get_person_id) {
            get_user_id = copy.get_user_id;
            organisation = copy.set_organisation;
            password = copy.set_password;
            avatar = copy.set_avatar;
            get_access = copy.get_access;
            rooms = copy.get_table;
            handler += subscribe.update_info;
        }

        #endregion

        #region Methods with supporting delegates

        public string set_organisation {
            get => organisation;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Work));
                organisation = value;
            }
        }

        public string set_password {
            get => password;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Password));
                password = value;
            }
        }

        public byte[] set_avatar {
            get => avatar;
            set {
                OnChange(this, new personal_handler(value));
                avatar = value;
            }
        }

        #endregion

        #region Usual methods

        /// <summary>
        ///     Get access
        /// </summary>
        public string get_access { get; }

        /// <summary>
        ///     Get user ID
        /// </summary>
        public int get_user_id { get; }

        /// <summary>
        ///     Get table array
        /// </summary>
        public List<room> get_table {
            get => rooms;
            set {
                rooms = new List<room>();
                foreach (var r in value) rooms.Add(r);
            }
        }

        #endregion
    }
}