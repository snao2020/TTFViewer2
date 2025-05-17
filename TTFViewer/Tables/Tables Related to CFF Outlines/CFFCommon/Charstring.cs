//ver1.9.1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.Model;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName(TypeNameKind.Method, "TypeName")]
    class SubrCharstring
    {
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Values(0, "TokensMethod")]
        [Values(1, ValuesKind.ParentValue, null)]
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [FieldName(0, null)]
        [Invalid(1, "IsInvalid")]
        [ValueFormat(1, "TokenText")]
        [ValueFormat(2, ValueFormatKind.Hex)]
        public IList<uint8[]> Tokens;

        static String TypeName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "CharString" : "charstring";

        static (UInt32, IEnumerable<uint8[]>)? TokensMethod(IAttributeService service)
        {
            if (service.GetValues(FieldValueKind.ParentConstraint, null).SingleValue(0) is UInt32 fileLength)
            {
                return (fileLength, CharstringHelper.EnumSubrToken(service.TableModel, service.FilePosition, fileLength));
            }

            return null;
        }

        static bool IsInvalid(IItemValueService ivs)
            => ivs.Value is uint8[] token && token.Length > 0
                && CharstringHelper.GetTokenKind(token[0]) == CFFTokenKind.Operator
                && CharstringHelper.GetOperatorInteger(token) is Int32 op
                && CFFHelper.IsCFF(ItemValueHelper.GetFontTableType(ivs)) is bool isCFF
                && CharstringHelper.IsMaskOperator(isCFF, op);

        static string TokenText(IItemValueService ivp)
        {
            Func<Type> getFontTableType = ()=>ItemValueHelper.GetFontTableType(ivp); 
            return CharstringHelper.GetTokenText(ivp.Value, getFontTableType);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName(TypeNameKind.Method, "TypeName")]
    class Charstring
    {
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Values(0, "TokensMethod")]
        [Values(1, ValuesKind.ParentValue, null)]
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [FieldName(0, null)]
        [Invalid(0, "IsInvalid")]
        [ValueFormat(1, "TokenText")]
        [ValueFormat(2, ValueFormatKind.Hex)]
        public IList<uint8[]> Tokens;

        static String TypeName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
                ? "CharString" : "charstring";

        static (UInt32, IEnumerable<uint8[]>)? TokensMethod(IAttributeService service)
        {
            if (service.GetValues(FieldValueKind.ElementList, null).SingleValue(0) is CharstringElementList el
                && el.SubrSelector != null
                && service.GetValues(FieldValueKind.ParentConstraint, null).SingleValue(0) is UInt32 fileLength)
            { 
                Int32? fontIndex
                    = service.GetValues(FieldValueKind.FontTableType, null).SingleValue(0) is Type fontTableType && fontTableType == typeof(CFFTable)
                    ? TablePathHelper.GetFirstIndex(service.Path)
                    : (Int32?)null;

                Int32 gid = TablePathHelper.GetLastIndex(service.Path);
                var filePosition = service.FilePosition;
                return (fileLength, CharstringHelper.EnumToken(service.TableModel, filePosition, fileLength, fontIndex, gid, el.SubrSelector));
            }
            return null;
        }

        static bool IsInvalid(IItemValueService ivp)
            => !(ivp.Value is IList<uint8[]> list && list.Count != 0);

        static string TokenText(IItemValueService ivp)
        {
            Func<Type> getFontTableTypeFunc = () => ItemValueHelper.GetFontTableType(ivp);
            return CharstringHelper.GetTokenText(ivp.Value, getFontTableTypeFunc);
        }
    }


    enum CFFCharstringOperators
    {
        // 0 00 Reserved
        hstem       = 0x01,
        // 2 02 Reserved
        vstem       = 0x03,
        vmoveto     = 0x04,
        rlineto     = 0x05,
        hlineto     = 0x06,
        vlineto     = 0x07,
        rrcurveto   = 0x08,
        // 9 09 Reserved
        callsubr    = 0x0a,

        [FieldName(0, "return")]
        return0     = 0x0b, // CFF2:reserved
        escape      = 0x0c,
        // 13 0d Reserved
        endchar     = 0x0e, // CFF2:reserved
        // 15 0f Reserved,  // CFF2:vsindex
        // 16 10 Reserved,  // CFF2:blend
        // 17 11 Reserved
        hstemhm     = 0x12,
        hintmask    = 0x13,
        cntrmask    = 0x14,
        rmoveto     = 0x15,
        hmoveto     = 0x16,
        vstemhm     = 0x17,
        rcurveline  = 0x18,
        rlinecurve  = 0x19,
        vvcurveto   = 0x1a,
        hhcurveto   = 0x1b,
        // 28 1c shortint
        callgsubr   = 0x1d,
        vhcurveto   = 0x1e,
        hvcurveto   = 0x1f,
        // 32-246 20-f6 <numbers>
        // 247-254 f7-fe <numbers>
        // 255 ff <number>

        // 12 0 0c 00 Reserved
        // 12 1 0c 01 Reserved
        // 12 2 0c 02 Reserved
        and         = 0x0c03,   // CFF2:Reserved
        or          = 0x0c04,   // CFF2:Reserved
        not         = 0x0c05,   // CFF2:Reserved
        // 12 6 0c 06 Reserved
        // 12 7 0c 07 Reserved
        // 12 8 0c 08 Reserved
        abs         = 0x0c09,   // CFF2:Reserved
        add         = 0x0c0a,   // CFF2:Reserved
        sub         = 0x0c0b,   // CFF2:Reserved
        div         = 0x0c0c,   // CFF2:Reserved
        // 12 13 0c 0d Reserved 
        neg         = 0x0c0e,   // CFF2:Reserved
        eq          = 0x0c0f,   // CFF2:Reserved
        // 12 16 0c 10 Reserved
        // 12 17 0c 11 Reserved
        drop        = 0x0c12,   // CFF2:Reserved
        //12 19 0c 13 Reserved
        put         = 0x0c14,   // CFF2:Reserved
        get         = 0x0c15,   // CFF2:Reserved
        ifelse      = 0x0c16,   // CFF2:Reserved
        random      = 0x0c17,   // CFF2:Reserved
        mul         = 0x0c18,   // CFF2:Reserved
        // 12 25 0c 19 Reserved
        sqrt        = 0x0c1a,   // CFF2:Reserved
        dup         = 0x0c1b,   // CFF2:Reserved
        exch        = 0x0c1c,   // CFF2:Reserved
        index       = 0x0c1d,   // CFF2:Reserved
        roll        = 0x0c1e,   // CFF2:Reserved
        // 12 31 0c 1f Reserved
        // 12 32 0c 20 Reserved
        // 12 33 0c 21 Reserved
        hflex       = 0x0c22,
        flex        = 0x0c23,
        hflex1      = 0x0c24,
        flex1       = 0x0c25,
        // 12 38-12 255 0c 26-0c ff Reserved
    }

    enum CFF2CharStringOperators
    {
        // 0 00 Reserved
        hstem = 0x01,
        // 2 02 Reserved
        vstem = 0x03,
        vmoveto = 0x04,
        rlineto = 0x05,
        hlineto = 0x06,
        vlineto = 0x07,
        rrcurveto = 0x08,
        // 9 09 Reserved
        callsubr = 0x0a,
        //[FieldName("return")]
        //return0 = 0x0b, // CFF2:reserved
        escape = 0x0c,
        // 13 0d Reserved
        // endchar = 0x0e, // CFF2:reserved
        vsindex = 0x0f,     // CFF Reserved
        blend = 0x10,       // CFF Reserved,
        // 17 11 Reserved
        hstemhm = 0x12,
        hintmask = 0x13,
        cntrmask = 0x14,
        rmoveto = 0x15,
        hmoveto = 0x16,
        vstemhm = 0x17,
        rcurveline = 0x18,
        rlinecurve = 0x19,
        vvcurveto = 0x1a,
        hhcurveto = 0x1b,
        // 28 1c shortint
        callgsubr = 0x1d,
        vhcurveto = 0x1e,
        hvcurveto = 0x1f,
        // 32-246 20-f6 <numbers>
        // 247-254 f7-fe <numbers>
        // 255 ff <number>

        // 12 0  0c 00 Reserved
        // 12 1  0c 01 Reserved
        // 12 2  0c 02 Reserved
        // 12 3  0c 03 Reserved CFF and
        // 12 4  0c 04 Reserved CFF or
        // 12 5  0c 05 Reserved CFF not
        // 12 6  0c 06 Reserved
        // 12 7  0c 07 Reserved
        // 12 8  0c 08 Reserved
        // 12 9  0c 09 Reserved CFF abs
        // 12 10 0c 0a Reserved CFF add
        // 12 11 0c 0b Reserved CFF sub
        // 12 12 0c 0c Reserved CFF div
        // 12 13 0c 0d Reserved 
        // 12 14 0c 0e Reserved CFF neg
        // 12 15 0c 0f Reserved CFF eq
        // 12 16 0c 10 Reserved
        // 12 17 0c 11 Reserved
        // 12 18 0c 12 Reserved CFF drop
        // 12 19 0c 13 Reserved
        // 12 20 0c 14 Reserved CFF put
        // 12 21 0c 15 Reserved CFF get
        // 12 22 0c 16 Reserved CFF ifelse
        // 12 23 0c 17 Reserved CFF random
        // 12 24 0c 18 Reserved CFF mul
        // 12 25 0c 19 Reserved
        // 12 26 0c 1a Reserved CFF sqrt
        // 12 27 0c 1b Reserved CFF dup
        // 12 28 0c 1c Reserved CFF exch
        // 12 29 0c 1d Reserved CFF index
        // 12 30 0c 1e Reserved CFF roll
        // 12 31 0c 1f Reserved
        // 12 32 0c 20 Reserved
        // 12 33 0c 21 Reserved
        hflex = 0x0c22,
        flex = 0x0c23,
        hflex1 = 0x0c24,
        flex1 = 0x0c25,
        // 12 38-12 255 0c 26-0c ff Reserved
    }


