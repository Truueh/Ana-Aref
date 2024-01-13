using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnaAref
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string killerName = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            // Thread to constantly update the time of the song currently playing
            Thread mainThread = new Thread(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(MainLoop);
                    Thread.Sleep(500);
                }
            });
            mainThread.Start();
        }

        private void MainLoop()
        {
            // variables
            string path = GetLogsPath(); // save path to logs file
            string line = "";
            string lastMatch = "";

            // file streaming variables
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BufferedStream bs = new BufferedStream(fs);
            StreamReader sr = new StreamReader(bs);

            // read every line of the file
            while ((line = sr.ReadLine()) != null)
            {
                lastMatch = FindKiller(line, lastMatch);
            }

            killerName = KillerNameTranslation(lastMatch); // update killer name variable
            killerLabel.Content = "Killer: " + killerName; // update killer label


            // close the file streaming
            sr.Close();
            bs.Close();
            fs.Close();
        }

        private string FindKiller(string line, string lastMatch)
        {
            // if line contains key word
            if (line.Contains("BP_Menu_Slasher"))
            {
                return line.Substring(line.IndexOf('r') + 1, 2); // return killer ID (eg: 01, 02, etc..)
            }

            return lastMatch;
        }

        private string GetLogsPath()
        {
            return Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + "/Local/DeadByDaylight/Saved/Logs/DeadByDaylight.log";
        }

        private string KillerNameTranslation(string ID)
        {
            string translatedName = "";
            switch (ID)
            {
                case "01":
                    translatedName = "Trapper";
                    break;
                case "02":
                    translatedName = "Wraith";
                    break;
                case "03":
                    translatedName = "Billy";
                    break;
                case "04":
                    translatedName = "Nurse";
                    break;
                case "05":
                    translatedName = "Hag";
                    break;
                case "06":
                    translatedName = "Myers";
                    break;
                case "07":
                    translatedName = "Doctor";
                    break;
                case "08":
                    translatedName = "Huntress";
                    break;
                case "09":
                    translatedName = "LeatherFace";
                    break;
                case "10":
                    translatedName = "Freddy";
                    break;
                case "11":
                    translatedName = "Pig";
                    break;
                case "12":
                    translatedName = "Clown";
                    break;
                case "13":
                    translatedName = "Spirit";
                    break;
                case "14":
                    translatedName = "Legion";
                    break;
                case "15":
                    translatedName = "Plague";
                    break;
                case "16":
                    translatedName = "GhostFace";
                    break;
                case "17":
                    translatedName = "DemoPLS";
                    break;
                case "18":
                    translatedName = "Oni";
                    break;
                case "19":
                    translatedName = "DeathSlinger";
                    break;
                default:
                    translatedName = "Null";
                    break;
            }
            return translatedName;
        }
    }
}