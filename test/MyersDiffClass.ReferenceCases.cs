﻿// Copyright (c) Sebastian Fischer. All Rights Reserved.
// Licensed under the MIT License.

namespace spkl.Diffs.Test;

public partial class MyersDiffClass
{
    private static readonly ReferenceCase[] ReferenceCases =
    [
        new ReferenceCase
        {
            AString = "a,b,c,a,b,b,a",
            BString = "c,b,a,b,a,c",
            ResultAABB =
            [
                (ResultType.A, "a", null),
                (ResultType.B, null, "c"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "c", null),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "b", null),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "c"),
            ],
            ResultBBAA =
            [
                (ResultType.B, null, "c"),
                (ResultType.A, "a", null),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "c", null),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "b", null),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "c"),
            ],
            ResultABAB =
            [
                (ResultType.A, "a", null),
                (ResultType.B, null, "c"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "c", null),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "b", null),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "c"),
            ],
            ResultBABA =
            [
                (ResultType.B, null, "c"),
                (ResultType.A, "a", null),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "c", null),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "b", null),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "c"),
            ],
            EditScript =
            [
                (0, 0, 1, 1),
                (2, 2, 1, 0),
                (5, 4, 1, 0),
                (7, 5, 0, 1)
            ]
        },
        new ReferenceCase
        {
            AString = "a,b,c,d,e,f,g,h,i,j,k,l",
            BString = "0,1,2,3,4,5,6,7,8,9",
            ResultAABB =
            [
                (ResultType.A, "a", null),
                (ResultType.A, "b", null),
                (ResultType.A, "c", null),
                (ResultType.A, "d", null),
                (ResultType.A, "e", null),
                (ResultType.A, "f", null),
                (ResultType.A, "g", null),
                (ResultType.A, "h", null),
                (ResultType.A, "i", null),
                (ResultType.A, "j", null),
                (ResultType.A, "k", null),
                (ResultType.A, "l", null),
                (ResultType.B, null, "0"),
                (ResultType.B, null, "1"),
                (ResultType.B, null, "2"),
                (ResultType.B, null, "3"),
                (ResultType.B, null, "4"),
                (ResultType.B, null, "5"),
                (ResultType.B, null, "6"),
                (ResultType.B, null, "7"),
                (ResultType.B, null, "8"),
                (ResultType.B, null, "9"),
            ],
            ResultBBAA =
            [
                (ResultType.B, null, "0"),
                (ResultType.B, null, "1"),
                (ResultType.B, null, "2"),
                (ResultType.B, null, "3"),
                (ResultType.B, null, "4"),
                (ResultType.B, null, "5"),
                (ResultType.B, null, "6"),
                (ResultType.B, null, "7"),
                (ResultType.B, null, "8"),
                (ResultType.B, null, "9"),
                (ResultType.A, "a", null),
                (ResultType.A, "b", null),
                (ResultType.A, "c", null),
                (ResultType.A, "d", null),
                (ResultType.A, "e", null),
                (ResultType.A, "f", null),
                (ResultType.A, "g", null),
                (ResultType.A, "h", null),
                (ResultType.A, "i", null),
                (ResultType.A, "j", null),
                (ResultType.A, "k", null),
                (ResultType.A, "l", null),
            ],
            ResultABAB =
            [
                (ResultType.A, "a", null),
                (ResultType.B, null, "0"),
                (ResultType.A, "b", null),
                (ResultType.B, null, "1"),
                (ResultType.A, "c", null),
                (ResultType.B, null, "2"),
                (ResultType.A, "d", null),
                (ResultType.B, null, "3"),
                (ResultType.A, "e", null),
                (ResultType.B, null, "4"),
                (ResultType.A, "f", null),
                (ResultType.B, null, "5"),
                (ResultType.A, "g", null),
                (ResultType.B, null, "6"),
                (ResultType.A, "h", null),
                (ResultType.B, null, "7"),
                (ResultType.A, "i", null),
                (ResultType.B, null, "8"),
                (ResultType.A, "j", null),
                (ResultType.B, null, "9"),
                (ResultType.A, "k", null),
                (ResultType.A, "l", null),
            ],
            ResultBABA =
            [
                (ResultType.B, null, "0"),
                (ResultType.A, "a", null),
                (ResultType.B, null, "1"),
                (ResultType.A, "b", null),
                (ResultType.B, null, "2"),
                (ResultType.A, "c", null),
                (ResultType.B, null, "3"),
                (ResultType.A, "d", null),
                (ResultType.B, null, "4"),
                (ResultType.A, "e", null),
                (ResultType.B, null, "5"),
                (ResultType.A, "f", null),
                (ResultType.B, null, "6"),
                (ResultType.A, "g", null),
                (ResultType.B, null, "7"),
                (ResultType.A, "h", null),
                (ResultType.B, null, "8"),
                (ResultType.A, "i", null),
                (ResultType.B, null, "9"),
                (ResultType.A, "j", null),
                (ResultType.A, "k", null),
                (ResultType.A, "l", null),
            ],
            EditScript =
            [
                (0, 0, 12, 10)
            ]
        },
        new ReferenceCase
        {
            AString = "0,1,2,3,4,5,6,7,8,9",
            BString = "a,b,c,d,e,f,g,h,i,j,k,l",
            ResultAABB =
            [
                (ResultType.A, "0", null),
                (ResultType.A, "1", null),
                (ResultType.A, "2", null),
                (ResultType.A, "3", null),
                (ResultType.A, "4", null),
                (ResultType.A, "5", null),
                (ResultType.A, "6", null),
                (ResultType.A, "7", null),
                (ResultType.A, "8", null),
                (ResultType.A, "9", null),
                (ResultType.B, null, "a"),
                (ResultType.B, null, "b"),
                (ResultType.B, null, "c"),
                (ResultType.B, null, "d"),
                (ResultType.B, null, "e"),
                (ResultType.B, null, "f"),
                (ResultType.B, null, "g"),
                (ResultType.B, null, "h"),
                (ResultType.B, null, "i"),
                (ResultType.B, null, "j"),
                (ResultType.B, null, "k"),
                (ResultType.B, null, "l"),
            ],
            ResultBBAA =
            [
                (ResultType.B, null, "a"),
                (ResultType.B, null, "b"),
                (ResultType.B, null, "c"),
                (ResultType.B, null, "d"),
                (ResultType.B, null, "e"),
                (ResultType.B, null, "f"),
                (ResultType.B, null, "g"),
                (ResultType.B, null, "h"),
                (ResultType.B, null, "i"),
                (ResultType.B, null, "j"),
                (ResultType.B, null, "k"),
                (ResultType.B, null, "l"),
                (ResultType.A, "0", null),
                (ResultType.A, "1", null),
                (ResultType.A, "2", null),
                (ResultType.A, "3", null),
                (ResultType.A, "4", null),
                (ResultType.A, "5", null),
                (ResultType.A, "6", null),
                (ResultType.A, "7", null),
                (ResultType.A, "8", null),
                (ResultType.A, "9", null),
            ],
            ResultABAB =
            [
                (ResultType.A, "0", null),
                (ResultType.B, null, "a"),
                (ResultType.A, "1", null),
                (ResultType.B, null, "b"),
                (ResultType.A, "2", null),
                (ResultType.B, null, "c"),
                (ResultType.A, "3", null),
                (ResultType.B, null, "d"),
                (ResultType.A, "4", null),
                (ResultType.B, null, "e"),
                (ResultType.A, "5", null),
                (ResultType.B, null, "f"),
                (ResultType.A, "6", null),
                (ResultType.B, null, "g"),
                (ResultType.A, "7", null),
                (ResultType.B, null, "h"),
                (ResultType.A, "8", null),
                (ResultType.B, null, "i"),
                (ResultType.A, "9", null),
                (ResultType.B, null, "j"),
                (ResultType.B, null, "k"),
                (ResultType.B, null, "l"),
            ],
            ResultBABA =
            [
                (ResultType.B, null, "a"),
                (ResultType.A, "0", null),
                (ResultType.B, null, "b"),
                (ResultType.A, "1", null),
                (ResultType.B, null, "c"),
                (ResultType.A, "2", null),
                (ResultType.B, null, "d"),
                (ResultType.A, "3", null),
                (ResultType.B, null, "e"),
                (ResultType.A, "4", null),
                (ResultType.B, null, "f"),
                (ResultType.A, "5", null),
                (ResultType.B, null, "g"),
                (ResultType.A, "6", null),
                (ResultType.B, null, "h"),
                (ResultType.A, "7", null),
                (ResultType.B, null, "i"),
                (ResultType.A, "8", null),
                (ResultType.B, null, "j"),
                (ResultType.A, "9", null),
                (ResultType.B, null, "k"),
                (ResultType.B, null, "l"),
            ],
            EditScript =
            [
                (0, 0, 10, 12)
            ]
        },
        new ReferenceCase
        {
            AString = "a,b,c,d,e,f,g,h,i,j,k,l",
            BString = "a,b,c,d,e,f,g,h,i,j,k,l",
            ResultAABB =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.Both, "k", "k"),
                (ResultType.Both, "l", "l"),
            ],
            ResultBBAA =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.Both, "k", "k"),
                (ResultType.Both, "l", "l"),
            ],
            ResultABAB =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.Both, "k", "k"),
                (ResultType.Both, "l", "l"),
            ],
            ResultBABA =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.Both, "k", "k"),
                (ResultType.Both, "l", "l"),
            ],
            EditScript = System.Array.Empty<(int, int, int, int)>()
        },
        new ReferenceCase
        {
            AString = "a,b,c,d,e,f",
            BString = "b,c,d,e,f,x",
            ResultAABB =
            [
                (ResultType.A, "a", null),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.B, null, "x")
            ],
            ResultBBAA =
            [
                (ResultType.A, "a", null),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.B, null, "x")
            ],
            ResultABAB =
            [
                (ResultType.A, "a", null),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.B, null, "x")
            ],
            ResultBABA =
            [
                (ResultType.A, "a", null),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.B, null, "x")
            ],
            EditScript =
            [
                (0, 0, 1, 0),
                (6, 5, 0, 1)
            ]
        },
        new ReferenceCase
        {
            AString = "b,c,d,e,f,x",
            BString = "a,b,c,d,e,f",
            ResultAABB =
            [
                (ResultType.B, null, "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.A, "x", null)
            ],
            ResultBBAA =
            [
                (ResultType.B, null, "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.A, "x", null)
            ],
            ResultABAB =
            [
                (ResultType.B, null, "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.A, "x", null)
            ],
            ResultBABA =
            [
                (ResultType.B, null, "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.Both, "f", "f"),
                (ResultType.A, "x", null)
            ],
            EditScript =
            [
                (0, 0, 0, 1),
                (5, 6, 1, 0)
            ]
        },
        new ReferenceCase
        {
            AString = "c1,a,c2,b,c,d,e,g,h,i,j,c3,k,l",
            BString = "C1,a,C2,b,c,d,e,I1,e,g,h,i,j,C3,k,I2,l",
            ResultAABB =
            [
                (ResultType.A, "c1", null),
                (ResultType.B, null, "C1"),
                (ResultType.Both, "a", "a"),
                (ResultType.A, "c2", null),
                (ResultType.B, null, "C2"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.B, null, "I1"),
                (ResultType.B, null, "e"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.A, "c3", null),
                (ResultType.B, null, "C3"),
                (ResultType.Both, "k", "k"),
                (ResultType.B, null, "I2"),
                (ResultType.Both, "l", "l"),
            ],
            ResultBBAA =
            [
                (ResultType.B, null, "C1"),
                (ResultType.A, "c1", null),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "C2"),
                (ResultType.A, "c2", null),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.B, null, "I1"),
                (ResultType.B, null, "e"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.B, null, "C3"),
                (ResultType.A, "c3", null),
                (ResultType.Both, "k", "k"),
                (ResultType.B, null, "I2"),
                (ResultType.Both, "l", "l"),
            ],
            ResultABAB =
            [
                (ResultType.A, "c1", null),
                (ResultType.B, null, "C1"),
                (ResultType.Both, "a", "a"),
                (ResultType.A, "c2", null),
                (ResultType.B, null, "C2"),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.B, null, "I1"),
                (ResultType.B, null, "e"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.A, "c3", null),
                (ResultType.B, null, "C3"),
                (ResultType.Both, "k", "k"),
                (ResultType.B, null, "I2"),
                (ResultType.Both, "l", "l"),
            ],
            ResultBABA =
            [
                (ResultType.B, null, "C1"),
                (ResultType.A, "c1", null),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "C2"),
                (ResultType.A, "c2", null),
                (ResultType.Both, "b", "b"),
                (ResultType.Both, "c", "c"),
                (ResultType.Both, "d", "d"),
                (ResultType.Both, "e", "e"),
                (ResultType.B, null, "I1"),
                (ResultType.B, null, "e"),
                (ResultType.Both, "g", "g"),
                (ResultType.Both, "h", "h"),
                (ResultType.Both, "i", "i"),
                (ResultType.Both, "j", "j"),
                (ResultType.B, null, "C3"),
                (ResultType.A, "c3", null),
                (ResultType.Both, "k", "k"),
                (ResultType.B, null, "I2"),
                (ResultType.Both, "l", "l"),
            ],
            EditScript =
            [
                (0, 0, 1, 1),
                (2, 2, 1, 1),
                (7, 7, 0, 2),
                (11, 13, 1, 1),
                (13, 15, 0, 1)
            ]
        },
        new ReferenceCase
        {
            AString = "F",
            BString = "0,F,1,2,3,4,5,6,7",
            ResultAABB =
            [
                (ResultType.B, null, "0"),
                (ResultType.Both, "F", "F"),
                (ResultType.B, null, "1"),
                (ResultType.B, null, "2"),
                (ResultType.B, null, "3"),
                (ResultType.B, null, "4"),
                (ResultType.B, null, "5"),
                (ResultType.B, null, "6"),
                (ResultType.B, null, "7"),
            ],
            ResultBBAA =
            [
                (ResultType.B, null, "0"),
                (ResultType.Both, "F", "F"),
                (ResultType.B, null, "1"),
                (ResultType.B, null, "2"),
                (ResultType.B, null, "3"),
                (ResultType.B, null, "4"),
                (ResultType.B, null, "5"),
                (ResultType.B, null, "6"),
                (ResultType.B, null, "7"),
            ],
            ResultABAB =
            [
                (ResultType.B, null, "0"),
                (ResultType.Both, "F", "F"),
                (ResultType.B, null, "1"),
                (ResultType.B, null, "2"),
                (ResultType.B, null, "3"),
                (ResultType.B, null, "4"),
                (ResultType.B, null, "5"),
                (ResultType.B, null, "6"),
                (ResultType.B, null, "7"),
            ],
            ResultBABA =
            [
                (ResultType.B, null, "0"),
                (ResultType.Both, "F", "F"),
                (ResultType.B, null, "1"),
                (ResultType.B, null, "2"),
                (ResultType.B, null, "3"),
                (ResultType.B, null, "4"),
                (ResultType.B, null, "5"),
                (ResultType.B, null, "6"),
                (ResultType.B, null, "7"),
            ],
            EditScript =
            [
                (0, 0, 0, 1),
                (1, 2, 0, 7)
            ]
        },
        new ReferenceCase
        {
            AString = "HELLO,WORLD",
            BString = ",,hello,,,,world,",
            ResultAABB =
            [
                (ResultType.A, "HELLO", null),
                (ResultType.A, "WORLD", null),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, "hello"),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, "world"),
                (ResultType.B, null, ""),
            ],
            ResultBBAA =
            [
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, "hello"),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, "world"),
                (ResultType.B, null, ""),
                (ResultType.A, "HELLO", null),
                (ResultType.A, "WORLD", null),
            ],
            ResultABAB =
            [
                (ResultType.A, "HELLO", null),
                (ResultType.B, null, ""),
                (ResultType.A, "WORLD", null),
                (ResultType.B, null, ""),
                (ResultType.B, null, "hello"),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, "world"),
                (ResultType.B, null, ""),
            ],
            ResultBABA =
            [
                (ResultType.B, null, ""),
                (ResultType.A, "HELLO", null),
                (ResultType.B, null, ""),
                (ResultType.A, "WORLD", null),
                (ResultType.B, null, "hello"),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, ""),
                (ResultType.B, null, "world"),
                (ResultType.B, null, ""),
            ],
            EditScript =
            [
                (0, 0, 2, 8),
            ]
        },
        new ReferenceCase
        {
            AString = "a,b,-,c,d,e,f,f",
            BString = "a,b,x,c,e,f",
            ResultAABB =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "-", null),
                (ResultType.B, null, "x"),
                (ResultType.Both, "c", "c"),
                (ResultType.A, "d", null),
                (ResultType.Both, "e", "e"),
                (ResultType.A, "f", null), // (Both, f, f) would also be valid
                (ResultType.Both, "f", "f"), // (A, f, null) would also be valid
            ],
            ResultBBAA =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.B, null, "x"),
                (ResultType.A, "-", null),
                (ResultType.Both, "c", "c"),
                (ResultType.A, "d", null),
                (ResultType.Both, "e", "e"),
                (ResultType.A, "f", null), // (Both, f, f) would also be valid
                (ResultType.Both, "f", "f"), // (A, f, null) would also be valid
            ],
            ResultABAB =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.A, "-", null),
                (ResultType.B, null, "x"),
                (ResultType.Both, "c", "c"),
                (ResultType.A, "d", null),
                (ResultType.Both, "e", "e"),
                (ResultType.A, "f", null), // (Both, f, f) would also be valid
                (ResultType.Both, "f", "f"), // (A, f, null) would also be valid
            ],
            ResultBABA =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "b", "b"),
                (ResultType.B, null, "x"),
                (ResultType.A, "-", null),
                (ResultType.Both, "c", "c"),
                (ResultType.A, "d", null),
                (ResultType.Both, "e", "e"),
                (ResultType.A, "f", null), // (Both, f, f) would also be valid
                (ResultType.Both, "f", "f"), // (A, f, null) would also be valid
            ],
            EditScript =
            [
                (2, 2, 1, 1),
                (4, 4, 1, 0),
                (6, 5, 1, 0) // (7, 6, 1, 0) would also be valid
            ]
        },
        new ReferenceCase
        {
            AString = "a,a,a,a,a,a,a,a,a,a",
            BString = "a,a,a,a,-,a,a,a,a,a",
            ResultAABB =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.A, "a", null),
                (ResultType.B, null, "-"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
            ],
            ResultBBAA =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "-"),
                (ResultType.A, "a", null),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
            ],
            ResultABAB =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.A, "a", null),
                (ResultType.B, null, "-"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
            ],
            ResultBABA =
            [
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.B, null, "-"),
                (ResultType.A, "a", null),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
                (ResultType.Both, "a", "a"),
            ],
            EditScript =
            [
                (4, 4, 1, 1),
            ]
        },
    ];
}
