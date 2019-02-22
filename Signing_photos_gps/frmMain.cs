using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.FolderSelect;
using Signing_photos_gps;
using System.IO.IsolatedStorage;
using System.IO;
using System.Threading;
using Microsoft.WindowsAPICodePack.Taskbar;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;

namespace Signing_photos_gps
{
    public partial class frmMain : Form
    {
        //1 Перезапись GPS
        //2 Формирование KML
        //3 Новый движек
        //4 Исключения основного потока
        
        //Global
        static BackgroundWorker bw=null;
        string settingtxtPathFolder=null, settingtxtPathGpsFile=null;
        //
        TaskbarManager prog = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var fsd = new FolderSelectDialog();
            fsd.Title = "Выберите папку с фотографиями *.jpg";
            if (settingtxtPathFolder != null)
            {
                fsd.InitialDirectory = settingtxtPathFolder;
            }
            else
            {
                fsd.InitialDirectory = @"c:\";
            }
            //
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                txtPathFolder.Text=fsd.FileName;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //Training
            //https://msdn.microsoft.com/en-us/windows7trainingcourse_win7taskbarmanaged_topic2#_Toc236828995
            //http://rsdn.org/article/dotnet/CSThreading2.xml
            //http://qaru.site/questions/158908/how-to-use-a-backgroundworker
            //проверка состояния, остановить если работает
            //Check if background worker is doing anything and send a cancellation if it is
            if(bw!=null)
            {
                if (bw.IsBusy)
                {
                    bw.CancelAsync();
                    return;
                }
            }
            bw = new BackgroundWorker();
            //Получение параметров
            varforbw varforbw1 = new varforbw();
            varforbw1.pathImages = txtPathFolder.Text;
            varforbw1.pathFileGPS = txtPathGpsFile.Text;
            varforbw1.overwriteGPSInfo = checkReplaceGPS.Checked;
            varforbw1.addOrRemoveTime = radioBtnAddTime.Checked;
   
