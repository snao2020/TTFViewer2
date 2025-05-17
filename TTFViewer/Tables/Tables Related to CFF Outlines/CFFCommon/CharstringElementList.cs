using System;
using System.Collections.Generic;
using System.Linq;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Tables
{
    class CharstringElementList : VirtualizingList<Charstring>, IElementList
    {
        public String BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        UInt32 FilePosition;
        IList<ITTFPrimitive> OffsetList;

        public SubrSelector SubrSelector { get; }

        public CharstringElementList(TableModel tableModel, String path, UInt32 filePosition, Type declaringType, object[] values)
        {
            BasePath = path;
            DeclaringType = declaringType;
            TableModel = tableModel;
            FilePosition = filePosition;
            if(values.SingleValue(0) is IList<ITTFPrimitive> offsetList)
                OffsetList = offsetList;
            if (values.SingleValue(1) is SubrSelector subrSelector)
                SubrSelector = subrSelector;

            var gen = new ElementListItemGenerator<Charstring>(this);
            ItemGenerator = gen;
        }

        public UInt32 GetFileLength()
        {
            if (OffsetList != null)
                return OffsetList.Last().ToNumber4() - OffsetList.First().ToNumber4();
            return 0;
        }

        public Int32 GetCount()
        {
            return OffsetList?.Count - 1 ?? 0;
        }

        public UInt32 GetElementPosition(Int32 index)
        {
            return FilePosition + (OffsetList?[index].ToNumber4() - 1 ?? 0);
        }

        public UInt32 GetElementLength(Int32 index)
        {
            if (OffsetList != null)
                return OffsetList[index + 1].ToNumber4() - OffsetList[index].ToNumber4();
            return 0;
        }

        public Type GetElementType(Int32 index)
        {
            return typeof(Charstring);
        }

        public object GetItem(Int32 index)
        {
            var result = TableModel.CreateObject(this, index);
            return result;
        }
    }
}
