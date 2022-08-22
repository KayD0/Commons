using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommonsMulti.Sample
{
    public class SampleMethod
    {
        public static void HeavyMethod1()
        {
            Console.WriteLine("すごく重い処理その1(´・ω・｀)はじまり");
            Thread.Sleep(5000);
            Console.WriteLine("すごく重い処理その1(´・ω・｀)おわり");
        }

        public static void HeavyMethod2()
        {
            Console.WriteLine("すごく重い処理その2(´・ω・｀)はじまり");
            Thread.Sleep(3000);
            Console.WriteLine("すごく重い処理その2(´・ω・｀)おわり");
        }

        public static string HeavyMethod3()
        {
            Console.WriteLine("すごく重い処理その2(´・ω・｀)はじまり");
            Thread.Sleep(3000);
            Console.WriteLine("すごく重い処理その2(´・ω・｀)おわり");
            return "hoge";
        }

        public static int HeavyMethod4()
        {
            Console.WriteLine("すごく重い処理その2(´・ω・｀)はじまり");
            Thread.Sleep(3000);
            Console.WriteLine("すごく重い処理その2(´・ω・｀)おわり");
            return 1;
        }

        public static int HeavyMethod5()
        {
            Console.WriteLine("すごく重い処理その2(´・ω・｀)はじまり");
            Thread.Sleep(3000);
            Console.WriteLine("すごく重い処理その2(´・ω・｀)おわり");
            return 2;
        }

        public static void HeavyMethod6(IProgress<int> progress)
        {
            for (var i = 1; i < 100; i++)
            {
                Thread.Sleep(100);
                progress.Report(i); // 進捗を報告
            }
        }
    }
}
