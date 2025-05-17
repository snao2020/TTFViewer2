using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class ValuesElementList<T> : VirtualizingList<T>, IElementList
    {
        public string BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        UInt32 FilePosition;
        UInt32 FileLength;

        public IList<T> Values => ValueArray.Value;
        Lazy<T[]> ValueArray = null;
        UInt32? ElementSize;
        Lazy<List<UInt32>> Positions = null;

        public ValuesElementList(TableModel tableModel, string path, UInt32 filePosition, Type declaringType, (UInt32, IEnumerable<T>) lengthValues)
        {
            BasePath = path;
            DeclaringType = declaringType;
            TableModel = tableModel;
            FilePosition = filePosition;
            FileLength = lengthValues.Item1;

            ValueArray = new Lazy<T[]>(() => CreateValueArray(lengthValues.Item2));
            Positions = new Lazy<List<UInt32>>(CreatePositions);

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }


        public ValuesElementList(TableModel tableModel, string path, UInt32 filePosition, Type declaringType, UInt32 length, IList<T> list)
        {
            BasePath = path;
            DeclaringType = declaringType;
            TableModel = tableModel;
            FilePosition = filePosition;
            FileLength = length; // lengthList.Item1;
            ValueArray = new Lazy<T[]>(() => CreateValueArray(list));// lengthList.Item2));

            // type: uint32,int32,uint16,int16,uint8,int8,
            if (typeof(T).GetInterface(typeof(ITTFPrimitive).FullName) != null)
                ElementSize = (UInt32)Marshal.SizeOf(typeof(T));
            else
                Positions = new Lazy<List<UInt32>>(CreatePositions);

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }


        T[] CreateValueArray(IEnumerable<T> enumerable)
        {
            T[] result = null;
            if (enumerable is T[] array)
                result = array;
            else if (enumerable != null)
                result = enumerable.ToArray();
            return result;
        }


        List<UInt32> CreatePositions()
        {
            if (ElementSize == null && Values != null)
            {
                var filePosition = FilePosition;
                var result = new List<UInt32> { filePosition };
                foreach (var value in Values)
                {
                    var len = TypeHelper.CalcSize(value);
                    filePosition += len;
                    result.Add(filePosition);
                }
                return result;
            }
            return null;
        }


        public UInt32 GetFileLength()
        {
            return (UInt32)FileLength;
        }

        public Int32 GetCount()
        {
            Int32 result = Values?.Count ?? 0;
            return result;
        }

        public UInt32 GetElementLength(int index)
        {
            UInt32 result;
            if (ElementSize is UInt32 size)
                result = size;
            else
                result = Positions.Value[index + 1] - Positions.Value[index];
            return result;
        }

        public UInt32 GetElementPosition(int index)
        {
            UInt32 result;
            if (ElementSize is UInt32 size)
                result = FilePosition + size * (UInt32)index;
            else
                result = Positions.Value[index];
            return result;
        }

        public Type GetElementType(int index)
        {
            return typeof(T);
        }

        public object GetItem(int index)
        {
            var positions = Positions?.Value;
            return Values[index];
        }
    }
}
