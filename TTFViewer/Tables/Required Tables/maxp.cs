// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("maxp")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(maxpTableVersion05), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00005000")]
    [ClassTypeCondition(typeof(maxpTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00010000")]
    [Invalid]
    [TypeName("maxp — Maximum Profile")]
    [BaseName("maxp")]
    class maxpTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        public Version16Dot16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("maxp — Maximum Profile, Version 0.5")]
    [BaseName("maxp")]
    class maxpTableVersion05
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "0x00005000 for version 0.5")]
        public Version16Dot16 version;

        [Description(0, "The number of glyphs in the font")]
        public uint16 numGlyphs;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("maxp — Maximum Profile, Version 1.0")]
    [BaseName("maxp")]
    class maxpTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "0x00010000 for version 1.0.")]
        public Version16Dot16 version;

        [Description(0, "The number of glyphs in the font.")]
        public uint16 numGlyphs;

        [Description(0, "Maximum points in a non-composite glyph")]
        public uint16 maxPoints;

        [Description(0, "Maximum contours in a non-composite glyph")]
        public uint16 maxContours;

        [Description(0, "Maximum points in a composite glyph")]
        public uint16 maxCompositePoints; // 

        [Description(0, "Maximum contours in a composite glyph")]
        public uint16 maxCompositeContours;

        [Description(0, "1 if instructions do not use the twilight zone (Z0), or 2 if instructions do use Z0; should be set to 2 in most cases")]
        public uint16 maxZones;

        [Description(0, "Maximum points used in Z0")]
        public uint16 maxTwilightPoints;

        [Description(0, "Number of Storage Area locations")]
        public uint16 maxStorage; // 

        [Description(0, "Number of FDEFs, equal to the highest function number + 1")]
        public uint16 maxFunctionDefs;

        [Description(0, "Number of IDEFs")]
        public uint16 maxInstructionDefs;

        [Description(0, "Maximum stack depth across Font Program ('fpgm' table), CVT Program('prep' table) and all glyph instructions(in the 'glyf' table)")]
        public uint16 maxStackElements;

        [Description(0, "Maximum byte count for glyph instructions")]
        public uint16 maxSizeOfInstructions;

        [Description(0, "Maximum number of components referenced at “top level” for any composite glyph")]
        public uint16 maxComponentElements;

        [Description(0, "Maximum levels of recursion; 1 for simple components")]
        public uint16 maxComponentDepth;
    }
#pragma warning restore IDE1006
}
