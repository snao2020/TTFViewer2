// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("hdmx")]
    [TypeName("hdmx — Horizontal Device Metrics")]
    [BaseName("hdmx")]
    class hdmxTable
    {
        [FieldName(0, null)]
        public HdmxHeader Header;
    }

    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(HdmxHeaderVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [Invalid]
    class HdmxHeader
    {
        [Description(0, "Table version")]
        public uint16 version;
    }
    

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("'hdmx' Header")]
    class HdmxHeaderVersion0 : HdmxHeader
    {
        //public uint16 version; // Table version number(0)

        [Description(0, "Number of device records.")]
        public uint16 numRecords;

        [Description(0, "Size of a device record, 32-bit aligned.")]
        public uint32 sizeDeviceRecord;

        [Count(0, FieldValueKind.Path, "numRecords")]
        [Length(1, FieldValueKind.Path, "sizeDeviceRecord", null)]
        [Description(0, "Array of device records.")]
        public IList<DeviceRecord> records;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class DeviceRecord
    {
        [Description(0, "Pixel size for following widths(as ppem).")]
        public uint8 pixelSize;

        [Description(0, "Maximum width.")]
        public uint8 maxWidth;

        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "Array of widths(numGlyphs is from the 'maxp' table).")]
        public IList<uint8> widths;
    }
#pragma warning restore IDE1006
}
