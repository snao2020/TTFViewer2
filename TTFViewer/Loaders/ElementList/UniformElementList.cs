using System;
using System.Collections.Generic;
using TTFViewer.DataTypes;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.Loaders
{
    static class UniformElementListHelper
    {
        // types.Item2 may be ITTFPrimitive, while elementType is a concrete type
        public static object Create(Type leftElementType, TableModel tableModel, string path, UInt32 filePosition, Type declaringType, Int32 count, (Type, UInt32) elementTypeSize)
        {
            var t = typeof(UniformElementList<>).MakeGenericType(new[] { leftElementType });
            Object result = Activator.CreateInstance(t, new object[] { tableModel, path, filePosition, declaringType, count, elementTypeSize.Item1, elementTypeSize.Item2 });
            return result;
        }
    }

    // INDEX.offset
    // [UniformType(1)]
    // IList<ITTFPrimitive> offset; // [count + 1] Offset array(from byte preceding object data)
    // ElementList<ITTFPrimitive> ElementType={ uint8 or uint16 or uint24 or uint32}
    class UniformElementList<T> : VirtualizingList<T>, IElementList
    {
        public string BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        UInt32 FilePosition;
        Int32 ElementCount;
        Type ElementType;
        UInt32 ElementLength;

        public UniformElementList(TableModel tableModel, string path, UInt32 filePosition, Type declaringType, Int32 count, Type elementType, UInt32 elementLength)
        {
            BasePath = path;
            DeclaringType = declaringType;

            TableModel = tableModel;
            FilePosition = filePosition;
            ElementCount = count;
            ElementType = elementType;
            ElementLength = elementLength;

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }

        public UInt32 GetFileLength()
        {
            return ((UInt32)ElementCount) * ElementLength;
        }

        public int GetCount()
        {
            return ElementCount;
        }

        public UInt32 GetElementPosition(Int32 index)
        {
            return FilePosition + ElementLength * (UInt32)index;
        }

        public UInt32 GetElementLength(Int32 index)
        {
            return ElementLength;
        }

        public Type GetElementType(Int32 index)
        {
            return ElementType;
        }

        public object GetItem(int index)
        {
            if (typeof(ITTFPrimitive).IsAssignableFrom(ElementType))
                //return TableModel.BinaryLoader.CreatePrimitive(ElementType, GetElementPosition(index));
                return TableModel.BinaryLoader.GetPrimitiveReader().Read(ElementType, GetElementPosition(index));
            object result = TableModel.CreateObject(this, index);
            return result;
        }
    }
}
