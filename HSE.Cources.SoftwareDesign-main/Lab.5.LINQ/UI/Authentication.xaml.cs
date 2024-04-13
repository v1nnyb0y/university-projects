using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Lab._5.LINQ.cfg;
using Provider;
using Provider.Handlers;
using Provider.Interfaces;

namespace Lab._5.LINQ.UI
{
    /// <summary>
    ///     Delegate for getting access
    /// </summary>
    /// <param name="args">Args for the delegate</param>
    /// <returns></returns>
    internal delegate Tuple<object, bool, CurrentProvider> GetAccessDelegate
    (
        AccessHandler args
    );

    /// <summary>
    ///     Initialize WPF form
    /// </summary>
    public partial class Authentication
    {
        #region Initialize

        /// <summary>
        ///     Initialize WPF form (Authentication)
        /// </summary>
        public Authentication() {
            GetAccess += (App.AppProvider as IHumanFunc).CheckAccess;
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
            Mistake.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Delegate

        /// <summary>
        ///     Event for the getting access
        /// </summary>
        private event GetAccessDelegate GetAccess;

        /// <summary>
        ///     On getting access
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        public virtual Tuple<object, bool, CurrentProvider> OnGetAccess
        (
            AccessHandler args
        ) {
            return GetAccess?.Invoke
                (
                 args
                );
        }

        #endregion

        #region GetAccess

        /// <summary>
        ///     Get access to the application
        /// </summary>
        /// <param name="sender">This button</param>
        /// <param name="e">Arguments</param>
        private void GetAccessBttnEvent
        (
            object          sender,
            RoutedEventArgs e
        ) {
            var (isShowMistake, isAccess, provider) = OnGetAccess
                (
                 new AccessHandler
                     (
                      Login.TextWithoutPlaceholder,
                      Password.Password
                     )
                );

            Mistake.Visibility = ReferenceEquals
                                     (
                                      isShowMistake,
                                      "On"
                                     )
                                     ? Visibility.Visible
                                     : Visibility.Hidden;

            if (!isAccess) return;

            App.AppProvider = provider;
            var mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
        }

        /// <summary>
        ///     If pressed enter, application will check the access
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments</param>
        private void IsEnterPressed
        (
            object       sender,
            KeyEventArgs e
        ) {
            if (e.Key == Key.Enter)
                AcceptBttn.RaiseEvent
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