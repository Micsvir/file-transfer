using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileTransfer
{
    public partial class fInfo : Form
    {
        public fInfo()
        {
            InitializeComponent();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = lvInfo.SelectedItems[0].Text.Substring(0, lvInfo.SelectedItems[0].Text.LastIndexOf('\\'));
            Process.Start(path);
            /*
            if (lvInfo.SelectedItems[0].Text == "Ошибок копирования")
            {
                fInfo badFilesInfo = new fInfo();
                badFilesInfo.lvInfo.Clear();
                badFilesInfo.lvInfo.Columns.Add("Имя файла");
                badFilesInfo.lvInfo.Columns[0].Width = 300;
                foreach (string curBadFile in Form1.badFilesForCopying)
                {
                    ListViewItem newLVI = new ListViewItem();
                    newLVI.Text = curBadFile;
                    //newLVI.SubItems.Add("ошибка копирования");
                    badFilesInfo.lvInfo.Items.Add(newLVI);
                }
                badFilesInfo.Show();
            }
            if (lvInfo.SelectedItems[0].Text == "Ошибок удаления")
            {
                fInfo badFilesInfo = new fInfo();
                badFilesInfo.lvInfo.Clear();
                badFilesInfo.lvInfo.Columns.Add("Имя файла");
                badFilesInfo.lvInfo.Columns[0].Width = 300;
                foreach (string curBadFile in Form1.badFilesForDeleting)
                {
                    ListViewItem newLVI = new ListViewItem();
                    newLVI.Text = curBadFile;
                    newLVI.SubItems.Add("ошибка удаления");
                    badFilesInfo.lvInfo.Items.Add(newLVI);
                }
                badFilesInfo.Show();
            }
            */
        }

        private void copyFullFileName_Click(object sender, EventArgs e)
        {
            string selectedFilesNames = "";
            foreach (ListViewItem curLVI in lvInfo.SelectedItems)
            {
                selectedFilesNames += curLVI.Text + "\n";
            }
            System.Windows.Forms.Clipboard.SetText(selectedFilesNames);
        }

        private void lvInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip2.Show(MousePosition);
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(lvInfo.SelectedItems[0].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openFileFolder_Click(object sender, EventArgs e)
        {
            Process.Start(lvInfo.SelectedItems[0].Text.Substring(0, lvInfo.SelectedItems[0].Text.LastIndexOf('\\')));
        }

        private void openCopiedFileFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string copiedDirPath = lvInfo.SelectedItems[0].Text.Replace(MainWindow.bgFinder.directoryToFindFrom, MainWindow.bgFinder.targetDirectory);
                copiedDirPath = copiedDirPath.Substring(0,copiedDirPath.LastIndexOf('\\'));
                MessageBox.Show(copiedDirPath);
                Process.Start(copiedDirPath);
            }
            catch
            {
                MessageBox.Show("Скорее всего, такой папки не существует");
            }
        }
    }
}
