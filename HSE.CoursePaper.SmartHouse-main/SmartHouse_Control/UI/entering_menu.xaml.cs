using System;
using System.Windows;
using System.Windows.Input;
using SmartHouse_Control.Handlers;
using SmartHouse_Control.Session;

namespace SmartHouse_Control.UI
{
    /// <summary>
    ///     Delegate for check login/password
    /// </summary>
    /// <param name="login">Entered login</param>
    /// <param name="password">Entered password</param>
    /// <returns></returns>
    internal delegate Tuple<Visibility, bool, user> wrong_login_or_password(access_handler args);

    /// <summary>
    ///     Логика взаимодействия для entering_menu.xaml
    /// </summary>
    public partial class entering_menu : Window
    {
        #region Start visual

        public entering_menu() {
            InitializeComponent();
        }

        //Opening the application
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            delegate_on = subscribe.check_login_password; // Subscribe function
            mistake.Visibility = Visibility.Hidden; // Make wrong message invisible
        }

        #endregion

        #region Delegate

        private event wrong_login_or_password delegate_on; // Event fot the wrong login or password

        /// <summary>
        ///     On doing function
        /// </summary>
        /// <param name="args">Args for function</param>
        /// <returns></returns>
        public virtual Tuple<Visibility, bool, user> OnCheck(access_handler args) {
            return delegate_on?.Invoke(args);
        }

        #endregion

        #region Get Access

        //Press bttn entering to the application
        private void Accept_bttn_Click(object sender, RoutedEventArgs e) {
            check_access_lvl();
        }

        //Check access lvl
        private void check_access_lvl() {
            var checked_info = OnCheck(new access_handler(login.Text, password.Password)); // Checked info is back
            mistake.Visibility = checked_info.Item1; // Make message of wrong login or password visible or not
            //if the login and password correct
            if (checked_info.Item2) {
                var mainMenu = new main_menu(checked_info.Item3); // Open main menu 
                mainMenu.Show();

                Close(); // Close this one
            }
        }

        //If pressed bttn
        private void Password_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) check_access_lvl();
        }

        #endregion
    }
}