//------------------------------------------------------------------------


    static class CharstringHelper
    {
        public static IEnumerable<uint8[]> EnumSubrToken(TableModel tableModel, UInt32 filePosition, UInt32 fileLength)
        {
            //IList<uint8[]> result = null;

            if (CFFHelper.IsCFF(tableModel.ValueType) is bool isCFF)
            {
                var primitiveReader = tableModel.BinaryLoader.GetPrimitiveReader();
                var uint8Enumerator = TokenHelper.GetEnumerable(primitiveReader, filePosition, fileLength).GetEnumerator();
                foreach (var i in CharstringReader.EnumToken(uint8Enumerator, null, isCFF))
                    yield return i.token;
            }
            yield break;
        }


        public static IEnumerable<uint8[]> EnumToken(TableModel tableModel, UInt32 filePosition, UInt32 fileLength, Int32? fontIndex, Int32 gid, SubrSelector subrSelector)
        {
            var primitiveReader = tableModel.BinaryLoader.GetPrimitiveReader();
            var reader = new CharstringReader(primitiveReader, fontIndex, gid, subrSelector);

            var uint8Enumerator = TokenHelper.GetEnumerable(primitiveReader, filePosition, fileLength).GetEnumerator();
            var stack = new CharstringStack();
            Func<CharstringTokenResult, bool> abortFunc
                = tokenResult =>
                {
                    switch (tokenResult)
                    {
                        case CharstringTokenResult.MoveTo:
                        case CharstringTokenResult.Mask:
                            return true;
                    }
                    return false;
                };

            List<uint8[]> log = new List<uint8[]>();

            reader.Interpret(uint8Enumerator, stack, abortFunc, log);
            if (log.Count > 0)
            {
                foreach (var i in log)
                    yield return i;
                foreach (var i in CharstringReader.EnumToken(uint8Enumerator, () => stack.GetMaskByteLength(), subrSelector.IsCFF))
                    yield return i.token;
            }
            yield break;
        }


        /*
        CharString Interpretation                  Number Range     Bytes
        Byte Value Represented      Required
        28         next 2 bytes are an int16       -32768 to +32767 3
        32 to 246   result = v - 139 	            -107 to +107     1
        247 to 250  with next byte, w,              +108 to +1131    2
                    result = (v - 247) * 256 + w + 108 	
        251 to 254  with next byte, w,              -108 to -1131    2
                    result = -[(v - 251) * 256] - w - 108 	
        255         next 4 bytes are a Fixed value  16-bit signed integer with 16 bits of fraction
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
            else if (b0 == 255)
                result = CFFTokenKind.Fixed;
            else if (b0 == 12)
                result = CFFTokenKind.Operator2;
            else
                result = CFFTokenKind.Operator1;

            return result;
        }

        public static Int32 GetTokenLength(CFFTokenKind tokenKind)
        {
            switch(tokenKind)
            {
                case CFFTokenKind.Integer1: return 1;
                case CFFTokenKind.Integer20:
                case CFFTokenKind.Integer21: return 2;
                case CFFTokenKind.Integer3: return 3;
                case CFFTokenKind.Fixed: return 5;
                case CFFTokenKind.Operator1: return 1;
                case CFFTokenKind.Operator2: return 2;
                default: return 0;
            }
        }

        public static bool IsMaskOperator(bool isCFF, Int32 op)
        {
            if (isCFF)
                return op == (Int32)CFFCharstringOperators.hintmask
                    || op == (Int32)CFFCharstringOperators.cntrmask;
            else
                return op == (Int32)CFF2CharStringOperators.hintmask
                    || op == (Int32)CFF2CharStringOperators.cntrmask;
        }


        public static FixedPoint_16_16 GetNumber(CFFTokenKind tokenKind, uint8[] token)
        {
            FixedPoint_16_16 result = null;
            if (GetTokenLength(tokenKind) == token.Length)
            {
                switch (tokenKind)
                {
                    case CFFTokenKind.Integer1:
                        //result = v - 139 - 107 to + 107 
                        result = new FixedPoint_16_16((Int16)(token[0] - 139), 0);
                        break;
                    case CFFTokenKind.Integer20:
                        //result = (v - 247) * 256 + w + 108
                        if (token.Length > 1)
                            result = new FixedPoint_16_16((Int16)((token[0] - 247) * 256 + token[1] + 108), 0);
                        break;
                    case CFFTokenKind.Integer21:
                        //result = -[(v - 251) * 256] - w - 108
                        if (token.Length > 1)
                            result = new FixedPoint_16_16((Int16)(-(token[0] - 251) * 256 - token[1] - 108), 0);
                        break;
                    case CFFTokenKind.Integer3:
                        //next 2 bytes are an int16
                        if (token.Length > 2)
                            result = new FixedPoint_16_16((Int16)(token[1] << 8 | token[2]), 0);
                        break;
                    case CFFTokenKind.Fixed:
                        //next 4 bytes are a Fixed value  16-bit signed integer with 16 bits of fraction
                        if (token.Length > 4)
                            result = new FixedPoint_16_16(
                                (Int16)(token[1] << 8 | token[2]),
                                (UInt16)(token[3] << 8 | token[4]));
                        break;
                }
            }
            return result;
        }


        public static Int32? GetOperatorInteger(uint8[] token)
        {
            if (token == null)
                return null;
            else if (token.Length > 1 && token[0] == 12)
                return token[0] << 8 | token[1];
            else if (token.Length > 0)
                return token[0];
            else
                return null;
        }


        public static Int32 GetSubrIndex(Int32 nSubrs, Int32 subrNumber)
        {
            Int32 bias;
            if (nSubrs < 1240)
                bias = 107;
            else if (nSubrs < 33900)
                bias = 1131;
            else
                bias = 32768;
            return bias + subrNumber;
        }


        public static uint8[] GetMaskBytes(IList<uint8> token)
        {
            uint8[] result = null;
            if (token != null && token.Count > .0)
            {
                if (token[0] == 12)
                {
                    if (token.Count > 2)
                        result = token.Skip(2).ToArray();
                }
                else if (token.Count > 1)
                    result = token.Skip(1).ToArray();
            }
            return result;
        }


        public static String GetTokenText(object value, Func<Type> getFontTableType)
        {
            if (value is uint8[] token && token.Length > 0)
            {
                var tokenKind = CharstringHelper.GetTokenKind(token[0]);
                if (tokenKind.HasFlag(CFFTokenKind.Operand))
                {
                    var num = CharstringHelper.GetNumber(tokenKind, token);
                    return CharstringHelper.GetNumberText(tokenKind, num);
                }
                else if (tokenKind.HasFlag(CFFTokenKind.Operator)
                    && CFFHelper.IsCFF(getFontTableType()) is bool isCFF)
                {
                    return CharstringHelper.GetOperatorText(isCFF, token);
                }
            }
            return null;
        }


        static String GetNumberText(CFFTokenKind tokenKind, FixedPoint_16_16 number)
        {
            switch (tokenKind)
            {
                case CFFTokenKind.Integer1:
                    var intPart = number.GetIntPart();
                    return $"{intPart} (0x{(Byte)intPart:x2})";
                case CFFTokenKind.Integer20:
                case CFFTokenKind.Integer21:
                case CFFTokenKind.Integer3:
                    var intPart2 = number.GetIntPart();
                    return $"{intPart2} (0x{(UInt16)intPart2:x4})";
                case CFFTokenKind.Fixed:
                    return number.ToString();
            }
            return null;
        }


        static String GetOperatorText(bool isCFF, uint8[] token)
        {
            String result = null;
            if (GetOperatorInteger(token) is Int32 op)
            {
                result = isCFF 
                    ? ItemValueHelper.GetEnumItemName(typeof(CFFCharstringOperators), op)
                    : ItemValueHelper.GetEnumItemName(typeof(CFF2CharStringOperators), op);

                if (IsMaskOperator(isCFF, op))
                {
                    var mask = GetMaskBytes(token);
                    if (mask != null)
                    {
                        result += String.Concat(mask.Select(i => $" {(Byte)i:x2}"));
                    }
                }
            }
            return result;
        }
    }

#pragma warning restore IDE1006
}
