// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;

namespace spkl.Diffs
{
    /// <summary>
    /// An array wrapper enabling arbitrary (including negative) element indexes.
    /// </summary>
    /// <typeparam name="T">The array element type.</typeparam>
    public class VArray<T> : IEnumerable<T>
    {
        private readonly T[] array;

        private readonly int offset;

        private VArray(int firstIndex, int length)
        {
            this.array = new T[length];
            this.offset = firstIndex;
        }

        /// <summary>
        /// Creates a new <see cref="VArray{T}"/> with indexes ranging from <paramref name="firstIndex"/> to <paramref name="lastIndex"/>.
        /// </summary>
        public static VArray<T> CreateFromTo(int firstIndex, int lastIndex)
        {
            if (lastIndex < firstIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(lastIndex));
            }

            return new VArray<T>(firstIndex, lastIndex - firstIndex + 1);
        }

        /// <summary>
        /// Creates a new <see cref="VArray{T}"/> with indexes starting at <paramref name="firstIndex"/> and the specified <paramref name="length"/>.
        /// </summary>
        public static VArray<T> CreateByLength(int firstIndex, int length)
        {
            return new VArray<T>(firstIndex, length);
        }

        /// <summary>
        /// Provides a debugging view combining all indexes with their corresponding elements.
        /// </summary>
        public (int index, T value)[] DebugView 
        {
            get
            {
                (int, T)[] result = new (int, T)[this.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (i + this.offset, this.array[i]);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to get or set.</param>
        public T this[int index]
        { 
            get => this.array[index - this.offset]; 
            set => this.array[index - this.offset] = value; 
        }

        /// <summary>
        /// The smallest index of this instance.
        /// </summary>
        public int LowerBoundInclusive => this.offset;

        /// <summary>
        /// The first index that is not part of the instance.
        /// </summary>
        public int UpperBoundExclusive => this.Count + this.offset;

        /// <inheritdoc cref="ICollection{T}.Count"/>
        public int Count => ((ICollection<T>)this.array).Count;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.array).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.array.GetEnumerator();
        }
    }
}
