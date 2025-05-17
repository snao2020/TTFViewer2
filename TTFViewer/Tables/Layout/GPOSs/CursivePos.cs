// var 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(CursivePosFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [TypeName("Lookup type 3 subtable: cursive attachment positioning")]
    [BaseName("CursivePos")]
    class CursivePos
    {
        [Description(0, "Format; identifier")]
        public uint16 posFormat;
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Cursive attachment positioning format 1")]
    [BaseName("CursivePosSubtable")]
    class CursivePosFormat1
    {
        [Description(0, "Format; identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of CursivePos subtable")]
        public Offset16 coverageOffset;

        [Description(0, "Number of EntryExit records")]
        public uint16 entryExitCount;

        [Count(0, FieldValueKind.Path, "entryExitCount")]
        [Description(0, "Array of EntryExit records, in Coverage index order")]
        public EntryExit[] entryExitRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class EntryExit
    {
        [TableType(typeof(Anchor))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to entryAnchor table, from beginning of CursivePos subtable(may be NULL)")]
        public Offset16 entryAnchorOffset;

        [TableType(typeof(Anchor))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to exitAnchor table, from beginning of CursivePos subtable(may be NULL)")]
        public Offset16 exitAnchorOffset;
    }
#pragma warning restore IDE1006
}
