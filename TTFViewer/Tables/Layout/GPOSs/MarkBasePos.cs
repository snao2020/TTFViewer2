// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(MarkBasePosFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("MarkBasePos")]
    class MarkBasePos
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("MarkBasePos")]
    class MarkBasePosFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to markCoverage table, from beginning of MarkBasePos subtable")]
        public Offset16 markCoverageOffset;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to baseCoverage table, from beginning of MarkBasePos subtable")]
        public Offset16 baseCoverageOffset;

        [Description(0, "Number of classes defined for marks")]
        public uint16 markClassCount;

        [TableType(typeof(MarkArray))]
        [Description(0, "Offset to MarkArray table, from beginning of MarkBasePos subtable")]
        public Offset16 markArrayOffset;

        [TableType(typeof(BaseArray))]
        [Description(0, "Offset to BaseArray table, from beginning of MarkBasePos subtable")]
        public Offset16 baseArrayOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseArray
    {
        [Description(0, "Number of BaseRecords")]
        public uint16 baseCount;

        [Count(0, FieldValueKind.Path, "baseCount")]
        [Description(0, "Array of BaseRecords, in order of baseCoverage Index")]
        public BaseRecord[] baseRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseRecord
    {
        [Count(0, FieldValueKind.OffsetSource, "\\markClassCount")]
        [TableType(typeof(Anchor))]
        [Description(0, "Array of offsets(one per mark class) to Anchor tables.Offsets are from beginning of BaseArray table, ordered by class")]
        public Offset16[] baseAnchorOffsets;
    }
#pragma warning restore IDE1006
}
