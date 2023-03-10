using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Policy;
using WindowsFormsApp1;

namespace CPR_PatcherTool
{
    public enum FileType { weaponEquip, weaponUnequip, SCAR, ALL };

    internal static class Program
    {
        public const string Version = "1.1.5.0";

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();//显示控制台
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); //释放控制台、关闭控制台

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                AllocConsole();

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                var filePathWin = (new FilePathWindow());
                Application.Run(filePathWin);
                if (filePathWin.isContinue)
                    Application.Run(new CPR_Form());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Expection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static public string[] files = new string[(int)FileType.ALL];
    }
}