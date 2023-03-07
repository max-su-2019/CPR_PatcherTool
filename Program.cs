using System.Runtime.InteropServices;
using WindowsFormsApp1;

namespace CPR_PatcherTool
{
    internal static class Program
    {
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
            AllocConsole();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new CPR_Form());

        }

        static string SelectPath() //弹出一个选择文件的对话框
        {
            var path = new FolderBrowserDialog();
            if (path.ShowDialog() == DialogResult.OK)
                return path.SelectedPath;

            return string.Empty;
        }
    }
}