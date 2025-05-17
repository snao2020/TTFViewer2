// ver 1.9.q
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("OS/2")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(OS2TableVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(OS2TableVersion1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(OS2TableVersion2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(OS2TableVersion3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [ClassTypeCondition(typeof(OS2TableVersion4), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "4")]
    [ClassTypeCondition(typeof(OS2TableVersion5), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "5")]
    [Invalid]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table")]
    [BaseName("OS/2")]
    class OS2Table
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The version number for the OS/2 table: 0x0000 to 0x0005")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table, Version 5")]
    [BaseName("OS/2")]
    class OS2TableVersion5
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The version number for the OS/2 table: 0x0005")]
        public uint16 version;

        [Description(0, "the arithmetic average of the escapement(width) of all non - zero width glyphs in the font")]
        public FWORD xAvgCharWidth;

        [Description(0, typeof(OS2WeightClass))]
        public uint16 usWeightClass;

        [Description(0, typeof(OS2WidthClass))]
        public uint16 usWidthClass;

        [Description(0, DescriptionKind.Method, "fsTypeDescription")]
        public uint16 fsType;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutPosition;

        [Description(0, "This parameter is a classification of font-family design.")]
        public int16 sFamilyClass;

        [Count(0, FieldValueKind.Unsigned, "10")]
        [Description(0, "Additional specifications are required for PANOSE to classify non-Latin charactersets.")]
        [Description(1, DescriptionKind.Method, "panoseItemDescription")]
        public IList<uint8> panose;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange1; // Bits 0–31

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange2; // Bits 32–63

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange3; // Bits 64–95

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange4; // Bits 96–127

        [Description(0, "The four-character identifier for the vendor of the given type face.")]
        public Tag achVendID;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, typeof(FontSelectionFlags))]
        public uint16 fsSelection;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usFirstCharIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usLastCharIndex;

        public FWORD sTypoAscender;

        public FWORD sTypoDescender;

        public FWORD sTypoLineGap;

        public UFWORD usWinAscent;

        public UFWORD usWinDescent;

        public uint32 ulCodePageRange1; // Bits 0–31

        public uint32 ulCodePageRange2; // Bits 32–63

        public FWORD sxHeight;

        public FWORD sCapHeight;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usDefaultChar;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usBreakChar;

        public uint16 usMaxContext;

        [Description(0, "TWIPS")]
        public uint16 usLowerOpticalPointSize;

        [Description(0, "TWIPS")]
        public uint16 usUpperOpticalPointSize;

        static string fsTypeDescription(IItemValueService ivp)
            => OS2Helper.fsTypeDescription(ivp);

        static string panoseItemDescription(IItemValueService ivp)
            => OS2Helper.panoseItemDescription(ivp);
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table, Version 4")]
    [BaseName("OS/2")]
    class OS2TableVersion4 : OS2TableVersion3
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table, Version 3")]
    [BaseName("OS/2")]
    class OS2TableVersion3 : OS2TableVersion2
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table, Version 2")]
    [BaseName("OS/2")]
    class OS2TableVersion2
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The version number for the OS/2 table: 0x0002 to 0x0004")]
        public uint16 version;

        [Description(0, "the arithmetic average of the escapement(width) of all non - zero width glyphs in the font")]
        public FWORD xAvgCharWidth;

        [Description(0, typeof(OS2WeightClass))]
        public uint16 usWeightClass;

        [Description(0, typeof(OS2WidthClass))]
        public uint16 usWidthClass;

        [Description(0, DescriptionKind.Method, "fsTypeDescription")]
        public uint16 fsType;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutPosition;

        [Description(0, "This parameter is a classification of font-family design.")]
        public int16 sFamilyClass;

        [Count(0, FieldValueKind.Unsigned, "10")]
        [Description(0, "Additional specifications are required for PANOSE to classify non-Latin charactersets.")]
        [Description(1, DescriptionKind.Method, "panoseItemDescription")]
        public IList<uint8> panose;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange1; // Bits 0–31

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange2; // Bits 32–63

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange3; // Bits 64–95

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange4; // Bits 96–127

        [Description(0, "The four-character identifier for the vendor of the given type face.")]
        public Tag achVendID;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, typeof(FontSelectionFlags))]
        public uint16 fsSelection;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usFirstCharIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usLastCharIndex;

        public FWORD sTypoAscender;

        public FWORD sTypoDescender;

        public FWORD sTypoLineGap;

        public UFWORD usWinAscent;

        public UFWORD usWinDescent;

        public uint32 ulCodePageRange1; // Bits 0–31

        public uint32 ulCodePageRange2; // Bits 32–63

        public FWORD sxHeight;

        public FWORD sCapHeight;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usDefaultChar;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usBreakChar;

        public uint16 usMaxContext;

        static string fsTypeDescription(IItemValueService ivp)
            => OS2Helper.fsTypeDescription(ivp);

        static string panoseItemDescription(IItemValueService ivp)
            => OS2Helper.panoseItemDescription(ivp);
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table, Version 1")]
    [BaseName("OS/2")]
    class OS2TableVersion1
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The version number for the OS/2 table: 0x0001")]
        public uint16 version;

        [Description(0, "the arithmetic average of the escapement(width) of all non - zero width glyphs in the font")]
        public FWORD xAvgCharWidth;

        [Description(0, typeof(OS2WeightClass))]
        public uint16 usWeightClass;

        [Description(0, typeof(OS2WidthClass))]
        public uint16 usWidthClass;

        [Description(0, DescriptionKind.Method, "fsTypeDescription")]
        public uint16 fsType;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutPosition;

        [Description(0, "This parameter is a classification of font-family design.")]
        public int16 sFamilyClass;

        [Count(0, FieldValueKind.Unsigned, "10")]
        [Description(0, "Additional specifications are required for PANOSE to classify non-Latin charactersets.")]
        [Description(1, DescriptionKind.Method, "panoseItemDescription")]
        public IList<uint8> panose;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange1; // Bits 0–31

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange2; // Bits 32–63

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange3; // Bits 64–95

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange4; // Bits 96–127

        [Description(0, "The four-character identifier for the vendor of the given type face.")]
        public Tag achVendID;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, typeof(FontSelectionFlags))]
        public uint16 fsSelection;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usFirstCharIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usLastCharIndex;

        public FWORD sTypoAscender;

        public FWORD sTypoDescender;

        public FWORD sTypoLineGap;

        public UFWORD usWinAscent;

        public UFWORD usWinDescent;

        public uint32 ulCodePageRange1; // Bits 0–31

        public uint32 ulCodePageRange2; // Bits 32–63

        static string fsTypeDescription(IItemValueService ivp)
            => OS2Helper.fsTypeDescription(ivp);

        static string panoseItemDescription(IItemValueService ivp)
            => OS2Helper.panoseItemDescription(ivp);
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("OS/2 — OS/2 and Windows Metrics Table, Version 0")]
    [BaseName("OS/2")]
    class OS2TableVersion0
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The version number for the OS/2 table: 0x0000")]
        public uint16 version;

        [Description(0, "the arithmetic average of the escapement(width) of all non - zero width glyphs in the font")]
        public FWORD xAvgCharWidth;

        [Description(0, typeof(OS2WeightClass))]
        public uint16 usWeightClass;

        [Description(0, typeof(OS2WidthClass))]
        public uint16 usWidthClass;

        [Description(0, DescriptionKind.Method, "fsTypeDescription")]
        public uint16 fsType;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySubscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptXOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD ySuperscriptYOffset;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutSize;

        [Description(0, DescriptionKind.EmRelative, null)]
        public FWORD yStrikeoutPosition;

        [Description(0, "This parameter is a classification of font-family design.")]
        public int16 sFamilyClass;

        [Count(0, FieldValueKind.Unsigned, "10")]
        [Description(0, "Additional specifications are required for PANOSE to classify non-Latin charactersets.")]
        [Description(1, DescriptionKind.Method, "panoseItemDescription")]
        public IList<uint8> panose;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange1; // Bits 0–31

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange2; // Bits 32–63

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange3; // Bits 64–95

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint32 ulUnicodeRange4; // Bits 96–127

        [Description(0, "The four-character identifier for the vendor of the given type face.")]
        public Tag achVendID;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, typeof(FontSelectionFlags))]
        public uint16 fsSelection;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usFirstCharIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        public uint16 usLastCharIndex;

        public FWORD sTypoAscender;

        public FWORD sTypoDescender;

        public FWORD sTypoLineGap;

        public UFWORD usWinAscent;

        public UFWORD usWinDescent;

        static string fsTypeDescription(IItemValueService ivp)
            => OS2Helper.fsTypeDescription(ivp);

        static string panoseItemDescription(IItemValueService ivp)
            => OS2Helper.panoseItemDescription(ivp);
    }


    [TypeName("Weight class")]
    enum OS2WeightClass
    {
        Thin = 100, // FW_THIN
        [FieldName(0, "Extra-light(Ultra-light)")]
        Extra_light = 200,// FW_EXTRALIGHT
        Light = 300, // FW_LIGHT
        [FieldName(0, "Normal(Regular)")]
        Normal = 400, // FW_NORMAL
        Medium = 500, // FW_MEDIUM
        [FieldName(0, "Semi-bold(Demi-bold)")]
        Semi_bold = 600, // FW_SEMIBOLD
        Bold = 700, //FW_BOLD
        [FieldName(0, "Extra-bold(Ultra-bold)")]
        Extra_bold = 800, // FW_EXTRABOLD
        [FieldName(0, "Black(Heavy)")]
        Black = 900, // FW_BLACK
    }

    [TypeName("Width class")]
    enum OS2WidthClass
    {
        [FieldName(0, "Ultra-condensed")]
        Ultra_condensed = 1, // FWIDTH_ULTRA_CONDENSED 50
        [FieldName(0, "Extra-condensed")]
        Extra_condensed = 2, // FWIDTH_EXTRA_CONDENSED 62.5
        Condensed = 3, // FWIDTH_CONDENSED 75
        [FieldName(0, "Semi-condensed")]
        Semi_condensed = 4, // FWIDTH_SEMI_CONDENSED 87.5
        [FieldName(0, "Medium(normal)")]
        Medium = 5, // FWIDTH_NORMAL 100
        [FieldName(0, "Semi-expanded")]
        Semi_expanded = 6,// FWIDTH_SEMI_EXPANDED 112.5
        Expanded = 7, // FWIDTH_EXPANDED 125
        [FieldName(0, "Extra-expanded")]
        Extra_expanded = 8, // FWIDTH_EXTRA_EXPANDED 150
        [FieldName(0, "Ultra-expanded")]
        Ultra_expanded = 9, // FWIDTH_ULTRA_EXPANDED 200
    }

    [Flags]
    [TypeName("permissions")]
    enum OS2Typeflags_permissions
    {
        // Mask = 0x000F
        [FieldName(0, "Installable embedding")]
        Installable = 1,
        [FieldName(0, "Restricted License embedding")]
        Restricted = 2,
        [FieldName(0, "Preview & Print embedding")]
        Preview = 4,
        [FieldName(0, "Editable embedding")]
        Editable = 8,
    }


    [Flags]
    [TypeName("Type flags.")]
    enum OS2Typeflags
    {
        // 4 – 7 Reserved, must be zero
        [FieldName(0, "No subsetting")]
        Bit8 = 0x0100,
        [FieldName(0, "Bitmap embedding only")]
        Bit9 = 0x0200,
        //10 –15 Reserved, must be zero
    }

    [Flags]
    [TypeName("Font selection flags")]
    enum FontSelectionFlags
    {
        ITALIC = 0x0001,
        UNDERSCORE = 0x0002,
        NEGATIVE = 0x0004,
        OUTLINED = 0x0008,
        STRIKEOUT = 0x0010,
        BOLD = 0x0020,
        REGULAR = 0x0040,
        USE_TYPO_METRICS = 0x0080,
        WWS = 0x0100,
        OBLIQUE = 0x0200,
        //10–15<reserved> Reserved; set to 0.
        }


