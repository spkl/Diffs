namespace spkl.Diffs.Test
{
    public partial class MyersDiffClass
    {
        public class ReferenceCase
        {
            public string[] A { get; set; }

            public string AString
            {
                set
                {
                    this.A = value.Split(',');
                }
            }

            public string[] B { get; set; }

            public string BString
            {
                set
                {
                    this.B = value.Split(',');
                }
            }

            public (ResultType ResultType, string AItem, string BItem)[] Result { get; set; }

            public (int LineA, int LineB, int CountA, int CountB)[] EditScript { get; set; }

            public override string ToString()
            {
                return $"{string.Join(",", this.A)} / {string.Join(",", this.B)}";
            }
        }
    }
}