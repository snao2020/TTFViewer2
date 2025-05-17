// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(PosExtensionFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [TypeName("ookup type 9 subtable: positioning subtable extension")]
    [BaseName("ExtensionPosSubtable")]
    class PosExtension
    {
        [Description(0, "Format identifier")]
        public uint16 posFormat;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ExtensionPosSubtable")]
    class PosExtensionFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [Description(0, "Lookup type of subtable referenced by extensionOffset(i.e.the extension subtable)")]
        public uint16 extensionLookupType;

        [TableSelect(FieldValueKind.Path, "extensionLookupType")]
        [TableCondition(typeof(SinglePos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Single_adjustment")]
        [TableCondition(typeof(PairPos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Pair_adjustment")]
        [TableCondition(typeof(CursivePos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Cursive_attachment")]
        [TableCondition(typeof(MarkBasePos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Mark_to_base_attachment")]
        [TableCondition(typeof(MarkLigPos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Mark_to_ligature_attachment")]
        [TableCondition(typeof(MarkMarkPos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Mark_to_mark_attachment")]
        [TableCondition(typeof(SequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Contextual_positioning")]
        [TableCondition(typeof(ChainedSequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Chained_contexts_positioning")]
        [TableCondition(typeof(PosExtension), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Positioning_extension")]
        [Description(0, "Offset to the extension subtable, of lookup type extensionLookupType, relative to the start of the ExtensionPosFormat1 subtable")]
        public Offset32 extensionOffset;
    }
#pragma warning restore IDE1006
}
