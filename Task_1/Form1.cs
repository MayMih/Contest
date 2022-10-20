using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Task_1
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_FILE_PATH = @".\Data.txt";

        internal string filePath { get; set; } = DEFAULT_FILE_PATH;

        public Form1()
        {
            InitializeComponent();
            numA.Minimum = numB.Minimum = numC.Minimum = numD.Minimum = 1;
            numA.Maximum = numB.Maximum = numC.Maximum = numD.Maximum = 100;
        }

        internal void BtLoadFromFile_Click(object sender, System.EventArgs e)
        {
            if (!Visible || openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Visible)
                {
                    filePath = openFileDialog1.FileName;
                }
                txtResult.Text += Environment.NewLine + "Загружен: \n" + filePath;
                try
                {
                    var sourceNumbers = File.ReadLines(filePath).First().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    // стоимость тарифа (р.)
                    numA.Value = uint.Parse(sourceNumbers[0]);
                    // пакет трафика (МБ)
                    numB.Value = uint.Parse(sourceNumbers[1]);
                    // цена за мегабайт сверх пакета (р.)
                    numC.Value = uint.Parse(sourceNumbers[2]);
                    // планируемый объём поттребления трафика (МБ)
                    numD.Value = uint.Parse(sourceNumbers[3]);
                }
                catch (Exception ex)
                {
                    string mes = $@"Ошибка чтения файла ""{filePath}""\n {ex}";
                    txtResult.Text += Environment.NewLine + mes;
                    if (this.Visible)
                    {
                        MessageBox.Show(mes);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        internal void BtCalculate_Click(object sender, EventArgs e)
        {
            int auxTraffic = (int)(numD.Value - numB.Value);
            uint res = (uint)(auxTraffic <= 0 ? numA.Value : numA.Value + auxTraffic * numC.Value);
            txtResult.Text += Environment.NewLine + $"Результат: {res} (р.)";
            Console.WriteLine(res);
        }
    }
}
