using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace patentrename
{
    public partial class Form1 : Form
    {
        string directoryPath = "";
        string prefix = "";
        string endfix = "";
        int mDelay = 1000;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string default_str = @"";
            
            if (directoryPathTb.Text.Equals(""))
            {
                MessageBox.Show("Please select folder");
                return;
            }

            prefix = prefixTb.Text;
            endfix = endfixTb.Text;

            if (prefix.Equals("") && endfix.Equals(""))
            {
                MessageBox.Show("Would prifix or endfix empty?");
            }

            string path = default_str + directoryPathTb.Text;
            handleDirectory(directoryPath);

            MessageBox.Show("The Work Done");
            runBt.Enabled = false;
        }

        public void handleDirectory(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles();

            processFile(files, prefix, endfix);

            string[] subDirEntities = Directory.GetDirectories(path);
            foreach (string subDir in subDirEntities)
                handleDirectory(subDir);
        }

        public void processFile(FileInfo[] fileNames, string prefix, string endfix)
        {
            foreach (FileInfo f in fileNames)
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(f.FullName);
                string newFile = f.DirectoryName + "\\" +prefix+ "_" + fileNameWithoutExt + "_"+ endfix + f.Extension;
                Thread.Sleep(mDelay);
                changedfileNameLb.Text = newFile;
                originFileNameLb.Text = f.FullName;
                File.Move(f.FullName, newFile);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void directoryPathBt_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.Description = "Custom Description"; //not mandatory

            if (fbd.ShowDialog() == DialogResult.OK)
                directoryPath = fbd.SelectedPath;
            else
                directoryPath = string.Empty;
            directoryPathTb.Text = directoryPath;
        }
    }
}
