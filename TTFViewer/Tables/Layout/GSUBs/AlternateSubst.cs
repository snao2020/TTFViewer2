// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(AlternateSubstFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("AlternateSubst")]
    class AlternateSubst
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("AlternateSubst")]
    class AlternateSubstFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of substitution subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Number of AlternateSet tables")]
        public uint16 alternateSetCount;

        [Count(0, FieldValueKind.Path, "alternateSetCount")]
        [TableType(typeof(AlternateSet))]
        [Description(0, "Array of offsets to AlternateSet tables.Offsets are from beginning of substitution subtable, ordered by Coverage index")]
        public IList<Offset16> alternateSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class AlternateSet
    {
        [Description(0, "Number of glyph IDs in the alternateGlyphIDs array")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "Array of alternate glyph IDs, in arbitrary order")]
        public IList<uint16> alternateGlyphIDs;
    }

#pragma warning restore IDE1006
}
