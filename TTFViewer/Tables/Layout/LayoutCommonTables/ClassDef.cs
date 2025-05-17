// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(ClassDefFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(ClassDefFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [Invalid]
    class ClassDef
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ClassDef")]
    class ClassDefFormat1
    {
        [Description(0, "Format identifier — format = 1")]
        public uint16 format;

        [Description(0, "First glyph ID of the classValueArray")]
        public uint16 startGlyphID;

        [Description(0, "Size of the classValueArray")]
        public uint16 glyphCount;

        [Count(0, FieldValueKind.Path, "glyphCount")]
        [Description(0, "Array of Class Values — one per glyph ID")]
        public IList<uint16> classValues;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("ClassDef")]
    class ClassDefFormat2
    {
        [Description(0, "Format identifier — format = 2")]
        public uint16 format;

        [Description(0, "Number of ClassRangeRecords")]
        public uint16 classRangeCount;

        [Count(0, FieldValueKind.Path, "classRangeCount")]
        [Description(0, "Array of ClassRangeRecords — ordered by startGlyphID")]
        public IList<ClassRange> classRangeRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class ClassRange
    {
        [Description(0, "First glyph ID in the range")]
        public uint16 startGlyphID;

        [Description(0, "Last glyph ID in the range")]
        public uint16 endGlyphID;

        [FieldName(0, "class")]
        [Description(0, "Applied to all glyphs in the range")]
        public uint16 class0;
    }
#pragma warning restore IDE1006
}
