// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(PairPosFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(PairPosFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [Invalid]
    [BaseName("PairPos")]
    class PairPos
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("PairPos")]
    class PairPosFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of PairPos subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Defines the types of data in valueRecord1 — for the first glyph in the pair (may be zero)")]
        public uint16 valueFormat1;

        [Description(0, "Defines the types of data in valueRecord2 — for the second glyph in the pair(may be zero)")]
        public uint16 valueFormat2;

        [Description(0, "Number of PairSet tables")]
        public uint16 pairSetCount;

        [Count(0, FieldValueKind.Path, "pairSetCount")]
        [TableType(typeof(PairSet))]
        [Description(0, "Array of offsets to PairSet tables.Offsets are from beginning of PairPos subtable, ordered by Coverage Index")]
        public IList<Offset16> pairSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class PairSet
    {
        [Description(0, "Number of PairValueRecords")]
        public uint16 pairValueCount;

        [Count(0, FieldValueKind.Path, "pairValueCount")]
        [Description(0, "Array of PairValueRecords, ordered by glyph ID of the second glyph")]
        public IList<PairValue> pairValueRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class PairValue
    {
        [Description(0, "Glyph ID of second glyph in the pair(first glyph is listed in the Coverage table)")]
        public uint16 secondGlyph;

        [Description(0, "Positioning data for the first glyph in the pair")]
        public ValueRecord valueRecord1;

        [Description(0, "Positioning data for the second glyph in the pair")]
        public ValueRecord valueRecord2;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("PairPos")]
    class PairPosFormat2
    {
        [Description(0, "Format identifier: format = 2")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of PairPos subtable")]
        public Offset16 coverageOffset;

        [Description(0, "ValueRecord definition — for the first glyph of the pair (may be zero)")]
        public uint16 valueFormat1;

        [Description(0, "ValueRecord definition — for the second glyph of the pair(may be zero)")]
        public uint16 valueFormat2;

        [TableType(typeof(ClassDef))]
        [Description(0, "Offset to ClassDef table, from beginning of PairPos subtable — for the first glyph of the pair")]
        public Offset16 classDef1Offset;

        [TableType(typeof(ClassDef))]
        [Description(0, "Offset to ClassDef table, from beginning of PairPos subtable — for the second glyph of the pair")]
        public Offset16 classDef2Offset;

        [Description(0, "Number of classes in classDef1 table — includes Class 0")]
        public uint16 class1Count;

        [Description(0, "Number of classes in classDef2 table — includes Class 0")]
        public uint16 class2Count;

        [Count(0, FieldValueKind.Path, "class1Count")]
        [Description(0, "Array of Class1 records, ordered by classes in classDef1")]
        public IList<Class1> class1Records;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Class1
    {
        [Count(0, FieldValueKind.Path, "\\class2Count")]
        [Description(0, "Array of Class2 records, ordered by classes in classDef2")]
        public IList<Class2> class2Records;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Class2
    {
        [Description(0, "Positioning for first glyph — empty if valueFormat1 = 0")]
        public ValueRecord valueRecord1;

        [Description(0, "Positioning for second glyph — empty if valueFormat2 = 0")]
        public ValueRecord valueRecord2;
    }
#pragma warning restore IDE1006
}
