// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(CoverageFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(CoverageFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [Invalid]
    class Coverage
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("Coverage")]
    class CoverageFormat1
    {
        [Description(0, "Format identifier — format = 1")]
        public uint16 format;

        [Description(0, "Number of glyphs in the glyph array")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "Array of glyph IDs — in numerical order")]
        public IList<uint16> glyphArray;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("Coverage")]
    class CoverageFormat2
    {
        [Description(0, "Format identifier — format = 2")]
        public uint16 format;

        [Description(0, "Number of RangeRecords")]
        public uint16 rangeCount;

        [Count(0, FieldValueKind.Path, "rangeCount")]
        [Description(0, "Array of glyph ranges — ordered by startGlyphID")]
        public RangeRecord[] rangeRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class RangeRecord
    {
        [Description(0, "First glyph ID in the range")]
        public uint16 startGlyphID;

        [Description(0, "Last glyph ID in the range")]
        public uint16 endGlyphID;

        [Description(0, "Coverage Index of first glyph ID in range")]
        public uint16 startCoverageIndex;
    }
#pragma warning restore IDE1006
}
