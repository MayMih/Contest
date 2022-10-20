using System;
using System.Windows.Forms;


namespace Task_1
{
    /// <summary>
    /// Задача 1. "Рассчёт тарифа2 (простая задача на файлы и формулы)
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                using (var frm = new Form1())
                {
                    frm.filePath = args[0].Trim();
                    frm.BtLoadFromFile_Click(frm, EventArgs.Empty);
                    frm.BtCalculate_Click(frm, EventArgs.Empty);
                }
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
