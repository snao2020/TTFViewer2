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
    [FontTable("name")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(nameTableVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(nameTableVersion1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [TypeName("Naming table")]
    [BaseName("name")]
    class nameTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(=0 or 1)")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Naming table version 0")]
    [BaseName("name")]
    class nameTableVersion0
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(=0)")]
        public uint16 version;

        [Description(0, "Number of name records")]
        public uint16 count;

        [TypeName("Offset16")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset to start of string storage (from start of table)")]
        public uint16 storageOffset;

        [Count(0, FieldValueKind.Path, "count")]
        [Description(0, "The name records where count is the number of records")]
        public IList<NameRecord> nameRecord;
        //(Variable)      Storage for the actual string data.    
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Naming table version 1")]
    [BaseName("name")]
    class nameTableVersion1
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(=0)")]
        public uint16 version;

        [Description(0, "Number of name records")]
        public uint16 count;

        [TypeName("Offset16")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset to start of string storage (from start of table)")]
        public uint16 storageOffset;

        [Count(0, FieldValueKind.Path, "count")]
        [Description(0, "The name records where count is the number of records")]
        public IList<NameRecord> nameRecord;

        [Description(0, "Number of language-tag records")]
        public uint16 langTagCount;

        [Count(0, FieldValueKind.Path, "langTagCount")]
        [Description(0, "The language-tag records where langTagCount is the number of records.")]
        public IList<LangTagRecord> langTagRecord;

        // (Variable) Storage for the actual string data.    
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class LangTagRecord
    {
        [Description(0, "Language-tag string length (in bytes)")]
        public uint16 length;

        [TableType(typeof(uint8[]))]
        [TablePosition("\\", FieldValueKind.Path, "\\storageOffset")]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "length")]
        [Description(0, "Language-tag string offset from start of storage area (in bytes)")]
        public Offset16 langTagOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class NameRecord
    {
        [Description(0, typeof(name_PlatformIDs))]
        public uint16 platformID;

        [Description(0, DescriptionKind.Method, "encodingIDDescription")]
        public uint16 encodingID;

        [Description(0, DescriptionKind.Method, "languageIDDescription")]
        public uint16 languageID;

        [Description(0, typeof(NameIDs))]
        public uint16 nameID;

        [Description(0, "String length (in bytes)")]
        public uint16 length;
      
        [TableType(typeof(uint8[]))]
        [TablePosition("\\", FieldValueKind.Path, "..\\..\\storageOffset")]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "length")]
        [Description(0, "String offset from start of storage area(in bytes)")]
        public Offset16 stringOffset;

        static string encodingIDDescription(IItemValueService ivp)
        {
            string result = null;
            if (ivp.Parent.Value is NameRecord nr)
            {
                switch (nr.platformID)
                {
                    case (Int32)name_PlatformIDs.Unicode:
                        result = ItemValueHelper.GetEnumItemName(typeof(name_UnicodeEncodingIDs), nr.encodingID);
                        break;
                    case (Int32)name_PlatformIDs.Macintosh:
                        result = ItemValueHelper.GetEnumItemName(typeof(name_MacintoshEncodingIDs), nr.encodingID);
                        break;
                    case (Int32)name_PlatformIDs.Windows:
                        result = ItemValueHelper.GetEnumItemName(typeof(name_WindowsEncodingIDs), nr.encodingID);
                        break;
                }
            }
            return result ?? "<unknown>";
        }


        static string languageIDDescription(IItemValueService ivp)
        {
            string result = null;
            if (ivp.Parent.Value is NameRecord nr)
            {
                switch (nr.platformID)
                {
                    case (Int32)name_PlatformIDs.Unicode:
                        result = ItemValueHelper.GetEnumItemName(typeof(UnicodeEncodingIDs), nr.encodingID);
                        break;
                    case (Int32)name_PlatformIDs.Macintosh:
                        result = ItemValueHelper.GetEnumItemName(typeof(name_MacintoshLanguageIDs), nr.languageID);
                        break;
                    case (Int32)name_PlatformIDs.Windows:
                        result = ItemValueHelper.GetEnumItemName(typeof(name_WindowsLanguageIDs), nr.languageID);
                        break;
                }
            }
            return result ?? "<unknown>";
        }
    }


    [TypeName("Name IDs")]
    enum NameIDs
    {
        [FieldName(0, "Copyright notice")]
        Copyright_notice = 0,

        [FieldName(0, "Font Family name")]
        Font_Family_name = 1,

        [FieldName(0, "Font Subfamily name")]
        Font_Subfamily_name = 2,

        [FieldName(0, "Unique font identifier")]
        Unique_font_identifier = 3,

        [FieldName(0, "Full font name")]
        Full_font_name = 4,

        [FieldName(0, "Version string")]
        Version_string = 5,

        [FieldName(0, "PostScript name")]
        PostScript_name = 6,

        [FieldName(0, "Trademark")]
        Trademark = 7,

        [FieldName(0, "Manufacturer Name")]
        Manufacturer_Name = 8,

        [FieldName(0, "Designer")]
        Designer = 9,

        [FieldName(0, "Description")]
        Description = 10,

        [FieldName(0, "URL Vendor")]
        URL_Vendor = 11,

        [FieldName(0, "URL Designer")]
        URL_Designer = 12,

        [FieldName(0, "License Description")]
        License_Description = 13,

        [FieldName(0, "License Info URL")]
        License_Info_URL = 14,

        [FieldName(0, "Reserved")]
        Reserved = 15,

        [FieldName(0, "Typographic Family name")]
        Typographic_Family_name = 16,

        [FieldName(0, "Typographic Subfamily name")]
        Typographic_Subfamily_name = 17,

        [FieldName(0, "Compatible Full(Macintosh only)")]
        Compatible_Full = 18,

        [FieldName(0, "Sample text")]
        Sample_text = 19,

        [FieldName(0, "PostScript CID findfont name")]
        PostScript_CID_findfont_name = 20,

        [FieldName(0, "WWS Family Name")]
        WWS_Family_Name = 21,

        [FieldName(0, "WWS Subfamily Name")]
        WWS_Subfamily_Name = 22,

        [FieldName(0, "Light Background Palette")]
        Light_Background_Palette = 23,

        [FieldName(0, "Dark Background Palette")]
        Dark_Background_Palette = 24,

        [FieldName(0, "Variations PostScript Name Prefix")]
        Variations_PostScript_Name_Prefix = 25,
    }


    [TypeName("Platform IDs")]
    enum name_PlatformIDs
    {
        Unicode = 0, //Various     None
        Macintosh = 1, //Script manager code     Various
        Windows = 3, // Windows encoding Various
    }


    [TypeName("Encoding IDs")]
    enum name_UnicodeEncodingIDs
    { 
        [FieldName(0, "unicode 1.0")]
        Unicode_1_0 = 0, // semantics—deprecated
        [FieldName(0, "nicode 1.1")]
        Unicode_1_1 = 1, // semantics—deprecated
        [FieldName(0, "ISO/IEC 10646")]
        ISO_IEC_10646 = 2, // semantics—deprecated
        [FieldName(0, "Unicode 2.0 BMP only")]
        Unicode_2_0_BMP_only = 3,
        [FieldName(0, "Unicode 2.0 full repertoire")]
        Unicode_2_0_full_repertoire = 4,
    }

    [TypeName("Encoding IDs")]
    enum name_MacintoshEncodingIDs
    {
        Roman = 0,
        Japanese = 1,
        [FieldName(0, "Chinese(Traditional)")]
        Chinese_Traditional = 2,
        Korean = 3,
        Arabic = 4,
        Hebrew = 5,
        Greek = 6,
        Russian = 7,
        RSymbol = 8,
        Devanagari = 9,
        Gurmukhi = 10,
        Gujarati = 11,
        Oriya = 12,
        Bengali = 13,
        Tamil = 14,
        Telugu = 15,
        Kannada = 16,
        Malayalam = 17,
        Sinhalese = 18,
        Burmese = 19,
        Khmer = 20,
        Thai = 21,
        Laotian = 22,
        Georgian = 23,
        Armenian = 24,
        [FieldName(0, "Chinese(Simplified)")]
        Chinese_Simplified = 25,
        Tibetan = 26,
        Mongolian = 27,
        Geez = 28,
        Slavic = 29,
        Vietnamese = 30,
        Sindhi = 31,
        Uninterpreted = 32,
    }


    [TypeName("Macintosh language IDs")]
    enum name_MacintoshLanguageIDs
    {
        English = 0,
        French = 1,
        German = 2,
        Italian = 3,
        Dutch = 4,
        Swedish = 5,
        Spanish = 6,
        Danish = 7,
        Portuguese = 8,
        Norwegian = 9,
        Hebrew = 10,
        Japanese = 11,
        Arabic = 12,
        Finnish = 13,
        Greek = 14,
        Icelandic = 15,
        Maltese = 16,
        Turkish = 17,
        Croatian = 18,
        [FieldName(0, "Chinese(Traditional)")]
        Chinese_Traditional = 19,
        Urdu = 20,
        Hindi = 21,
        Thai = 22,
        Korean = 23,
        Lithuanian = 24,
        Polish = 25,
        Hungarian = 26,
        Estonian = 27,
        Latvian = 28,
        Sami = 29,
        Faroese = 30,
        [FieldName(0, "Farsi/Persian")]
        Farsi_Persian = 31,
        Russian = 32,
        [FieldName(0, "Chinese(Simplified)")]
        Chinese_Simplified = 33,
        Flemish = 34,
        [FieldName(0, "Irish Gaelic")]
        Irish_Gaelic = 35,
        Albanian = 36,
        Romanian = 37,
        Czech = 38,
        Slovak = 39,
        Slovenian = 40,
        Yiddish = 41,
        Serbian = 42,
        Macedonian = 43,
        Bulgarian = 44,
        Ukrainian = 45,
        Byelorussian = 46,
        Uzbek = 47,
        Kazakh = 48,
        [FieldName(0, "Azerbaijani(Cyrillic script)")]
        Azerbaijani_Cyrillic_script = 49,
        [FieldName(0, "Azerbaijani(Arabic script)")]
        Azerbaijani_Arabic_script = 50,
        Armenian = 51,
        Georgian = 52,
        Moldavian = 53,
        Kirghiz = 54,
        Tajiki = 55,
        Turkmen = 56,
        [FieldName(0, "Mongolian(Mongolian script)")]
        Mongolian_Mongolian_script = 57,
        [FieldName(0, "Mongolian(Cyrillic script)")]
        Mongolian_Cyrillic_script = 58,
        Pashto = 59,
        Kurdish = 60,
        Kashmiri = 61,
        Sindhi = 62,
        Tibetan = 63,
        Nepali = 64,
        Sanskrit = 65,
        Marathi = 66,
        Bengali = 67,
        Assamese = 68,
        Gujarati = 69,
        Punjabi = 70,
        Oriya = 71,
        Malayalam = 72,
        Kannada = 73,
        Tamil = 74,
        Telugu = 75,
        Sinhalese = 76,
        Burmese = 77,
        Khmer = 78,
        Lao = 79,
        Vietnamese = 80,
        Indonesian = 81,
        Tagalog = 82,
        [FieldName(0, "Malay(Roman script)")]
        Malay_Roman_script = 83,
        [FieldName(0, "Malay(Arabic script)")]
        Malay_Arabic_script = 84,
        Amharic = 85,
        Tigrinya = 86,
        Galla = 87,
        Somali = 88,
        Swahili = 89,
        [FieldName(0, "Kinyarwanda/Ruanda")]
        Kinyarwanda_Ruanda = 90,
        Rundi = 91,
        [FieldName(0, "Nyanja/Chewa")]
        Nyanja_Chewa = 92,
        Malagasy = 93,
        Esperanto = 94,
        Welsh = 128,
        Basque = 129,
        Catalan = 130,
        Latin = 131,
        Quechua = 132,
        Guarani = 133,
        Aymara = 134,
        Tatar = 135,
        Uighur = 136,
        Dzongkha = 137,
        [FieldName(0, "Javanese(Roman script)")]
        Javanese_Roman_script = 138,
        [FieldName(0, "Sundanese(Roman script)")]
        Sundanese_Roman_script = 139,
        Galician = 140,
        Afrikaans = 141,
        Breton = 142,
        Inuktitut = 143,
        [FieldName(0, "Scottish Gaelic")]
        Scottish_Gaelic = 144,
        [FieldName(0, "Manx Gaelic")]
        Manx_Gaelic = 145,
        [FieldName(0, "Irish Gaelic(with dot above)")]
        Irish_Gaelic_with_dot_above = 146,
        Tongan = 147,
        [FieldName(0, "Greek(polytonic)")]
        Greek_polytonic = 148,
        Greenlandic = 149,
        [FieldName(0, "Azerbaijani(Roman script)")]
        Azerbaijani_Roman_script = 150,
    }


    [TypeName("Windows encoding IDs")]
    enum name_WindowsEncodingIDs
    { 
        Symbol = 0,
        [FieldName(0, "Unicode BMP")]
        Unicode_BMP = 1,
        ShiftJIS = 2,
        PRC = 3,
        Big5 = 4,
        Wansung = 5,
        Johab = 6,
        [FieldName(0, "Reserved")]
        Reserved7 = 7,
        [FieldName(0, "Reserved")]
        Reserved8 = 8,
        [FieldName(0, "Reserved")]
        Reserved9 = 9,
        [FieldName(0, "Unicode full repertoire")]
        Unicode_full_repertoire = 10,
    }


    [TypeName("Windows language IDs")]
    enum name_WindowsLanguageIDs
    {
        [FieldName(0, "Afrikaans South Africa")]
        Afrikaans_South_Africa = 0x0436,
        [FieldName(0, "Albanian Albania")]
        Albanian_Albania = 0x041C,
        [FieldName(0, "Alsatian France")]
        Alsatian_France = 0x0484,
        [FieldName(0, "Amharic Ethiopia")]
        Amharic_Ethiopia = 0x045E,
        [FieldName(0, "Arabic Algeria")]
        Arabic_Algeria = 0x1401,
        [FieldName(0, "Arabic Bahrain")]
        Arabic_Bahrain = 0x3C01,
        [FieldName(0, "Arabic Egypt")]
        Arabic_Egypt = 0x0C01,
        [FieldName(0, "Arabic Iraq")]
        Arabic_Iraq = 0x0801,
        [FieldName(0, "Arabic Jordan")]
        Arabic_Jordan = 0x2C01,
        [FieldName(0, "Arabic Kuwait")]
        Arabic_Kuwait = 0x3401,
        [FieldName(0, "Arabic Lebanon")]
        Arabic_Lebanon = 0x3001,
        [FieldName(0, "Arabic Libya")]
        Arabic_Libya = 0x1001,
        [FieldName(0, "Arabic Morocco")]
        Arabic_Morocco = 0x1801,
        [FieldName(0, "Arabic Oman")]
        Arabic_Oman = 0x2001,
        [FieldName(0, "Arabic Qatar")]
        Arabic_Qatar = 0x4001,
        [FieldName(0, "Arabic Saudi Arabia")]
        Arabic_SaudiArabia = 0x0401,
        [FieldName(0, "Arabic Syria")]
        Arabic_Syria = 0x2801,
        [FieldName(0, "Arabic Tunisia")]
        Arabic_Tunisia = 0x1C01,
        [FieldName(0, "Arabic U.A.E.")]
        Arabic_UAE = 0x3801,
        [FieldName(0, "Arabic Yemen")]
        Arabic_Yemen = 0x2401,
        [FieldName(0, "Armenian Armenia")]
        Armenian_Armenia = 0x042B,
        [FieldName(0, "Assamese India")]
        Assamese_India = 0x044D,
        [FieldName(0, "Azeri(Cyrillic) Azerbaijan")]
        Azeri_Cyrillic_Azerbaijan = 0x082C,
        [FieldName(0, "Azeri(Latin) Azerbaijan")]
        Azeri_Latin_Azerbaijan = 0x042C,
        [FieldName(0, "Bashkir Russia")]
        Bashkir_Russia = 0x046D,
        [FieldName(0, "Basque Basque")]
        Basque_Basque = 0x042D,
        [FieldName(0, "Belarusian Belarus")]
        Belarusian_Belarus = 0x0423,
        [FieldName(0, "Bengali Bangladesh")]
        Bengali_Bangladesh = 0x0845,
        [FieldName(0, "Bengali India")]
        Bengali_India = 0x0445,
        [FieldName(0, "Bosnian(Cyrillic) Bosnia and Herzegovina")]
        Bosnian_Cyrillic_BosniaAndHerzegovina = 0x201A,
        [FieldName(0, "Bosnian(Latin) Bosnia and Herzegovina")]
        Bosnian_Latin_BosniaAndHerzegovina = 0x141A,
        [FieldName(0, "Breton France")]
        Breton_France = 0x047E,
        [FieldName(0, "Bulgarian Bulgaria")]
        Bulgarian_Bulgaria = 0x0402,
        [FieldName(0, "Catalan Catalan")]
        Catalan_Catalan = 0x0403,
        [FieldName(0, "Chinese Hong Kong S.A.R.")]
        Chinese_HongKongSAR = 0x0C04,
        [FieldName(0, "Chinese Macao S.A.R.")]
        Chinese_MacaoSAR = 0x1404,
        [FieldName(0, "Chinese People's Republic of China")]
        Chinese_PeoplesRepublicOfChina = 0x0804,
        [FieldName(0, "Chinese Singapore")]
        Chinese_Singapore = 0x1004,
        [FieldName(0, "Chinese Taiwan")]
        Chinese_Taiwan = 0x0404,
        [FieldName(0, "Corsican France")]
        Corsican_France = 0x0483,
        [FieldName(0, "Croatian Croatia")]
        Croatian_Croatia = 0x041A,
        [FieldName(0, "Croatian (Latin) Bosnia and Herzegovina")]
        Croatian_Latin_BosniaAndHerzegovina = 0x101A,
        [FieldName(0, "Czech Czech Republic")]
        Czech_CzechRepublic = 0x0405,
        [FieldName(0, "Danish Denmark")]
        Danish_Denmark = 0x0406,
        [FieldName(0, "Dari Afghanistan")]
        Dari_Afghanistan = 0x048C,
        [FieldName(0, "Divehi Maldives")]
        Divehi_Maldives = 0x0465,
        [FieldName(0, "Dutch Belgium")]
        Dutch_Belgium = 0x0813,
        [FieldName(0, "Dutch Netherlands")]
        Dutch_Netherlands = 0x0413,
        [FieldName(0, "English Australia")]
        English_Australia = 0x0C09,
        [FieldName(0, "English Belize")]
        English_Belize = 0x2809,
        [FieldName(0, "English Canada")]
        English_Canada = 0x1009,
        [FieldName(0, "English Caribbean")]
        English_Caribbean = 0x2409,
        [FieldName(0, "English India")]
        English_India = 0x4009,
        [FieldName(0, "English Ireland")]
        English_Ireland = 0x1809,
        [FieldName(0, "English Jamaica")]
        English_Jamaica = 0x2009,
        [FieldName(0, "English Malaysia")]
        English_Malaysia = 0x4409,
        [FieldName(0, "English New Zealand")]
        English_NewZealand = 0x1409,
        [FieldName(0, "English Republic of the Philippines")]
        English_RepublicOfThePhilippines = 0x3409,
        [FieldName(0, "English Singapore")]
        English_Singapore = 0x4809,
        [FieldName(0, "English South Africa")]
        English_SouthAfrica = 0x1C09,
        [FieldName(0, "English Trinidad and Tobago")]
        English_TrinidadAndTobago = 0x2C09,
        [FieldName(0, "English United Kingdom")]
        English_UnitedKingdom = 0x0809,
        [FieldName(0, "English United States")]
        English_UnitedStates = 0x0409,
        [FieldName(0, "English Zimbabwe")]
        English_Zimbabwe = 0x3009,
        [FieldName(0, "Estonian Estonia")]
        Estonian_Estonia = 0x0425,
        [FieldName(0, "Faroese Faroe Islands")]
        Faroese_FaroeIslands = 0x0438,
        [FieldName(0, "Filipino Philippines")]
        Filipino_Philippines = 0x0464,
        [FieldName(0, "Finnish Finland")]
        Finnish_Finland = 0x040B,
        [FieldName(0, "French Belgium")]
        French_Belgium = 0x080C,
        [FieldName(0, "French Canada")]
        French_Canada = 0x0C0C,
        [FieldName(0, "French France")]
        French_France = 0x040C,
        [FieldName(0, "French Luxembourg")]
        French_Luxembourg = 0x140c,
        [FieldName(0, "French Principality of Monaco")]
        French_PrincipalityOfMonaco = 0x180C,
        [FieldName(0, "French Switzerland")]
        French_Switzerland = 0x100C,
        [FieldName(0, "Frisian Netherlands")]
        Frisian_Netherlands = 0x0462,
        [FieldName(0, "Galician Galician")]
        Galician_Galician = 0x0456,
        [FieldName(0, "Georgian Georgia")]
        Georgian_Georgia = 0x0437,
        [FieldName(0, "German Austria")]
        German_Austria = 0x0C07,
        [FieldName(0, "German Germany")]
        German_Germany = 0x0407,
        [FieldName(0, "German Liechtenstein")]
        German_Liechtenstein = 0x1407,
        [FieldName(0, "German Luxembourg")]
        German_Luxembourg = 0x1007,
        [FieldName(0, "German Switzerland")]
        German_Switzerland = 0x0807,
        [FieldName(0, "Greek Greece")]
        Greek_Greece = 0x0408,
        [FieldName(0, "Greenlandic Greenland")]
        Greenlandic_Greenland = 0x046F,
        [FieldName(0, "Gujarati India")]
        Gujarati_India = 0x0447,
        [FieldName(0, "Hausa (Latin) Nigeria")]
        Hausa_Latin_Nigeria = 0x0468,
        [FieldName(0, "Hebrew Israel")]
        Hebrew_Israel = 0x040D,
        [FieldName(0, "Hindi India")]
        Hindi_India = 0x0439,
        [FieldName(0, "Hungarian Hungary")]
        Hungarian_Hungary = 0x040E,
        [FieldName(0, "Icelandic Iceland")]
        Icelandic_Iceland = 0x040F,
        [FieldName(0, "Igbo Nigeria")]
        Igbo_Nigeria = 0x0470,
        [FieldName(0, "Indonesian Indonesia")]
        Indonesian_Indonesia = 0x0421,
        [FieldName(0, "Inuktitut Canada")]
        Inuktitut_Canada = 0x045D,
        [FieldName(0, "Inuktitut (Latin) Canada")]
        Inuktitut_Latin_Canada = 0x085D,
        [FieldName(0, "Irish Ireland")]
        Irish_Ireland = 0x083C,
        [FieldName(0, "isiXhosa South Africa")]
        isiXhosa_SouthAfrica = 0x0434,
        [FieldName(0, "isiZulu South Africa")]
        isiZulu_SouthAfrica = 0x0435,
        [FieldName(0, "Italian Italy")]
        Italian_Italy = 0x0410,
        [FieldName(0, "Italian Switzerland")]
        Italian_Switzerland = 0x0810,
        [FieldName(0, "Japanese Japan")]
        Japanese_Japan = 0x0411,
        [FieldName(0, "Kannada India")]
        Kannada_India = 0x044B,
        [FieldName(0, "Kazakh Kazakhstan")]
        Kazakh_Kazakhstan = 0x043F,
        [FieldName(0, "Khmer Cambodia")]
        Khmer_Cambodia = 0x0453,
        [FieldName(0, "K'iche Guatemala")]
        Kiche_Guatemala = 0x0486,
        [FieldName(0, "Kinyarwanda Rwanda")]
        Kinyarwanda_Rwanda = 0x0487,
        [FieldName(0, "Kiswahili Kenya")]
        Kiswahili_Kenya = 0x0441,
        [FieldName(0, "Konkani India")]
        Konkani_India = 0x0457,
        [FieldName(0, "Korean Korea")]
        Korean_Korea = 0x0412,
        [FieldName(0, "Kyrgyz Kyrgyzstan")]
        Kyrgyz_Kyrgyzstan = 0x0440,
        [FieldName(0, "Lao Lao P.D.R.")]
        Lao_LaoPDR = 0x0454,
        [FieldName(0, "Latvian Latvia")]
        Latvian_Latvia = 0x0426,
        [FieldName(0, "Lithuanian Lithuania")]
        Lithuanian_Lithuania = 0x0427,
        [FieldName(0, "Lower Sorbian Germany")]
        Lower_Sorbian_Germany = 0x082E,
        [FieldName(0, "Luxembourgish Luxembourg")]
        Luxembourgish_Luxembourg = 0x046E,
        [FieldName(0, "Macedonian North Macedonia")]
        Macedonian_NorthMacedonia = 0x042F,
        [FieldName(0, "Malay Brunei Darussalam")]
        Malay_BruneiDarussalam = 0x083E,
        [FieldName(0, "Malay Malaysia")]
        Malay_Malaysia = 0x043E,
        [FieldName(0, "Malayalam India")]
        Malayalam_India = 0x044C,
        [FieldName(0, "Maltese Malta")]
        Maltese_Malta = 0x043A,
        [FieldName(0, "Maori New Zealand")]
        Maori_NewZealand = 0x0481,
        [FieldName(0, "Mapudungun Chile")]
        Mapudungun_Chile = 0x047A,
        [FieldName(0, "Marathi India")]
        Marathi_India = 0x044E,
        [FieldName(0, "Mohawk Mohawk")]
        Mohawk_Mohawk = 0x047C,
        [FieldName(0, "Mongolian (Cyrillic) Mongolia")]
        Mongolian_Cyrillic_Mongolia = 0x0450,
        [FieldName(0, "Mongolian (Traditional) People's Republic of China")]
        Mongolian_Traditional_PeoplesRepublicOfChina = 0x0850,
        [FieldName(0, "Nepali Nepal")]
        Nepali_Nepal = 0x0461,
        [FieldName(0, "Norwegian (Bokmal) Norway")]
        Norwegian_Bokmal_Norway = 0x0414,
        [FieldName(0, "Norwegian (Nynorsk) Norway")]
        Norwegian_Nynorsk_Norway = 0x0814,
        [FieldName(0, "Occitan France")]
        Occitan_France = 0x0482,
        [FieldName(0, "Odia (formerly Oriya) India")]
        Odia_formerly_Oriya_India = 0x0448,
        [FieldName(0, "Pashto Afghanistan")]
        Pashto_Afghanistan = 0x0463,
        [FieldName(0, "Polish Poland")]
        Polish_Poland = 0x0415,
        [FieldName(0, "Portuguese Brazil")]
        Portuguese_Brazil = 0x0416,
        [FieldName(0, "Portuguese Portugal")]
        Portuguese_Portugal = 0x0816,
        [FieldName(0, "Punjabi India")]
        Punjabi_India = 0x0446,
        [FieldName(0, "Quechua Bolivia")]
        Quechua_Bolivia = 0x046B,
        [FieldName(0, "Quechua Ecuador")]
        Quechua_Ecuador = 0x086B,
        [FieldName(0, "Quechua Peru")]
        Quechua_Peru = 0x0C6B,
        [FieldName(0, "Romanian Romania")]
        Romanian_Romania = 0x0418,
        [FieldName(0, "Romansh Switzerland")]
        Romansh_Switzerland = 0x0417,
        [FieldName(0, "Russian Russia")]
        Russian_Russia = 0x0419,
        [FieldName(0, "Sami (Inari) Finland")]
        Sami_Inari_Finland = 0x243B,
        [FieldName(0, "Sami (Lule) Norway")]
        Sami_Lule_Norway = 0x103B,
        [FieldName(0, "Sami (Lule) Sweden")]
        Sami_Lule_Sweden = 0x143B,
        [FieldName(0, "Sami (Northern) Finland")]
        Sami_Northern_Finland = 0x0C3B,
        [FieldName(0, "Sami (Northern) Norway")]
        Sami_Northern_Norway = 0x043B,
        [FieldName(0, "Sami (Northern) Sweden")]
        Sami_Northern_Sweden = 0x083B,
        [FieldName(0, "Sami (Skolt) Finland")]
        Sami_Skolt_Finland = 0x203B,
        [FieldName(0, "Sami (Southern) Norway")]
        Sami_Southern_Norway = 0x183B,
        [FieldName(0, "Sami (Southern) Sweden")]
        Sami_Southern_Sweden = 0x1C3B,
        [FieldName(0, "Sanskrit India")]
        Sanskrit_India = 0x044F,
        [FieldName(0, "Serbian (Cyrillic) Bosnia and Herzegovina")]
        Serbian_Cyrillic_BosniaAndHerzegovina = 0x1C1A,
        [FieldName(0, "Serbian (Cyrillic) Serbia")]
        Serbian_Cyrillic_Serbia = 0x0C1A,
        [FieldName(0, "Serbian (Latin) Bosnia and Herzegovina")]
        Serbian_Latin_BosniaAndHerzegovina = 0x181A,
        [FieldName(0, "Serbian (Latin) Serbia")]
        Serbian_Latin_Serbia = 0x081A,
        [FieldName(0, "Sesotho sa Leboa South Africa")]
        Sesotho_sa_Leboa_SouthAfrica = 0x046C,
        [FieldName(0, "Setswana South Africa")]
        Setswana_SouthAfrica = 0x0432,
        [FieldName(0, "Sinhala Sri Lanka")]
        Sinhala_SriLanka = 0x045B,
        [FieldName(0, "Slovak Slovakia")]
        Slovak_Slovakia = 0x041B,
        [FieldName(0, "Slovenian Slovenia")]
        Slovenian_Slovenia = 0x0424,
        [FieldName(0, "Spanish Argentina")]
        Spanish_Argentina = 0x2C0A,
        [FieldName(0, "Spanish Bolivia")]
        Spanish_Bolivia = 0x400A,
        [FieldName(0, "Spanish Chile")]
        Spanish_Chile = 0x340A,
        [FieldName(0, "Spanish Colombia")]
        Spanish_Colombia = 0x240A,
        [FieldName(0, "Spanish Costa Rica")]
        Spanish_CostaRica = 0x140A,
        [FieldName(0, "Spanish Dominican Republic")]
        Spanish_DominicanRepublic = 0x1C0A,
        [FieldName(0, "Spanish Ecuador")]
        Spanish_Ecuador = 0x300A,
        [FieldName(0, "Spanish El Salvador")]
        Spanish_ElSalvador = 0x440A,
        [FieldName(0, "Spanish Guatemala")]
        Spanish_Guatemala = 0x100A,
        [FieldName(0, "Spanish Honduras")]
        Spanish_Honduras = 0x480A,
        [FieldName(0, "Spanish Mexico")]
        Spanish_Mexico = 0x080A,
        [FieldName(0, "Spanish Nicaragua")]
        Spanish_Nicaragua = 0x4C0A,
        [FieldName(0, "Spanish Panama")]
        Spanish_Panama = 0x180A,
        [FieldName(0, "Spanish Paraguay")]
        Spanish_Paraguay = 0x3C0A,
        [FieldName(0, "Spanish Peru")]
        Spanish_Peru = 0x280A,
        [FieldName(0, "Spanish Puerto Rico")]
        Spanish_PuertoRico = 0x500A,
        [FieldName(0, "Spanish (Modern Sort) Spain")]
        Spanish_Modern_Sort_Spain = 0x0C0A,
        [FieldName(0, "Spanish (Traditional Sort) Spain")]
        Spanish_Traditional_Sort_Spain = 0x040A,
        [FieldName(0, "Spanish United States")]
        Spanish_UnitedStates = 0x540A,
        [FieldName(0, "Spanish Uruguay")]
        Spanish_Uruguay = 0x380A,
        [FieldName(0, "Spanish Venezuela")]
        Spanish_Venezuela = 0x200A,
        [FieldName(0, "Swedish Finland")]
        Swedish_Finland = 0x081D,
        [FieldName(0, "Swedish Sweden")]
        Swedish_Sweden = 0x041D,
        [FieldName(0, "Syriac Syria")]
        Syriac_Syria = 0x045A,
        [FieldName(0, "Tajik (Cyrillic) Tajikistan")]
        Tajik_Cyrillic_Tajikistan = 0x0428,
        [FieldName(0, "Tamazight (Latin) Algeria")]
        Tamazight_Latin_Algeria = 0x085F,
        [FieldName(0, "Tamil India")]
        Tamil_India = 0x0449,
        [FieldName(0, "Tatar Russia")]
        Tatar_Russia = 0x0444,
        [FieldName(0, "Telugu India")]
        Telugu_India = 0x044A,
        [FieldName(0, "Thai Thailand")]
        Thai_Thailand = 0x041E,
        [FieldName(0, "Tibetan PRC")]
        Tibetan_PRC = 0x0451,
        [FieldName(0, "Turkish Turkey")]
        Turkish_Turkey = 0x041F,
        [FieldName(0, "Turkmen Turkmenistan")]
        Turkmen_Turkmenistan = 0x0442,
        [FieldName(0, "Uighur PRC")]
        Uighur_PRC = 0x0480,
        [FieldName(0, "Ukrainian Ukraine")]
        Ukrainian_Ukraine = 0x0422,
        [FieldName(0, "Upper Sorbian Germany")]
        Upper_Sorbian_Germany = 0x042E,
        [FieldName(0, "Urdu Islamic Republic of Pakistan")]
        Urdu_IslamicRepublicOfPakistan = 0x0420,
        [FieldName(0, "Uzbek (Cyrillic) Uzbekistan")]
        Uzbek_Cyrillic_Uzbekistan = 0x0843,
        [FieldName(0, "Uzbek (Latin) Uzbekistan")]
        Uzbek_Latin_Uzbekistan = 0x0443,
        [FieldName(0, "Vietnamese Vietnam")]
        Vietnamese_Vietnam = 0x042A,
        [FieldName(0, "Welsh United Kingdom")]
        Welsh_UnitedKingdom = 0x0452,
        [FieldName(0, "Wolof Senegal")]
        Wolof_Senegal = 0x0488,
        [FieldName(0, "Yakut Russia")]
        Yakut_Russia = 0x0485,
        [FieldName(0, "Yi PRC")]
        Yi_PRC = 0x0478,
        [FieldName(0, "Yoruba Nigeria")]
        Yoruba_Nigeria = 0x046A,
    }
#pragma warning restore IDE1006
}
