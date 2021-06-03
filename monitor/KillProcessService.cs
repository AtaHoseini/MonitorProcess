using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace monitor
{
    public class KillProcessService
    {
        public bool Execute(int maxLifeTime, Process process)
        {
            TimeSpan execution_time = DateTime.Now.Subtract(process.StartTime);
            if (execution_time.TotalMinutes >= maxLifeTime)
            {
                process.Kill();
                return true;
            }
            else
                return false;
        }

        //public void SetTasks(int interval, int Lifetime, Process process)
        //{
        //    var taskList = new List<Task>();
        //    taskList.Add(
        //           Task.Factory.StartNew(() =>
        //           {
        //               int chekloop = Lifetime / interval;
        //               int lastcheckloop = Lifetime % interval;
        //               var now = DateTime.Now;
        //               Console.Write("Start at :{0}", now.ToString("HH:mm:ss"));
        //               for (int i = 0; i < chekloop; i++)
        //               {
        //                   Thread.Sleep(TimeSpan.FromMinutes(interval));
        //                   var firstTime = DateTime.Now;
        //                   if (i % 5 == 0)
        //                       Console.Write("\n");
        //                   Console.Write("+{0} min=> {1}", interval, firstTime.ToString("HH:mm:ss"));
        //                   Console.Write(" | ");
        //               }
        //               if (lastcheckloop > 0)
        //               {
        //                   Thread.Sleep(TimeSpan.FromMinutes(lastcheckloop));
        //                   var lastTime = DateTime.Now;
        //                   Console.Write("+{0} min=>{1}", lastcheckloop, lastTime.ToString("HH:mm:ss"));
        //                   Console.Write(" | ");
        //               }
        //               process.Kill();
        //           })
        //       );
        //    Task.WaitAll(taskList.ToArray());
        //}
    }
}
