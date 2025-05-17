using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.ViewModel
{
#pragma warning disable IDE1006

    static class ItemViewModelHelper
    {
        public static Int32 GetGroupAxis(ItemViewModel ivm)
        {
            Int32 result = 0;
            while (ivm != null && ivm is GroupItemViewModel)
            {
                result++;
                ivm = ivm.Parent;
            }
            return result;
        }


        static UInt32 GetContainerLength(IList list)
        {
            UInt32 result = 0;
            if (list is IElementList iel)
                result = iel.GetFileLength();
            else
            {
                UInt32 fileLength = 0;
                foreach (var item in list)
                    fileLength += TypeHelper.CalcSize(item);
                result = fileLength;
            }
            return result;
        }


        static IList GetSubContainer(int i, ItemViewModel ivm, Int32 itemIndex, Int32 itemCount)
        {
            IList result = null;
            if (ivm.Value is IElementList iel)
            {
                var t = ivm.Value.GetType();
                var t4 = t.BaseType;
                if(t4.IsGenericType)
                {
                    String name = $"[{i}]";
                    var parent = ivm;
                    while(parent.Value is IGroupContainer)
                    {
                        name = $"{parent.Name}{name}";
                        parent = parent.Parent;
                    }
                    var t2 = t4.GenericTypeArguments[0];
                    var t3 = typeof(GroupElementList<>).MakeGenericType(new[] { t2 });
                    var parameter = new object[] { name, iel, itemIndex, itemCount };
                    result = Activator.CreateInstance(t3, parameter) as IList;
                }
                //result = new SubcontainerElementList<uint8>(iel, itemIndex, itemCount);
            }
            else if (ivm.Value is IList<uint8> list)
            {
                result  = list.Skip(itemIndex).Take(itemCount).ToArray();
            }
            return result;

        }


        public static List<ItemViewModel> CreateGroup(ItemViewModel ivm)
        {
            List<ItemViewModel> result = null;

            Int32 axis = GetGroupAxis(ivm);

            var ivp = new ItemValueProvider(ivm.ItemValueProvider);
            var count = ivp.GetGroupCount(axis);
            if(count > 0)
            {
                UInt32 filePosition = ivm.FilePosition;

                Int32 itemIndex = 0;

                result = new List<ItemViewModel>();
                for(int i = 0; i < count; i++)
                {
                    var itemCount = ivp.GetGroupItemCount(axis, itemIndex);
                    var container = GetSubContainer(i, ivm, itemIndex, itemCount);
                    var fileLength = GetContainerLength(container);
                    var child = new GroupItemViewModel(ivm, filePosition, fileLength, $"[{i}]", container);
                    result.Add(child);

                    filePosition += fileLength;
                    itemIndex += itemCount;
                }
            }
            return result;
        }


        public static (FieldInfo, Int32)GetFieldInfoAxis(ItemViewModel ivm)
        {
            Int32 axis = 0;
            for (; ; )
            {
                if (ivm is TTFItemViewModel)
                {
                    return (null, 0);
                }
                else if (ivm is FieldViewModel fvm)
                {
                    if (fvm.Name.First() == '[')
                    {
                        axis++;
                        ivm = ivm.Parent;
                    }
                    else
                    {
                        var fi = fvm.Parent.ValueType.GetField(fvm.Name);
                        return (fi, axis);
                    }
                }
                else
                    break;
            }
            return (null, axis);
        }

        public static string FieldBaseName(string name)
        {
            string result = null;

            if (name != null)
            {
                int index = name.IndexOf('[');
                if (index >= 0)
                    result = name.Substring(0, index);
                else
                {
                    index = name.IndexOf('(');
                    if (index >= 0)
                        result = name.Substring(0, index);
                    else
                        result = name;
                }
            }
            return result;
        }

        public static UInt32 FieldIndex(string name)
        {
            UInt32 result = UInt32.MaxValue;

            if (name != null)
            {
                int index0 = name.LastIndexOf('[');
                int index1 = name.LastIndexOf(']');
                if (index0 >= 0)
                {
                    string sub = name.Substring(index0 + 1, index1 - index0 - 1);
                    result = UInt32.Parse(sub);
                }
                else
                {
                    index0 = name.LastIndexOf('(');
                    index1 = name.LastIndexOf(')');
                    if (index0 >= 0)
                    {
                        string sub = name.Substring(index0 + 1, index1 - index0 - 1);
                        result = UInt32.Parse(sub);
                    }
                }
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
            if(pos > 0)
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


        public static List<ItemViewModel> CreateModelValues(ItemViewModel ivm, object obj, TableModel model)
        {
            List<ItemViewModel> result = null;
            bool shouldSort = false;

            if (obj != null)
            {
                result = new List<ItemViewModel>();

                UInt32 filePosition = ivm.FilePosition;

                var fis = TypeHelper.B2DFieldInfos(obj.GetType());
                foreach (FieldInfo fi in fis)
                {
                    var attr = AttributeHelper.GetAttribute<PositionAttribute>(fi);
                    if (attr != null && attr.MethodName == null)
                    {
                        shouldSort = true;
                        foreach (var item in EnumItem(attr.Axis, fi.Name, fi, fi.GetValue(obj)))
                        {
                            var position = item.Item2 != null ? ViewModelHelper.GetTopFieldPosition(obj, ivm.FilePosition, item.Item1) : null;
                            if (position != null)
                            {
                                var name = item.Item1.Replace("\\", "");
                                var child = new CFFItemViewModel(name, item.Item2, ivm, position != null ? (UInt32)position : filePosition);
                                filePosition += child.FileLength;
                                result.Add(child);
                            }
                        }
                    }
                    else
                    {
                        object fieldValue = fi.GetValue(obj);
                        if (fieldValue != null)
                        {
                            var child = new FieldViewModel(fi.Name, fieldValue, ivm, filePosition);
                            filePosition += child.FileLength;
                            result.Add(child);
                        }
                    }
                }
            }
            if(shouldSort)
            {
                result.Sort((lhs, rhs) => lhs.FilePosition.CompareTo(rhs.FilePosition));
            }
            return result;
        }

        public static IEnumerable<(string, object)> EnumItem(Int32 axis, string path, FieldInfo fi, object obj)
        {
            if (axis == 0)
                yield return (path, obj);

            else if (axis > 0 && obj is IList list)
            {
                var index = 0;
                foreach (var child in list)
                {
                    var ret = EnumItem(axis - 1, $"{path}\\[{index++}]", fi, child);
                    foreach (var i in ret)
                        yield return i;
                }
            }
            yield break;
        }


        public static List<ItemViewModel> CreateFieldChildren(ItemViewModel ivm, object obj)
        {
            List<ItemViewModel> result = null;
            if (obj != null)
            {
                UInt32 filePosition = ivm.FilePosition;
                result = new List<ItemViewModel>();
                if (obj is Array array)
                {
                    string fieldName = "";
                    for (int i = 0; i < array.Length; i++)
                    {
                        object elementValue = array.GetValue(i);
                        string name = $"{fieldName}[{i}]";
                        result.Add(new FieldViewModel(elementValue?.GetType(), name, elementValue, ivm, filePosition));
                        filePosition += TypeHelper.CalcSize(elementValue);
                    }
                    return result;
                }
                else if (obj is ICollection ic)
                {
                    Type icType = obj.GetType();
                    Type elementType = icType.GenericTypeArguments[0];
                    
                    UInt32 cnt = 0;
                    foreach (object i in ic)
                    {
                        string name = $"[{cnt++}]";
                        var child = new FieldViewModel(elementType, name, i, ivm, filePosition);
                        filePosition += child.FileLength;
                        result.Add(child);
                    }
                    return result;
                }

                Add(result, ivm, obj, obj.GetType(), ref filePosition);
            }
            return result;
        }


        static Type GetModelValueType(ItemViewModel ivm)
        {
            while (ivm.GetType() != typeof(TTFItemViewModel))
                ivm = ivm.Parent;
            return ivm.ValueType;
        }

        static void Add(List<ItemViewModel> list, ItemViewModel ivm, object obj, Type type, ref UInt32 filePosition)
        {
            var fis = TypeHelper.B2DFieldInfos(type);
            foreach (FieldInfo fi in fis)
            {
                if (fi.GetValue(obj) == null)
                {
                    if(AttributeHelper.GetAttribute<HiddenIfNullAttribute>(fi) != null)
                        continue;
                }

                Type fieldType = fi.FieldType;

                object fieldValue = fi.GetValue(obj);
                if (fieldValue != null)
                {
                    object fieldValue2 = fi.GetValue(obj);
                    if (fieldValue2 != null)
                    {
                        var child = new FieldViewModel(fi.Name, fieldValue2, ivm, filePosition);
                        filePosition += child.FileLength;
                        list.Add(child);
                    }
                }
                else
                {
                    var child = new FieldViewModel(fi.Name, null, ivm, filePosition);
                    list.Add(child);
                }
            }
        }


        class DataBuffer<T> where T:class
        {
            public T this[int index]
            {
                get => Get(index);
                set => Set(index, value);
            }


            class Block
            {
                public int Index;
                public T[] Data;
            }

            int BlockCount;
            int BlockSize;
            List<Block> BlockList;

            
            public DataBuffer(int blockCountShift, int blockSizeShift, int count)
            {
                int blockCount = 2 ^ blockCountShift;
                int blockSize = 2 ^ blockSizeShift;
                if(count <= blockCount * blockSize)
                {
                    BlockCount = 1;
                    BlockSize = count;
                    BlockList = new List<Block>(1)
                    {
                        new Block { Index = 0, Data = new T[count] },
                    };
                }
                else
                {
                    BlockCount = blockCount;
                    BlockSize = blockSize;
                    BlockList = new List<Block>(BlockCount);
                }
            }


            T Get(int index)
            {
                int blockIndex = index / BlockSize;
                Block block = FindBlock(blockIndex);
                if (block == null)
                    return null;
                else
                {
                    int pos = index - blockIndex * BlockSize;
                    return block.Data[pos];
                }
            }


            void Set(int index, T value)
            {
                int blockIndex = index / BlockSize;
                Block block = FindBlock(blockIndex);
                if (block != null)
                {
                    int pos = index - blockIndex * BlockSize;
                    block.Data[pos] = value;
                }
            }


            Block FindBlock(int blockIndex)
            {
                var ret = BlockList.Find(b => b.Index == blockIndex);
                if(ret == null)
                {
                    if(BlockList.Count < BlockCount)
                    {
                        ret = new Block { Index = blockIndex, Data = new T[BlockSize] };
                        BlockList.Insert(0, ret);
                        return ret;
                    }
                    else
                    {
                        int pos = BlockCount - 1;
                        ret = BlockList[pos];
                        BlockList.RemoveAt(pos);
                        ret.Index = blockIndex;
                        for(int i = 0; i < BlockSize; i++)
                            ret.Data[i] = null;
                        BlockList.Insert(0, ret);
                        return ret;
                    }
                }
                else
                {
                    BlockList.Remove(ret);
                    BlockList.Insert(0, ret);
                    return ret;
                }
            }
        }


        class VirtualizingListGenerator : VirtualizingList<ItemViewModel>.IItemGenerator
        {
            ItemViewModel Parent;
            IElementList IEL;
            ItemViewModel[] IVMList;

            public VirtualizingListGenerator(ItemViewModel parent, IElementList iel)
            {
                Parent = parent;
                IEL = iel;
                IVMList = new ItemViewModel[GetCount()];
            }


            public int GetCount()
            {
                return IEL.GetCount();
            }


            public ItemViewModel GetItem(Int32 index)
            {
                ItemViewModel result = null;
                result = IVMList[index];
                if (result == null)
                {
                    result = new FieldViewModel(IEL, index, Parent);
                    IVMList[index] = result;
                }
                return result;
            }

            public void SetItem(int index, ItemViewModel value)
            {
                throw new NotImplementedException();
            }
        }


        public static IList<ItemViewModel> CreateLazyLoadChildren(ItemViewModel ivm, IElementList iel)
        {
            var generator = new VirtualizingListGenerator(ivm, iel);
            var result = new VirtualizingList<ItemViewModel>
            {
                ItemGenerator = generator,
            };
            return result;
        }


        static bool Find(ref List<ItemViewModel> path, ItemViewModel parent, UInt32 filePosition, int level)
        {
            bool result = false;
            var children = parent.ChildList;
            if (children == null || children.Count == 0)
            {
                result = parent.FilePosition <= filePosition && filePosition < parent.FilePosition + parent.FileLength;
            }
            else
            {
                foreach (ItemViewModel ivm in children)
                {
                    if (ivm.FilePosition <= filePosition && filePosition < ivm.FilePosition + ivm.FileLength)
                    {
                        if (typeof(ITTFPrimitive).IsAssignableFrom(ivm.ValueType))
                        {
                            result = true;
                            path.Insert(0, ivm);
                            break;
                        }
                        else
                        {
                            result = Find(ref path, ivm, filePosition, level + 1);
                            if (result)
                            {
                                path.Insert(0, ivm);
                                break;
                            }
                        }
                    }
                    else if (typeof(ITTFPrimitive).IsAssignableFrom(ivm.ValueType))
                    {
                        //Debug.WriteLine($"2 ILoadable {ivm.Text}");
                        if (ivm.ValueType == typeof(Offset32))
                        {
                            result = Find(ref path, ivm, filePosition, level + 1);
                            if (result)
                            {
                                path.Insert(0, ivm);
                                break;
                            }
                        }
                    }
                    else
                    {
                        result = Find(ref path, ivm, filePosition, level + 1);
                        if (result)
                        {
                            path.Insert(0, ivm);
                            break;
                        }
                    }

                }
            }
            return result;
        }

    }

#pragma warning restore IDE1006
}
