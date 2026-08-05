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

        public static MainForm MainForm { get; set; } = null!;

        public static Assembly Assembly { get; private set; } = Assembly.GetExecutingAssembly();

        [STAThread]
        private static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
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