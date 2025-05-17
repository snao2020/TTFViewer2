//ver 1.9,1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(FDSelect_Format0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(FDSelect_Format3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [Invalid]
    class FDSelect
    {
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [Description(0, DescriptionKind.Method, "formatDescription")]
        public uint8 format; // = 0

        static String formatDescription(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "0 or 3 or 4" : "0 or 3";
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("FDSelect Format0")]
    class FDSelect_Format0 : FDSelect
    {
        //public uint8 format; // = 0

        [Count(0, FieldValueKind.Path, "\\CharStringsINDEX\\[]\\count")]
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [FieldName(0, FieldNameKind.Method, "fdsFieldName")]
        [Description(0, "FD selector array")]
        public IList<uint8> fds;

        static String fdsFieldName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "fontDICTIDs" : "fds";
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("FDSelect Format3")]
    class FDSelect_Format3 : FDSelect
    {
        //public uint8 format; // = 3

        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [FieldName(0, FieldNameKind.Method, "nRangesFieldName")]
        [Description(0, "Number of ranges")]
        public uint16 nRanges;

        [Count(0, FieldValueKind.Path, "nRanges")]
        [FieldName(0, FieldNameKind.Method, "Range3FieldName")]
        [Description(0, "Range3 array(see Table 29)")]
        public IList<FDSelect_Range3> Range3;

        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [Description(0, "Sentinel GID(see below)")]
        public uint16 sentinel;

        static String nRangesFieldName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "numRanges" : "nRanges";

        static String Range3FieldName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "ranges" : "Range3";
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Range3")]
    class FDSelect_Range3
    {
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [Description(0, "First glyph index in range")]
        public uint16 first;

        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [FieldName(0, FieldNameKind.Method, "fdFieldName")]
        [Description(0, "FD index for all glyphs in range")]
        public uint8 fd;

        static String fdFieldName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "fontDICTID " : "fd";
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("FontDICTSelectFormat4")]
    class FDSelect_Format4 : FDSelect
    {
        //public uint8 format; // = 4

        [Description(0, "Number of ranges")]
        public uint32 numRanges;

        [Count(0, FieldValueKind.Path, "numRanges")]
        [Description(0, "Array of Range4 records(see below)")]
        public IList<FDSelect_Range4> ranges;

        [Description(0, "Sentinel glyph ID")]
        public uint32 sentinel;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Range4")]
    class FDSelect_Range4
    {
        [Description(0, "First glyph ID in range")]
        public uint32 first;

        [Description(0, "FontDICT index for all glyphs in range")]
        public uint16 fontDICTID;
    }

    //----------------------------------------------------------------------------


    static class FDSelectHelper
    {
        public static Int32? GetFDIndex(FDSelect fdSelect, Int32 gid)
        {
            Int32? result = null;

            switch(fdSelect)
            {
                case FDSelect_Format0 format0:
                    if(gid < format0.fds.Count)
                        result = format0.fds[gid];
                    break;

                case FDSelect_Format3 format3:
                    if (gid < format3.sentinel)
                    {
                        var item = format3.Range3.Last(i => i.first <= gid);
                        result = item.fd;
                    }
                    break;

                case FDSelect_Format4 format4:
                    if (gid < format4.sentinel)
                    {
                        var item = format4.ranges.Last(i => i.first <= gid);
                        result = item.fontDICTID;
                    }
                    break;
            }

            return result;
        }
    }

#pragma warning restore IDE1006
}
