using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

namespace BigTextReader
{
    public partial class MainForm : Form
    {
        private List<string> currentLines;
        private StreamReader textReader;
        private int maxLines = 100;
        private string currentFile;

        public MainForm()
        {
            InitializeComponent();
            currentLines = new List<string>();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;
            ofd.Title = "Select a text file";
            ofd.CheckFileExists = true;
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                currentLines.Clear();
                currentFile = ofd.FileName;
                textReader = new StreamReader(currentFile);
                string line = textReader.ReadLine();
                int lineCount = 0;
                while (line != null && lineCount < maxLines)
                {
                    currentLines.Add(line);
                    line = textReader.ReadLine();
                    lineCount++;
                }
                LoadCurrentLines();
            }
        }

        private void LoadCurrentLines()
        {
            string content = string.Empty;
            foreach (string line in currentLines)
            {
                content += line + Environment.NewLine;
            }
            rtxtboxContent.Text = content;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outputFileName = Interaction.InputBox("Enter File Name", "Input Dialog", "");
            DirectoryInfo dinfo = Directory.GetParent(currentFile);
            outputFileName = Path.Combine(dinfo.FullName, outputFileName);
            StreamWriter sw = new StreamWriter(outputFileName);
            for (int i = 0; i < currentLines.Count(); i++)
            {
                sw.WriteLine(currentLines[i]);
            }
            sw.Close();
        }

        private void loadNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLines.Clear();
            string line = textReader.ReadLine();
            int lineCount = 0;
            while (line != null && lineCount < maxLines)
            {
                currentLines.Add(line);
                line = textReader.ReadLine();
                lineCount++;
            }
            LoadCurrentLines();
        }
    }
}
