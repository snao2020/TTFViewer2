// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(MultipleSubstFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("MultipleSubst")]
    class MultipleSubst
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("MultipleSubst")]
    class MultipleSubstFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of substitution subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Number of Sequence table offsets in the sequenceOffsets array")]
        public uint16 sequenceCount;

        [TableType(typeof(Sequence))]
        [Count(0, FieldValueKind.Path, "sequenceCount")]
        [Description(0, "Array of offsets to Sequence tables.Offsets are from beginning of substitution subtable, ordered by Coverage index")]
        public IList<Offset16> sequenceOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Sequence")]
    class Sequence
    {
        [Description(0, "Number of glyph IDs in the substituteGlyphIDs array.This must always be greater than 0")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "String of glyph IDs to substitute")]
        public uint16[] substituteGlyphIDs;
    }
#pragma warning restore IDE1006
}
