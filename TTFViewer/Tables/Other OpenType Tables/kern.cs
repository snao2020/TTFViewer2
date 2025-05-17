// ver 1.9.1 KernSubtableFormat2 is not tested
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("kern")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "Header\\version", null)]
    [ClassTypeCondition(typeof(kernTable), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [Invalid]
    [TypeName("kern - Kerning")]
    [BaseName("kern")]
    class kernTableInvalid
    {
        [FieldName(0, "")]
        public KernHeaderInvalid Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [TypeName("KernHeader")]
    class KernHeaderInvalid
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(0)")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("kern - Kerning")]
    [BaseName("kern")]
    class kernTable
    {
        [FieldName(0, "")]
        public KernHeader Header;

        [Count(0, FieldValueKind.Path, "Header\\nTables")]
        [TypeName("subtable")]
        [FieldName(0, null)]
        public IList<KerningSubtable> subtables;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class KernHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(0)")]
        public uint16 version;

        [Description(0, "Number of subtables in the kerning table.")]
        public uint16 nTables;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class KerningSubtable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Kern subtable version number")]
        public uint16 version;

        [Description(0, "Length of the subtable, in bytes(including this header).")]
        public uint16 length;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "coverageDescription")]
        public uint16 coverage;

        [TypeSelect(FieldValueKind.Path, "coverage", "Mask:0xff00")]
        [TypeCondition(typeof(KernSubtableUnknown), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TypeCondition(typeof(KernSubtableFormat0), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0x0000")]
        [TypeCondition(typeof(KernSubtableFormat2), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0x0200")]
        public KernSubtable subtable;


        static string coverageDescription(IItemValueService ivp)
        {
            string result = null;
            if (ivp.Value is uint16 u16)
            {
                var v = (Int32)u16;
                result = ItemValueHelper.GetEnumItemName(typeof(CoverageFieldBits), v & 0x000F);
                var format = (v & (Int32)CoverageFieldBits.format) >> 8;
                result += $", format{format}";
            }
            return result;
        }
    }


    [Flags]
    enum CoverageFieldBits
    {
        horizontal = 1,   // if table has horizontal data, 0 if vertical.
        minimum = 2,      // If this bit is set to 1, the table has minimum values.If set to 0, the table has kerning values.
        [FieldName(0, "cross-stream")]
        cross_stream = 4, // If set to 1, kerning is perpendicular to the flow of the text.
                          // If the text is normally written horizontally, kerning will be done in the up and down directions.If kerning values are positive, the text will be kerned upwards; if they are negative, the text will be kerned downwards.
                          // If the text is normally written vertically, kerning will be done in the left and right directions.If kerning values are positive, the text will be kerned to the right; if they are negative, the text will be kerned to the left.
                          // The value 0x8000 in the kerning data resets the cross-stream kerning back to 0.
        override_ = 8,    // If this bit is set to 1 the value in this table should replace the value currently being accumulated.
        reserved1 = 0x00f0, // Reserved.This should be set to zero.
        format = 0xff00,  // Format of the subtable. Only formats 0 and 2 have been defined.Formats 1 and 3 through 255 are reserved for future use.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("KernSubtable")]
    class KernSubtable
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Unknown Format")]
    class KernSubtableUnknown : KernSubtable
    {
        [Length(0, FieldValueKind.Path, "..\\length", "Sub:6")]
        [FieldName(0, null)]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint8> Paddings;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class KernSubtableFormat0 : KernSubtable
    {
        [Description(0, "This gives the number of kerning pairs in the table.")]
        public uint16 nPairs;

        [Description(0, "The largest power of two less than or equal to the value of nPairs, multiplied by the size in bytes of an entry in the table.")]
        public uint16 searchRange;

        [Description(0, "This is calculated as log2 of the largest power of two less than or equal to the value of nPairs. This value indicates how many iterations of the search loop will have to be made")]
        public uint16 entrySelector;

        [Description(0, "The value of nPairs minus the largest power of two less than or equal to nPairs, and then multiplied by the size in bytes of an entry in the table.")]
        public uint16 rangeShift;

        [Count(0, FieldValueKind.Path, "nPairs")]
        [Description(0, "Array of KernPair records.")]
        public IList<KernPair> kernPairs;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class KernPair
    {
        [Description(0, "The glyph index for the left-hand glyph in the kerning pair.")]
        public uint16 left;

        [Description(0, "The glyph index for the right-hand glyph in the kerning pair.")]
        public uint16 right;

        [Description(0, "The kerning value for the above pair, in FUnits.If this value is greater than zero, the characters will be moved apart. If this value is less than zero, the character will be moved closer together.")]
        public FWORD value;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class KernSubtableFormat2 : KernSubtable
    {
        [Description(0, "The width, in bytes, of a row in the table.")]
        public uint16 rowWidth;

        [TableType(typeof(KernClassTable))]
        [Description(0, "Offset from beginning of this subtable to left-hand class table.")]
        public Offset16 leftClassOffset;

        [TableType(typeof(KernClassTable))]
        [Description(0, "Offset from beginning of this subtable to right-hand class table.")]
        public Offset16 rightClassOffset;

        [TableType(typeof(FWORD[]))]
        [Description(0, "Offset from beginning of this subtable to the start of the kerning array.")]
        public Offset16 kerningArrayOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("ClassTable")]
    class KernClassTable
    {
        [Description(0, "First glyph in class range.")]
        public uint16 firstGlyph;

        [Description(0, "Number of glyph in class range.")]
        public uint16 nGlyphs;

        [Count(0, FieldValueKind.Path, "nGlyphs")]
        public IList<uint16> ClassValue;
    }

#pragma warning restore IDE1006
}
