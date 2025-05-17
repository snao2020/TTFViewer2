// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("avar")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(avarTable_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("avar Axis variation table")]
    [BaseName("avar")]
    class avarTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the axis variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the axis variations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("avar Axis variation table")]
    [BaseName("avar")]
    class avarTable_Version10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the axis variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the axis variations table — set to 0")]
        public uint16 minorVersion;

        [FieldName(0, "<reserved>")]
        [Description(0, "Permanently reserved; set to zero")]
        public uint16 reserved;

        [Description(0, "The number of variation axes for this font.This must be the same number as axisCount in the 'fvar' table")]
        public uint16 axisCount;

        [Count(0, FieldValueKind.Path, "axisCount")]
        [Description(0, "The segment maps array — one segment map for each axis, in the order of axes specified in the 'fvar' table")]
        public SegmentMaps[] axisSegmentMaps;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SegmentMaps
    {
        [Description(0, "The number of correspondence pairs for this axis")]
        public uint16 positionMapCount;

        [Count(0, FieldValueKind.Path, "positionMapCount")]
        [Description(0, "The array of axis value map records for this axis")]
        public IList<AxisValueMap> axisValueMaps;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class AxisValueMap
    {
        [Description(0, "A normalized coordinate value obtained using default normalization")]
        public F2DOT14 fromCoordinate;

        [Description(0, "The modified, normalized coordinate value")]
        public F2DOT14 toCoordinate;
    }

#pragma warning restore IDE1006
}
