namespace TTFViewer.DataTypes
{
    [TypeName("CFF DICT Operators")]
    public enum CFFDICTOperators
    {
        //Name              FieldValue           Operand(s)        Default, notes
        //Table 9 Top DICT Operator Entries
        version = 0,                        // SID           –, FontInfo
        Notice = 1,                         // SID           –, FontInfo
        Copyright = 12 << 8 | 0,            // SID           –, FontInfo
        FullName = 2,                       // SID           –, FontInfo
        FamilyName = 3,                     // SID           –, FontInfo
        Weight = 4,                         // SID           –, FontInfo
        isFixedPitch = 12 << 8 | 1,         // boolean       0 (false), FontInfo
        ItalicAngle = 12 << 8 | 2,          // number        0, FontInfo
        UnderlinePosition = 12 << 8 | 3,    // number        –100, FontInfo
        UnderlineThickness = 12 << 8 | 4,   // number        50, FontInfo
        PaintType = 12 << 8 | 5,            // number        0
        CharstringType = 12 << 8 | 6,       // number        2
        FontMatrix = 12 << 8 | 7,           // array         0.001 0 0 0.001 0 0
        UniqueID = 13,                      // number        –
        FontBBox = 5,                       // array         0 0 0 0
        StrokeWidth = 12 << 8 | 8,          // number        0
        XUID = 14,                          // array         –
        charset = 15,                       // number        0, charset offset(0)
        Encoding = 16,                      // number        0, encoding offset(0)
        CharStrings = 17,                   // number        –, CharStrings offset(0)
        Private = 18,                       // number number Private DICT size and offset(0)
        SyntheticBase = 12 << 8 | 20,       // number        –, synthetic base font index
        PostScript = 12 << 8 | 21,          // SID           –, embedded PostScript language code
        BaseFontName = 12 << 8 | 22,        // SID           –, (added as needed by Adobe-based technology)
        BaseFontBlend = 12 << 8 | 23,       // delta         –, (added as needed by Adobe-based technology)   }

        //Table 10 CIDFont Operator Extensions
        ROS = 12 << 8 | 30,                 // SID SID number–, Registry Ordering Supplement
        CIDFontVersion = 12 << 8 | 31,      // number        0
        CIDFontRevision = 12 << 8 | 32,     // number        0
        CIDFontType = 12 << 8 | 33,         // number        0
        CIDCount = 12 << 8 | 34,            // number        8720
        UIDBase = 12 << 8 | 35,             // number        –

        [FieldName(0, "FontDICTINDEXOffset")]
        FDArray = 12 << 8 | 36,             // number        –, Font DICT(FD) INDEX offset(0)
        FDSelect = 12 << 8 | 37,            // number        –, FDSelect offset(0)
        FontName = 12 << 8 | 38,            // SID           –, FD FontName


        // Private DICT Operators
        BlueValues = 6,                     // delta         –
        OtherBlues = 7,                     // delta         –
        FamilyBlues = 8,                    // delta         –
        FamilyOtherBlues = 9,               // delta         –
        BlueScale = 12 << 8 | 9,            // number        0.039625
        BlueShift = 12 << 8 | 10,           // number        7
        BlueFuzz = 12 << 8 | 11,            // number        1
        StdHW = 10,                         // number        –
        StdVW = 11,                         // number        –
        StemSnapH = 12 << 8 | 12,           // delta         –
        StemSnapV = 12 << 8 | 13,           // delta         –
        ForceBold = 12 << 8 | 14,           // boolean       false
        LanguageGroup = 12 << 8 | 17,       // number        0
        ExpansionFactor = 12 << 8 | 18,     // number        0.06
        initialRandomSeed = 12 << 8 | 19,   // number        0
        Subrs = 19,                         // number        –, Offset(self) to local subrs
        defaultWidthX = 20,                 // number        0, see below
        nominalWidthX = 21,                 // number        0, see below
    }


