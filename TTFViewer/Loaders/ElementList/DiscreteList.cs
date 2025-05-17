using System;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class DiscreteList<T> : VirtualizingList<T>, IElementList
    {
        public string BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        UInt32 FilePosition;
        Int32 CountN;

        public DiscreteList(TableModel tableModel, string path, UInt32 filePosition, Int32 count, Type declaringType)
        {
            
            TableModel = tableModel;
            BasePath = path;
            FilePosition = filePosition;
            DeclaringType = declaringType;
            CountN = count;

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }


        public int GetCount()
        {
            return CountN;
        }

        public uint GetElementLength(int index)
        {
            return 0;
        }

        public uint GetElementPosition(int index)
        {
            return 0;
        }

        public Type GetElementType(int index)
        {
            return typeof(T);
        }

        public uint GetFileLength()
        {
            return 0;
        }


        public bool IsNull(int index)
        {
            return false;
        }

        public object GetItem(int index)
        {
            var result = TableModel.CreateObject(this, index);
            return result;
        }
    }
}
