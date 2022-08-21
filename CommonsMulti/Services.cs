using CommonsMulti.Sample;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CommonsMulti
{
    public class Services
    {
        public void thread1() 
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                SampleMethod.HeavyMethod1();
            }));

            thread.Start();

            SampleMethod.HeavyMethod2();
        }

        public void task_simple()
        {
            // Taskを生成しています。Task.RunメソッドはTaskを生成するFactoryクラスで、
            // 戻り値はTask型のオブジェクトです。
            // 引数には、delegateを渡します。単純にHeavyMethod1を実行したい場合は、
            // 引数なしのdelegateを渡します。
            Task task = Task.Run(() => {
                SampleMethod.HeavyMethod1();
            });

            SampleMethod.HeavyMethod2();
        }

        public void task_wait_end()
        {
            // 1の整数を返すHeavyMethod1のTaskを生成します。
            Task<int> task1 = Task.Run(() => {
                return SampleMethod.HeavyMethod4();
            });

            // 2の整数を返すHeavyMethod2のTaskを生成します。
            Task<int> task2 = Task.Run(() => {
                return SampleMethod.HeavyMethod5();
            });

            // 1の整数を返すHeavyMethod1のTaskを生成します。
            Task<int> task3 = Task.Run(() => {
                return SampleMethod.HeavyMethod4();
            });

            // 1の整数を返すHeavyMethod1のTaskを生成します。
            Task<int> task4 = Task.Run(() => {
                return SampleMethod.HeavyMethod4();
            });


            List<Task<int>> taskList = new List<Task<int>>();
            taskList.Add(task1);
            taskList.Add(task2);
            taskList.Add(task3);
            taskList.Add(task4);

            // WhenAllの引数に、先程生成したTaskを入れると、HeavyMethod1、HeavyMethod2が終了するまで
            // 次のコードに進みません。つまり待機します。
            Task.WhenAll(taskList);

            int sum = taskList.Select(x => x.Result).Sum();

            Console.WriteLine(sum);
        }
    }
}
