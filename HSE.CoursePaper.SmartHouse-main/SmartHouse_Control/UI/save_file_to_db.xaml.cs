using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using SmartHouse_Control.Handlers;
using SmartHouse_Control.Session;
using ComboBox = System.Windows.Controls.ComboBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace SmartHouse_Control.UI
{
    /// <summary>
    ///     Логика взаимодействия для save_file_to_db.xaml
    /// </summary>
    public partial class save_file_to_db : Window
    {
        #region Accept new model

        private void Button_Click(object sender, RoutedEventArgs e) {
            var accesses = new string[fio_access.Items.Count];
            var i = 0;
            foreach (var item in fio_access.Items) {
                accesses[i] = (fio_access.Items[i] as Data).access;
                ++i;
            }

            OnUpdate(accesses, rooms[current_index].get_room_id);
            Close();
        }

        #endregion

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            var row = fio_access.ContainerFromElement(sender as ComboBox) as DataGridRow;
            var rowIndex = row.GetIndex();

            if (!is_entering)
                (fio_access.Items[rowIndex] as Data).access = ((sender as ComboBox).SelectedItem as TextBlock).Text;
        }

        #region Exit

        private void Window_Closing(object sender, CancelEventArgs e) {
            (Owner as room_menu_admin).is_active_saving = false;
        }

        #endregion

        #region Fields

        private string path; //Path to file 
        private bool is_file_load; //File null or not
        private readonly List<room> rooms; //managed rooms
        private int current_index; //Index in the list rooms of the current room
        private bool is_entering; //is entering to the page

        #endregion

        #region Start Visual

        public save_file_to_db(string _path, bool is_load, List<room> _rooms) {
            InitializeComponent();
            path = _path;
            rooms = _rooms;
            current_index = 0;
            is_file_load = is_load;
        }

        //Load window
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            get_users += subscribe.load_all_users;
            do_update += subscribe.update_accesses;
            is_entering = true;

            //Fill combo box
            foreach (var item in rooms)
                current_room.Items.Add(
                    new ComboBoxItem
                        {
                            Content = item.get_name
                        }
                );

            current_index = 0;
            current_room.Text = rooms[current_index].get_name;
            fill_table();
            is_entering = false;
        }

        //if room changed
        private void Current_room_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            current_index = current_room.SelectedIndex;
            fill_table();
        }

        /// <summary>
        ///     Filling the table
        /// </summary>
        private void fill_table() {
            fio_access.Items.Clear();
            var dt = OnGet(rooms[current_index].get_room_id);

            for (var i = 0; i < dt.Rows.Count; ++i)
                fio_access.Items.Add(
                    new Data
                        {
                            fio = dt.Rows[i].ItemArray[0].ToString(),
                            access = dt.Rows[i].ItemArray[1].ToString()
                        }
                );
        }

        public class Data
        {
            public string fio { get; set; }
            public string access { get; set; }
        }

        #endregion

        #region Delegates

        //Create new user
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            if (name.TextWithoutPlaceholder == "" ||
                second_name.TextWithoutPlaceholder == "" ||
                last_name.TextWithoutPlaceholder == "" ||
                login.TextWithoutPlaceholder == "" ||
                password.Password == "") {
                MessageBox.Show(
                    "Ошибка в добавлении пользователя. Не все поля заполнены",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            var room_ids = new List<int>();
            foreach (var item in rooms) room_ids.Add(item.get_room_id);

            var is_added = subscribe.add_new_user(
                room_ids,
                second_name.TextWithoutPlaceholder,
                name.TextWithoutPlaceholder,
                last_name.TextWithoutPlaceholder,
                login.TextWithoutPlaceholder,
                password.Password
            );

            if (is_added)
                fill_table();
            else
                MessageBox.Show(
                    "Ошибка добавления пользователя. Пользователь с таким логином и паролем уже существует.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        #region Delegate for save model

        //Delegate for insert model
        private delegate void save_to_db(string path);

        //Event
        private event save_to_db go_save;

        /// <summary>
        ///     Go save files
        /// </summary>
        /// <param name="path">Path to file</param>
        private void OnSave(string path) {
            go_save?.Invoke(path);
        }

        #endregion

        #region Delegate for output all users except admins

        /// <summary>
        ///     Delegate for getting access table
        /// </summary>
        /// <returns></returns>
        private delegate DataTable get_dt(int id_room);

        //Event
        private event get_dt get_users;

        private DataTable OnGet(int id) {
            return get_users?.Invoke(id);
        }

        #endregion

        #region Delegate for updating model

        /// <summary>
        ///     Delegate for updating access
        /// </summary>
        private delegate void update_access(string[] accesses, int id_room);

        //Event
        private event update_access do_update;

        /// <summary>
        ///     Updating
        /// </summary>
        /// <param name="accesses"></param>
        private void OnUpdate(string[] accesses, int id_room) {
            do_update?.Invoke(accesses, id_room);
        }

        #endregion

        #endregion

        #region Right input

        /// <summary>
        ///     Check english char
        /// </summary>
        /// <param name="c">Char</param>
        /// <returns></returns>
        private bool is_en_char(char c, bool ok) {
            if (c >= 'a' && c <= 'z')
                return true;
            if (c >= 'A' && c <= 'Z')
                return true;
            if (c == '2' && ok)
                return true;
            if (InputLanguage.CurrentInputLanguage.LayoutName == "Русская")
                if (c == 219 || c == 190 || c == 186 || c == 222 || c == 188 || c == 221)
                    return true;

            return false;
        }

        /// <summary>
        ///     if input space
        /// </summary>
        /// <param name="c">Input char</param>
        /// <returns></returns>
        private bool is_space(char c) {
            if (c == ' ')
                return true;

            return false;
        }

        /// <summary>
        ///     Delete or back bttn
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        private bool is_delete_or_back(Key key) {
            if (key == Key.Delete || key == Key.Back) return true;

            return false;
        }

        //Check right input
        private void PreviewKeyDown(object sender, KeyEventArgs e) {
            var input = (char) KeyInterop.VirtualKeyFromKey(e.Key);
            e.Handled = !(is_en_char(input, false) || is_delete_or_back(e.Key)) || is_space(input);
        }

        #endregion
    }
}