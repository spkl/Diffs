// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace spkl.Diffs.Test
{
    public partial class MyersDiffClass
    {
        [Test]
        public void InputValidation()
        {
            Assert.That(() => new MyersDiff<string>(null, new string[] { "" }), Throws.ArgumentNullException);
            Assert.That(() => new MyersDiff<string>(new string[] { "" }, null), Throws.ArgumentNullException);
            Assert.That(() => new MyersDiff<string>(new string[0], new string[0]), Throws.Nothing);
        }

        [Test]
        public void CustomComparer()
        {
            string[] a = new[] { "a", "b", "c", "a", "b", "b", "a" };
            string[] b = new[] { "C", "B", "A", "B", "A", "C" };

            MyersDiff<string> diff = new MyersDiff<string>(a, b, StringComparer.OrdinalIgnoreCase);

            (ResultType, string, string)[] result = new[]
            {
                (ResultType.A, "a", null),
                (ResultType.B, null, "C"),
                (ResultType.Both, "b", "B"),
                (ResultType.A, "c", null),
                (ResultType.Both, "a", "A"),
                (ResultType.Both, "b", "B"),
                (ResultType.A, "b", null),
                (ResultType.Both, "a", "A"),
                (ResultType.B, null, "C"),
            };

            (int, int, int, int)[] editScript = new[]
            {
                    (0, 0, 1, 1),
                    (2, 2, 1, 0),
                    (5, 4, 1, 0),
                    (7, 5, 0, 1)
            };

            Assert.Multiple(() =>
            {
                Assert.That(diff.GetResult().ToArray(), Is.EqualTo(result));
                Assert.That(diff.GetEditScript().ToArray(), Is.EqualTo(editScript));
            });
        }

        [Test]
        [TestCaseSource(nameof(ReferenceCases))]
        public void ReferenceResult(ReferenceCase testCase)
        {
            Assert.That(new MyersDiff<string>(testCase.A, testCase.B).GetResult().ToArray(), Is.EqualTo(testCase.ResultAABB), "Default order");
            Assert.That(new MyersDiff<string>(testCase.A, testCase.B).GetResult(ResultOrder.AABB).ToArray(), Is.EqualTo(testCase.ResultAABB), "AABB");
            Assert.That(new MyersDiff<string>(testCase.A, testCase.B).GetResult(ResultOrder.BBAA).ToArray(), Is.EqualTo(testCase.ResultBBAA), "BBAA");
            Assert.That(new MyersDiff<string>(testCase.A, testCase.B).GetResult(ResultOrder.ABAB).ToArray(), Is.EqualTo(testCase.ResultABAB), "ABAB");
            Assert.That(new MyersDiff<string>(testCase.A, testCase.B).GetResult(ResultOrder.BABA).ToArray(), Is.EqualTo(testCase.ResultBABA), "BABA");
        }

        [Test]
        [TestCaseSource(nameof(ReferenceCases))]
        public void ReferenceEditScript(ReferenceCase testCase)
        {
            Assert.That(new MyersDiff<string>(testCase.A, testCase.B).GetEditScript().ToArray(), Is.EqualTo(testCase.EditScript));
        }

        [Test]
        [TestCase("D54ACE32535F00233F43B243D0EF1EDE.mus", "Smooth_Operator.mus")]
        [TestCase("Smooth_Operator.mus", "D54ACE32535F00233F43B243D0EF1EDE.mus")]
        public void ApplyDiffResult(string fileA, string fileB)
        {
            byte[] a = File.ReadAllBytes(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", fileA));
            byte[] b = File.ReadAllBytes(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", fileB));

            MyersDiff<byte> d = new MyersDiff<byte>(a, b);
            List<byte> result1 = ApplyEditScript(a, b, d);
            List<byte> result2 = ApplyResults(d);

            Assert.That(result1, Is.EqualTo(b), "Result constructed from edit script must match original file");
            Assert.That(result2, Is.EqualTo(b), "Result constructed from result item list must match original file");
        }

        private static List<byte> ApplyEditScript(byte[] a, byte[] b, MyersDiff<byte> d)
        {
            List<byte> result = new List<byte>();
            Queue<(int LineA, int LineB, int CountA, int CountB)> editScript = new Queue<(int, int, int, int)>(d.GetEditScript());
            int currentLine = 0;
            while (currentLine < a.Length)
            {
                if (editScript.Count > 0 && editScript.Peek().LineA == currentLine)
                {
                    (int lineA, int lineB, int countA, int countB) = editScript.Dequeue();
                    for (int i = 0; i < countB; i++)
                    {
                        result.Add(b[lineB + i]);
                    }

                    if (countA == 0)
                    {
                        result.Add(a[currentLine]);
                        currentLine++;
                    }
                    else
                    {
                        currentLine = lineA + countA;
                    }
                }
                else
                {
                    result.Add(a[currentLine]);
                    currentLine++;
                }
            }

            return result;
        }

        private static List<byte> ApplyResults(MyersDiff<byte> d)
        {
            List<byte> result = new List<byte>();
            foreach ((ResultType resultType, byte aItem, byte bItem) in d.GetResult())
            {
                switch (resultType)
                {
                    case ResultType.A:
                        break;
                    case ResultType.B:
                        result.Add(bItem);
                        break;
                    case ResultType.Both:
                        Assert.That(aItem, Is.EqualTo(bItem), "When ResultType is Both, the AItem and BItem must be equal");
                        result.Add(aItem);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
