// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("LTSH")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(LTSHTableVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [Invalid]
    [TypeName("LTSH - Linear Threshold")]
    [BaseName("LTSH")]
    class LTSHTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number(starts at 0).")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("LTSH - Linear Threshold")]
    [BaseName("LTSH")]
    class LTSHTableVersion0
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number(starts at 0).")]
        public uint16 version;

        [Description(0, "Number of glyphs(from “numGlyphs” in 'maxp' table).")]
        public uint16 numGlyphs;

        [Count(0, FieldValueKind.Path, "numGlyphs")]
        [Description(0, "The vertical pixel height at which the glyph can be assumed to scale linearly.On a per glyph basis")]
        public IList<uint8> yPixels;
    }
#pragma warning restore IDE1006
}
