using SmartHouse_Control.Handlers;

namespace SmartHouse_Control.Session
{
    internal delegate void change_info_person(object source, personal_handler args); //Delegate

    /// <summary>
    ///     Personal info for current person
    /// </summary>
    public abstract class person
    {
        #region Fields

        private string name; //Person name
        private string second_name; //Person second name
        private string last_name; //Person last name
        private string phone_number; //Phone number of person
        private string e_mail; //E-mail of person
        private string address; //Current address of person

        #endregion

        #region Initialize

        /// <summary>
        ///     Initialize class
        /// </summary>
        protected person() { }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="_name">First name</param>
        /// <param name="_second_name">Second name</param>
        /// <param name="_last_name">Last name</param>
        /// <param name="_phone_number">Phone number</param>
        /// <param name="_e_mail">E-mail</param>
        /// <param name="_address">Address</param>
        /// <param name="_id_address">ID of address</param>
        /// <param name="_ID">Person ID</param>
        protected person(string _name,
            string _second_name,
            string _last_name,
            string _phone_number,
            string _e_mail,
            string _address,
            int _id_address,
            int _ID) {
            name = _name;
            second_name = _second_name;
            last_name = _last_name;
            phone_number = _phone_number;
            e_mail = _e_mail;
            address = _address;
            get_address_id = _id_address;
            get_person_id = _ID;
            handler += subscribe.update_info;
        }

        #endregion

        #region Delegate

        private event change_info_person handler; //Event

        /// <summary>
        ///     Doing subscripted function
        /// </summary>
        /// <param name="args"></param>
        private void OnChange(object source, personal_handler args) {
            handler?.Invoke(source, args);
        }

        #endregion

        #region Methods with supporting delegates

        public string set_name {
            get => name;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Name));
                name = value;
            }
        }

        public string set_second_name {
            get => second_name;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Second_Name));
                second_name = value;
            }
        }

        public string set_last_name {
            get => last_name;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Last_Name));
                last_name = value;
            }
        }

        public string set_phone {
            get => phone_number;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Phone_number));
                phone_number = value;
            }
        }

        public string set_mail {
            get => e_mail;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Mail));
                e_mail = value;
            }
        }

        public string set_address {
            get => address;
            set {
                OnChange(this, new personal_handler(value, personal_handler.Type.Address));
                address = value;
            }
        }

        #endregion

        #region Usual Methods

        /// <summary>
        ///     Get FIO
        /// </summary>
        public string get_fio => second_name + " " + name + " " + last_name;

        /// <summary>
        ///     Get Person ID
        /// </summary>
        public int get_person_id { get; }

        /// <summary>
        ///     Get address ID
        /// </summary>
        public int get_address_id { get; }

        #endregion
    }
}