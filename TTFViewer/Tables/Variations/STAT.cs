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
    [FontTable("STAT")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "Header\\majorVersion, Header\\minorVersion", null)]
    [ClassTypeCondition(typeof(STATTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [ClassTypeCondition(typeof(STATTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0001")]
    [ClassTypeCondition(typeof(STATTableVersion12), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0002")]
    [TypeName("STAT — Style Attributes Table")]
    [BaseName("STAT")]
    class STATTableInvalid
    {
        public StyleAttributesHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("STAT")]
    [TypeName("STAT — Style Attributes Table")]
    [BaseName("STAT")]
    class STATTableVersion10
    {
        public StyleAttributesHeaderVersion10 Header;

        [Position(0, "\\", FieldValueKind.Path, "Header\\designAxesOffset")]
        [Count(0, FieldValueKind.Path, "Header\\designAxisCount")]
        public IList<AxisRecord> designAxes; // [designAxisCount] The design-axes array.

        [Position(0, "\\", FieldValueKind.Path, "Header\\offsetToAxisValueOffsets")]
        [Count(0, FieldValueKind.Path, "Header\\axisValueCount")]
        [TableType(typeof(AxisValueTable))]
        [TablePosition("\\", FieldValueKind.Path, "Header\\offsetToAxisValueOffsets")]
        [Description(0, "Array of offsets to axis value tables, in bytes from the start of the axis value offsets array")]
        public IList<Offset16> axisValueOffsets; //[axisValueCount] Array of offsets to axis value tables, in bytes from the start of the axis value offsets array
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("STAT")]
    [TypeName("STAT — Style Attributes Table")]
    [BaseName("STAT")]
    class STATTableVersion12
    {
        public StyleAttributesHeaderVersion12 Header;

        [Position(0, "\\", FieldValueKind.Path, "Header\\designAxesOffset")]
        [Count(0, FieldValueKind.Path, "Header\\designAxisCount")]
        public IList<AxisRecord> designAxes; // [designAxisCount] The design-axes array.

        [Position(0, "\\", FieldValueKind.Path, "Header\\offsetToAxisValueOffsets")]
        [Count(0, FieldValueKind.Path, "Header\\axisValueCount")]
        [TablePosition("\\", FieldValueKind.Path, "Header\\offsetToAxisValueOffsets")]
        [TableType(typeof(AxisValueTable))]
        [Description(0, "Array of offsets to axis value tables, in bytes from the start of the axis value offsets array")]
        public IList<Offset16> axisValueOffsets; //[axisValueCount] Array of offsets to axis value tables, in bytes from the start of the axis value offsets array
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Style attributes header")]
    class StyleAttributesHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the style attributes table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the style attributes table — set to 0 or 2")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Style attributes header")]
    [BaseName("StyleAttributesHeader")]
    class StyleAttributesHeaderVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the style attributes table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the style attributes table — set to 0 or 2")]
        public uint16 minorVersion;

        [Description(0, "The size in bytes of each axis record")]
        public uint16 designAxisSize;

        [Description(0, "The number of design axis records.In a font with an 'fvar' table, this value must be greater than or equal to the axisCount value in the 'fvar' table.In all fonts, must be greater than zero if axisValueCount is greater than zero")]
        public uint16 designAxisCount;

        [TypeName("Offset32")]
        [ValueFormat(0, ValueFormatKind.Hex, Option=ValueFormatOption.RawType)]
        [Description(0, "Offset in bytes from the beginning of the STAT table to the start of the design axes array. If designAxisCount is zero, set to zero; if designAxisCount is greater than zero, must be greater than zero")]
        public uint32 designAxesOffset;

        [Description(0, "The number of axis value tables")]
        public uint16 axisValueCount;

        [TypeName("Offset32")]
        [ValueFormat(0, ValueFormatKind.Hex, Option =ValueFormatOption.RawType)]
        [Description(0, "Offset in bytes from the beginning of the STAT table to the start of the design axes value offsets array. If axisValueCount is zero, set to zero; if axisValueCount is greater than zero, must be greater than zero")]
        public uint32 offsetToAxisValueOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Style attributes header")]
    class StyleAttributesHeaderVersion12
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the style attributes table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the style attributes table — set to 0 or 2")]
        public uint16 minorVersion;

        [Description(0, "The size in bytes of each axis record")]
        public uint16 designAxisSize;

        [Description(0, "The number of design axis records.In a font with an 'fvar' table, this value must be greater than or equal to the axisCount value in the 'fvar' table.In all fonts, must be greater than zero if axisValueCount is greater than zero")]
        public uint16 designAxisCount;

        [TypeName("Offset32")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset in bytes from the beginning of the STAT table to the start of the design axes array. If designAxisCount is zero, set to zero; if designAxisCount is greater than zero, must be greater than zero")]
        public uint32 designAxesOffset;

        [Description(0, "The number of axis value tables")]
        public uint16 axisValueCount;

        [TypeName("Offset32")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset in bytes from the beginning of the STAT table to the start of the design axes value offsets array. If axisValueCount is zero, set to zero; if axisValueCount is greater than zero, must be greater than zero")]
        public uint32 offsetToAxisValueOffsets;

        [Description(0, "Name ID used as fallback when projection of names into a particular font model produces a subfamily name containing only elidable elements")]
        public uint16 elidedFallbackNameID;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class AxisRecord
    {
        [Description(0, "A tag identifying the axis of design variation")]
        public Tag axisTag;

        [Description(0, "The name ID for entries in the 'name' table that provide a display string for this axis")]
        public uint16 axisNameID;

        [Description(0, "A value that applications can use to determine primary sorting of face names, or for ordering of descriptors when composing family or face names")]
        public uint16 axisOrdering;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(AxisValueFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001")]
    [ClassTypeCondition(typeof(AxisValueFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0002")]
    [ClassTypeCondition(typeof(AxisValueFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0003")]
    [ClassTypeCondition(typeof(AxisValueFormat4), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0004")]
    [Invalid]
    [TypeName("Axis Value table")]
    [BaseName("AxisValue")]
    class AxisValueTable
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Axis value table, format 1")]
    [BaseName("AxisValue")]
    class AxisValueFormat1
    {
        [Description(0, "Format identifier — set to 1")]
        public uint16 format;

        [Description(0, "Zero-base index into the axis record array identifying the axis of design variation to which the axis value record applies.Must be less than designAxisCount")]
        public uint16 axisIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Flags — see below for details")]
        public uint16 flags;

        [Description(0, "The name ID for entries in the 'name' table that provide a display string for this attribute value")]
        public uint16 valueNameID;

        [Description(0, "A numeric value for this attribute value")]
        public Fixed value;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Axis value table, format 2")]
    [BaseName("AxisValue")]
    class AxisValueFormat2
    {
        [Description(0, "Format identifier — set to 2")]
        public uint16 format;

        [Description(0, "Zero-base index into the axis record array identifying the axis of design variation to which the axis value record applies.Must be less than designAxisCount")]
        public uint16 axisIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Flags — see below for details")]
        public uint16 flags;

        [Description(0, "The name ID for entries in the 'name' table that provide a display string for this attribute value")]
        public uint16 valueNameID;

        [Description(0, "A nominal numeric value for this attribute value")]
        public Fixed nominalValue;

        [Description(0, "The minimum value for a range associated with the specified name ID")]
        public Fixed rangeMinValue;

        [Description(0, "The maximum value for a range associated with the specified name ID")]
        public Fixed rangeMaxValue;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Axis value table, format 3")]
    [BaseName("AxisValue")]
    class AxisValueFormat3
    {
        [Description(0, "Format identifier — set to 3")]
        public uint16 format;

        [Description(0, "Zero-base index into the axis record array identifying the axis of design variation to which the axis value record applies.Must be less than designAxisCount")]
        public uint16 axisIndex;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Flags — see below for details")]
        public uint16 flags;

        [Description(0, "The name ID for entries in the 'name' table that provide a display string for this attribute value")]
        public uint16 valueNameID;

        [Description(0, "A numeric value for this attribute value")]
        public Fixed value;

        [Description(0, "The numeric value for a style-linked mapping from this value")]
        public Fixed linkedValue;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Axis value table, format 4")]
    [BaseName("AxisValue")]
    class AxisValueFormat4
    {
        [Description(0, "Format identifier — set to 4")]
        public uint16 format;

        [Description(0, "The total number of axes contributing to this axis-values combination")]
        public uint16 axisCount;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Flags — see below for details")]
        public uint16 flags;

        [Description(0, "The name ID for entries in the 'name' table that provide a display string for this combination of axis values")]
        public uint16 valueNameID;

        [Count(0, FieldValueKind.Path, "axisCount")]
        [Description(0, "Array of AxisValue records that provide the combination of axis values, one for each contributing axis")]
        public AxisValue[] axisValues;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class AxisValue
    {
        [Description(0, "Zero-base index into the axis record array identifying the axis to which this value applies. Must be less than designAxisCount")]
        public uint16 axisIndex;

        [Description(0, "A numeric value for this attribute value")]
        public Fixed value;
    }


    [Flags]
    enum STATFlags
    {
        OLDER_SIBLING_FONT_ATTRIBUTE = 0x0001, // If set, this axis value table provides axis value information that is applicable to other fonts within the same font family. This is used if the other fonts were released earlier and did not include information about values for some axis. If newer versions of the other fonts include the information themselves and are present, then this record is ignored.
        ELIDABLE_AXIS_VALUE_NAME = 0x0002, // If set, it indicates that the axis value represents the “normal” value for the axis and may be omitted when composing name strings.
        Reserved = 0xFFFC, // Reserved for future use — set to zero.
    }

#pragma warning restore IDE1006
}
