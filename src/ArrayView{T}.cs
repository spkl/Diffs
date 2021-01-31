using System.Collections;
using System.Collections.Generic;

namespace spkl.Diffs
{
    /// <summary>
    /// A (worse-performing) .NET standard 2.0 / .NET Full Framework alternative to the Span&lt;T&gt;/Memory&lt;T&gt; type.
    /// This struct gives you the ability to encapsulate a segment of an array into an object that gives array-like read and write access to the original array.
    /// For example: new ArrayView&lt;int&gt;(new int[]{0, 1, 2, 3}, 1, 2) gives you access to indexes 1 and 2 from the original array,
    /// where ArrayView index 0 corresponds to original array index 1.
    /// </summary>
    /// <typeparam name="T">The array element type.</typeparam>
    public readonly struct ArrayView<T> : IReadOnlyList<T>
    {
        private readonly T[] array;

        /// <summary>
        /// The start offset/index of this <see cref="ArrayView{T}"/> in relation to the original array.
        /// Index 0 of the <see cref="ArrayView{T}"/> corresponds to index <see cref="StartOffset"/> of the original array.
        /// </summary>
        public int StartOffset { get; }

        /// <summary>
        /// Gets the number of elements of the <see cref="ArrayView{T}"/>.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Creates a new instance granting access to all of the array.
        /// </summary>
        /// <param name="array">The array.</param>
        public ArrayView(T[] array)
            : this(array, 0, array.Length)
        {
        }

        /// <summary>
        /// Creates a new instance granting access to the array elements from <paramref name="startIndex"/> to the end of the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="startIndex">The start index.</param>
        public ArrayView(T[] array, int startIndex)
            : this(array, startIndex, array.Length - startIndex)
        {
        }

        /// <summary>
        /// Creates a new instance granting access to <paramref name="length"/> array elements, beginning from <paramref name="startIndex"/>.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length of the <see cref="ArrayView{T}"/>.</param>
        public ArrayView(T[] array, int startIndex, int length)
        {
            this.array = array;
            this.StartOffset = startIndex;
            this.Length = length;
        }

        /// <summary>
        /// Creates a new instance granting access to the <see cref="ArrayView{T}"/> elements from <paramref name="startIndex"/> to the end of the <see cref="ArrayView{T}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="ArrayView{T}"/>.</param>
        /// <param name="startIndex">The start index.</param>
        public ArrayView(ArrayView<T> source, int startIndex)
            : this(source, startIndex, source.Length - startIndex)
        {
        }

        /// <summary>
        /// Creates a new instance granting access to <paramref name="length"/> array elements,
        /// beginning from <paramref name="startIndex"/> (in relation to the <paramref name="source"/> indexes).
        /// </summary>
        /// <param name="source">The source <see cref="ArrayView{T}"/>.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length of the <see cref="ArrayView{T}"/>.</param>
        public ArrayView(ArrayView<T> source, int startIndex, int length)
        {
            this.array = source.array;
            this.StartOffset = source.StartOffset + startIndex;
            this.Length = length;
        }

        /// <summary>
        /// Creates a new <see cref="ArrayView{T}"/> from this instance, skipping the first <paramref name="count"/> elements.
        /// </summary>
        /// <param name="count">The number of elements to skip from the start of this <see cref="ArrayView{T}"/>.</param>
        /// <returns>A new <see cref="ArrayView{T}"/> with a smaller number of elements.</returns>
        public ArrayView<T> TrimStart(int count)
        {
            return new ArrayView<T>(this, count);
        }

        /// <summary>
        /// Creates a new <see cref="ArrayView{T}"/> from this instance, skipping the last <paramref name="count"/> elements.
        /// </summary>
        /// <param name="count">The number of elements to skip from the end of this <see cref="ArrayView{T}"/>.</param>
        /// <returns>A new <see cref="ArrayView{T}"/> with a smaller number of elements.</returns>
        public ArrayView<T> TrimEnd(int count)
        {
            return new ArrayView<T>(this, 0, this.Length - count);
        }

        /// <summary>
        /// Creates a new <see cref="ArrayView{T}"/> from this instance,
        /// granting access to element indexes [<paramref name="startIndex"/>..<paramref name="endIndexExclusive"/>] (in relation to this instances indexes).
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndexExclusive">The index of the first element not included in the new <see cref="ArrayView{T}"/>.</param>
        /// <returns>A new <see cref="ArrayView{T}"/> with a smaller number of elements.</returns>
        public ArrayView<T> Range(int startIndex, int endIndexExclusive)
        {
            return new ArrayView<T>(this, startIndex, endIndexExclusive - startIndex);
        }

        /// <summary>
        /// Creates a new <see cref="ArrayView{T}"/> from this instance,
        /// granting access to the <see cref="ArrayView{T}"/> elements from <paramref name="startIndex"/> to the end of the <see cref="ArrayView{T}"/>.
        /// </summary>
        /// <remarks>
        /// This is essentially the same as calling <see cref="TrimStart(int)"/>.
        /// </remarks>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A new <see cref="ArrayView{T}"/> with a smaller number of elements.</returns>
        public ArrayView<T> Range(int startIndex)
        {
            return this.TrimStart(startIndex);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// The operation is performed on the original array; indexes are translated accordingly.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        public T this[int index]
        {
            get => this.array[index + this.StartOffset];
            set => this.array[index + this.StartOffset] = value;
        }

        /// <inheritdoc />
        public int Count => this.Length;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this[i];
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
