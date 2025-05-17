using System;

namespace TTFViewer.DataTypes
{
    class GroupElementList<T> : VirtualizingList<T>, IElementList, IGroupContainer
    {
        public string BasePath => SourceList.BasePath;
        public Type DeclaringType => SourceList.DeclaringType;

        public IElementList SourceList { get; }
        public Int32 FirstIndex { get; }
        public Int32 ItemCount { get; }

        string Name;

        public GroupElementList(String name, IElementList sourceList, Int32 firstIndex, Int32 count)
        {
            Name = name;
            SourceList = sourceList;
            FirstIndex = firstIndex;
            ItemCount = count;

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }

        public uint GetFileLength()
        {
            return GetElementPosition(Count - 1) - GetElementPosition(0) + GetElementLength(ItemCount - 1);
        }

        public int GetCount()
        {
            return ItemCount;
        }

        public uint GetElementLength(int index)
        {
            return SourceList.GetElementLength(FirstIndex + index);
        }

        public uint GetElementPosition(int index)
        {
            return SourceList.GetElementPosition(FirstIndex + index);
        }

        public Type GetElementType(int index)
        {
            return SourceList.GetElementType(FirstIndex + index);
        }

        public object GetItem(int index)
        {
            return SourceList.GetItem(FirstIndex + index);
        }
    }
}
