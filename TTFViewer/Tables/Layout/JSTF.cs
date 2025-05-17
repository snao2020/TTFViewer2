// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("JSTF")]
    [TypeName("JSTF — Justification Table")]
    [BaseName("JSTF")]
    class JSTFTable
    {
        [FieldName(0, null)]
        public JSTFHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(JSTFHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001,0x0000")]
    [Invalid]
    [TypeName("JSTF header")]
    [BaseName("JSTFHeader")]
    class JSTFHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the JSTF table, = 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the JSTF table, = 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("JSTF header")]
    class JSTFHeader_Version10 : JSTFHeader
    {
        //public uint16 majorVersion; // Major version of the JSTF table, = 1
        //public uint16 minorVersion; // Minor version of the JSTF table, = 0

        [Description(0, "Number of JstfScriptRecords in this table")]
        public uint16 jstfScriptCount;

        [Count(0, FieldValueKind.Path, "jstfScriptCount")]
        [Description(0, "Array of JstfScriptRecords, in alphabetical order by jstfScriptTag")]
        public IList<JstfScriptRecord> jstfScriptRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class JstfScriptRecord
    {
        [Description(0, "4-byte JstfScript identification")]
        public Tag jstfScriptTag;

        [TableType(typeof(JstfScript))]
        [Description(0, "Offset to JstfScript table, from beginning of JSTF Header")]
        public Offset16 jstfScriptOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class JstfScript
    {
        [TableType(typeof(ExtenderGlyph))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ExtenderGlyph table, from beginning of JstfScript table(may be NULL)")]
        public Offset16 extenderGlyphOffset;

        [TableType(typeof(JstfLangSys))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to default JstfLangSys table, from beginning of JstfScript table(may be NULL)")]
        public Offset16 defJstfLangSysOffset;

        [Description(0, "Number of JstfLangSysRecords in this table- may be zero(0)")]
        public uint16 jstfLangSysCount;

        [Count(0, FieldValueKind.Path, "jstfLangSysCount")]
        [Description(0, "Array of JstfLangSysRecords, in alphabetical order by JstfLangSysTag")]
        public IList<JstfLangSysRecord> jstfLangSysRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class JstfLangSysRecord
    {
        [Description(0, "4-byte JstfLangSys identifier")]
        public Tag jstfLangSysTag;

        [TableType(typeof(JstfLangSys))]
        [Description(0, "Offset to JstfLangSys table, from beginning of JstfScript table")]
        public Offset16 jstfLangSysOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ExtenderGlyph
    {
        [Description(0, "Number of extender glyphs in this script")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "Extender glyph IDs — in increasing numerical order")]
        public uint16[] extenderGlyphs;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("JstfLangSys table")]
    class JstfLangSys
    {
        [Description(0, "Number of JstfPriority tables")]
        public uint16 jstfPriorityCount;

        [Count(0, FieldValueKind.Path, "jstfPriorityCount")]
        [TableType(typeof(JstfPriority))]
        [Description(0, "Array of offsets to JstfPriority tables, from beginning of JstfLangSys table, in priority order")]
        public IList<Offset16> jstfPriorityOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class JstfPriority
    {
        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to shrinkage-enable JstfGSUBModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gsubShrinkageEnableOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to shrinkage-disable JstfGSUBModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gsubShrinkageDisableOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to shrinkage-enable JstfGPOSModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gposShrinkageEnableOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to shrinkage-disable JstfGPOSModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gposShrinkageDisableOffset;

        [TableType(typeof(JstfMax))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to shrinkage JstfMax table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 shrinkageJstfMaxOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to extension-enable JstfGSUBModList table, from beginnning of JstfPriority table(may be NULL)")]
        public Offset16 gsubExtensionEnableOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to extension-disable JstfGSUBModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gsubExtensionDisableOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to extension-enable JstfGPOSModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gposExtensionEnableOffset;

        [TableType(typeof(JstfModList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to extension-disable JstfGPOSModList table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 gposExtensionDisableOffset;

        [TableType(typeof(JstfMax))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to extension JstfMax table, from beginning of JstfPriority table(may be NULL)")]
        public Offset16 extensionJstfMaxOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class JstfModList
    {
        [Description(0, "Number of lookups for this modification")]
        public uint16 lookupCount;

        [Count(0, FieldValueKind.Path, "lookupCount")]
        [Description(0, "Array of Lookup indices into the GSUB or GPOS LookupList, in increasing numerical order")]
        public IList<uint16> gsubLookupIndices;
    }

    // 1.9.1 deleted
    /*
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("JstfGSUBModList table")]
    class JstfGSUBModListTable
    {
        [Description(0, "Number of lookups for this modification")]
        public uint16 lookupCount;

        [Count(0, FieldValueKind.Path, "lookupCount")]
        [Description(0, "Array of Lookup indices into the GSUB LookupList, in increasing numerical order")]
        public uint16[] gsubLookupIndices;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("JstfGPOSModList table")]
    class JstfGPOSModListTable
    {
        [Description(0, "Number of lookups for this modification")]
        public uint16 lookupCount;

        [Count(0, FieldValueKind.Path, "lookupCount")]
        [Description(0, "Array of Lookup indices into the GPOS LookupList, in increasing numerical order")]
        public uint16[] gposLookupIndices;
    }
    */

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class JstfMax
    {
        [Description(0, "Number of lookup Indices for this modification")]
        public uint16 lookupCount;

        [Count(0, FieldValueKind.Path, "lookupCount")]
        [TableType(typeof(Lookup))]
        [Description(0, "Array of offsets to GPOS-type lookup tables, from beginning of JstfMax table, in design order")]
        public IList<Offset16> lookupOffsets;
    }
#pragma warning restore IDE1006
}
