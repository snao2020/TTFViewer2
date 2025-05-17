using System;
using System.Diagnostics;
using System.Linq;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.Tables;

namespace TTFViewer.Model
{
    static class TableModelHelper
    {
        public static TableLoader GetTableLoader(this TableModel tableModel)
        {
            return tableModel.BinaryLoader.GetTableLoader(tableModel);
        }

        public static TableLoader GetTableLoader(this TableModel tableModel, LoadItem2 loadItem)
        {
            return tableModel.BinaryLoader.GetTableLoader(tableModel, loadItem);
        }


        public static UInt32 CalcFileLength(this TableModel model, IElementList iel, Int32 index)//, Type type, UInt32 filePosition)
        {
            var tableLoader = model.BinaryLoader.GetTableLoader(model, iel, index);
            return tableLoader.GetFileLength();
        }


        public static object CreateObject(this TableModel model, IElementList iel, Int32 index)
        {
            var tableLoader = model.BinaryLoader.GetTableLoader(model, iel, index);
            return tableLoader.GetValue("");
        }

        public static TableModel GetChildModel(TableModel parent, string pathString)
        {
            var loader = parent.BinaryLoader.GetTableLoader(parent, pathString);
            if (loader.GetValue2("").ToOffsetOrNull() is UInt32 offset)
            {
                var cp = GetChildParam(loader, offset);
                return new TableModel(parent, cp); //.Item1, cpf.Item2);           
            }
            else
                throw new ArgumentException($"TableModelHelper.GetChildModel path={pathString}");
        }


        static ChildParam GetChildParam(TableLoader parentLoader, UInt32 offset)
        {
            var reader = new TableAttributeReader(parentLoader);
            var parentModel = parentLoader.TableModel;

            if (reader.GetTypeFlags() is ValueTuple<Type, CreateModelFlags> tableTypeFlags)
            {
                if (reader.GetPosition(offset) is UInt32 filePosition)
                {
                    var lengthCount = reader.GetLengthOrCount();

                    var classReader = new ClassAttributeReader(parentModel, filePosition, tableTypeFlags.Item1);
                    tableTypeFlags.Item1 = classReader.GetValueType();
                    if (lengthCount.Item1 == null && lengthCount.Item2 == null)
                    {
                        var classReader2 = new ClassAttributeReader(parentModel, filePosition, tableTypeFlags.Item1);
                        lengthCount.Item1 = classReader2.GetLength();
                    }

                    return new ChildParam
                    {
                        Path = parentLoader.GetFullPath(),
                        FilePosition = filePosition,
                        FileLength = lengthCount.Item1,
                        ElementCount = lengthCount.Item2,
                        BaseType = tableTypeFlags.Item1,
                        CreateModelFlags = tableTypeFlags.Item2,
                    };
                }
                else // TableOffsetFlags.MayBeNull && offset == 0
                {
                    return new ChildParam
                    {
                        Path = parentLoader.GetFullPath(),
                        FilePosition = 0,   // invalid value
                        FileLength = null,     // invalid value
                        ElementCount = null,
                        BaseType = typeof(NullTable),
                        CreateModelFlags = tableTypeFlags.Item2 | CreateModelFlags.Invalid,
                    };
                }
            }
            else
            {
                return new ChildParam
                {
                    Path = parentLoader.GetFullPath(),
                    FilePosition = 0,
                    FileLength = 0,
                    ElementCount = null,
                    BaseType = typeof(ErrorTable),
                    CreateModelFlags = CreateModelFlags.Invalid,
                };
            }
        }


        public static TableModel GetFontTableModel(TableModel model, Tag tag)
        {
            TableModel result = null;

            while (model != null && model.ValueType != typeof(TableDirectory_Version10))
                model = model.Parent;

            if (model != null && model.GetValue() is TableDirectory_Version10 td)
            {
                var index = td.tableRecords.TakeWhile(tr => tr.tableTag != tag).Count();
                if (index < td.tableRecords.Length)
                {
                    var fi = typeof(TableDirectory_Version10).GetField("tableRecords");
                    string path = $"{fi.Name}\\[{index}]\\offset";
                    var child = GetChildModel(model, path);
                    if (child != null && child.CreateModelFlags.HasFlag(CreateModelFlags.Invalid))
                        child = null;
                    
                    result = child;
                }
            }

            return result;
        }


        public static object GetFontTableValue(TableModel model, Tag tag, string path)
        {
            object result = null;

            var child = GetFontTableModel(model, tag);
            if (child != null)
            {
                result = child.BinaryLoader.GetTableLoader(child).GetValue(path);
            }
            if (child == null || result == null)
                Debug.WriteLine($"GetFontTableValue tag={tag} path={path}");
            return result;
        }


        public static object CreateFontTable(TableModel model, Tag tag)
        {
            object result = null;

            var child = GetFontTableModel(model, tag);
            if (child != null)
            {
                result = child.GetValue();
            }
            return result;
        }
    }
}
