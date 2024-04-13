using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Lab._5.LINQ.cfg;
using Lab._5.LINQ.UI.Controls;
using Provider.Interfaces;
using ButtonBase = System.Windows.Controls.Primitives.ButtonBase;
using ComboBox = System.Windows.Controls.ComboBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Lab._5.LINQ.UI
{
    /// <summary>
    ///     Managing Access Menu of the Application
    /// </summary>
    public partial class ManagingUsersMenu
    {
        #region Initialize

        /// <summary>
        ///     Initialize WPF form (Managing user's access)
        /// </summary>
        public ManagingUsersMenu(
            
        ) {
            AdministratedRoomsSource = new List<ComboBoxSource>();
            AccessedUsersSource      = new List<TableSource>();
            InitializeComponent();
        }

        #endregion

        #region Start visual

        /// <summary>
        ///     Load form event
        /// </summary>
        /// <param name="sender">This window</param>
        /// <param name="e">Extra info</param>
        private void LoadWindowEvent
        (
            object          sender,
            RoutedEventArgs e
        ) {
            App.AppProvider.User.Rooms.ForEach
                (
                 room => AdministratedRoomsSource.Add
                     (
                      new ComboBoxSource
                          {
                              RoomId = room.RoomId,
                              Name   = room.Name
                          }
                     )
                );
            AdministratedRooms.ItemsSource   = AdministratedRoomsSource;
            AdministratedRooms.SelectedIndex = 0;
            BackToAddingBttn.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Update DataGrid
        /// </summary>
        private void UpdateTable() {
            AccessedUsersSource = new List<TableSource>();
            (App.AppProvider as IRoomFunc).GetUsersInRoom
                                           (
                                            AdministratedRoomsSource[AdministratedRooms.SelectedIndex]
                                               .RoomId
                                           )
                                          .ForEach
                                               (
                                                userAccess => AccessedUsersSource.Add
                                                    (
                                                     new TableSource
                                                         {
                                                             AddressId = (int)userAccess[0],
                                                             AccessId  = (int)userAccess[1],
                                                             FIO       = (string)userAccess[2],
                                                             Access    = (string)userAccess[3]
                                                         }
                                                    )
                                               );
            AccessedUsers.ItemsSource = AccessedUsersSource;
        }

        #endregion

        #region Dispose form

        /// <summary>
        ///     Closing form event
        /// </summary>
        /// <param name="sender">This form</param>
        /// <param name="e">Extra info about closing</param>
        private void ClosingWindowEvent
        (
            object          sender,
            CancelEventArgs e
        ) {
            ((MainMenu) Owner).IsManagingMenuOpen = false;
        }

        #endregion

        #region Fields

        #region Table source

        /// <summary>
        ///     Class for the table columns
        /// </summary>
        public class TableSource
        {
            public int    AddressId { get; set; }
            public int    AccessId { get; set; }
            public string FIO      { get; set; }
            public string Access   { get; set; }
        }

        #endregion

        /// <summary>
        ///     Source for the table
        /// </summary>
        public List<TableSource> AccessedUsersSource { get; set; }

        #region ComboBox source

        /// <summary>
        ///     Class for the comboBox items
        /// </summary>
        public class ComboBoxSource : INotifyPropertyChanged
        {
            private string _name;

            public int RoomId { get; set; }

            public string Name
                {
                    get => _name;
                    set {
                        _name = value;
                        OnPropertyChanged();
                    }
                }

            #region Delegate update

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged
            (
                [CallerMemberName] string propertyName = null
            ) {
                PropertyChanged?.Invoke
                    (
                     this,
                     new PropertyChangedEventArgs
                         (
                          propertyName
                         )
                    );
            }

            #endregion
        }

        #endregion

        /// <summary>
        ///     Source for the comboBox
        /// </summary>
        public List<ComboBoxSource> AdministratedRoomsSource { get; set; }

        #endregion

        #region Changing access

        /// <summary>
        ///     Change administrated room
        /// </summary>
        /// <param name="sender">Source of the comboBox</param>
        /// <param name="e">Extra info about item</param>
        private void ChangingAdministratedRoom
        (
            object                    sender,
            SelectionChangedEventArgs e
        ) {
            UpdateTable();
        }

        /// <summary>
        ///     Change access for the user
        /// </summary>
        /// <param name="sender">Sourced list item</param>
        /// <param name="e">Extra info about selected item</param>
        private void ChangingAccess
        (
            object                    sender,
            SelectionChangedEventArgs e
        ) {
            var changedIndex = (AccessedUsers.ContainerFromElement
                                    (
                                     (ComboBox) sender
                                    ) as DataGridRow)?.GetIndex();
            if (changedIndex != null)
                AccessedUsersSource[(int) changedIndex]
                       .Access = (((ComboBox) sender).SelectedItem as TextBlock)?.Text;
        }

        /// <summary>
        ///     Pressed button for accepting Changes
        /// </summary>
        /// <param name="sender">Source button</param>
        /// <param name="e">Arguments</param>
        private void AcceptChangingInAccess
        (
            object          sender,
            RoutedEventArgs e
        ) {
            var accessedUser = new List<object[]>();
            AccessedUsersSource.ForEach
                (
                 dataGridRow => accessedUser.Add
                     (
                      new object[]
                          {
                              dataGridRow.AccessId,
                              dataGridRow.Access
                          }
                     )
                );
            (App.AppProvider as IRoomFunc).UpdateAccessesAsync
                (
                 accessedUser
                );
        }

        #endregion

        #region Manipulation User

        #region Deleting User

        /// <summary>
        ///     User selected from the dataGrid
        /// </summary>
        /// <param name="sender">Source of the DataGrid</param>
        /// <param name="eventArgs">Arguments</param>
        private void UserChanging
        (
            object    sender,
            EventArgs eventArgs
        ) {
            if (!(AccessedUsers.SelectedValue is TableSource dataGridRow)) return;

            var arrayFIO = dataGridRow.FIO.Split
                (
                 ' '
                );
            SecondNameField.Text = arrayFIO[0];
            NameField.Text       = arrayFIO[1];
            FatherNameField.Text = arrayFIO[2];

            LoginField.Text        = "";
            PasswordField.Password = "";

            NameField.IsReadOnly       = false;
            SecondNameField.IsReadOnly = false;
            FatherNameField.IsReadOnly = false;
            LoginField.IsEnabled       = false;
            PasswordField.IsEnabled    = false;

            ManipulateBttn.Content      = "Удалить";
            BackToAddingBttn.Visibility = Visibility.Visible;
            BackToAddingBttn.IsEnabled  = true;
        }

        /// <summary>
        ///     Delete User
        /// </summary>
        private void DeleteUser() {
            (App.AppProvider as IHumanFunc).DeleteUserAsync
                (
                 AccessedUsersSource[AccessedUsers.SelectedIndex]
                    .AddressId
                );
            AccessedUsersSource.RemoveAt
                (
                 AccessedUsers.SelectedIndex
                );
            AccessedUsers.ItemsSource = null;
            AccessedUsers.ItemsSource = AccessedUsersSource;
        }

        #endregion

        #region Adding User

        #region Right symbols

        /// <summary>
        ///     Check english char
        /// </summary>
        /// <param name="c">Char</param>
        /// <param name="isShiftPressed">Shift was pressed</param>
        /// <returns></returns>
        private protected virtual bool IsEnglishChar
        (
            char c,
            bool isShiftPressed
        ) {
            if (c >= 'a' &&
                c <= 'z')
                return true;
            if (c >= 'A' &&
                c <= 'Z')
                return true;
            if (c == '2' && isShiftPressed) return true;

            if (InputLanguage.CurrentInputLanguage.LayoutName != "Русская") return false;

            return c == 219 || c == 190 || c == 186 || c == 222 || c == 188 || c == 221;
        }

        /// <summary>
        ///     if input space
        /// </summary>
        /// <param name="c">Input char</param>
        /// <returns></returns>
        private protected virtual bool IsSpace
        (
            char c
        ) {
            return c == ' ';
        }

        /// <summary>
        ///     Delete or back bttn
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        private protected virtual bool IsDeleteBackspace
        (
            Key key
        ) {
            return key == Key.Delete || key == Key.Back;
        }

        #endregion

        /// <summary>
        ///     Check right symbols in fields
        /// </summary>
        /// <param name="sender">TextField source</param>
        /// <param name="e">Extra info about pressed key</param>
        private void CheckInputSymbol
        (
            object       sender,
            KeyEventArgs e
        ) {
            var inputKey = (char) KeyInterop.VirtualKeyFromKey
                (
                 e.Key
                );
            e.Handled = !(IsEnglishChar
                              (
                               inputKey,
                               false
                              ) ||
                          IsDeleteBackspace
                              (
                               e.Key
                              ) ||
                          IsSpace
                              (
                               inputKey
                              ));
        }

        /// <summary>
        ///     Back to adding new user
        /// </summary>
        /// <param name="sender">Source of the button</param>
        /// <param name="e">Extra info</param>
        private void BackToAdding
        (
            object          sender,
            RoutedEventArgs e
        ) {
            var fields = new object[]
                             {
                                 NameField,
                                 SecondNameField,
                                 FatherNameField,
                                 LoginField
                             };
            foreach (var field in fields) {
                ((TextField) field).IsEnabled = true;
                ((TextField) field).Text      = "";
            }

            PasswordField.IsEnabled = true;
            PasswordField.Password  = "";

            BackToAddingBttn.IsEnabled  = false;
            BackToAddingBttn.Visibility = Visibility.Hidden;
            ManipulateBttn.Content      = "Добавить";

            AccessedUsers.SelectedIndex = -1;
        }

        /// <summary>
        ///     Add new user
        /// </summary>
        private bool AddUser(out bool isExist) {
            isExist = (App.AppProvider as IHumanFunc).IsUserExist
                (
                 LoginField.TextWithoutPlaceholder,
                 PasswordField.Password
                );

            if (NameField.TextWithoutPlaceholder       == "" ||
                SecondNameField.TextWithoutPlaceholder == "" ||
                FatherNameField.TextWithoutPlaceholder == "" ||
                LoginField.TextWithoutPlaceholder      == "" ||
                PasswordField.Password                 == "" ||
                isExist)
                return false;


            var ids = (App.AppProvider as IHumanFunc).AddNewUserAsync
                (
                 LoginField.TextWithoutPlaceholder,
                 PasswordField.Password,
                 AdministratedRoomsSource[AdministratedRooms.SelectedIndex]
                    .RoomId,
                 SecondNameField.TextWithoutPlaceholder,
                 NameField.TextWithoutPlaceholder,
                 FatherNameField.TextWithoutPlaceholder
                ).Result;
            AccessedUsersSource.Add(new TableSource()
                                        {
                                            Access = "Отсутствует доступ",
                                            FIO = $"{SecondNameField.TextWithoutPlaceholder} {NameField.TextWithoutPlaceholder} {FatherNameField.TextWithoutPlaceholder}",
                                            AccessId = (int)ids[0],
                                            AddressId = (int)ids[1]
                                        });
            AccessedUsers.ItemsSource = null;
            AccessedUsers.ItemsSource = AccessedUsersSource;

            return true;

        }

        /// <summary>
        ///     Some mistakes in add user fields
        /// </summary>
        private protected virtual async void SomeMistake(bool isExist) {
            await Task.Run
                (
                 () =>
                 {
                     Dispatcher.BeginInvoke
                         (
                          DispatcherPriority.Render,
                          (ThreadStart) delegate
                                        {
                                            if (NameField.TextWithoutPlaceholder == "") {
                                                NameField.Background = new SolidColorBrush(Colors.PaleVioletRed)
                                                                           {
                                                                               Opacity = 0.3
                                                                           };
                                            }

                                            if (SecondNameField.TextWithoutPlaceholder == "") {
                                                SecondNameField.Background = new SolidColorBrush(Colors.PaleVioletRed)
                                                                           {
                                                                               Opacity = 0.3
                                                                           };

                                            }

                                            if (FatherNameField.TextWithoutPlaceholder == "") {
                                                FatherNameField.Background = new SolidColorBrush(Colors.PaleVioletRed)
                                                                           {
                                                                               Opacity = 0.3
                                                                           };

                                            }

                                            if (LoginField.TextWithoutPlaceholder == "") {
                                                LoginField.Background = new SolidColorBrush(Colors.PaleVioletRed)
                                                                           {
                                                                               Opacity = 0.3
                                                                           };

                                            }

                                            if (PasswordField.Password == "") {
                                                PasswordField.Background = new SolidColorBrush(Colors.PaleVioletRed)
                                                                           {
                                                                               Opacity = 0.3
                                                                           };
                                            }

                                            if (isExist) {
                                                PasswordField.Background = LoginField.Background = new SolidColorBrush
                                                                                                   (
                                                                                                    Colors.PaleVioletRed
                                                                                                   )
                                                                                                       {
                                                                                                           Opacity = 0.3
                                                                                                       };
                                            }
                                        });
                     Thread.Sleep
                         (
                          2000
                         );

                     Dispatcher.BeginInvoke
                         (
                          DispatcherPriority.Render,
                          (ThreadStart) delegate
                                        {
                                            NameField.Background =
                                                SecondNameField.Background =
                                                    FatherNameField.Background =
                                                        LoginField.Background =
                                                            PasswordField.Background = new SolidColorBrush
                                                                                       (
                                                                                        Colors.White
                                                                                       )
                                                                                           {
                                                                                               Opacity = 1
                                                                                           };
                                        }
                         );
                 }
                );
        }

        #endregion

        /// <summary>
        ///     Adding/Deleting new user
        /// </summary>
        /// <param name="sender">Source of the button</param>
        /// <param name="e">Extra info</param>
        private void UserManipulation
        (
            object          sender,
            RoutedEventArgs e
        ) {
            if (ReferenceEquals
                (
                 ManipulateBttn.Content,
                 "Удалить"
                )) {
                DeleteUser();
            }
            else {
                var shouldBeBackPressed = AddUser(out var isExist);
                if (!shouldBeBackPressed) {
                    SomeMistake(isExist);
                    return;
                }
            }

            BackToAddingBttn.RaiseEvent
                (
                 new RoutedEventArgs
                     (
                      ButtonBase.ClickEvent
                     )
                );
        }

        #endregion
    }
}