    [TypeName("CFF2 DICT Operators")]
    public enum CFF2DICTOperators
    {
        // 0 to 5 	<reserved> 	
        BlueValues = 6,
        OtherBlues = 7,
        FamilyBlues = 8,
        FamilyOtherBlues = 9,
        StdHW = 10,
        StdVW = 11,
        //escape = 12, // First byte of a 2-byte operator.
        // 13 to 16 	<reserved> 	

        [FieldName(0, "CharStringINDEXOffset")]
        CharStrings = 17,

        [FieldName(0, "PrivateDICTOffset")]
        Private = 18,

        [FieldName(0, "LocalSubrINDEXOffset")]
        Subrs = 19,

        // 20 to 21 	<reserved> 	
        vsindex = 22,
        blend = 23,

        [FieldName(0, "VariationStoreOffset")]
        vstore = 24,

        // 25 to 27 <reserved>
        // 28 <numbers> First byte of a 3-byte sequence specifying a signed integer value (following two bytes are an int16).
        // 29 <numbers> 	First byte of a 5-byte sequence specifying a signed integer value(following four bytes are an int32).
        // BCD = 30,
        // 31 	<reserved> 	
        // 32 to 246 <numbers>
        // 247 to 254 <numbers> First byte of a 2-byte sequence specifying a number.
        // 255 <reserved>         

        // 12 0 to 12 6 	<reserved> 	
        FontMatrix = 12 << 8 | 7,
        // 12 8 	<reserved> 	
        BlueScale = 12 << 8 | 9,
        BlueShift = 12 << 8 | 10,
        BlueFuzz = 12 << 8 | 11,
        StemSnapH = 12 << 8 | 12,
        StemSnapV = 12 << 8 | 13,
        // 12 14 to 12 16 	<reserved> 	
        LanguageGroup = 12 << 8 | 17,
        ExpansionFactor = 12 << 8 | 18,
        // 12 19 to 12 35 	<reserved> 	

        [FieldName(0, "FontDICTINDEXOffset")]
        FDArray = 12 << 8 | 36,   // offset
        
        [FieldName(0, "FontDICTSelectOffset")]
        FDSelect = 12 << 8 | 37,  // offset

        // 12 38 to 12 255  <reserved>
    }

    enum CFF2DICTOperators2
    {
        // TopDICT
        CharStringINDEXOffset = 0x11, // 17  yes —
        VariationStoreOffset = 0x18, // 24  only for fonts with variations —
        FontDICTINDEXOffset = 0x0c24, // 12,36  yes —
        FontDICTSelectOffset = 0x0c25, // 12,37  no —
        FontMatrix = 0x0c07, // 12,7  no 0.001 0 0 0.001 0 

        // FontDICT
        PrivateDICTOffset = 0x12, //(18)

        // PrivateDICT
        LocalSubrINDEXOffset = 0x13, // 19  — subroutines no
        vsindex = 0x16, // 22  0 variation no
        blend = 0x17, // 23  — variation yes
        BlueValues = 0x06, // 6  — hinting yes
        OtherBlues = 0x07, // 7  — hinting yes
        FamilyBlues = 0x08, // 8  — hinting no
        FamilyOtherBlues = 0x09, // 9  — hinting no
        BlueScale = 0x0c09, // 12,9  0.039625 hinting no
        BlueShift = 0x0c0a, // 12,10  7 hinting no
        BlueFuzz = 0x0c0b, // 12,11  1 hinting no
        StdHW = 0x0a, // 10  — hinting yes
        StdVW = 0x0b, // 11  — hinting yes
        StemSnapH = 0x0c0c, // 12,12  — hinting yes
        StemSnapV = 0x0c0d,// 12,13  — hinting yes
        LanguageGroup = 0x0c11, // 12,17  0 hinting no
        ExpansionFactor = 0x0c12, // 12,18  0.06 hinting no
    }
}
