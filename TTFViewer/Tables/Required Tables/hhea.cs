// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("hhea")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(hheaTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("hhea — Horizontal Header Table")]
    [BaseName("hhea")]
    class hheaTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the horizontal header table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the horizontal header table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("hhea — Horizontal Header Table")]
    [BaseName("hhea")]
    class hheaTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the horizontal header table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the horizontal headertable — set to 0")]
        public uint16 minorVersion;

        [Description(0, "Typographic ascent—see note below")]
        public FWORD ascender;

        [Description(0, "Typographic descent—see note below")]
        public FWORD descender;

        [Description(0, "Typographic line gap. Negative LineGap values are treated as zero in some legacy platform implementations")]
        public FWORD lineGap;

        [Description(0, "Maximum advance width value in 'hmtx' table")]
        public UFWORD advanceWidthMax;

        [Description(0, "Minimum left sidebearing value in 'hmtx' table for glyphs with contours (empty glyphs should be ignored)")]
        public FWORD minLeftSideBearing;

        [Description(0, "Minimum right sidebearing value; calculated as min(aw - (lsb + xMax - xMin)) for glyphs with contours(empty glyphs should be ignored)")]
        public FWORD minRightSideBearing;

        [Description(0, "Max(lsb + (xMax - xMin))")]
        public FWORD xMaxExtent;

        [Description(0, "Used to calculate the slope of the cursor(rise/run); 1 for vertical")]
        public int16 caretSlopeRise;

        [Description(0, "0 for vertical")]
        public int16 caretSlopeRun;

        [Description(0, "The amount by which a slanted highlight on a glyph needs to be shifted to produce the best appearance.Set to 0 for non-slanted fonts")]
        public int16 caretOffset;

        [FieldName(0, "(reserved)")]
        [Description(0, "set to 0")]
        public int16 reserved0;

        [FieldName(0, "(reserved)")]
        [Description(0, "set to 0")]
        public int16 reserved1;

        [FieldName(0, "(reserved)")]
        [Description(0, "set to 0")]
        public int16 reserved2;

        [FieldName(0, "(reserved)")]
        [Description(0, "set to 0")]
        public int16 reserved3;

        [Description(0, "0 for current format.")]
        public int16 metricDataFormat;

        [Description(0, "Number of hMetric entries in 'hmtx' table")]
        public uint16 numberOfHMetrics;
    }

    /*
    'hhea' entry Tag in the MVAR table 
    caretOffset 	'hcof'
    caretSlopeRise 	'hcrs'
    caretSlopeRun 	'hcrn'
    */
#pragma warning restore IDE1006
}
