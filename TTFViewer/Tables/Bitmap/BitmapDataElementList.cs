using System;
using System.Collections.Generic;
using System.Linq;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006
    class GlyphBitmapDataManager
    {
        BitmapInfo BitmapInfo;

        List<Int32>[] BitmapDataPositions;

        List<Int32> BitmapSizeToPositions;


        public GlyphBitmapDataManager(TableModel bitmapLocationModel)
        {
            if (bitmapLocationModel != null)
            {
                BitmapInfo = new BitmapInfo(bitmapLocationModel);

                var bitmapSizeInfos = BitmapInfo.BitmapSizeInfos;

                Int32 sum = 0;
                BitmapDataPositions = BitmapInfo.BitmapSizeInfos
                    .Select(i => func(ref sum, i))
                    .ToArray();

                BitmapSizeToPositions = BitmapDataPositions
                    .Select(i => i.First())
                    .ToList();
                BitmapSizeToPositions.Add(sum);
            }
        }


        List<Int32> func(ref Int32 sum, BitmapSizeInfo bitmapSizeInfo)
        {
            Int32 sum2 = sum;

            var result = bitmapSizeInfo.IndexSubTables
                .Select(i => i.ImageCount)
                .Select(i => sum2 += i)
                .ToList();
            result.Insert(0, sum);

            sum = sum2;

            return result;
        }


        public Int32 GetBitmapSizeCount()
        {
            if (BitmapSizeToPositions != null)
                return BitmapSizeToPositions.Count - 1;
            return 0;
        }


        public Int32 GetSubTableCount(Int32 bitmapSizeIndex)
        {
            if (BitmapDataPositions != null && bitmapSizeIndex < GetBitmapSizeCount())
                return BitmapDataPositions[bitmapSizeIndex].Count - 1;
            return 0;
        }


        public Int32 GetBitmapDataCount()
        {
            if (BitmapSizeToPositions != null)
                return BitmapSizeToPositions.Last();
            return 0;
        }


        public (Int32, Int32) GetBitmapDataRange(Int32 bitmapSizeIndex)
        {
            if (BitmapDataPositions != null && bitmapSizeIndex < GetBitmapSizeCount())
            {
                var list = BitmapDataPositions[bitmapSizeIndex];
                return (list.First(), list.Last() - 1);
            }
            return (0, 0);
        }

        public (Int32, Int32) GetBitmapDataRange(Int32 bitmapSizeIndex, Int32 subTableIndex)
        {
            if (bitmapSizeIndex < GetBitmapSizeCount())
            {
                var list = BitmapDataPositions[bitmapSizeIndex];
                if (subTableIndex < list.Count - 1)
                {
                    var start = list[subTableIndex];
                    var end = list[subTableIndex + 1] - 1;
                    return (start, end);
                }
            }
            return (0, 0);
        }


        public Int32? GetBitmapSizeIndex(Int32 bitmapDataIndex)
        {
            Int32? result = null;
            if (bitmapDataIndex < GetBitmapDataCount())
            {
                result = BitmapSizeToPositions.BinarySearch(bitmapDataIndex);
                if (result < 0)
                    result = ~result - 1;
            }
            return result;
        }


        // return bitmapSizeIndex,subTableIndex,imageIndex
        public (Int32, Int32, Int32)? DivideBitmapDataIndex(Int32 bitmapDataIndex)
        {
            if (bitmapDataIndex < GetBitmapDataCount())
            {
                var bitmapSizeIndex = BitmapSizeToPositions.BinarySearch(bitmapDataIndex);
                if (bitmapSizeIndex < 0)
                    bitmapSizeIndex = ~bitmapSizeIndex - 1;

                var subTableIndexes = BitmapDataPositions[bitmapSizeIndex];
                var subTableIndex = subTableIndexes.BinarySearch(bitmapDataIndex);
                if (subTableIndex < 0)
                    subTableIndex = ~subTableIndex - 1;

                var firstIndex = subTableIndexes[subTableIndex];
                var imageIndex = bitmapDataIndex - firstIndex;

                return (bitmapSizeIndex, subTableIndex, imageIndex);
            }
            return null;
        }


        public BitmapSizeInfo GetBitmapSize(Int32 bitmapSizePosition)
        {
            return BitmapInfo?.BitmapSizeInfos[bitmapSizePosition];
        }


        public IIndexSubTable GetSubTable(Int32 bitmapSizePosition, Int32 subTablePosition)
        {
            return BitmapInfo?.BitmapSizeInfos[bitmapSizePosition].IndexSubTables[subTablePosition];
        }
    }


    class BitmapDataElementList : VirtualizingList<GlyphBitmapData>, IElementList
    {
        public string BasePath { get; }
        public Type DeclaringType { get; }

        TableModel TableModel;
        UInt32 FilePosition;

        GlyphBitmapDataManager _GlyphBitmapDataManager;
        public GlyphBitmapDataManager GlyphBitmapDataManager
            => _GlyphBitmapDataManager ?? (_GlyphBitmapDataManager = new GlyphBitmapDataManager(TableModel));

        public BitmapDataElementList(TableModel tableModel, string path, UInt32 filePosition, Type declaringType, Object[] values)
        {
            BasePath = path;
            DeclaringType = declaringType;
            TableModel = tableModel;
            FilePosition = filePosition;

            var gen = new ElementListItemGenerator<GlyphBitmapData>(this);
            ItemGenerator = gen;
        }

        public UInt32 GetFileLength()
        {
            UInt32 result = 0;
            if (TableModel.FileLength is UInt32 u32)
                result = u32 - (FilePosition - TableModel.FilePosition);
            return result;
        }

        public Int32 GetCount()
        {
            var result = GlyphBitmapDataManager.GetBitmapDataCount();
            return result;
        }

        public uint GetElementLength(int index)
        {
            if (GlyphBitmapDataManager.DivideBitmapDataIndex(index) is ValueTuple<Int32, Int32, Int32> ret)
            {
                var subTable = GlyphBitmapDataManager.GetSubTable(ret.Item1, ret.Item2);
                var result = subTable.GetLength(ret.Item3);
                return result;
            }
            return 0;
        }

        public uint GetElementPosition(int index)
        {
            if (GlyphBitmapDataManager.DivideBitmapDataIndex(index) is ValueTuple<Int32, Int32, Int32> ret)
            {
                var subTable = GlyphBitmapDataManager.GetSubTable(ret.Item1, ret.Item2);
                var result = subTable.GetOffset(ret.Item3) + TableModel.FilePosition;
                return result;
            }
            return 0;
        }

        public Type GetElementType(int index)
        {
            return typeof(GlyphBitmapData);
        }

        public object GetItem(int index)
        {
            return TableModel.CreateObject(this, index);
        }
    }
#pragma warning restore IDE1006
}
