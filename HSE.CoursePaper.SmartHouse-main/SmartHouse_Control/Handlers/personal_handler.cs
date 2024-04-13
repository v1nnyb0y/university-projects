using System;

namespace SmartHouse_Control.Handlers
{
    /// <summary>
    ///     Personal changed handler
    /// </summary>
    internal class personal_handler : EventArgs
    {
        public personal_handler(string input, Type input_type) {
            info_type = input_type;
            switch (input_type) {
                case Type.Name:
                    name = input;
                    break;
                case Type.Second_Name:
                    second_name = input;
                    break;
                case Type.Last_Name:
                    last_name = input;
                    break;
                case Type.Function:
                    function = input;
                    break;
                case Type.Mail:
                    mail = input;
                    break;
                case Type.Phone_number:
                    phone_number = input;
                    break;
                case Type.Work:
                    work = input;
                    break;
                case Type.Password:
                    password = input;
                    break;
                case Type.Address:
                    address = input;
                    break;
            }
        }


        /// <summary>
        ///     Initialize handler
        /// </summary>
        /// <param name="value"></param>
        public personal_handler(byte[] _avatar) {
            info_type = Type.Default;
            avatar = _avatar;
        }

        public string name { get; }
        public string second_name { get; }
        public string last_name { get; }
        public string phone_number { get; }
        public string address { get; }
        public string mail { get; }
        public string work { get; }
        public string function { get; }
        public string password { get; }
        public byte[] avatar { get; }
        public Type info_type { get; }

        internal enum Type
        {
            Name,
            Second_Name,
            Last_Name,
            Phone_number,
            Mail,
            Work,
            Function,
            Password,
            Address,
            Default
        }
    }
}