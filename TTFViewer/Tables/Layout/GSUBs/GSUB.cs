// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [TypeName("GSUB LookupType Enumeration")]
    enum GsubLookupType
    {
        Single = 1, // (format 1.1 1.2) Replace one glyph with one glyph
        Multiple = 2, // (format 2.1) Replace one glyph with more than one glyph
        Alternate = 3, // (format 3.1)  Replace one glyph with one of many glyphs
        Ligature = 4, //(format 4.1)   Replace multiple glyphs with one glyph
        [FieldName(0, "Contextual substitution")]
        Contextual_substitution = 5, //(format 5.1 5.2 5.3)    Replace one or more glyphs in context
        [FieldName(0, "Chained contexts substitution")]
        Chained_contexts_substitution = 6, // Context(format 6.1 6.2 6.3)   Replace one or more glyphs in chained context
        [FieldName(0, "Substitution extension")]
        Substitution_extension = 7,//  Substitution(format 7.1)     Extension mechanism for other substitutions(i.e. this excludes the Extension type substitution itself)
        [FieldName(0, "Reverse chaining context single")]
        Reverse_chaining_context_single = 8, // chaining context single(format 8.1)    Applied in reverse order, replace single glyph in chaining context
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("GSUB")]
    [TypeName("GSUB — Glyph Substitution Table")]
    [BaseName("GSUB")]
    class GSUBTable
    {
        [FieldName(0, null)]
        public GSUBHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion,minorVersion", null)]
    [ClassTypeCondition(typeof(GSUBHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001,0x0000")]
    [ClassTypeCondition(typeof(GSUBHeader_Version11), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001,0x0001")]
    [Invalid]
    [TypeName("GSUB Header")]
    [BaseName("GSUBHeader")]
    class GSUBHeader
    {
        [Description(0, "Major version of the GSUB table, = 1")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the GSUB table, = 0 or 1")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GSUB Header, version 1.0")]
    class GSUBHeader_Version10 : GSUBHeader
    {
        //public uint16 majorVersion; // Major version of the GSUB table, = 1
        //public uint16 minorVersion; // Minor version of the GSUB table, = 0

        [TableType(typeof(ScriptList))]
        [Description(0, "Offset to ScriptList table, from beginning of GSUB table")]
        public Offset16 scriptListOffset;
        
        [TableType(typeof(FeatureList))]
        [Description(0, "Offset to FeatureList table, from beginning of GSUB table")]
        public Offset16 featureListOffset;
        
        [TableType(typeof(LookupList))]
        [Description(0, "Offset to LookupList table, from beginning of GSUB table")]
        public Offset16 lookupListOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GSUB Header, Version 1.1")]
    class GSUBHeader_Version11 : GSUBHeader
    {
        //public uint16 majorVersion; // Major version of the GSUB table, = 1
        //public uint16 minorVersion; // Minor version of the GSUB table, = 1

        [TableType(typeof(ScriptList))]
        [Description(0, "Offset to ScriptList table, from beginning of GSUB table")]
        public Offset16 scriptListOffset;

        [TableType(typeof(FeatureList))]
        [Description(0, "Offset to FeatureList table, from beginning of GSUB table")]
        public Offset16 featureListOffset;

        [TableType(typeof(LookupList))]
        [Description(0, "Offset to LookupList table, from beginning of GSUB table")]
        public Offset16 lookupListOffset;

        [TableType(typeof(FeatureVariations))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to FeatureVariations table, from beginning of the GSUB table(may be NULL)")]
        public Offset32 featureVariationsOffset;
    }
#pragma warning restore IDE1006
}
