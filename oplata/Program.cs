using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace oplata
{
    static class Program
    {
        private static bool InstanceExists()
        {
            bool createdNew;
            new Mutex(false, "OneInstanceApplication", out createdNew);
            return (!createdNew);
        }
        [STAThread]
        static void Main()
        {
            if (InstanceExists())
            {
                MessageBox.Show("Программа уже запущенна");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new authentification());
            if (Globals.iii == 1)
            { Application.Run(new Main()); }
        }
    }
}
