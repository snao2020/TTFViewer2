// var 1.9.1 format2 is not tested format10 is not tested
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;



namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("cmap")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(cmapTableVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [Invalid]
    [TypeName("cmap - Character to Glyph Index Mapping Table")]
    [BaseName("cmap")]
    class cmapTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(0)")]
        public uint16 version;        
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'cmap' - Character to Glyph Index Mapping Table")]
    class cmapTableVersion0
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number(0)")]
        public uint16 version;
        

        [Description(0, "Number of encoding tables that follow")]
        public uint16 numTables; 
        

        [Count(0, FieldValueKind.Path, "numTables")]
        [Description(1, DescriptionKind.Method, "encodingRecordsDescription")]
        public IList<EncodingRecord> encodingRecords;

        static string encodingRecordsDescription(IItemValueService ivp)
        {
            var cmap = ivp.Parent?.Parent;
            if (cmap != null)
            {
                if (ivp.FilePosition is UInt32 filePosition && cmap.FilePosition is UInt32 cmapPosition)
                {
                    var offset = (UInt32)Marshal.OffsetOf(typeof(EncodingRecord), "subtableOffset");
                    var offsetValue = (Offset32)ivp.LoadValue(filePosition + offset, typeof(Offset32));
                    var format = (uint16)ivp.LoadValue(cmapPosition + offsetValue, typeof(uint16));
                    return $"Subtable Format {format}";
                }
            }
            return null;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class EncodingRecord
    {
        [Description(0, typeof(PlatformIDs))]
        public uint16 platformID;

        //[Description("Platform-specific encoding ID")]
        [Description(0, DescriptionKind.Method, "encodingIDDescription")]
        public uint16 encodingID;

        [TableType(typeof(cmapSubtable))]
        [Description(0, "Byte offset from beginning of table to the subtable for this encoding")]
        public Offset32 subtableOffset;

        static string encodingIDDescription(IItemValueService ivp)
        {
            string result = null;
            if (ivp.Parent.Value is EncodingRecord er)
            {
                switch (er.platformID)
                {
                    case (Int32)PlatformIDs.Unicode:
                        result = ItemValueHelper.GetEnumItemName(typeof(UnicodeEncodingIDs), er.encodingID);
                        break;
                    case (Int32)PlatformIDs.Macintosh:
                        result = "<Older Macintosh versions>";
                        break;
                    case (Int32)PlatformIDs.ISO:
                        break;
                    case (Int32)PlatformIDs.Windows:
                        result = ItemValueHelper.GetEnumItemName(typeof(WindowsEncodingIDs), er.encodingID);
                        break;
                    case (Int32)PlatformIDs.Custom:
                        break;
                }
            }
            return result ?? "<unknown>";
        }
    }


    [TypeName("Platform IDs")]
    enum PlatformIDs
    {
        Unicode = 0, // Various
        Macintosh = 1, // Script manager code
        ISO = 2, // ISO encoding[deprecated]
        Windows = 3, // Windows encoding
        Custom = 4, // Custom
    }


    [TypeName("Encoding IDs")]
    enum UnicodeEncodingIDs // platform ID=0
    {
        [FieldName(0, "Unicode 1.0 semantics")]
        Unicode10 = 0, // semantics—deprecated

        [FieldName(0, "Unicode 1.1 semantics")]
        Unicode11 = 1, // semantics—deprecated

        [FieldName(0, "ISO/IEC 10646 semantics")]
        ISOIEC10646 = 2, //ISO/IEC 10646 semantics—deprecated

        [FieldName(0, "Unicode 2.0 and onwords semantics,Unicode BMP only")]
        Unicode20BMP = 3, // Unicode 2.0 and onwards semantics, Unicode BMP only

        [FieldName(0, "Unicode 2.0 and onwords semantics,Unicode full repertoire")]
        Unicode20Full = 4, // Unicode 2.0 and onwards semantics, Unicode full repertoire

        [FieldName(0, "Unicode variation sequences-for use with subtable format 14")]
        UnicodeVariation = 5, // Unicode Variation Sequences—for use with subtable format 14

        [FieldName(0, "Unicode full repertoire-for use with subtable format 13")]
        UnicodeNull = 6, // Unicode full repertoire—for use with subtable format 13
    }


    // Macintosh platform (platform ID = 1)

    [TypeName("Encoding IDs")]
    enum ISOEncodingIDs // platform ID = 2
    {
        [FieldName(0, "7-bit ASCII")]
        ASCII_7bit = 0,

        [FieldName(0, "ISO 10646")]
        ISO10646 = 1,

        [FieldName(0, "ISO 8859-1")]
        ISO8859_1 = 2,
    }


    [TypeName("Encoding IDs")]
    enum WindowsEncodingIDs // platform ID = 3
    {
        Symbol = 0,
        [FieldName(0, "Unicode BMP")]
        UnicodeBMP = 1,
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


    [TypeName("Encoding IDs")]
    enum CustomEncodingIDs // platform ID = 4
    {
        // Custom platform (platform ID = 4) and OTF Windows NT compatibility mapping
        //0-255 	OTF Windows NT compatibility mapping
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(cmapSubtableFormat0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(cmapSubtableFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(cmapSubtableFormat4), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "4")]
    [ClassTypeCondition(typeof(cmapSubtableFormat6), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "6")]
    [ClassTypeCondition(typeof(cmapSubtableFormat8), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "8")]
    [ClassTypeCondition(typeof(cmapSubtableFormat10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "10")]
    [ClassTypeCondition(typeof(cmapSubtableFormat12), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "12")]
    [ClassTypeCondition(typeof(cmapSubtableFormat13), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "13")]
    [ClassTypeCondition(typeof(cmapSubtableFormat14), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "14")]
    [Invalid]
    [TypeName("'cmap' Subtable")]
    [BaseName("cmapSubtable")]
    [Description(0, "Unknown format number")]
    class cmapSubtable
    {
        [Description(0, "Format number")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 0")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat0
    {
        [Description(0, "Format number is set to 0.")]
        public uint16 format;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "This is the length in bytes of the subtable.")]
        public uint16 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document.")]
        public uint16 language;

        [Count(0, FieldValueKind.Unsigned, "256")]
        [Description(0, "An array that maps character codes to glyph index values")]
        public IList<uint8> glyphIdArray;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 2")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat2
    {
        [Description(0, "Format number is set to 2")]
        public uint16 format;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "This is the length in bytes of the subtable.")]
        public uint16 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document")]
        public uint16 language;

        [Count(0, FieldValueKind.Unsigned, "256")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "Array that maps high bytes to subHeaders: value is subHeader index × 8")]
        public IList<uint16> subHeaderKeys;

        [Count(0, "subHeadersCount")]
        [Description(0, "Variable-length array of SubHeader records.")]
        public IList<SubHeader> subHeaders;

        [Count(0, FieldValueKind.ParentConstraint, null)]
        [Description(0, "Variable-length array containing subarrays used for mapping the low byte of 2-byte characters")]
        public IList<uint16> glyphIdArray;

        static Int32 subHeadersCount(IAttributeService service)
        {
            Int32 result = 0;
            if(service.GetValues(FieldValueKind.Path, "subHeaderKeys").SingleValue(0) is IList<uint16> subHeaderKeys)
            {
                result = subHeaderKeys.Max() / 8 + 1;
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class SubHeader
    {
        [Description(0, "First valid low byte for this SubHeader")]
        public uint16 firstCode;

        [Description(0, "Number of valid low bytes for this SubHeader")]
        public uint16 entryCount;

        [Description(0, "See text below.")]
        public int16 idDelta;

        [Description(0, "See text below")]
        public uint16 idRangeOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 4")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat4
    {
        [Description(0, "Format number is set to 4")]
        public uint16 format;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "This is the length in bytes of the subtable")]
        public uint16 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document")]
        public uint16 language;

        [Description(0, "2 × segCount")]
        public uint16 segCountX2;

        [Description(0, "Maximum power of 2 less than or equal to segCount, times 2 ((2**floor(log2(segCount))) * 2, where “**” is an exponentiation operator)")]
        public uint16 searchRange;

        [Description(0,"Maximum power of 2 less than or equal to segCount, times 2 ((2**floor(log2(segCount))) * 2, where “**” is an exponentiation operator)")]
        public uint16 entrySelector;

        [Description(0, "segCount times 2, minus searchRange((segCount* 2) - searchRange)")]
        public uint16 rangeShift;

        [Count(0, FieldValueKind.Path, "segCountX2", "Div:2")]
        [Description(0, "End characterCode for each segment, last=0xFFFF.")]
        public IList<uint16> endCode;

        [Description(0, "Set to 0.")]
        public uint16 reservedPad;

        [Count(0, FieldValueKind.Path, "segCountX2", "Div:2")]
        [Description(0, "Start character code for each segment.")]
        public IList<uint16> startCode;

        [Count(0, FieldValueKind.Path, "segCountX2", "Div:2")]
        [Description(0, "Delta for all character codes in segment.")]
        public IList<int16> idDelta;

        [Count(0, FieldValueKind.Path, "segCountX2", "Div:2")]
        [Description(0, "Offsets into glyphIdArray or 0")]
        public IList<uint16> idRangeOffsets;

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Glyph index array (arbitrary length)")]
        public IList<uint16> glyphIdArray;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 6")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat6
    {
        [Description(0, "Format number is set to 6.")]
        public uint16 format;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "This is the length in bytes of the subtable.")]
        public uint16 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document.")]
        public uint16 language;

        [Description(0, "First character code of subrange.")]
        public uint16 firstCode;

        [Description(0, "Number of character codes in subrange.")]
        public uint16 entryCount;

        [Count(0, FieldValueKind.Path, "entryCount")]
        [Description(0, "Array of glyph index values for character codes in the range")]
        public IList<uint16> glyphIdArray;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 8")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat8
    {
        [Description(0, "Subtable format; set to 8.")]
        public uint16 format;

        [Description(0, "Reserved; set to 0")]
        public uint16 reserved;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Byte length of this subtable (including the header)")]
        public uint32 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document.")]
        public uint32 language;

        [Count(0, FieldValueKind.Unsigned, "8192")]
        [Description(0, "Tightly packed array of bits (8K bytes total) indicating whether the particular 16-bit(index) value is the start of a 32-bit character code")]
        public IList<uint8> is32;

        [Description(0, "Number of groupings which follow")]
        public uint32 numGroups;

        [Count(0, FieldValueKind.Path, "numGroups")]
        [Description(0, "Array of SequentialMapGroup records.")]
        public IList<SequentialMapGroup> groups;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class SequentialMapGroup
    {
        [Description(0, "First character code in this group; note that if this group is for one or more 16-bit character codes(which is determined from the is32 array), this 32-bit value will have the high 16-bits set to zero")]
        public uint32 startCharCode;

        [Description(0, "Last character code in this group; same condition as listed above for the startCharCode")]
        public uint32 endCharCode;

        [Description(0, "Glyph index corresponding to the starting character code")]
        public uint32 startGlyphID;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 10")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat10
    {
        [Description(0, "Subtable format; set to 10.")]
        public uint16 format;

        [Description(0, "Reserved; set to 0")]
        public uint16 reserved;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Byte length of this subtable (including the header)")]
        public uint32 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document.")]
        public uint32 language;

        [Description(0, "First character code covered")]
        public uint32 startCharCode;

        [Description(0, "Number of character codes covered")]
        public uint32 numChars;

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Array of glyph indices for the character codes covered")]
        public IList<uint16> glyphIdArray;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 12")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat12
    {
        [Description(0, "Subtable format; set to 12.")]
        public uint16 format;

        [Description(0, "Reserved; set to 0")]
        public uint16 reserved;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Byte length of this subtable (including the header)")]
        public uint32 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document")]
        public uint32 language;

        [Description(0, "Number of groupings which follow")]
        public uint32 numGroups;

        [Count(0, FieldValueKind.Path, "numGroups")]
        public IList<SequentialMapGroup> groups;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 13")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat13
    {
        [Description(0, "Subtable format; set to 13.")]
        public uint16 format;

        [Description(0, "Reserved; set to 0")]
        public uint16 reserved;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Byte length of this subtable (including the header)")]
        public uint32 length;

        [Description(0, "For requirements on use of the language field, see “Use of the language field in 'cmap' subtables” in this document.")]
        public uint32 language;

        [Description(0, "Number of groupings which follow")]
        public uint32 numGroups;
    
        [Count(0, FieldValueKind.Path, "numGroups")]
        public IList<ConstantMapGroup> groups; // [numGroups] Array of ConstantMapGroup records.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class ConstantMapGroup
    {
        [Description(0, "First character code in this group")]
        public uint32 startCharCode;

        [Description(0, "Last character code in this group")]
        public uint32 endCharCode;

        [Description(0, "Glyph index to be used for all the characters in the group’s range.")]
        public uint32 glyphID;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassLength(ClassValueKind.FieldPath, "length")]
    [TypeName("'cmap' Subtable Format 14")]
    [BaseName("cmapSubtable")]
    class cmapSubtableFormat14
    {
        [Description(0, "Subtable format.Set to 14.")]
        public uint16 format;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Byte length of this subtable (including this header)")]
        public uint32 length;

        [Description(0, "Number of variation Selector Records")]
        public uint32 numVarSelectorRecords;

        [Count(0, FieldValueKind.Path, "numVarSelectorRecords")]
        [Description(0, "Array of VariationSelector records.")]
        public IList<VariationSelector> varSelector;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class VariationSelector
    {
        [Description(0, "Variation selector")]
        public uint24 varSelector;

        [TableType(typeof(DefaultUVS))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset from the start of the format 14 subtable to Default UVS Table.May be 0.")]
        public Offset32 defaultUVSOffset;

        [TableType(typeof(NonDefaultUVS))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset from the start of the format 14 subtable to Non-Default UVS Table.May be 0")]
        public Offset32 nonDefaultUVSOffset;    
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DefaultUVS
    {
        [Description(0, "Number of Unicode character ranges.")]
        public uint32 numUnicodeValueRanges;

        [Count(0, FieldValueKind.Path, "numUnicodeValueRanges")]
        [Description(0, "Array of UnicodeRange records.")]
        public IList<UnicodeRange> ranges;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class UnicodeRange
    {
        [Description(0, "First value in this range")]
        public uint24 startUnicodeValue;

        [Description(0, "Number of additional values in this range")]
        public uint8 additionalCount;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class NonDefaultUVS
    {
        [Description(0, "Number of UVS Mappings that follow")]
        public uint32 numUVSMappings;

        [Count(0, FieldValueKind.Path, "numUVSMappings")]
        [Description(0, "Array of UVSMapping records")]
        public IList<UVSMapping> uvsMappings;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class UVSMapping
    {
        [Description(0, "Base Unicode value of the UVS")]
        public uint24 unicodeValue;

        [Description(0, "Glyph ID of the UVS")]
        public uint16 glyphID;
    }

#pragma warning restore IDE1006
}
