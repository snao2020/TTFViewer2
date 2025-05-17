//ver1,9,1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", "Mask:0x7f")]
    [ClassTypeCondition(typeof(CFFEncodings_Format0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(CFFEncodings_Format1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [TypeName("Encodings")]
    [BaseName("Encodings")]
    class CFFEncodings
    {
        [TypeName("Card8")]
        [Description(0, "0 or 1")]
        public uint8 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Encodings Format0")]
    class CFFEncodings_Format0 : CFFEncodings
    {
        //public uint8 format; // =0

        [TypeName("Card8")]
        [Description(0, "Number of encoded glyphs")]
        public uint8 nCodes;

        [Count(0, FieldValueKind.Path, "nCodes")]
        [TypeName("Card8")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "Code array")]
        public uint8[] code;

        [TypeSelect(FieldValueKind.Path, "format", "Mask:0x80")]
        [TypeCondition(null, AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0x00")]
        public SupplementalEncodingData supplementalEncodingData;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Encodings Format1")]
    class CFFEncodings_Format1 : CFFEncodings
    {
        //public uint8 format; // = 1

        [TypeName("Cord8")]
        [Description(0, "Number of code ranges")]
        public uint8 nRanges;

        [Count(0, FieldValueKind.Path, "nRanges")]
        [Description(0, "Range1 array(see Table 13)")]
        public EncodingRange1[] Range1;

        [TypeSelect(FieldValueKind.Path, "format", "Mask:0x80")]
        [TypeCondition(null, AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0x00")]
        public SupplementalEncodingData supplementalEncodingData;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Range1 Format (Encoding)")]
    class EncodingRange1
    {
        [TypeName("Card8")]
        [Description(0, "First code in range")]
        public uint8 first;

        [TypeName("Card8")]
        [Description(0, "Codes left in range(excluding first)")]
        public uint8 nLeft;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Supplemental Encoding Data")]
    class SupplementalEncodingData
    {
        [TypeName("Card8")]
        [Description(0, "Number of supplementary mappings")]
        public uint8 nSups;

        [Count(0, FieldValueKind.Path, "nSups")]
        [Description(0, "Supplementary encoding array(see Table 15  below)")]
        public Supplement[] Supplement;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Supplemental Encoding Data")]
    class Supplement
    {
        [TypeName("Card8")]
        [Description(0, "Encoding")]
        public uint8 code;

        [TypeName("SID")]
        [Description(0, "Name")]
        public uint16 glyph;
    }


    [TypeName("Encoding ID")]
    enum EncodingID
    {
        [FieldName(0, "Standard Encoding")]
        StandardEncoding = 0,
        [FieldName(0, "Expert Encoding")]
        ExpertEncoding = 1,
    }
#pragma warning restore IDE1006
}
