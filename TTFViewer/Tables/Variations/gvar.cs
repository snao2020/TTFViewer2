// ver1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("gvar")]
    [TypeName("gvar — Glyph Variations Table")]
    [BaseName("gvar")]
    class gvarTable
    {
        [FieldName(0, null)]
        public gvarHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(gvarHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("'gvar' header:")]
    [BaseName("gvarHeader")]
    class gvarHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the glyph variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the glyph variations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'gvar' header:")]
    class gvarHeader_Version10 : gvarHeader
    {
        //public uint16 majorVersion; // Major version number of the glyph variations table — set to 1.
        //public uint16 minorVersion; // Minor version number of the glyph variations table — set to 0.

        [Description(0, "The number of variation axes for this font.This must be the same number as axisCount in the 'fvar' table")]
        public uint16 axisCount;

        [Description(0, "The number of shared tuple records. Shared tuple records can be referenced within glyph variation data tables for multiple glyphs, as opposed to other tuple records stored directly within a glyph variation data table")]
        public uint16 sharedTupleCount;

        [TableType(typeof(SharedTuplesArray))]
        [Description(0, "Offset from the start of this table to the shared tuple records")]
        public Offset32 sharedTuplesOffset;

        [Description(0, "The number of glyphs in this font.This must match the number of glyphs stored elsewhere in the font")]
        public uint16 glyphCount;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Bit-field that gives the format of the offset array that follows.If bit 0 is clear, the offsets are uint16; if bit 0 is set, the offsets are uint32")]
        public uint16 flags;

        [TypeName("Offset32")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset from the start of this table to the array of GlyphVariationData tables")]
        public uint32 glyphVariationDataArrayOffset;

        [Count(0, FieldValueKind.Path, "glyphCount", "Add:1")]
        [UniformType(1)]
        [TypeSelectAttribute(FieldValueKind.Path, "flags", "Mask:0x0001")]
        [TypeConditionAttribute(typeof(Offset16), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0")]
        [TypeConditionAttribute(typeof(Offset32), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]

        [TableSelect("glyphVariationDataTableSelect")]
        [TableCondition(typeof(GlyphVariationDataHeader), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TableCondition(typeof(NullTable), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition(TablePositionFlag.Method,  "glyphVariationDataTablePosition")]

        [TypeName("Offset16 or Offset32")]
        [ValueFormat(1, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offsets from the start of the GlyphVariationData array to each GlyphVariationData table")]
        public IList<ITTFPrimitive> glyphVariationDataOffsets;

        static object[] glyphVariationDataTableSelect(IAttributeService service)
        {
            if (service.GetValues(FieldValueKind.Path, "glyphVariationDataOffsets").SingleValue(0) is IList<ITTFPrimitive> glyphVariationDataOffsets)
            {
                var index = TablePathHelper.GetLastIndex(service.Path);
                var b = glyphVariationDataOffsets[index + 1].ToNumber4() == glyphVariationDataOffsets[index].ToNumber4() ? 0 : 1;
                return new object[] { b };
            }
            return null;
        }

        static UInt32? glyphVariationDataTablePosition(IAttributeService service)
        {
            UInt32? result = null;
            var values = service.GetValues(FieldValueKind.Path, "flags, glyphVariationDataArrayOffset, glyphVariationDataOffsets");
            if(values.SingleValue(0) is uint16 flags
                && values.SingleValue(1) is uint32 glyphVariationDataArrayOffset
                && values.SingleValue(2) is IList<ITTFPrimitive> glyphVariationDataOffsets)
            {
                var index = TablePathHelper.GetLastIndex(service.Path);
                var offset = glyphVariationDataOffsets[index].ToNumber4();
                if ((flags & 0x0001) == 0)
                    offset *= 2;
                result = service.TableModel.FilePosition + glyphVariationDataArrayOffset + offset;
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Shared tuples array")]
    class SharedTuplesArray
    {
        [Count(0, FieldValueKind.OffsetSource, "sharedTupleCount")]
        [Description(0, "Array of tuple records shared across all glyph variation data tables")]
        public Tuple[] sharedTuples;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphVariationData header")]
    class GlyphVariationDataHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, DescriptionKind.Method, "tupleVariationCountDescription")]
        public uint16 tupleVariationCount;
        
        [TableType(typeof(IList<uint8>))]
        [TableLength(TableLengthKind.ElementCount, "dataCount")]
        [Description(0, "Offset from the start of the GlyphVariationData table to the serialized data")]
        public Offset16 dataOffset;

        [Count(0, FieldValueKind.Path, "tupleVariationCount", "Mask:0x0fff")]
        [Description(0, "Array of tuple variation headers")]
        public IList<TupleVariationHeader> tupleVariationHeaders;
        
        static string tupleVariationCountDescription(IItemValueService ivp)
        {
            // "A packed field.The high 4 bits are flags, and the low 12 bits are the number of tuple variation tables for this glyph.The number of tuple variation tables can be any number between 1 and 4095"
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
            var values = service.GetValues(FieldValueKind.OffsetSource, "\\Header\\flags, \\Header\\glyphVariationDataOffsets");
            if(values.SingleValue(1) is IList<ITTFPrimitive> offsets)
            {
                if(service.GetValues(FieldValueKind.Path, "dataOffset").SingleValue(0) is Offset16 dataOffset)
                {
                    var index = TablePathHelper.GetLastIndex(service.TableModel.SourcePath);
                    var length = offsets[index + 1].ToNumber4() - offsets[index].ToNumber4();
                    if (offsets[index] is Offset16)
                        length *= 2;
                    result = (Int32)(length - dataOffset);
                }
            }
            return result; ;
        }
    }

    /*
        Point number    Element
        0 	Base glyph
        1 	Accent glyph
        2 	Left side bearing point
        3 	Right side bearing point
        4 	Top side bearing point
        5 	Bottom side bearing point
    */

#pragma warning restore IDE1006
}
