using System;

namespace Provider.Handlers
{
    /// <summary>
    ///     Handler event class during getting access
    /// </summary>
    public class AccessHandler : EventArgs
    {
        /// <summary>
        ///     Initialize class for checking login/password
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        public AccessHandler
        (
            string login,
            string password
        )
        {
            Login = login;
            Password = password;
        }

        /// <summary>
        ///     Initialize class for the find extra info
        /// </summary>
        /// <param name="id"></param>
        public AccessHandler
        (
            int id
        )
        {
            Id = id;
        }

        public string Login { get; } //Login
        public string Password { get; } //Password
        public int Id { get; } //ID user
    }
}