//-----------------------------------------------------------

    static class OS2Helper
    {
        public static string fsTypeDescription(IItemValueService ivp)
        {
            Int32 typeFlags = (uint16)ivp.Value;
            var permissionMask = 0x000F;

            var permission = typeFlags & permissionMask;
            var permissionText = ItemValueHelper.GetEnumItemName(typeof(OS2Typeflags_permissions), permission);

            var flags = typeFlags & (~permissionMask);
            var flagsText = ItemValueHelper.GetEnumItemName(typeof(OS2Typeflags), flags);

            //var result = String.Join("|", new[] { permissionText, flagsText });
            String result;
            if (permissionText != null && flagsText != null)
                result = $"{permissionText} | {flagsText}";
            else
                result = $"{permissionText}{flagsText}";
            return result;
        }

        public static string panoseItemDescription(IItemValueService ivp)
        {
            var texts = new[]
            {
                "bFamilyType",
                "bSerifStyle",
                "bWeight",
                "bProportion",
                "bContrast",
                "bStrokeVariation",
                "bArmStyle",
                "bLetterform",
                "bMidline",
                "bXHeight",
            };

            if (TablePathHelper.GetIndex(ivp.Name) is Int32 index)
                return texts[index];
            return null;
        }
    }

#pragma warning restore IDE1006
}
