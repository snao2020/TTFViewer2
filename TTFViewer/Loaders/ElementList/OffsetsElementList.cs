using System;
using System.Collections.Generic;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class OffsetsContainerHelper
    {
        public static object Create(Type leftElementType, TableModel tableModel, string path, Type declaringType,
            object positionList, UInt32 basePosition, string option, UInt32? fileLength)
        {
            var type = positionList.GetType();
            var iface = type.GetInterface("System.Collections.Generic.IList`1");
            if (iface != null)
            {
                var defs = iface.GenericTypeArguments;
                if (defs.Length == 1)
                {
                    var t = typeof(OffsetsElementList<,>);
                    var t2 = t.MakeGenericType(new[] { leftElementType, defs[0] });
                    var args = new object[] { tableModel, path, declaringType, positionList, basePosition, option, fileLength };
                    var result = Activator.CreateInstance(t2, args);
                    return result;
                }
            }
            return null;
        }
        

        public static object Create(Type leftElementType, TableModel tableModel, string path, Type declaringType, 
            Func<IList<UInt32>> positionsFunc, UInt32? fileLength)
        {
            var t = typeof(OffsetsElementList<,>);
            var t2 = t.MakeGenericType(new[] { leftElementType, typeof(UInt32) });
            var args = new object[] { tableModel, path, declaringType, positionsFunc, fileLength };
            var result = Activator.CreateInstance(t2, args);
            return result;
        }
    }


    class OffsetsElementList<T, T2> : VirtualizingList<T>, IElementList
    {
        public string BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        UInt32? FileLength;

        Lazy<IList<T2>> OffsetList;
        Func<IList<T2>, Int32, UInt32> OffsetToPositionFunc;
        
        public OffsetsElementList(TableModel tableModel, string path, Type declareingType,
            IList<T2> offsetList, UInt32 basePosition, string option, UInt32? fileLength)
        {
            BasePath = path;
            DeclaringType = declareingType;

            TableModel = tableModel;
            FileLength = fileLength;
            OffsetList = new Lazy<IList<T2>>(() => offsetList);
            OffsetToPositionFunc = (list, index) => AttributeHelper.ProcessMath(list[index], option).ToNumber4() + basePosition;

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }
        
        public OffsetsElementList(TableModel tableModel, string path, Type declareingType,
            Func<IList<T2>> positionsFunc, UInt32? fileLength)
        {
            BasePath = path;
            DeclaringType = declareingType;

            TableModel = tableModel;
            FileLength = fileLength;
            OffsetList = new Lazy<IList<T2>>(positionsFunc);
            OffsetToPositionFunc = (list, index) => list[index].ToNumber4();

            var gen = new ElementListItemGenerator<T>(this);
            ItemGenerator = gen;
        }

        public uint GetFileLength()
        {
            if (FileLength == null)
            {
                var endIndex = OffsetList.Value.Count - 1;
                FileLength = OffsetToPositionFunc(OffsetList.Value, endIndex)
                    - OffsetToPositionFunc(OffsetList.Value, 0);
            }
            return (UInt32)FileLength;
        }

        public int GetCount()
        {
            var result =  OffsetList.Value.Count - 1;
            return result;
        }


        public uint GetElementPosition(int index)
        {
            UInt32 result = OffsetToPositionFunc(OffsetList.Value, index);
            return result;
        }

        public uint GetElementLength(int index)
        {
            return OffsetToPositionFunc(OffsetList.Value, index + 1)
                - OffsetToPositionFunc(OffsetList.Value, index);
        }

        public Type GetElementType(Int32 index)
        {
            return typeof(T);
        }
        
        public object GetItem(int index)
        {
            var len = GetElementLength(index);
            if (len == 0)
                return null;
            var result = TableModel.CreateObject(this, index);
            return result;
        }
    }
}
