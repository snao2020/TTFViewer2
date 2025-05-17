// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(LigatureSubstFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("LigatureSubst")]
    class LigatureSubst
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("LigatureSubst")]
    class LigatureSubstFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of substitution subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Number of LigatureSet tables")]
        public uint16 ligatureSetCount;

        [TableType(typeof(LigatureSet))]
        [Count(0, FieldValueKind.Path, "ligatureSetCount")]
        [Description(0, "Array of offsets to LigatureSet tables.Offsets are from beginning of substitution subtable, ordered by Coverage index")]
        public IList<Offset16> ligatureSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LigatureSet
    {
        [Description(0, "Number of Ligature tables")]
        public uint16 ligatureCount;

        [Count(0, FieldValueKind.Path,"ligatureCount")]
        [TableType(typeof(Ligature))]
        [Description(0, "Array of offsets to Ligature tables.Offsets are from beginning of LigatureSet table, ordered by preference")]
        public IList<Offset16> ligatureOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Ligature
    {
        [Description(0, "glyph ID of ligature to substitute")]
        public uint16 ligatureGlyph;

        [Description(0, "Number of components in the ligature")]
        public uint16 componentCount;

        [Count(0, FieldValueKind.Path, "componentCount", "Sub:1")]
        [Description(0, "Array of component glyph IDs — start with the second component, ordered in writing direction")]
        public uint16[] componentGlyphIDs;
    }
#pragma warning restore IDE1006
}
