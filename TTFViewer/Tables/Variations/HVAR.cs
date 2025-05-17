// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("HVAR")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(HVARTable_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("HVAR — Horizontal Metrics Variations Table")]
    [BaseName("HVAR")]
    class HVARTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "")]
        public uint16 majorVersion; // Major version number of the horizontal metrics variations table — set to 1.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "")]
        public uint16 minorVersion; // Minor version number of the horizontal metrics variations table — set to 0.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("HVAR — Horizontal Metrics Variations Table")]
    [BaseName("HVAR")]
    class HVARTable_Version10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the horizontal metrics variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the horizontal metrics variations table — set to 0")]
        public uint16 minorVersion;

        [TableType(typeof(ItemVariationStore))]
        [Description(0, "Offset in bytes from the start of this table to the item variation store table")]
        public Offset32 itemVariationStoreOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset in bytes from the start of this table to the delta-set index mapping for advance widths (may be NULL)")]
        public Offset32 advanceWidthMappingOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset in bytes from the start of this table to the delta-set index mapping for left side bearings (may be NULL)")]
        public Offset32 lsbMappingOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset in bytes from the start of this table to the delta-set index mapping for right side bearings (may be NULL)")]
        public Offset32 rsbMappingOffset;
    }
#pragma warning restore IDE1006
}
