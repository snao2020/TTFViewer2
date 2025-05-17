// var 1.9.1 not tested
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("sbix")]
    [TypeName("sbix — Standard Bitmap Graphics Table")]
    [BaseName("sbix")]
    class sbixTable
    {
        [FieldName(0, null)]
        public sbixHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(sbixHeader_Version1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [TypeName("'sbix' Header")]
    [BaseName("sbixHeader")]
    class sbixHeader
    {
        [Description(0, "Table version number — set to 1")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'sbix' Header")]
    class sbixHeader_Version1 : sbixHeader
    {
        //public uint16 version;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Bit 0: Set to 1,Bit 1: Draw outlines,Bits 2 to 15: reserved (set to 0)")]
        public uint16 flags; // Bit 0: Set to 1.
                             // Bit 1: Draw outlines.
                             // Bits 2 to 15: reserved (set to 0).

        [Description(0, "Number of bitmap strikes")]
        public uint32 numStrikes;

        [TableType(typeof(Strikes))]
        [Count(0, FieldValueKind.Path, "numStrikes")]
        [Description(0, "Offsets from the beginning of the 'sbix' table to data for each individual bitmap strike")]
        public IList<Offset32> strikeOffsets; // [numStrikes] Offsets from the beginning of the 'sbix' table to data for each individual bitmap strike.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Strikes
    {
        [Description(0, "The PPEM size for which this strike was designed")]
        public uint16 ppem;

        [Description(0, "The device pixel density (in PPI) for which this strike was designed. (E.g., 96 PPI, 192 PPI.)")]
        public uint16 ppi;

        [TableType(typeof(GlyphData))]
        [Count(0, FieldValueKind.FontTableValue, "maxp\\numGlyphs", "Add:1")]
        [Description(0, "[numGlyphs + 1]   Offset from the beginning of the strike data header to bitmap data for an individual glyph ID")]
        public IList<Offset32> glyphDataOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Glyph data")]
    class GlyphData
    {
        [Description(0, "The horizontal(x-axis) offset from the left edge of the graphic to the glyph’s origin.That is, the x-coordinate of the point on the baseline at the left edge of the glyph")]
        public int16 originOffsetX;

        [Description(0, "The vertical (y-axis) offset from the bottom edge of the graphic to the glyph’s origin.That is, the y-coordinate of the point on the baseline at the left edge of the glyph")]
        public int16 originOffsetY;

        [Description(0, "Indicates the format of the embedded graphic data: one of 'jpg ', 'png ' or 'tiff', or the special format 'dupe")]
        public Tag graphicType;

        [Length(0, "dataLength")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "The actual embedded graphic data.The total length is inferred from sequential entries in the glyphDataOffsets array and the fixed size (8 bytes) of the preceding fields")]
        public uint8[] data;
        
        static UInt32 dataLength(IAttributeService service)
        {
            if(service.GetValues(FieldValueKind.OffsetSource, "glyphDataOffsets").SingleValue(0) is IList<Offset32> glyphDataOffsets)
            {
                var index = TablePathHelper.GetLastIndex(service.Path);
                var nextPos = glyphDataOffsets[index + 1];
                var currPos = glyphDataOffsets[index];
                return nextPos - currPos - (UInt32)Marshal.SizeOf(typeof(uint16)) * 2 - (UInt32)Marshal.SizeOf(typeof(Tag));
            }
            return 0;
        }
    }
#pragma warning restore IDE1006
}
