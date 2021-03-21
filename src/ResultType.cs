// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

namespace spkl.Diffs
{
    /// <summary>
    /// Describes the contents of a result item.
    /// </summary>
    public enum ResultType : byte
    {
        /// <summary>
        /// The result only contains an item from sequence A.
        /// </summary>
        A,
        /// <summary>
        /// The result only contains an item from sequence B.
        /// </summary>
        B,
        /// <summary>
        /// The result contains an item from both sequence A and B.
        /// </summary>
        Both
    }
}
