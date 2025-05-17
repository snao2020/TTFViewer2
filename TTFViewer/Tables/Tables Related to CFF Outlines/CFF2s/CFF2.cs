// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006
    /*
    Header
    TopDICT
    GlobalSubrINDEX
    CharStringINDEX
    FontDICTSelect  not required 
    FontDICTINDEX 	
        FontDICT#0
        FontDICT#1
        ...
        FontDICT#n
    PrivateDICT#0
    PrivateDICT#1
    ...
    PrivateDICT#n
    VariationStore  not required 	
    */

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("CFF2")]
    [TypeName("CFF2 — Compact Font Format (CFF) Version 2")]
    [BaseName("CFF2")]
    class CFF2Table
    {
        [TypeName("")]
        [Description(0, "Fixed location")]
        public CFF2Header Header;


        [Verification(0, AttributeConditionKind.Equal, FieldValueKind.Path, "Header", typeof(CFF2Header_Version20))]
        [Position(0, "\\", FieldValueKind.Path,  "Header\\headerSize")]
        [Length(0, FieldValueKind.Path, "Header\\topDictSize", null)]
        [TypeName("")]
        public DICT TopDICT;


        [Verification(0, AttributeConditionKind.Equal, FieldValueKind.Path, "TopDICT", typeof(DICT))]
        [TypeName("")]
        public INDEX<IList<SubrCharstring>> GlobalSubrINDEX;


        [Position(0, "\\", FieldValueKind.Path, "TopDICT\\Data", CFF2DICTOperators.vstore, 0)]
        [TypeName("")]
        public CFF2VariationStore VariationStore;


        [Position(0, "\\", FieldValueKind.Path, "TopDICT\\Data", CFF2DICTOperators.CharStrings, 0)]
        [TypeName("")]
        public INDEX<IList<Charstring>> CharStringINDEX;


        [Position(0, "\\", FieldValueKind.Path, "TopDICT\\Data", CFF2DICTOperators.FDSelect, 0)]
        [TypeSelect(FieldValueKind.PeekValue, "uint8", null)]
        [TypeCondition(typeof(FDSelect_Format0), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0")]
        [TypeCondition(typeof(FDSelect_Format3), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "3")]
        [TypeCondition(typeof(FDSelect_Format4), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "4")]
        [TypeName("")]
        [FieldName(0, FieldNameKind.Method, "FontDICTSelectFieldName")]
        [Description(0, "Present only if there is more than one Font DICT in the Font DICT INDEX")]
        public FDSelect FDSelect;

        static String FontDICTSelectFieldName(IItemValueService ivs)
        {
            String result;
            switch (ivs.LoadValue(ivs.FilePosition, typeof(uint8)))
            {
                case 0: result = "FontDICTSelectFormat0"; break;
                case 3: result = "FontDICTSelectFormat3"; break;
                case 4: result = "FontDICTSelectFormat4"; break;
                default: result = "FontDICTSelect"; break;
            }
            return result;
        }


        [Position(0, "\\", FieldValueKind.Path, "TopDICT\\Data", CFF2DICTOperators.FDArray, 0)]
        [TypeName("")]
        [FieldName(0, "Font DICT INDEX")]
        //public ArrayINDEX<DICT[]> FontDICTINDEX;
        public INDEX<DICT[]> FontDICTINDEX;


        [Count(0, FieldValueKind.Path, "FontDICTINDEX\\count")]
        [Position(1, "\\", FieldValueKind.Path, "FontDICTINDEX\\data\\[]\\Data", CFF2DICTOperators.Private, 1)]
        [Length(1, FieldValueKind.Path, "FontDICTINDEX\\data\\[]\\Data", CFF2DICTOperators.Private, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "PrivateDICTFieldName")]
        [Description(1, "One per Font DICT")]
        public DICT[] PrivateDICT;

        static String PrivateDICTFieldName(IItemValueService ivs)
            => CFFHelper.FontIndexText(ivs, "PrivateDICT");

        [Count(0, FieldValueKind.Path, "FontDICTINDEX\\count")]
        [Position(1, "PrivateDICT\\[]", FieldValueKind.Path, "PrivateDICT\\[]\\Data", CFF2DICTOperators.Subrs, 0)]
        [TypeName("")]
        [FieldName(1, FieldNameKind.Method, "LocalSubrINDEXFieldName")]
        public INDEX<IList<SubrCharstring>>[] LocalSubrINDEX;

        static String LocalSubrINDEXFieldName(IItemValueService ivs, String name)
            => CFFHelper.FontIndexText(ivs, "LocalSubrINDEX");
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion,  minorVersion", null)]
    [ClassTypeCondition(typeof(CFF2Header_Version20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x02, 0x00")]
    [Invalid]
    [TypeName("CFF2 Header")]
    [BaseName("Header")]
    class CFF2Header
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Format major version.Set to 2")]
        public uint8 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Format minor version.Set to zero")]
        public uint8 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("CFF2 Header")]
    class CFF2Header_Version20 : CFF2Header
    {
        //public uint8 majorVersion; // Format major version.Set to 2.
        //public uint8 minorVersion; // Format minor version.Set to zero.

        [Description(0, "Header size (bytes)")]
        public uint8 headerSize;

        [Description(0, "Length of Top DICT structure in bytes")]
        public uint16 topDictSize;
    }

#pragma warning restore IDE1006
}