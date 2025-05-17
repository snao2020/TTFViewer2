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
    class LoadItem2
    {
        [Flags]
        enum ValueState
        {
            None = 0,
            Position = 1,
            Created = 2,
            Loaded = Created | 4,
            Completed = Position | Loaded,
        }


        // ctor
        public LoadItem2 Parent { get; }
        private List<LoadItem2> Children { get; }
        public string Name { get; }
        public IElementList ElementList { get; private set; }

        public UInt32 FilePosition { get; private set; }

        public Type LeftValueType { get; private set; }
        public Type ObjectType => Object?.GetType();

        object Object;
        ValueState State;


        static FieldInfo GetFieldInfo(BinaryReader binaryReader, TableModel tableModel, LoadItem2 loadItem)
        {
            var p = TablePathHelper.GetFieldPath2(loadItem.GetFullPath());
            if (String.IsNullOrEmpty(p))
                return null;

            if (loadItem.ElementList != null)
            {
                if (loadItem.ElementList.DeclaringType != null)
                {
                    var basePath2 = loadItem.ElementList.BasePath; ;
                    for (; ; )
                    {
                        var ret = TablePathHelper.RemoveElement(basePath2);
                        if (ret == null)
                            break;
                        basePath2 = ret;
                    }
                    if (basePath2 != null)
                    {
                        string name;
                        var pos = basePath2.LastIndexOf('\\');
                        if (pos < 0)
                            name = basePath2;
                        else
                            name = basePath2.Substring(pos + 1);
                        var fi = loadItem.ElementList.DeclaringType.GetField(name);
                        if (fi != null)
                        {
                            return fi;
                        }
                    }
                }
            }


            var fullPath = loadItem.GetFullPath();

            var orgFp = binaryReader.BaseStream.Position;

            var item = loadItem;
            string basePath = null;
            for (; ; )
            {
                if (item != null)
                {
                    if (TablePathHelper.RemoveElement(item.Name) != null)
                    {
                        var parent = item.Parent;
                        if (parent == null)
                            basePath = item.ElementList?.BasePath;
                        item = parent;
                    }
                    else
                        break;
                }

                else if (basePath != null)
                {
                    var path = TablePathHelper.RemoveElement(basePath);
                    if (path == null)
                    {
                        break;
                    }
                    else
                    {
                        basePath = path;
                    }
                }
            }
            string fieldName;
            if (basePath != null)
            {
                var pos = basePath.LastIndexOf('\\');
                if (pos > .0)
                {
                    fieldName = basePath.Substring(pos + 1);
                    basePath = basePath.Substring(0, pos);
                }
                else
                {
                    fieldName = basePath;
                    basePath = "";
                }
                var rootLoader = new TableLoader(binaryReader, tableModel);
                item = rootLoader.GetDescendent(basePath);
            }
            else
            {
                fieldName = item.Name;
                item = item.Parent;
            }
            FieldInfo result = null;
            if (item != null)
            {
                var fi = item.GetFieldInfo(fieldName, binaryReader, tableModel);
                if (fi != null)
                    result = fi;
            }
            binaryReader.BaseStream.Position = orgFp;
            return result;
        }


        FieldInfo GetFieldInfo(string fieldName, BinaryReader reader, TableModel tableModel)
        {
            FieldInfo result = null;

            if (Object == null)
            {
                GetItem("", reader, tableModel);
            }
            if (Object != null)
            {
                result = Object.GetType().GetField(fieldName);
            }
            return result;
        }


        public Object ConvertToArray(TableModel tableModel, FieldInfo fi, IElementList iel)
        {
            Object result = null;
            if (LeftValueType.IsArray && iel is IList list)
            {
                Type elementType = LeftValueType.GetElementType();
                var array3 = Array.CreateInstance(elementType, list.Count);
                for (Int32 i = 0; i < list.Count; i++)
                {
                    var childName = $"[{i}]";
                    var child = Children.Find(c => c.Name == childName);
                    if (child == null)
                    {
                        child = new LoadItem2(this, iel, i);// childName, elementType);
                    }
                }
                result = array3;
            }

            return result;
        }


        public static LoadItem2 CreateItem(string name, UInt32 filePosition, Type valueType)
        {
            LoadItem2 result = new LoadItem2(name, filePosition, valueType);
            return result;
        }


        LoadItem2(string name, UInt32 filePosition, Type valueType)
        {
            Name = name; // "";
            FilePosition = filePosition;
            State = ValueState.Position;
            LeftValueType = valueType;
            Children = new List<LoadItem2>();
        }


        public LoadItem2(IElementList elementList, Int32 index)
        {
            ElementList = elementList;
            Name = $"[{index}]";
            FilePosition = elementList.GetElementPosition(index);
            if (elementList.GetType().IsGenericType
                && elementList.GetType().GetGenericTypeDefinition() == typeof(DiscreteList<>))
                State = ValueState.None;
            else
                State = ValueState.Position;
            LeftValueType = elementList.GetElementType(index);
            Children = new List<LoadItem2>();
        }


        LoadItem2(LoadItem2 parent, IElementList elementList, Int32 index)
        {
            Parent = parent;
            Children = new List<LoadItem2>();

            Name = $"[{index}]";
            FilePosition = elementList.GetElementPosition(index);
            LeftValueType = elementList.GetElementType(index);
            ElementList = elementList;

            if (parent != null)
                parent.Children.Add(this);

            if (elementList.GetType().IsGenericType
                && elementList.GetType().GetGenericTypeDefinition() == typeof(DiscreteList<>))
                State = ValueState.None;
            else
                State = ValueState.Position;
        }


        LoadItem2(TableModel tableModel, LoadItem2 parent, FieldInfo fi, string name, Type leftValueType)
        {
            Parent = parent;
            Children = new List<LoadItem2>();
            Name = name;
            LeftValueType = leftValueType;
            if (parent != null)
                parent.Children.Add(this);
            var axis = TablePathHelper.GetAxis(parent.GetFullPath());
            State = ValueState.None;
        }


        public string GetFullPath()
        {
            var result = Parent != null ? Parent.GetFullPath() : ElementList?.BasePath;
            if (string.IsNullOrEmpty(result))
                result = Name;
            else if (!string.IsNullOrEmpty(Name))
                result += $"\\{Name}";
            return result;
        }


        public (string, List<Int32>) GetFieldValueKeyIndexes()
        {
            (string, List<Int32>) result = ("", new List<Int32>());
            if (Parent != null)
                result = Parent.GetFieldValueKeyIndexes();
            else
            {
                var basePath = ElementList?.BasePath;
                if (!string.IsNullOrEmpty(basePath))
                {
                    var strs = basePath.Split('\\');
                    foreach (var str in strs)
                    {
                        if (str == "")
                            continue;
                        else if (str[0] == '[')
                        {
                            result.Item1 += "\\[]";
                            result.Item2.Add(Int32.Parse(str.Substring(1, str.Length - 2)));
                        }
                        else
                        {
                            if (result.Item1 == "")
                                result.Item1 = str;
                            else
                                result.Item1 += $"\\{str}";
                        }

                    }
                }
            }

            if (!string.IsNullOrEmpty(Name) && Name.Last() == ']')
            {
                result.Item1 += "\\[]";
                result.Item2.Add(Int32.Parse(Name.Substring(1, Name.Length - 2)));
            }
            else
            {
                if (result.Item1 == "")
                    result.Item1 = Name;
                else if (Name != "")
                    result.Item1 += $"\\{Name}";
            }
            return result;
        }


        public LoadItem2 GetChild(string fieldName, BinaryReader binaryReader, TableModel tableModel)
        {
            //var pathList = new List<string> { fieldName };
            var result = GetItem(fieldName, binaryReader, tableModel);
            return result;
        }


        public LoadItem2 FindChild(string name)
        {
            foreach (var i in Children)
            {
                if (i.Name == name)
                    return i;
            }
            return null;
        }


        public object GGetValue(string path, BinaryReader reader, TableModel tableModel)
        {
            object result = null;
            var item = GetItem(path, reader, tableModel); // peek:false
            if (item != null)
            {
                item.Load(reader, tableModel);
                result = item.Object;
            }
            return result;
        }


        public UInt32 GetLength(BinaryReader reader, TableModel tableModel)
        {
            GetItem(null, reader, tableModel);

            var rbItem = GetRightBottomItem();

            var result = rbItem.FilePosition - FilePosition;

            if (rbItem.Object is ITTFPrimitive primitive)
                result += (UInt32)Marshal.SizeOf(primitive.GetType());
            else if (rbItem.Object is IElementList iel)
                result += iel.GetFileLength();

            return result;
        }

        LoadItem2 GetRightBottomItem()
        {
            if (Children.Count > 0)
                return Children.Last().GetRightBottomItem();
            else
                return this;
        }

        string GetChildName(List<string> pathList)
        {
            string childName = null;

            if (pathList != null && pathList.Count == 0)
                childName = "";

            if (pathList != null && pathList.Count > 0)
            {
                childName = pathList[0];
                if (childName == "[LAST]")
                {
                    if (Object is IElementList iel && iel.GetCount() > 0)
                    {
                        childName = $"[{iel.GetCount() - 1}]";
                    }
                    else if (Object is Array array && array.Length > 0)
                    {
                        childName = $"[{array.Length - 1}]";
                    }
                }
            }
            return childName;
        }


        void Load(BinaryReader reader, TableModel tableModel)
        {
            long fp = reader.BaseStream.Position;
            reader.BaseStream.Position = FilePosition;
            var fullPath = GetFullPath();
            FieldInfo fi = GetFieldInfo(reader, tableModel, this);
            DoLoad(reader, tableModel, fi); //, null);

            reader.BaseStream.Position = fp;
        }


        // side effect : reader.BaseStream.Position will be changed
        void DoLoad(BinaryReader reader, TableModel tableModel, FieldInfo fieldInfo)
        {
            SetPositionObject(reader, tableModel, fieldInfo);

            if (State == ValueState.Completed)
            {
                var rbItem = GetRightBottomItem();
                if (rbItem.Object is IElementList iel)
                    reader.BaseStream.Position = rbItem.FilePosition + iel.GetFileLength();
                else if (Object is ITTFPrimitive)
                    reader.BaseStream.Position = rbItem.FilePosition + (UInt32)Marshal.SizeOf(rbItem.Object);
                else
                    reader.BaseStream.Position = rbItem.FilePosition;
                return;
            }

            State |= ValueState.Loaded;
            switch (Object)
            {
                case ITTFPrimitive primitive:
                    ProcessPrimitive(reader, primitive, true);// load:true 
                    break;

                case IElementList iel:
                    GetChildOfElementList(reader, fieldInfo, iel, null); //childName:null
                    break;

                case Array array:
                    foreach (var child in EnumArray(reader, tableModel, fieldInfo, array))
                    {
                        if (TablePathHelper.GetIndex(child.Name) is Int32 index)
                        {
                            child.DoLoad(reader, tableModel, fieldInfo);
                            array.SetValue(child.Object, index);
                        }
                        else
                            throw new ArgumentException();
                    }
                    break;

                default:
                    if (Object != null)
                    {
                        foreach (var child in EnumClass(reader, tableModel))
                        {
                            child.Item1.DoLoad(reader, tableModel, child.Item2);
                            child.Item2.SetValue(Object, child.Item1.Object);
                        }
                    }
                    break;
            }
        }


        // if path == null, Create this and all subtrees
        // if path == "", Create this only
        LoadItem2 GetItem(string path, BinaryReader reader, TableModel tableModel)
        {
            List<string> pathList = null;
            if (path != null)
                pathList = path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            long fp = reader.BaseStream.Position;

            FieldInfo fi = GetFieldInfo(reader, tableModel, this);
            var result = DoGetItem2(pathList, reader, tableModel, fi);

            reader.BaseStream.Position = fp;

            if (result == null && pathList?.Count > 0)
                throw new ArgumentException($"LoadItem2.GetItem argument error path={path}");

            return result;
        }


        LoadItem2 DoGetItem2(List<string> pathList, BinaryReader reader, TableModel tableModel, FieldInfo fieldInfo) //, bool measure)
        {
            SetPositionObject(reader, tableModel, fieldInfo);

            var childName = GetChildName(pathList);
            if (childName == "")
                return this;

            LoadItem2 result = null;

            switch (Object)
            {
                case ITTFPrimitive primitive:
                    ProcessPrimitive(reader, primitive, false);// load:false
                    break;

                case IElementList iel:
                    result = GetChildOfElementList(reader, fieldInfo, iel, childName);
                    if (result != null)
                    {
                        pathList?.RemoveAt(0);
                        result = result.DoGetItem2(pathList, reader, tableModel, fieldInfo);
                    }
                    break;

                case Array array:
                    foreach (var child in EnumArray(reader, tableModel, fieldInfo, array))
                    {
                        if (TablePathHelper.GetIndex(child.Name) is Int32 index)
                        {
                            if (childName == child.Name)
                            {
                                pathList?.RemoveAt(0);
                                result = child.DoGetItem2(pathList, reader, tableModel, fieldInfo);
                                break;
                            }
                            else
                            {
                                child.DoGetItem2(null, reader, tableModel, fieldInfo); //, measure);
                            }
                            array.SetValue(child.Object, index);
                        }
                        else
                            throw new ArgumentException();
                    }
                    break;

                default:
                    if (Object != null)
                    {
                        foreach (var childFi in EnumClass(reader, tableModel))
                        {
                            if (childName == childFi.Item1.Name)
                            {
                                pathList?.RemoveAt(0);
                                result = childFi.Item1.DoGetItem2(pathList, reader, tableModel, childFi.Item2);
                                break;
                            }
                            else
                            {
                                childFi.Item1.DoGetItem2(null, reader, tableModel, childFi.Item2);
                            }
                        }
                    }
                    else if(pathList != null)
                        pathList.Clear();
                    break;
            }
            return result;
        }


        void ProcessPrimitive(BinaryReader reader, ITTFPrimitive primitive, bool load)
        {
            if (load)
                primitive.Load(reader);
            else
                reader.BaseStream.Position = FilePosition + (UInt32)Marshal.SizeOf(Object);
        }


        LoadItem2 GetChildOfElementList(BinaryReader reader, FieldInfo fi, IElementList iel, string childName)
        {
            if (childName != null)
            {
                Int32? index = TablePathHelper.GetIndex(childName);
                if (index is Int32 i32)
                {
                    var elementPosition = iel.GetElementPosition(i32); // index);
                    reader.BaseStream.Position = elementPosition;
                    childName = $"[{index}]";
                    Type elementType = iel.GetElementType(i32);

                    var child = new LoadItem2(this, iel, i32); // index);
                    return child;
                }
                else
                {
                    throw new ArgumentException($"LoadItem2.GetItem {childName}");
                }
            }
            else //if (AttributeHelper.GetAttribute<LabelAttribute>(fi, TablePathHelper.GetAxis(GetFullPath())) == null)
            {
                reader.BaseStream.Position += iel.GetFileLength();
                return null;
            }
        }


        IEnumerable<LoadItem2> EnumArray(BinaryReader reader, TableModel tableModel, FieldInfo fi, Array array)
        {
            foreach (var child in Children)
                yield return child;
            yield break;
        }


        IEnumerable<(LoadItem2, FieldInfo)> EnumClass(BinaryReader reader, TableModel tableModel)
        {
            if (Object != null)
            {
                foreach (var childFi in TypeHelper.B2DFieldInfos(Object.GetType()))
                {
                    var child = Children.Find(i => i.Name == childFi.Name);
                    if (child == null)
                    {
                        child = new LoadItem2(tableModel, this, childFi, childFi.Name, childFi.FieldType);
                    }
                    yield return (child, childFi); ;
                }
            }
            yield break;
        }


        void SetPositionObject(BinaryReader reader, TableModel tableModel, FieldInfo fieldInfo)
        {
            var tableLoader = tableModel.BinaryLoader.GetTableLoader(tableModel, this);
            var verificationReader = new VerificationAttributeReader(tableLoader);
            if(!verificationReader.IsValid(fieldInfo))
            {
                State = ValueState.Completed;
                //2025/04/29 start
                FilePosition = (UInt32)reader.BaseStream.Position;
                //2025/04/29 ebd
                Object = null;
                return;
            }
            if (!State.HasFlag(ValueState.Position))
            {
                State |= ValueState.Position;
                FilePosition = (UInt32)reader.BaseStream.Position;
                if (fieldInfo != null)
                {
                    var axis = TablePathHelper.GetAxis(GetFullPath());
                    var attr = DataTypes.AttributeHelper.GetAttribute<PositionAttribute>(fieldInfo, axis);
                    if (attr != null)
                    {
                        var positionAttrReader = new PositionAttributeReader(tableModel, this);
                        var fp2 = positionAttrReader.GetFilePosition(fieldInfo.DeclaringType, attr);
                        if (fp2 is UInt32 u32)
                        {
                            FilePosition = u32;
                        }
                        else // if(DICT,Operator not found
                        {
                            State = ValueState.Completed;
                            Object = null;
                        }
                    }
                }
            }
            reader.BaseStream.Position = FilePosition;

            if (!State.HasFlag(ValueState.Created))
            {
                State |= ValueState.Created;
                Object = TableLoadHelper.CreateItemObject(this, tableModel, fieldInfo);
            }
        }
    }
}
