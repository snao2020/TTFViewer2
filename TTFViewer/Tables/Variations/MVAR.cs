// var 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("MVAR")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(MVARTable_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("MVAR — Metrics Variations Table")]
    [BaseName("MVAR")]
    class MVARTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the metrics variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the metrics variations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("MVAR — Metrics Variations Table")]
    [BaseName("MVAR")]
    class MVARTable_Version10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the metrics variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the metrics variations table — set to 0")]
        public uint16 minorVersion;

        [FieldName(0, "(reserved)")]
        [Description(0, "Not used; set to 0")]
        public uint16 reserved;

        [Description(0, "The size in bytes of each value record — must be greater than zero")]
        public uint16 valueRecordSize;

        [Description(0, "The number of value records — may be zero")]
        public uint16 valueRecordCount;

        [TableType(typeof(ItemVariationStore))]
        [Description(0, "Offset in bytes from the start of this table to the item variation store table.If valueRecordCount is zero, set to zero; if valueRecordCount is greater than zero, must be greater than zero")]
        public Offset16 itemVariationStoreOffset;

        [Count(0, FieldValueKind.Path, "valueRecordCount")]
        [Description(0, "Array of value records that identify target items and the associated delta-set index for each.The valueTag records must be in binary order of their valueTag field")]
        public MVARValueRecord[] valueRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("ValueRecord")]
    class MVARValueRecord
    {
        [Description(0, "Four-byte tag identifying a font-wide measure")]
        public Tag valueTag;

        [Description(0, "A delta-set outer index — used to select an item variation data subtable within the item variation store")]
        public uint16 deltaSetOuterIndex;

        [Description(0, "A delta-set inner index — used to select a delta-set row within an item variation data subtable")]
        public uint16 deltaSetInnerIndex;
    }

#pragma warning restore IDE1006
}
