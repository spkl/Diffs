using System;
using System.Collections;
using System.Collections.Generic;

namespace Diffs
{
    public class VArray<T> : IEnumerable<T>
    {
        private readonly T[] array;

        private readonly int offset;

        private VArray(int firstIndex, int length)
        {
            this.array = new T[length];
            this.offset = firstIndex;
        }

        public static VArray<T> CreateFromTo(int firstIndex, int lastIndex)
        {
            if (lastIndex < firstIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(lastIndex));
            }

            return new VArray<T>(firstIndex, lastIndex - firstIndex + 1);
        }

        public static VArray<T> CreateByLength(int firstIndex, int length)
        {
            return new VArray<T>(firstIndex, length);
        }

        public (int index, T value)[] DebugView 
        {
            get
            {
                (int, T)[] result = new (int, T)[this.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (i + offset, this.array[i]);
                }

                return result;
            }
        }

        public T this[int index]
        { 
            get => this.array[index - this.offset]; 
            set => this.array[index - this.offset] = value; 
        }

        public int LowerBoundInclusive => this.offset;

        public int UpperBoundExclusive => this.Count + offset;

        public int Count => ((ICollection<T>)array).Count;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }
    }
}
