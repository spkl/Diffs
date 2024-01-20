// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace spkl.Diffs
{
    /// <summary>
    /// Provides the diff result or shortest edit script for two sequences A and B using Eugene Myers diff algorithm.
    /// </summary>
    /// <typeparam name="T">The sequence item type.</typeparam>
    public class MyersDiff<T>
    {
        private readonly T[] aValues, bValues;

        private readonly bool[] aRemoved, bAdded;

        private readonly IEqualityComparer<T> comparer;

        private readonly VArray<int> Vf, Vr;

        /// <summary>
        /// Creates a new instance of the <see cref="MyersDiff{T}"/> class
        /// and calculates the diff result of sequences A and B
        /// using the <see cref="object.Equals(object)"/> method to determine item equality.
        /// </summary>
        /// <param name="aValues">Item sequence A.</param>
        /// <param name="bValues">Item sequence B.</param>
        public MyersDiff(T[] aValues, T[] bValues)
            : this(aValues, bValues, null)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MyersDiff{T}"/> class
        /// and calculates the diff result of sequences A and B
        /// using the provided <see cref="IEqualityComparer{T}"/> to determine item equality.
        /// </summary>
        /// <param name="aValues">Item sequence A.</param>
        /// <param name="bValues">Item sequence B.</param>
        /// <param name="comparer">The implementation to determine item equality.</param>
        public MyersDiff(T[] aValues, T[] bValues, IEqualityComparer<T> comparer)
        {
            this.aValues = aValues ?? throw new ArgumentNullException(nameof(aValues));
            this.bValues = bValues ?? throw new ArgumentNullException(nameof(bValues));
            this.comparer = comparer;
            this.aRemoved = new bool[this.aValues.Length];
            this.bAdded = new bool[this.bValues.Length];

            int VMAX = (aValues.Length + bValues.Length) * 2;

            this.Vf = VArray<int>.CreateFromTo(-VMAX, VMAX);
            this.Vr = VArray<int>.CreateFromTo(-VMAX, VMAX);

            this.LCS(new IndexRange(0, aValues.Length), new IndexRange(0, bValues.Length));
        }

        private bool AreEqual(int aIndex, int bIndex)
        {
            if (this.comparer != null)
            {
                return this.comparer.Equals(this.aValues[aIndex], this.bValues[bIndex]);
            }

            return object.Equals(this.aValues[aIndex], this.bValues[bIndex]);
        }

        /// <summary>
        /// Gets the calculated diff result in the form of matched items/lines:
        /// ResultType: Specifies whether the line includes only an item from A, from B or from both sequences.
        /// AItem: The item from sequence A; this is the default value/null if resultType is <see cref="ResultType.B"/>.
        /// BItem: The item from sequence B, this is the default value/null if resultType is <see cref="ResultType.A"/>.
        /// </summary>
        /// <returns>An enumerable of diff lines containing one unmatched or two matched items.</returns>
        /// <remarks>This is this equal to calling <see cref="GetResult(ResultOrder)"/> with <see cref="ResultOrder.AABB"/>.</remarks>
        public IEnumerable<(ResultType ResultType, T AItem, T BItem)> GetResult()
        {
            return this.GetResult(ResultOrder.AABB);
        }

        /// <summary>
        /// Gets the calculated diff result in the form of matched items/lines:
        /// ResultType: Specifies whether the line includes only an item from A, from B or from both sequences.
        /// AItem: The item from sequence A; this is the default value/null if resultType is <see cref="ResultType.B"/>.
        /// BItem: The item from sequence B, this is the default value/null if resultType is <see cref="ResultType.A"/>.
        /// </summary>
        /// <param name="order">The order in which unmatched items/lines are returned.</param>
        /// <returns>An enumerable of diff lines containing one unmatched or two matched items.</returns>
        public IEnumerable<(ResultType ResultType, T AItem, T BItem)> GetResult(ResultOrder order)
        {
            ResultType GetPreferredNext(ResultType current)
            {
                if (current == ResultType.Both)
                {
                    // A first or B first
                    return (order == ResultOrder.ABAB || order == ResultOrder.AABB) ? ResultType.A : ResultType.B;
                }

                if (order == ResultOrder.AABB || order == ResultOrder.BBAA)
                {
                    // Section mode - keep same side.
                    return current;
                }
                else
                {
                    // Line mode - switch side.
                    return current == ResultType.A ? ResultType.B : ResultType.A;
                }
            }

            ResultType preferredNext = GetPreferredNext(ResultType.Both);
            int currentA = 0, currentB = 0;
            while (currentA < this.aRemoved.Length || currentB < this.bAdded.Length)
            {
                if (currentA < this.aRemoved.Length && currentB < this.bAdded.Length)
                {
                    if (!this.aRemoved[currentA] && !this.bAdded[currentB])
                    {
                        yield return (ResultType.Both, this.aValues[currentA], this.bValues[currentB]);
                        currentA++;
                        currentB++;
                        preferredNext = GetPreferredNext(ResultType.Both);
                    }
                    else if (preferredNext == ResultType.A)
                    {
                        if (this.aRemoved[currentA])
                        {
                            yield return (ResultType.A, this.aValues[currentA], default(T));
                            currentA++;
                            preferredNext = GetPreferredNext(ResultType.A);
                        }
                        else if (this.bAdded[currentB])
                        {
                            yield return (ResultType.B, default(T), this.bValues[currentB]);
                            currentB++;
                            preferredNext = GetPreferredNext(ResultType.B);
                        }
                    }
                    else // preferredNext == ResultType.B
                    {
                        if (this.bAdded[currentB])
                        {
                            yield return (ResultType.B, default(T), this.bValues[currentB]);
                            currentB++;
                            preferredNext = GetPreferredNext(ResultType.B);
                        }
                        else if (this.aRemoved[currentA])
                        {
                            yield return (ResultType.A, this.aValues[currentA], default(T));
                            currentA++;
                            preferredNext = GetPreferredNext(ResultType.A);
                        }
                    }
                }
                else if (currentA < this.aRemoved.Length)
                {
                    yield return (ResultType.A, this.aValues[currentA], default(T));
                    currentA++;
                }
                else
                {
                    yield return (ResultType.B, default(T), this.bValues[currentB]);
                    currentB++;
                }
            }
        }

        /// <summary>
        /// Gets the edit script that results from the comparison of the two sequences.
        /// Every item of the enumerable equals one edit instruction. Read this as follows:
        /// LineA, CountA: Starting at index LineA in sequence A, remove CountA items.
        /// LineB, CountB: Starting at index LineB in sequence B, insert CountB items.
        /// Line numbers start with 0 and correspond to the sequences that were put into the constructor.
        /// </summary>
        /// <returns>An enumerable of edit instructions.</returns>
        public IEnumerable<(int LineA, int LineB, int CountA, int CountB)> GetEditScript()
        {
            int currentA = 0, currentB = 0;
            int skippedA = 0, skippedB = 0;
            foreach ((ResultType result, T aItem, T bItem) in this.GetResult())
            {
                if (result == ResultType.Both)
                {
                    if (skippedA > 0 || skippedB > 0)
                    {
                        yield return (currentA - skippedA, currentB - skippedB, skippedA, skippedB);
                        skippedA = 0;
                        skippedB = 0;
                    }

                    currentA++;
                    currentB++;
                }
                else if (result == ResultType.A)
                {
                    currentA++;
                    skippedA++;
                }
                else
                {
                    currentB++;
                    skippedB++;
                }
            }

            if (skippedA > 0 || skippedB > 0)
            {
                yield return (currentA - skippedA, currentB - skippedB, skippedA, skippedB);
            }
        }

        private void LCS(IndexRange A, IndexRange B)
        {
            while (A.Length > 0 && B.Length > 0 && this.AreEqual(A[0], B[0]))
            {
                A = A.TrimStart(1);
                B = B.TrimStart(1);
            }

            while (A.Length > 0 && B.Length > 0 && this.AreEqual(A[^1], B[^1]))
            {
                A = A.TrimEnd(1);
                B = B.TrimEnd(1);
            }

            if (A.Length == 0)
            {
                foreach (int lineNumber in B)
                {
                    // Line was added
                    this.bAdded[lineNumber] = true;
                }
            }
            else if (B.Length == 0)
            {
                foreach (int lineNumber in A)
                {
                    // Line was removed
                    this.aRemoved[lineNumber] = true;
                }
            }
            else
            {
                (int x, int y) = this.SMS(A, B);
                this.LCS(A.Range(0, x), B.Range(0, y));
                this.LCS(A.Range(x), B.Range(y));
            }
        }

        private (int x, int y) SMS(IndexRange A, IndexRange B)
        {
            int N = A.Length;
            int M = B.Length;

            int VMAX = M + N + 3;
            int MAX = (int)Math.Ceiling((VMAX)/2.0);
            int delta = N-M;
            bool deltaIsEven = delta % 2 == 0;
            bool deltaIsOdd = !deltaIsEven;

            this.Vf[1] = 0;
            this.Vr[delta + 1] = N + 1;

            for (int D = 0; D <= MAX; D++)
            {
                for (int k = -D; k <= D; k += 2)
                {
                    int x, y;
                    if (k == -D || k != D && this.Vf[k-1] < this.Vf[k+1])
                    {
                        x = this.Vf[k+1];
                    }
                    else
                    {
                        x = this.Vf[k-1] + 1;
                    }

                    y = x - k;
                    while (x < N && y < M && this.AreEqual(A[x], B[y]))
                    {
                        x++;
                        y++;
                    }

                    this.Vf[k] = x;

                    if (deltaIsOdd && k >= delta - (D - 1) && k <= delta + D - 1 && this.Vf[k] >= this.Vr[k])
                    {
                        return (x, y);
                    }
                }

                for (int k = -D + delta; k <= D + delta; k += 2)
                {
                    int x, y;
                    if (k == -D + delta || k != D + delta && this.Vr[k-1] >= this.Vr[k+1])
                    {
                        x = this.Vr[k+1] - 1;
                    }
                    else
                    {
                        x = this.Vr[k - 1];
                    }

                    y = x - k;

                    while (x > 0 && y > 0 && this.AreEqual(A[x - 1], B[y - 1]))
                    {
                        x--;
                        y--;
                    }

                    this.Vr[k] = x;

                    if (deltaIsEven && k >= -D && k <= D && this.Vf[k] >= this.Vr[k])
                    {
                        return (x, y);
                    }
                }
            }

            throw new Exception($"Length of an SES is greater than {MAX}");
        }
    }
}
