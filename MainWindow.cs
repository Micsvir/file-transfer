using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UtilSubSys;
using System.Threading;
using System.Diagnostics;

namespace FileTransfer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            tbRootFolder.Text = "C:\\";
            tbFileMask.Text = "*.*";
            tbFolder.Text = "C:\\destFolder";
            lcurFileName.Text = "";
            lPercents.Text = "";
            lCopingErrorsAmount.Text = "";
            lDeletingErrorsAmount.Text = "";
            lFilesSize.Text = "";
            FilesSearcher.FileHasBeenFound += new FilesSearcher.FoundFilesHandler(ShowNextFoundFile); //отображает текущий найденный файл в lCurFileName.Text
            FilesSearcher.FileIsBrowsing += new FilesSearcher.BrowsingFileHandler(ShowCurrentBrowsingFile); //отображает имя текущего просматриваемого во время поиска файла в lCurFileName.Text
            FilesSearcher.FileHasBeenFound += new FilesSearcher.FoundFilesHandler(AddFoundFileToListBox); //Добавляет найденные файлы в lbResults по мере их обнаружения
            BackgroundFinder.FilesHaveBeenFound += new BackgroundFinder.BackgroundFinderEventHandler(FindingFinishedSizeCaption);
            BackgroundFinder.FilesHaveBeenFound += new BackgroundFinder.BackgroundFinderEventHandler(FindingFinishedCaption);
            BackgroundFinder.FileHasBeenCopied += new BackgroundFinder.BackgroundFinderFilesHandler(ShowNextCopiedFile);
            BackgroundFinder.FileHasBeenCopied += new BackgroundFinder.BackgroundFinderFilesHandler(CopiedFilesSize);
            BackgroundFinder.FilesHaveBeenCopied += new BackgroundFinder.BackgroundFinderEventHandler(CopingFinishedCaption);
            BackgroundFinder.FileHasBeenDeleted += new BackgroundFinder.BackgroundFinderFilesHandler(ShowNextDeletedFile);
            BackgroundFinder.FileHasBeenDeleted += new BackgroundFinder.BackgroundFinderFilesHandler(DeletedFilesSize);
            BackgroundFinder.FilesHaveBeenDeleted += new BackgroundFinder.BackgroundFinderEventHandler(DeletingFinishedCaption);
            BackgroundFinder.FileHasBeenCopied += new BackgroundFinder.BackgroundFinderFilesHandler(CopingPercents);
            BackgroundFinder.FileHasBeenDeleted += new BackgroundFinder.BackgroundFinderFilesHandler(DeletingPercents);
            FilesSearcher.FileHasBeenFound += new FilesSearcher.FoundFilesHandler(FoundFilesAmount);
            BackgroundFinder.CopingErrorOccured += new BackgroundFinder.BackgroundFinderEventHandler(ShowCopingErrorsAmount);
            BackgroundFinder.DeletingErrorOccured += new BackgroundFinder.BackgroundFinderEventHandler(ShowDeletingErrorsAmount);
        }

        //Global variables

        public class BackgroundFinder
        {
            public string directoryToFindFrom;
            public string targetDirectory;
            private string[] fileMasks;
            private string[] searchingText;
            private Thread bgFind;
            private Thread bgCopy;
            private Thread bgDelete;
            public UtilSubSys.FilesSearcher.SearchResults result;
            public bool isWorking
            {
                get
                {
                    return _isWorking;
                }
            }
            private bool _isWorking;
            public delegate void BackgroundFinderEventHandler();
            public delegate void BackgroundFinderFilesHandler(FilesSearcher.FoundObjectInfo fileInfo);
            public static event BackgroundFinderEventHandler FilesHaveBeenFound;
            public static event BackgroundFinderEventHandler FilesHaveBeenCopied;
            public static event BackgroundFinderFilesHandler FileHasBeenCopied;
            public static event BackgroundFinderEventHandler FilesHaveBeenDeleted;
            public static event BackgroundFinderFilesHandler FileHasBeenDeleted;
            public static event BackgroundFinderEventHandler CopingErrorOccured;
            public static event BackgroundFinderEventHandler DeletingErrorOccured;

            private void FindMethod()
            {
                _isWorking = true;
                //если выбран вариант поиска файлов без поиска текста внутри файлов
                //(chbSearchWithText.Enabled = false, searchTextInFiles = false), все остается по-старому
                if (!searchTextInFiles)
                {
                    if (this.fileMasks.Length > 0)
                    {
                        UtilSubSys.FilesSearcher searcher = new FilesSearcher();
                        this.result = new FilesSearcher.SearchResults();
                        this.result = searcher.GetFiles(directoryToFindFrom, this.fileMasks, true);
                        foreach (FilesSearcher.FoundObjectInfo curObjInfo in bgFinder.result.foundObjectsList)
                        {
                            foundFilesSize += curObjInfo.Size;
                        }
                        if (FilesHaveBeenFound != null)
                        {
                            FilesHaveBeenFound();
                        }
                    }
                    else
                    {
                        UtilSubSys.FilesSearcher searcher = new FilesSearcher();
                        this.result = new FilesSearcher.SearchResults();
                        this.result = searcher.GetFiles(directoryToFindFrom, true);
                        foreach (FilesSearcher.FoundObjectInfo curObjInfo in bgFinder.result.foundObjectsList)
                        {
                            foundFilesSize += curObjInfo.Size;
                        }
                        if (FilesHaveBeenFound != null)
                        {
                            FilesHaveBeenFound();
                        }
                    }
                }
                //если же выбран вариант поиска файлов с поиском текста внутри файлов,
                //предыдущий вариант поиска должен быть соответствующим образом изменен
                else
                {
                    if (this.fileMasks.Length > 0)
                    {
                        UtilSubSys.FilesSearcher searcher = new FilesSearcher();

                        //если выбрана опция поиска текста только вначале файлов, 
                        //необходимо присвоить соответствующее значение переменной searchAtFilesBegining 
                        //объекту класса FilesSearcher
                        searcher.searchAtFilesBegining = searchTextAtFilesBegining;

                        this.result = new FilesSearcher.SearchResults();
                        this.result = searcher.GetFiles(directoryToFindFrom, this.fileMasks, this.searchingText, true);
                        //подсчет занимаемого найденными файлами дискового пространства
                        foreach (FilesSearcher.FoundObjectInfo curObjInfo in bgFinder.result.foundObjectsList)
                        {
                            foundFilesSize += curObjInfo.Size;
                        }
                        if (FilesHaveBeenFound != null)
                        {
                            FilesHaveBeenFound();
                        }
                    }
                    else
                    {
                        UtilSubSys.FilesSearcher searcher = new FilesSearcher();

                        //если выбрана опция поиска текста только вначале файлов, 
                        //необходимо присвоить соответствующее значение переменной searchAtFilesBegining 
                        //объекту класса FilesSearcher
                        searcher.searchAtFilesBegining = searchTextAtFilesBegining;

                        string[] fileMask = {"*.*"};
                        this.result = new FilesSearcher.SearchResults();
                        this.result = searcher.GetFiles(directoryToFindFrom, fileMask, this.searchingText, true);
                        //подсчет занимаемого найденными файлами дискового пространства
                        foreach (FilesSearcher.FoundObjectInfo curObjInfo in bgFinder.result.foundObjectsList)
                        {
                            foundFilesSize += curObjInfo.Size;
                        }
                        if (FilesHaveBeenFound != null)
                        {
                            FilesHaveBeenFound();
                        }
                    }
                }
                _isWorking = false;
            }

            private void CopyMethod()
            {
                _isWorking = true;
                foreach (FilesSearcher.FoundObjectInfo curObjectInfo in this.result.foundObjectsList)
                {
                    string newFileName = curObjectInfo.Name.Replace(directoryToFindFrom, targetDirectory);

                    //проверяем, существует ли целевая директория
                    string directory = newFileName.Substring(0, newFileName.LastIndexOf('\\') + 1);
                    if (!System.IO.Directory.Exists(directory))
                    {
                        //если нет, создаем ее и копируем в нее файл
                        try
                        {
                            System.IO.Directory.CreateDirectory(directory);
                            try
                            {
                                System.IO.File.Copy(curObjectInfo.Name, newFileName, true);
                                copiedFilesCount++;
                                //регистрация события
                                if (FileHasBeenCopied != null)
                                {
                                    FileHasBeenCopied(curObjectInfo);
                                }
                            }
                            catch
                            {
                                //MessageBox.Show(ex.Message);
                                //регистрация события
                                badFilesForCopying.Add(curObjectInfo.Name);
                                if (CopingErrorOccured != null)
                                {
                                    CopingErrorOccured();
                                }
                            }
                        }
                        catch
                        {
                            //MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        //иначе просто копируем туда файл
                        try
                        {
                            System.IO.File.Copy(curObjectInfo.Name, newFileName, true);
                            copiedFilesCount++;
                            
                            //регистрация события
                            if (FileHasBeenCopied != null)
                            {
                                FileHasBeenCopied(curObjectInfo);
                            }
                        }
                        catch
                        {
                            //MessageBox.Show(ex.Message);
                            badFilesForCopying.Add(curObjectInfo.Name);
                            
                            //регистрация события
                            if (CopingErrorOccured != null)
                            {
                                CopingErrorOccured();
                            }
                        }
                    }
                }
                if (FilesHaveBeenCopied != null)
                {
                    FilesHaveBeenCopied();
                }
                _isWorking = false;
            }

            private void DeleteMethod()
            {
                _isWorking = true;
                foreach (FilesSearcher.FoundObjectInfo curObjectInfo in result.foundObjectsList)
                {
                    try
                    {
                        System.IO.File.Delete(curObjectInfo.Name);
                        deletedFilesCount++;
                        //регистрация события
                        if (FileHasBeenDeleted != null)
                        {
                            FileHasBeenDeleted(curObjectInfo);
                        }
                    }
                    catch
                    {
                        //MessageBox.Show(ex.Message);
                        badFilesForDeleting.Add(curObjectInfo.Name);
                        //регистрация события
                        if (DeletingErrorOccured != null)
                        {
                            DeletingErrorOccured();
                        }
                    }
                }
                
                if (FilesHaveBeenDeleted != null)
                {
                    FilesHaveBeenDeleted();
                }
                _isWorking = false;
            }

            public void Find()
            {
                bgFind = new Thread(new ThreadStart(FindMethod));
                bgFind.IsBackground = true;
                bgFind.Start();
            }

            public void Copy()
            {
                bgCopy = new Thread(new ThreadStart(CopyMethod));
                bgCopy.IsBackground = true;
                bgCopy.Start();
            }

            public void Delete()
            {
                bgDelete = new Thread(new ThreadStart(DeleteMethod));
                bgDelete.IsBackground = true;
                bgDelete.Start();
            }

            public void StopFind()
            {
                bgFind.Abort();
                _isWorking = false;
            }

            public void StopCopy()
            {
                bgCopy.Abort();
                _isWorking = false;
            }

            public void StopDelete()
            {
                bgDelete.Abort();
                _isWorking = false;
            }

            //метод возвращает форматированную строку, выражая кол-во указанного объема информации в Gb, Mb или bytes
            //в зависимости от ее объема
            public static string FormattedSizeString(double filesSize)
            {
                string result = "";
                if ((float)filesSize / 1024 / 1024 / 1024 > 1)
                {
                    result = (Math.Round(((float)filesSize / 1024 / 1024 / 1024), 1)).ToString() + " Gb";
                }
                else
                {
                    if ((float)foundFilesSize / 1024 / 1024 > 1)
                    {
                        result = (Math.Round(((float)filesSize / 1024 / 1024), 1)).ToString() + " Mb";
                    }
                    else
                    {
                        if ((float)foundFilesSize / 1024 > 1)
                        {
                            result = (Math.Round(((float)filesSize / 1024), 1)).ToString() + " Kb";
                        }
                        else
                        {
                            result = (Math.Round((filesSize), 1)).ToString() + " bytes";
                        }
                    }
                }
                return result;
            }

            public BackgroundFinder()
            {
                result = new FilesSearcher.SearchResults();
                result.foundObjectsList = new List<FilesSearcher.FoundObjectInfo>();
                directoryToFindFrom = "";
                targetDirectory = "";
                fileMasks = new string[0];
                searchingText = new string[0]; 
                _isWorking = false;
                bgFind = new Thread(new ThreadStart(FindMethod));
                bgCopy = new Thread(new ThreadStart(CopyMethod));
                bgDelete = new Thread(new ThreadStart(DeleteMethod));
            }
            public BackgroundFinder(string dirToFindFrom, string dirToCopyTo)
            {
                result = new FilesSearcher.SearchResults();
                result.foundObjectsList = new List<FilesSearcher.FoundObjectInfo>();
                directoryToFindFrom = dirToFindFrom;
                targetDirectory = dirToCopyTo;
                fileMasks = new string[0];
                searchingText = new string[0]; 
                _isWorking = false;
                bgFind = new Thread(new ThreadStart(FindMethod));
                bgCopy = new Thread(new ThreadStart(CopyMethod));
                bgDelete = new Thread(new ThreadStart(DeleteMethod));
            }
            public BackgroundFinder(string dirToFindFrom, string dirToCopyTo, string[] masks)
            {
                result = new FilesSearcher.SearchResults();
                result.foundObjectsList = new List<FilesSearcher.FoundObjectInfo>();
                directoryToFindFrom = dirToFindFrom;
                targetDirectory = dirToCopyTo;
                fileMasks = masks;
                searchingText = new string[0]; 
                //searchHasBeenFinished = false;
                _isWorking = false;
                bgFind = new Thread(new ThreadStart(FindMethod));
                bgCopy = new Thread(new ThreadStart(CopyMethod));
                bgDelete = new Thread(new ThreadStart(DeleteMethod));
            }
            public BackgroundFinder(string dirToFindFrom, string dirToCopyTo, string[] masks, string[] searchingText)
            {
                result = new FilesSearcher.SearchResults();
                result.foundObjectsList = new List<FilesSearcher.FoundObjectInfo>();
                directoryToFindFrom = dirToFindFrom;
                targetDirectory = dirToCopyTo;
                fileMasks = masks;
                this.searchingText = searchingText;
                //searchHasBeenFinished = false;
                _isWorking = false;
                bgFind = new Thread(new ThreadStart(FindMethod));
                bgCopy = new Thread(new ThreadStart(CopyMethod));
                bgDelete = new Thread(new ThreadStart(DeleteMethod));
            }
        }

        static public List<string> badFilesForCopying = new List<string>();
        static public List<string> badFilesForDeleting = new List<string>();
        static public float foundFilesSize = 0;
        static public int foundFilesCount = 0;
        static public int copiedFilesCount = 0;
        static public int deletedFilesCount = 0;
        static public float copDelFilesSize = 0;
        static public bool searchTextInFiles = false;
        static public bool searchTextAtFilesBegining = false;
        public static BackgroundFinder bgFinder = new BackgroundFinder();

        public delegate void labelCallBack(FilesSearcher.FoundObjectInfo fileInfo);
        public void ShowNextFoundFile(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lcurFileName.InvokeRequired)
            {
                labelCallBack newCallBack = new labelCallBack(ShowNextFoundFile);
                this.Invoke(newCallBack, new object[] { fileInfo });
            }
            else
            {
                lcurFileName.Text = fileInfo.Name;
                foundFilesCount++;
            }
        }
        
        public void AddFoundFileToListBox(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lbResults.InvokeRequired)
            {
                labelCallBack lcb = new labelCallBack(AddFoundFileToListBox);
                this.Invoke(lcb, new object[] { fileInfo });
            }
            else
            {
                if ((lbResults.Items.Count > 0) && (lbResults.Items[lbResults.Items.Count - 1].ToString() != fileInfo.Name))
                {
                    lbResults.Items.Add(fileInfo.Name);
                    lbResults.SelectedIndex = lbResults.Items.Count - 1;
                }
                
                if (lbResults.Items.Count == 0)
                {
                    lbResults.Items.Add(fileInfo.Name);
                    lbResults.SelectedIndex = lbResults.Items.Count - 1;
                }
            }
        }
        
        public void ShowNextCopiedFile(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lcurFileName.InvokeRequired)
            {
                labelCallBack newCallBack = new labelCallBack(ShowNextCopiedFile);
                this.Invoke(newCallBack, new object[] { fileInfo });
            }
            else
            {
                lcurFileName.Text = fileInfo.Name;
            }
        }
        
        public void ShowNextDeletedFile(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lcurFileName.InvokeRequired)
            {
                labelCallBack newCallBack = new labelCallBack(ShowNextDeletedFile);
                this.Invoke(newCallBack, new object[] { fileInfo });
            }
            else
            {
                lcurFileName.Text = fileInfo.Name;
            }
        }

        //делегат и метод, его реализующий, необходимый для отображения в lcurFileName не только
        //текущего найденного файла, но и вообще всех просматриваемых файлов во время поиска
        public delegate void labelCallBack2(string browsingFileName);
        public void ShowCurrentBrowsingFile(string browsingFileName)
        {
            if (lcurFileName.InvokeRequired)
            {
                labelCallBack2 newCallBack = new labelCallBack2(ShowCurrentBrowsingFile);
                this.Invoke(newCallBack, new object[] { browsingFileName });
            }
            else
            {
                lcurFileName.Text = browsingFileName;
            }
        }
        
        //проценты выполнения
        public void CopingPercents(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lPercents.InvokeRequired)
            {
                labelCallBack lcb = new labelCallBack(CopingPercents);
                this.Invoke(lcb, new object[] { fileInfo });
            }
            else
            {
                float percents = 0;
                percents = (float)Math.Round((float)(copiedFilesCount + badFilesForCopying.Count) / foundFilesCount * 100, 2);
                lPercents.Text = copiedFilesCount.ToString() + " из " + foundFilesCount.ToString() + "   ( " + percents + "% )";
            }
        }
        
        public void DeletingPercents(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lPercents.InvokeRequired)
            {
                labelCallBack lcb = new labelCallBack(DeletingPercents);
                this.Invoke(lcb, new object[] { fileInfo });
            }
            else
            {
                float percents = 0;
                percents = (float)(deletedFilesCount + badFilesForDeleting.Count) / foundFilesCount * 100;
                lPercents.Text = copiedFilesCount.ToString() + " из " + foundFilesCount.ToString() + "   ( " + percents + "% )";
            }
        }
        
        //кол-во найденных файлов
        public void FoundFilesAmount(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lPercents.InvokeRequired)
            {
                labelCallBack lcb = new labelCallBack(FoundFilesAmount);
                this.Invoke(lcb, new object[] { fileInfo });
            }
            else
            {
                lPercents.Text = foundFilesCount.ToString();
            }
        }
        
        //размер скопированных и удаленных данных
        public void CopiedFilesSize(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lFilesSize.InvokeRequired)
            {
                labelCallBack lcb = new labelCallBack(CopiedFilesSize);
                this.Invoke(lcb, new object[] { fileInfo });
            }
            else
            {
                copDelFilesSize += fileInfo.Size;
                lFilesSize.Text = "( " + BackgroundFinder.FormattedSizeString(copDelFilesSize) + " of " + BackgroundFinder.FormattedSizeString(foundFilesSize) + " )";
            }
        }
        
        public void DeletedFilesSize(FilesSearcher.FoundObjectInfo fileInfo)
        {
            if (lFilesSize.InvokeRequired)
            {
                labelCallBack lcb = new labelCallBack(CopiedFilesSize);
                this.Invoke(lcb, new object[] { fileInfo });
            }
            else
            {
                copDelFilesSize += fileInfo.Size;
                lFilesSize.Text = "( " + BackgroundFinder.FormattedSizeString(copDelFilesSize) + " of " + BackgroundFinder.FormattedSizeString(foundFilesSize) + " )";
            }
        }

        public delegate void labelEmptyCallBack();
        public void FindingFinishedCaption()
        {
            if (lcurFileName.InvokeRequired)
            {
                labelEmptyCallBack lecb = new labelEmptyCallBack(FindingFinishedCaption);
                this.Invoke(lecb, new object[] { });
            }
            else
            {
                StopRotateTheCircle();
                lcurFileName.Text = "Поиск завершен";
                MessageBox.Show("Поиск завершен");
            }
        }
        
        public void FindingFinishedSizeCaption()
        {
            if (lFilesSize.InvokeRequired)
            {
                labelEmptyCallBack lecb = new labelEmptyCallBack(FindingFinishedSizeCaption);
                this.Invoke(lecb, new object[] { });
            }
            else
            {
                lFilesSize.Text = "( " + BackgroundFinder.FormattedSizeString(foundFilesSize) + " )";
            }
        }
        public void FindingFinishedFillListBox()
        {
            if (lbResults.InvokeRequired)
            {
                labelEmptyCallBack lecb = new labelEmptyCallBack(FindingFinishedFillListBox);
                this.Invoke(lecb, new object[] {});
            }
            else
            {
                foreach (FilesSearcher.FoundObjectInfo curFoundObjectInfo in bgFinder.result.foundObjectsList)
                {
                    lbResults.Items.Add(curFoundObjectInfo.Name);
                }
            }
        }
        public void CopingFinishedCaption()
        {
            if (lcurFileName.InvokeRequired)
            {
                labelEmptyCallBack lecb = new labelEmptyCallBack(CopingFinishedCaption);
                this.Invoke(lecb, new object[] { });
            }
            else
            {
                StopRotateTheCircle();
                lcurFileName.Text = "Копирование завершено";
                MessageBox.Show("Копирование завершено");
            }
        }
        public void DeletingFinishedCaption()
        {
            if (lcurFileName.InvokeRequired)
            {
                labelEmptyCallBack lecb = new labelEmptyCallBack(DeletingFinishedCaption);
                this.Invoke(lecb, new object[] { });
            }
            else
            {
                StopRotateTheCircle();
                lcurFileName.Text = "Удаление завершено";
                MessageBox.Show("Удаление завершено");
            }
        }
        
        //отображение ошибок копирования и удаления
        public void ShowCopingErrorsAmount()
        {
            if (lCopingErrorsAmount.InvokeRequired)
            {
                labelEmptyCallBack cb = new labelEmptyCallBack(ShowCopingErrorsAmount);
                this.Invoke(cb, new object[] { });
            }
            else
            {
                lCopingErrorsAmount.Text = badFilesForCopying.Count.ToString();
            }
        }
        
        public void ShowDeletingErrorsAmount()
        {
            if (lDeletingErrorsAmount.InvokeRequired)
            {
                labelEmptyCallBack cb = new labelEmptyCallBack(ShowDeletingErrorsAmount);
                this.Invoke(cb, new object[] { });
            }
            else
            {
                lDeletingErrorsAmount.Text = badFilesForDeleting.Count.ToString();
            }
        }

        //при первом нажатии на chbSearchWithText, принимает значение true,
        //чтобы в дальнейшем не докучать
        bool messageHasBeenShown = false;
        bool messageHasBeenShown2 = false;

        int fileNameNumber = 1;
        private void StartRotateTheCircle()
        {
            timer1.Start();
            pbCircle.Visible = true;
        }
        private void StopRotateTheCircle()
        {
            timer1.Stop();
            pbCircle.Visible = false;
        }
        
        private void bFind_Click(object sender, EventArgs e)
        {
            if (!bgFinder.isWorking)
            {
                //обнуление всего, что есть
                badFilesForCopying.Clear();
                badFilesForDeleting.Clear();
                lbResults.Items.Clear();
                foundFilesCount = 0;
                copiedFilesCount = 0;
                deletedFilesCount = 0;
                foundFilesSize = 0;
                lcurFileName.Text = "";
                lPercents.Text = "";
                lCopingErrorsAmount.Text = "";
                lDeletingErrorsAmount.Text = "";
                lFilesSize.Text = "";

                //проверка, какой поиск выполняется: с поиском текста или без него
                if (!chbSearchWithText.Checked)
                {
                    if (tbRootFolder.Text != "" /*&& tbFolder.Text != "" */ && tbFileMask.Text != "")
                    {
                        string[] masks;
                        string pathToCopyFrom;
                        string pathToCopyTo;

                        pathToCopyFrom = tbRootFolder.Text;
                        if (pathToCopyFrom[pathToCopyFrom.Length - 1] != '\\')
                        {
                            pathToCopyFrom = pathToCopyFrom + "\\";
                        }
                        pathToCopyTo = tbFolder.Text;
                        if (pathToCopyTo[pathToCopyTo.Length - 1] != '\\')
                        {
                            pathToCopyTo = pathToCopyTo + "\\";
                        }

                        if (tbFileMask.Text.IndexOf(';') != -1)
                        {
                            masks = tbFileMask.Text.Split(';');
                        }
                        else
                        {
                            masks = new string[] { tbFileMask.Text };

                        }
                        bgFinder = new BackgroundFinder(pathToCopyFrom, pathToCopyTo, masks);
                        StartRotateTheCircle();
                        bgFinder.Find();
                    }
                    else
                    {
                        MessageBox.Show("Убедитесь, что указана директория для поиска, директория для создания копии и маска поиска файлов");
                    }
                }
                else
                {
                    if (tbRootFolder.Text != "" && /*tbFolder.Text != "" &&*/ tbFileMask.Text != "" && tbSearchingText.Text != "")
                    {
                        lcurFileName.Text = "Начат поиск файлов";

                        string[] masks;
                        string[] searchingText;
                        string pathToCopyFrom;
                        string pathToCopyTo;

                        pathToCopyFrom = tbRootFolder.Text;
                        if (pathToCopyFrom[pathToCopyFrom.Length - 1] != '\\')
                        {
                            pathToCopyFrom = pathToCopyFrom + "\\";
                        }
                        pathToCopyTo = tbFolder.Text;
                        if (pathToCopyTo[pathToCopyTo.Length - 1] != '\\')
                        {
                            pathToCopyTo = pathToCopyTo + "\\";
                        }

                        if (tbFileMask.Text.IndexOf(';') != -1)
                        {
                            masks = tbFileMask.Text.Split(';');
                        }
                        else
                        {
                            masks = new string[] { tbFileMask.Text };

                        }
                        if (tbSearchingText.Text.IndexOf(';') != -1)
                        {
                            searchingText = tbSearchingText.Text.Split(';');
                        }
                        else
                        {
                            searchingText = new string[] { tbSearchingText.Text };
                        }
                        bgFinder = new BackgroundFinder(pathToCopyFrom, pathToCopyTo, masks, searchingText);
                        StartRotateTheCircle();
                        bgFinder.Find();
                    }
                    else
                    {
                        MessageBox.Show("Убедитесь, что указана директория для поиска, директория для создания копии, маска поиска файлов и искомый текст");
                    }
                }
            }
            else
            {
                MessageBox.Show("Дождитесь завершения предыдущего процесса");
            }
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            if (!bgFinder.isWorking)
            {
                try
                {
                    if (bgFinder.result.foundObjectsList.Count > 0)
                    {
                        if (tbFolder.Text.Length > 0)
                        {
                            bgFinder.targetDirectory = tbFolder.Text;
                            if (bgFinder.targetDirectory[bgFinder.targetDirectory.Length - 1] != '\\')
                            {
                                bgFinder.targetDirectory = bgFinder.targetDirectory + "\\";
                            }
                            //обнуление переменной
                            copDelFilesSize = 0;
                            //запуск копирования
                            StartRotateTheCircle();
                            bgFinder.Copy();
                        }
                        else
                        {
                            MessageBox.Show("Укажите директорию для создания копии");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Массив найденных файлов пуст.\nКопирование невозможно");
                    }
                }
                catch
                {
                    MessageBox.Show("Поиск был завершен некорректно. Выполните поиск повторно");
                }
            }
            else
            {
                MessageBox.Show("Дождитесь завершения предыдущего процесса");
            }
        }
        
        private void bSave_Click(object sender, EventArgs e)
        {

        }
        
        private void bLoad_Click(object sender, EventArgs e)
        {

        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (!bgFinder.isWorking)
            {
                try
                {
                    if (bgFinder.result.foundObjectsList.Count > 0)
                    {
                        //обнуление переменной
                        copDelFilesSize = 0;
                        //запуск удаления
                        StartRotateTheCircle();
                        bgFinder.Delete();
                    }
                    else
                    {
                        MessageBox.Show("Список найденных файлов пуст. Удаление невозможно");
                    }
                }
                catch
                {
                    MessageBox.Show("Поиск был завершен некорректно. Выполните поиск повторно");
                }
            }
            else
            {
                MessageBox.Show("Дождитесь завершения предыдущего процесса");
            }
        }

        private void bShowInfo_Click(object sender, EventArgs e)
        {
            if (!bgFinder.isWorking)
            {
                fInfo infoForm = new fInfo();

                ListViewItem newLVI = new ListViewItem();
                newLVI.Text = "Файлов найдено";
                newLVI.SubItems.Add(foundFilesCount.ToString());
                infoForm.lvInfo.Items.Add(newLVI);

                newLVI = new ListViewItem();
                newLVI.Text = "Файлов скопировано";
                newLVI.SubItems.Add(copiedFilesCount.ToString());
                infoForm.lvInfo.Items.Add(newLVI);

                newLVI = new ListViewItem();
                newLVI.Text = "Ошибок копирования";
                newLVI.SubItems.Add(badFilesForCopying.Count.ToString());
                infoForm.lvInfo.Items.Add(newLVI);

                newLVI = new ListViewItem();
                newLVI.Text = "Файлов удалено";
                newLVI.SubItems.Add(deletedFilesCount.ToString());
                infoForm.lvInfo.Items.Add(newLVI);

                newLVI = new ListViewItem();
                newLVI.Text = "Ошибок удаления";
                newLVI.SubItems.Add(badFilesForDeleting.Count.ToString());
                infoForm.lvInfo.Items.Add(newLVI);

                infoForm.Show();
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            if (bgFinder.isWorking)
            {
                bgFinder.StopCopy();
                bgFinder.StopFind();
                bgFinder.StopDelete();
                StopRotateTheCircle();
                lcurFileName.Text = "Процесс остановлен";
                MessageBox.Show("Процесс остановлен");
            }
            else
            {
                MessageBox.Show("Нет активных процессов");
            }
        }

        private void lCopingErrorsAmount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!bgFinder.isWorking)
            {
                fInfo badFilesInfo = new fInfo();
                badFilesInfo.lvInfo.Clear();
                badFilesInfo.lvInfo.Columns.Add("Имя файла");
                badFilesInfo.lvInfo.Columns[0].Width = 790;
                foreach (string curBadFile in MainWindow.badFilesForCopying)
                {
                    ListViewItem newLVI = new ListViewItem();
                    newLVI.Text = curBadFile;
                    //newLVI.SubItems.Add("ошибка копирования");
                    badFilesInfo.lvInfo.Items.Add(newLVI);
                }
                badFilesInfo.Show();
            }
            else
            {
                MessageBox.Show("Дождитесь окончания активного процесса");
            }
        }

        private void lDeletingErrorsAmount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!bgFinder.isWorking)
            {
                fInfo badFilesInfo = new fInfo();
                badFilesInfo.lvInfo.Clear();
                badFilesInfo.lvInfo.Columns.Add("Имя файла");
                badFilesInfo.lvInfo.Columns[0].Width = 790;
                foreach (string curBadFile in MainWindow.badFilesForDeleting)
                {
                    ListViewItem newLVI = new ListViewItem();
                    newLVI.Text = curBadFile;
                    newLVI.SubItems.Add("ошибка удаления");
                    badFilesInfo.lvInfo.Items.Add(newLVI);
                }
                badFilesInfo.Show();
            }
            else
            {
                MessageBox.Show("Дождитесь окончания активного процесса");
            }
        }

        private void tbFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void chSearchWithText_CheckedChanged(object sender, EventArgs e)
        {
            tbSearchingText.Enabled = !tbSearchingText.Enabled;
            searchTextInFiles = chbSearchWithText.Checked;
            if (chbSearchWithText.Checked && !messageHasBeenShown)
            {
                MessageBox.Show("Функция поиска текста в файлах на данный момент реализована для следующих типов файлов:\ntxt\nini\ninf\ncfg\nconf\nconfig\nurl\nlog\nxml\nrtf\ndoc\ndocx\n\n" +
                    "ВНИМАНИЕ!\nЕсли не выбрана опция поиска текста только в начале файлов, Файлы форматов doc и docx, содержащие свыше 5000 слов, не обрабатываются, ввиду огромной длительности выполнения данной операции\n" + 
                    "(файл, объемом 2,3 Мб и содержащий 107000 слов обрабатывался 2ч 15мин), иначе поиск текста будет осуществляться во всех файлах, но только в первых 100 параграфах.");
                messageHasBeenShown = true;
            }
            if (chbSearchWithText.Checked)
            {
                chbSearchAtFilesBegining.Visible = true;
            }
            else
            {
                chbSearchAtFilesBegining.Visible = false;
            }
        }

        private void chbSearchAtFilesBegining_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSearchAtFilesBegining.Checked && !messageHasBeenShown2)
            {
                MessageBox.Show("Поиск будет осуществляться по всем файлам, но только в первых 100 параграфах каждого файла");
                messageHasBeenShown2 = true;
            }
            if (chbSearchAtFilesBegining.Checked)
            {
                searchTextAtFilesBegining = true;
            }
            else
            {
                searchTextAtFilesBegining = false;
            }
        }

        private void lbResults_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbResults.SelectedIndex != -1)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(lbResults.SelectedItem.ToString());
                lbResults.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openFileFolder_Click(object sender, EventArgs e)
        {
            string path = lbResults.SelectedItem.ToString().Substring(0,lbResults.SelectedItem.ToString().LastIndexOf('\\'));
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fileNameNumber == 37)
            {
                fileNameNumber = 1;
            }
            Bitmap curImage = (Bitmap)FileTransfer.Properties.Resources.ResourceManager.GetObject("circle" + fileNameNumber.ToString());
            pbCircle.Image = curImage;
            fileNameNumber++;
            
        }

        private void lPercents_TextChanged(object sender, EventArgs e)
        {
            lFilesSize.Location = new Point(lPercents.Location.X + lPercents.Width + 20, lFilesSize.Location.Y);
        }
    }
}
