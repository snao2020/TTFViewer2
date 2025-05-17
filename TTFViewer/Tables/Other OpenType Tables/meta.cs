// var 1.9.1 not tested
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("meta")]
    [TypeName("meta — Metadata Table")]
    [BaseName("meta")]
    class metaTable
    {
        [FieldName(0, null)]
        public MetaHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(MetaHeaderVersion1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [TypeName("Metadata header")]
    [BaseName("MetaHeader")]
    class MetaHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number of the metadata table — set to 1.")]
        public uint32 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Metadata header")]
    class MetaHeaderVersion1 : MetaHeader
    {
        //public uint32 version; //  Version number of the metadata table — set to 1.

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Flags — currently unused; set to 0.")]
        public uint32 flags;

        [TypeName("(reserved)")]
        [Description(0, "Not used; should be set to 0.")]
        public uint32 reserved;

        [Description(0, "The number of data maps in the table.")]
        public uint32 dataMapsCount;

        [Count(0, FieldValueKind.Path, "dataMapsCount")]
        [Description(0, "Array of data map records.")]
        public IList<DataMap> dataMaps;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class DataMap
    {
        [Description(0, "A tag indicating the type of metadata.")]
        public Tag tag;

        [TableType(typeof(IList<uint8>))]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "dataLength")]
        [Description(0, "Offset in bytes from the beginning of the metadata table to the data for this tag.")]
        public Offset32 dataOffset;

        [Description(0, "Length of the data, in bytes.The data is not required to be padded to any byte boundary.")]
        public uint32 dataLength;
    }

    /*
    Metadata Tags
    Tag Name    Format Description
    appl(reserved)      Reserved — used by Apple.
    bild(reserved) Reserved — used by Apple.
    dlng Design languages Text, using only Basic Latin(ASCII) characters.Indicates languages and/or scripts for the user audiences that the font was primarily designed for. Only one instance is used.See below for additional details.
    slng    Supported languages     Text, using only Basic Latin(ASCII) characters.Indicates languages and/or scripts that the font is declared to be capable of supporting.Only one instance is used.See below for additional details.
    */

#pragma warning restore IDE1006
}
