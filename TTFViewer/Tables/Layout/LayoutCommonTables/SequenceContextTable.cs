// ver  1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class SequenceLookup
    {
        [Description(0, "Index(zero-based) into the input glyph sequence")]
        public uint16 sequenceIndex;

        [Description(0, "Index(zero-based) into the LookupList")]
        public uint16 lookupListIndex;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(SequenceContextFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(SequenceContextFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(SequenceContextFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [Invalid]
    class SequenceContext
    {
        [Description(0, "Format identifier: format")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SequenceContext")]
    class SequenceContextFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of SequenceContextFormat1 table")]
        public Offset16 coverageOffset;

        [Description(0, "Number of SequenceRuleSet tables")]
        public uint16 seqRuleSetCount;

        [Count(0, FieldValueKind.Path, "seqRuleSetCount")]
        [TableType(typeof(SequenceRuleSet))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Array of offsets to SequenceRuleSet tables, from beginning of SequenceContextFormat1 table(offsets may be NULL)")]
        public IList<Offset16> seqRuleSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SequenceRuleSet
    {
        [Description(0, "Number of SequenceRule tables")]
        public uint16 seqRuleCount;

        [Count(0, FieldValueKind.Path, "posRuleCout")]
        [TableType(typeof(Sequence))]
        [Description(0, "Array of offsets to SequenceRule tables, from beginning of the SequenceRuleSet table")]
        public IList<Offset16> seqRuleOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SequenceRule
    {
        [Description(0, "Number of glyphs in the input glyph sequence")]
        public uint16 glyphCount;

        [Description(0, "Number of SequenceLookupRecords")]
        public uint16 seqLookupCount;

        [Count(0, FieldValueKind.Path,  "glyphCount", "Sub:1")]
        [Description(0, "[glyphCount - 1] Array of input glyph IDs—starting with the second glyph")]
        public IList<uint16> inputSequence;

        [Count(0, FieldValueKind.Path, "seqLookupCount")]
        [Description(0, "Array of Sequence lookup records")]
        public IList<SequenceLookup> seqLookupRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SequenceContext")]
    class SequenceContextFormat2
    {
        [Description(0, "Format identifier: format = 2")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of SequenceContextFormat2 table")]
        public Offset16 coverageOffset;

        [TableType(typeof(ClassDef))]
        [Description(0, "Offset to ClassDef table, from beginning of SequenceContextFormat2 table")]
        public Offset16 classDefOffset;

        [Description(0, "Number of ClassSequenceRuleSet tables")]
        public uint16 classSeqRuleSetCount;

        [Count(0, FieldValueKind.Path, "classSeqRuleSetCount")]
        [TableType(typeof(ClassSequenceRuleSet))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Array of offsets to ClassSequenceRuleSet tables, from beginning of SequenceContextFormat2 table(may be NULL)")]
        public IList<Offset16> classSeqRuleSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ClassSequenceRuleSet
    {
        [Description(0, "Number of ClassSequenceRule tables")]
        public uint16 classSeqRuleCount;

        [Count(0, FieldValueKind.Path, "classSeqRuleCount")]
        [TableType(typeof(ClassSequenceRule))]
        [Description(0, "Array of offsets to ClassSequenceRule tables, from beginning of ClassSequenceRuleSet table")]
        public IList<Offset16> classSeqRuleOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("ClassSequenceRule table")]
    class ClassSequenceRule
    {
        [Description(0, "Number of glyphs to be matched")]
        public uint16 glyphCount;

        [Description(0, "Number of SequenceLookupRecords")]
        public uint16 seqLookupCount;

        [Count(0, FieldValueKind.Path, "glyphCount", "Sub:1")]
        [Description(0, "[glyphCount - 1]   Sequence of classes to be matched to the input glyph sequence, beginning with the second glyph position")]
        public IList<uint16> inputSequence;

        [Count(0, FieldValueKind.Path, "seqLookupCount")]
        [Description(0, "Array of SequenceLookupRecords")]
        public IList<SequenceLookup> seqLookupRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("SequenceContext")]
    class SequenceContextFormat3
    {
        [Description(0, "Format identifier: format = 3")]
        public uint16 format;

        [Description(0, "Number of glyphs in the input sequence")]
        public uint16 glyphCount;

        [Description(0, "Number of SequenceLookupRecords")]
        public uint16 seqLookupCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [TableType(typeof(Coverage))]
        [Description(0, "Array of offsets to Coverage tables, from beginning of SequenceContextFormat3 subtable")]
        public IList<Offset16> coverageOffsets;

        [Count(0, FieldValueKind.Path, "seqLookupCount")]
        [Description(0, "Array of SequenceLookupRecords")]
        public IList<SequenceLookup> seqLookupRecords;
    }
#pragma warning restore IDE1006
}
