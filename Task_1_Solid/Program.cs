using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task_1_Solid
{

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
                        var sourceNumbers = File.ReadLines(filePath).First().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

        partial class Form1
        {
            /// <summary>
            /// Обязательная переменная конструктора.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Освободить все используемые ресурсы.
            /// </summary>
            /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Код, автоматически созданный конструктором форм Windows

            /// <summary>
            /// Требуемый метод для поддержки конструктора — не изменяйте 
            /// содержимое этого метода с помощью редактора кода.
            /// </summary>
            private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                this.splitContainer1 = new System.Windows.Forms.SplitContainer();
                this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
                this.btCalculate = new System.Windows.Forms.Button();
                this.label1 = new System.Windows.Forms.Label();
                this.numD = new System.Windows.Forms.NumericUpDown();
                this.label2 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.numA = new System.Windows.Forms.NumericUpDown();
                this.numB = new System.Windows.Forms.NumericUpDown();
                this.numC = new System.Windows.Forms.NumericUpDown();
                this.btLoadFromFile = new System.Windows.Forms.Button();
                this.txtResult = new System.Windows.Forms.TextBox();
                this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
                this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
                this.splitContainer1.Panel1.SuspendLayout();
                this.splitContainer1.Panel2.SuspendLayout();
                this.splitContainer1.SuspendLayout();
                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.numD)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numC)).BeginInit();
                this.SuspendLayout();
                // 
                // splitContainer1
                // 
                this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
                this.splitContainer1.Location = new System.Drawing.Point(0, 0);
                this.splitContainer1.Name = "splitContainer1";
                this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
                // 
                // splitContainer1.Panel1
                // 
                this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
                // 
                // splitContainer1.Panel2
                // 
                this.splitContainer1.Panel2.Controls.Add(this.txtResult);
                this.splitContainer1.Size = new System.Drawing.Size(767, 433);
                this.splitContainer1.SplitterDistance = 187;
                this.splitContainer1.TabIndex = 0;
                // 
                // tableLayoutPanel1
                // 
                this.tableLayoutPanel1.ColumnCount = 5;
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                this.tableLayoutPanel1.Controls.Add(this.btCalculate, 4, 1);
                this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.numD, 3, 1);
                this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
                this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
                this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
                this.tableLayoutPanel1.Controls.Add(this.numA, 1, 0);
                this.tableLayoutPanel1.Controls.Add(this.numB, 1, 1);
                this.tableLayoutPanel1.Controls.Add(this.numC, 3, 0);
                this.tableLayoutPanel1.Controls.Add(this.btLoadFromFile, 4, 0);
                this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
                this.tableLayoutPanel1.Name = "tableLayoutPanel1";
                this.tableLayoutPanel1.RowCount = 2;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tableLayoutPanel1.Size = new System.Drawing.Size(743, 170);
                this.tableLayoutPanel1.TabIndex = 8;
                // 
                // btCalculate
                // 
                this.btCalculate.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.btCalculate.AutoSize = true;
                this.btCalculate.Location = new System.Drawing.Point(591, 116);
                this.btCalculate.Name = "btCalculate";
                this.btCalculate.Size = new System.Drawing.Size(119, 23);
                this.btCalculate.TabIndex = 9;
                this.btCalculate.Text = "Рассчитать";
                this.btCalculate.UseVisualStyleBackColor = true;
                this.btCalculate.Click += new System.EventHandler(this.BtCalculate_Click);
                // 
                // label1
                // 
                this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(5, 36);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(121, 13);
                this.label1.TabIndex = 4;
                this.label1.Text = "А - Абонентская плата";
                // 
                // numD
                // 
                this.numD.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.numD.Location = new System.Drawing.Point(466, 117);
                this.numD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
                this.numD.Name = "numD";
                this.numD.Size = new System.Drawing.Size(89, 20);
                this.numD.TabIndex = 1;
                this.numD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
                // 
                // label2
                // 
                this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(261, 121);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(199, 13);
                this.label2.TabIndex = 5;
                this.label2.Text = "D - Планируймый объём потребления";
                // 
                // label3
                // 
                this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(266, 36);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(189, 13);
                this.label3.TabIndex = 6;
                this.label3.Text = "С - цена за мегабайт (сверх пакета)";
                // 
                // label4
                // 
                this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.label4.AutoSize = true;
                this.label4.Location = new System.Drawing.Point(3, 121);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(126, 13);
                this.label4.TabIndex = 7;
                this.label4.Text = "В - мегабайтф в пакете";
                // 
                // numA
                // 
                this.numA.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.numA.Location = new System.Drawing.Point(135, 32);
                this.numA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
                this.numA.Name = "numA";
                this.numA.Size = new System.Drawing.Size(120, 20);
                this.numA.TabIndex = 0;
                this.numA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
                // 
                // numB
                // 
                this.numB.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.numB.Location = new System.Drawing.Point(135, 117);
                this.numB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
                this.numB.Name = "numB";
                this.numB.Size = new System.Drawing.Size(120, 20);
                this.numB.TabIndex = 3;
                this.numB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
                // 
                // numC
                // 
                this.numC.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.numC.Location = new System.Drawing.Point(466, 32);
                this.numC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
                this.numC.Name = "numC";
                this.numC.Size = new System.Drawing.Size(89, 20);
                this.numC.TabIndex = 2;
                this.numC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
                // 
                // btLoadFromFile
                // 
                this.btLoadFromFile.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.btLoadFromFile.AutoSize = true;
                this.btLoadFromFile.Location = new System.Drawing.Point(591, 31);
                this.btLoadFromFile.Name = "btLoadFromFile";
                this.btLoadFromFile.Size = new System.Drawing.Size(119, 23);
                this.btLoadFromFile.TabIndex = 8;
                this.btLoadFromFile.Text = "Загрузить из файла";
                this.btLoadFromFile.UseVisualStyleBackColor = true;
                this.btLoadFromFile.Click += new System.EventHandler(this.BtLoadFromFile_Click);
                // 
                // txtResult
                // 
                this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
                this.txtResult.Location = new System.Drawing.Point(0, 0);
                this.txtResult.Multiline = true;
                this.txtResult.Name = "txtResult";
                this.txtResult.ReadOnly = true;
                this.txtResult.Size = new System.Drawing.Size(763, 238);
                this.txtResult.TabIndex = 0;
                // 
                // contextMenuStrip1
                // 
                this.contextMenuStrip1.Name = "contextMenuStrip1";
                this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
                // 
                // openFileDialog1
                // 
                this.openFileDialog1.FileName = "openFileDialog1";
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(767, 433);
                this.Controls.Add(this.splitContainer1);
                this.Name = "Form1";
                this.Text = "Form1";
                this.splitContainer1.Panel1.ResumeLayout(false);
                this.splitContainer1.Panel2.ResumeLayout(false);
                this.splitContainer1.Panel2.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
                this.splitContainer1.ResumeLayout(false);
                this.tableLayoutPanel1.ResumeLayout(false);
                this.tableLayoutPanel1.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.numD)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numC)).EndInit();
                this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.SplitContainer splitContainer1;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.NumericUpDown numB;
            private System.Windows.Forms.NumericUpDown numC;
            private System.Windows.Forms.NumericUpDown numD;
            private System.Windows.Forms.NumericUpDown numA;
            private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            private System.Windows.Forms.TextBox txtResult;
            private System.Windows.Forms.Button btCalculate;
            private System.Windows.Forms.Button btLoadFromFile;
            private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
            private System.Windows.Forms.OpenFileDialog openFileDialog1;
        }
}
