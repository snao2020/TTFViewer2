using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TTFViewer.DataTypes
{
#pragma warning disable IDE1006

    static class TypeHelper
    {
        public static List<Type> GetParameterTypes(MethodInfo mi)
        {
            if (mi != null)
            {
                var infos = mi.GetParameters();
                var list = new List<Type>();
                foreach (var info in infos)
                {
                    list.Add(info.ParameterType);
                }
                return list;
            }
            return null;
        }

        public static bool IsNullable(Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static (Type, string) GetTypeValue(string str)
        {
            if (str != null)
            {
                var pos = str.LastIndexOf('.');
                if (pos > 0)
                {
                    var typeName = str.Substring(0, pos);
                    var valueName = str.Substring(pos + 1);
                    var type = Type.GetType(typeName);
                    return (type, valueName);
                }
            }
            return (null, null);
        }


        public static (Type, Int32) GetElementTypeRank(Type type)
        {
            Int32 rank = 0;
            while(type != null &&  IsArrayOrIListT(type))
            {
                rank++;
                type = GetElementType(type);
            }
            return (type, rank);
        }

        public static Type GetElementType(Type type)
        {
            Type result = null;

            Type iListT = type.GetInterface("System.Collections.Generic.IList`1");
            if (iListT != null)
            {
                result = iListT.GenericTypeArguments.FirstOrDefault();
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>))
            {
                result = type.GenericTypeArguments.FirstOrDefault();
            }
            return result;
        }


        public static bool IsArrayOrIListT(Type type)
        {
            bool result = false;
            if (type != null)
            {
                if (type == typeof(Array))
                    result = true;
                else if (type.GetInterface("System.Collections.Generic.IList`1") != null)
                    result = true;
                else
                    result = type.IsGenericType
                            && type.GetGenericTypeDefinition() == typeof(IList<>);
            }
            return result;
        }

        public static UInt32 OffsetOf(Type t, string fieldName)
        {
            UInt32 result = 0;
            foreach (var fi in B2DFieldInfos(t))
            {
                if (fi.Name == fieldName)
                {
                    return result;
                }
                else if (IsArrayOrIListT(fi.FieldType))
                {
                    break;
                }
                else
                {
                    result += SizeOf(fi.FieldType);
                }
            }
            throw new ArgumentException($"ArgumentException TypeHelper.OffsetOf  Type={t.Name} fieldName={fieldName}");
        }


        public static UInt32 SizeOf(Type t)
        {
            UInt32 result = 0;
            if(t == null)
                throw new ArgumentException("ArgumentException TypeHelper.SizeOf  Type=<null>");
            if (typeof(ITTFPrimitive).IsAssignableFrom(t))
                result += (UInt32)Marshal.SizeOf(t);
            else if(IsArrayOrIListT(t))
                throw new ArgumentException($"ArgumentException TypeHelper.SizeOf  Type={t.Name}");
            else if(t.IsClass)
            {
                foreach (var fi in B2DFieldInfos(t))
                {
                    result += SizeOf(fi.FieldType);
                }
            }
            return result;
        }


        public static bool IsUniform(Type t)
        {
            bool result = true;

            if (t == null)
                throw new ArgumentException("ArgumentException TypeHelper.IsUniform  Type=<null>");
            else if (t == typeof(ITTFPrimitive))
                return false;
            else if (IsArrayOrIListT(t))
                result = false;
            else if (t.IsClass)
            {
                foreach (var fi in B2DFieldInfos(t))
                {
                    if (!IsUniform(fi.FieldType))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }


        public static List<FieldInfo> B2DFieldInfos(Type t)
        {
            List<FieldInfo> result = null;
            if (t != null)
            {
                //result = B2DFieldInfos(t.BaseType);
                //FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).OrderBy(field=>field.MetadataToken).ToArray();
                FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Instance).OrderBy(field => field.MetadataToken).ToArray();
                //FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).OrderBy(field => Marshal.OffsetOf(t, field.Name).ToInt32()).ToArray();
                // https://stackoverflow.com/questions/8067493/if-getfields-doesnt-guarantee-order-how-does-layoutkind-sequential-work
                // unlikely's answer
                //FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Instance).OrderBy(field => Marshal.OffsetOf(t, field.Name).ToInt32()).ToArray();

                if (result != null)
                    result.AddRange(fis);
                else
                    result = fis.ToList();
            }
            return result;
        }

        public static UInt32 CalcSize(object obj)
        {
            if (obj == null)
                return 0;
            if (obj is IElementList ie1)
                return ie1.GetFileLength();
            if (obj is ITTFPrimitive) // il)
            {
                return (UInt32)Marshal.SizeOf(obj);
            }
            else if (obj is Array array)
            {
                UInt32 size = 0;
                for (int i = 0; i < array.Length; i++)
                    size += CalcSize(array.GetValue(i));
                return size;
            }
            else if (obj is IList list)
            {
                return (UInt32)list.Count * CalcSize(list[0]);
            }
            else
            {
                UInt32 size = 0;
                Type type = obj.GetType();
                FieldInfo[] fis = type.GetFields(
                    BindingFlags.Public | BindingFlags.Instance);
                foreach (var fi in fis)
                {
                    object value = fi.GetValue(obj);
                    if (value != null)
                        size += CalcSize(value);
                }
                return size;
            }
        }


    }
#pragma warning restore IDE1006
}
