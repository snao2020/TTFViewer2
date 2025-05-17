// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(SinglePosFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(SinglePosFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [Invalid]
    [BaseName("SinglePos")]
    class SinglePos
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }

    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SinglePos")]
    class SinglePosFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of SinglePos subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Defines the types of data in the ValueRecord")]
        public uint16 valueFormat;

        [Description(0, "Defines positioning value(s) — applied to all glyphs in the Coverage table")]
        public ValueRecord valueRecord;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SinglePos")]
    class SinglePosFormat2
    {
        [Description(0, "Format identifier: format = 2")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of SinglePos subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Defines the types of data in the ValueRecords")]
        public uint16 valueFormat;

        [Description(0, "Number of ValueRecords — must equal glyphCount in the Coverage table")]
        public uint16 valueCount;
        
        [Count(0, FieldValueKind.Path, "valueCount")]
        [Description(0, "Array of ValueRecords — positioning values applied to glyphs")]
        public IList<ValueRecord> valueRecords;
    }
#pragma warning restore IDE1006
}
