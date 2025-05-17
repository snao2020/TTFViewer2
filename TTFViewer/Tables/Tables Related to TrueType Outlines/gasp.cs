// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("gasp")]
    [TypeName("gasp — Grid-fitting and Scan-conversion Procedure Table")]
    [BaseName("gasp")]
    class gaspTable
    {
        [FieldName(0, null)]
        public gaspHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(gaspHeader_Version1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")] // apple
    [ClassTypeCondition(typeof(gaspHeader_Version1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")] // msdocs(1.9.0)
    [Invalid]
    [TypeName("'gasp' Header")]
    [BaseName("gaspHeader")]
    class gaspHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number(set to 1)")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'gasp' Header")]
    [BaseName("gaspHeader")]
    class gaspHeader_Version1 : gaspHeader
    {
        //public uint16 version; // Version number(set to 1)

        [Description(0, "Number of records to follow")]
        public uint16 numRanges;

        [Count(0, FieldValueKind.Path, "numRanges")]
        [Description(0, "Sorted by ppem")]
        public IList<GaspRange> gaspRanges;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class GaspRange
    {
        [Description(0, "Upper limit of range, in PPEM")]
        public uint16 rangeMaxPPEM;

        [Description(0, typeof(RangeGaspBehavior))]
        public uint16 rangeGaspBehavior;
    }


    [Flags]
    enum RangeGaspBehavior
    {
        GASP_GRIDFIT = 0x0001, // Use gridfitting
        GASP_DOGRAY = 0x0002,  // Use grayscale rendering
        GASP_SYMMETRIC_GRIDFIT = 0x0004, // Use gridfitting with ClearType symmetric smoothing
        // Only supported in version 1 'gasp'
        GASP_SYMMETRIC_SMOOTHING = 0x0008, //Use smoothing along multiple axes with ClearType®
        // Only supported in version 1 'gasp'
        Reserved = 0xFFF0, // Reserved flags — set to 0
    }
    ;
#pragma warning restore IDE1006
}
