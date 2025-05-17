// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("GDEF")]
    [TypeName("GDEF — Glyph Definition Table")]
    [BaseName("GDEF")]
    class GDEFTable
    {
        [FieldName(0, null)]
        public GDEFHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(GDEFHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [ClassTypeCondition(typeof(GDEFHeader_Version12), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0002")]
    [ClassTypeCondition(typeof(GDEFHeader_Version13), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0003")]
    [Invalid]
    [TypeName("GDEF Header")]
    [BaseName("GDEFHeader")]
    class GDEFHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the GDEF table, = 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the GDEF table, = 0 or 2 or 3")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GDEF Header Version 1.0")]
    class GDEFHeader_Version10 : GDEFHeader
    {
        //public uint16 majorVersion; // Major version of the GDEF table, = 1
        //public uint16 minorVersion; // Minor version of the GDEF table, = 0

        [TableType(typeof(ClassDef))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to class definition table for glyph type, from beginning of GDEF header(may be NULL)")]
        public Offset16 glyphClassDefOffset;

        [TableType(typeof(AttachList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to attachment point list table, from beginning of GDEF header(may be NULL)")]
        public Offset16 attachListOffset;

        [TableType(typeof(LigCaretList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ligature caret list table, from beginning of GDEF header(may be NULL)")]
        public Offset16 ligCaretListOffset;

        [TableType(typeof(ClassDef))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to class definition table for mark attachment type, from beginning of GDEF header(may be NULL)")]
        public Offset16 markAttachClassDefOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GDEF Header, version 1.2")]
    class GDEFHeader_Version12 : GDEFHeader
    {
        //public uint16 majorVersion; // Major version of the GDEF table, = 1
        //public uint16 minorVersion; // Minor version of the GDEF table, = 2

        [TableType(typeof(ClassDef))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to class definition table for glyph type, from beginning of GDEF header(may be NULL)")]
        public Offset16 glyphClassDefOffset;

        [TableType(typeof(AttachList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to attachment point list table, from beginning of GDEF header(may be NULL)")]
        public Offset16 attachListOffset;

        [TableType(typeof(LigCaretList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ligature caret list table, from beginning of GDEF header(may be NULL)")]
        public Offset16 ligCaretListOffset;

        [TableType(typeof(ClassDef))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to class definition table for mark attachment type, from beginning of GDEF header(may be NULL)")]
        public Offset16 markAttachClassDefOffset;

        [TableType(typeof(MarkGlyphSets))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to the table of mark glyph set definitions, from beginning of GDEF header(may be NULL)")]
        public Offset16 markGlyphSetsDefOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GDEF Header, version 1.3")]
    class GDEFHeader_Version13 : GDEFHeader
    {
        //public uint16 majorVersion; // Major version of the GDEF table, = 1
        //public uint16 minorVersion; // Minor version of the GDEF table, = 3

        [TableType(typeof(ClassDef))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to class definition table for glyph type, from beginning of GDEF header(may be NULL)")]
        public Offset16 glyphClassDefOffset;

        [TableType(typeof(AttachList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to attachment point list table, from beginning of GDEF header(may be NULL)")]
        public Offset16 attachListOffset;

        [TableType(typeof(LigCaretList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ligature caret list table, from beginning of GDEF header(may be NULL)")]
        public Offset16 ligCaretListOffset;

        [TableType(typeof(ClassDef))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to class definition table for mark attachment type, from beginning of GDEF header(may be NULL)")]
        public Offset16 markAttachClassDefOffset;

        [TableType(typeof(MarkGlyphSets))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to the table of mark glyph set definitions, from beginning of GDEF header(may be NULL)")]
        public Offset16 markGlyphSetsDefOffset;

        [TableType(typeof(ItemVariationStore))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to the Item Variation Store table, from beginning of GDEF header(may be NULL)")]
        public Offset32 itemVarStoreOffset;
    }


    enum GlyphClassDef
    {
        [FieldName(0, "Base glyph")]
        BaseGlyph = 1, //(single character, spacing glyph)
        [FieldName(0, "Ligature glyph")]
        LigatureGlyph = 2, // (multiple character, spacing glyph)
        [FieldName(0, "Mark glyph")]
        MarkGlyph = 3, // (non-spacing combining glyph)
        [FieldName(0, "Component glyph")]
        ComponentGlyph = 4, // (part of single character, spacing glyph)
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class AttachList
    {
        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table - from beginning of AttachList table")]
        public Offset16 coverageOffset;

        [Description(0, "Number of glyphs with attachment points")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [TableType(typeof(AttachPoint))]
        [Description(0, "Array of offsets to AttachPoint tables-from beginning of AttachList table-in Coverage Index order")]
        public IList<Offset16> attachPointOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class AttachPoint
    {
        [Description(0, "Number of attachment points on this glyph")]
        public uint16 pointCount;

        [Count(0, FieldValueKind.Path, "pointCount")]
        [Description(0, "Array of contour point indices -in increasing numerical order")]
        public IList<uint16> pointIndices;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LigCaretList
    {
        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table - from beginning of LigCaretList table")]
        public Offset16 coverageOffset;

        [Description(0, "Number of ligature glyphs")]
        public uint16 ligGlyphCount;

        [Count(0, FieldValueKind.Path, "ligGlyphCount")]
        [TableType(typeof(LigGlyph))]
        [Description(0, "Array of offsets to LigGlyph tables, from beginning of LigCaretList table —in Coverage Index order")]
        public IList<Offset16> ligGlyphOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LigGlyph
    {
        [Description(0, "Number of CaretValue tables for this ligature (components - 1)")]
        public uint16 caretCount;

        [Count(0, FieldValueKind.Path, "caretCount")]
        [TableType(typeof(CaretValue))]
        [Description(0, "Array of offsets to CaretValue tables, from beginning of LigGlyph table — in increasing coordinate order")]
        public IList<Offset16> caretValueOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(CaretValueFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(CaretValueFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(CaretValueFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [Invalid]
    class CaretValue
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [BaseName("CaretValue")]
    class CaretValueFormat1
    {
        [Description(0, "Format identifier: format = 1")]
        public uint16 format;

        [Description(0, "X or Y value, in design units")]
        public int16 coordinate;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [BaseName("CaretValue")]
    class CaretValueFormat2
    {
        [Description(0, "Format identifier: format = 2")]
        public uint16 fFormat;

        [Description(0, "Contour point index on glyph")]
        public uint16 caretValuePointIndex;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [BaseName("CaretValue")]
    class CaretValueFormat3
    {
        [Description(0, "Format identifier-format = 3")]
        public uint16 format;

        [Description(0, "X or Y value, in design units")]
        public int16 coordinate;

        [TableType(typeof(Device))]
        [Description(0, "Offset to Device table(non-variable font) / Variation Index table(variable font) for X or Y value-from beginning of CaretValue table")]
        public Offset16 deviceOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(MarkGlyphSetsTable_Format1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    class MarkGlyphSets
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("MarkGlyphSets")]
    [BaseName("MarkGlyphSets")]
    class MarkGlyphSetsTable_Format1
    {
        [Description(0, "Format identifier == 1")]
        public uint16 format;

        [Description(0, "Number of mark glyph sets defined")]
        public uint16 markGlyphSetCount;

        [Count(0, FieldValueKind.Path, "markGlyphSetCount")]
        [TableType(typeof(Coverage))]
        [Description(0, "Array of offsets to mark glyph set coverage tables")]
        public IList<Offset32> coverageOffsets;
    }
#pragma warning restore IDE1006
}
