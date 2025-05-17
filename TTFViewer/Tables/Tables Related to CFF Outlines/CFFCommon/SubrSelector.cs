using System;
using System.Collections.Generic;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
    class SubrSelector
    {
        public bool IsCFF { get; }
        public FDSelect FDSelect { get; }
        public UInt32? GlobalSubrINDEXPosition { get; }
        public UInt32?[] LocalSubrINDEXPositions { get; }

        public SubrSelector(bool isCFF, FDSelect fdSelect, UInt32? globalSubrINDEXPosition, UInt32?[] localSubrINDEXPositions)
        {
            IsCFF = isCFF;
            FDSelect = fdSelect;
            GlobalSubrINDEXPosition = globalSubrINDEXPosition;
            LocalSubrINDEXPositions = localSubrINDEXPositions;
        }

        public IEnumerable<uint8> GetGlobalSubrReader(PrimitiveReader reader, Int32 subrNumber)
        {
            if(GetRange(reader, GlobalSubrINDEXPosition, subrNumber) is ValueTuple<UInt32,UInt32> range)
                return TokenHelper.GetEnumerable(reader, range.Item1, range.Item2);
            return null;
        }

        public IEnumerable<uint8> GetLocalSubrReader(PrimitiveReader reader, Int32 gid, Int32 subrNumber)
        {
            if (LocalSubrINDEXPositions != null
                && FDSelectHelper.GetFDIndex(FDSelect, gid) is Int32 index
                && index >= 0 && index < LocalSubrINDEXPositions.Length)
            {
                if (GetRange(reader, LocalSubrINDEXPositions[index], subrNumber) is ValueTuple<UInt32, UInt32> range)
                    return TokenHelper.GetEnumerable(reader, range.Item1, range.Item2);
            }
            return null;
        }

        (UInt32,UInt32)? GetRange(PrimitiveReader reader, UInt32? subrINDEXPosition, Int32 subrNumber)
        {
            if (subrINDEXPosition is UInt32 filePosition)
            {
                var indexInfo = new INDEXInfo(IsCFF, reader, filePosition);
                if (indexInfo.Count > 0)
                {
                    var index = CharstringHelper.GetSubrIndex(indexInfo.Count, subrNumber);
                    if(INDEXInfo.GetDataPosition(indexInfo, index) is UInt32 start
                        && INDEXInfo.GetDataPosition(indexInfo, index + 1) is UInt32 end)
                    {
                        return (start, end - start);
                    }
                }
            }
            return null;
        }
    }
}
