    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace FileCompare
    {
    public partial class FileCompare : Form
    {
        BackgroundWorker worker = new BackgroundWorker();

        public FileCompare()
        {
            InitializeComponent();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.ProgressChanged += backgroundWorker1_ProgressChanged;
            worker.DoWork += backgroundWorker1_DoWork;
        }

        void CopyFile(string source, string des)
        {
            FileStream copFile = new FileStream(des, FileMode.Create);
            FileStream origFile = new FileStream(source, FileMode.Open);

            byte[] bt = new byte[1048756];
            int readByte;

            while((readByte = origFile.Read(bt, 0, bt.Length)) > 0)
            {
                copFile.Write(bt, 0, readByte);
                worker.ReportProgress((int)(origFile.Position * 100 / origFile.Length));
                //Please change a path...
                string logFilePath = @"C:\Users\NShingange\Desktop\logFile.txt";

                if (!File.Exists(logFilePath))
                {
                    // Create a file to write to.
                    using (StreamWriter lg = File.CreateText(logFilePath))
                    {
                        lg.WriteLine(source + " Copied to " + des);
                    }
                }
                else
                {
                    using (StreamWriter lg = File.AppendText(logFilePath))
                    {
                        lg.WriteLine(source + " Copied to " + des);
                    }

                }
            }
            origFile.Close();
            copFile.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog b1 = new OpenFileDialog();

            if (b1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = b1.FileName;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog b2 = new FolderBrowserDialog();

            if (b2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = Path.Combine(b2.SelectedPath, Path.GetFileName(textBox1.Text));

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            CopyFile(textBox1.Text, textBox2.Text);
        }

        private void backgroundWorker1_ProgressChanged(object sender,ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label5.Text = progressBar1.Value.ToString() + "%";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string file_name = @"C:\Users\NShingange\Desktop\logFile.txt";
            System.Diagnostics.Process.Start(file_name);
        }

        private void FileCompare_Load(object sender, EventArgs e)
        {

        }
    }
    }
