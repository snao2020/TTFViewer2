// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("CPAL")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(CPALTable_Version0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0000")]
    [ClassTypeCondition(typeof(CPALTable_Version1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001")]
    [Invalid]
    [TypeName("CPAL — Color Palette Table")]
    [BaseName("CPAL")]
    class CPALTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("CPAL — Color Palette Table: version 0")]
    [BaseName("CPAL")]
    class CPALTable_Version0
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(=0)")]
        public uint16 version;

        [Description(0, "Number of palette entries in each palette")]
        public uint16 numPaletteEntries;

        [Description(0, "Number of palettes in the table")]
        public uint16 numPalettes;

        [Description(0, "Total number of color records, combined for all palettes")]
        public uint16 numColorRecords;

        [TableType(typeof(IList<ColorRecord>))]//, CreateModelFlags=CreateModelFlags.Buffered)]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "numColorRecords")]
        [Description(0, "Offset from the beginning of CPAL table to the first ColorRecord")]
        public Offset32 colorRecordsArrayOffset;

        [Count(0, FieldValueKind.Path, "numPalettes")]
        [Description(0, "Index of each palette’s first color record in the combined color record array")]
        public IList<uint16> colorRecordIndices;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("CPAL — Color Palette Table: version 1")]
    class CPALTable_Version1
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number (= 1)")]
        public uint16 version;

        [Description(0, "Number of palette entries in each palette")]
        public uint16 numPaletteEntries;

        [Description(0, "Number of palettes in the table")]
        public uint16 numPalettes;

        [Description(0, "Total number of color records, combined for all palettes")]
        public uint16 numColorRecords;

        [TableType(typeof(IList<ColorRecord>))]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "numColorRecords")]
        [Description(0, "Offset from the beginning of CPAL table to the first ColorRecord")]
        public Offset32 colorRecordsArrayOffset;

        [Count(0, FieldValueKind.Path, "numPalettes")]
        [Description(0, "Index of each palette’s first color record in the combined color record array")]
        public IList<uint16> colorRecordIndices;

        [TableType(typeof(PaletteTypeArray))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset from the beginning of CPAL table to the Palette Type Array. Set to 0 if no array is provided")]
        public Offset32 paletteTypesArrayOffset;

        [TableType(typeof(PaletteLabelsArray))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset from the beginning of CPAL table to the Palette Labels Array. Set to 0 if no array is provided")]
        public Offset32 paletteLabelsArrayOffset;

        [TableType(typeof(PaletteEntryLabelArray))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset from the beginning of CPAL table to the Palette Entry Label Array.Set to 0 if no array is provided")]
        public Offset32 paletteEntryLabelsArrayOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class ColorRecord
    {
        [Description(0, "Blue value(B0)")]
        public uint8 blue;

        [Description(0, "Green value(B1)")]
        public uint8 green;

        [Description(0, "Red value(B2)")]
        public uint8 red;

        [Description(0, "Alpha value(B3)")]
        public uint8 alpha;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Palette types array")]
    class PaletteTypeArray
    {
        [Count(0, FieldValueKind.OffsetSource, "numPalettes")]
        [Description(0, "Array of 32-bit flag fields that describe properties of each palette.See below for details")]
        [Description(1, DescriptionKind.Method, "paletteTypeText")]
        public IList<uint32> paletteTypes;

        static string paletteTypeText(IItemValueService ivp)
        {
            if (ivp.Value is uint32 flags)
            {
                if (flags == 0)
                    return "<none>";

                UInt32 f = flags & ~(UInt32)CPALFlags.Reserved;
                UInt32 r = flags ^ f;
                var result = ItemValueHelper.GetEnumItemName(typeof(CPALFlags), (Int32)f);
                if (r != 0)
                    result += $"|{r:x08}";
                return result;
            }
            return null;
        }
    }


    [Flags]
    [TypeName("")]
    enum CPALFlags
    {
        USABLE_WITH_LIGHT_BACKGROUND = 0x0001, // Bit 0: palette is appropriate to use when displaying the font on a light background such as white.
        USABLE_WITH_DARK_BACKGROUND = 0x0002, // Bit 1: palette is appropriate to use when displaying the font on a dark background such as black.
        Reserved = 0xFFFC, // Reserved for future use — set to 0.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Palette labels array")]
    class PaletteLabelsArray
    {
        [Count(0, FieldValueKind.OffsetSource, "numPalettes")]
        [Description(0, "Array of 'name' table IDs(typically in the font-specific name ID range) that specify user interface strings associated with each palette.Use 0xFFFF if no name ID is provided for a particular palette.")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint16> paletteLabels;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Palette entry label array")]
    class PaletteEntryLabelArray
    {
        [Count(0, FieldValueKind.OffsetSource, "numPaletteEntries")]
        [Description(0, "Array of 'name' table IDs(typically in the font-specific name ID range) that specify user interface strings associated with each palette entry, e.g. “Outline”, “Fill”. This set of palette entry labels applies to all palettes in the font.Use 0xFFFF if no name ID is provided for a particular palette entry")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public IList<uint16> paletteEntryLabels;
    }
#pragma warning restore IDE1006
}
