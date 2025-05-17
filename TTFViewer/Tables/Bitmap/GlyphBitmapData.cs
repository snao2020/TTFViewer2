// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class EbdtComponent
    {
        [Description(0, "Component glyph ID")]
        public uint16 glyphID; // Component glyph ID

        [Description(0, "Position of component left")]
        public int8 xOffset; // Position of component left

        [Description(0, "Position of component top")]
        public int8 yOffset; // Position of component top
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [TypeName("GlyphBitmap")]
    [BaseName("GlyphBitmapData")]
    class GlyphBitmapData
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat1")]
    class GlyphBitmapData_Format1 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public SmallGlyphMetrics smallMetrics; // Metrics information for the glyph

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Byte-aligned bitmap data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint8> imageData; // [variable]     Byte-aligned bitmap data
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat2")]
    class GlyphBitmapData_Format2 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public SmallGlyphMetrics smallMetrics; // Metrics information for the glyph

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Bit-aligned bitmap data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint8> imageData; //[variable]     Bit-aligned bitmap data
    }


    // Format 3: (obsolete)
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat3")]
    class GlyphBitmapData_Format3 : GlyphBitmapData
    {
    }


    // Format 4: (not supported) metrics in EBLC, compressed data
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat4")]
    class GlyphBitmapData_Format4 : GlyphBitmapData
    {
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat5")]
    class GlyphBitmapData_Format5 : GlyphBitmapData
    {
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Bit-aligned bitmap data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint8> imageData; //[variable]     Bit-aligned bitmap data 
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat6 ")]
    class GlyphBitmapData_Format6 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public BigGlyphMetrics bigMetrics; // Metrics information for the glyph

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Byte-aligned bitmap data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint8> imageData; // [variable]     Byte-aligned bitmap data
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat7")]
    class GlyphBitmapData_Format7 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public BigGlyphMetrics bigMetrics; // Metrics information for the glyph

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Bit-aligned bitmap data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint8> imageData; // [variable]     Bit-aligned bitmap data
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat8")]
    class GlyphBitmapData_Format8 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public SmallGlyphMetrics smallMetrics; // Metrics information for the glyph

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Pad to 16-bit boundary")]
        public uint8 pad; // Pad to 16-bit boundary

        [Description(0, "Number of components")]
        public uint16 numComponents; //   Number of components

        [Count(0, FieldValueKind.Path, "numComponents")]
        [Description(0, "Array of EbdtComponent records")]
        public IList<EbdtComponent> components; // [numComponents] Array of EbdtComponent records
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat9")]
    class GlyphBitmapData_Format9 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public BigGlyphMetrics bigMetrics; // Metrics information for the glyph

        [Description(0, "Number of components")]
        public uint16 numComponents; // Number of components

        [Count(0, FieldValueKind.Path, "numComponents")]
        [Description(0, "Array of EbdtComponent records")]
        public IList<EbdtComponent> components; // [numComponents] Array of EbdtComponent records
    }


    // CBDT only

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat17")]
    class GlyphBitmapData_Format17 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public SmallGlyphMetrics glyphMetrics; // Metrics information for the glyph

        [Description(0, "Length of data in bytes")]
        public uint32 dataLen; // Length of data in bytes

        [Count(0, FieldValueKind.Path, "dataLen")]
        [Description(0, "[dataLen] Raw PNG data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public uint8[] data; // [dataLen] Raw PNG data
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat18")]
    class GlyphBitmapData_Format18 : GlyphBitmapData
    {
        [Description(0, "Metrics information for the glyph")]
        public BigGlyphMetrics glyphMetrics; // Metrics information for the glyph

        [Description(0, "Length of data in bytes")]
        public uint32 dataLen; // Length of data in bytes

        [Count(0, FieldValueKind.Path, "dataLen")]
        [Description(0, "Raw PNG data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public uint8[] data; // [dataLen] Raw PNG data
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphBitmapFormat19")]
    class GlyphBitmapData_Format19 : GlyphBitmapData
    {
        [Description(0, "Length of data in bytes")]
        public uint32 dataLen; // Length of data in bytes

        [Count(0, FieldValueKind.Path, "dataLen")]
        [Description(0, "Raw PNG data")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public uint8[] data; // [dataLen] Raw PNG data
    }
#pragma warning restore IDE1006
}
