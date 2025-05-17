//ver1.9.1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DICT
    {
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Values(0, "DataMethod")]
        [FieldName(0, null)]
        [Description(1, DescriptionKind.Method, "OperatorDescription")]
        public IList<DICTData> Data;

        static (UInt32, IEnumerable<DICTData>)? DataMethod(IAttributeService service)
        {
            if (service.GetValues(FieldValueKind.ParentConstraint, null).SingleValue(0) is UInt32 fileLength)
            {
                var filePosition = service.FilePosition;
                //Func<UInt32, IList<DICTData>> result = fileLength => DICTHelper.EnumDICTData(service.PrimitiveReader, filePosition, fileLength).ToArray();
                return (fileLength, DICTHelper.EnumDICTData(service.PrimitiveReader, filePosition, fileLength));
            }
            return null;
        }

        static string OperatorDescription(IItemValueService ivp)
        {
            if (ivp.Value is DICTData data)
            {
                if (DICTHelper.GetOperatorInteger(data.Operator) is Int32 i32)
                {
                    Type fontTableType = ItemValueHelper.GetFontTableType(ivp);
                    if (fontTableType == typeof(CFFTable))
                        return ItemValueHelper.GetEnumItemName(typeof(CFFDICTOperators), i32);
                    else if (fontTableType == typeof(CFF2Table))
                        return ItemValueHelper.GetEnumItemName(typeof(CFF2DICTOperators), i32);
                }
            }
            return null;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("DICT Data")]
    class DICTData
    {
        [Values(0, ValuesKind.ParentValue, "Operands")]
        [Values(1, ValuesKind.ParentValue, null)]
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [ValueFormat(1, "DICTOperandText")]
        [ValueFormat(2, ValueFormatKind.Hex)]
        public uint8[][] Operands;

        [Values(0, ValuesKind.ParentValue, "Operator")]
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [ValueFormat(0, "DICTOperatorText")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        public uint8[] Operator;

        static string DICTOperandText(IItemValueService ivp)
        {
            if (ivp.Value is uint8[] token)
            {
                Int32? i32 = DICTHelper.GetOperandInteger(token);
                if (i32 != null)
                {
                    switch (token.Length)
                    {
                        case 1: return $"{i32} (0x{i32:x02})";
                        case 2:
                        case 3: return $"{i32} (0x{i32:x04})";
                        case 5: return $"{i32} (0x{i32:x08})";
                        default: return null;
                    }
                }
                else
                    return DICTHelper.GetOperandFloat(token);
            }
            return null;
        }


        static string DICTOperatorText(IItemValueService ivp)
        {
            if (ivp.Value is uint8[] array)
            {
                if (DICTHelper.GetOperatorInteger(array) is Int32 i32)
                {
                    Type fontTableType = ItemValueHelper.GetFontTableType(ivp);
                    if (fontTableType == typeof(CFFTable))
                        return ItemValueHelper.GetEnumItemName(typeof(CFFDICTOperators), i32);
                    else if (fontTableType == typeof(CFF2Table))
                        return ItemValueHelper.GetEnumItemName(typeof(CFF2DICTOperators), i32);
                }
            }
            return null;
        }
    }

//---------------------------------------------------

    static class DICTHelper
    {
        public static IEnumerable<DICTData> EnumDICTData(PrimitiveReader reader, UInt32 filePosition, UInt32 fileLength)
        {
            var operands = new List<uint8[]>();
            foreach (var token in EnumToken(reader, filePosition, fileLength))
            {
                if(token.tokenKind.HasFlag(CFFTokenKind.Operand))
                    operands.Add(token.token);

                else if (token.tokenKind.HasFlag(CFFTokenKind.Operator))
                {
                    if (operands.Count > 0)
                    {
                        var operandArray = operands.ToArray();
                        operands.Clear();
                        yield return new DICTData { Operands = operandArray, Operator = token.token };
                    }
                    else
                        break;
                }
                else
                    throw new InvalidOperationException("DICTHelper.EnumDICTData"); ;
            }
            yield break;
        }

        /*
        Operators and operands may be distinguished by inspection of 
        their first byte: 0–21 specify operators and 28, 29, 30, and 
        32–254 specify operands (numbers). Byte values 22–27, 31, and 
        255 are reserved. An operator may be preceded by up to a 
        maximum of 48 operands. 

        Two-byte operators have an initial escape byte of 12

        Operand Encoding
        Size    b0 range    FieldValue range         FieldValue calculation
        1       [32, 246]   [–107, +107]        b0–139
        2       [247, 250]  [+108, +1131]       (b0–247)*256+b1+108
        2       [251, 254]  [–1131, –108]       –(b0–251)*256–b1–108
        3       [28]        [–32768, 32767]     b1<<8|b2
        5       [29]        [–(2^31),(2^31–1)]  b1<<24|b2<<16|b3<<8|b4
        */

        public static CFFTokenKind GetTokenKind(uint8 b0)
        {
            CFFTokenKind result;

            if (b0 >= 32 && b0 <= 246)
                result = CFFTokenKind.Integer1;
            else if (b0 >= 247 && b0 <= 250)
                result = CFFTokenKind.Integer20;
            else if (b0 >= 251 && b0 <= 254)
                result = CFFTokenKind.Integer21;
            else if (b0 == 28)
                result = CFFTokenKind.Integer3;
            else if (b0 == 29)
                result = CFFTokenKind.Integer5;
            else if (b0 == 30)
                result = CFFTokenKind.Float;
            else if (b0 == 12)
                result = CFFTokenKind.Operator2;
            else
                result = CFFTokenKind.Operator1;

            return result;
        }

        // if Float, return 0
        public static Int32 GetTokenLength(CFFTokenKind tokenKind)
        {
            switch (tokenKind)
            {
                case CFFTokenKind.Integer1: return 1;
                case CFFTokenKind.Integer20:
                case CFFTokenKind.Integer21: return 2;
                case CFFTokenKind.Integer3: return 3;
                case CFFTokenKind.Integer5: return 5;
                case CFFTokenKind.Float: return 0; // special case
                case CFFTokenKind.Operator1: return 1;
                case CFFTokenKind.Operator2: return 2;
                default: return 0;
            }
        }

        static IEnumerable<(CFFTokenKind tokenKind, uint8[] token)> EnumToken(PrimitiveReader reader, UInt32 filePosition, UInt32 fileLength)
        {
            var uint8Enumerator = TokenHelper.GetEnumerable(reader, filePosition, fileLength).GetEnumerator();
            while (TokenHelper.ReadByte(uint8Enumerator) is uint8 b0)
            {
                uint8[] token;
                var tokenKind = GetTokenKind(b0);
                if (tokenKind == CFFTokenKind.Float)
                {
                    var bs = new List<uint8> { b0 };
                    for (; ; )
                    {
                        if (TokenHelper.ReadByte(uint8Enumerator) is uint8 b)
                        {
                            bs.Add(b);
                            if ((b & 0x0f) == 0x0f)
                            {
                                token = bs.ToArray();
                                break;
                            }
                        }
                        else
                        {
                            token = null;
                            break;
                        }
                    }
                }
                else
                {
                    var tokenLength = GetTokenLength(tokenKind);
                    token = TokenHelper.ReadToken(uint8Enumerator, b0, tokenLength);
                }

                if (token != null)
                    yield return (tokenKind, token);
                else
                {
                    yield return (CFFTokenKind.ReadError, null);
                    break;
                }
            }
            yield break;
        }


        public static uint8[] GetOperand(IList<DICTData> data, CFFDICTOperators op, Int32 operandIndex)
        {
            var dictData = data?.FirstOrDefault(i => GetCFFDICTOperator(i.Operator) == op);
            if (dictData != null)
            {
                var operands = dictData.Operands;
                if (operandIndex >= 0 && operandIndex < operands.Length)
                    return operands[operandIndex];
                else
                    throw new ArgumentException($"DICTHelper.GetOperand operandIndex out of range {operandIndex}");
            }
            else
                return null;
        }


        public static uint8[] GetOperand(IList<DICTData> data, CFF2DICTOperators op, Int32 operandIndex)
        {
            var dictData = data?.FirstOrDefault(i => GetCFF2DICTOperator(i.Operator) == op);
            if (dictData != null)
            {
                var operands = dictData.Operands;
                if (operandIndex >= 0 && operandIndex < operands.Length)
                    return operands[operandIndex];
                else
                    throw new ArgumentException($"DICTHelper.GetOperand operandIndex out of range {operandIndex}");
            }
            else
                return null;
        }


        public static Int32 GetOperandInteger(uint8[] token)
        {
            var b0 = token[0];
            if (b0 >= 32)
            {
                if (b0 <= 246)
                    return b0 - 139;
                else if (b0 <= 250)
                    return (b0 - 247) * 256 + token[1] + 108;
                else if (b0 <= 254)
                    return -(b0 - 251) * 256 - token[1] - 108;
            }
            else if (b0 == 28)
                return token[1] << 8 | token[2];
            else if (b0 == 29)
                return token[1] << 24 | token[2] << 16 | token[3] << 8 | token[4];

            return 0;
        }


        /*
        Table 5 Nibble Definitions
        Nibble  Represents
        0 – 9   0 – 9
        a.      (decimal point)
        b       E
        c       E– 
        d       <reserved>
        e –     (minus)
        f       end of number
        */
        public static string GetOperandFloat(uint8[] array)
        {
            if (array[0] == 30 && (array.Last() & 0x0f) == 0x0f)
            {
                var result = "";
                foreach (var i in array)
                {
                    var str = ((Byte)i).ToString("x2")
                    .Replace('a', '.')
                    .Replace('b', 'E')
                    .Replace("c", "E-")
                    .Replace('e', '-')
                    .Replace("f", "");
                    result += str;
                }
                return result;
            }
            return null;
        }


        public static Int32? GetOperatorInteger(IList<uint8> token)
        {
            if (token != null)
            {
                if (token.Count == 1)
                    return token[0];

                else if (token.Count == 2)
                    return token[0] << 8 | token[1];
            }
            return null;
        }


        public static CFFDICTOperators? GetCFFDICTOperator(IList<uint8> token)
        {
            CFFDICTOperators? result = null;

            if (DICTHelper.GetOperatorInteger(token) is Int32 i32)
            {
                var op = (CFFDICTOperators)i32;
                if (Enum.IsDefined(typeof(CFFDICTOperators), op))
                    result = op;
            }
            return result;
        }

        public static CFF2DICTOperators? GetCFF2DICTOperator(IList<uint8> token)
        {                                      
            CFF2DICTOperators? result = null;

            if (DICTHelper.GetOperatorInteger(token) is Int32 i32)
            {
                var op = (CFF2DICTOperators)i32;
                if (Enum.IsDefined(typeof(CFF2DICTOperators), op))
                    result = op;
            }
            return result;
        }
    }

#pragma warning restore IDE1006
}
