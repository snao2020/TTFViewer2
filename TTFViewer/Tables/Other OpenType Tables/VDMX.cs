// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;



namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("VDMX")]
    [TypeName("VDMX - Vertical Device Metrics")]
    [BaseName("VDMX")]
    class VDMXTable
    {
        public VdmxHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(VdmxHeaderVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(VdmxHeaderVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    class VdmxHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number(0 or 1).")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("VdmxHeader")]
    class VdmxHeaderVersion0 : VdmxHeader
    {
        //public uint16 version; // Version number(0 or 1).

        [Description(0, "Number of VDMX groups present")]
        public uint16 numRecs;

        [Description(0, "Number of aspect ratio groupings")]
        public uint16 numRatios;

        [Count(0, FieldValueKind.Path, "numRatios")]
        [Description(0, "Ratio record array.")]
        public IList<RatioRange> ratRange;

        [Count(0, FieldValueKind.Path, "numRatios")]
        [TableType(typeof(VDMXGroup))]
        [Description(0, "Offset from start of this table to the VDMXGroup table for a corresponding RatioRange record.")]
        public IList<Offset16> vdmxGroupOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class RatioRange
    {
        [Description(0, "Character set (see below).")]
        public uint8 bCharSet;

        [Description(0, "FieldValue to use for x-Ratio")]
        public uint8 xRatio;

        [Description(0, "Starting y-Ratio value.")]
        public uint8 yStartRatio;

        [Description(0, "Ending y-Ratio value.")]
        public uint8 yEndRatio;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class VDMXGroup
    {
        [Description(0, "Number of height records in this group")]
        public uint16 recs;

        [Description(0, "Starting yPelHeight")]
        public uint8 startsz;

        [Description(0, "Ending yPelHeight")]
        public uint8 endsz;

        [Count(0, FieldValueKind.Path, "recs")]
        [Description(0, "The VDMX records")]
        public IList<vTable> entry;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class vTable
    {
        [Description(0, "yPelHeight to which values apply.")]
        public uint16 yPelHeight;

        [Description(0, "Maximum value (in pels) for this yPelHeight.")]
        public int16 yMax;

        [Description(0, "Minimum value (in pels) for this yPelHeight.")]
        public int16 yMin;
    }
#pragma warning restore IDE1006
}
