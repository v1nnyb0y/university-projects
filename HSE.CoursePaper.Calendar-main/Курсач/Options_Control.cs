using Курсач.Properties;

namespace Курсач
{
    internal class Options_Control
    {
        private static ClassOptions options = new ClassOptions();

        public static ClassOptions GetClassOptions => options;

        public static void NewOption(int firstday, bool notalarm, bool numberweeks) {
            options = new ClassOptions(firstday, notalarm, numberweeks);
        }

        public static void SaveOptions() {
            options.Save();
            if (options.FirstDay == 0)
                Settings.Default.LoadSettings = false;
            else
                Settings.Default.LoadSettings = true;
        }

        public static void LoadOptions() {
            options.Load(ref options);
        }
    }
}