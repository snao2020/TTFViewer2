using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;

namespace TTFViewer.ViewModel
{
    /*
    parameter value : model value
    parameter path  : absolute path
    */

    class ViewModelHelper
    {
        // abc[1][2][3] --> ("abc",(1,2,3))
        // abc[1]a[2][3] --> ("abc", empty)
        public static (string, List<Int32>) GetNameIndexes(string name)
        {
            var list = new List<Int32>();
            var pos = name.IndexOf('[');
            if (pos < 0)
                return (name, list);
            else
            {
                var body = name.Substring(0, pos);
                name = name.Substring(pos + 1);
                for (; ; )
                {
                    var end = name.IndexOf(']');
                    if (end <= 0)
                        return (null, null); //error
                    else
                    {
                        var sub = name.Substring(0, end);
                        list.Add(Int32.Parse(sub));
                        end++;
                        if (name.Length > end)
                        {
                            if (name[end] == '[')
                                name = name.Substring(end + 1);
                            else
                            {
                                list.Clear();
                                break;
                            }
                        }
                        else
                            break;
                    }
                }
                return (body, list);
            }
        }


        static UInt32? GetFilePosition(object value, UInt32 filePosition, string path)
        {
            if (path == null)
                return filePosition;

            if (path[0] == '\\')
                path = path.Substring(1);

            string name;
            var pos = path.IndexOf('\\');
            if (pos > 0)
            {
                name = path.Substring(0, pos);
                path = path.Substring(pos + 1);
            }
            else
            {
                name = path;
                path = null;
            }
            if (name == "")
                return filePosition;

            else if (name[0] == '[')
            {
                var index = Int32.Parse(name.Substring(1, name.Length - 2));
                if (value is IElementList iel)
                {
                    if (iel is IList list)
                        return GetFilePosition(list[index], iel.GetElementPosition(index), path);
                    else
                        return 0;
                }
                else if (value is Array array)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        var childValue = array.GetValue(i);
                        if (i == index)
                            return GetFilePosition(childValue, filePosition, path);
                        else
                        {
                            var fp = GetFilePosition(childValue, filePosition, path);
                            if (fp is UInt32 u32)
                                filePosition = u32;
                            else
                                return null;
                        }
                    }
                    return filePosition;
                }
                else
                    return null;
            }
            else
            {
                var type = value.GetType();
                if (type.IsClass)
                {
                    foreach (FieldInfo fi in TypeHelper.B2DFieldInfos(type))
                    {
                        object childValue = fi.GetValue(value);
                        if (fi.Name == name)
                        {
                            return GetFilePosition(childValue, filePosition, path);
                        }
                        else
                            filePosition += TypeHelper.CalcSize(childValue);
                    }
                }
                return filePosition;
            }
        }


        static object[] GetPathValues(object modelValue, string srcPath, PositionAttribute attr)
        {
            object[] result = null;

            if(attr.Value?.ValueKind.Value is FieldValueKind vk && vk == FieldValueKind.Path)
            {
                var indexes = TablePathHelper.GetIndexes(srcPath);
                var paths = attr.Value.ValueParameter.Split(',');
                var list = new List<object>();
                foreach (var path in paths)
                {
                    var s = path.Trim();
                    s = TablePathHelper.MergePathIndexes(s, indexes);
                    var o = GetPathValue(modelValue, s);
                    list.Add(o);
                }
                result = list.ToArray();
            }
            return result;
        }


        static object GetPathValue(object value, string path)
        {
            if (path == null || value == null)
                return value;

            if (path[0] == '\\')
                path = path.Substring(1);
            string name;
            var pos = path.IndexOf('\\');
            if (pos > 0)
            {
                name = path.Substring(0, pos);
                path = path.Substring(pos + 1);
            }
            else
            {
                name = path;
                path = null;
            }
            if (name[0] == '[')
            {
                if (value is IList list)
                {
                    var index = Int32.Parse(name.Substring(1, name.Length - 2));
                    var childValue = list[index];
                    return GetPathValue(childValue, path);
                }
                else
                    return null;
            }
            else
            {
                var type = value.GetType();
                if (type.IsClass)
                {
                    var fi = type.GetField(name);
                    if (fi != null)
                    {
                        var childValue = fi.GetValue(value);
                        return GetPathValue(childValue, path);
                    }
                }
                return null;
            }

        }

        public static IEnumerable<(string path, object obj)> EnumItem(string path, FieldInfo fi, object obj)
        {
            if (obj is IList list)
            {
                var index = 0;
                foreach (var child in list)
                {
                    var ret = EnumItem($"{path}\\[{index++}]", fi, child);
                    foreach (var i in ret)
                        yield return i;
                }
                yield break;
            }
            else
                yield return (path, obj);
        }


        public static UInt32? GetTopFieldPosition(object modelValue, UInt32 modelFilePosition, string path)
        {
            var fieldName = TablePathHelper.GetFieldPath(path);
            if (fieldName.Contains('\\'))
                throw new ArgumentException($"ViewModelHelper.GetPosition not support child position {path}");
            var modelValueType = modelValue.GetType();
            var axis = TablePathHelper.GetAxis(path);
            var fi = modelValueType.GetField(fieldName);
            var attr = DataTypes.AttributeHelper.GetAttribute<PositionAttribute>(fi, axis);
            if (attr != null)
            {
                return GetPositionAttrPosition(modelValueType, modelValue, modelFilePosition, path, attr);
            }
            else
            {
                return GetNoAttrPosition(modelValue, modelFilePosition, path, fieldName);
            }

        }

        static UInt32? GetNoAttrPosition(object modelValue, UInt32 modelFilePosition, string path, string fieldName)
        {
            FieldInfo prevFieldInfo = null;
            foreach(var fi in TypeHelper.B2DFieldInfos(modelValue.GetType()))
            {
                if (fi.Name == fieldName)
                {
                    if (prevFieldInfo == null)
                        return modelFilePosition;
                    else
                    {
                        UInt32? prevPosition = GetTopFieldPosition(modelValue, modelFilePosition, prevFieldInfo.Name);
                        if(prevPosition is UInt32 u32)
                        {
                            var length = TypeHelper.CalcSize(prevFieldInfo.GetValue(modelValue));
                            return u32 + length;
                        }
                    }
                }
                else
                    prevFieldInfo = fi;
            }
            return null;
        }


        static UInt32? GetPositionAttrPosition(Type declaringType, object modelValue, UInt32 modelFilePosition, string path, PositionAttribute attr)
        {
            UInt32? result = null;

            object[] values = GetPathValues(modelValue, path, attr);

            Int32? offs = PositionAttributeReader.GetOffset(declaringType, attr, /*modelValue.GetType(),*/ values);
            if (offs == null)
                return result;

            if (attr.BasePosition != null)
            {
                if (attr.BasePosition == "\\")
                    result = (UInt32)(modelFilePosition + (Int32)offs);
                else
                {
                    var basePath = attr.BasePosition.Trim();
                    if (basePath.Contains("[]"))
                    {
                        var indexes = TablePathHelper.GetIndexes(path);
                        basePath = TablePathHelper.MergePathIndexes(basePath, indexes);
                    }
                    var basePosition = GetTopFieldPosition(modelValue, modelFilePosition, basePath);
                    if (basePosition is UInt32 u32)
                        result = (UInt32)(u32 + (Int32)offs);
                    else
                        result = null;
                }
            }
            return result;
        }
    }
}
