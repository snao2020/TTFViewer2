// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;



namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("vhea")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(vheaTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00010000")]
    [ClassTypeCondition(typeof(vheaTableVersion11), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00011000")]
    [Invalid]
    [TypeName("vhea — Vertical Header Table")]
    [BaseName("vhea")]
    class vheaTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number of the vertical header table; ")]
        public Version16Dot16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("vhea — Vertical Header Table Version 1.0")]
    class vheaTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number of the vertical header table; 0x00010000 for version 1.0")]
        public Version16Dot16 version;

        [Description(0, "Distance in FUnits from the centerline to the previous line’s descent.")]
        public FWORD ascent;

        [Description(0, "Distance in FUnits from the centerline to the next line’s ascent.")]
        public FWORD descent;

        [Description(0, "Reserved; set to 0")]
        public FWORD lineGap;

        [Description(0, "The maximum advance height measurement -in FUnits found in the font.This value must be consistent with the entries in the vertical metrics table.")]
        public UFWORD advanceHeightMax;

        [Description(0, "The minimum top sidebearing measurement found in the font, in FUnits.This value must be consistent with the entries in the vertical metrics table.")]
        public FWORD minTopSideBearing;

        [Description(0, "The minimum bottom sidebearing measurement found in the font, in FUnits.This value must be consistent with the entries in the vertical metrics table.")]
        public FWORD minBottomSideBearing;

        [Description(0, "Defined as yMaxExtent = max(tsb + (yMax - yMin)).")]
        public FWORD yMaxExtent;

        [Description(0, "The value of the caretSlopeRise field divided by the value of the caretSlopeRun Field determines the slope of the caret. A value of 0 for the rise and a value of 1 for the run specifies a horizontal caret. A value of 1 for the rise and a value of 0 for the run specifies a vertical caret. Intermediate values are desirable for fonts whose glyphs are oblique or italic.For a vertical font, a horizontal caret is best.")]
        public int16 caretSlopeRise;

        [Description(0, "See the caretSlopeRise field. FieldValue= 1 for nonslanted vertical fonts.")]
        public int16 caretSlopeRun;

        [Description(0, "The amount by which the highlight on a slanted glyph needs to be shifted away from the glyph in order to produce the best appearance. Set value equal to 0 for nonslanted fonts.")]
        public int16 caretOffset;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved0;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved1;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved2;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved3;

        [Description(0, "Set to 0.")]
        public int16 metricDataFormat;

        [Description(0, "Number of advance heights in the vertical metrics table.")]
        public uint16 numOfLongVerMetrics;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("vhea — Vertical Header Table Version 1.1")]
    [BaseName("vhea")]
    class vheaTableVersion11
    {
        [Description(0, "Version number of the vertical header table; 0x00011000 for version 1.1")]
        public Version16Dot16 version;

        [Description(0, "The vertical typographic ascender for this font.It is the distance in FUnits from the ideographic em-box center baseline for the vertical axis to the right edge of the ideographic em-box.")]
        public FWORD vertTypoAscender;

        [Description(0, "The vertical typographic descender for this font.It is the distance in FUnits from the ideographic em-box center baseline for the vertical axis to the left edge of the ideographic em-box.")]
        public FWORD vertTypoDescender;

        [Description(0, "The vertical typographic gap for this font.An application can determine the recommended line spacing for single spaced vertical text for an OpenType font by the following expression: ideo embox width + vhea.vertTypoLineGap")]
        public FWORD vertTypoLineGap;

        [Description(0, "The maximum advance height measurement -in FUnits found in the font. This value must be consistent with the entries in the vertical metrics table.")]
        public UFWORD advanceHeightMax;

        [Description(0, "The minimum top sidebearing measurement found in the font, in FUnits.This value must be consistent with the entries in the vertical metrics table.")]
        public FWORD minTopSideBearing;

        [Description(0, "The minimum bottom sidebearing measurement found in the font, in FUnits.This value must be consistent with the entries in the vertical metrics table.")]
        public FWORD minBottomSideBearing;

        [Description(0, "Defined as yMaxExtent = max(tsb + (yMax - yMin)).")]
        public FWORD yMaxExtent;

        [Description(0, "The value of the caretSlopeRise field divided by the value of the caretSlopeRun Field determines the slope of the caret. A value of 0 for the rise and a value of 1 for the run specifies a horizontal caret. A value of 1 for the rise and a value of 0 for the run specifies a vertical caret. Intermediate values are desirable for fonts whose glyphs are oblique or italic.For a vertical font, a horizontal caret is best.")]
        public int16 caretSlopeRise;

        [Description(0, "See the caretSlopeRise field. FieldValue= 1 for nonslanted vertical fonts.")]
        public int16 caretSlopeRun;

        [Description(0, "The amount by which the highlight on a slanted glyph needs to be shifted away from the glyph in order to produce the best appearance. Set value equal to 0 for nonslanted fonts.")]
        public int16 caretOffset;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved0;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved1;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved2;

        [FieldName(0, "reserved")]
        [Description(0, "Set to 0.")]
        public int16 reserved3;

        [Description(0, "Set to 0.")]
        public int16 metricDataFormat;

        [Description(0, "Number of advance heights in the vertical metrics table.")]
        public uint16 numOfLongVerMetrics;
    }
#pragma warning restore IDE1006
}
