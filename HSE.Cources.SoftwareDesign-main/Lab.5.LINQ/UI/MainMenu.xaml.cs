using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Lab._5.LINQ.cfg;

namespace Lab._5.LINQ.UI
{
    /// <summary>
    ///     Main Form of the application
    /// </summary>
    public partial class MainMenu
    {
        #region Initialize

        /// <summary>
        ///     Initialize WPF form (Main Menu)
        /// </summary>
        public MainMenu() {
            InitializeComponent();
        }

        #endregion

        #region Start visual

        /// <summary>
        ///     Event after Window loaded
        /// </summary>
        /// <param name="sender">This window</param>
        /// <param name="e">Extra info</param>
        private void LoadWindowEvent
        (
            object          sender,
            RoutedEventArgs e
        ) {
            ClockStart();
            if (App.AppProvider.User.Access != "Администратор") {
                if (App.AppProvider.User.Rooms.Count == 0) {
                    RoomName.Visibility = Visibility.Hidden;
                    return;
                }

                RoomName.Text = App.AppProvider.User.Rooms[0]
                                   .Name;
            }
            else {
                RoomName.Text = "Администрирование";
            }
        }

        /// <summary>
        ///     Clock start
        /// </summary>
        private void ClockStart() {
            var timer = new DispatcherTimer
                            {
                                Interval = new TimeSpan
                                    (
                                     0,
                                     0,
                                     1
                                    ),
                                IsEnabled = true
                            };
            timer.Tick +=
                (
                    o,
                    t
                ) =>
                {
                    Clock.Text = DateTime.Now.ToShortTimeString();
                };
            timer.Start();
        }

        #endregion

        #region Dispose form

        /// <summary>
        ///     Event after Window closed
        /// </summary>
        /// <param name="sender">This form</param>
        /// <param name="e">Extra info</param>
        private void ClosedWindowEvent
        (
            object    sender,
            EventArgs e
        ) {
            Application.Current.Shutdown();
        }

        /// <summary>
        ///     Event for the button to close the window
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Mouse info</param>
        private void OnCloseApplication
        (
            object               sender,
            MouseButtonEventArgs e
        ) {
            Application.Current.Shutdown();
        }

        #endregion

        #region Open menues

        #region Fields 

        private PersonalMenu _personalMenu;
        public  bool         IsPersonalMenuOpen;

        private ManagingUsersMenu _managingUsersMenu;
        public  bool              IsManagingMenuOpen;

        #endregion

        /// <summary>
        ///     Open personal menu event
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Mouse info</param>
        private void OnOpenPersonalMenu
        (
            object               sender,
            MouseButtonEventArgs e
        ) {
            if (IsPersonalMenuOpen) return;

            _personalMenu = new PersonalMenu
                                {
                                    Owner = this
                                };
            _personalMenu.Show();
            IsPersonalMenuOpen = true;
        }

        /// <summary>
        ///     Open room menu event
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Mouse info</param>
        private void OnOpenRoomMenu
        (
            object               sender,
            MouseButtonEventArgs e
        ) {
            if (IsManagingMenuOpen || App.AppProvider.User.Access != "Администратор") return;

            _managingUsersMenu = new ManagingUsersMenu
                                     {
                                         Owner = this
                                     };
            _managingUsersMenu.Show();
            IsManagingMenuOpen = true;
        }

        /// <summary>
        ///     Open filter menu event
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Mouse info</param>
        private void OnOpenFilterMenu
        (
            object               sender,
            MouseButtonEventArgs e
        ) { }

        #endregion
    }
}