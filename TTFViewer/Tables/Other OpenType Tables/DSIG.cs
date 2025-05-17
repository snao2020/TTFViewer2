// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("DSIG")]
    [TypeName("DSIG - Digital Signature Table")]
    [BaseName("DSIG")]
    class DSIGTable
    {
        [FieldName(0, null)]
        public DsigHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(DsigHeaderVersion1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    class DsigHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number of the DSIG table")]
        public uint32 version; 
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("DsigHeader")]
    [BaseName("DsigHeader")]
    class DsigHeaderVersion1 : DsigHeader
    {
        //public uint32 version; // Version number of the DSIG table(0x00000001)

        [Description(0, "Number of signatures in the table")]
        public uint16 numSignatures;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "permission flags Bit 0: cannot be resigned Bits 1-7: Reserved(Set to 0)")]
        public uint16 flags;

        [Count(0, FieldValueKind.Path, "numSignatures")]
        [Description(0, "Array of signature records")]
        public IList<SignatureRecord> signatureRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class SignatureRecord
    {
        [Description(0, "Format of the signature")]
        public uint32 format;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Length of signature in bytes")]
        public uint32 length;

        [TableSelect(FieldValueKind.Path, "format")]
        [TableCondition(typeof(SignatureBlockFormat1), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TableCondition(typeof(SignatureBlock), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TableLength(TableLengthKind.FileLength, FieldValueKind.Path, "length")]
        [Description(0, "Offset to the signature block from the beginning of the table")]
        public Offset32 signatureBlockOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [TypeName("Signature Block")]
    [BaseName("SignatureBlock")]
    class SignatureBlock
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Signature Block Format 1")]
    [BaseName("SignatureBlock")]
    class SignatureBlockFormat1
    {
        [Description(0, "Reserved for future use; set to zero.")]
        public uint16 reserved1;

        [Description(0, "Reserved for future use; set to zero.")]
        public uint16 reserved2;

        [Description(0, "Length(in bytes) of the PKCS#7 packet in the signature field.")]
        public uint32 signatureLength;

        [Count(0, FieldValueKind.Path, "signatureLength")]
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "PKCS#7 packet")]
        public IList<uint8> signature;
    }
#pragma warning restore IDE1006
}
