using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TTFViewer.Model;

namespace TTFViewer.ViewModel
{
    public class FileLayoutTree
    {
        public UInt32 Start { get; }
        public UInt32 End { get; }
        public string Name { get; }
        public List<List<UInt32>> Paths { get; }

        public List<FileLayoutTree> Children { get; set; }
        public List<FileLayoutTree> Crossings { get; set; }


        public FileLayoutTree(UInt32 filePosition, UInt32 fileLength, string name, List<UInt32> path)
        {
            Start = filePosition;
            End = filePosition + fileLength;
            Name = name;
            Paths = new List<List<UInt32>>
            {
                path,
            };
        }


        public void AddChild(FileLayoutTree item)
        {
            if (Children != null)
            {
                int index = Children.BinarySearch(item, new CompareInterval());
                if (index >= 0)
                {
                    var dest = Children[index];
                    if(dest.Start == item.Start && dest.End == item.End)
                    {
                        dest.Paths.Add(item.Paths[0]);
                    }
                    else if(dest.Start < item.Start)
                    {
                        if(dest.End < item.End) // crossing
                        {
                            AddCrossing(item);
                        }
                        else   // dest contains item
                        {
                            dest.AddChild(item);
                        }
                    }
                    
                    else
                    {
                        if(dest.End < item.End) // item contains dest
                        {
                            /*
                            item.AddChild(dest);
                            Children.Remove(dest);
                            Children.Insert(index, item);
                            while(Children[index + 1] != null && 
                            */
                        }
                        else    //crossing
                        {
                            AddCrossing(item);
                        }
                    }
                }
                else // not found
                {
                    Children.Insert(~index, item);
                }
            }              
            else // first time
            {
                Children = new List<FileLayoutTree> { item, };
            }
        }

        void AddCrossing(FileLayoutTree item)
        {

        }
        /*
        FileLayoutTree Find(UInt32 filePosition)
        {
            FileLayoutTree result = null;
            if(Children != null)
            {
                int pos = Children.BinarySearch();
                if(pos >= 0)
                {
                    result = Children[pos].Find(filePosition);
                    if (result == null;)
                        result = Children[pos];
                }
            }
            return result;
        }
        */

        class CompareInterval : IComparer<FileLayoutTree>
        {
            public int Compare(FileLayoutTree x, FileLayoutTree y)
            {
                if (x.End <= y.Start)
                    return -1;
                else
                {
                    if (x.Start < y.End)
                        return 0;
                    else
                        return 1;
                }
            }
        }
    }


    public class FileLayoutViewModel
    {
        public FileLayoutTree Root { get; }

        public FileLayoutViewModel(TTFItemModel root)
        {
            if(root != null)
            {
                Root = new FileLayoutTree(0, 0, null, null);
                foreach(var i in root.Children)
                    Add(i, null);
            }
        }


        bool Add(TTFItemModel child, List<UInt32> path)
        {
            //TTFItemModel child = itemModel.GetChild(index);
            bool result = child != null;
            if (result)
            {
                List<UInt32> p;
                if (path == null)
                    p = new List<UInt32>();
                else
                    p = new List<UInt32>(path);
                p.Add(child.FilePosition);
                //Debug.WriteLine($"path.Count={p.Count}");


                string name = child is ErrorItemModel eim ? eim.Message : child.ValueType != null ? MSDocsNameAttribute.GetName(child.ValueType) : string.Empty;
                var item = new FileLayoutTree(child.FilePosition, child.FileLength, name, p);
                Root.AddChild(item);
                /*
                var p = List.Find(i => i.FilePosition == item.FilePosition && i.FileLength == item.FileLength);
                if(p == null)
                {
                    p = item;
                    List.Add(item);
                }
                */
                //p.Paths.Add(path);
                /*
                for (int i = 0; ; i++)
                {
                    if (!Add(child, i, path))
                        break;
                }
                */
                foreach (var i in child.Children)
                    Add(i, p);
            }
            return result;
        }






#if false
        public FileLayoutViewModel(TTFItemModel root)
        {
            if (root != null)
            {
                Root = new FileLayoutTree(0, 0, null, null);
                Add(root, 0, null);
            }
        }


        void AddChildren(TTFItemModel itemModel, List<UInt32> path)
        {
            if (itemModel != null)
            {
                //if (itemModel.Parent == null)
                //    Add(itemModel, 0, path);
                //else
                {
                    UInt32 count = itemModel.ChildCount;
                    AddFields(itemModel.Value, itemModel, path, ref count);
                    if (count > 0)
                    {
                        Array assoc = itemModel.Associations;
                        if (assoc != null)
                        {
                            foreach (object obj in assoc)
                            {
                                AddFields(obj, itemModel, path, ref count);
                            }
                        }
                    }
                }
            }
        }

