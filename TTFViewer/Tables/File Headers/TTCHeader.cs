// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [StartupTable("ttcf")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(TTCHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [ClassTypeCondition(typeof(TTCHeader_Version20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0002, 0x0000")]
    [Invalid]
    [TypeName("TTC Header")]
    [BaseName("TTCHeader")]
    class TTCHeaderTable
    {
        [Description(0, "Font Collection ID string: 'ttcf' (used for fonts with CFF or CFF2 outlines as well as TrueType outlines)")]
        public Tag ttcTag;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the TTC Header")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the TTC Header")]
        public uint16 minorVersion;         
    }


    // batang.ttc version5.00
    //  majorVerion=1,minorVersion=0
    //  but has DSIG table
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("TTC Header Version 1.0")]
    [BaseName("TTCHeader")]
    class TTCHeader_Version10
    {
        [Description(0, "Font Collection ID string: 'ttcf' (used for fonts with CFF or CFF2 outlines as well as TrueType outlines)")]
        public Tag ttcTag;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the TTC Header, = 1 or 2.")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the TTC Header, = 0.")]
        public uint16 minorVersion;

        [Description(0, "Number of fonts in TTC")]
        public uint32 numFonts;

        [Count(0, FieldValueKind.Path, "numFonts")]
        [TableType(typeof(TableDirectory))]
        [TablePosition(null)]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "Array of offsets to the TableDirectory for each font from the beginning of the file")]
        public Offset32[] tableDirectoryOffsets;

        [TypeSelectAttribute(FieldValueKind.PeekValue, "Tag", null)]
        [TypeConditionAttribute(typeof(Tag), AttributeConditionKind.Equal, FieldValueKind.Tag, "DSIG")]
        [TypeName("uint32")]
        [Description(0, "Tag indicating that a DSIG table exists, 0x44534947 ('DSIG') (null if no signature)")]
        public Tag? dsigTag;         

        [TypeSelectAttribute(FieldValueKind.Path, "dsigTag", null)]
        [TypeConditionAttribute(typeof(uint32), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<any>")]
        [TypeName("uint32")]
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The length(in bytes) of the DSIG table(null if no signature)")]
        public uint32? dsigLength;   

        [TypeSelectAttribute("dsigOffsetMethod")]
        [TypeConditionAttribute(typeof(Offset32), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<any>")]
        [TableType(typeof(DSIGTable))]
        [TablePosition(null, Flags = TablePositionFlag.MayBeNULL)]
        [TableLength(TableLengthKind.FileLength, FieldValueKind.Path, "dsigLength")]
        [TypeName("uint32")]
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "The offset(in bytes) of the DSIG table from the beginning of the TTC file(null if no signature)")]
        public Offset32? dsigOffset; 

        static object[] dsigOffsetMethod(IAttributeService service)
        {
            object[] result = null;

            var values = service.GetValues(FieldValueKind.Path, "dsigTag, dsigLength");
            if (values.SingleValue(0) is Tag dsigTag 
                && values.SingleValue(1) is uint32 dsigLength)
            {
                if (dsigTag == (Tag)"DSIG" && dsigLength > 0)
                {
                    result = new object[] { 0 };
                }
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("TTC Header Version 2.0")]
    [BaseName("TTCHeader")]
    class TTCHeader_Version20 : TTCHeader_Version10
    {
    }

#pragma warning restore IDE1006
}
