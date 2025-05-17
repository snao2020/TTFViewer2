// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;



namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    enum GposLookupType
    {
        [FieldName(0, "Single adjustment")]
        Single_adjustment = 1, // Adjust position of a single glyph

        [FieldName(0, "Pair adjustment")]
        Pair_adjustment = 2, // Adjust position of a pair of glyphs

        [FieldName(0, "Cursive attachment")]
        Cursive_attachment = 3, // Attach cursive glyphs

        [FieldName(0, "Mark-to-base attachment")]
        Mark_to_base_attachment = 4, // Attach a combining mark to a base glyph

        [FieldName(0, "Mark-to-ligature attachment")]
        Mark_to_ligature_attachment = 5, // Attach a combining mark to a ligature

        [FieldName(0, "Mark-to-mark attachment")]
        Mark_to_mark_attachment = 6, // Attach a combining mark to another mark

        [FieldName(0, "Contextual positioning")]
        Contextual_positioning = 7, // Position one or more glyphs in context

        [FieldName(0, "Chained contexts positioning")]
        Chained_contexts_positioning = 8, // Context positioning Position one or more glyphs in chained context

        [FieldName(0, "Positioning extension")]
        Positioning_extension = 9, // positioning   Extension mechanism for other positionings
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("GPOS")]
    [TypeName("GPOS — Glyph Positioning Table")]
    [BaseName("GPOS")]
    class GPOSTable
    {
        [FieldName(0, null)]
        public GPOSHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion,minorVersion", null)]
    [ClassTypeCondition(typeof(GPOSHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001,0x0000")]
    [ClassTypeCondition(typeof(GPOSHeader_Version11), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001,0x0001")]
    [Invalid]
    [TypeName("GPOS Header")]
    [BaseName("GPOSHeader")]
    class GPOSHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the GPOS table, = 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the GPOS table, = 0 or 1")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GPOS Header, version 1.0")]
    class GPOSHeader_Version10 : GPOSHeader
    {
        //public uint16 majorVersion; // Major version of the GPOS table, = 1
        //public uint16 minorVersion; // Minor version of the GPOS table, = 0

        [TableType(typeof(ScriptList))]
        [Description(0, "Offset to ScriptList table, from beginning of GPOS table")]
        public Offset16 scriptListOffset;

        [TableType(typeof(FeatureList))]
        [Description(0, "Offset to FeatureList table, from beginning of GPOS table")]
        public Offset16 featureListOffset;

        [TableType(typeof(LookupList))]
        [Description(0, "Offset to LookupList table, from beginning of GPOS table")]
        public Offset16 lookupListOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GPOS Header, version 1.1")]
    class GPOSHeader_Version11 : GPOSHeader
    {
        //public uint16 majorVersion; // Major version of the GPOS table, = 1
        //public uint16 minorVersion; // Minor version of the GPOS table, = 1

        [TableType(typeof(ScriptList))]
        [Description(0, "Offset to ScriptList table, from beginning of GPOS table")]
        public Offset16 scriptListOffset;

        [TableType(typeof(FeatureList))]
        [Description(0, "Offset to FeatureList table, from beginning of GPOS table")]
        public Offset16 featureListOffset;

        [TableType(typeof(LookupList))]
        [Description(0, "Offset to LookupList table, from beginning of GPOS table")]
        public Offset16 lookupListOffset;

        [TableType(typeof(FeatureVariations))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to FeatureVariations table, from beginning of GPOS table(may be NULL)")]
        public Offset32 featureVariationsOffset;
    }
#pragma warning restore IDE1006
}