// var 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("cvar — CVT Variations Table")]
    [BaseName("cver")]
    class cvarTable
    {
        public cvarHeader header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(cvarHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("'cvar' table header")]
    [BaseName("cvarHeader")]
    class cvarHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the CVT variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the CVT variations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'cvar' table header")]
    class cvarHeader_Version10 : cvarHeader
    {
        //public uint16 majorVersion; // Major version number of the CVT variations table — set to 1.
        //public uint16 minorVersion; // Minor version number of the CVT variations table — set to 0.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "tupleVariationCountDescription")]
        public uint16 tupleVariationCount;

        [TableType(typeof(IList<uint8>))]
        [TableLength(TableLengthKind.ElementCount, "dataCount")]
        [Description(0, "Offset from the start of the 'cvar' table to the serialized data")]
        public Offset16 dataOffset;

        [Count(0, FieldValueKind.Path, "tupleVariationCount", "Mask:0x0fff")]
        [Description(0, "Array of tuple variation headers")]
        public IList<TupleVariationHeader> tupleVariationHeaders;

        static string tupleVariationCountDescription(IItemValueService ivp)
        {
            // A packed field.The high 4 bits are flags, and the low 12 bits are the number of tuple-variation data tables.The count can be any number between 1 and 4095
            if (ivp.Value is uint16 u16)
            {
                Int32 count = (Int32)u16 & (Int32)TupleVariationCountMask.COUNT_MASK;
                Int32 flags = (Int32)u16 & ~(Int32)TupleVariationCountMask.COUNT_MASK;

                string result = (flags & (Int32)TupleVariationCountMask.SHARED_POINT_NUMBERS) != 0 ? "flags=SHARED_POINT_NUMBERS, " : "";
                result += $"count={count}";
                return result;
            }
            return null;
        }

        static Int32 dataCount(IAttributeService service)
        {
            Int32 result = 0;
            var values = service.GetValues(FieldValueKind.Path, "tupleVariationCount");
            if (values.SingleValue(0) is uint16 tupleVariationCount)
            {
                result = (Int32)tupleVariationCount & (Int32)TupleVariationCountMask.COUNT_MASK;
            }
            return result; ;
        }
    }

#pragma warning restore IDE1006
}
