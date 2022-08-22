using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Commons.DataUtil.UtilSort
{
    public static class SelectionSort<T> where T : IComparable<T>
    {
        /// <summary>
        /// 選択ソート
        /// https://www.hanachiru-blog.com/entry/2020/08/20/120000#%E9%81%B8%E6%8A%9E%E3%82%BD%E3%83%BC%E3%83%88
        /// </summary>
        public static T[] Excuse(IEnumerable<T> items)
        {
            var nums = items.ToArray();
            var n = nums.Length;

            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                    if (nums[min].CompareTo(nums[j]) > 0) min = j;

                Swap(ref nums[i], ref nums[min]);
            }

            return nums;
        }

        private static void Swap(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}
