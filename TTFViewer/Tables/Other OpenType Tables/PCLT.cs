// ver 1.9.1
using System;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("PCLT")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(PCLTTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("PCLT - PCL 5 Table")]
    [BaseName("PCLT")]
    class PCLTTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The current PCLT table version is 1.0.")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The current PCLT table version is 1.0.")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("PCLT - PCL 5 Table")]
    [BaseName("PCLT")]
    class PCLTTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The current PCLT table version is 1.0.")]
        public uint16 majorVersion; // The current PCLT table version is 1.0.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The current PCLT table version is 1.0.")]
        public uint16 minorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 fontNumber;

        [Description(0, "The width of the space in FUnits")]
        public uint16 pitch;

        public uint16 xHeight;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "styleDescription")]
        public uint16 style;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "typeFamilyDescription")]
        public uint16 typeFamily;

        public uint16 capHeight;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 symbolSet;

        [Count(0, FieldValueKind.Unsigned, "16")]
        [Description(0, DescriptionKind.AsciiText, null)]
        public int8[] typeface;

        [Count(0, FieldValueKind.Unsigned, "8")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public int8[] characterComplement;

        [Count(0, FieldValueKind.Unsigned, "6")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.AsciiText, null)]
        public int8[] fileName;

        public int8 strokeWeight;

        [Description(0, typeof(WidthType))]
        public int8 widthType;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "serifStyleDescription")]
        public uint8 serifStyle;

        [FieldName(0, "(reserved—set to 0)")]
        public uint8 reserved;

        static string styleDescription(IItemValueService ivs)
        {
            Int32 style = (uint16)ivs.Value;
            String result = ItemValueHelper.GetEnumItemName(typeof(PCLTStyleStructure), style & 0x03e0);
            result += ", " + ItemValueHelper.GetEnumItemName(typeof(PCLTStyleWidth), style & 0x001c);
            result += ", " +ItemValueHelper.GetEnumItemName(typeof(PCLTStylePosture), style & 0x0003);
            return result;
        }

        static string typeFamilyDescription(IItemValueService ivs)
        {
            Int32 typeFamily = (uint16)ivs.Value;
            String result = ItemValueHelper.GetEnumItemName(typeof(PCLTVendorCodes), typeFamily & 0xf000);
            result += $" typeface family code=0x{typeFamily & 0x0fff:x3}";
            return result;
        }

        static String serifStyleDescription(IItemValueService ivs)
        {
            Int32 style = (uint8)ivs.Value;
            String result = ItemValueHelper.GetEnumItemName(typeof(Bottom6bit), style & 0x3f);
            result += ", " + ItemValueHelper.GetEnumItemName(typeof(Top2bit), style & 0xc0);
            return result;
        }
    }


    //Structure(bits 5-9)
    [TypeName("Structure")]
    enum PCLTStyleStructure
    { 
        Solid = 0 << 5,
        Outline = 1 << 5,
        Inline = 2 << 5,
        Contour = 3 << 5,
        [FieldName(0, "Solid with shadow")]
        Solid_with_shadow = 4 << 5,
        [FieldName(0, "Outline with shadow")]
        Outline_with_shadow = 5 << 5,
        [FieldName(0, "Inline with shadow")]
        Inline_with_shadow = 6 << 5,
        [FieldName(0, "Contour, or edged, with shadow")]
        Contour_with_shadow = 7 << 5,
        [FieldName(0, "Pattern filled")]
        Pattern_filled = 8 << 5,
        [FieldName(0, "Pattern filled #1")]
        Pattern_filled_1 = 9 << 5,
        [FieldName(0, "Pattern filled #2")]
        Pattern_filled_2 = 10 << 5,
        [FieldName(0, "Pattern filled #3")]
        Pattern_filled_3 = 11 << 5,
        [FieldName(0, "Pattern filled with shadow")]
        Pattern_filled_with_shadow = 12 << 5,
        [FieldName(0, "Pattern filled with shadow #1")]
        Pattern_filled_with_shadow_1 = 13 << 5,
        [FieldName(0, "Pattern filled with shadow #2")]
        Pattern_filled_with_shadow_2 = 14 << 5,
        [FieldName(0, "Pattern filled with shadow #3")]
        Pattern_filled_with_shadow_3 = 15 << 5,
        Inverse = 16 << 5,
        [FieldName(0, "Inverse with border")]
        Inverse_with_border = 17 << 5,
        //18-31 reserved
    }


    //Width(bits 2-4)
    [TypeName("Width")]
    enum PCLTStyleWidth
    {
        normal = 0 << 2,
        condensed = 1 << 2,
        compressed = 2 << 2,
        [FieldName(0, "extra compressed")]
        extra_compressed = 3 << 2,
        [FieldName(0, "ultra compressed")]
        ultra_compressed = 4 << 2,
        reserved = 5 << 2,
        [FieldName(0, "expanded, extended")]
        expanded, extended = 6 << 2,
        [FieldName(0, "extra expanded")]
        extra_expanded = 7 << 2,
    }


    //Posture(bits 0-1) [0x0000,0x0003]
    [TypeName("Posture")]
    enum PCLTStylePosture
    { 
        upright = 0,
        [FieldName(0, "oblique, italic")]
        oblique = 1,
        [FieldName(0, "alternate italic")]
        alternate = 2,
        reserved = 3,
    }


    [TypeName("Vendor Codes")] //(bits 12-15)
    enum PCLTVendorCodes
    {
        reserved = 0 << 12,
        [FieldName(0, "Agfa Corporation")]
        Agfa = 1 << 12,
        [FieldName(0, "Bitstream Inc")]
        Bitstream = 2 << 12,
        [FieldName(0, "Linotype Company")]
        Linotype = 3 << 12,
        [FieldName(0, "Monotype Typography Ltd")]
        Monotype= 4 << 12,
        [FieldName(0, "Adobe Systems")]
        Adobe = 5 << 12,
        [FieldName(0, "font repackagers")]
        repackagers = 6 << 12,
        [FieldName(0, "vendors of unique typefaces")]
        unique_typefaces = 7 << 12,
        //8 - 15 reserved
    }


    enum WidthType
    {
        [FieldName(0, "Ultra Compressed")]
        Ultra_Compressed = -5,
        [FieldName(0, "Extra Compressed")]
        Extra_Compressed = -4,
        [FieldName(0, "Compressed, or Extra Condensed")]
        Compressed = -3,
        Condensed = -2,
        Normal = 0,
        Expanded = 2,
        [FieldName(0, "Extra Expanded")]
        Extra_Expanded = 3,
    }


    [TypeName("SerifStyle Bottom 6 bit")]
    enum Bottom6bit
    { 
        [FieldName(0, "Sans Serif Square")]
        Sans_Serif_Square = 0,
        [FieldName(0, "Sans Serif Round")]
        Sans_Serif_Round = 1,
        [FieldName(0, "Serif Line")]
        Serif_Line = 2,
        [FieldName(0, "Serif Triangle")]
        Serif_Triangle = 3,
        [FieldName(0, "Serif Swath")]
        Serif_Swath = 4,
        [FieldName(0, "Serif Block")]
        Serif_Block = 5,
        [FieldName(0, "Serif Bracket")]
        Serif_Bracket = 6,
        [FieldName(0, "Rounded Bracket")]
        Rounded_Bracket = 7,
        [FieldName(0, "Flair Serif, Modified Sans")]
        Flair_Serif = 8,
        [FieldName(0, "Script Nonconnecting")]
        Script_Nonconnecting = 9,
        [FieldName(0, "Script Joining")]
        Script_Joining = 10,
        [FieldName(0, "Script Calligraphic")]
        Script_Calligraphic = 11,
        [FieldName(0, "Script Broken Letter")]
        Script_Broken_Letter = 12,
    }

    [TypeName("SerifStyle Top 2 bit")]
    enum Top2bit
    { 
        [FieldName(0, "reserved")]
        reserved0 = 0 << 6,
        [FieldName(0, "Sans Serif/Monoline")]
        Sans_Serif = 1 << 6,
        [FieldName(0, "Serif/Contrasting")]
        Serif = 2 << 6,
        [FieldName(0, "reserved")]
        reserved1 = 3 << 6,
    }
#pragma warning restore IDE1006
}
