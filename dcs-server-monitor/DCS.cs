using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace dcs_server_monitor
{
    class DCS
    {
        public ProcessStartInfo dcsInfo;
        public Process dcsProc;
        private bool areWeDead = false;
        public readonly int MaxDeadTime = 60;
        private int deadTime = 0;
        private Timer deadTimer;

        public Timer DeadTimer { get => deadTimer; private set => deadTimer = value; }
        public bool AreWeDead { get => areWeDead; private set => areWeDead = value; }
        public int DeadTime { get => deadTime; private set => deadTime = value; }

        public DCS(string FileName, string Args, string BatPath)
        {
            ProcessStartInfo BatInfo = new ProcessStartInfo(BatPath);
            BatInfo.CreateNoWindow = false;
            BatInfo.UseShellExecute = false;
            Process BatProc = Process.Start(BatInfo);
            BatProc.WaitForExit();
            
            dcsInfo = new ProcessStartInfo();
            dcsInfo.FileName = FileName;
            dcsInfo.UseShellExecute = true;
            dcsInfo.CreateNoWindow = false;
            dcsInfo.WindowStyle = ProcessWindowStyle.Normal;
            dcsInfo.Arguments = Args;
            dcsProc = Process.Start(dcsInfo);

            DeadTimer = new Timer(DeadTimer_Tick, new AutoResetEvent(false), 1000, 1000);
        }

        public bool CheckProcess()
        {
            if (dcsProc.HasExited)
            {
                return false;
            }
            if (dcsProc.Responding)
            {
                return true;
            }
            return false;
        }

        public void DeadTimer_Tick(object State)
        {
            if (!dcsProc.HasExited)
            {
                if (!dcsProc.Responding)
                {
                    DeadTime++;
                    Console.WriteLine($"Dead tick: {MaxDeadTime - DeadTime}");
                    if (DeadTime > MaxDeadTime) { AreWeDead = true; }
                }
                else
                {
                    DeadTime = 0;
                }
            }
        }

        public void RefreshProcess()
        {
            dcsProc.Refresh();
        }

        public bool ShutdownProcess()
        {
            dcsProc.Kill();
            return true;
        }
    }
}
