// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("VVAR")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(VVARTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("VVAR — Vertical Metrics Variations Table")]
    [BaseName("VVAR")]
    class VVARTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the metrics variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the metrics variations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("VVAR — Vertical Metrics Variations Table")]
    [BaseName("VVAR")]
    class VVARTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the metrics variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the metrics variations table — set to 0")]
        public uint16 minorVersion;

        [TableType(typeof(ItemVariationStore))]
        [Description(0, "Offset in bytes from the start of this table to the item variation store table")]
        public Offset32 itemVariationStoreOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset in bytes from the start of this table to the delta-set index mapping for advance heights (may be NULL)")]
        public Offset32 advanceHeightMappingOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "jOffset in bytes from the start of this table to the delta-set index mapping for top side bearings (may be NULL)")]
        public Offset32 tsbMappingOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset in bytes from the start of this table to the delta-set index mapping for bottom side bearings (may be NULL)")]
        public Offset32 bsbMappingOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset in bytes from the start of this table to the delta-set index mapping for Y coordinates of vertical origins (may be NULL)")]
        public Offset32 vOrgMappingOffset;
    }
#pragma warning restore IDE1006
}
