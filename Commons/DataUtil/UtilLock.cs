using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Data;
using System.Collections.Concurrent;

namespace Commons.DataUtil
{
    public class UtilLock
    {
        private static UtilLock instance = null;
        public static UtilLock Instance => instance ?? (instance = new UtilLock());

        public ConcurrentQueue<int> Queue { get; }

        public UtilLock()
        {
            Console.WriteLine("Constructor called.");
            Queue = new ConcurrentQueue<int>();
        }
    }

    public class UtilLock2
    {
        private static Lazy<UtilLock2> instance = new Lazy<UtilLock2>();
        public static UtilLock2 Instance => instance.Value;

        public ConcurrentQueue<int> Queue { get; }

        public UtilLock2()
        {
            Console.WriteLine("Constructor called.");
            Queue = new ConcurrentQueue<int>();
        }
    }
}
