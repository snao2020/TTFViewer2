// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.Model;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Tuple
    {
        [Count(0, "axisCount")]
        [Description(0, "Coordinate array specifying a position within the font’s variation space.The number of elements must match the axisCount specified in the 'fvar' table")]
        public F2DOT14[] coordinates;

        static Int32 axisCount(IAttributeService service)
        {
            Int32 result = 0;
            if (service.GetValues(FieldValueKind.FontTableType, null).SingleValue(0) is Type fontTableType)
            {
                if (fontTableType == typeof(fvarTable))
                    result = (Int32)service.GetValues(FieldValueKind.FontTableValue, "fvar\\Header\\axisCount").SingleValue(0).ToNumber4();
                else if (fontTableType == typeof(gvarTable))
                    result = (Int32)service.GetValues(FieldValueKind.FontTableValue, "gvar\\Header\\axisCount").SingleValue(0).ToNumber4();
                else
                {
                    result = (Int32)service.GetValues(FieldValueKind.FontTableValue, "fvar\\Header\\axisCount").SingleValue(0).ToNumber4();
                    if(result == 0)
                        result = (Int32)service.GetValues(FieldValueKind.FontTableValue, "gvar\\Header\\axisCount").SingleValue(0).ToNumber4();
                }
            }
            return result;
        }
    }
    /*
    dup --> moveto gvar.cs

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("GlyphVariationData header")]
    public class GlyphVariationDataHeader
    {
        public uint16 tupleVariationCount; // A packed field.The high 4 bits are flags (see below), and the low 12 bits are the number of tuple variation tables for this glyph.The count can be any number between 1 and 4095.

        public Offset16 dataOffset; // Offset from the start of the GlyphVariationData table to the serialized data.

        [Container(ContainerKind.Count, FieldValueKind.Path, "tupleVariationCount")]
        public TupleVariationHeader[] tupleVariationHeaders; // [tupleVariationCount]  Array of tuple variation headers.
    }
    */
    /*
    dup --> moveto cvar.cs

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'cvar' table header:")]
    public class cvarTableHeader
    {
        public uint16 majorVersion; // Major version number of the 'cvar' table — set to 1.
        public uint16 minorVersionh; // Minor version number of the 'cvar' table — set to 0.
        public uint16 tupleVariationCount; // A packed field.The high 4 bits are flags(see below), and the low 12 bits are the number of tuple variation tables.The count can be any number between 1 and 4095.

        public Offset16 dataOffset; //  Offset from the start of the 'cvar' table to the serialized data.

        [Container(ContainerKind.Count, FieldValueKind.Path, "tupleVariationCount")]
        public TupleVariationHeader[] tupleVariationHeaders; // [tupleVariationCount]  Array of tuple variation headers.
    }
    */

    enum TupleVariationCountMask
    {
        SHARED_POINT_NUMBERS = 0x8000, // Flag indicating that some or all tuple variation tables reference a shared set of “point” numbers.These shared numbers are represented as packed point number data at the start of the serialized data.
        Reserved = 0x7000, // Reserved for future use — set to 0.
        COUNT_MASK = 0x0FFF, // Mask for the low bits to give the number of tuple variation tables.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class TupleVariationHeader
    {
        [Description(0, "The size in bytes of the serialized data for this tuple variation table")]
        public uint16 variationDataSize;

        [Description(0, "A packed field.The high 4 bits are flags (see below). The low 12 bits are an index into a shared tuple records array")]
        public uint16 tupleIndex;

        [TypeSelectAttribute(FieldValueKind.Path, "tupleIndex", "Mask:0x8000")]  // 0x8000=EMBEDDED_PEAK_TUPLE
        [TypeConditionAttribute(null, AttributeConditionKind.Equal, FieldValueKind.Unsigned,  "0x0000")]
        [Description(0, "Peak tuple record for this tuple variation table — optional, determined by flags in the tupleIndex value")]
        public Tuple peakTuple; // Note that this must always be included in the 'cvar' table.

        [TypeSelectAttribute(FieldValueKind.Path, "tupleIndex", "Mask:0x4000")]  // 0x4000=INTERMEDIATE_REGION
        [TypeConditionAttribute(null, AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0x0000")]
        [Description(0, "Intermediate start tuple record for this tuple variation table — optional, determined by flags in the tupleIndex value")]
        public Tuple intermediateStartTuple;

        [TypeSelectAttribute(FieldValueKind.Path, "tupleIndex", "Mask:0x4000")]  // 0x4000=INTERMEDIATE_REGION
        [TypeConditionAttribute(null, AttributeConditionKind.Equal, FieldValueKind.Unsigned,  "0x0000")]
        [Description(0, "Intermediate end tuple record for this tuple variation table — optional, determined by flags in the tupleIndex value")]
        public Tuple intermediateEndTuple;

        static string tupleIndexDescriptor(IItemValueService ivp)
        {
            // A packed field.The high 4 bits are flags (see below). The low 12 bits are an index into a shared tuple records array.
            if (ivp.Value is uint16 u16)
            {
                Int32 flags = (Int32)u16 & ~(Int32)tupleIndexFormat.TUPLE_INDEX_MASK;
                Int32 index = (Int32)u16 & (Int32)tupleIndexFormat.TUPLE_INDEX_MASK;

                string flagString = ItemValueHelper.GetEnumItemName(typeof(tupleIndexFormat), flags);
                string result = string.IsNullOrEmpty(flagString) ? "" : $"flags={flagString}, ";
                result += $"index={index}";
                return result;
            }
            return null;
        }
    }


    [TypeName("tupleIndex")]
    enum tupleIndexFormat
    {
        EMBEDDED_PEAK_TUPLE = 0x8000, // Flag indicating that this tuple variation header includes an embedded peak tuple record, immediately after the tupleIndex field.If set, the low 12 bits of the tupleIndex value are ignored.
                                      // Note that this must always be set within the 'cvar' table.
        INTERMEDIATE_REGION = 0x4000, // Flag indicating that this tuple variation table applies to an intermediate region within the variation space. If set, the header includes the two intermediate-region, start and end tuple records, immediately after the peak tuple record (if present).
        PRIVATE_POINT_NUMBERS = 0x2000, // Flag indicating that the serialized data for this tuple variation table includes packed “point” number data. If set, this tuple variation table uses that number data; if clear, this tuple variation table uses shared number data found at the start of the serialized data for this glyph variation data or 'cvar' table.
        Reserved = 0x1000, // Reserved for future use — set to 0.
        TUPLE_INDEX_MASK = 0x0FFF, // Mask for the low 12 bits to give the shared tuple records index.
    }


    enum controlByteMask
    {
        POINTS_ARE_WORDS = 0x80, // Flag indicating the data type used for point numbers in this run.If set, the point numbers are stored as unsigned 16-bit values (uint16); if clear, the point numbers are stored as unsigned bytes (uint8).
        POINT_RUN_COUNT_MASK = 0x7F, // Mask for the low 7 bits of the control byte to give the number of point number elements, minus 1.
    }


    enum PackedDeltasMask
    {
        DELTAS_ARE_ZERO = 0x80, // Flag indicating that this run contains no data (no explicit delta values are stored), and that the deltas for this run are all zero.
        DELTAS_ARE_WORDS = 0x40, // Flag indicating the data type for delta values in the run. If set, the run contains 16 - bit signed deltas(int16); if clear, the run contains 8 - bit signed deltas(int8).
        DELTA_RUN_COUNT_MASK = 0x3F, // Mask for the low 6 bits to provide the number of delta values in the run, minus one.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(DeltaSetIndexMapFormat0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(DeltaSetIndexMapFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [BaseName("DeltaSetIndexMap")]
    class DeltaSetIndexMap
    {
        [Description(0, "DeltaSetIndexMap format")]
        public uint8 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("DeltaSetIndexMap format 0")]
    [BaseName("DeltaSetIndexMap")]
    class DeltaSetIndexMapFormat0
    {
        [Description(0, "DeltaSetIndexMap format: set to 0")]
        public uint8 format;

        [Description(0, DescriptionKind.Method, "entryFormatDescription")]
        public uint8 entryFormat; // A packed field that describes the compressed representation of delta-set indices.See details below

        [Description(0, "The number of mapping entries")]
        public uint16 mapCount;

        [Count(0, "mapDataMethod")]
        [Description(0, "The delta-set index mapping data. See details below")]
        public IList<uint8> mapData;

        static Int32 mapDataMethod(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.Path, "entryFormat,mapCount");
            if (values[0] is uint8 entryFormat
                && values[1] is uint16 mapCount)
            {
                return mapCount >> ((entryFormat & (Int32)EntryFormatFieldMasks.INNER_INDEX_BIT_COUNT_MASK) + 1);
            }
            return 0;
        }

        static string entryFormatDescription(IItemValueService ivp)
        {
            // A packed field that describes the compressed representation of delta-set indices.See details below.
            if (ivp.Parent.Value is DeltaSetIndexMapFormat0 map)
            {
                Int32 entryFormat = map.entryFormat;
                Int32 mapEntrySize = ((entryFormat & (Int32)EntryFormatFieldMasks.MAP_ENTRY_SIZE_MASK) >> 4) + 1;
                Int32 innerIndexBitCount = entryFormat & (Int32)EntryFormatFieldMasks.INNER_INDEX_BIT_COUNT_MASK;
                return $"mapEntrySize={mapEntrySize},innerIndexBitCount={innerIndexBitCount}";
            }
            return null;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("DeltaSetIndexMap format 1")]
    [BaseName("DeltaSetIndexMap")]
    class DeltaSetIndexMapFormat1
    {
        [Description(0, "DeltaSetIndexMap format: set to 1")]
        public uint8 format;

        [Description(0, "entryFormatDescription")]
        public uint8 entryFormat; // A packed field that describes the compressed representation of delta-set indices. See details below.

        [Description(0, "The number of mapping entries")]
        public uint32 mapCount;

        [Count(0, "mapDataMethod")]
        [Description(0, "The delta-set index mapping data. See details below")]
        public IList<uint8> mapData;

        static Int32 mapDataMethod(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.Path, "entryFormat,mapCount");
            if (values[0] is uint8 entryFormat
                && values[1] is uint16 mapCount)
            {
                return mapCount >> ((entryFormat & (Int32)EntryFormatFieldMasks.INNER_INDEX_BIT_COUNT_MASK) + 1);
            }
            return 0;
        }

        static string entryFormatDescription(IItemValueService ivp)
        {
            // A packed field that describes the compressed representation of delta-set indices.See details below.
            if (ivp.Parent.Value is DeltaSetIndexMapFormat0 map)
            {
                Int32 entryFormat = map.entryFormat;
                Int32 mapEntrySize = ((entryFormat & (Int32)EntryFormatFieldMasks.MAP_ENTRY_SIZE_MASK) >> 4) + 1;
                Int32 innerIndexBitCount = entryFormat & (Int32)EntryFormatFieldMasks.INNER_INDEX_BIT_COUNT_MASK;
                return $"mapEntrySize={mapEntrySize},innerIndexBitCount={innerIndexBitCount}";
            }
            return null;
        }
    }

    [TypeName("EntryFormat field masks")]
    enum EntryFormatFieldMasks
    {
        INNER_INDEX_BIT_COUNT_MASK = 0x0F, //Mask for the low 4 bits, which give the count of bits minus one that are used in each entry for the inner-level index.
        MAP_ENTRY_SIZE_MASK = 0x30, // Mask for bits that indicate the size in bytes minus one of each entry.
        Reserved = 0xC0, // Reserved for future use — set to 0.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class VariationRegionList
    {
        [Description(0, "The number of variation axes for this font.This must be the same number as axisCount in the 'fvar' table")]
        public uint16 axisCount;

        [Description(0, "The number of variation region tables in the variation region list. Must be less than 32,768")]
        public uint16 regionCount;

        [Count(0, FieldValueKind.Path, "regionCount")]
        [Description(0, "Array of variation regions")]
        public IList<VariationRegion> variationRegions;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class VariationRegion
    {
        [Count(0, FieldValueKind.Path, "..\\..\\axisCount")]
        [Description(0, "Array of region axis coordinates records, in the order of axes given in the 'fvar' table")]
        public IList<RegionAxisCoordinates> regionAxes;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class RegionAxisCoordinates
    {
        [Description(0, "The region start coordinate value for the current axis")]
        public F2DOT14 startCoord;

        [Description(0, "The region peak coordinate value for the current axis")]
        public F2DOT14 peakCoord;

        [Description(0, "The region end coordinate value for the current axis")]
        public F2DOT14 endCoord;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(ItemVariationStoreFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001")]
    [Invalid]
    class ItemVariationStore
    {
        [Description(0, "Format")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("ItemVariationStore")]
    [BaseName("ItemVariationStore")]
    class ItemVariationStoreFormat1 : ItemVariationStore // inherit for CFF2ValiationStore
    {
        //public uint16 format;

        [TableType(typeof(VariationRegionList))]
        [TablePosition(".\\")]  // for CFF2VariationStore
        [Description(0, "Offset in bytes from the start of the item variation store to the variation region list")]
        public Offset32 variationRegionListOffset;

        [Description(0, "The number of item variation data subtables")]
        public uint16 itemVariationDataCount;

        [Count(0, FieldValueKind.Path, "itemVariationDataCount")]
        [TableType(typeof(ItemVariationData))]
        [TablePosition(".\\")]  // for CFF2VariationStore
        [Description(0, "Offsets in bytes from the start of the item variation store to each item variation data subtable")]
        public IList<Offset32> itemVariationDataOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ItemVariationData
    {
        [Description(0, "The number of delta sets for distinct items")]
        public uint16 itemCount;

        [Description(0, "The number of deltas in each delta set that use a 16-bit representation. Must be less than or equal to regionIndexCount")]
        public uint16 wordDeltaCount;

        [Description(0, "The number of variation regions referenced")]
        public uint16 regionIndexCount;

        [Count(0, FieldValueKind.Path, "regionIndexCount")]
        [Description(0, "Array of indices into the variation region list for the regions referenced by this item variation data table")]
        public IList<uint16> regionIndexes;

        [Count(0, FieldValueKind.Path, "itemCount")]
        [Length(1, "deltaSetLengthMethod")]
        [UniformType(1)]
        [Description(0, "Delta-set rows")]
        public IList<DeltaSet> deltaSets;

        static UInt32 deltaSetLengthMethod(IAttributeService service)
        {
            UInt32 result = 0;
            var values = service.GetValues(FieldValueKind.Path, "itemCount, wordDeltaCount, regionIndexCount");
            if(values.SingleValue(0) is uint16 itemCount
                && values.SingleValue(1) is uint16 wordDeltaCount
                && values.SingleValue(2) is uint16 regionIndexCount)
            {
                bool longWords = (wordDeltaCount & (Int32)WordDeltaMask.LONG_WORDS) != 0;
                Int32 wordCount = wordDeltaCount & (Int32)WordDeltaMask.WORD_DELTA_COUNT_MASK;
                result = (UInt32)(wordCount * 2 + (regionIndexCount - wordCount));
                if (longWords)
                    result *= 2;
            }
            return result;
        }
    }


    [Flags]
    [TypeName("")]
    enum WordDeltaMask
    {
        LONG_WORDS = 0x8000, // Flag indicating that “word” deltas are long (int32)
        WORD_DELTA_COUNT_MASK = 0x7FFF, // Count of “word” deltas
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DeltaSet
    {
        [Count(0, FieldValueKind.Path, "..\\..\\regionIndexCount")]
        [TypeSelectAttribute("deltaDataMethod")]
        [TypeConditionAttribute(typeof(int8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TypeConditionAttribute(typeof(int16), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "2")]
        [TypeConditionAttribute(typeof(int32), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "4")]
        [TypeName("int16 and int8 or int32 and int16")]
        [ValueFormat(1, ValueFormatKind.Default, Option =ValueFormatOption.RawType)]
        [Description(0, "Variation delta values")]
        public IList<ITTFPrimitive> deltaData;

        static object[] deltaDataMethod(IAttributeService service)
        {
            object[] result = null;

            if(service.GetValues(FieldValueKind.Path, "..\\..\\wordDeltaCount").SingleValue(0) is uint16 wordDeltaCount)
            {
                bool longWords = (wordDeltaCount & (Int32)WordDeltaMask.LONG_WORDS) != 0;
                Int32 wordCount = wordDeltaCount & (Int32)WordDeltaMask.WORD_DELTA_COUNT_MASK;
                Int32 index = TablePathHelper.GetLastIndex(service.Path);
                if (index < wordCount)
                    result = new object[] { longWords ? 4 : 2 };
                else
                    result = new object[] { longWords ? 2 : 1 };
            }
            return result;
        }
    }

#pragma warning restore IDE1006
}


