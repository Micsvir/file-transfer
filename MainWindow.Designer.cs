namespace FileTransfer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tbFileMask = new System.Windows.Forms.TextBox();
            this.gbSearchParams = new System.Windows.Forms.GroupBox();
            this.chbSearchAtFilesBegining = new System.Windows.Forms.CheckBox();
            this.chbSearchWithText = new System.Windows.Forms.CheckBox();
            this.tbSearchingText = new System.Windows.Forms.TextBox();
            this.bStop = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.tbRootFolder = new System.Windows.Forms.TextBox();
            this.lRootFolder = new System.Windows.Forms.Label();
            this.bCopy = new System.Windows.Forms.Button();
            this.bFind = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.lPathForCopying = new System.Windows.Forms.Label();
            this.lFileMask = new System.Windows.Forms.Label();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.lbResults = new System.Windows.Forms.ListBox();
            this.lcurFileName = new System.Windows.Forms.Label();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.lFilesSize = new System.Windows.Forms.Label();
            this.pbCircle = new System.Windows.Forms.PictureBox();
            this.lDeletingErrorsAmount = new System.Windows.Forms.LinkLabel();
            this.lCopingErrorsAmount = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lPercents = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbSearchParams.SuspendLayout();
            this.gbResults.SuspendLayout();
            this.gbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCircle)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFileMask
            // 
            this.tbFileMask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileMask.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFileMask.Location = new System.Drawing.Point(119, 21);
            this.tbFileMask.Name = "tbFileMask";
            this.tbFileMask.Size = new System.Drawing.Size(596, 13);
            this.tbFileMask.TabIndex = 0;
            // 
            // gbSearchParams
            // 
            this.gbSearchParams.Controls.Add(this.chbSearchAtFilesBegining);
            this.gbSearchParams.Controls.Add(this.chbSearchWithText);
            this.gbSearchParams.Controls.Add(this.tbSearchingText);
            this.gbSearchParams.Controls.Add(this.bStop);
            this.gbSearchParams.Controls.Add(this.bDelete);
            this.gbSearchParams.Controls.Add(this.tbRootFolder);
            this.gbSearchParams.Controls.Add(this.lRootFolder);
            this.gbSearchParams.Controls.Add(this.bCopy);
            this.gbSearchParams.Controls.Add(this.bFind);
            this.gbSearchParams.Controls.Add(this.tbFolder);
            this.gbSearchParams.Controls.Add(this.lPathForCopying);
            this.gbSearchParams.Controls.Add(this.lFileMask);
            this.gbSearchParams.Controls.Add(this.tbFileMask);
            this.gbSearchParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSearchParams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbSearchParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbSearchParams.Location = new System.Drawing.Point(0, 0);
            this.gbSearchParams.Name = "gbSearchParams";
            this.gbSearchParams.Size = new System.Drawing.Size(721, 126);
            this.gbSearchParams.TabIndex = 1;
            this.gbSearchParams.TabStop = false;
            this.gbSearchParams.Text = "Параметры поиска и копирования";
            // 
            // chbSearchAtFilesBegining
            // 
            this.chbSearchAtFilesBegining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSearchAtFilesBegining.AutoSize = true;
            this.chbSearchAtFilesBegining.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbSearchAtFilesBegining.Location = new System.Drawing.Point(528, 76);
            this.chbSearchAtFilesBegining.Name = "chbSearchAtFilesBegining";
            this.chbSearchAtFilesBegining.Size = new System.Drawing.Size(187, 17);
            this.chbSearchAtFilesBegining.TabIndex = 6;
            this.chbSearchAtFilesBegining.Text = "Искать только в начале файлов";
            this.chbSearchAtFilesBegining.UseVisualStyleBackColor = true;
            this.chbSearchAtFilesBegining.Visible = false;
            this.chbSearchAtFilesBegining.CheckedChanged += new System.EventHandler(this.chbSearchAtFilesBegining_CheckedChanged);
            // 
            // chbSearchWithText
            // 
            this.chbSearchWithText.AutoSize = true;
            this.chbSearchWithText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbSearchWithText.Location = new System.Drawing.Point(15, 77);
            this.chbSearchWithText.Name = "chbSearchWithText";
            this.chbSearchWithText.Size = new System.Drawing.Size(92, 17);
            this.chbSearchWithText.TabIndex = 4;
            this.chbSearchWithText.Text = "Искать текст";
            this.chbSearchWithText.UseVisualStyleBackColor = true;
            this.chbSearchWithText.CheckedChanged += new System.EventHandler(this.chSearchWithText_CheckedChanged);
            // 
            // tbSearchingText
            // 
            this.tbSearchingText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchingText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSearchingText.Enabled = false;
            this.tbSearchingText.Location = new System.Drawing.Point(119, 78);
            this.tbSearchingText.Name = "tbSearchingText";
            this.tbSearchingText.Size = new System.Drawing.Size(403, 13);
            this.tbSearchingText.TabIndex = 5;
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bStop.Location = new System.Drawing.Point(640, 96);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 23);
            this.bStop.TabIndex = 10;
            this.bStop.Text = "Стоп";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bDelete
            // 
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDelete.Location = new System.Drawing.Point(177, 96);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(75, 23);
            this.bDelete.TabIndex = 9;
            this.bDelete.Text = "Удалить";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // tbRootFolder
            // 
            this.tbRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRootFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRootFolder.Location = new System.Drawing.Point(119, 40);
            this.tbRootFolder.Name = "tbRootFolder";
            this.tbRootFolder.Size = new System.Drawing.Size(596, 13);
            this.tbRootFolder.TabIndex = 1;
            // 
            // lRootFolder
            // 
            this.lRootFolder.AutoSize = true;
            this.lRootFolder.Location = new System.Drawing.Point(12, 40);
            this.lRootFolder.Name = "lRootFolder";
            this.lRootFolder.Size = new System.Drawing.Size(99, 13);
            this.lRootFolder.TabIndex = 6;
            this.lRootFolder.Text = "Корневой каталог";
            // 
            // bCopy
            // 
            this.bCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCopy.Location = new System.Drawing.Point(96, 96);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(75, 23);
            this.bCopy.TabIndex = 8;
            this.bCopy.Text = "Копировать";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // bFind
            // 
            this.bFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bFind.Location = new System.Drawing.Point(15, 96);
            this.bFind.Name = "bFind";
            this.bFind.Size = new System.Drawing.Size(75, 23);
            this.bFind.TabIndex = 7;
            this.bFind.Text = "Найти";
            this.bFind.UseVisualStyleBackColor = true;
            this.bFind.Click += new System.EventHandler(this.bFind_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFolder.Location = new System.Drawing.Point(119, 59);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(596, 13);
            this.tbFolder.TabIndex = 2;
            this.tbFolder.TextChanged += new System.EventHandler(this.tbFolder_TextChanged);
            // 
            // lPathForCopying
            // 
            this.lPathForCopying.AutoSize = true;
            this.lPathForCopying.Location = new System.Drawing.Point(12, 59);
            this.lPathForCopying.Name = "lPathForCopying";
            this.lPathForCopying.Size = new System.Drawing.Size(94, 13);
            this.lPathForCopying.TabIndex = 2;
            this.lPathForCopying.Text = "Целевой каталог";
            // 
            // lFileMask
            // 
            this.lFileMask.AutoSize = true;
            this.lFileMask.Location = new System.Drawing.Point(12, 21);
            this.lFileMask.Name = "lFileMask";
            this.lFileMask.Size = new System.Drawing.Size(75, 13);
            this.lFileMask.TabIndex = 1;
            this.lFileMask.Text = "Маска файла";
            // 
            // gbResults
            // 
            this.gbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResults.Controls.Add(this.lbResults);
            this.gbResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbResults.Location = new System.Drawing.Point(0, 126);
            this.gbResults.Name = "gbResults";
            this.gbResults.Size = new System.Drawing.Size(721, 264);
            this.gbResults.TabIndex = 2;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "Результаты поиска";
            // 
            // lbResults
            // 
            this.lbResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResults.FormattingEnabled = true;
            this.lbResults.HorizontalScrollbar = true;
            this.lbResults.Location = new System.Drawing.Point(3, 16);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(715, 234);
            this.lbResults.TabIndex = 11;
            this.lbResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbResults_MouseClick);
            // 
            // lcurFileName
            // 
            this.lcurFileName.AutoSize = true;
            this.lcurFileName.Location = new System.Drawing.Point(137, 62);
            this.lcurFileName.Name = "lcurFileName";
            this.lcurFileName.Size = new System.Drawing.Size(66, 13);
            this.lcurFileName.TabIndex = 1;
            this.lcurFileName.Text = "curFileName";
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.lFilesSize);
            this.gbInfo.Controls.Add(this.pbCircle);
            this.gbInfo.Controls.Add(this.lDeletingErrorsAmount);
            this.gbInfo.Controls.Add(this.lCopingErrorsAmount);
            this.gbInfo.Controls.Add(this.label4);
            this.gbInfo.Controls.Add(this.label3);
            this.gbInfo.Controls.Add(this.label2);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Controls.Add(this.lPercents);
            this.gbInfo.Controls.Add(this.lcurFileName);
            this.gbInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbInfo.Location = new System.Drawing.Point(0, 396);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(721, 85);
            this.gbInfo.TabIndex = 3;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Информация";
            // 
            // lFilesSize
            // 
            this.lFilesSize.AutoSize = true;
            this.lFilesSize.Location = new System.Drawing.Point(157, 23);
            this.lFilesSize.Name = "lFilesSize";
            this.lFilesSize.Size = new System.Drawing.Size(13, 13);
            this.lFilesSize.TabIndex = 11;
            this.lFilesSize.Text = "0";
            // 
            // pbCircle
            // 
            this.pbCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCircle.InitialImage = null;
            this.pbCircle.Location = new System.Drawing.Point(667, 9);
            this.pbCircle.Name = "pbCircle";
            this.pbCircle.Size = new System.Drawing.Size(50, 50);
            this.pbCircle.TabIndex = 10;
            this.pbCircle.TabStop = false;
            this.pbCircle.Visible = false;
            // 
            // lDeletingErrorsAmount
            // 
            this.lDeletingErrorsAmount.AutoSize = true;
            this.lDeletingErrorsAmount.Location = new System.Drawing.Point(137, 49);
            this.lDeletingErrorsAmount.Name = "lDeletingErrorsAmount";
            this.lDeletingErrorsAmount.Size = new System.Drawing.Size(13, 13);
            this.lDeletingErrorsAmount.TabIndex = 9;
            this.lDeletingErrorsAmount.TabStop = true;
            this.lDeletingErrorsAmount.Text = "0";
            this.lDeletingErrorsAmount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lDeletingErrorsAmount_LinkClicked);
            // 
            // lCopingErrorsAmount
            // 
            this.lCopingErrorsAmount.AutoSize = true;
            this.lCopingErrorsAmount.Location = new System.Drawing.Point(137, 36);
            this.lCopingErrorsAmount.Name = "lCopingErrorsAmount";
            this.lCopingErrorsAmount.Size = new System.Drawing.Size(13, 13);
            this.lCopingErrorsAmount.TabIndex = 8;
            this.lCopingErrorsAmount.TabStop = true;
            this.lCopingErrorsAmount.Text = "0";
            this.lCopingErrorsAmount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lCopingErrorsAmount_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ошибки удаления:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ошибки копирования:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Текущий файл: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Файлов всего:";
            // 
            // lPercents
            // 
            this.lPercents.AutoSize = true;
            this.lPercents.Location = new System.Drawing.Point(137, 23);
            this.lPercents.Name = "lPercents";
            this.lPercents.Size = new System.Drawing.Size(13, 13);
            this.lPercents.TabIndex = 2;
            this.lPercents.Text = "0";
            this.lPercents.TextChanged += new System.EventHandler(this.lPercents_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFile,
            this.openFileFolder});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(203, 48);
            // 
            // openFile
            // 
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(202, 22);
            this.openFile.Text = "Открыть файл";
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // openFileFolder
            // 
            this.openFileFolder.Name = "openFileFolder";
            this.openFileFolder.Size = new System.Drawing.Size(202, 22);
            this.openFileFolder.Text = "Открыть папку с файлом";
            this.openFileFolder.Click += new System.EventHandler(this.openFileFolder_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 481);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.gbResults);
            this.Controls.Add(this.gbSearchParams);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FileTransfer";
            this.gbSearchParams.ResumeLayout(false);
            this.gbSearchParams.PerformLayout();
            this.gbResults.ResumeLayout(false);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCircle)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileMask;
        private System.Windows.Forms.GroupBox gbSearchParams;
        private System.Windows.Forms.Label lFileMask;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label lPathForCopying;
        private System.Windows.Forms.GroupBox gbResults;
        private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.Button bFind;
        private System.Windows.Forms.Label lRootFolder;
        private System.Windows.Forms.TextBox tbRootFolder;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.ListBox lbResults;
        private System.Windows.Forms.Label lcurFileName;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Label lPercents;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lCopingErrorsAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lDeletingErrorsAmount;
        private System.Windows.Forms.CheckBox chbSearchWithText;
        private System.Windows.Forms.TextBox tbSearchingText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openFile;
        private System.Windows.Forms.ToolStripMenuItem openFileFolder;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pbCircle;
        private System.Windows.Forms.Label lFilesSize;
        private System.Windows.Forms.CheckBox chbSearchAtFilesBegining;
    }
}

