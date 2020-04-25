using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dcs_server_monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DCS dCSObj;
        private int refreshRate;
        private Timer rTimer;
        private System.Windows.Threading.DispatcherTimer dTimer;
        private readonly string WeatherBatPath = @"C:\Users\Office PC\Saved Games\DCS.openbeta_server\Missions\weather.bat";
        private readonly int RestartTime = 28800;
        private int tick = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        internal DCS DCSObj { get => dCSObj; set => dCSObj = value; }
        internal int RefreshRate { get => refreshRate; set => refreshRate = value; }
        internal Timer RTimer { get => rTimer; set => rTimer = value; }
        internal System.Windows.Threading.DispatcherTimer DTimer { get => dTimer; set => dTimer = value; }
        internal int Tick { get => tick; set => tick = value; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(@"C:\Users\Office PC\Saved Games\DCS.openbeta_server\Missions");
            DCSObj = StartServer();
            DTimer = new System.Windows.Threading.DispatcherTimer();
            DTimer.Tick += new EventHandler(DTimer_TickEH);
            DTimer.Interval = new TimeSpan(10000000);
            DTimer.Start();
            RTimer = new Timer(Timer_Tick, new AutoResetEvent(false), 1000, 1000);
            Console.Write("Main window loaded\n");
            Console.Write($"{DTimer}\n");
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(DCSObj.dcsProc.HasExited)) { DCSObj.dcsProc.Kill(); }
                Close();
            }
            catch (Exception ex)
            {
                Console.Write($"{ex}");
                Close();
            }
        }

        private void DTimer_TickEH(object sender, EventArgs e)
        {
            DCSObj.RefreshProcess();
            if (DCSObj.CheckProcess())
            {
                InfoBox.Items.Clear();
                InfoBox.Items.Add($"Is running: {DCSObj.dcsProc.Responding}");
                InfoBox.Items.Add($"Physical memory usage: {DCSObj.dcsProc.WorkingSet64 / 1024}K");
                InfoBox.Items.Add($"Base priority: {DCSObj.dcsProc.BasePriority}");
                InfoBox.Items.Add($"User processor time: {DCSObj.dcsProc.UserProcessorTime}");
                InfoBox.Items.Add($"Privileged processor time: {DCSObj.dcsProc.PrivilegedProcessorTime}");
                InfoBox.Items.Add($"Total procesor time: {DCSObj.dcsProc.TotalProcessorTime}");
                InfoBox.Items.Add($"Paged system memory size: {DCSObj.dcsProc.PagedSystemMemorySize64 / 1024}K");
                InfoBox.Items.Add($"Paged memory size: {DCSObj.dcsProc.PagedMemorySize64 / 1024}K");
            }
            else
            {
                InfoBox.Items.Clear();
                InfoBox.Items.Add($"Process is non-responsive... {DCSObj.MaxDeadTime - DCSObj.DeadTime}");
                if (DCSObj.AreWeDead)
                {
                    Tick = RestartTime;
                }
            }

            TimeSpan timeRemaining = TimeSpan.FromSeconds(RestartTime - Tick);
            string timeRemainingStr = timeRemaining.ToString(@"hh\:mm\:ss");
            TextBox_TimeRemaining.Text = timeRemainingStr;
            ProgressBar_Restart.Value = (double)Tick / RestartTime;
        }

        private void TextBox_RefreshRate_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //Enforce digital input only
            string t = TextBox_RefreshRate.Text;
            foreach (char c in t)
            {
                if (!(c >= '0' && c <= '9'))
                {
                    TextBox_RefreshRate.Text = "1000";
                }
            }
            if (t.Length > 4) { TextBox_RefreshRate.Text = "9999"; }
            if (!(DTimer == null)) { DTimer.Interval = new TimeSpan(10000 * Convert.ToInt32(TextBox_RefreshRate.Text)); }
        }

        private void InfoBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            //nothing yet
        }

        private DCS StartServer()
        {
            StopServer();
            return new DCS("DCS.exe", "--server --norender", WeatherBatPath);
        }
        private void StopServer()
        {
            if (!(DCSObj == null))
            {
                if (!DCSObj.dcsProc.HasExited)
                {
                    DCSObj.ShutdownProcess();
                }
            }
        }

        private void Button_KillServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBox_KillSafety.IsChecked.Value)
            {
                StopServer();
                CheckBox_KillSafety.IsChecked = false;
            }
        }

        private void Button_RestartServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBox_KillSafety.IsChecked.Value)
            {
                DCSObj = StartServer();
                CheckBox_KillSafety.IsChecked = false;
            }
        }

        private void Timer_Tick(object stateInfo)
        {
            Tick++;
            if (Tick > RestartTime)
            {
                DCSObj = StartServer();
                Tick = 0;
            }
        }
    }
}
