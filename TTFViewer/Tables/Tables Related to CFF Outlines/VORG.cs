//ver1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [FontTable("VORG")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(VORGTable_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [TypeName("VORG — Vertical Origin Table")]
    [BaseName("VORG")]
    class VORGTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version(starting at 1)")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version(starting at 0)")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("VORG — Vertical Origin Table")]
    [BaseName("VORG")]
    class VORGTable_Version10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version(starting at 1)")]
        public uint16 majorVersion; // Major version(starting at 1). Set to 1.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version(starting at 0)")]
        public uint16 minorVersion; // Minor version(starting at 0). Set to 0.

        [Description(0, "The y coordinate of a glyph’s vertical origin, in the font’s design coordinate system, to be used if no entry is present for the glyph in the vertOriginYMetrics array")]
        public int16 defaultVertOriginY;

        [Description(0, "Number of elements in the vertOriginYMetrics array")]
        public uint16 numVertOriginYMetrics;

        [Count(0, FieldValueKind.Path, "numVertOriginYMetrics")]
        public IList<VertOriginYMetrics> vertOriginYMetrics;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class VertOriginYMetrics
    {
        [Description(0, "Glyph index")]
        public uint16 glyphIndex;

        [Description(0, "Y coordinate, in the font’s design coordinate system, of the vertical origin of glyph with index glyphIndex")]
        public int16 vertOriginY;
    }

#pragma warning restore IDE1006
}
