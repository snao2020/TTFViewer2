using System;
using System.Collections.Generic;
using System.Linq;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    delegate UInt32 CalcElementLengthFunc(IElementList iel, Int32 index);

    static class VariableContainerHelper
    {
        public static object Create(Type leftElementType, TableModel tableModel, string path, UInt32 filePosition, Type declaringType, Int32 elementCount, UInt32? fileLength)
        {
            var t = typeof(VariableElementList<>).MakeGenericType(new[] { leftElementType });
            object result = Activator.CreateInstance(t, new object[] { tableModel, path, filePosition, declaringType, elementCount, fileLength});
            return result;
        }

        public static object Create(Type leftElementType, TableModel tableModel, string path, UInt32 filePosition, Type declaringType, UInt32 fileLength)
        {
            var t = typeof(VariableElementList<>).MakeGenericType(new[] { leftElementType });
            object result = Activator.CreateInstance(t, new object[] { tableModel, path, filePosition, declaringType, fileLength });
            return result;
        }
    }


    class VariableElementList<T> : VirtualizingList<T>, IElementList
    {
        public string BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        Int32? ElementCount;
        UInt32? FileLength;
        List<UInt32> Positions;
        Action<Int32?> ValidatePositionFunc;

        // count is valid, fileLength is option
        public VariableElementList(TableModel tableModel, string path, UInt32 filePosition, Type declaringType, Int32 elementCount, UInt32? fileLength)
        {
            BasePath = path;
            DeclaringType = declaringType;

            TableModel = tableModel;
            FileLength = fileLength;

            ElementCount = elementCount;
            Positions = new List<UInt32> { filePosition };
            //WhileFunc = i => true;
            ValidatePositionFunc = ValidatePositionByCount;

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }


        // no count
        public VariableElementList(TableModel tableModel, string path, UInt32 filePosition, Type declaringType, UInt32 fileLength)
        {
            BasePath = path;
            DeclaringType = declaringType;

            TableModel = tableModel;
            FileLength = fileLength;

            Positions = new List<UInt32> { filePosition };
            ValidatePositionFunc = ValidatePositionByLength;

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }


        public bool IsValid(Int32 index)
        {
            return index + 1 < Positions.Count;
        }


        public UInt32 GetFileLength()
        {
            if(FileLength == null)
            {
                ValidatePositionFunc(ElementCount);
                FileLength = Positions.Last() - Positions.First();
            }
            return (UInt32)FileLength;
        }


        public int GetCount()
        {
            if (ElementCount == null)
            {
                ValidatePositionFunc(null);
                return Positions.Count - 1;
            }
            else
                return (Int32)ElementCount;
        }


        public UInt32 GetElementPosition(Int32 index)
        {
            if (index == 0)
                return Positions[0];
            ValidatePositionFunc(index);
            var result = Positions[index];
            return result;
        }


        public UInt32 GetElementLength(Int32 index)
        {
            ValidatePositionFunc(index + 1);
            if (Positions.Count == 1)
                return 0;
            var result = Positions[index + 1] - Positions[index];
            return result;
        }


        public Type GetElementType(Int32 index)
        {
            return typeof(T);
        }

        public object GetItem(int index)
        {
            object result = TableModel.CreateObject(this, index);
            return result;
        }

        void ValidatePositionByCount(Int32? index)
        {
            for (int i = Positions.Count - 1; i < index; i++)
            {
                var len = TableModel.CalcFileLength(this, i);
                if (len == 0)
                    break;
                Positions.Add(Positions.Last() + len);
            }
        }

        void ValidatePositionByLength(Int32? index)
        {
            if (ElementCount == null)
            {
                if (Positions.Count != 1)
                    return;
                else
                    index = Int32.MaxValue;
            }
            Func<IElementList, bool> WhileFunc = i => i.GetElementPosition(i.GetCount()) - i.GetElementPosition(0) < FileLength;
            for (int i = Positions.Count - 1; i < index; i++)
            {
                var len = TableModel.CalcFileLength(this, i);
                if (len == 0)
                    break;
                Positions.Add(Positions.Last() + len);
                if(GetElementPosition(GetCount()) - GetElementPosition(0) >= FileLength)
                //if (!WhileFunc(this))
                    break;
            }
        }
    }
}

