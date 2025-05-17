using System;
using System.Collections.Generic;

namespace TTFViewer.DataTypes
{
    static class ValueHelper
    {
        public static object SingleValue(this object[] values, Int32 index)
        {
            if (values != null && values.Length > index)
                return values[index];
            return null;

        }
       

        public static bool IsSelect2(AttributeConditionKind selectKind, object[] lhs, object[] rhs)
        {
            if (lhs.Length != rhs.Length)
                return false;

            for (int i = 0; i < lhs.Length; i++)
            {
                var lv = lhs[i];
                var rv = rhs[i];
                if (lv != null && lv.GetType() == typeof(object))
                    if (rv != null)
                        continue;
                    else
                        return false;
                else if (rv != null && rv.GetType() == typeof(object))
                    if (lv != null)
                        continue;
                    else
                        return false;
                else if (!IsSelect(selectKind, lv, rv))
                    return false;
            }
            return true;
        }


        public static bool IsSelect(AttributeConditionKind selectKind, object lhs, object rhs)
        {
            switch (selectKind)
            {
                case AttributeConditionKind.Equal:
                    return IsEqual(lhs, rhs);
               
                case AttributeConditionKind.NotEqual:
                    return !IsEqual(lhs, rhs);
                
                case AttributeConditionKind.HasFlag:
                    return HasFlag(lhs, rhs);
            }
            return false; ;
        }


        static bool IsEqual(object lhs, object rhs)
        {
            if (GetIsNulls(lhs, rhs) is ValueTuple<bool, bool> result0)
                return result0.Item1 == result0.Item2;

            else if (GetTypes(lhs, rhs) is ValueTuple<Type, Type> result1)
                return result1.Item1 == result1.Item2;

            else if (GetTags(lhs, rhs) is ValueTuple<Tag, Tag> result2)
                return result2.Item1 == result2.Item2;

            else if (GetEnums2(lhs, rhs) is ValueTuple<UInt32?, UInt32?> enumValues)
            {
                if (enumValues.Item1 is UInt32 u0
                    && enumValues.Item2 is UInt32 u1)
                {
                    return u0 == u1;
                }
                else
                    return false;
            }
            else if (GetNumbers(lhs, rhs) is ValueTuple<UInt32, UInt32> numbers)
                return numbers.Item1 == numbers.Item2;
            else
                return false;
        }


        static bool HasFlag(object lhs, object rhs)
        {
            // compare null
            if (GetIsNulls(lhs, rhs) != null)
                return false;

            else if (GetTypes(lhs, rhs) != null)
                return false;

            else if (GetTags(lhs, rhs) != null)
                return false;

            else if (GetEnums2(lhs, rhs) is ValueTuple<UInt32?, UInt32?> enumValues)
            {
                if (enumValues.Item1 is UInt32 u0
                    && enumValues.Item2 is UInt32 u1)
                {
                    return DoHasFlag(u0, u1);
                }
                else
                    return false;
            }
            else if (GetNumbers(lhs, rhs) is ValueTuple<UInt32, UInt32> numbers)
            {
                return DoHasFlag(numbers.Item1, numbers.Item2);
            }
            else
                return false;
        }

        static bool DoHasFlag(UInt32 u0, UInt32 u1)
        {
            return (u0 & u1) != 0;
        }

        static (bool, bool)? GetIsNulls(object lhs, object rhs)
        {
            if (lhs == null)
                return (true, rhs == null);
            else if (rhs == null)
                return (lhs == null, true); ;
            return null;
        }


        static (Type, Type)? GetTypes(object lhs, object rhs)
        {
            if (lhs is Type t0)
            {
                if (rhs is Type t1)
                    return (t0, t1);
                else
                    return (t0, rhs.GetType());
            }
            else if (rhs is Type t2)
                return (lhs.GetType(), t2);

            return null;
        }


        static (Tag, Tag)? GetTags(object lhs, object rhs)
        {
            if (lhs is Tag t0 && rhs is Tag t1)
                return (t0, t1);
            return null;
        }


        static (UInt32?, UInt32?)? GetEnums2(object lhs, object rhs)
        {
            if (lhs is Enum e0 && rhs is Enum e1)
            {
                if (e0.GetType() == e1.GetType())
                    return ((UInt32)(Int32)lhs, (UInt32)(Int32)rhs);
                else
                    return (null, null);
            }
            return null;
        }


        static (UInt32, UInt32)? GetNumbers(object lhs, object rhs)
        {
            if (lhs is Enum && rhs is Tag)
                return null;
            else if (lhs is Tag && rhs is Enum)
                return null;
            else
            {
                if (lhs.ToNumberOrNull() is UInt32 u0
                    && rhs.ToNumberOrNull() is UInt32 u1)
                {
                    return (u0, u1);
                }
            }
            return null;
        }


        public static UInt32? ToNumberOrNull(this object obj)
        {
            UInt32? result = null;

            switch (obj)
            {
                case uint32 u32: result = u32; break;
                case int32 i32: result = (UInt32)(Int32)i32; break;
                case uint24 u24: result = u24; break;
                case uint16 u16: result = u16; break;
                case int16 i16: result = (UInt32)(Int16)i16; break;
                case uint8 u8: result = u8; break;
                case int8 i8: result = (UInt32)(SByte)i8; break;
                case Offset32 o32: result = (UInt32)o32; break;
                case Offset24 o24: result = (UInt32)o24; break;
                case Offset16 o16: result = (UInt32)o16; break;
                case Version16Dot16 version: result = (UInt32)version; break;
                case UInt32 u32: result = u32; break;
                case Int32 i32: result = (UInt32)(Int32)i32; break;
                case UInt16 u16: result = u16; break;
                case Int16 i16: result = (UInt32)(Int16)i16; break;
                case Byte u8: result = u8; break;
                case SByte i8: result = (UInt32)(SByte)i8; break;
                case Tag tag: result = tag; break;
                case Enum e: result = (UInt32)(Int32)obj; break;
            }

            return result;
        }

        public static UInt32 ToNumber4(this object obj)
        {
            UInt32 result = obj.ToNumberOrNull() ?? 0;
            return result;
        }


        public static UInt32? ToOffsetOrNull(this object obj)
        {
            UInt32? result = null;

            switch (obj)
            {
                case Offset32 o32: result = (UInt32)o32; break;
                case Offset24 o24: result = (UInt32)o24; break;
                case Offset16 o16: result = (UInt32)o16; break;
            }

            return result;
        }

        public static Byte[] uint8ListToByteArray(IList<uint8> list)
        {
            var dst = new List<Byte>();
            foreach (var i in list)
                dst.Add(i);
            return dst.ToArray();
        }

        public static Byte[] int8ListToByteArray(IList<int8> list)
        {
            var dst = new List<Byte>();
            foreach (var i in list)
                dst.Add((Byte)(SByte)i);
            return dst.ToArray();
        }
    }
}