            varforbw1.addTime = new System.TimeSpan(Convert.ToInt32(numericHour.Value), Convert.ToInt32(numericMin.Value),0);
            //Запуск фона
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            //
            prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
            btnRun.Text = "Остановить";
            //
            //cls
            richtbLog.Clear();
            richtbLog.AppendText("====================" + "\n");
            richtbLog.AppendText("Запуск процесса" + "\n");
            richtbLog.AppendText(DateTime.Now.ToString() + "\n");
            richtbLog.AppendText("====================" + "\n");
            //Запуск фонового процесса
            bw.RunWorkerAsync(varforbw1);
        }

        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw_local = sender as BackgroundWorker;
            varforbw varforbw1 = (varforbw)e.Argument;
            string appendText;
            //
            /*try
            {*/
            //Получение списка файлов 
            List<string> filesjpg = Directory.GetFiles(varforbw1.pathImages, "*.jpg").ToList<string>();
            //Проверка наличия файлов
            if(filesjpg.Count<1)
            {
                    throw new Exception("Нет файлов для обработки");                    
            }
            //Фото сервис
            PhotoService ps = new PhotoService();
            //Передача параметров работы
            ps.Params.addOrRemoveTime = varforbw1.addOrRemoveTime;
            ps.Params.addTime = varforbw1.addTime; // n
            ps.Params.overwriteGPSInfo = varforbw1.overwriteGPSInfo; // n
            ps.Params.pathFileGPS = varforbw1.pathFileGPS;
            //Парсинг файла GPS
            ps.ValidateFileGPS();
            //Расчет одного процента для прогресса
            float one_percent;
            one_percent = filesjpg.Count / (float)100;
            int percent = 0;
            //обработка
            int i = 0;
            string rez;
            //
            appendText = "Получен список файлов. Всего: "+filesjpg.Count.ToString()+" файла";
            bw_local.ReportProgress(percent, appendText);
            //
            var listPointsWithPhoto = new List<KeyValuePair<string, PointGPS>>();
            PointGPS point_local;
            //
            foreach (var s in filesjpg)
            {
                appendText = "Обработка фотографии: " + new FileInfo(s).Name;
                bw_local.ReportProgress(percent, appendText);
                //Обработка фото
                point_local = null;
                rez =ps.WriteGPSinImage_PresentationCore(s,out point_local);
                if(rez=="ok")
                {
                    listPointsWithPhoto.Add(new KeyValuePair<string, PointGPS>(s, point_local));
                    //
                    appendText = "Фотография успешно обработана";
                }else
                {
                    appendText = rez;
                }
                //Процент выполнения
                i++;
                percent = Convert.ToInt32(i / one_percent);
                //Разделитель
                appendText = appendText+"\n--------------------";
                //Прогресс Вывод результата
                bw_local.ReportProgress(percent, appendText);
                //Check if there is a request to cancel the process
                if (bw_local.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            //Формирование файла KML по фотографиям
            //appendText =  "\nФормирование файла KML по фотографиям";
            //bw_local.ReportProgress(percent, appendText);
            //

            //ps.CreateKML(listPointsWithPhoto);

            //
            bw_local.ReportProgress(100);
            /*}
            catch (Exception ex)
            {
                appendText = "ОШИБКА: "+ ex.Message;
                bw_local.ReportProgress(100, appendText);
                e.Cancel = true;
            }*/
    
        }
        private void bw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
                prog.SetProgressValue(e.ProgressPercentage, 100);
                if(e.UserState!=null)
                  {
                     richtbLog.AppendText(e.UserState.ToString() + "\n");
                  }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            richtbLog.AppendText("====================" + "\n");
            richtbLog.AppendText(DateTime.Now.ToString() + "\n");
            //
            if (e.Cancelled == true)
            {
                prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Paused);
                richtbLog.AppendText("Процесс приостановлен" + "\n");
            }
            else if (e.Error != null)
            {
                prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Error);
                richtbLog.AppendText("Ошибка при обработки фотографий" + "\n");
            }
            else
            {
                prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
                richtbLog.AppendText("Процесс успешно завершен" + "\n");
            }
            richtbLog.AppendText("====================" + "\n");
            //
            btnRun.Text = "Запустить";
        }
        private void btnSelectFileGps_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogGPS = new OpenFileDialog();
            openFileDialogGPS.Filter = "Файл KML(*.kmz)|*.kmz|Файл KML(*.kml)|*.kml|All files(*.*)|*.*";
            openFileDialogGPS.Multiselect = false;
            openFileDialogGPS.Title = "Выберите файл с координатами kml";
            //
            if (settingtxtPathGpsFile != null)
            {
                openFileDialogGPS.InitialDirectory = settingtxtPathGpsFile;
            }
            else
            {
                openFileDialogGPS.InitialDirectory = @"c:\";
            }
            //
            if (openFileDialogGPS.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            txtPathGpsFile.Text = openFileDialogGPS.FileName;
        }

        private void txtPathFolder_TextChanged(object sender, EventArgs e)
        {
            if((txtPathFolder.Text.Length>0)&&(txtPathGpsFile.Text.Length > 0))
                {
                    btnRun.Enabled = true;
                }else
                    btnRun.Enabled  = false;
                 {
            }
        }

        private void txtPathGpsFile_TextChanged(object sender, EventArgs e)
        {
            if ((txtPathFolder.Text != "") || (txtPathGpsFile.Text != ""))
            {
                btnRun.Enabled = true;
            }
            else
                btnRun.Enabled = false;
            {
            }
        }
        private void WriteUserData()
        {
            if ((txtPathFolder.Text.Length > 0) && (txtPathGpsFile.Text.Length > 0))
            {
                // create an isolated storage stream...
                IsolatedStorageFileStream userDataFile =
                  new IsolatedStorageFileStream("UserData1.dat", FileMode.Create);
                // create a writer to the stream...
                StreamWriter writeStream = new StreamWriter(userDataFile);
                // write strings to the Isolated Storage file...
                writeStream.WriteLine(txtPathFolder.Text);
                writeStream.WriteLine(new FileInfo(txtPathGpsFile.Text).Directory);
                
            // Tidy up by flushing the stream buffer and then closing
            // the streams...
                writeStream.Flush();
                writeStream.Close();
                userDataFile.Close();
            }
        }
        private void ReadUserData()
        {
            // create an isolated storage stream...
            IsolatedStorageFileStream userDataFile =
                  new IsolatedStorageFileStream("UserData1.dat", FileMode.OpenOrCreate);
                // create a writer to the stream...
                StreamReader readStream = new StreamReader(userDataFile);
            // write strings to the Isolated Storage file...
            settingtxtPathFolder= readStream.ReadLine();
            settingtxtPathGpsFile =readStream.ReadLine();
            // Tidy up by flushing the stream buffer and then closing
            // the streams...
                readStream.Close();
                userDataFile.Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteUserData();
        }

        private void richtbLog_TextChanged(object sender, EventArgs e)
        {
            RichTextBox rtx = (RichTextBox)sender;
            // set the current caret position to the end
            rtx.SelectionStart = rtx.Text.Length;
            // scroll it automatically
            rtx.ScrollToCaret();
        }    

        private void frmMain_Load(object sender, EventArgs e)
        {
            //read config
            ReadUserData();
        }
    }
}
