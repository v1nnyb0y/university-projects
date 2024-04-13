using System;

namespace SmartHouse_Control.Handlers
{
    /// <summary>
    ///     Handler event class during access
    /// </summary>
    public class access_handler : EventArgs
    {
        /// <summary>
        ///     Initialize class for checking login/password
        /// </summary>
        /// <param name="_login">Login</param>
        /// <param name="_password">Password</param>
        public access_handler(string _login, string _password) {
            login = _login;
            password = _password;
        }

        /// <summary>
        ///     Initialize class for the find extra info
        /// </summary>
        /// <param name="_ID"></param>
        public access_handler(int _ID) {
            ID = _ID;
        }

        public string login { get; } //Login
        public string password { get; } //Password
        public int ID { get; } //ID user
    }
}