// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("EBSC")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(EBSCTable_Version20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0002, 0x0000")]
    [Invalid]
    [TypeName("EBSC — Embedded Bitmap Scaling Table")]
    [BaseName("EBSCTable")]
    class EBSCTable
    {
        [Description(0, "Major version of the EBSC table")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the EBSC table")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("EBSC — Embedded Bitmap Scaling Table")]
    [BaseName("EBSCTable")]
    class EBSCTable_Version20
    {
        [Description(0, "Major version of the EBSC table")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the EBSC table")]
        public uint16 minorVersion;

        public uint32 numSizes;

        [Count(0, FieldValueKind.Path, "numSizes")]
        [Description(0, "Array of BitmapScale records, one for each strike")]
        public IList<BitmapScale> strikes;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class BitmapScale
    {
        [Description(0, "line metrics")]
        public SbitLineMetrics hori;

        [Description(0, "line metrics")]
        public SbitLineMetrics vert;

        [Description(0, "target horizontal pixels per Em")]
        public uint8 ppemX;

        [Description(0, "target vertical pixels per Em")]
        public uint8 ppemY;

        [Description(0, "use bitmaps of this size")]
        public uint8 substitutePpemX;

        [Description(0, "use bitmaps of this size")]
        public uint8 substitutePpemY;
    }
#pragma warning restore IDE1006
}
