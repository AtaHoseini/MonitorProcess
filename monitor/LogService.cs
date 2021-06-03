using System;
using System.Diagnostics;
using System.IO;

namespace monitor
{
    public static class LogService
    {
        public static void log(Process process)
        {
            TimeSpan execution_time = DateTime.Now.Subtract(process.StartTime);
            string msg = string.Format("{0} - Process {1} With PID {2} has been terminated, lifetime {3} mins",
                   DateTime.Now, process.ProcessName, process.Id, execution_time.TotalMinutes);
            File.AppendAllText("monitor.log", msg + Environment.NewLine);
        }

    }
}
