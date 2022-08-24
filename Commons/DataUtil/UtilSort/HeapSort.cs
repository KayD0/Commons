using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Commons.DataUtil.UtilSort
{
    public static class HeapSort<T> where T : IComparable<T>
    {
        public enum OrderBy
        {
            Ascending,      // 昇順
            Descending      // 降順
        }

        public static IEnumerable<T> Excuse(IEnumerable<T> items, IComparer<T> comparer)
        {
            var heap = new Heap(items, comparer);
            var n = heap.Size;

            for (int i = 0; i < n; i++)
                yield return heap.Pop();
        }

        public static IEnumerable<T> Excuse(IEnumerable<T> items, OrderBy way)
        {
            if (way == OrderBy.Ascending) return Excuse(items, Comparer<T>.Create((a, b) => b.CompareTo(a)));
            else return Excuse(items, Comparer<T>.Create((a, b) => a.CompareTo(b)));
        }

        public class Heap
        {
            private T[] _tree;
            public int Size { get; private set; }

            private IComparer<T> _comparer;

            public Heap(IEnumerable<T> items)
                => Create(items);

            public Heap(IEnumerable<T> items, IComparer<T> comparer)
            {
                _comparer = comparer;
                Create(items);
            }

            /// <summary>
            /// ヒープ作成
            /// </summary>
            public void Create(IEnumerable<T> items)
            {
                var tree = items.ToArray();

                for (int i = 0; i < tree.Length; i++)
                {
                    // 挿入するデータの添字
                    var n = i;
                    while (n > 0)
                    {
                        var parent = (n - 1) / 2;
                        if (Compare(tree[parent], tree[n]) < 0) Swap(ref tree[parent], ref tree[n]);
                        n = parent;
                    }
                }

                _tree = tree;
                Size = tree.Length;
            }

            /// <summary>
            /// ヒープから根を取り出す
            /// </summary>
            public T Pop()
            {
                if (Size == 0) throw new Exception("ヒープのSizeが0なのでPopすることはできません.");

                // 根を取り出す
                var root = _tree[0];
                _tree[0] = _tree[Size - 1];
                Size--;

                // ヒープの構造が保たれるよう操作
                var i = 0;
                while (2 * i + 1 < Size)
                {
                    var leftChild = 2 * i + 1;
                    var rightChild = (2 * i + 2 < Size) ? (2 * i + 2) : -1;

                    // 子の大きい方と比べる
                    var target = leftChild;
                    if (rightChild != -1)
                    {
                        if (Compare(_tree[leftChild], _tree[rightChild]) < 0) target = rightChild;
                    }

                    // もし子の方が大きければ交換する
                    if (target != -1 && Compare(_tree[i], _tree[target]) < 0) Swap(ref _tree[i], ref _tree[target]);
                    i = target;
                }

                return root;
            }

            private int Compare(T a, T b)
            {
                if (_comparer == null) a.CompareTo(b);
                return _comparer.Compare(a, b);
            }

            private void Swap(ref T x, ref T y)
            {
                var tmp = x;
                x = y;
                y = tmp;
            }
        }
    }
}
