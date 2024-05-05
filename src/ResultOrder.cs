// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

namespace spkl.Diffs;

/// <summary>
/// Determines in which order the <see cref="MyersDiff{T}.GetResult(ResultOrder)"/> method returns items.
/// </summary>
public enum ResultOrder
{
    /// <summary>
    /// Items are ordered in alternating sections, first A, then B.
    /// </summary>
    AABB,
    /// <summary>
    /// Items are ordered in alternating sections, first B, then A.
    /// </summary>
    BBAA,
    /// <summary>
    /// Items are ordered in alternating lines, first A, then B.
    /// </summary>
    ABAB,
    /// <summary>
    /// Items are ordered in alternating lines, first B, then A.
    /// </summary>
    BABA
}
