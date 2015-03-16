using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDSSMakePaymentParent
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
            Application.Run(new RDDSMakePayments.rfrmLogin());
        }
        static Program()
        {
            Telerik.WinControls.RadTypeResolver.Instance.ResolveTypesInCurrentAssembly = true;
            
        }
    }
}
