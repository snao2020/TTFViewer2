//ver1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(CFFCharsets_Format0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(CFFCharsets_Format1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(CFFCharsets_Format2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [TypeName("Charsets")]
    [BaseName("Charsets")]
    class CFFCharsets
    {
        [TypeName("Card8")]
        [Description(0, "0 or 1 or 2")]
        public uint8 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Charsets Format0")]
    class CFFCharsets_Format0 : CFFCharsets
    {
        //public uint8 format; // = 0

        [Count(0, FieldValueKind.Path, "\\CharStringsINDEX\\[]\\count", "SubIfNonZero:1")]
        [TypeName("SID")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(0, "[nGlyphs–1] Glyph name array")]
        public IList<uint16> glyph;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Charsets Format1")]
    class CFFCharsets_Format1 : CFFCharsets
    {
        //public uint8 format; // = 1

        [Count(0, "CountRange1")]
        [TypeName("Range1")]
        [Description(0, "[<varies>] Range1 array(see Table 19)")]
        public IList<CharsetRange1> Range1;

        static Int32 CountRanges1(IAttributeService service)
        {
            Int32 result = 0;
            
            if (service.GetValues(FieldValueKind.Path, "\\CharStringsINDEX\\[]\\count").SingleValue(0) is uint16 count)
            {
                result = CFFCharsetHelper.CountRange(service.PrimitiveReader, service.FilePosition, typeof(uint8), count);
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Range1 Format(Charset)")]
    class CharsetRange1
    {
        [TypeName("SID")]
        [Description(0, "First glyph in range")]
        public uint16 first;

        [TypeName("Card8")]
        [Description(0, "Glyphs left in range(excluding first)")]
        public uint8 nLeft;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Charsets Format2")]
    class CFFCharsets_Format2 : CFFCharsets
    {
        //public uint8 format; // = 2
        
        [Count(0, "CountRange2")]
        [TypeName("Range2")]
        [Description(0, "[<varies>] Range2 array(see Table 21)")]
        public IList<CharsetRange2> Range2;
        
        static Int32 CountRange2(IAttributeService service)
        {
            Int32 result = 0;

            if (service.GetValues(FieldValueKind.Path, "\\CharStringsINDEX\\[]\\count").SingleValue(0) is uint16 count)
            {
                result = CFFCharsetHelper.CountRange(service.PrimitiveReader, service.FilePosition, typeof(uint16), count);
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Range2 Format(Charset)")]
    class CharsetRange2
    {
        [TypeName("SID")]
        [Description(0, "First glyph in range")]
        public uint16 first;

        [TypeName("Card16")]
        [Description(0, "Glyphs left in range(excluding first)")]
        public uint16 nLeft;
    }


    [TypeName("Charset ID")]
    enum CharsetID
    {
        ISOAdobe = 0,
        Expert = 1,
        ExpertSubset = 2,
    }

//------------------------------------------------------------------------------

    static class CFFCharsetHelper
    {
        public static Int32 CountRange(PrimitiveReader reader, UInt32 filePosition, Type nLeftType, UInt32 glyphCount)
        {
            glyphCount--; // .notdef glyph name is omitted

            UInt32 nLeftSize = (UInt32)Marshal.SizeOf(nLeftType);
            Int32 result = 0;
            do
            {
                result++;
                filePosition += 2; // skip 'first'
                var nleft = reader.Read(nLeftType, filePosition).ToNumber4();
                filePosition += nLeftSize;
                glyphCount -= 1 + nleft;
            }
            while (glyphCount > 0);

            return result;
        }
    }
#pragma warning restore IDE1006
}
