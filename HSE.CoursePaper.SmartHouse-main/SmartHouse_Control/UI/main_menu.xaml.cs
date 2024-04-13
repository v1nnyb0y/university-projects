using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SmartHouse_Control.InfoS;
using SmartHouse_Control.Session;

namespace SmartHouse_Control.UI
{
    /// <summary>
    ///     Логика взаимодействия для main_menu.xaml
    /// </summary>
    public partial class main_menu : Window, IUser
    {
        private filter_menu control_menu; //Control and data menu
        private personal_page pg; //Personal page
        private qbe_menu qbe; //QBE menu
        private room_menu_admin room_admin; //Room page (for admin)
        private room_menu_user room_user; //Room page (for user)
        public user user_obj { get; set; } //Persona

        #region Start visual

        public main_menu(user autorized_user) {
            InitializeComponent();
            user_obj = new user(autorized_user);
        }

        //Load form delegate
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            clock_start(); //Start clock
            if (user_obj.get_access != "Администратор") {
                if (user_obj.get_table.Count == 0) {
                    room_name.Visibility = Visibility.Hidden;
                    return;
                }

                room_name.Text = user_obj.get_table[0].get_name; //Set room name
            }
            else {
                room_name.Text = "Администрирование";
            }
        }

        /// <summary>
        ///     Clock start
        /// </summary>
        private void clock_start() {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { clock.Text = DateTime.Now.ToShortTimeString(); };
            timer.Start();
        }

        #endregion

        #region Personal page

        //is active
        public bool is_active_personal_page;

        //Open personal page
        private void Window_personal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!is_active_personal_page) {
                pg = new personal_page(user_obj); //Initialize personal page
                pg.Owner = this; //Make this window a owner
                pg.Show(); //Show personal page
                is_active_personal_page = true;
            }
        }

        #endregion

        #region Room menu

        //is active
        public bool is_active_room_menu;

        //Open room menu
        private void Window_room_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!is_active_room_menu) {
                if (user_obj.get_access != "Администратор") {
                    is_active_room_menu = true;
                    room_user = new room_menu_user(
                        user_obj.get_table,
                        0); //Initialize room menu
                    room_user.Owner = this; //Make this window a owner
                    room_user.Show(); //Show window
                }
                else {
                    is_active_room_menu = true;
                    room_admin = new room_menu_admin(false, user_obj); //Initialize room menu
                    room_admin.Owner = this; //Make this window a owner
                    room_admin.Show(); //Show window
                }
            }
        }

        #endregion

        #region Control and data menu

        //is active
        public bool is_active_menu;

        //Open menu
        private void Window_filters_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!is_active_menu) {
                if (user_obj.get_access != "Администратор") {
                    qbe = new qbe_menu(); //Initialize qbe
                    qbe.Owner = this; //Set owner
                    qbe.Show(); //Show
                }
                else {
                    control_menu = new filter_menu(
                        user_obj.get_table
                    ); //Initialize filter
                    control_menu.Owner = this; //Set owner
                    control_menu.Show(); //Show
                }

                is_active_menu = true;
            }
        }

        /// <summary>
        ///     Set new settings for the model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="new_settigns"></param>
        public void set_new_settings(int id, string new_settigns, params int[] ids) {
            var list = user_obj.get_table;
            list[id].Settings = new_settigns;
            user_obj.get_table = list;
        }

        #endregion

        #region Exit

        //Mouse down
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Application.Current.Shutdown();
        }

        //Exit
        private void Window_Closed(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }

        #endregion
    }
}