// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("CBLC")]
    [TypeName("CBLC — Color Bitmap Location Table")]
    class CBLCTable
    {
        [FieldName(0, null)]
        public CBLCHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(CBLCHeader_Version30), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0003,0x0000")]
    [Invalid]
    [TypeName("CblcHeader")]
    [BaseName("CBLCHeader")]
    class CBLCHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the CBLC table")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the CBLC table")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("CblcHeader")]
    class CBLCHeader_Version30 : CBLCHeader
    {
        //public uint16 majorVersion; // Major version of the CBLC table, = 3.
        //public uint16 minorVersion; // Minor version of the CBLC table, = 0.

        [Description(0, "Number of BitmapSize tables")]
        public uint32 numSizes;

        [Count(0, FieldValueKind.Path, "numSizes")]
        [Description(0, "BitmapSize records array")]
        public IList<BitmapSize> bitmapSizes;
    }
#pragma warning restore IDE1006
}
