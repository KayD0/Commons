using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Commons.DataUtil.UtilSort
{
    public static class QuickSort
    {
        /// <summary>
        /// クイックソート
        /// </summary>
        /// <param name="array">対象の配列</param>
        /// <param name="left">ソート範囲の最初のインデックス</param>
        /// <param name="right">ソート範囲の最後のインデックス</param>
        public static void Sort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            // 範囲が一つだけなら処理を抜ける
            if (left >= right) return;

            // ピポットを選択(範囲の先頭・真ん中・末尾の中央値を使用)
            T pivot = Median(array[left], array[(left + right) / 2], array[right]);

            int i = left;
            int j = right;

            while (i <= j)
            {
                // array[i] < pivot まで左から探索
                while (i < right && array[i].CompareTo(pivot) < 0) i++;
                // array[i] >= pivot まで右から探索
                while (j > left && array[j].CompareTo(pivot) >= 0) j--;

                if (i > j) break;
                Swap<T>(ref array[i], ref array[j]);

                // 交換を行った要素の次の要素にインデックスを進める
                i++;
                j--;
            }

            Sort(array, left, i - 1);
            Sort(array, i, right);
        }

        /// <summary>
        /// 中央値を求める
        /// </summary>
        private static T Median<T>(T x, T y, T z) where T : IComparable<T>
        {
            // x > y なら1以上の整数値が返される
            if (x.CompareTo(y) > 0) Swap(ref x, ref y);
            if (x.CompareTo(z) > 0) Swap(ref x, ref z);
            if (y.CompareTo(z) > 0) Swap(ref y, ref z);
            return y;
        }

        /// <summary>
        /// 参照を入れ替える(値型だと変数のコピーになってしまうため)
        /// </summary>
        private static void Swap<T>(ref T x, ref T y) where T : IComparable<T>
        {
            var tmp = x;
            x = y;
            y = tmp;
        }
    }
}
