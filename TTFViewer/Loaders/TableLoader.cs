using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class TableLoader
    {
        BinaryReader Reader;
        public TableModel TableModel { get; }
        LoadItem2 LoadItem { get; }

        public TableLoader(BinaryReader reader, TableModel tableModel)
        {
            Reader = reader;
            TableModel = tableModel;
            LoadItem = LoadItem2.CreateItem("", tableModel.FilePosition, tableModel.ValueType);//, i=>TableLoadHelper.CreateModelObject(tableModel));
        }

        public TableLoader(BinaryReader reader, TableModel tableModel, LoadItem2 loadItem)
        {
            Reader = reader;
            LoadItem = loadItem;
            TableModel = tableModel;
        }

        public TableLoader(BinaryReader reader, TableModel tableModel, IElementList iel, Int32 index)
        {
            Reader = reader;
            LoadItem = new LoadItem2(iel, index);
            TableModel = tableModel;
        }

        public TableLoader(BinaryReader reader, TableModel tableModel, string path)
        {
            Reader = reader;
            TableModel = tableModel;
            var loader = new TableLoader(reader, tableModel);
            LoadItem = loader.GetDescendent(path);
        }


        public string GetName()
        {
            return LoadItem.Name;
        }

        public string GetFullPath()
        {
            return LoadItem.GetFullPath();
        }


        public UInt32 GetFilePosition()
        {
            return LoadItem.FilePosition;
        }


        public UInt32? GetFilePositionNull(string path)
        {
            UInt32? result = null;

            var reader = new PathValueReader(TableModel, LoadItem);
            var ret = reader.ProcessPath(path);
            if (ret.Item1 != null)
            {
                var item = ret.Item1;
                var pathList = ret.Item2.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var fp = Reader.BaseStream.Position;
                while (item != null && pathList.Count > 0)
                {
                    item = item.GetChild(pathList.First(), Reader, TableModel);
                    pathList.RemoveAt(0);
                }

                if (item != null)
                {
                    if (item.ObjectType != null)
                        result = item.FilePosition;
                }
                else
                    throw new ArgumentException($"TableLoader.GetFilePosition2 path={path}");
            }
            return result;
        }


        public UInt32 GetFileLength()
        {
            return LoadItem.GetLength(Reader, TableModel);
        }


        public object GetValue(string path)
        {
            object result = null;
            var ret = TableLoadHelper.ProcessPath1(TableModel, LoadItem, path);
            if (ret.Item1 != null)
            {
                var fp = Reader.BaseStream.Position;
                result = ret.Item1.GGetValue(ret.Item2, Reader, TableModel);
                Reader.BaseStream.Position = fp;
            }
            else
                Debug.WriteLine($"Error TableLoader.GetValue {path}");

            return result;
        }

        public object GetValue2(string path)
        {
            object result = null;

            if (path.Contains(".\\"))
                throw new ArgumentException($"ArgumentException TableLoader.GetValue2 fullpath={LoadItem.GetFullPath()} path={path}");

            var fp = Reader.BaseStream.Position;
            result = LoadItem.GGetValue(path, Reader, TableModel);
            Reader.BaseStream.Position = fp;

            return result;
        }

        public LoadItem2 GetDescendent(string path)
        {
            var pathList = path.Split(new Char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var loadItem = LoadItem;
            foreach (var name in pathList)
            {
                loadItem = loadItem.GetChild(name, Reader, TableModel);
                if (loadItem == null)
                    break;
            }
            return loadItem;
        }

        public FieldInfo GetFieldInfo()
        {
            return LoadItemHelper.GetFieldInfo(LoadItem);
        }

        public string ResolvePathIndex(string path)
        {
            if (path != null)
            {
                path = path.Trim();
                if (path.Contains("[]"))
                {
                    var fvki = LoadItem.GetFieldValueKeyIndexes();
                    path = TablePathHelper.MergePathIndexes(path, fvki.Item2);
                }
            }
            return path;
        }

        public UInt32 GetBasePosition(string basePosition)
        {
            UInt32 result = 0;
            if (basePosition != null)
            {
                if (basePosition == string.Empty)
                    return LoadItem.FilePosition;
                var item = LoadItem;
                while (item.Parent != null)
                    item = item.Parent;

                var s = ResolvePathIndex(basePosition);
                var list = s.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var i in list)
                {
                    item = item.FindChild(i);
                    if (item == null)
                        break;
                }
                if (item != null)
                {
                    result = item.FilePosition;
                }
            }
            return result;
        }


        public object GetPathValue(string path)
        {
            var s = ResolvePathIndex(path);
            var valueReader = new PathValueReader(TableModel, LoadItem);
            object result = valueReader.GetValue(s);
            return result;
        }


        public IElementList GetElementList()
        {
            if (LoadItem != null)
            {
                if (LoadItem.ElementList != null)
                    return LoadItem.ElementList;
                else
                {
                    var loadItem = LoadItem.Parent;
                    if (loadItem.ElementList != null)
                        return loadItem.ElementList;
                }
            }
            return null;
        }


        public UInt32? GetParentConstraintLength()
        {
            var el = LoadItem.ElementList;
            if (el != null)
            {
                var index = TablePathHelper.GetLastIndex(LoadItem.GetFullPath());
                UInt32 len = el.GetElementLength(index);
                return len;
            }
            UInt32? result = GetTypeLength(TableModel, LoadItem);
            return result;
        }

        static UInt32? GetTypeLength(TableModel tableModel, LoadItem2 loadItem)
        {
            UInt32? result = null;

            var tableLoader = tableModel.BinaryLoader.GetTableLoader(tableModel, loadItem);
            UInt32 parentPosition;
            var parentItem = loadItem.Parent;
            if (parentItem == null)
            {
                result = tableModel.FileLength;
                parentPosition = tableModel.FilePosition;
            }
            else if (parentItem.ElementList != null
                    && !(parentItem.ElementList.GetType().IsGenericType
                        && parentItem.ElementList.GetType().GetGenericTypeDefinition() == typeof(DiscreteList<>)))
            {
                var index = TablePathHelper.GetLastIndex(parentItem.GetFullPath());
                result = parentItem.ElementList.GetElementLength(index);
                parentPosition = parentItem.ElementList.GetElementPosition(index);
            }
            else
            {
                var parentFi = LoadItemHelper.GetFieldInfo(parentItem);
                var parentAttr = DataTypes.AttributeHelper.GetAttribute<LengthAttribute>(parentFi);
                if (parentAttr != null)
                {
                    var parentLoader = tableModel.BinaryLoader.GetTableLoader(tableModel, parentItem);
                    var parentReader = new LengthAttributeReader(parentLoader);
                    result = parentReader.GetLength(parentAttr, parentFi.DeclaringType);
                    parentPosition = parentItem.FilePosition;
                }
                else
                {
                    var parentReader = new ClassAttributeReader(tableModel, parentItem.FilePosition, parentItem.ObjectType);
                    result = parentReader.GetLength();
                    parentPosition = parentItem.FilePosition;
                }
                if (result == null)
                {
                    result = GetTypeLength(tableModel, parentItem);
                }
            }
            if (result is UInt32 u32)
            {
                result = u32 - (loadItem.FilePosition - parentPosition);
            }
            return result;
        }
    }
}
