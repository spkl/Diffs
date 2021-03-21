// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using System.Linq;

namespace spkl.Diffs.Test
{
    public class ArrayViewClass
    {
        private double[] testArray;

        [SetUp]
        public void SetUp()
        {
            this.testArray = new double[] { 0.0, 10.0, 20.0, 30.0, 40.0, 50.0, 60.0 };
        }

        [Test]
        public void ConstructorAndProperties1()
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray);
            Assert.That(arrayView.StartOffset, Is.EqualTo(0));
            Assert.That(arrayView.Length, Is.EqualTo(this.testArray.Length));
            Assert.That(arrayView.Count, Is.EqualTo(this.testArray.Length));
            Assert.That(arrayView, Is.EqualTo(this.testArray));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        public void ConstructorAndProperties2(int startIndex)
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray, startIndex);
            Assert.That(arrayView.StartOffset, Is.EqualTo(startIndex));
            Assert.That(arrayView.Length, Is.EqualTo(this.testArray.Length - startIndex));
            Assert.That(arrayView.Count, Is.EqualTo(this.testArray.Length - startIndex));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Skip(startIndex)));
        }

        [Test]
        [TestCase(0, 7)]
        [TestCase(1, 6)]
        [TestCase(3, 2)]
        public void ConstructorAndProperties3(int startIndex, int length)
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray, startIndex, length);
            Assert.That(arrayView.StartOffset, Is.EqualTo(startIndex));
            Assert.That(arrayView.Length, Is.EqualTo(length));
            Assert.That(arrayView.Count, Is.EqualTo(length));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Skip(startIndex).Take(length)));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        public void ConstructorAndProperties4(int startIndex)
        {
            ArrayView<double> arrayView = new ArrayView<double>(new ArrayView<double>(this.testArray), startIndex);
            Assert.That(arrayView.StartOffset, Is.EqualTo(startIndex));
            Assert.That(arrayView.Length, Is.EqualTo(this.testArray.Length - startIndex));
            Assert.That(arrayView.Count, Is.EqualTo(this.testArray.Length - startIndex));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Skip(startIndex)));
        }

        [Test]
        [TestCase(0, 7)]
        [TestCase(1, 6)]
        [TestCase(3, 2)]
        public void ConstructorAndProperties5(int startIndex, int length)
        {
            ArrayView<double> arrayView = new ArrayView<double>(new ArrayView<double>(this.testArray), startIndex, length);
            Assert.That(arrayView.StartOffset, Is.EqualTo(startIndex));
            Assert.That(arrayView.Length, Is.EqualTo(length));
            Assert.That(arrayView.Count, Is.EqualTo(length));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Skip(startIndex).Take(length)));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        public void TrimStart(int trim)
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray).TrimStart(trim);
            Assert.That(arrayView.StartOffset, Is.EqualTo(trim));
            Assert.That(arrayView.Length, Is.EqualTo(this.testArray.Length - trim));
            Assert.That(arrayView.Count, Is.EqualTo(this.testArray.Length - trim));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Skip(trim)));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        public void TrimEnd(int trim)
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray).TrimEnd(trim);
            Assert.That(arrayView.StartOffset, Is.EqualTo(0));
            Assert.That(arrayView.Length, Is.EqualTo(this.testArray.Length - trim));
            Assert.That(arrayView.Count, Is.EqualTo(this.testArray.Length - trim));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Take(this.testArray.Length - trim)));
        }

        [Test]
        public void TrimStartTrimEnd()
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray).TrimStart(1).TrimEnd(2);
            Assert.That(arrayView.StartOffset, Is.EqualTo(1));
            Assert.That(arrayView.Length, Is.EqualTo(this.testArray.Length - 3));
            Assert.That(arrayView.Count, Is.EqualTo(this.testArray.Length - 3));
            Assert.That(arrayView, Is.EqualTo(this.testArray.Skip(1).Take(this.testArray.Length - 3)));
        }

        [Test]
        public void Range()
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray);
            ArrayView<double> arrayView2 = arrayView.Range(1, 3);
            Assert.That(arrayView2.StartOffset, Is.EqualTo(1));
            Assert.That(arrayView2.Length, Is.EqualTo(2));
            Assert.That(arrayView2.Count, Is.EqualTo(2));
            Assert.That(arrayView2, Is.EqualTo(new[] { 10.0, 20.0 }));

            ArrayView<double> arrayView3 = arrayView.Range(3);
            Assert.That(arrayView3.StartOffset, Is.EqualTo(3));
            Assert.That(arrayView3.Length, Is.EqualTo(4));
            Assert.That(arrayView3.Count, Is.EqualTo(4));
            Assert.That(arrayView3, Is.EqualTo(new[] { 30.0, 40.0, 50.0, 60.0 }));
        }

        [Test]
        public void Indexer()
        {
            ArrayView<double> arrayView = new ArrayView<double>(this.testArray, 2, 3);
            arrayView[0] = 21.0;
            arrayView[1] = 32.0;
            arrayView[2] = 43.0;

            Assert.That(arrayView, Is.EqualTo(new[] { 21.0, 32.0, 43.0 }));
            Assert.That(this.testArray, Is.EqualTo(new[] { 0.0, 10.0, 21.0, 32.0, 43.0, 50.0, 60.0 }));
        }
    }
}
