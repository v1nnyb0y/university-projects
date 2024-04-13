using System.Windows;
using Provider;

namespace Lab._5.LINQ.cfg
{
    /// <summary>
    ///     Application resources
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     Current provider for the applications
        /// </summary>
        public static CurrentProvider AppProvider;

        /// <summary>
        ///     App start
        /// </summary>
        /// <param name="sender">This application</param>
        /// <param name="e">Info about start</param>
        private void AppStart
        (
            object           sender,
            StartupEventArgs e
        ) {
            AppProvider = new CurrentProvider();
        }
    }
}