// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("EBLC")]
    [TypeName("EBLC — Embedded Bitmap Location Table")]
    [BaseName("EBLC")]
    class EBLCTable
    {
        [FieldName(0, null)]
        public EBLCHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(EBLCHeader_Version20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0002, 0x0000")]
    [Invalid]
    [TypeName("EBLC Header")]
    [BaseName("EBLCHeader")]
    class EBLCHeader
    { 
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the EBLC table, = 2")]
        public uint16 majorVersion; // Major version of the EBLC table, = 2.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the EBLC table, = 0")]
        public uint16 minorVersion; // Minor version of the EBLC table, = 0.
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("EBLC Header")]
    class EBLCHeader_Version20 : EBLCHeader
    {
        //public uint16 majorVersion; // Major version of the EBLC table, = 2.
        //public uint16 minorVersion; // Minor version of the EBLC table, = 0.

        [Description(0, "Number of BitmapSize tables")]
        public uint32 numSizes; // Number of BitmapSize tables.    

        [Count(0, FieldValueKind.Path, "numSizes")]
        [Description(0, "BitmapSize records array")]
        public IList<BitmapSize> bitmapSizes; // BitmapSize records array.
    }
#pragma warning restore IDE1006
}