        void AddFields(object value, TTFItemModel itemModel, List<UInt32> path, ref UInt32 count)
        {
            if (value != null && count > 0)
            {
                if (value is Offset32 o32)
                {
                    count--;
                    Add(itemModel, o32, path);
                }
                else if(value is Offset16 o16)
                {
                    count--;
                    Add(itemModel, o16, path);
                }
                else if(value is Array array)
                {
                    foreach (var o in array)
                        AddFields(o, itemModel, path, ref count);

                }
                else 
                {
                    Type valueType = value.GetType();
                    if (valueType.IsClass)
                    { 
                        FieldInfo[] fis = valueType.GetFields(BindingFlags.Instance | BindingFlags.Public);
                        foreach (FieldInfo fi in fis)
                        {
                            AddFields(fi.GetValue(value), itemModel, path, ref count);
                        }
                    }
                }
            }
        }
        /*
        void AddFields(object value, TTFItemModel itemModel, List<UInt32> path, ref UInt32 count)
        {
            if (value != null)
            {
                FieldInfo[] fis = value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (FieldInfo fi in fis)
                {
                    if (count <= 0)
                        break;
                    Type fieldType = fi.FieldType;
                    if (fieldType == typeof(Offset32))
                    {
                        count--;
                        Add(itemModel, (Offset32)fi.GetValue(value), path);
                    }
                    else if (fieldType == typeof(Offset16))
                    {
                        count--;
                        Add(itemModel, (Offset16)fi.GetValue(value), path);
                    }
                    else if (fieldType.IsArray)
                    {
                        Array array = (Array)fi.GetValue(value);
                        Type elementType = fieldType.GetElementType();
                        for (int i = 0; i < array.Length; i++)
                        {
                            AddFields(array.GetValue(i), itemModel, path, ref count);
                            / *
                            if (elementType == typeof(Offset32))
                            {
                                count--;
                                Add(itemModel, (Offset32)array.GetValue(i), path);
                            }
                            else if (elementType == typeof(Offset16))
                            {
                                count--;
                                Add(itemModel, (Offset16)array.GetValue(i), path);
                            }
                            else if (fieldType.IsClass)
                            {
                                AddFields(array.GetValue(i), itemModel, path, ref count);
                            }
                            * /
                        }
                    }
                    else if (fieldType.IsClass)
                    {
                        AddFields(fi.GetValue(value), itemModel, path, ref count);
                    }
                }
            }
        }
        */

        void Add(TTFItemModel itemModel, UInt32 offset, List<UInt32>path)
        {
            TTFItemModel child = itemModel[offset];
            if (child != null)
            {
                List<UInt32> p;
                if (path == null)
                    p = new List<UInt32>();
                else
                    p = new List<UInt32>(path);
                p.Add(child.FilePosition);
                //Debug.WriteLine($"path.Count={p.Count}");
                 

                string name = child is ErrorItemModel eim ? eim.Message : child.ValueType != null ? MSDocsNameAttribute.GetName(child.ValueType) : string.Empty;
                var item = new FileLayoutTree(child.FilePosition, child.FileLength + TTFItemModelHelper.GetAssociationsLength(child), name, p);
                Root.AddChild(item);
                /*
                var p = List.Find(i => i.FilePosition == item.FilePosition && i.FileLength == item.FileLength);
                if(p == null)
                {
                    p = item;
                    List.Add(item);
                }
                */
                //p.Paths.Add(path);

                AddChildren(child, p);
            }
        }
#endif


#if false
        FileLayoutItem Find(UInt32 position)
        {
            FileLayoutItem result = null;
            foreach(var p in List.Where(i => i.FilePosition <= position && position < i.FilePosition + i.FileLength))
            {
                if (result == null || p.FileLength < result.FileLength)
                    result = p;
            }
            return result;
        }

        void SetPaths(TTFItemModel itemModel)
        {
            Type valueType = itemModel.ValueType;
            object value = itemModel.Value;
            AddPaths(value);
            Array associations = itemModel.Associations;
            if (associations != null)
                foreach (var obj in associations)
                    AddPaths(obj);
        }


        void AddPaths(object value)
        {
            /*
            if (value != null)
            {
                Type type = value.GetType();
                if (Tyoeof(Model.ILoadable).IsAssignable(type))
                {
                    
                }
                else if()
                FieldInfo[] infos = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (var fi in infos)
                {
                    
                }
            }
            */
        }
#endif
    }
}
