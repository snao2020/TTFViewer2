using System;
using System.Reflection;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.Loaders
{
    static class AttributeHelper2
    {
        public static T GetValueT2<T>(IAttributeReader reader, Type declaringType, Attribute attr)
        {
            T result = default(T);

            if (reader != null && attr is IValueAttribute valueAttribute && valueAttribute.Value != null)
            {
                object[] values = reader.GetValues(valueAttribute.Value);
                result = ConvertT<T>(values);
            }
            else if (reader != null && attr is IMethodAttribute methodAttribute && methodAttribute.MethodName != null)
            {
                if (declaringType != null)
                {
                    result = InvokeMethodT2<T>(reader, declaringType, methodAttribute.MethodName);
                }
            }
            return result;
        }


        static T InvokeMethodT2<T>(IAttributeReader reader, Type declaringType, string methodName)
        {
            T result = default(T);

            methodName = methodName.Trim();
            MethodInfo mi = AttributeHelper.GetMethodInfo(declaringType, methodName, null);
            if (mi != null)
            {
                var types = TypeHelper.GetParameterTypes(mi);
                if (types.Count == 1
                    && typeof(IAttributeService).IsAssignableFrom(types[0])
                    && mi.ReturnType == typeof(T))
                {
                    object[] parameters = new object[] { reader.GetAttributeService() };
                    if (mi.Invoke(null, parameters) is T t)
                        result = t;
                }
                else
                    throw new ArgumentException("AttributeHelper.InvokeMethod3");
            }

            return result;
        }

        static T ConvertT<T>(object[] values)
        {
            T result = default(T);
            if ((object)values is T t0)
            {
                result = t0;
            }
            else
            {
                object o = values.SingleValue(0);
                if (o is T t)
                    result = t;
                else
                {
                    var num = o.ToNumberOrNull();
                    if ((object)num is T t3)
                        result = t3;
                    else if (num is UInt32 u32)
                    {
                        if ((object)u32 is T t4)
                            result = t4;
                        else if ((object)(Int32)u32 is T t5)
                            result = t5;
                    }
                }
            }
            return result;
        }


        public static C SelectAttribute4<S,C>(IAttributeReader reader, S selectAttribute, MemberInfo mi)
            where S : Attribute, IValueAttribute//, IMethodAttribute
            where C : Attribute, IValueAttribute, IConditionAttribute
        {
            C result = null;

            var t2 = mi is FieldInfo fi ? fi.DeclaringType : mi is Type t ? t : null;
            object[] values0 = GetValueT2<object[]>(reader, t2, selectAttribute);
            if (values0 != null)
            {
                var condAttrs = AttributeHelper.GetAttributes<C>(mi);
                foreach (var attr in condAttrs)
                {
                    object[] values1 = reader.GetValues(attr.Value);
                    if (ValueHelper.IsSelect2(attr.ConditionKind, values0, values1))
                    {
                        return attr;
                    }
                    else if (attr.ConditionKind == AttributeConditionKind.Default)
                    {
                        result = attr;
                    }
                }
            }
            return result;
        }

        public static (Type, T)? SearchTagTable<T>(TableKey tableKey, object value0) where T : Attribute, ITagAttribute
        {
            (Type, T)? result = null;

            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                var attr = AttributeHelper.GetAttribute<T>(t);
                if (attr != null)
                {
                    if (ValueHelper.IsSelect(AttributeConditionKind.Equal, value0, attr.Tag))
                    {
                        result = (t, attr);
                        break;
                    }
                    else if (attr.Tag == Tag.Null)
                    {
                        result = (t, attr);
                    }
                }
            }
            return result;
        }

        //---------------------------------------------------------------

        public static object GetTableValueType(TableModel tableModel, string s)
        {
            if (tableModel != null)
            {
                var level = AttributeHelper.GetUnsigned(s);
                var model = tableModel;
                for (int i = 0; i < level; i++)
                {
                    model = model.Parent;
                    if (model == null)
                        throw new ArgumentException("AttributeHelper2.DoGetModelValueType");
                }
                return model.ValueType;
            }
            return null;
        }


        public static Type GetFontTableType(TableModel tableModel, string valueParameter)
        {
            if (!string.IsNullOrEmpty(valueParameter))
            {
                if (tableModel != null)
                {
                    var targModel = TableModelHelper.GetFontTableModel(tableModel, (Tag)valueParameter);
                    return targModel.ValueType;
                }
            }
            else
            {
                var kind = GetParentKind(tableModel);
                if (kind == TableOwnerKind.FontTableOwner)
                    return tableModel.ValueType;
                else if (kind == TableOwnerKind.FontTableChild)
                    return GetFontTableType(tableModel.Parent, null);
            }
            return null;
        }


        public static object GetFontTableValue(TableModel tableModel, string valueParameter)
        {
            var pos = valueParameter.IndexOf('\\');
            if (pos >= 0)
            {
                var s0 = valueParameter.Substring(0, pos).Trim();
                var s1 = valueParameter.Substring(pos).Trim();
                return TableModelHelper.GetFontTableValue(tableModel, s0, s1);
            }
            return null;
        }


        public static object CreateFieldObject(BinaryLoader loader, UInt32 filePosition, Type declaringType, string path)
        {
            if (loader != null)
            {
                var paths = path.Split(new[] { '\\' }, 2);
                var fi = declaringType.GetField(paths[0]);
                if (fi != null)
                {
                    if (fi.FieldType.IsClass)
                    {
                        UInt32 offset = (UInt32)Marshal.OffsetOf(declaringType, paths[0]);
                        return CreateFieldObject(loader, filePosition + offset, fi.FieldType, paths[1]);
                    }
                    else if (fi.FieldType.GetInterface(typeof(ITTFPrimitive).FullName) != null)
                    {
                        UInt32 offset = TypeHelper.OffsetOf(declaringType, paths[0]);
                        //return loader.CreatePrimitive(fi.FieldType, filePosition + offset);
                        return loader.GetPrimitiveReader().Read(fi.FieldType, filePosition + offset);
                    }
                    else if (fi.FieldType.IsGenericType && fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var type = fi.FieldType.GenericTypeArguments[0];
                        if (typeof(ITTFPrimitive).IsAssignableFrom(type))
                        {
                            UInt32 offset = TypeHelper.OffsetOf(declaringType, paths[0]);
                            //return loader.CreatePrimitive(type, filePosition + offset);
                            return loader.GetPrimitiveReader().Read(type, filePosition + offset);
                        }
                    }
                }
            }
            throw new ArgumentException("AttributeHelper.CreateObject3");
        }


        public static object CreatePeekObject(BinaryLoader loader, UInt32 filePosition, string valueParameter)
        {
            if (loader != null)
            {
                var typeOffset = GetPeekTypeOffset(valueParameter);
                if (typeof(ITTFPrimitive).IsAssignableFrom(typeOffset.Item1))
                {
                    //return loader.CreatePrimitive(typeOffset.Item1, filePosition + typeOffset.Item2);
                    return loader.GetPrimitiveReader().Read(typeOffset.Item1, filePosition + typeOffset.Item2);
                }
            }
            throw new ArgumentException("AttributeHelper.CreateObject2");
        }


        public static object GetOffsetSourceValue(TableModel tableModel, string valueParameter)
        {
            object result = null;

            UInt32 level = 1;
            var strs = valueParameter.Split(':');
            if (strs.Length == 2)
            {
                if (!string.IsNullOrEmpty(strs[0]))
                    level = System.Math.Max(UInt32.Parse(strs[0]), 1);
                valueParameter = strs[1];
            }

            var theModel = tableModel;
            while (--level != 0 && theModel != null)
                theModel = theModel.Parent;

            string path = theModel.SourcePath;
            if (valueParameter[0] == '\\')
                path = valueParameter;
            else
            {
                var pos = path.LastIndexOf('\\');
                path = path.Substring(0, pos + 1) + valueParameter;
            }
            result = theModel.Parent.BinaryLoader.GetTableLoader(theModel.Parent).GetValue(path); //.ToNumber3();
            return result;
        }


        enum TableOwnerKind
        {
            None = 0,
            FontTableOwner = 1,
            FontTableChild = 2,
        }

        static TableOwnerKind GetParentKind(TableModel tableModel)
        {
            var parentType = tableModel?.Parent?.ValueType;
            if (parentType == null)
                return TableOwnerKind.None;
            else if (IsAllowedType(typeof(TableDirectory), parentType))
                return TableOwnerKind.FontTableOwner;
            else if (IsAllowedType(typeof(TTCHeaderTable), parentType))
            {
                if (tableModel.ValueType.IsSubclassOf(typeof(TableDirectory)))
                    return TableOwnerKind.None;
                else
                    return TableOwnerKind.FontTableOwner;
            }
            else
                return TableOwnerKind.FontTableChild;
        }

        static bool IsAllowedType(Type baseType, Type createdType)
        {
            if (baseType == createdType)
                return true;
            var attrs = DataTypes.AttributeHelper.GetAttributes<ClassTypeConditionAttribute>(baseType);
            foreach (var attr in attrs)
            {
                if (attr.Type == createdType)
                    return true;
            }
            return false;
        }

        static (Type, UInt32) GetPeekTypeOffset(string valueParameter)
        {
            string typeName;
            UInt32 offset;
            Int32 pos = valueParameter.IndexOf(':');
            if (pos > 0)
            {
                typeName = valueParameter.Substring(pos + 1);
                offset = UInt32.Parse(valueParameter.Substring(0, pos));
            }
            else
            {
                typeName = valueParameter;
                offset = 0;
            }

            Type type = Type.GetType($"{typeof(ITTFPrimitive).Namespace}.{typeName}");
            return (type, offset);
        }
    }
}
