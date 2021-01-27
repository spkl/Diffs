using System.Collections;
using System.Collections.Generic;

namespace spkl.Diffs
{
    public readonly struct ArrayView<T> : IReadOnlyList<T>
    {
        private readonly T[] array;

        public int StartOffset { get; }

        public int Length { get; }

        public ArrayView(T[] array)
            : this(array, 0, array.Length)
        {
        }

        public ArrayView(T[] array, int startIndex)
            : this(array, startIndex, array.Length - startIndex)
        {
        }

        public ArrayView(T[] array, int startIndex, int length)
        {
            this.array = array;
            this.StartOffset = startIndex;
            this.Length = length;
        }

        public ArrayView(ArrayView<T> source, int startIndex)
            : this(source, startIndex, source.Length - startIndex)
        {
        }

        public ArrayView(ArrayView<T> source, int startIndex, int length)
        {
            this.array = source.array;
            this.StartOffset = source.StartOffset + startIndex;
            this.Length = length;
        }

        public ArrayView<T> TrimStart(int count)
        {
            return new ArrayView<T>(this, count);
        }

        public ArrayView<T> TrimEnd(int count)
        {
            return new ArrayView<T>(this, 0, this.Length - count);
        }

        public ArrayView<T> Range(int startIndex, int endIndexExclusive)
        {
            return new ArrayView<T>(this, startIndex, endIndexExclusive - startIndex);
        }

        public ArrayView<T> Range(int startIndex)
        {
            return this.TrimStart(startIndex);
        }

        public T this[int index]
        {
            get => this.array[index + this.StartOffset];
            set => this.array[index + this.StartOffset] = value;
        }

        public int Count => this.Length;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
