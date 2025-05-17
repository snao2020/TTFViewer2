using System;
using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.Loaders
{
    class ValuesAttributeReader
    {
        TableLoader TableLoader;

        public ValuesAttributeReader(TableLoader tableLoader)
        {
            TableLoader = tableLoader;
        }

        public IElementList GetContainer(ValuesAttribute attr, FieldInfo fi, Int32 axis, Type elementType)
        {
            IElementList result = null;

            if (attr != null)
            {
                if (attr.MethodName != null)
                {
                    var reader = new FieldValueReader(TableLoader, fi.DeclaringType);
                    var service = reader.GetAttributeService();
                    MethodInfo mi = AttributeHelper.GetMethodInfo(fi.DeclaringType, attr.MethodName, null);
                    var ret = mi.Invoke(null, new object[] { service });
                    if (ret != null)
                    {
                        var t = typeof(ValuesElementList<>).MakeGenericType(new[] { elementType });
                        result = Activator.CreateInstance(t, new object[] { TableLoader.TableModel, TableLoader.GetFullPath(), (Object)TableLoader.GetFilePosition(), fi.DeclaringType, ret }) as IElementList;
                    }
                }
                
                else if (attr.ValuesKind == ValuesKind.ParentValue)
                {
                    if(GetLengthArray(attr.ParentFieldName) is ValueTuple<UInt32, Array> lengthArray)
                    {
                        var t = typeof(ValuesElementList<>).MakeGenericType(new[] { elementType });
                        result = Activator.CreateInstance(t, new object[] { TableLoader.TableModel, TableLoader.GetFullPath(), (Object)TableLoader.GetFilePosition(), fi.DeclaringType, lengthArray.Item1, lengthArray.Item2 }) as IElementList;
                    }
                }
            }
            return result;
        }


        (UInt32,Array)? GetLengthArray(String parentFieldName)//ArrayValueAttribute attr)
        {
            var parentElementList = TableLoader.GetElementList();
            if (parentElementList != null)
            {
                var index = TablePathHelper.GetLastIndex(TableLoader.GetFullPath());
                var elementLength = parentElementList.GetElementLength(index);
                var elementType = parentElementList.GetElementType(index);
                var elementValue = parentElementList.GetItem(index);
                if(parentFieldName == null)
                {
                    if (elementValue is Array array)
                        return (elementLength, array);
                }
                else
                {
                    var fi = elementValue.GetType().GetField(parentFieldName);
                    if (fi != null)
                        return (elementLength, fi.GetValue(elementValue) as Array);
                }
            }
            return null;
        }
    }
}
