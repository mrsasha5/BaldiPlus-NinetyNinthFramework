using NineNineFrameworkTool.Data;
using NineNineFrameworkTool.Forms;
using System.Globalization;
using System.Reflection;
using System.Security.Principal;

namespace NineNineFrameworkTool
{
    internal static class Program
    {
        /*public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent())
            .IsInRole(WindowsBuiltInRole.Administrator);*/

        public const string MutexGuid = "5747DCFF-E39D-4727-9045-E46C32FE9A1B";
        public const string ErrorStartTitle = "Ops!";
        public const string ErrorStartDesc = "Ninety-Ninth Framework GUI Tool is active. If you accidentally closed all forms, use a notify icon in the taskbar to get them back.";

        public static MainForm MainForm { get; set; } = null!;

        public static Assembly Assembly { get; private set; } = Assembly.GetExecutingAssembly();

        [STAThread]
        private static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            using (Mutex mutex = new Mutex(false, MutexGuid))
            {
                bool appDuplicated = true;
                try { if (mutex.WaitOne(0, false)) appDuplicated = false; }
                catch { }
                
                if (appDuplicated)
                {
                    MessageBox.Show(ErrorStartDesc, ErrorStartTitle);
                    return;
                }

                if (AppConfigDataManager.Load(out _))
                {
                    MainForm = new MainForm();
                    MainForm.Show();
                    MainForm.Activate();
                }
                else
                {
                    new FirstStartForm().Show();
                }

                Application.Run();
            }
        }
    }
}