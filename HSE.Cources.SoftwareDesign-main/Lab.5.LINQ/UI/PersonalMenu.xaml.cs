using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Lab._5.LINQ.cfg;
using Lab._5.LINQ.UI.Controls;
using Provider.CurrentSession;
using Provider.Handlers;
using Provider.Interfaces;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Lab._5.LINQ.UI
{
    /// <summary>
    ///     Delegate for sending data to the db
    /// </summary>
    /// <param name="args">Arguments</param>
    internal delegate void UpdateData
    (
        NewPersonalInfoHandler args
    );

    /// <summary>
    ///     Personal Menu of the Application
    /// </summary>
    public partial class PersonalMenu
    {
        #region Initialize

        /// <summary>
        ///     Initialize WPF form (Personal Menu)
        /// </summary>
        public PersonalMenu() {
            AccessedRoomsSource =  new List<TableSource>();
            AvatarByteArray     =  App.AppProvider.User.Avatar;
            SendNewData         += (App.AppProvider as IHumanFunc).UpdateAuthorizedHuman;
            InitializeComponent();
        }

        #endregion

        #region Dispose form

        /// <summary>
        ///     Close window event
        /// </summary>
        /// <param name="sender">This window</param>
        /// <param name="cancelEventArgs">Extra info</param>
        private void ClosingWindowEvent
        (
            object          sender,
            CancelEventArgs cancelEventArgs
        ) {
            ((MainMenu) Owner).IsPersonalMenuOpen = false;
        }

        #endregion

        #region Delegate

        /// <summary>
        ///     Event for the delegate
        /// </summary>
        private event UpdateData SendNewData;

        /// <summary>
        ///     Send data to the data base invoke
        /// </summary>
        /// <param name="args">Arguments (what should be updated)</param>
        private protected virtual void OnSendNewData
        (
            NewPersonalInfoHandler args
        ) {
            SendNewData?.Invoke
                (
                 args
                );
        }

        #endregion

        #region Fields

        /// <summary>
        ///     Path to file
        /// </summary>
        public object AvatarByteArray { get; set; }

        #region Table source

        /// <summary>
        ///     Class for the table columns
        /// </summary>
        public class TableSource
        {
            public string Name   { get; set; }
            public string Access { get; set; }
        }

        #endregion

        /// <summary>
        ///     Source for the AccessedRooms table
        /// </summary>
        public List<TableSource> AccessedRoomsSource { get; set; }

        /// <summary>
        ///     Is changing mode active
        /// </summary>
        private bool _isChangingActive;

        #endregion

        #region Start Visual

        /// <summary>
        ///     Load form event
        /// </summary>
        /// <param name="sender">This form</param>
        /// <param name="e">Extra info</param>
        private void LoadWindowEvent
        (
            object          sender,
            RoutedEventArgs e
        ) {
            SetInfoToFields
                (
                 true
                );
            DeactivateChanging();
            MistakeInPassword.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Set info to fields
        /// </summary>
        /// <param name="isWindowOpening"></param>
        private protected virtual void SetInfoToFields
        (
            bool isWindowOpening
        ) {
            //Set personal info
            UserName.Text        = App.AppProvider.User.FIO;
            UserAccess.Text      = App.AppProvider.User.Access;
            UserPhoneNumber.Text = App.AppProvider.User.PhoneNumber;
            UserEMail.Text       = App.AppProvider.User.EMail;
            UserWorkplace.Text   = App.AppProvider.User.Organization;

            if (isWindowOpening)
                App.AppProvider.User.Rooms.ForEach
                    (
                     room => AccessedRoomsSource.Add
                         (
                          new TableSource
                              {
                                  Name   = room.Name,
                                  Access = room.Access
                              }
                         )
                    );
        }

        #endregion

        #region Changing info On

        #region Right symbols

        /// <summary>
        ///     Check number of spaces
        /// </summary>
        /// <param name="userName">Input string for checking amount of spaces</param>
        /// <returns></returns>
        private protected virtual bool IsSpacesEqualThree
        (
            string userName
        ) {
            return userName.Split
                            (
                             ' '
                            )
                           .Length <=
                   3;
        }

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

        /// <summary>
        ///     Check number
        /// </summary>
        /// <param name="c">Char</param>
        /// <returns></returns>
        private protected virtual bool IsNumber
        (
            char c
        ) {
            return c >= '0' && c <= '9';
        }

        #endregion

        /// <summary>
        ///     Check input symbol (not to print wrong one)
        /// </summary>
        /// <param name="sender">Source of the TextField</param>
        /// <param name="e">Key info</param>
        private void CheckInputSymbol
        (
            object       sender,
            KeyEventArgs e
        ) {
            var pressedKey = (char) KeyInterop.VirtualKeyFromKey
                (
                 e.Key
                );
            switch ((sender as TextField)?.Name) {
                case "UserName":
                    {
                        e.Handled = !(IsEnglishChar
                                          (
                                           pressedKey,
                                           false
                                          ) ||
                                      IsDeleteBackspace
                                          (
                                           e.Key
                                          ) ||
                                      IsSpace
                                          (
                                           pressedKey
                                          )) ||
                                    !IsSpacesEqualThree
                                        (
                                         UserName.TextWithoutPlaceholder + pressedKey
                                        );
                        break;
                    }
                case "UserPhoneNumber":
                    {
                        e.Handled = (!(IsNumber
                                           (
                                            pressedKey
                                           ) ||
                                       IsSpace
                                           (
                                            pressedKey
                                           )) ||
                                     UserPhoneNumber.TextWithoutPlaceholder.Length >= 11) &&
                                    !IsDeleteBackspace
                                        (
                                         e.Key
                                        );
                        break;
                    }
                case "UserEMail":
                    {
                        e.Handled = !(IsEnglishChar
                                          (
                                           pressedKey,
                                           e.KeyboardDevice.Modifiers == ModifierKeys.Shift
                                          ) ||
                                      IsSpace
                                          (
                                           pressedKey
                                          ) ||
                                      IsDeleteBackspace
                                          (
                                           e.Key
                                          ));
                        break;
                    }
                case "UserAddress":
                    {
                        e.Handled = !(IsEnglishChar
                                          (
                                           pressedKey,
                                           false
                                          ) ||
                                      IsNumber
                                          (
                                           pressedKey
                                          ) ||
                                      IsDeleteBackspace
                                          (
                                           e.Key
                                          ) ||
                                      IsSpace
                                          (
                                           pressedKey
                                          ));
                        break;
                    }
                case "UserWorkplace":
                    {
                        e.Handled = !(IsEnglishChar
                                          (
                                           pressedKey,
                                           false
                                          ) ||
                                      IsNumber
                                          (
                                           pressedKey
                                          ) ||
                                      IsDeleteBackspace
                                          (
                                           e.Key
                                          ) ||
                                      IsSpace
                                          (
                                           pressedKey
                                          ));
                        break;
                    }
                default: return;
            }
        }

        /// <summary>
        ///     Set/Apply new settings button was pressed
        /// </summary>
        /// <param name="sender">Source of the button</param>
        /// <param name="e">Extra info</param>
        private void OnSetNewSettings
        (
            object          sender,
            RoutedEventArgs e
        ) {
            if (_isChangingActive) {
                AcceptNewSettings();
                DeactivateChanging();
                SettingConfigure.Content = FindResource
                    (
                     "MakeChangingImage"
                    );
            }
            else {
                ActivateChanging();
                SettingConfigure.Content = FindResource
                    (
                     "AcceptChangingImage"
                    );
            }
        }

        private protected virtual void ActivateChanging() {
            var textFields = new[]
                                 {
                                     UserName,
                                     UserAccess,
                                     UserWorkplace,
                                     UserAddress,
                                     UserEMail,
                                     UserPhoneNumber
                                 };

            foreach (var textField in textFields) {
                textField.BorderBrush = Brushes.Black;
                textField.BorderThickness = new Thickness
                    (
                     1
                    );
                textField.IsReadOnly = false;
                if (textField.TextWithoutPlaceholder == "") textField.IsPlaceholderShowing = true;
            }

            _isChangingActive = true;
        }

        #endregion

        #region Changing info Off

        /// <summary>
        ///     Deactivate changing (refactoring TextFields)
        /// </summary>
        private protected virtual void DeactivateChanging() {
            var textFields = new[]
                                 {
                                     UserName,
                                     UserAccess,
                                     UserWorkplace,
                                     UserAddress,
                                     UserEMail,
                                     UserPhoneNumber
                                 };
            foreach (var textField in textFields) {
                textField.BorderBrush = Brushes.Transparent;
                textField.BorderThickness = new Thickness
                    (
                     0
                    );
                textField.IsReadOnly = true;
                if (textField.TextWithoutPlaceholder == "") textField.IsPlaceholderShowing = false;
            }

            _isChangingActive = false;
        }

        /// <summary>
        ///     Close Alarm for one Click
        /// </summary>
        /// <param name="sender">Source of the alarm</param>
        /// <param name="e">Arguments</param>
        private void ClosedAlarm
        (
            object    sender,
            EventArgs e
        ) {
            switch ((sender as Popup)?.Name) {
                case "MistakeInEMail":
                    {
                        MistakeInFIO.IsOpen         = false;
                        MistakeInPhoneNumber.IsOpen = false;
                        return;
                    }
                case "MistakeInFIO":
                    {
                        MistakeInEMail.IsOpen       = false;
                        MistakeInPhoneNumber.IsOpen = false;
                        return;
                    }
                case "MistakeInPhoneNumber":
                    {
                        MistakeInEMail.IsOpen = false;
                        MistakeInFIO.IsOpen   = false;
                        return;
                    }
                default: return;
            }
        }

        /// <summary>
        ///     Accept new settings for the User/Person
        /// </summary>
        private protected virtual void AcceptNewSettings() {
                     Dispatcher.BeginInvoke
                         (
                          new Action(()=>
                                        {
                                            var mistakeInFio = CheckUserName();
                                            var mistakeInPhoneNumber = CheckPhoneNumber();
                                            var mistakeInEMail = CheckEMail();

                                            if (!(mistakeInEMail && mistakeInFio && mistakeInPhoneNumber))
                                            {
                                                if (!mistakeInEMail) MistakeInEMail.IsOpen             = true;
                                                if (!mistakeInFio) MistakeInFIO.IsOpen                 = true;
                                                if (!mistakeInPhoneNumber) MistakeInPhoneNumber.IsOpen = true;
                                                SetInfoToFields
                                                    (
                                                     false
                                                    );
                                                return;
                                            }

                                            App.AppProvider.User.FIO          = UserName.TextWithoutPlaceholder;
                                            App.AppProvider.User.Address      = UserAddress.TextWithoutPlaceholder;
                                            App.AppProvider.User.EMail        = UserEMail.TextWithoutPlaceholder;
                                            App.AppProvider.User.Organization = UserWorkplace.TextWithoutPlaceholder;
                                            App.AppProvider.User.Avatar       = GetNewAvatar();
                                            OnSendNewData
                                                (
                                                 new NewPersonalInfoHandler
                                                     (
                                                      NewPersonalInfoHandler.UpdateType.AvatarAndTextInfo
                                                     )
                                                );
                                        })
                         );
        }

        #region Check right input

        /// <summary>
        ///     Check user name right input
        /// </summary>
        /// <returns></returns>
        private protected virtual bool CheckUserName() {
            return Dispatcher.InvokeAsync(() =>
                                          {
                                              var regexFIO = new Regex
                                                  (
                                                   @"([A-ZА-Я]{1}[a-zа-я]+){1}[ ]{1}([A-ZА-Я]{1}[a-zа-я]+){1}[ ]{1}([A-ZА-Я]{1}[a-zа-я]+){1}"
                                                  );
                                              return regexFIO.IsMatch
                                                  (
                                                   UserName.TextWithoutPlaceholder
                                                  );
                                          }).Result;
        }

        /// <summary>
        ///     Check user phone right input
        /// </summary>
        /// <returns></returns>
        private protected virtual bool CheckPhoneNumber() {
            return Dispatcher.InvokeAsync(() =>
                                          {
                                              var regexPhoneNumber = new Regex
                                                  (
                                                   @"\d{11}"
                                                  );
                                              return regexPhoneNumber.IsMatch
                                                  (
                                                   UserPhoneNumber.TextWithoutPlaceholder
                                                  );
                                          }).Result;
        }

        /// <summary>
        ///     Check user e-mail right input
        /// </summary>
        /// <returns></returns>
        private protected virtual bool CheckEMail() {
            return Dispatcher.InvokeAsync
                              (
                               () =>
                               {
                                   var regexEMail = new Regex
                                       (
                                        @"^.+@.+\..+$"
                                       );
                                   return regexEMail.IsMatch
                                       (
                                        UserEMail.TextWithoutPlaceholder
                                       );
                               }
                              )
                             .Result;
        }

        #endregion

        #endregion

        #region Change additional info (avatar, password)

        /// <summary>
        ///     Save avatar from the Image
        /// </summary>
        private protected virtual byte[] GetNewAvatar() {
            var           bitmapSource  = (BitmapSource) UserAvatar.Source;
            BitmapEncoder encoderToJpeg = new JpegBitmapEncoder();
            encoderToJpeg.Frames.Add
                (
                 BitmapFrame.Create
                     (
                      bitmapSource
                     )
                );
            byte[] newAvatar;
            using (var memoryStream = new MemoryStream()) {
                encoderToJpeg.Save
                    (
                     memoryStream
                    );
                newAvatar = memoryStream.ToArray();
            }

            return newAvatar;
        }

        /// <summary>
        ///     Click on the image to set new avatar
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Extra info</param>
        private void SetNewAvatar
        (
            object               sender,
            MouseButtonEventArgs e
        ) {
            var fileDialog = new OpenFileDialog
                                 {
                                     Filter   = "Аватар (PNG)|*.png|Авaтар (JPEG)|*.jpg",
                                     FileName = $"AvatarOfUser_{UserName.TextWithoutPlaceholder}"
                                 };
            if (fileDialog.ShowDialog
                    (
                     this
                    ) ==
                true) {
                UserAvatar.Source = new BitmapImage
                    (
                     new Uri
                         (
                          fileDialog.FileName
                         )
                    );
                App.AppProvider.User.Avatar = GetNewAvatar();
                OnSendNewData
                    (
                     new NewPersonalInfoHandler
                         (
                          NewPersonalInfoHandler.UpdateType.Avatar
                         )
                    );
            }
        }

        /// <summary>
        ///     Button with accepting password was pressed
        /// </summary>
        /// <param name="sender">This button</param>
        /// <param name="e">Extra info about it</param>
        private void OnChangePassword
        (
            object          sender,
            RoutedEventArgs e
        ) {
            var mistakeText = "Неверно введенный старый\n пароль";
            var successText = "Пароль успешно изменен";
            if (App.AppProvider.User.Password == OldPassword.Password &&
                !(App.AppProvider as IHumanFunc).IsUserExist(App.AppProvider.User.UserId, NewPassword.Password)) {
                App.AppProvider.User.Password = NewPassword.Password;
                OnSendNewData
                    (
                     new NewPersonalInfoHandler
                         (
                          NewPersonalInfoHandler.UpdateType.Password
                         )
                    );
                MistakeInPassword.Text       = successText;
                MistakeInPassword.Foreground = Brushes.Green;
            }
            else {
                MistakeInPassword.Text       = mistakeText;
                MistakeInPassword.Foreground = Brushes.Red;
            }

            MistakeInPassword.Visibility = Visibility.Visible;
        }

        #endregion
    }
}