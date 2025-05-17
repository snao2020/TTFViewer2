//ver1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(SingleSubstFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(SingleSubstFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [Invalid]
    [BaseName("SingleSubst")]
    class SingleSubst
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SingleSubst")]
    class SingleSubstFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of substitution subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Add to original glyph ID to get substitute glyph ID")]
        public int16 deltaGlyphID;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SingleSubst")]
    class SingleSubstFormat2
    {
        [Description(0, "Format identifier: format = 2")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of substitution subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Number of glyph IDs in the substituteGlyphIDs array")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "Array of substitute glyph IDs — ordered by Coverage index")]
        public IList<uint16> substituteGlyphIDs;
    }
#pragma warning restore IDE1006
}
