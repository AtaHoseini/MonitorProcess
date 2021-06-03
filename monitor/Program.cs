using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

/// <summary>
/// AtaHoseini 
/// ata.hoseini@gmail.com
/// </summary>

namespace monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckInputArgs(args);

            string processName = args[0];
            int maxLifeTime = Convert.ToInt32(args[1]);
            int monitoringFreq = Convert.ToInt32(args[2]);

            StartMonitoring(processName, maxLifeTime, monitoringFreq);
        }


        /// <summary>
        /// start the monitoring process in a this function and handle the termination
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="maxLifeTime"></param>
        /// <param name="monitoringFreq"></param>
        private static void StartMonitoring(string processName, int maxLifeTime, int monitoringFreq)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("> Monitoring started, Press Ctrl+C to stop");
            Console.ResetColor();
            do
            {
                while (!Console.KeyAvailable)
                {
                    TryToKillProcess(processName, maxLifeTime, monitoringFreq);
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void TryToKillProcess(string processName, int maxLifeTime, int monitoringFreq)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            var killProcessService = new KillProcessService();

            if (processes.Length > 0)
            {
                foreach (var process in processes)
                {
                    if (killProcessService.Execute(maxLifeTime, process))
                        LogService.log(process);
                }
                //processes.ToList().ForEach(process => killProcessService.Execute(maxLifeTime, process));
            }
            else
            {
                Console.WriteLine(string.Format("{0} - Checked and no process with th name '{1}' is found...", DateTime.Now, processName));
            }
            Thread.Sleep(monitoringFreq *60* 1000);
        }


        /// <summary>
        /// Logs the events in a file
        /// </summary>
        /// <param name="msg"></param>

        private static void Log(string msg)
        {
            File.AppendAllText("monitor.log", msg + Environment.NewLine);
        }


        /// <summary>
        /// Checks the input args parameters in order to get valid ones
        /// </summary>
        /// <param name="inputs">the args parameters of main function</param>
        private static void CheckInputArgs(string[] inputs)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (inputs.Length < 3)
            {
                Console.WriteLine("Error: the input params are not correct - check length");
                ShowHelp();
                Environment.Exit(0);
            }

            if (!int.TryParse(inputs[1], out _))
            {
                Console.WriteLine("Error: max_life_time must be number - check max life time in number");
                ShowHelp();
                Environment.Exit(0);
            }

            if (!int.TryParse(inputs[2], out _))
            {
                Console.WriteLine("Error: monitoring_frequesncy must be number - check monitoring freq in number");
                ShowHelp();
                Environment.Exit(0);
            }
            Console.ResetColor();
        }


        /// <summary>
        /// just show how to run program
        /// </summary>
        private static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("\nThe Correct format to run the program is:\n" +
                "monitor.exe process_name max_life_time monitoring_frequesncy\n" +
                "max_life_time is number and in minutes\n" +
                "monitoring_frequesncy is number and in minutes\n" +
                "Please concider that monitoring_frequesncy must be less than max_life_time"
                );
            Console.ResetColor();
        }
    }

}
