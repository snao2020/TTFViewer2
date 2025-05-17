// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(ChainedSequenceContextFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(ChainedSequenceContextFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(ChainedSequenceContextFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [Invalid]
    [BaseName("ChainedSequenceContext")]
    class ChainedSequenceContext
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ChainedSequenceContext")]
    class ChainedSequenceContextFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of ChainSequenceContextFormat1 table")]
        public Offset16 coverageOffset;

        [Description(0, "Number of ChainedSequenceRuleSet tables")]
        public uint16 chainedSeqRuleSetCount;

        [Count(0, FieldValueKind.Path, "chainedSeqRuleSetCount")]
        [TableType(typeof(ChainedClassSequenceRuleSet))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Array of offsets to ChainedSeqRuleSet tables, from beginning of ChainedSequenceContextFormat1 table(may be NULL)")]
        public IList<Offset16> chainedSeqRuleSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ChainedSequenceRuleSet
    {
        [Description(0, "Number of ChainedSequenceRule tables")]
        public uint16 chainedSeqRuleCount;

        [Count(0, FieldValueKind.Path, "chainedSeqRuleCount")]
        [TableType(typeof(ChainedSequenceRule))]
        [Description(0, "Array of offsets to ChainedSequenceRule tables, from beginning of ChainedSequenceRuleSet table")]
        public IList<Offset16> chainedSeqRuleOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ChainedSequenceRule
    {
        [Description(0, "Number of glyphs in the backtrack sequence")]
        public uint16 backtrackGlyphCount;

        [Count(0, FieldValueKind.Path, "backtrackGlyphCount")]
        [Description(0, "Array of backtrack glyph IDs")]
        public IList<uint16> backtrackSequence;

        [Description(0, "Number of glyphs in the input sequence")]
        public uint16 inputGlyphCount;

        [Count(0, FieldValueKind.Path, "inputGlyphCount", "Sub:1")]
        [Description(0, "[inputGlyphCount - 1] Array of input glyph IDs—start with second glyph")]
        public IList<uint16> inputSequence;

        [Description(0, "Number of glyphs in the lookahead sequence")]
        public uint16 lookaheadGlyphCount;

        [Count(0, FieldValueKind.Path, "lookaheadGlyphCount")]
        [Description(0, "Array of lookahead glyph IDs")]
        public IList<uint16> lookaheadSequence;

        [Description(0, "Number of SequenceLookupRecords")]
        public uint16 seqLookupCount;

        [Count(0, FieldValueKind.Path, "seqLookupCount")]
        [Description(0, "Array of SequenceLookupRecords")]
        public IList<SequenceLookup> seqLookupRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ChainedSequenceContext")]
    class ChainedSequenceContextFormat2
    {
        [Description(0, "Format identifier: format = 2")]
        public uint16 format;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from beginning of ChainedSequenceContextFormat2 table")]
        public Offset16 coverageOffset;

        [TableType(typeof(ClassDef))]
        [Description(0, "Offset to ClassDef table containing backtrack sequence context, from beginning of ChainedSequenceContextFormat2 table")]
        public Offset16 backtrackClassDefOffset;

        [TableType(typeof(ClassDef))]
        [Description(0, "Offset to ClassDef table containing input sequence context, from beginning of ChainedSequenceContextFormat2 table")]
        public Offset16 inputClassDefOffset;

        [TableType(typeof(ClassDef))]
        [Description(0, "Offset to ClassDef table containing lookahead sequence context, from beginning of ChainedSequenceContextFormat2 table")]
        public Offset16 lookaheadClassDefOffset;

        [Description(0, "Number of ChainedClassSequenceRuleSet tables")]
        public uint16 chainedClassSeqRuleSetCount;

        [Count(0, FieldValueKind.Path, "chainedClassSeqRuleSetCount")]
        [TableType(typeof(ChainedClassSequenceRuleSet))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Array of offsets to ChainedClassSequenceRuleSet tables, from beginning of ChainedSequenceContextFormat2 table(may be NULL)")]
        public IList<Offset16> chainedClassSeqRuleSetOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ChainedClassSequenceRuleSet
    {
        [Description(0, "Number of ChainedClassSequenceRule tables")]
        public uint16 chainedClassSeqRuleCount;

        [Count(0, FieldValueKind.Path, "chainedClassSeqRuleCount")]
        [TableType(typeof(ChainedClassSequenceRule))]
        [Description(0, "Array of offsets to ChainedClassSequenceRule tables, from beginning of ChainedClassSequenceRuleSet")]
        public IList<Offset16> chainedClassSeqRuleOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ChainedClassSequenceRule
    {
        [Description(0, "Number of glyphs in the backtrack sequence")]
        public uint16 backtrackGlyphCount;

        [Count(0, FieldValueKind.Path, "backtrackGlyphCount")]
        [Description(0, "Array of backtrack-sequence classes")]
        public IList<uint16> backtrackSequence;

        [Description(0, "Total number of glyphs in the input sequence")]
        public uint16 inputGlyphCount;

        [Count(0, FieldValueKind.Path, "inputGlyphCount", "Sub:1")]
        [Description(0, "[inputGlyphCount - 1] Array of input sequence classes, beginning with the second glyph position")]
        public IList<uint16> inputSequence;

        [Description(0, "Number of glyphs in the lookahead sequence")]
        public uint16 lookaheadGlyphCount;

        [Count(0, FieldValueKind.Path, "lookaheadGlyphCount")]
        [Description(0, "Array of lookahead-sequence classes")]
        public IList<uint16> lookaheadSequence;

        [Description(0, "Number of SequenceLookupRecords")]
        public uint16 seqLookupCount;

        [Count(0, FieldValueKind.Path, "seqLookupCount")]
        [Description(0, "Array of SequenceLookupReco")]
        public IList<SequenceLookup> seqLookupRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ChainedSequenceContext")]
    class ChainedSequenceContextFormat3
    {
        [Description(0, "Format identifier: format = 3")]
        public uint16 format;

        [Description(0, "Number of glyphs in the backtrack sequence")]
        public uint16 backtrackGlyphCount;

        [Count(0, FieldValueKind.Path, "backtrackGlyphCount")]
        [TableType(typeof(Coverage))]
        [Description(0, "Array of offsets to coverage tables for the backtrack sequence")]
        public IList<Offset16> backtrackCoverageOffsets;

        [Description(0, "Number of glyphs in the input sequence")]
        public uint16 inputGlyphCount;

        [Count(0, FieldValueKind.Path, "inputGlyphCount")]
        [TableType(typeof(Coverage))]
        [Description(0, "Array of offsets to coverage tables for the input sequence")]
        public IList<Offset16> inputCoverageOffsets;

        [Description(0, "Number of glyphs in the lookahead sequence")]
        public uint16 lookaheadGlyphCount;

        [Count(0, FieldValueKind.Path, "lookaheadGlyphCount")]
        [TableType(typeof(Coverage))]
        [Description(0, "Array of offsets to coverage tables for the lookahead sequence")]
        public IList<Offset16> lookaheadCoverageOffsets;

        [Description(0, "Number of SequenceLookupRecords")]
        public uint16 seqLookupCount;

        [Count(0, FieldValueKind.Path, "seqLookupCount")]
        [Description(0, "Array of SequenceLookupRecords")]
        public IList<SequenceLookup> seqLookupRecords;
    }
#pragma warning restore IDE1006
}
