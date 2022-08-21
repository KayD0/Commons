using System;
using System.Collections.Generic;
using System.Text;

namespace CommonsMulti
{
    public class Process
    {
        Services s = new Services();
        public void Main1() 
        {
            
            s.thread1();
        }

        public void Main2()
        {

            s.task_simple();
        }

        public void Main3()
        {

            s.task_wait_end();
        }
    }
}
