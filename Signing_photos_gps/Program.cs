using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Signing_photos_gps
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //This code checks if the OS is at least Windows 7
            if (System.Environment.OSVersion.Version < new Version(6, 1))
            {
                MessageBox.Show("Ваша версия операционной системы не поддерживается,\n" +
                    "требуется версия не ниже Windows 7.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            //
            Application.Run(new frmMain());
        }
    }
}
