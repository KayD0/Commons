using Commons.DataUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commons
{
    class Program
    {
        static void Main(string[] args)
        {

            // 5個のスレッドで並列に実行
            for (int i = 0; i < 5; i++)
            {
                _ = Task.Run(() =>
                {
                    for (int j = 0; j < 3; j++)
                    {
                        UtilLock2.Instance.Queue.Enqueue(j);
                        Thread.Sleep(1);
                    }
                });
            }

            var count = 0;
            while (count < 15)
            {
                int item = 0;
                if (UtilLock2.Instance.Queue.TryDequeue(out item))
                {
                    Console.Write(item);
                    count++;
                }
                Thread.Sleep(1);
            }
            Console.ReadLine();
        }
    }
}
