// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace spkl.Diffs.Test;

public class IndexRangeClass
{
    private class EnumeratorWrapper : IEnumerable<int>
    {
        private readonly IEnumerator<int> enumerator;
        public EnumeratorWrapper(IEnumerator<int> enumerator) => this.enumerator = enumerator;
        public IEnumerator<int> GetEnumerator() => this.enumerator;
        IEnumerator IEnumerable.GetEnumerator() => this.enumerator;
    }

    [Test]
    [TestCase(0, 3, new[] { 0, 1, 2 })]
    [TestCase(1, 4, new[] { 1, 2, 3, 4 })]
    [TestCase(-1, 3, new[] { -1, 0, 1 })]
    public void ConstructorAndProperties(int start, int length, int[] contents)
    {
        IndexRange range = new(start, length);
        Assert.That(range.Start, Is.EqualTo(start));
        Assert.That(range.Length, Is.EqualTo(length));
        Assert.That(new EnumeratorWrapper(range.GetEnumerator()), Is.EqualTo(contents));
    }

    [Test]
    [TestCase(0, 3)]
    [TestCase(1, 4)]
    [TestCase(-1, 3)]
    public void Indexer(int start, int length)
    {
        IndexRange range = new(start, length);
        foreach (int index in Enumerable.Range(start, length))
        {
            Assert.That(range[index], Is.EqualTo(start + index));
        }
    }

    [Test]
    [TestCase(0, 3, 1, new[] { 1, 2 })]
    [TestCase(0, 3, 0, new[] { 0, 1, 2 })]
    [TestCase(1, 4, 2, new[] { 3, 4 })]
    [TestCase(-2, 3, 1, new[] { -1, 0 })]
    public void TrimStart(int start, int length, int trim, int[] contents)
    {
        IndexRange range = new(start, length);
        IndexRange trimmedRange = range.TrimStart(trim);
        Assert.That(range.Start, Is.EqualTo(start));
        Assert.That(range.Length, Is.EqualTo(length));
        Assert.That(trimmedRange.Start, Is.EqualTo(start + trim));
        Assert.That(trimmedRange.Length, Is.EqualTo(length - trim));
        Assert.That(new EnumeratorWrapper(trimmedRange.GetEnumerator()), Is.EqualTo(contents));
    }

    [Test]
    [TestCase(0, 3, 1, new[] { 0, 1 })]
    [TestCase(0, 3, 0, new[] { 0, 1, 2 })]
    [TestCase(1, 4, 2, new[] { 1, 2 })]
    [TestCase(-2, 3, 1, new[] { -2, -1 })]
    public void TrimEnd(int start, int length, int trim, int[] contents)
    {
        IndexRange range = new(start, length);
        IndexRange trimmedRange = range.TrimEnd(trim);
        Assert.That(range.Start, Is.EqualTo(start));
        Assert.That(range.Length, Is.EqualTo(length));
        Assert.That(trimmedRange.Start, Is.EqualTo(start));
        Assert.That(trimmedRange.Length, Is.EqualTo(length - trim));
        Assert.That(new EnumeratorWrapper(trimmedRange.GetEnumerator()), Is.EqualTo(contents));
    }

    [Test]
    [TestCase(0, 3, 0, 2, new[] { 0, 1 })]
    [TestCase(0, 3, 1, 3, new[] { 1, 2 })]
    [TestCase(1, 4, 1, 4, new[] { 2, 3, 4 })]
    [TestCase(1, 4, 2, 3, new[] { 3 })]
    [TestCase(1, 4, 3, 3, new int[0])]
    [TestCase(-2, 4, 1, 3, new[] { -1, 0 })]
    public void Range2(int start, int length, int rangeStart, int rangeEnd, int[] contents)
    {
        IndexRange range = new(start, length);
        IndexRange newRange = range.Range(rangeStart, rangeEnd);
        Assert.That(range.Start, Is.EqualTo(start));
        Assert.That(range.Length, Is.EqualTo(length));
        Assert.That(newRange.Start, Is.EqualTo(start + rangeStart));
        Assert.That(newRange.Length, Is.EqualTo(rangeEnd - rangeStart));
        Assert.That(new EnumeratorWrapper(newRange.GetEnumerator()), Is.EqualTo(contents));
    }

    [Test]
    [TestCase(0, 3, 1, new[] { 1, 2 })]
    [TestCase(0, 3, 0, new[] { 0, 1, 2 })]
    [TestCase(1, 4, 2, new[] { 3, 4 })]
    [TestCase(-2, 3, 1, new[] { -1, 0 })]
    public void Range1(int start, int length, int rangeStart, int[] contents)
    {
        IndexRange range = new(start, length);
        IndexRange newRange = range.Range(rangeStart);
        Assert.That(range.Start, Is.EqualTo(start));
        Assert.That(range.Length, Is.EqualTo(length));
        Assert.That(newRange.Start, Is.EqualTo(start + rangeStart));
        Assert.That(newRange.Length, Is.EqualTo(length - rangeStart));
        Assert.That(new EnumeratorWrapper(newRange.GetEnumerator()), Is.EqualTo(contents));
    }
}
