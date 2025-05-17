using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTFViewer.DataTypes;
using TTFViewer.Tables;

namespace TTFViewer.ViewModel
{
    static class ItemValueHelper
    {
        public static Int32 GetFirstIndex(IGroupContainer groupContainer)
        {
            Int32 result = 0;
            while (groupContainer != null)
            {
                result += groupContainer.FirstIndex;
                groupContainer = groupContainer.SourceList as IGroupContainer;
            }
            return result;
        }


        public static Object GetFieldValue(IItemValueService ivs, String fieldName)
        {
            var fi = ivs.Value.GetType().GetField(fieldName);
            var result = fi.GetValue(ivs.Value);
            return result;
        }


        public static Type GetFontTableType(IItemValueService ivp)
        {
            if (ivp != null)
            {
                if (ivp.IsTableModel)
                {
                    var attr2 = AttributeHelper.GetAttribute<FontTableAttribute>(ivp.ValueType);
                    if (attr2 != null)
                        return ivp.ValueType;
                }
                return GetFontTableType(ivp.Parent);
            }
            return null;
        }

        public static string AsciiDescription(IItemValueService ivs)
        {
            if (ivs.Value is IList<uint8> bytes)
            {
                Byte[] b = ValueHelper.uint8ListToByteArray(bytes);
                return $"\"{Encoding.ASCII.GetString(b)}\"";
            }
            else if (ivs.Value is IList<int8> bytes2) // PCLT.typeface
            {
                Byte[] b = ValueHelper.int8ListToByteArray(bytes2);
                return $"\"{Encoding.ASCII.GetString(b)}\"";
            }
            return null;
        }


        public static string UnicodeDescription(IItemValueService ivs)
        {
            if (ivs.Value is IList<uint8> bytes)
            {
                Byte[] b = ValueHelper.uint8ListToByteArray(bytes);
                return $"\"{Encoding.BigEndianUnicode.GetString(b)}\"";
            }
            else if (ivs.Value is IList<int8> bytes2)
            {
                Byte[] b = ValueHelper.int8ListToByteArray(bytes2);
                return $"\"{Encoding.BigEndianUnicode.GetString(b)}\"";
            }
            return null;
        }


        public static string RelativeValueDescription(IItemValueService ivp)
        {
            string result = null;
            if (ivp.GetFontTableValue("head", "unitsPerEm") is uint16 unitsPerEm)
            {
                if (ivp.Value is int16 i16)
                    return $"relative={(double)i16 / (double)unitsPerEm}";
                else if (ivp.Value is uint16 u16)
                    return $"relative={(double)u16 / (double)unitsPerEm}";
            }
            return result;
        }


        public static string InstructionDescription(IItemValueService ivp)
        {
            if (ivp.Parent.Value is IList<uint8> list)
            {
                Int32 index = (Int32)(ivp.FilePosition - ivp.Parent.FilePosition);
                if (IsInstruction(list, index))
                {
                    return ((GlyphInstruction)(Byte)list[index]).ToString();
                }
            }
            return null;
        }


        static bool IsInstruction(IList<uint8> list, Int32 index)
        {
            bool result = true;
            for (Int32 i = 0; i < index; i++)
            {
                Byte instruction = list[i];
                Int32 range = 0;
                if (instruction == (Byte)GlyphInstruction.NPUSHB)
                {
                    Int32 n = list[i + 1];
                    range = n + 1;
                }
                else if (instruction == (Byte)GlyphInstruction.NPUSHW)
                {
                    Int32 n = list[i + 1];
                    range = 2 * n + 1;
                }
                else if ((instruction & 0xF0) == 0xB0)
                {
                    range = (Int32)(instruction & 0x07U) + 1;
                    if ((instruction & 0x08) != 0)
                        range *= 2;
                }
                if (index <= i + range)
                    return false;
                else
                    i += range;
            }
            return result;
        }


        public static string DefaultText(IItemValueService ivp)
        {
            string result;

            if (ivp.Value == null)
            {
                result = "<null>";
            }
            else if (ivp.ValueType == typeof(Tag))
            {
                object value = ivp.Value;
                return $"'{value}' (0x{value:X})";
            }
            else if (ivp.ValueType.IsClass)
            {
                result = null;
            }
            else
            {
                result = ivp.Value.ToString();
            }
            
            return result;
        }


        public static string HexText(IItemValueService ivp)
        {
            object value = ivp.Value;
            if (value == null)
                return "<null>";
            return $"0x{value:X}";
        }
        
        public static string DecimalText(IItemValueService ivp)
        {
            object value = ivp.Value;
            if (value == null)
                return "<null>";
            return $"{value:d}";
        }


        static String GetEnumName(Type enumType, String enumName)
        {
            var fi = enumType.GetField(enumName);
            var attr = AttributeHelper.GetAttribute<FieldNameAttribute>(fi);
            var text = attr?.FieldNameKind == FieldNameKind.Text ? attr.Text : enumName;
            return FormatText(text);

        }

        public static String GetEnumItemName(Type enumType, Int32 value)
        {
            String result = null;

            if (enumType.IsEnum)
            {
                if (Attribute.IsDefined(enumType, typeof(FlagsAttribute)))
                {
                    var list = new List<String>();
                    var array = Enum.GetValues(enumType);
                    foreach(var e in array)
                    {
                        var i = (Int32)e;
                        if((i & value) != 0)
                        {
                            value &= ~i;
                            list.Add(GetEnumName(enumType, e.ToString()));
                        }
                    }
                    if (list.Count > 0)
                        result = string.Join(" | ", list);

                    if (value != 0)
                        result = $"{result} (remains:{value:x8})";
                }
                else
                {
                    object obj = Enum.ToObject(enumType, value);
                    if (Enum.IsDefined(enumType, obj))
                    {
                        result = GetEnumName(enumType, obj.ToString());
                    }
                    else
                    {
                        result = $"unkown={value}";
                    }
                }
            }
            return result;
        }


        public static String FormatText(String text)
        {
            if (text != null)
            {
                text = text.Trim();
                if (text.Length > 0 && text[0] != '\'' && text.Contains(' '))
                    text = $"'{text}'";
            }
            return text;
        }
    }
}
