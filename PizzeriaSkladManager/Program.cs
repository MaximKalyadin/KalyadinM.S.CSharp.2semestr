using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzeriaSkladManager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApiClient.Connect();

            var form = new FormLogin();
            if (form.ShowDialog() == DialogResult.OK &&
                form.Password.Equals(ConfigurationManager.AppSettings["Password"]))
            {
                Application.Run(new FormMain());
            }
            else
                MessageBox.Show("Неверный логин!", "Ошибка", MessageBoxButtons.OK);
        }
    }
}
