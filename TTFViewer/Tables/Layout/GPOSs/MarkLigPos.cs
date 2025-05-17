// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(MarkLigPosFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("MarkLigPos")]
    class MarkLigPos
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("MarkLigPos")]
    class MarkLigPosFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to markCoverage table, from beginning of MarkLigPos subtable")]
        public Offset16 markCoverageOffset;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to ligatureCoverage table, from beginning of MarkLigPos subtable")]
        public Offset16 ligatureCoverageOffset;

        [Description(0, "Number of defined mark classes")]
        public uint16 markClassCount;

        [TableType(typeof(MarkArray))]
        [Description(0, "Offset to MarkArray table, from beginning of MarkLigPos subtable")]
        public Offset16 markArrayOffset;

        [TableType(typeof(LigatureArray))]
        [Description(0, "Offset to LigatureArray table, from beginning of MarkLigPos subtable")]
        public Offset16 ligatureArrayOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LigatureArray
    {
        [Description(0, "Number of LigatureAttach table offsets")]
        public uint16 ligatureCount;

        [Count(0, FieldValueKind.Path, "ligatureCount")]
        [TableType(typeof(LigatureAttach))]
        [Description(0, "Array of offsets to LigatureAttach tables.Offsets are from beginning of LigatureArray table, ordered by ligatureCoverage index")]
        public IList<Offset16> ligatureAttachOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LigatureAttach
    {
        [Description(0, "Number of ComponentRecords in this ligature")]
        public uint16 componentCount;

        [Count(0, FieldValueKind.Path, "componentCount")]
        [Description(0, "Array of Component records, ordered in writing direction")]
        public IList<ComponentRecord> componentRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ComponentRecord
    {
        [Count(0, FieldValueKind.OffsetSource, "2:\\markClassCount")]
        [TableType(typeof(Anchor))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Array of offsets(one per class) to Anchor tables.Offsets are from beginning of LigatureAttach table, ordered by class (may be NULL)")]
        public IList<Offset16> ligatureAnchorOffsets;
    }
#pragma warning restore IDE1006
}
