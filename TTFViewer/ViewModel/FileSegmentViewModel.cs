using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.ViewModel
{
#if false
    class Info
    {
        public string PathString { get; }
        //private List<UInt32> Path { get; }
        public string Name { get; }
        public bool Primary { get; private set; }

        public Info(string pathString, string name)
        {
            PathString = pathString;
            //Path = path;
            Name = name;
        }


        public void SetPrimary(bool primary)
        {
            Primary = primary;
        }
    }


    class FileSegment
    {
        public UInt32 Start { get; private set; }
        public UInt32 End { get; private set; }

        //public List<List<UInt32>> Paths { get; private set; }
        //public List<string> Names { get; private set; }
        public List<Info> Infos { get; private set; }

        public FileSegment(UInt32 start, UInt32 end)
        {
            Start = start;
            End = end;
        }


        public void AddPath(string pathString, string name)
        {
            if (Infos == null)
                Infos = new List<Info>();
            Infos.Add(new Info(pathString, name));
            /*
            if (Paths == null)
                Paths = new List<List<uint>>();
            if (path != null)
                Paths.Add(path);
            Start = start;
            End = end;
            if (Names == null)
                Names = new List<string>();
            if (name != null)
                Names.Add(name);
            */
        }
        /*
        bool StartWith(List<UInt32> lhs, List<UInt32>rhs)
        {
            bool result = false;
            if(lhs.Count >= rhs.Count)
            {
                var sub = lhs.GetRange(0, rhs.Count);
                result = sub.SequenceEqual(rhs);
            }

            return result;
        }
        */
        public void UpdatePrimary()
        {            
            foreach (var i in Infos)
            {
                if (i != null)
                    i.SetPrimary(true);
            }
             
            //if (Infos.Count < 2)
            //    return;

            //int skip = 0;
            foreach (var i in Infos)
            {
                //DebugPath(i.Path);
                foreach (var j in Infos)//.Skip(++skip))
                {
                    //DebugPath(j.Path);
                    
                    //if(i != j && StartWith(i.Path, j.Path))
                    if(i != j && i.PathString.StartsWith(j.PathString))
                    {
                        j.SetPrimary(false);
                        break;
                    } 
                    
                }
            }
        }

#if false
        public void UpdatePrimary()
        {
            foreach (var i in Infos)
            {
                if (i != null)
                    i.SetPrimary(true);
            }

            if (Infos.Count < 2)
                return;

            foreach(var i in Infos)
            {
                if(i != null && i.Path != null && i.Path.Count > 1)
                {
                    var p = new List<UInt32>(i.Path);
                    p.RemoveAt(i.Path.Count - 1);
                    //DebugPath(i.Path);
                    //DebugPath(p);
                    foreach(var j in Infos)
                    {
                        if (j != null && j.Primary)
                        {
                            DebugPath(j.Path);
                            DebugPath(p);

                            if (p.SequenceEqual(j.Path))
                                j.SetPrimary(false);
                            /*
                            bool isEqual = true;
                            for(int k = 0; isEqual && k < p.Count; k++)
                            {
                                isEqual = p[k] == j.Path[k];
                            }
                            if (!isEqual)
                                j.SetPrimary(false);
                            */
                        }
                    }
                    //var sel = Infos.FindAll(info => info.Path.Equals(p));
                    //Debug.WriteLine("FindAll {0}",  sel != null ? sel.Count : 0);
                    //foreach (var j in Infos.FindAll(info => info.Path.Equals(p)))
                    //    j.SetPrimary(false);
                }
            }                     
        }
#endif
        static void DebugPath(List<UInt32> path)
        {
        }
    }


    class FileSegmentViewModel
    {
        class Item
        {
            public UInt32 Start;
            public UInt32 End;
            public string PathString;
            //public List<UInt32> Path;
            public string Name;

            public Item(UInt32 start, UInt32 length, string pathString, string name)
            {
                Start = start;
                End = start + length;
                PathString = pathString;
                //Path = path;
                Name = name;
            }
        }


        public UInt32[] Points { get; }
        public FileSegment[] Segments { get; }

#if false
        //void Add(TTFClassModel parent, List<Item> items, KeyValuePair<string, ChildParam> childFactory, string pathString) //, List<UInt32> path)
        void Add(TableModel parent, List<Item> items, ChildParam childParam, ChildParamFlags flags, string pathString) //, List<UInt32> path)
        {
            //TTFClassModel model = Activator.CreateInstance(childFactory.Value.Item1, childFactory.Value.Item2) as TTFClassModel;
            //TTFClassModel model = TTFClassHelper.CreateTTFClassModel(parent, childFactory.Value); // as TTFClassModel;
            //TTFClassModel model = new TTFClassModel(parent, childFactory.Value); // as TTFClassModel;
            //TTFClassModel model = TTFClassModel.CreateTTFClassModel(parent, childFactory); // as TTFClassModel;
            TableModel model = new TableModel(parent, childParam); //, CreateModelFlags.None); // as TTFClassModel;
            if (model != null)
            {
                //fieldPathChildPair.ChildModel;
                UInt32 filePosition = model.FilePosition;
                var valueType = model.ValueType;
#if false
            if (valueType != null && HiddenClassAttribute.IsDefined(valueType))
            {
//#if false
                //var childPairs = model.GetFieldPathChildPairs();
                object value = model.Value;
                //Debug.WriteLine($"Add-1 {value}");
                foreach (var element in TTFClassHelper.GetElements(value))
                {
                    Debug.WriteLine($"Add-2 {element.FieldName}");
//#if false
                    //Debug.WriteLine($"{filePosition:X8} {element.FieldType.Name} {element.FieldName}={element.Value}");
                    UInt32 length = TTFClassHelper.SizeOf(element.Value);
                    //var p = new List<UInt32>(path);
                    //p.AddRange(fieldPathChildPair.FieldPath);
                    //p.Add(filePosition);
                    items.Add(new Item(filePosition, length, fieldPathChildPair.PathString, element.FieldType.Name));
                    //Debug.WriteLine($"Add-4 {childPairs}");
                    var childPair = model.GetFieldPathChildPairs(i => i[0] == filePosition).FirstOrDefault();
                    if(childPair != null)
                    {
                        childPair.FieldPath.RemoveAt(0);
                        Add(items, childPair, pathString, p);
                    }
                    /*
                    foreach (var i in childPairs)
                    {
                        Debug.WriteLine($"Add-5 {i.Value}");
                        if (i.Key[0] == filePosition)
                        {
                            Debug.WriteLine("Add-7");
                            i.Key.RemoveAt(0);
                            Add(items, i, p);
                        }
                    }
                    */
                    filePosition += length;
                    //Debug.WriteLine("Add-8");
//#endif
                }
                //Debug.WriteLine("Add-9");
                /*
                foreach (var i in model.GetFieldPathChildPairs2())
                {
                    foreach (var p in i.Key)
                        Debug.Write($"{p:X8}\\");
                    Debug.WriteLine($" {i.Value.GetType().Name}");
                }
                */
//#endif
            }
            else            
#endif
                {
                    //var p = new List<UInt32>(path);
                    //p.AddRange(fieldPathChildPair.FieldPath);
                    //p.Add(filePosition);

                    //string name = valueType != null ? CommonNameAttribute.GetName(valueType) : model.GetType().Name;
                    string name;
                    if (valueType != null)
                    {
                        name = BaseNameAttribute.GetName(valueType);
                        if (HiddenClassAttribute.IsDefined(valueType))
                            name = "~" + name;
                    }
                    else
                        name = model.GetType().Name;

                    //string pathStr = string.Join("\\", new[] { pathString, childFactory.Key, name }.Where(s => !string.IsNullOrEmpty(s)));
                    string pathStr = string.Join("\\", new[] { pathString, childParam.Path, name }.Where(s => !string.IsNullOrEmpty(s)));
                    if (pathStr[1] == '\\' && pathStr[0] == '\\')
                        pathStr = pathStr.Remove(0, 1);

                    //items.Add(new Item(filePosition, model.FileLength, pathStr, name));
                    items.Add(new Item(filePosition, model.FileLength, pathStr, name));
                    /*
                    *** EnumChildParm --> EnumOffsets ***
                    foreach (var i in model.TableBuilder.EnumChildParam(model))
                    {
                        if (!i.Item2.HasFlag(ChildParamFlags.NoSegmentView))
                        {
                            Add(model, items, i.Item1, i.Item2, pathString);
                        }
                    }
                    */
                }
            }
        }
#endif

#if false
        void Add(List<Item> items, KeyValuePair<List<UInt32>, TTFItemModel> fieldPathChildPair, List<UInt32> path)
        {
            TTFItemModel model = fieldPathChildPair.Value;
            UInt32 filePosition = model.FilePosition;
            Type valueType = model.ValueType;
            /*
            if (valueType != null && HiddenClassAttribute.IsDefined(valueType))
            {
                object value = model.Value;
                foreach (var fi in TTFItemModelHelper.B2DFieldInfos(valueType))
                {
                    Type fieldType = fi.FieldType;
                    if (fi.GetValue(value) is Array array)
                    {
                        Type elementType = fieldType.GetElementType();
                        foreach(var element in array)
                        {
                            var p = new List<UInt32>(path);
                            p.AddRange(fieldPathChildPair.Key);

                            p.Add(filePosition);
                            UInt32 length = TTFItemModelHelper.SizeOf(element);
                            items.Add(new Item(filePosition, length, p, elementType.Name));
                            filePosition += length;
                            foreach (var i in model.GetFieldPathChildPairs2())
                                Add(items, i, p);
                        }

                    }
                    else
                    {
                        var p = new List<UInt32>(path);
                        p.AddRange(fieldPathChildPair.Key);

                        p.Add(filePosition);
                        UInt32 length = TTFItemModelHelper.SizeOf(fi.GetValue(value));
                        items.Add(new Item(filePosition, length, p, fieldType.Name));
                        filePosition += length;
                        foreach (var i in model.GetFieldPathChildPairs2())
                            Add(items, i, p);
                    }
                }
            }
            else
            */
            {
                var p = new List<UInt32>(path);
                p.AddRange(fieldPathChildPair.Key);

                p.Add(filePosition);
                items.Add(new Item(filePosition, model.FileLength, p, valueType != null ? valueType.Name : model.GetType().Name));
                foreach (var i in model.GetFieldPathChildPairs2())
                    Add(items, i, p);
            }
        }
#endif

        public FileSegmentViewModel(TableModel root)
        {
            var items = new List<Item>();
            if (root != null)
            {
                var path = new List<UInt32>();
                /*
                *** EnumChildParam --> EnumOffsets ***
                foreach (var i in root.TableBuilder.EnumChildParam(root))
                {
                    if (!i.Item2.HasFlag(ChildParamFlags.NoSegmentView))
                    {
                        //Debug.WriteLine($"FileSegmentViewModel-2 {i} {path.Count}");
                        //Add(root, items, i, ""); //, path);
                        Add(root, items, i.Item1, i.Item2, ""); //, path);
                        //Debug.WriteLine("FileSegmentViewModel-3");
                    }
                }
                */
                //Debug.WriteLine("FileSegmentViewModel-4");
                //foreach (var i in items)
                //    Debug.WriteLine($"{i.Start:X8} {i.End:X8} {i.Path.Count} {i.Name}");
                Points = CreatePoints(items);
                //foreach (var i in Points)
                //    Debug.WriteLine($"{i:X8}");
                Segments = CreateSegments(items, Points);

                int count = 0;
                foreach (var i in Segments)
                {
                    if (i != null)
                        i.UpdatePrimary();
                    //else
                    //    Debug.WriteLine($"empty {count}");
                    count++;
                }

            }
        }


        //public List<List<UInt32>> FindPath(UInt32 filePosition)
        public List<string> FindPath(UInt32 filePosition)
        {
            //List<List<UInt32>> result = null;
            List<string> result = null;

            int index = Array.BinarySearch(Points, filePosition);
            if (index < 0)
                index = ~index - 1;
            {
                FileSegment segment = Segments[index];
                if(segment != null && segment.Infos != null)
                {
                    //result = new List<List<UInt32>>();
                    result = new List<string>();
                    foreach(var i in segment.Infos)
                    {
                        if (i.Primary)
                            //result.Add(new List<UInt32>(i.Path));
                            result.Add(i.PathString);
                    }
                }
            }
            return result;
        }


        static UInt32[] CreatePoints(List<Item> items)
        {
            var list = new List<UInt32>();
            foreach (var i in items)
            {
                int index0 = list.BinarySearch(i.Start);
                if (index0 < 0)
                    list.Insert(~index0, i.Start);

                // file layout may have GAPs, therefore, checking End is needed.
                int index1 = list.BinarySearch(i.End);
                if (index1 < 0)
                    list.Insert(~index1, i.End);
            }
            return list.ToArray();
        }



        static FileSegment[] CreateSegments(List<Item> items, UInt32[] points)
        {
            var segments = new FileSegment[points.Length];
            foreach (var item in items)
            {
                int index = Array.BinarySearch(points, item.Start);
                for (int i = index; points[i] < item.End; i++)
                {
                    if (segments[i] == null)
                        segments[i] = new FileSegment(points[i], points[i+1]);
                    segments[i].AddPath(item.PathString, item.Name);
                }
                /*
                for (int i = index; points[i] < item.End; i++)
                {
                    if (segments[i] == null)
                        segments[i] = new FileSegment(item.Start, item.End);
                    segments[i].AddPath(item.PathString, item.Name);
                }
                */
            }
            return segments;
        }
    }
#endif
}
