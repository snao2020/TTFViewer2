// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("post")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(postTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00010000")]
    [ClassTypeCondition(typeof(postTableVersion20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00020000")]
    [ClassTypeCondition(typeof(postTableVersion25), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00025000")]
    [ClassTypeCondition(typeof(postTableVersion30), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00030000")]
    [Invalid]
    [TypeName("post - PostScript Table")]
    [BaseName("post")]
    class postTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "0x00010000 for version 1.0, 0x00020000 for version 2.0 0x00025000 for version 2.5 (deprecated) 0x00030000 for version 3.0")]
        public Version16Dot16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("post - PostScript Table : Version 1.0")]
    [BaseName("post")]
    class postTableVersion10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "0x00010000 for version 1.0, 0x00020000 for version 2.0 0x00025000 for version 2.5 (deprecated) 0x00030000 for version 3.0")]
        public Version16Dot16 version;

        [Description(0, "Italic angle in counter-clockwise degrees from the vertical.Zero for upright text, negative for text that leans to the right(forward)")]
        public Fixed italicAngle;

        [Description(0, "This is the suggested distance of the top of the underline from the baseline(negative values indicate below baseline). The PostScript definition of this FontInfo dictionary key(the y coordinate of the center of the stroke) is not used for historical reasons.The value of the PostScript key may be calculated by subtracting half the underlineThickness from the value of this field")]
        public FWORD underlinePosition;

        [Description(0, "Suggested values for the underline thickness.In general, the underline thickness should match the thickness of the underscore character (U+005F LOW LINE), and should also match the strikeout thickness, which is specified in the OS/2 table")]
        public FWORD underlineThickness;

        [Description(0, "Set to 0 if the font is proportionally spaced, non-zero if the font is not proportionally spaced(i.e.monospaced)")]
        public uint32 isFixedPitch;

        [Description(0, "Minimum memory usage when an OpenType font is downloaded")]
        public uint32 minMemType42;

        [Description(0, "Maximum memory usage when an OpenType font is downloaded")]
        public uint32 maxMemType42;

        [Description(0, "Minimum memory usage when an OpenType font is downloaded as a Type 1 font")]
        public uint32 minMemType1;

        [Description(0, "Maximum memory usage when an OpenType font is downloaded as a Type 1 font")]
        public uint32 maxMemType1;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("post - PostScript Table : Version 2.0")]
    [BaseName("post")]
    class postTableVersion20 : postTableVersion10
    {
        [Description(0, "Number of glyphs(this should be the same as numGlyphs in 'maxp' table)")]
        public uint16 numGlyphs; 

        [Count(0, FieldValueKind.Path, "numGlyphs")]
        [Description(0, "Array of indices into the string data.See below for details.")]
        public IList<uint16> glyphNameIndex;

#if false // use group
        
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, DescriptionKind.Method, "stringDataDescription")]

        [FieldName(1, FieldNameKind.Method, "stringDataName")]
        [ValueFormat(1, ValueFormatKind.Hex)]

        [GroupCountMethod(0, "GroupCount")]
        [GroupItemCountMethod(0, "GroupItemCount")]
        [GroupTextMethod(0, "GroupText")]
        public IList<uint8> stringData;
        
        static string stringDataDescription(IItemValueService ivs)
        {
            Int32 count = GroupCount(ivs);
            var result = $"string count = {count} Storage for the string data";
            return result;
        }

        static string stringDataName(IItemValueService ivs)
        {
            var firstIndex = ItemValueHelper.GetFirstIndex(ivs.GroupContainer);
            var index = TablePathHelper.GetLastIndex(ivs.Name);
            return $"[{firstIndex + index}]";
        }


        static Int32 GroupCount(IItemValueService parentIVS)
        {
            var parent = parentIVS.Parent;
            var v0 = ItemValueHelper.GetFieldValue(parent, "numGlyphs");
            var v1 = ItemValueHelper.GetFieldValue(parent, "glyphNameIndex");
            if (v0 is uint16 numGlyphs && v1 is IList<uint16> glyphNameIndex)
            {
                Int32 count = 0;
                for (int i = 0; i < numGlyphs; i++)
                {
                    if (glyphNameIndex[i] >= 258)
                        count++;
                }
                return count;
            }
            return 0;
        }


        static Int32 GroupItemCount(IItemValueService parentIVS, Int32 stringDataIndex)
        {
            Int32 result = 0;
            if(parentIVS.Value is IList<uint8> list)
            {
                var count = list[stringDataIndex];
                result = count + 1;
            }
            return result;
        }


        static String GroupText(IItemValueService ivp)
        {
            String result = null;
            if (ivp.Value is IList<uint8> list)
            {
                int index = Int32.Parse(ivp.Name.Replace("[", "").Replace("]", "")) + 258;
                var bytes = ValueHelper.uint8ListToByteArray(list);
                return $"glyphNameIndex#{index}=\'{Encoding.ASCII.GetString(bytes, 1, bytes[0])}\'";
            }
            return result;
        }

#else // use uin8[][]

        [TypeName("uint8")]
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Count(0, "stringDataCount2")]
        [Count(1, FieldValueKind.PeekValue, "uint8", "Add:1")]
        [Description(1, DescriptionKind.Method, "pascalStringDescription")]
        [ValueFormat(2, ValueFormatKind.Hex)]
        
        public IList<IList<uint8>> stringData; // [variable]    Storage for the string data.    

        static Int32 stringDataCount2(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.Path, "numGlyphs, glyphNameIndex");
            if (values[0] is uint16 numGlyphs && values[1] is IList<uint16> glyphNameIndex)
            {
                Int32 count = 0;
                for (int i = 0; i < numGlyphs; i++)
                {
                    if (glyphNameIndex[i] >= 258)
                        count++;
                }
                return count;
            }
            return 0;
        }

        static string pascalStringDescription(IItemValueService ivs)
        {
            if (ivs.Value is IList<uint8> list)
            {
                int index = Int32.Parse(ivs.Name.Replace("[", "").Replace("]", "")) + 258;
                var bytes = ValueHelper.uint8ListToByteArray(list);
                return $"NameIndex={index} PascalString \'{Encoding.ASCII.GetString(bytes, 1, bytes[0])}\'";
            }
            return null;
        }
#endif
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("post - PostScript Table : Version 2.5")]
    class postTableVersion25 : postTableVersion10
    {
        [Description(0, "Number of glyphs")]
        public uint16 numGlyphs;
        
        [Count(0, FieldValueKind.Path, "numGlyphs")]
        [Description(0, "Difference between graphic index and standard order of glyph")]
        public IList<int8> offset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("post - PostScript Table : Version 3.0")]
    class postTableVersion30 : postTableVersion10
    {
    }

#pragma warning restore IDE1006
}
