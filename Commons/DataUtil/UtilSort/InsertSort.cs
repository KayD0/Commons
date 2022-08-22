using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Commons.DataUtil.UtilSort
{
    public static class InsertSort
    {
        /// </summary>
        public static int[] InsertionSort(IEnumerable<int> items)
        {
            var nums = items.ToArray();
            var n = nums.Length;

            // n-1回ループ
            for (int i = 1; i < n; i++)
            {
                // aを取り出す
                var a = nums[i];
                var k = i;

                // aより左にある操作済みの任意の箇所にaを挿入
                while (k >= 1 && a < nums[k - 1])
                {
                    nums[k] = nums[k - 1];
                    k--;
                }

                nums[k] = a;
            }

            return nums;
        }
    }
}
