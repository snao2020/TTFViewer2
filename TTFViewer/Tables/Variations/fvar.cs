// var 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("fvar")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "Header\\majorVersion, Header\\minorVersion", null)]
    [ClassTypeCondition(typeof(fvarTable), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("fvar — Font Variations Table")]
    [BaseName("fvar")]
    class fvarTableInvalid
    {
        [FieldName(0, null)]
        public fvarHeaderInvalid Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'fvar' Header")]
    [BaseName("fvarHeader")]
    class fvarHeaderInvalid
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the font variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the font variations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("fvar — Font Variations Table")]
    [BaseName("fvar")]
    class fvarTable
    {
        [FieldName(0, null)]
        public fvarHeader Header;
        
        [Position(0, "\\", FieldValueKind.Path, "Header\\axesArrayOffset")]
        [Count(0, FieldValueKind.Path, "Header\\axisCount")]
        [Description(0, "The variation axis array")]
        public IList<VariationAxisRecord> axes;
        
        [Count(0, FieldValueKind.Path, "Header\\instanceCount")]
        //[Count(0, FieldValueKind.Unsigned, "1")]
        [Description(0, "The named instance array")]
        public IList<InstanceRecord> instances;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'fvar' Header")]
    class fvarHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version number of the font variations table — set to 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version number of the font variations table — set to 0")]
        public uint16 minorVersion;

        [TypeName("Offset16")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset in bytes from the beginning of the table to the start of the VariationAxisRecord array")]
        public uint16 axesArrayOffset;

        [FieldName(0, "(reserved)")]
        [Description(0, "This field is permanently reserved.Set to 2")]
        public uint16 reserved;

        [Description(0, "The number of variation axes in the font (the number of records in the axes array)")]
        public uint16 axisCount;

        [Description(0, "The size in bytes of each VariationAxisRecord — set to 20 (0x0014) for this version")]
        public uint16 axisSize;

        [Description(0, "The number of named instances defined in the font(the number of records in the instances array)")]
        public uint16 instanceCount;

        [Description(0, "The size in bytes of each InstanceRecord — set to either axisCount * sizeof(Fixed) + 4, or to axisCount* sizeof(Fixed) + 6")]
        public uint16 instanceSize;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class VariationAxisRecord
    {
        [Description(0, "Tag identifying the design variation for the axis")]
        public Tag axisTag;

        [Description(0, "The minimum coordinate value for the axis")]
        public Fixed minValue;

        [Description(0, "The default coordinate value for the axis")]
        public Fixed defaultValue;

        [Description(0, "The maximum coordinate value for the axis")]
        public Fixed maxValue;

        [Description(0, "Axis qualifiers — see details below")]
        public uint16 flags;

        [Description(0, "The name ID for entries in the 'name' table that provide a display name for this axis")]
        public uint16 axisNameID;
    }


    [Flags]
    [TypeName("")]
    enum fvarFlags
    {
        HIDDEN_AXIS = 0x0001, // The axis should not be exposed directly in user interfaces.
        Reserved = 0xFFFE, // Reserved for future use — set to 0.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class UserTuple
    {
        [Count(0, FieldValueKind.Path, "\\Header\\axisCount")]
        [Description(0, "Coordinate array specifying a position within the font’s variation space")]
        public Fixed[] coordinates;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class InstanceRecord
    {
        [Description(0, "The name ID for entries in the 'name' table that provide subfamily names for this instance")]
        public uint16 subfamilyNameID;

        [Description(0, "Reserved for future use — set to 0")]
        public uint16 flags;

        [Description(0, "The coordinates array for this instance")]
        public UserTuple coordinates;

        [Description(0, "Optional.The name ID for entries in the 'name' table that provide PostScript names for this instance")]
        public uint16 postScriptNameID;
    }

#pragma warning restore IDE1006
}
