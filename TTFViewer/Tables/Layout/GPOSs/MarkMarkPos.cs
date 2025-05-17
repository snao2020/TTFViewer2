// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(MarkMarkPosFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("MarkMarkPos")]
    class MarkMarkPos
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("MarkMarkPos")]
    class MarkMarkPosFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Combining Mark Coverage table, from beginning of MarkMarkPos subtable")]
        public Offset16 mark1CoverageOffset;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Base Mark Coverage table, from beginning of MarkMarkPos subtable")]
        public Offset16 mark2CoverageOffset;

        [Description(0, "Number of Combining Mark classes defined")]
        public uint16 markClassCount;

        [TableType(typeof(MarkArray))]
        [Description(0, "Offset to MarkArray table for mark1, from beginning of MarkMarkPos subtable")]
        public Offset16 mark1ArrayOffset;

        [TableType(typeof(Mark2Array))]
        [Description(0, "")]
        public Offset16 mark2ArrayOffset; // Offset to Mark2Array table for mark2, from beginning of MarkMarkPos subtable.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Mark2Array
    {
        [Description(0, "Number of Mark2 records")]
        public uint16 mark2Count;

        [Count(0, FieldValueKind.Path, "mark2Count")]
        [Description(0, "Array of Mark2Records, in Coverage order")]
        public IList<Mark2> mark2Records;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Mark2
    {
        [Count(0, FieldValueKind.OffsetSource, "\\markClassCount")]
        [TableType(typeof(Anchor))]
        [Description(0, "Array of offsets(one per class) to Anchor tables.Offsets are from beginning of Mark2Array table, in class order")]
        public IList<Offset16> mark2AnchorOffsets;
    }
#pragma warning restore IDE1006
}
