using NUnit.Framework;
using System;
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
    }
}
