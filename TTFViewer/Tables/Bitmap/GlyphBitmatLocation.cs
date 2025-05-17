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
    [UniformType]
    class BitmapSize
    {
        [TableType(typeof(IndexSubtableList))]
        [Description(0, DescriptionKind.Method, "indexSubTableArrayOffsetDescription")]
        public Offset32 indexSubtableListOffset;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [FieldName(0, "indexSubtableListSize")]
        [Description(0, "Number of bytes in corresponding index subtables and array")]
        public uint32 indexTablesSize; // Number of bytes in corresponding index subtables and array.

        [Description(0, DescriptionKind.Method, "numberOfIndexSubTablesDescription")]
        public uint32 numberOfIndexSubTables;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Not used; set to 0")]
        public uint32 colorRef; // Not used; set to 0.

        [Description(0, "Line metrics for text rendered horizontally")]
        public SbitLineMetrics hori; // Line metrics for text rendered horizontally.

        [Description(0, "Line metrics for text rendered vertically")]
        public SbitLineMetrics vert; // Line metrics for text rendered vertically.

        [Description(0, "Lowest glyph index for this size")]
        public uint16 startGlyphIndex; // Lowest glyph index for this size.

        [Description(0, "Highest glyph index for this size")]
        public uint16 endGlyphIndex; // Highest glyph index for this size.

        [Description(0, "Horizontal pixels per em")]
        public uint8 ppemX; // Horizontal pixels per em.

        [Description(0, "Vertical pixels per em")]
        public uint8 ppemY; // Vertical pixels per em.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "bitDepthDescription")]
        public uint8 bitDepth;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "flagsDescription")]
        public int8 flags;


        static string indexSubTableArrayOffsetDescription(IItemValueService ivp)
        {
            var fontTableType = ItemValueHelper.GetFontTableType(ivp);
            if (fontTableType == typeof(CBLCTable))
                //return "Offset to index subtable from beginning of CBLC.";
                return "Offset to index subtable from beginning of CBLC";
            else if (fontTableType == typeof(EBLCTable))
                //return "Offset to IndexSubtableArray, from beginning of EBLC";
                return "Offset to IndexSubtableList, from beginning of EBLC";
            else
                return null;
        }

        static string numberOfIndexSubTablesDescription(IItemValueService ivp)
        {
            var fontTableType = ItemValueHelper.GetFontTableType(ivp);
            if (fontTableType == typeof(CBLCTable))
                return "There is an index subtable for each range or format change.";
            else if (fontTableType == typeof(EBLCTable))
                return "There is an IndexSubtable for each range or format change.";
            else
                return null;
        }

        static string bitDepthDescription(IItemValueService ivp)
        {
            string valueText = string.Empty;
            if (ivp.Value is uint8 u8)
                valueText = ItemValueHelper.GetEnumItemName(typeof(BitDepth), u8);

            var fontTableType = ItemValueHelper.GetFontTableType(ivp);
            if (fontTableType == typeof(CBLCTable))
                return $"{valueText} In addtition to already defined bitDepth values 1, 2, 4, and 8 supported by existing implementations, the value of 32 is used to identify color bitmaps with 8 bit per pixel RGBA channels.";
            else if (fontTableType == typeof(EBLCTable))
                return $"{valueText} The Microsoft rasterizer v.1.7 or greater supports the following bitDepth values, as described below: 1, 2, 4, and 8.";
            else
                return $"{valueText}";
        }

        static string flagsDescription(IItemValueService ivp)
        {
            string valueText = string.Empty;
            if (ivp.Value is int8 i8)
                valueText = ItemValueHelper.GetEnumItemName(typeof(BitmapFlags), i8);

            var fontTableType = ItemValueHelper.GetFontTableType(ivp);
            if (fontTableType == typeof(CBLCTable))
                return $"{valueText} TextVertical or horizontal (see Bitmap Flags, below).";
            else if (fontTableType == typeof(EBLCTable))
                return $"{valueText} Vertical or horizontal (see the Bitmap Flags section of the EBLC table chapter).";
            else
                return $"{valueText}";
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class SbitLineMetrics
    {
        public int8 ascender;
        public int8 descender;
        public uint8 widthMax;
        public int8 caretSlopeNumerator;
        public int8 caretSlopeDenominator;
        public int8 caretOffset;
        public int8 minOriginSB;
        public int8 minAdvanceSB;
        public int8 maxBeforeBL;
        public int8 minAfterBL;
        public int8 pad1;
        public int8 pad2;
    }


    [TypeName("Bit depth")]
    enum BitDepth
    {
        [FieldName(0, "black/white")]
        BlackWhite = 1,
        [FieldName(0, "4 levels of gray")]
        N4LevelsOfGray = 2,
        [FieldName(0, "16 levels of gray")]
        N16LevelsOsGray = 4,
        [FieldName(0, "256 levels of gray")]
        N256LevelsOfGray = 8,
        [FieldName(0, "RGBA channels")]
        RGBA = 32,
    }


    [Flags]
    [TypeName("Bitmap flags")]
    enum BitmapFlags
    {
        HORIZONTAL_METRICS = 0x01, // Horizontal
        VERTICAL_METRICS = 0x02, // Vertical
        Reserved = 0xFC,//For future use — set to 0.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BigGlyphMetrics
    {
        [Description(0, "Number of rows of data")]
        public uint8 height;

        [Description(0, "Number of columns of data")]
        public uint8 width;

        [Description(0, "Distance in pixels from the horizontal origin to the left edge of the bitmap")]
        public int8 horiBearingX;

        [Description(0, "Distance in pixels from the horizontal origin to the top edge of the bitmap")]
        public int8 horiBearingY;

        [Description(0, "Horizontal advance width in pixels")]
        public uint8 horiAdvance;

        [Description(0, "Distance in pixels from the vertical origin to the left edge of the bitmap")]
        public int8 vertBearingX;

        [Description(0, "Distance in pixels from the vertical origin to the top edge of the bitmap")]
        public int8 vertBearingY;

        [Description(0, "Vertical advance width in pixels")]
        public uint8 vertAdvance;
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SmallGlyphMetrics
    {
        [Description(0, "Number of rows of data")]
        public uint8 height;

        [Description(0, "Number of columns of data")]
        public uint8 width;

        [Description(0, "Distance in pixels from the horizontal origin to the left edge of the bitmap (for horizontal text); or distance in pixels from the vertical origin to the top edge of the bitmap (for vertical text)")]
        public int8 bearingX;

        [Description(0, "Distance in pixels from the horizontal origin to the top edge of the bitmap (for horizontal text); or distance in pixels from the vertical origin to the left edge of the bitmap (for vertical text)")]
        public int8 bearingY;

        [Description(0, "Horizontal or vertical advance width in pixels")]
        public uint8 advance;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class IndexSubtableList
    {
        [Count(0, FieldValueKind.OffsetSource, "numberOfIndexSubTables")]
        [Description(0, "Array of IndexSubtableRecords.")]
        public IList<IndexSubtableRecord> indexSubtableRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class IndexSubtableRecord
    {
        [Description(0, "First glyph ID of this range")]
        public uint16 firstGlyphIndex;

        [Description(0, "Last glyph ID of this range(inclusive)")]
        public uint16 lastGlyphIndex;

        [TableType(typeof(IndexSubtable))]
        [Description(0, "Offset to an IndexSubtable from the start of the IndexSubtableList")]
        public Offset32 indexSubtableOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class IndexSubHeader
    {
        [Description(0, "Format of this IndexSubTable")]
        public uint16 indexFormat;

        [Description(0, "Format of EBDT image data")]
        public uint16 imageFormat;

        [TypeName("Offset32")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset to image data in EBDT table")]
        public uint32 imageDataOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "header\\indexFormat", null)]
    [ClassTypeCondition(typeof(IndexSubtableFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(IndexSubtableFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(IndexSubtableFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [ClassTypeCondition(typeof(IndexSubtableFormat4), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "4")]
    [ClassTypeCondition(typeof(IndexSubtableFormat5), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "5")]
    [Invalid]
    [BaseName("IndexSubtable")]
    class IndexSubtable
    {
        [Description(0, "Header info")]
        public IndexSubHeader header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("IndexSubtable")]
    class IndexSubtableFormat1
    {
        [Description(0, "Header info")]
        public IndexSubHeader header;

        [Count(0, "GetSbitOffsetsCount")]
        [TypeName("Offset32")]
        [ValueFormat(1, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "sbitOffsets[glyphIndex] + imageDataOffset = glyphData")]
        public IList<uint32> sbitOffsets; // sizeOfArray = (lastGlyph - firstGlyph + 1) + 1 + 1 pad if needed

        private static Int32 GetSbitOffsetsCount(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.OffsetSource, "firstGlyphIndex, lastGlyphIndex");
            if (values[0] is uint16 firstGlyphIndex && values[1] is uint16 lastGlyphIndex)
            {
                Int32 result = (lastGlyphIndex - firstGlyphIndex + 1) + 1;
                return result;
            }
            return 0;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("IndexSubtable")]
    class IndexSubtableFormat2
    {
        [Description(0, "Header info")]
        public IndexSubHeader header; // Header info.

        [Description(0, "All the glyphs are of the same size")]
        public uint32 imageSize;

        [Description(0, "All glyphs have the same metrics; glyph data may be compressed, byte-aligned, or bit-aligned")]
        public BigGlyphMetrics bigMetrics;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("IndexSubtable")]
    class IndexSubtableFormat3
    {
        [Description(0, "Header info")]
        public IndexSubHeader header; // Header info.

        [TypeName("Offset16")]
        [Count(0, "GetSbitOffsetsCount")]
        [Description(0, "sbitOffets[glyphIndex] + imageDataOffset = glyphData sizeOfArray = (lastGlyph - firstGlyph + 1) + 1 + 1 pad if needed")]
        [ValueFormat(1, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        public IList<uint16> sbitOffsets;

        private static Int32 GetSbitOffsetsCount(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.OffsetSource, "firstGlyphIndex, lastGlyphIndex");
            if (values[0] is uint16 firstGlyphIndex && values[1] is uint16 lastGlyphIndex)
            {
                Int32 result = (lastGlyphIndex - firstGlyphIndex + 1) + 1;
                return result;
            }
            return 0;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("IndexSubtable")]
    class IndexSubtableFormat4
    {
        [Description(0, "Header info")]
        public IndexSubHeader header; // Header info.

        [Description(0, "Array length")]
        public uint32 numGlyphs;

        [Count(0, FieldValueKind.Path, "numGlyphs", "Add:1")]
        [Description(0, "[numGlyphs + 1] One per glyph")]
        public IList<GlyphIdOffsetPair> glyphArray;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class GlyphIdOffsetPair
    {
        [Description(0, "Glyph ID of glyph present")]
        public uint16 glyphID;

        [TypeName("Offset16")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Location in EBDT")]
        public uint16 sbitOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("IndexSubTable")]
    class IndexSubtableFormat5
    {
        [Description(0, "Header info")]
        public IndexSubHeader header; // Header info.

        [Description(0, "All glyphs have the same data size")]
        public uint32 imageSize;

        [Description(0, "All glyphs have the same metrics")]
        public BigGlyphMetrics bigMetrics;

        [Description(0, "Array length")]
        public uint32 numGlyphs;

        [Count(0, FieldValueKind.Path, "numGlyphs")]
        [Description(0, "One per glyph, sorted by glyph ID")]
        public IList<uint16> glyphIdArray;
    }
#pragma warning restore IDE1006
}
