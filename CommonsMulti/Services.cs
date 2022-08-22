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
        //https://tech-lab.sios.jp/archives/15637
        //https://qwerty2501.hatenablog.com/entry/2014/04/24/235849
        //https://zenn.dev/okazuki/articles/async-sync-webapi-dotnetfw
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

            try
            {
                // WhenAllの引数に、先程生成したTaskを入れると、HeavyMethod1、HeavyMethod2が終了するまで
                // 次のコードに進みません。つまり待機します。
                Task.WhenAll(taskList);
                //Task.WhenAll(task1, task2);
            }
            // 例外をキャッチする場合には、Waitメソッドを実施している部分をtry...catchでくくります。
            // 例外が発生すると、AggregateExceptionにラップされてスローされます。
            catch (AggregateException e)
            {
                Console.WriteLine("受け止めたよ⌒ﾟヾ( ･д･ o) キャッチ!!");
            }


            int sum = taskList.Select(x => x.Result).Sum();

            Console.WriteLine(sum);
        }

        public async Task task_progressAsync()
        {
            int value = 0; // プログレスバーの初期化

            var progress = new Progress<int>(x => {
                value = x; // 進捗を管理するオブジェクト(progress)の値とプログレスバーの値を紐づけ
                Console.WriteLine(value);
            });

            await Task.Run(() => SampleMethod.HeavyMethod6(progress));
        }

        public async Task task_progressAsync2()
        {
            int value = 0; // プログレスバーの初期化

            var progress = new Progress<int>(x => {
                value = x; // 進捗を管理するオブジェクト(progress)の値とプログレスバーの値を紐づけ
                Console.WriteLine(value);
            });

            await Task.Run(() => SampleMethod.HeavyMethod6(progress));
        }


    }

    public class TaskProgressAsync2 
    {
    
    }
}
