using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Task_1
{
    public partial class Form1 : Form
    {
        private string filePath = @".\Data.txt";

        public Form1()
        {
            InitializeComponent();
            numA.Minimum = numB.Minimum = numC.Minimum = numD.Minimum = 1;
            numA.Maximum = numB.Maximum = numC.Maximum = numD.Maximum = 100;
        }

        private void BtLoadFromFile_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                txtResult.Text += Environment.NewLine + "Загружен: \n" + filePath;
                try
                {
                    var sourceNumbers = File.ReadLines(filePath).First().Split();
                    numA.Value = uint.Parse(sourceNumbers[0]);
                    numB.Value = uint.Parse(sourceNumbers[1]);
                    numC.Value = uint.Parse(sourceNumbers[2]);
                    numD.Value = uint.Parse(sourceNumbers[3]);
                }
                catch (Exception ex)
                {
                    string mes = $@"Ошибка чтения файла ""{filePath}""\n {ex}";
                    txtResult.Text += Environment.NewLine + mes;
                    MessageBox.Show(mes);
                }
            }
        }

        private void BtCalculate_Click(object sender, EventArgs e)
        {
            
        }
    }
}
