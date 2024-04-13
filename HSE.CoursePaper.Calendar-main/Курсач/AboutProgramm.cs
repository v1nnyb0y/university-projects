using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Курсач
{
    internal partial class AboutProgramm : Form
    {
        public AboutProgramm() {
            InitializeComponent();
            Text = string.Format("О программе {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format("Версия {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            richTextBox1.Text = "                                                 Инструкция по использованию:\n" +
                                AssemblyDescription;
        }

    #region Методы доступа к атрибутам сборки

        public string AssemblyTitle {
            get {
                var attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    if (titleAttribute.Title != "") return titleAttribute.Title;
                }

                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription {
            get {
                var attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) return "";
                var arr = ((AssemblyDescriptionAttribute) attributes[0]).Description;
                return arr;
            }
        }

        public string AssemblyProduct {
            get {
                var attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                var attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                var attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) return "";
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

    #endregion
    }
}