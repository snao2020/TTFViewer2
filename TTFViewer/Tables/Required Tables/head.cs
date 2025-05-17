// ver 1.9.1
using System;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("head")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(headTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("head - Font Header Table")]
    [BaseName("head")]
    class headTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the font header table")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the font header table.")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("head - Font Header Table")]
    [BaseName("head")]
    class headTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the font header table — set to 1.")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the font header table — set to 0.")]
        public uint16 minorVersion;

        [Description(0, "Set by font manufacturer.")]
        public Fixed fontRevision;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "To compute: set it to 0, sum the entire font as uint32, then store 0xB1B0AFBA - sum.If the font is used as a component in a font collection file, the value of this field will be invalidated by changes to the file structure and font table directory, and must be ignored.")]
        public uint32 checksumAdjustment;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Set to 0x5F0F3CF5")]
        public uint32 magicNumber;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, typeof(headFlags))]
        public uint16 flags;    // Bit 0: Baseline for font at y= 0.
                                // Bit 1: Left sidebearing point at x= 0(relevant only for TrueType rasterizers) — see the note below regarding variable fonts.
                                // Bit 2: Instructions may depend on point size.
                                // Bit 3: Force ppem to integer values for all internal scaler math; may use fractional ppem sizes if this bit is clear.It is strongly recommended that this be set in hinted fonts.
                                // Bit 4: Instructions may alter advance width (the advance widths might not scale linearly).
                                // Bit 5: This bit is not used in OpenType, and should not be set in order to ensure compatible behavior on all platforms. If set, it may result in different behavior for vertical layout in some platforms. (See Apple’s specification for details regarding behavior in Apple platforms.)
                                // Bits 6–10: These bits are not used in Opentype and should always be cleared. (See Apple’s specification for details regarding legacy used in Apple platforms.)
                                // Bit 11: Font data is “lossless” as a result of having been subjected to optimizing transformation and / or compression(such as e.g.compression mechanisms defined by ISO / IEC 14496 - 18, MicroType Express, WOFF 2.0 or similar) where the original font functionality and features are retained but the binary compatibility between input and output font files is not guaranteed. As a result of the applied transform, the DSIG table may also be invalidated.
                                // Bit 12: Font converted(produce compatible metrics).
                                // Bit 13: Font optimized for ClearType™. Note, fonts that rely on embedded bitmaps(EBDT) for rendering should not be considered optimized for ClearType, and therefore should keep this bit cleared.
                                // Bit 14: Last Resort font.If set, indicates that the glyphs encoded in the 'cmap' subtables are simply generic symbolic representations of code point ranges and don’t truly represent support for those code points.If unset, indicates that the glyphs encoded in the 'cmap' subtables represent proper support for those code points.
                                // Bit 15: Reserved, set to 0.

        [Description(0, "Set to a value from 16 to 16384.Any value in this range is valid.In fonts that have TrueType outlines, a power of 2 is recommended as this allows performance optimizations in some rasterizers")]
        public uint16 unitsPerEm;

        [Description(0, "Number of seconds since 12:00 midnight that started January 1st 1904 in GMT / UTC time zone.")]
        public LONGDATETIME created;

        [Description(0, "Number of seconds since 12:00 midnight that started January 1st 1904 in GMT / UTC time zone.")]
        public LONGDATETIME modified;

        [Description(0, DescriptionKind.Method, "RelativeInt16Description")]
        public int16 xMin;

        [Description(0, DescriptionKind.Method, "RelativeInt16Description")]
        public int16 yMin;

        [Description(0, DescriptionKind.Method, "RelativeInt16Description")]
        public int16 xMax;

        [Description(0, DescriptionKind.Method, "RelativeInt16Description")]
        public int16 yMax;

        [Description(0, typeof(headMacStyle))]
        public uint16 macStyle; // Bit 0: Bold(if set to 1);
                                // Bit 1: Italic(if set to 1)
                                // Bit 2: Underline(if set to 1)
                                // Bit 3: Outline(if set to 1)
                                // Bit 4: Shadow(if set to 1)
                                // Bit 5: Condensed(if set to 1)
                                // Bit 6: Extended(if set to 1)
                                // Bits 7–15: Reserved(set to 0).

        [Description(0, "Smallest readable size in pixels.")]
        public uint16 lowestRecPPEM;

        [Description(0, typeof(headFontDirectionHint))]
        public int16 fontDirectionHint; // Deprecated(Set to 2).
                                        // 0: Fully mixed directional glyphs;
                                        // 1: Only strongly left to right;
                                        // 2: Like 1 but also contains neutrals;
                                        // -1: Only strongly right to left;
                                        // -2: Like - 1 but also contains neutrals.
                                        // (A neutral character has no inherent directionality; it is not a character with zero(0) width.Spaces and punctuation are examples of neutral characters. Non - neutral characters are those with inherent directionality. For example, Roman letters(left - to - right) and Arabic letters(right - to - left) have directionality. In a “normal” Roman font where spaces and punctuation are present, the font direction hints should be set to two(2).)

        [Description(0, "0 for short offsets (Offset16), 1 for long(Offset32).")]
        public int16 indexToLocFormat;

        [Description(0, "0 for current format.")]
        public int16 glyphDataFormat;

        static string RelativeInt16Description(IItemValueService ivp)
        {
            string rel = ItemValueHelper.RelativeValueDescription(ivp);
            if (rel == null)
                return "For all glyph bounding boxes";
            else
                return $"For all glyph bounding boxes ({rel})";
        }
    }


    [Flags]
    enum headFlags
    {
        [FieldName(0, "Baseline")] // Baseline for font at y= 0
        Bit0 = 0x0001, 
        [FieldName(0, "Left sidebearing")] // Left sidebearing point at x= 0(relevant only for TrueType rasterizers) — see the note below regarding variable fonts
        Bit1 = 0x0002,
        [FieldName(0, "may depend on point size")] //Instructions may depend on point size" ),
        Bit2 = 0x0004,
        [FieldName(0, "Force ppem to integer")] //Force ppem to integer values for all internal scaler math; may use fractional ppem sizes if this bit is clear.It is strongly recommended that this be set in hinted fonts" ),
        Bit3 = 0x0008,
        [FieldName(0, "may alter advance width")] //"Instructions may alter advance width (the advance widths might not scale linearly)" ),
        Bit4 = 0x0010,
        [FieldName(0, "not used in OpenType")] //This bit is not used in OpenType, and should not be set in order to ensure compatible behavior on all platforms. If set, it may result in different behavior for vertical layout in some platforms. (See Apple’s specification for details regarding behavior in Apple platforms.)" ),
        Bit5 = 0x0020,
        [FieldName(0, "not used in Opentype")] //These bits are not used in Opentype and should always be cleared. (See Apple’s specification for details regarding legacy used in Apple platforms.)" ),
        Bit6 = 0x0040,
        [FieldName(0, "not used in Opentype")] //These bits are not used in Opentype and should always be cleared. (See Apple’s specification for details regarding legacy used in Apple platforms.)" ),
        Bit7 = 0x0080,
        [FieldName(0, "not used in Opentype")] //These bits are not used in Opentype and should always be cleared. (See Apple’s specification for details regarding legacy used in Apple platforms.)" ),
        Bit8 = 0x0100,
        [FieldName(0, "not used in Opentype")] //These bits are not used in Opentype and should always be cleared. (See Apple’s specification for details regarding legacy used in Apple platforms.)" ),
        Bit9 = 0x0200,
        [FieldName(0, "not used in Opentype")] //These bits are not used in Opentype and should always be cleared. (See Apple’s specification for details regarding legacy used in Apple platforms.)" ),
        Bit10 = 0x0400,
        [FieldName(0, "lossless")] //Font data is “lossless” as a result of having been subjected to optimizing transformation and / or compression(such as e.g.compression mechanisms defined by ISO / IEC 14496 - 18, MicroType Express, WOFF 2.0 or similar) where the original font functionality and features are retained but the binary compatibility between input and output font files is not guaranteed. As a result of the applied transform, the DSIG table may also be invalidated" ),
        Bit11 = 0x0800,
        [FieldName(0, "Font converted")] //Font converted(produce compatible metrics)" ),
        Bit12 = 0x1000,
        [FieldName(0, "Font optimized")] //Font optimized for ClearType™. Note, fonts that rely on embedded bitmaps(EBDT) for rendering should not be considered optimized for ClearType, and therefore should keep this bit cleared" ),
        Bit13 = 0x2000,
        [FieldName(0, "Last Resort font")] //Last Resort font.If set, indicates that the glyphs encoded in the 'cmap' subtables are simply generic symbolic representations of code point ranges and don’t truly represent support for those code points.If unset, indicates that the glyphs encoded in the 'cmap' subtables represent proper support for those code points" ),
        Bit14 = 0x4000,
        [FieldName(0, "Reserved")] //Reserved, set to 0" ),
        Bit15 = 0x8000,
    }


    [Flags]
    enum headMacStyle
    {
        Bold = 0x0001,   // Bit 0: Bold(if set to 1);
        Italic = 0x0002, // Bit 1: Italic(if set to 1)
        Underline = 0x0004,  // Bit 2: Underline(if set to 1)
        Outline = 0x0008,     // Bit 3: Outline(if set to 1)
        Shadow = 0x0010,     // Bit 4: Shadow(if set to 1)
        Condensed = 0x0020,  // Bit 5: Condensed(if set to 1)
        Extended = 0x0040,  // Bit 6: Extended(if set to 1)
        [FieldName(0, "Reserved")]
        Bit7 = 0x0080,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit8 = 0x0100,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit9 = 0x0200,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit10 = 0x0400,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit11 = 0x0800,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit12 = 0x1000,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit13 = 0x2000,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit14 = 0x4000,// Bits 7–15: Reserved(set to 0).
        [FieldName(0, "Reserved")]
        Bit15 = 0x8000,// Bits 7–15: Reserved(set to 0).
    }


    enum headFontDirectionHint
    { 
        [FieldName(0, "Fully mixed directional glyphs")]
        Zero = 0, //Fully mixed directional glyphs;
        [FieldName(0, "Only strongly left to right")]
        One = 1, //Only strongly left to right;
        [FieldName(0, "Like 1 but also contains neutrals")]
        Two = 2, //Like 1 but also contains neutrals;
        [FieldName(0, "Only strongly right to left;")]
        MinusOne = -1, //Only strongly right to left;
        [FieldName(0, "Like - 1 but also contains neutrals.")]
        MinusTwo = -2, //Like - 1 but also contains neutrals.
        // (A neutral character has no inherent directionality; it is not a character with zero(0) width.Spaces and punctuation are examples of neutral characters. Non - neutral characters are those with inherent directionality. For example, Roman letters(left - to - right) and Arabic letters(right - to - left) have directionality. In a “normal” Roman font where spaces and punctuation are present, the font direction hints should be set to two(2).)
    }

#pragma warning restore IDE1006
}