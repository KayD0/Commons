using System;
using System.Collections.Generic;
using System.Text;

namespace CommonsDelegade
{
    public class Process
    {
        private delegate void testDlgt();
        private delegate bool testDlgtStr(string str);

        public void Main() 
        {
            testDlgt dlgt1 = new testDlgt(button1_toku);
            this.KyotuSyori(dlgt1);
            testDlgt dlgt2 = new testDlgt(button2_toku);
            this.KyotuSyori(dlgt2);

            //testDlgtStr dlgtStr = null;
            //dlgtStr += new testDlgtStr(button3_toku("test"));
            //dlgtStr("");
            //dlgtStr += new testDlgtStr(button3_toku);
            //dlgtStr("");

            testDlgtStr dlgtStr = button3_toku;
            dlgtStr?.Invoke("test");
            
            this.KyotuSyori(dlgtStr,"test");

        }


        private void KyotuSyori(testDlgt dlgt)
        {
            Console.WriteLine("共通処理");
            dlgt();
            Console.WriteLine("共通処理");
        }

        private void KyotuSyori(testDlgtStr dlgt,string test)
        {
            Console.WriteLine("共通処理");
            dlgt(test);
            Console.WriteLine("共通処理");
        }

        private void button1_toku()
        {
            Console.WriteLine("button1の特別な処理");
        }
        private void button2_toku()
        {
            Console.WriteLine("button2の特別な処理");
        }
        private bool button3_toku(string str)
        {
            Console.WriteLine($"button2の特別な処理：{str}");
            return true;
        }
    }
}
