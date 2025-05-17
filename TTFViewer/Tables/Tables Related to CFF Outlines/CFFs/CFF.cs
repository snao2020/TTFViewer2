// var 1.9 
//ver1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("CFF ")]
    [TypeName("CFF — Compact Font Format table")]
    [BaseName("CFF")]
    class CFFTable
    {
        [TypeName("")]
        public CFFHeader Header;


        [Verification(0, AttributeConditionKind.Equal, FieldValueKind.Path, "Header", typeof(CFFHeader_Version10))]
        [Position(0, "\\", FieldValueKind.Path, "Header\\hdrSize")]
        [TypeName("")]
        [FieldName(0, "Name INDEX")]
        public INDEX<IList<uint8[]>> NameINDEX;


        [Verification(0, AttributeConditionKind.Equal, FieldValueKind.Path, "NameINDEX", typeof(INDEX_Valid<IList<uint8[]>>))]
        [TypeName("")]
        [FieldName(0, "Top DICT INDEX")]
        public INDEX<DICT[]> TopDICTINDEX;


        [Verification(0, AttributeConditionKind.Equal, FieldValueKind.Path, "TopDICTINDEX", typeof(INDEX_Valid<DICT[]>))]
        [TypeName("")]
        [FieldName(0, "String INDEX")]
        public INDEX<IList<uint8[]>> StringINDEX;


        [Verification(0, AttributeConditionKind.Equal, FieldValueKind.Path, "StringINDEX", typeof(INDEX_Valid<IList<uint8[]>>))]
        [TypeName("")]
        [FieldName(0, "Global Subr INDEX")]
        public INDEX<IList<SubrCharstring>> GlobalSubrINDEX;


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.CharStrings, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "CharStringsINDEXFieldName")]
        public INDEX<IList<Charstring>>[] CharStringsINDEX;

        static string CharStringsINDEXFieldName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "CharStrings INDEX");


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.Encoding, 0, CanCreate = "CanCreateEncodings")]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "EncodingsFieldName")]
        public CFFEncodings[] Encodings;

        static bool CanCreateEncodings(Int32 operandInteger)
            => operandInteger > (Int32)EncodingID.ExpertEncoding;

        static string EncodingsFieldName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Encodings");


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.charset, 0, CanCreate="CanCreateCharsets")]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "CharsetsFieldName")]
        public CFFCharsets[] Charsets;

        static bool CanCreateCharsets(Int32 operandInteger)
            => operandInteger > (Int32)CharsetID.ExpertSubset;

        static string CharsetsFieldName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Charsets");


        #region nonCID

        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.Private, 1)]
        [Length(1, FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.Private, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "PrivateDICTName")]
        public DICT[] PrivateDICT; // per-font

        static string PrivateDICTName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Private DICT");


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "PrivateDICT\\[]", FieldValueKind.Path, "PrivateDICT\\[]\\Data", CFFDICTOperators.Subrs, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "LocalSubrINDEXName")]
        public INDEX<IList<SubrCharstring>>[] LocalSubrINDEX; // per-font

        static string LocalSubrINDEXName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Local Subr INDEX");

#if false
#endif
        #endregion nonCID


        #region CID

        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.FDSelect, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "FDSelectFieldName")]
        public FDSelect[] FDSelect;

        static string FDSelectFieldName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "FDSelect");


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "TopDICTINDEX\\data\\[]\\Data", CFFDICTOperators.FDArray, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "FontDICTINDEXName")]
        public INDEX<DICT[]>[] FontDICTINDEX;

        static string FontDICTINDEXName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Font DICT INDEX");


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Count(1, FieldValueKind.Path, "FontDICTINDEX\\[]\\count")]
        [Position(2, "\\", FieldValueKind.Path, "FontDICTINDEX\\[]\\data\\[]\\Data", CFFDICTOperators.Private, 1)]
        [Length(2, FieldValueKind.Path, "FontDICTINDEX\\[]\\data\\[]\\Data", CFFDICTOperators.Private, 0)]
        [TypeName("")]
        [FieldName(2, FieldNameKind.Method, "CIDPrivateDICTName")]
        public DICT[][] CIDPrivateDICT; // per-font

        static string CIDPrivateDICTName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Private DICT");


        [Count(0, FieldValueKind.Path, "NameINDEX\\count")]
        [Count(1, FieldValueKind.Path, "FontDICTINDEX\\[]\\count")]
        [Position(2, "CIDPrivateDICT\\[]\\[]", FieldValueKind.Path, "CIDPrivateDICT\\[]\\[]\\Data", CFFDICTOperators.Subrs, 0)]
        [TypeName("")]
        [FieldName(2, FieldNameKind.Method, "CIDLocalSubrINDEXName")]
        public INDEX<IList<SubrCharstring>>[][] CIDLocalSubrINDEX; // per-font

        static string CIDLocalSubrINDEXName(IItemValueService ivp)
            => CFFHelper.FontIndexText(ivp, "Local Subr INDEX");

#if false
#endif
        #endregion CID

#if false
#endif

        // Copyright and Trademark Notices
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "major,minor", null)]
    [ClassTypeCondition(typeof(CFFHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x01, 0x00")]
    [Invalid]
    [TypeName("Header")]
    [BaseName("Header")]
    class CFFHeader
    {
        [TypeName("Card8")]
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Format major version(starting at 1)")]
        public uint8 major;


        [TypeName("Card8")]
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Format minor version(starting at 0)")]
        public uint8 minor;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Header")]
    class CFFHeader_Version10 : CFFHeader
    {
        //public uint8 major; // Format major version(starting at 1)
        //public uint8 minor; // Format minor version(starting at 0)

        [TypeName("Card8")]
        [Description(0, "Header size(bytes)")]
        public uint8 hdrSize;

        [TypeName("OffSize")]
        [Description(0, "Absolute offset(0) size public")]
        public uint8 offSize;
    }

#pragma warning restore IDE1006
}