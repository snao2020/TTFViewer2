using System;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class PathValueReader
    {
        TableModel TableModel;
        LoadItem2 LoadItem;
        bool LoadItemPointToDeclaringType;

        public PathValueReader(TableModel model, LoadItem2 loadItem)
        {
            TableModel = model;
            LoadItem = loadItem;
            LoadItemPointToDeclaringType = false;
        }

        public object GetValue(string path)
        {
            object result = null;         

            var ret = ProcessPath(path);
            if (ret.Item1 != null)
            {
                var debFP2 = ret.Item1.GetFullPath();
                var loader = TableModel.BinaryLoader.GetTableLoader(TableModel, ret.Item1);
                result = loader.GetValue2(ret.Item2);
            }
            else
            {
                var source = LoadItem != null ? LoadItem.GetFullPath() : "";
                throw new ArgumentException($"ArgumentException PathValueReader.GetValue path={path}, source={source}");
            }
            return result;
        }


        public (LoadItem2, string) ProcessPath(string targetPath)
        {
            if (targetPath.StartsWith("\\") || LoadItem == null)
            {
                LoadItem2 item = GetRoot();
                return (item, targetPath);
            }
            else
            {
                var sp = new SourcePosition(LoadItem);
                sp.MoveToFieldPosition();
                if(!LoadItemPointToDeclaringType)
                    sp.Up();
                for (; ; )
                {
                    if (targetPath.StartsWith(".\\"))
                    {
                        targetPath = targetPath.Substring(2);
                    }
                    else if (targetPath.StartsWith("..\\"))
                    {
                        targetPath = targetPath.Substring(3);
                        sp.Up();
                    }
                    else
                        break;
                }
                var result = sp.GetCurrent(TableModel, targetPath);
                return result;
            }
        }


        LoadItem2 GetRoot()
        {
            LoadItem2 p = LoadItem;
            if (p != null)
            {
                while (p.Parent != null)
                    p = p.Parent;
                if (p.ElementList == null)
                    return p;
            }
            return LoadItem2.CreateItem("", TableModel.FilePosition, TableModel.ValueType);//, i => TableLoadHelper.CreateModelObject(TableModel));
        }


        class SourcePosition
        {
            LoadItem2 LoadItem;
            string BasePath;

            public SourcePosition(LoadItem2 loadItem)
            {
                LoadItem = loadItem;
            }

            public void MoveToFieldPosition()
            {
                for (; ; )
                {
                    if (LoadItem != null)
                    {
                        if (LoadItem.Name.EndsWith("]"))
                            Up();
                        else
                            break;
                    }
                    else
                    {
                        if (BasePath.EndsWith("]"))
                            Up();
                        else
                            break;
                    }
                }
            }

            public void Up()
            {
                if (LoadItem != null)
                {
                    var parent = LoadItem.Parent;
                    if (parent == null)
                    {
                        BasePath = LoadItem.ElementList?.BasePath;
                    }
                    LoadItem = parent;
                }
                else
                {
                    BasePath = RemoveLastPath(BasePath);
                }
            }

            public (LoadItem2, string) GetCurrent(TableModel tableModel, string path)
            {
                if (LoadItem != null)
                {
                    return (LoadItem, path);
                }
                else if (BasePath != null)
                {
                    var item = LoadItem2.CreateItem("", tableModel.FilePosition, tableModel.ValueType);//, i => TableLoadHelper.CreateModelObject(tableModel));
                    path = $"{BasePath}\\{path}";
                    return (item, path);
                }
                else
                    return (null, null);
            }


            static string RemoveLastPath(string path)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var pos = path.LastIndexOf('\\');
                    if (pos >= 0)
                        return path.Substring(0, pos);
                    else
                        return string.Empty;
                }
                return null;
            }           
        }
    }
}
