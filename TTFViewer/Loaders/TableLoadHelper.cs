using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TTFViewer.DataTypes;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.Loaders
{
    static class TableLoadHelper
    {
        // return (rightElementType, sizeof(sourceElementType)
        public static (Type, UInt32)? GetUniformTypeSize(TableLoader tableLoader, FieldInfo fieldInfo, Type leftElementType, Int32 axis)
        {
            // type: uint32,int32,uint16,int16,uint8,int8,
            if (leftElementType.GetInterface(typeof(ITTFPrimitive).FullName) != null)
            {
                return (leftElementType, (UInt32)Marshal.SizeOf(leftElementType));
            }
            else if (AttributeHelper.GetAttribute<UniformTypeAttribute>(fieldInfo, axis) != null || DataTypes.AttributeHelper.GetAttribute<UniformTypeAttribute>(leftElementType) != null)
            {
                if (leftElementType == typeof(ITTFPrimitive))
                {
                    TypeSelectAttributeReader typeSelectAttrReader = new TypeSelectAttributeReader(tableLoader);// TableModel, LoadItem);
                    var attr = AttributeHelper.GetAttribute<TypeSelectAttribute>(fieldInfo);
                    var rightElementType = typeSelectAttrReader.GetValueType(0, fieldInfo, attr);
                    if (rightElementType == typeof(ITTFPrimitive))
                        return (rightElementType, 0);
                    return (rightElementType, (UInt32)Marshal.SizeOf(rightElementType));
                }
                else
                {
                    var lengthAttribute = AttributeHelper.GetAttribute<LengthAttribute>(fieldInfo, axis);
                    if (lengthAttribute != null)
                    {
                        var reader = new LengthAttributeReader(tableLoader);
                        var length2 = reader.GetLength(lengthAttribute, fieldInfo.DeclaringType);
                        return (leftElementType, length2);
                    }
                    else
                        return (leftElementType, (UInt32)Marshal.SizeOf(leftElementType));
                }
            }
            return null;
        }


        static bool CompareList(List<string> lhs, List<string> rhs)
        {
            if (lhs.Count != rhs.Count)
                return true;
            for (int i = 0; i < lhs.Count; i++)
                if (lhs[i] != rhs[i])
                    return true;
            return false;
        }


        public static (LoadItem2, string) ProcessPath1(TableModel tableModel, LoadItem2 item, string path)
        {
            var pathList = path.Split('\\').ToList();
            if (path.StartsWith("\\"))
            {
                pathList.RemoveAt(0);
                item = GetRoot(item, tableModel);
                if (item.Parent == null && item.ElementList?.BasePath != null)
                {
                    //Debug.WriteLine("TableLoadHelper.GetPathList");
                    item = LoadItem2.CreateItem("", tableModel.FilePosition, tableModel.ValueType);//, i => TableLoadHelper.CreateModelObject(tableModel));
                }
            }
            else if (pathList[0] == "")
                pathList.RemoveAt(0);

            else if (pathList[0][0] == '.')
            {
                string basePath = null;
                while (pathList[0] == "..")
                {
                    pathList.RemoveAt(0);
                    if (basePath == null)
                    {
                        var parent = item.Parent;
                        if (parent != null)
                            item = parent;
                        else
                        {
                            basePath = item.ElementList?.BasePath;
                            if (basePath == null)
                                return (null, null);
                            item = LoadItem2.CreateItem("", tableModel.FilePosition, tableModel.ValueType);//, i => TableLoadHelper.CreateModelObject(tableModel));
                        }
                    }
                    else
                    {
                        int pos = basePath.LastIndexOf('\\');
                        if (pos >= 0)
                            basePath = basePath.Substring(0, pos);
                        else
                            basePath = "";

                    }
                }
                if (basePath != null)
                {
                    var baseList = basePath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    baseList.AddRange(pathList);
                    pathList = baseList;
                }
            }
            else
            {

            }
            string result;
            if (pathList == null)
                result = null;
            else if (pathList.Count == 0 || pathList[0] == "")
                result = "";
            else
            {
                result = "";
                foreach (var s in pathList)
                    result += $"{s}\\"; ;
            }
            return (item, result); ;
        }


        static List<string> GetPathList(string path)
        {
            var result = new List<string>();

            var pathList = path.Split('\\');

            foreach (var s in pathList)
            {
                var list = s.Split('[');
                if (s.Length == 0 || s[0] != '[')
                    result.Add(list[0]);
                foreach (var i in list.Skip(1))
                    result.Add("[" + i);
            }

            return result;
        }


        static LoadItem2 GetRoot(LoadItem2 item, TableModel tableModel)
        {
            LoadItem2 p = item;
            while (p.Parent != null)
                p = p.Parent;

            return p;
        }




        //------------ContainerAttributeReader

        public static object CreateItemObject(LoadItem2 item, TableModel tableModel, FieldInfo fi)
        {
            object result = null;

            var leftValueType = item.LeftValueType;
            var fullPath = item.GetFullPath();

            var axis = TablePathHelper.GetAxis(item.GetFullPath());

            if (fi == null)
            {
                // ModelObject
                if (String.IsNullOrEmpty(item.GetFullPath()))
                {
                    return CreateModelObject(tableModel);
                }
            }
            else //if (fi != null)
            {
                var types = GGetContainerAndElementType(leftValueType);
                if (types.Item1 != null)
                {
                    var reader = new ContainerReader(tableModel, item, fi);
                    result = reader.GetContainer(types.Item2, axis);
                    if (types.Item1 == typeof(Array) && result is IElementList iel)
                        result = item.ConvertToArray(tableModel, fi, iel);
                    else
                        return result;
                }
            }

            if (result != null && !leftValueType.IsAssignableFrom(result.GetType()))
            {
                //result = null;
            }
            else if (result == null)
            {
                if (item.ElementList == null
                    || DataTypes.AttributeHelper.GetAttribute<UniformTypeAttribute>(fi, axis) == null)
                {
                    var attr = DataTypes.AttributeHelper.GetAttribute<TypeSelectAttribute>(fi);
                    if (attr != null)// && !attr.Uniform)
                    {
                        var tableLoader = tableModel.GetTableLoader(item);
                        var attrReader = new TypeSelectAttributeReader(tableLoader);// tableModel, item);
                        var vt = attrReader.GetValueType(axis, fi, attr);
                        if (vt == null
                            && (fi.FieldType.IsGenericType
                                && (fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                        || fi.FieldType.GetGenericTypeDefinition() == typeof(IList<>))
                                    || fi.FieldType.IsClass))
                        {
                            return null;
                        }
                        result = TableLoadHelper.CreateItemObject2(tableModel, item, fi, vt ?? leftValueType); //, valueInfos);
                    }
                }
                if (result == null)
                {
                    var classReader = new ClassAttributeReader(tableModel, item.FilePosition, leftValueType);
                    var vt = classReader.GetValueType();
                    result = TableLoadHelper.CreateItemObject2(tableModel, item, fi, vt ?? leftValueType); //, valueInfos);
                }

            }
            return result;
        }


        static object CreateItemObject2(TableModel tableModel, LoadItem2 item, FieldInfo fi, Type leftValueType)
        {
            if (leftValueType.IsArray)
                return null;
            else if (leftValueType.IsGenericType && leftValueType.GetGenericTypeDefinition() == typeof(IList<>))
                return null;
            else if (leftValueType.IsGenericType && leftValueType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var result = Activator.CreateInstance(leftValueType);
                return result;
            }
            else
            {
                return Activator.CreateInstance(leftValueType);
            }
        }


        public static (Type, Type) GGetContainerAndElementType(Type leftValueType)
        {
            (Type, Type) result = (null, null);
            if (leftValueType != null)
            {
                if (leftValueType.IsArray)
                {
                    result.Item1 = typeof(Array);
                    result.Item2 = leftValueType.GetElementType();
                }
                else if (leftValueType.IsGenericType && leftValueType.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    result.Item1 = typeof(IElementList);
                    result.Item2 = leftValueType.GenericTypeArguments[0];
                }
            }
            return result;
        }


        public static object CreateModelObject(TableModel tableModel)
        {
            object result = null;
            var valueType = tableModel.ValueType;
            var types = GGetContainerAndElementType(valueType);
            if (types.Item1 != null)
            {
                if (tableModel.ElementCount is Int32 i32)
                {
                    if (i32 > 0)
                    {
                        result = UniformElementListHelper.Create(types.Item2, tableModel, "", tableModel.FilePosition, null, i32, (types.Item2, (UInt32)Marshal.SizeOf(types.Item2)));
                    }
                }
            }

            if (result != null && !valueType.IsAssignableFrom(result.GetType()))
            {
                //result = null; ;
            }
            else if (result == null)
            {
                result = Activator.CreateInstance(valueType);
            }
            return result;
        }
    }
}
