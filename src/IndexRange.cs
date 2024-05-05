// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;

namespace spkl.Diffs;

/// <summary>
/// Encapsulates a range of indexes that can be enumerated.
/// </summary>
public readonly ref struct IndexRange
{
    private readonly int start;

    private readonly int length;

    /// <summary>
    /// The start of the range, i.e. the first index.
    /// </summary>
    public int Start => this.start;

    /// <summary>
    /// The length of the range, i.e. the number of indexes in the range.
    /// </summary>
    public int Length => this.length;

    /// <summary>
    /// Creates a new instance with the specified <paramref name="start"/> and <paramref name="length"/>.
    /// </summary>
    public IndexRange(int start, int length)
    {
        this.start = start;
        this.length = length;
    }

    /// <summary>
    /// Gets the index at the specified offset.
    /// Offset '0' equals <see cref="Start"/>.
    /// </summary>
    /// <returns>The offset of the index to get.</returns>
    public int this[int offset]
    {
        get => offset + this.start;
    }

    /// <summary>
    /// Creates a new <see cref="IndexRange"/> by trimming <paramref name="count"/> number of indexes from the start of this instance.
    /// </summary>
    /// <param name="count">Number of indexes to trim from the start.</param>
    /// <returns>A new instance with a smaller range.</returns>
    public IndexRange TrimStart(int count)
    {
        return new IndexRange(this.start + count, this.length - count);
    }

    /// <summary>
    /// Creates a new <see cref="IndexRange"/> by trimming <paramref name="count"/> number of indexes from the end of this instance.
    /// </summary>
    /// <param name="count">Number of indexes to trim from the end.</param>
    /// <returns>A new instance with a smaller range.</returns>
    public IndexRange TrimEnd(int count)
    {
        return new IndexRange(this.start, this.length - count);
    }

    /// <summary>
    /// Creates a new <see cref="IndexRange"/> by specifying the indexes of the indexes to start and end with.
    /// </summary>
    /// <param name="startIndex">The index of the index in this range to start the new range with.</param>
    /// <param name="endIndexExclusive">The index of the index in this range that is the first one not contained in the new range.</param>
    /// <returns>A new instance with a smaller range.</returns>
    public IndexRange Range(int startIndex, int endIndexExclusive)
    {
        return new IndexRange(this.start + startIndex, endIndexExclusive - startIndex);
    }

    /// <summary>
    /// Creates a new <see cref="IndexRange"/> that starts with the index that is at index <paramref name="startIndex"/> of this instance.
    /// The last index of the new instance is the same as in this instance.
    /// This is equal to calling <see cref="TrimStart(int)"/>.
    /// </summary>
    /// <param name="startIndex">The index of the index in this range to start the new range with.</param>
    /// <returns>A new instance with a smaller range.</returns>
    public IndexRange Range(int startIndex)
    {
        return this.TrimStart(startIndex);
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<int> GetEnumerator()
    {
        return Enumerable.Range(this.start, this.length).GetEnumerator();
    }
}
