// ver1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(SubstExtensionFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("SubstExtension")]
    class SubstExtension
    {
        [Description(0, "Format identifier.Set to 1")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SubstExtension")]
    class SubstExtensionFormat1
    {
        [Description(0, "Format identifier.Set to 1")]
        public uint16 format;

        [Description(0, "")]
        public uint16 extensionLookupType; // Lookup type of subtable referenced by extensionOffset(that is, the extension subtable).

        [TableSelect(FieldValueKind.Path, "extensionLookupType")]
        [TableCondition(typeof(SingleSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Single")]
        [TableCondition(typeof(MultipleSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Multiple")]
        [TableCondition(typeof(AlternateSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Alternate")]
        [TableCondition(typeof(LigatureSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Ligature")]
        [TableCondition(typeof(SequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Contextual_substitution")]
        [TableCondition(typeof(ChainedSequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Chained_contexts_substitution")]
        [TableCondition(typeof(SubstExtension), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Substitution_extension")]
        [TableCondition(typeof(ReverseChainSingleSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Reverse_chaining_context_single")]
        [Description(0, "Offset to the extension subtable, of lookup type extensionLookupType, relative to the start of the ExtensionSubstFormat1 subtable")]
        public Offset32 extensionOffset;
    }
#pragma warning restore IDE1006
}