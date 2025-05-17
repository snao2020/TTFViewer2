using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TTFViewer.DataTypes
{
    static class AttributeHelper
    {
        public static T[] GetAttributes<T>(MemberInfo mi) where T : Attribute
        {
            T[] result;

            if (mi != null)
            {
                result = Attribute.GetCustomAttributes(mi, typeof(T)) as T[];
            }
            else
                result = new T[0];

            return result;
        }


        public static T[] GetAttributes<T>(FieldInfo fi, Int32 axis) where T : Attribute, IAxisAttribute
        {
            T[] result;

            if (fi != null)
            {
                result = (Attribute.GetCustomAttributes(fi, typeof(T)) as T[])
                    .Where(i => i.Axis == axis)
                    .ToArray();
            }
            else
                result = new T[0];

            return result;
        }


        public static T GetAttribute<T>(MemberInfo mi) where T : Attribute
        {
            T result = null;

            if (mi != null)
            {
                result = Attribute.GetCustomAttribute(mi, typeof(T)) as T;
            }

            return result;
        }

        
        public static T GetAttribute<T>(FieldInfo fi, Int32 axis) where T : Attribute, IAxisAttribute
        {
            T result = null;

            if (fi != null)
            {
                result = (Attribute.GetCustomAttributes(fi, typeof(T)) as T[])
                    .Where(i=>i.Axis == axis)
                    .FirstOrDefault();
            }

            return result;
        }


        public static MethodInfo GetMethodInfo(Type declaringType, string methodString, Type[] types)
        {
            string[] strs = methodString.Split('.'); // "className.methodName"
            string methodName = null;
            if (strs.Length == 2)
            {
                var s0 = strs[0].Trim();
                declaringType = Type.GetType($"{declaringType.Namespace}.{s0}");
                methodName = strs[1].Trim();
            }
            else if (strs.Length == 1)
                methodName = methodString.Trim();

            if (declaringType != null && methodName != null)
            {
                if(types != null)
                    return declaringType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, types, null);
                else
                    return declaringType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            }
            return null;
        }


        public static Delegate GetMethod(Type declaringType, string methodString, Type delegateType)
        {
            Delegate result = null;

            List<String> strs = methodString.Split('.').ToList(); // "nameSpace.className.methodName"
            if (strs.Count > 3)
                throw new ArgumentException($"AttributeHelper.GetMethod methodString={methodString}");

            String nameSpace;
            if (strs.Count == 3)
            {
                nameSpace = strs[0].Trim();
                strs.RemoveAt(0);
            }
            else
                nameSpace = declaringType.Namespace;

            if (strs.Count == 2)
            {
                var s0 = strs[0].Trim();
                declaringType = Type.GetType($"{nameSpace}.{s0}");
                strs.RemoveAt(0);
            }

            string methodName = null;
            if (strs.Count == 1)
                methodName = strs[0].Trim();

            if (declaringType != null && methodName != null)
            {
                methodName = methodName.Trim();
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy;
                var mi = declaringType.GetMethod(methodName, flags);
                var method = Delegate.CreateDelegate(delegateType, mi);
                result = method;
            }
            return result;
        }


        public static object ProcessMath(object value, string option)
        {
            if (value != null && option != null)
            {
                if (value.ToNumberOrNull() is UInt32 num)
                {
                    foreach (var str in option.Split(','))
                    {
                        var strs = str.Split(':');
                        if (strs.Length == 2)
                        {
                            num = Math(num, strs[0].Trim(), strs[1].Trim());
                        }
                        else
                            throw new ArgumentException($"AttributeeHelper20.GetSingleValue Unknown option \"{option}\"");
                    }
                    value = num;
                }
            }
            return value;
        }


        static UInt32 Math(UInt32 value, string op, string arg)
        {
            var num = GetUnsigned(arg);
            switch (op)
            {
                case "Mask": return value & num;
                case "Add": return value + num;
                case "Sub": return value - num;
                case "Mul": return value * num;
                case "Div": return value / num;
                case "AddIfNonZero": return value != 0 ? value + num : 0;
                case "SubIfNonZero": return value != 0 ? value - num : 0;
                default:
                    throw new ArgumentException($"AttributeeHelper20.Math Unknown operator \"{op}\"");
            }
        }


        public static UInt32 GetUnsigned(string s)
        {
            try
            {
                s = s.ToLower();
                s = s.Replace("_", "");
                if (s.StartsWith("0x"))
                    return Convert.ToUInt32(s.Substring(2), 16);
                else if (s.StartsWith("0b"))
                    return Convert.ToUInt32(s.Substring(2), 2);
                else
                    return Convert.ToUInt32(s);
            }
            catch
            {
                throw new ArgumentException($"AttributeHelper.GetUnsigned \"{s}\"");
            }
        }


        public static object GetEnum(string valueParam)
        {
            Type enumType = null;
            var strs = valueParam.Split('|');
            string enumString = null;
            foreach (var s in strs)
            {
                var str = s.Trim();
                var typeValue = TypeHelper.GetTypeValue(str);
                if (typeValue.Item1 != null && typeValue.Item1.IsEnum)
                {
                    if (enumType == null)
                    {
                        enumType = typeValue.Item1;
                        enumString = typeValue.Item2;
                    }
                    else if (typeValue.Item1 == enumType)
                    {
                        enumString += $",{typeValue.Item2}";
                    }
                    else
                        throw new ArgumentException($"GetConstValue FieldValueKind.Enum ${valueParam}");
                }
                else
                    throw new ArgumentException($"GetConstValue FieldValueKind.Enum ${valueParam}");
            }
            if (enumString != null)
                return Enum.Parse(enumType, enumString);
            else
                throw new ArgumentException($"GetConstValue FieldValueKind.Enum ${valueParam}");
        }


        public static Tag GetTag(string valueParameter)
        {
            return (Tag)valueParameter;
        }


        public static Type GetType(string typeName)
        {
            var result = Type.GetType(typeName);
            if (result == null)
                throw new ArgumentException($"AttributeHelper20.GetType ${typeName}");
            return result;
        }
    }
}
