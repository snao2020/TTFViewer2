// var 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LookupList
    {
        [Description(0, "Number of lookups in this table")]
        public uint16 lookupCount;

        [Count(0, FieldValueKind.Path, "lookupCount")]
        [TableType(typeof(Lookup))]
        [Description(0, "Array of offsets to Lookup tables, from beginning of LookupList — zero based (first lookup is Lookup index = 0)")]
        public IList<Offset16> lookupOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Lookup
    {
        [ValueFormat(0, "lookupTypeText")]
        [Description(0, "Different enumerations for GSUB and GPOS")]
        public uint16 lookupType;

        [ValueFormat(0, "lookupFlagText")]
        [Description(0, "Lookup qualifiers")]
        public uint16 lookupFlag;

        [Description(0, "Number of subtables for this lookup")]
        public uint16 subTableCount;

        [Count(0, FieldValueKind.Path, "subTableCount")]
        [TableSelect("SubtableType")]
        [TableCondition(typeof(SinglePos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Single_adjustment")]
        [TableCondition(typeof(PairPos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Pair_adjustment")]
        [TableCondition(typeof(CursivePos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Cursive_attachment")]
        [TableCondition(typeof(MarkBasePos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Mark_to_base_attachment")]
        [TableCondition(typeof(MarkLigPos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Mark_to_ligature_attachment")]
        [TableCondition(typeof(MarkMarkPos), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Mark_to_mark_attachment")]
        [TableCondition(typeof(SequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Contextual_positioning")]
        [TableCondition(typeof(ChainedSequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Chained_contexts_positioning")]
        [TableCondition(typeof(PosExtension), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GposLookupType.Positioning_extension")]

        [TableCondition(typeof(SingleSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Single")]
        [TableCondition(typeof(MultipleSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Multiple")]
        [TableCondition(typeof(AlternateSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Alternate")]
        [TableCondition(typeof(LigatureSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Ligature")]
        [TableCondition(typeof(SequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Contextual_substitution")]
        [TableCondition(typeof(ChainedSequenceContext), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Chained_contexts_substitution")]
        [TableCondition(typeof(SubstExtension), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Substitution_extension")]
        [TableCondition(typeof(ReverseChainSingleSubst), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.GsubLookupType.Reverse_chaining_context_single")]
        [Description(0, "Array of offsets to lookup subtables, from beginning of Lookup table")]
        public IList<Offset16> subtableOffsets;
        
        [TypeName("uint16")]
        [TypeSelectAttribute(FieldValueKind.Path, "lookupFlag", "Mask:0x0010")]  // 0x0010 = USE_MARK_FILTERING_SET
        [TypeConditionAttribute(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.LookupFlag.USE_MARK_FILTERING_SET")]
        [Description(0, "Index (base 0) into GDEF mark glyph sets structure. This field is only present if the USE_MARK_FILTERING_SET lookup flag is set")]
        public uint16? markFilteringSet;

        static object[] SubtableType(IAttributeService service)
        {
            if(service.GetValues(FieldValueKind.Path, "lookupType").SingleValue(0) is uint16 lookupType)
            //if (values != null && values.Length > 0 && values[0] is uint16 lookupType)
            {
                if(service.GetValues(FieldValueKind.FontTableType, null).SingleValue(0) is Type fontTableType)
                //if (v != null && v.Length == 1 && v[0] is Type fontTableType)
                {
                    if (fontTableType == typeof(GPOSTable))
                    {
                        GposLookupType e = (GposLookupType)(Int32)lookupType;
                        if (Enum.IsDefined(typeof(GposLookupType), e))
                        {
                            return new object[] { e };
                        }
                    }
                    else if (fontTableType == typeof(GSUBTable))
                    {
                        GsubLookupType e = (GsubLookupType)(Int32)lookupType;
                        if (Enum.IsDefined(typeof(GsubLookupType), e))
                        {
                            return new object[] { e };
                        }
                    }
                }
            }
            return null;
        }


        static string lookupTypeText(IItemValueService ivp)
        {
            if (ivp.Value is uint16 value)
            {
                var fontTableType = ItemValueHelper.GetFontTableType(ivp);
                if (fontTableType == typeof(GPOSTable))
                    return ItemValueHelper.GetEnumItemName(typeof(GposLookupType), value);
                else if (fontTableType == typeof(GSUBTable))
                    return ItemValueHelper.GetEnumItemName(typeof(GsubLookupType), value);
                else
                    return value.ToString();
                    //return ItemValueHelper.DefaultText(ivp);
                    //return ItemValueHelper.FieldText(ivp);
            }
            return null;
        }


        static string lookupFlagText(IItemValueService ivp)
        {
            if (ivp.Value is uint16 flag)
            {
                if (flag == 0)
                    return "0";
                else
                    return ItemValueHelper.GetEnumItemName(typeof(LookupFlag), flag);
            }
            return null;
        }
    }



    [Flags]
    enum LookupFlag
    {
        RIGHT_TO_LEFT = 0x0001, // This bit relates only to the correct processing of the cursive attachment lookup type(GPOS lookup type 3).When this bit is set, the last glyph in a given sequence to which the cursive attachment lookup is applied, will be positioned on the baseline.
        IGNORE_BASE_GLYPHS = 0x0002, // If set, skips over base glyphs
        IGNORE_LIGATURES = 0x0004, // If set, skips over ligatures
        IGNORE_MARKS = 0x0008, // If set, skips over all combining marks
        USE_MARK_FILTERING_SET = 0x0010, // If set, indicates that the lookup table structure is followed by a MarkFilteringSet field.The layout engine skips over all mark glyphs not in the mark filtering set indicated.
        reserved = 0x00E0, // For future use(Set to zero)
        MARK_ATTACHMENT_CLASS_FILTER = 0xFF00, // If not zero, skips over all marks of attachment type different from specified.
    }
#pragma warning restore IDE1006
}
