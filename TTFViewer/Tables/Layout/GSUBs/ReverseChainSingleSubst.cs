// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(ReverseChainSingleSubstFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("ReverseChainSingleSubst")]
    class ReverseChainSingleSubst
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ReverseChainSingleSubst")]
    class ReverseChainSingleSubstFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of substitution subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Number of glyphs in the backtrack sequence")]
        public uint16 backtrackGlyphCount;

        [TableType(typeof(Coverage))]
        [Count(0, FieldValueKind.Path, "backtrackGlyphCount")]
        [Description(0, "Array of offsets to coverage tables in backtrack sequence, in glyph sequence order")]
        public Offset16[] backtrackCoverageOffsets;

        [Description(0, "Number of glyphs in lookahead sequence")]
        public uint16 lookaheadGlyphCount;

        [Count(0, FieldValueKind.Path, "lookaheadGlyphCount")]
        [TableType(typeof(Coverage))]
        [Description(0, "Array of offsets to coverage tables in lookahead sequence, in glyph sequence order")]
        public Offset16[] lookaheadCoverageOffsets;

        [Description(0, "Number of glyph IDs in the substituteGlyphIDs array")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "Array of substitute glyph IDs — ordered by Coverage index")]
        public uint16[] substituteGlyphIDs;
    }
#pragma warning restore IDE1006
}
