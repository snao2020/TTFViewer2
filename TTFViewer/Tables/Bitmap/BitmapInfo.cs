using System;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Tables
{
    class BitmapInfo
    {
        public BitmapSizeInfo[] BitmapSizeInfos { get; }

        public BitmapInfo(TableModel bitmapDataTableModel)
        {
            var bitmapDataTableType = bitmapDataTableModel.ValueType;
            if (bitmapDataTableType == typeof(EBDTTable_Version20))
                BitmapSizeInfos = CreateBitmapSizeInfos(TableModelHelper.GetFontTableModel(bitmapDataTableModel, "EBLC"), 2, 0);
            else if (bitmapDataTableType == typeof(CBDTTable_Version30))
                BitmapSizeInfos = CreateBitmapSizeInfos(TableModelHelper.GetFontTableModel(bitmapDataTableModel, "CBLC"), 3, 0);
        }


        static BitmapSizeInfo[] CreateBitmapSizeInfos(TableModel bitmapLocationTableModel, UInt16 majorVersion, UInt16 minorVersion)
        {
            BitmapSizeInfo[] result = null;

            if (bitmapLocationTableModel != null)
            {
                var filePosition = bitmapLocationTableModel.FilePosition;
                var reader = bitmapLocationTableModel.BinaryLoader.GetPrimitiveReader();
                if (reader.Read<uint16>(filePosition) == majorVersion
                    && reader.Read<uint16>(filePosition + 2) == minorVersion)
                {
                    filePosition += 4;
                    var numSizes = (Int32)(UInt32)reader.Read<uint32>(filePosition);
                    filePosition += 4;

                    result = Enumerable.Range(0, numSizes)
                        .Select(i => new BitmapSizeInfo(bitmapLocationTableModel, filePosition, i))
                        .ToArray();
                }
            }
            return result;
        }        
    }


    class BitmapSizeInfo
    {
        public IIndexSubTable[] IndexSubTables { get; }

        // subset of BitmapSize
        public UInt16 StartGlyphIndex { get; }
        public UInt16 EndGlyphIndex { get; }
        public Byte PPEMX { get; }
        public Byte PPEMY { get; }
        public Byte BitDepth { get; }


        public BitmapSizeInfo(TableModel tableModel, UInt32 filePosition, Int32 index)
        {
            filePosition += (UInt32)(index * Marshal.SizeOf(typeof(BitmapSize)));

            var reader = tableModel.BinaryLoader.GetPrimitiveReader();

            var bitmapSizeBlock = reader.DirectRead(filePosition + 4 * 4 + 2 * (UInt32)Marshal.SizeOf(typeof(SbitLineMetrics)), 7);
            Array.Reverse(bitmapSizeBlock, 0, 4);
            StartGlyphIndex = BitConverter.ToUInt16(bitmapSizeBlock, 2);
            EndGlyphIndex = BitConverter.ToUInt16(bitmapSizeBlock, 0);
            PPEMX = bitmapSizeBlock[4];
            PPEMY = bitmapSizeBlock[5];
            BitDepth = bitmapSizeBlock[6];

            var indexSubTableArrayOffset = reader.Read<Offset32>(filePosition);
            var indexSubTableArrayPosition = tableModel.FilePosition + indexSubTableArrayOffset;
            var numberOfIndexSubTables = reader.Read<uint32>(filePosition + 8);

            IndexSubTables = Enumerable.Range(0, (Int32)(UInt32)numberOfIndexSubTables)
                .Select(i => CreateIndexSubTable(reader, indexSubTableArrayPosition, i))
                .ToArray();
        }


        static IIndexSubTable CreateIndexSubTable(PrimitiveReader reader, UInt32 indexSubTableArrayPosition, Int32 index)
        {
            IIndexSubTable result = null;

            var indexSubtableRecordPosition = indexSubTableArrayPosition + (UInt32)(index * Marshal.SizeOf(typeof(IndexSubtableRecord)));
            var indexSubtableRecordBlock = reader.DirectRead(indexSubtableRecordPosition, 8);

            Array.Reverse(indexSubtableRecordBlock, 0, 4);
            var lastGlyphIndex = BitConverter.ToUInt16(indexSubtableRecordBlock, 0);
            var firstGlyphIndex = BitConverter.ToUInt16(indexSubtableRecordBlock, 2);

            Array.Reverse(indexSubtableRecordBlock, 4, 4);
            var indexSubtableOffset = BitConverter.ToUInt32(indexSubtableRecordBlock, 4);
            var indexSubTablePosition = indexSubTableArrayPosition + indexSubtableOffset;

            var indexFormat = reader.Read<uint16>(indexSubTablePosition);
            switch (indexFormat)
            {
                case 1:
                    result = new IndexSubTable_1(reader, firstGlyphIndex, lastGlyphIndex, indexSubTablePosition);
                    break;
                case 2:
                    result = new IndexSubTable_2(reader, firstGlyphIndex, lastGlyphIndex, indexSubTablePosition);
                    break;
                case 3:
                    result = new IndexSubTable_3(reader, firstGlyphIndex, lastGlyphIndex, indexSubTablePosition);
                    break;
                case 4:
                    result = new IndexSubTable_4(reader, firstGlyphIndex, lastGlyphIndex, indexSubTablePosition);
                    break;
                case 5:
                    result = new IndexSubTable_5(reader, firstGlyphIndex, lastGlyphIndex, indexSubTablePosition);
                    break;
            }
            return result;
        }
    }


    interface IIndexSubTable
    {
        (UInt16, UInt16) GlyphIndexRange { get; }
        Int32 ImageCount { get; }
        UInt16 ImageFormat { get; }

        UInt32 GetOffset(Int32 index);
        UInt32 GetLength(Int32 index);
        UInt16 GetGlyphId(Int32 index);
    }


    class IndexSubTable_1 : IIndexSubTable
    {
        public (UInt16, UInt16) GlyphIndexRange => (FirstGlyphIndex, LastGlyphIndex);
        public Int32 ImageCount => LastGlyphIndex - FirstGlyphIndex + 1;
        public UInt16 ImageFormat => InnerData.ImageFormat;

        public UInt32 GetOffset(Int32 index) => InnerData.GetOffset(index);
        public UInt32 GetLength(Int32 index) => InnerData.GetLength(index);
        public UInt16 GetGlyphId(Int32 index) => (UInt16)(FirstGlyphIndex + index);

        UInt16 FirstGlyphIndex;
        UInt16 LastGlyphIndex;

        InnerDataClass _InnerData;
        InnerDataClass InnerData => _InnerData ?? (_InnerData = CreateInnerData());

        class InnerDataClass
        {
            public UInt16 ImageFormat;
            public UInt32 ImageDataOffset;

            public UInt32 GetOffset(Int32 index) => ImageDataOffset + SBitOffsetFunc(index);
            public UInt32 GetLength(Int32 index) => SBitOffsetFunc(index + 1) - SBitOffsetFunc(index);

            public Func<Int32, UInt32> SBitOffsetFunc;
        }

        Func<InnerDataClass> CreateInnerData;

        public IndexSubTable_1(PrimitiveReader reader, UInt16 firstGlyphIndex, UInt16 lastGlyphIndex, UInt32 indexSubTablePosition)
        {
            FirstGlyphIndex = firstGlyphIndex;
            LastGlyphIndex = lastGlyphIndex;

            CreateInnerData = () => DoCreateInnerData(reader, indexSubTablePosition);
        }

        static InnerDataClass DoCreateInnerData(PrimitiveReader reader, UInt32 indexSubTablePosition)
        {
            return new InnerDataClass
            {
                ImageFormat = reader.Read<uint16>(indexSubTablePosition + 2),
                ImageDataOffset = reader.Read<uint32>(indexSubTablePosition + 4),
                SBitOffsetFunc = i => reader.Read<uint32>(indexSubTablePosition + 8 + (UInt32)i * 4),
            };
        }
    }


    class IndexSubTable_2 : IIndexSubTable
    {
        public (UInt16, UInt16) GlyphIndexRange => (FirstGlyphIndex, LastGlyphIndex);
        public Int32 ImageCount => LastGlyphIndex - FirstGlyphIndex + 1;
        public UInt16 ImageFormat => InnerData.ImageFormat;
        public UInt32 GetOffset(Int32 index) => InnerData.GetOffset(index);
        public UInt32 GetLength(Int32 index) => InnerData.ImageSize;
        public UInt16 GetGlyphId(Int32 index) => (UInt16)(FirstGlyphIndex + index);

        UInt16 FirstGlyphIndex;
        UInt16 LastGlyphIndex;

        InnerDataClass _InnerData;
        InnerDataClass InnerData => _InnerData ?? (_InnerData = CreateInnerData());

        class InnerDataClass
        {
            public UInt16 ImageFormat;
            public UInt32 ImageDataOffset;
            public UInt32 ImageSize;
            public UInt32 GetOffset(Int32 index) => ImageDataOffset + ImageSize * (UInt32)index;
        }

        Func<InnerDataClass> CreateInnerData;

        public IndexSubTable_2(PrimitiveReader reader, UInt16 firstGlyphIndex, UInt16 lastGlyphIndex, UInt32 indexSubTablePosition)
        {
            FirstGlyphIndex = firstGlyphIndex;
            LastGlyphIndex = lastGlyphIndex;

            CreateInnerData = () => DoCreateInnerData(reader, indexSubTablePosition);
        }


        static InnerDataClass DoCreateInnerData(PrimitiveReader reader, UInt32 indexSubTablePosition)
        {
            return new InnerDataClass
            {
                ImageFormat = reader.Read<uint16>(indexSubTablePosition + 2),
                ImageDataOffset = reader.Read<uint32>(indexSubTablePosition + 4),
                ImageSize = reader.Read<uint32>(indexSubTablePosition + 8),
            };
        }
    }


    class IndexSubTable_3 : IIndexSubTable
    {
        public (UInt16, UInt16) GlyphIndexRange => (FirstGlyphIndex, LastGlyphIndex);
        public Int32 ImageCount => LastGlyphIndex - FirstGlyphIndex + 1;
        public UInt16 ImageFormat => InnerData.ImageFormat;
        public UInt32 GetOffset(Int32 index) => InnerData.GetOffset(index);
        public UInt32 GetLength(Int32 index) => InnerData.GetLength(index);
        public UInt16 GetGlyphId(Int32 index) => (UInt16)(FirstGlyphIndex + index);

        UInt16 FirstGlyphIndex;
        UInt16 LastGlyphIndex;

        InnerDataClass _InnerData;
        InnerDataClass InnerData => _InnerData ?? (_InnerData = CreateInnerData());

        class InnerDataClass
        {
            public UInt16 ImageFormat;
            public UInt32 ImageDataOffset;

            public UInt32 GetOffset(Int32 index) => ImageDataOffset + SBitOffsetFunc(index);
            public UInt32 GetLength(Int32 index) => (UInt32)SBitOffsetFunc(index + 1) - (UInt32)SBitOffsetFunc(index);

            public Func<Int32, uint16> SBitOffsetFunc;
        }

        Func<InnerDataClass> CreateInnerData;

        public IndexSubTable_3(PrimitiveReader reader, UInt16 firstGlyphIndex, UInt16 lastGlyphIndex, UInt32 indexSubTablePosition)
        {
            FirstGlyphIndex = firstGlyphIndex;
            LastGlyphIndex = lastGlyphIndex;

            CreateInnerData = () => DoCreateInnerData(reader, indexSubTablePosition);
        }


        static InnerDataClass DoCreateInnerData(PrimitiveReader reader, UInt32 indexSubTablePosition)
        {
            return new InnerDataClass
            {
                ImageFormat = reader.Read<uint16>(indexSubTablePosition + 2),
                ImageDataOffset = reader.Read<uint32>(indexSubTablePosition + 4),
                SBitOffsetFunc = i => reader.Read<uint16>(indexSubTablePosition + 8 + (UInt32)i * 2),
            };
        }
    }


    class IndexSubTable_4 : IIndexSubTable
    {
        public (UInt16, UInt16) GlyphIndexRange => (FirstGlyphIndex, LastGlyphIndex);
        public Int32 ImageCount => (Int32)NumGlyphs;
        public UInt16 ImageFormat => InnerData.ImageFormat;

        public UInt32 GetOffset(Int32 index) => InnerData.GetOffset(index);
        public UInt32 GetLength(Int32 index) => InnerData.GetLength(index);
        public UInt16 GetGlyphId(Int32 index) => InnerData.GlyphIdFunc(index);

        UInt16 FirstGlyphIndex;
        UInt16 LastGlyphIndex;
        UInt32 NumGlyphs;

        InnerDataClass _InnerData;
        InnerDataClass InnerData => _InnerData ?? (_InnerData = CreateInnerData());

        class InnerDataClass
        {
            public UInt16 ImageFormat;
            public UInt32 ImageDataOffset;
            public UInt32 GetOffset(Int32 index) => ImageDataOffset + OffsetFunc(index);
            public UInt32 GetLength(Int32 index) => (UInt32)OffsetFunc(index + 1) - (UInt32)OffsetFunc(index);
            public Func<Int32, uint16> GlyphIdFunc;
            public Func<Int32, uint16> OffsetFunc;
        }

        Func<InnerDataClass> CreateInnerData;

        public IndexSubTable_4(PrimitiveReader reader, UInt16 firstGlyphIndex, UInt16 lastGlyphIndex, UInt32 indexSubTablePosition)
        {
            FirstGlyphIndex = firstGlyphIndex;
            LastGlyphIndex = lastGlyphIndex;
            NumGlyphs = reader.Read<uint32>(indexSubTablePosition + 8);

            CreateInnerData = () => DoCreateInnerData(reader, indexSubTablePosition);
        }


        static InnerDataClass DoCreateInnerData(PrimitiveReader reader, UInt32 indexSubTablePosition)
        {
            return new InnerDataClass
            {
                ImageFormat = reader.Read<uint16>(indexSubTablePosition + 2),
                ImageDataOffset = reader.Read<uint32>(indexSubTablePosition + 4),

                GlyphIdFunc = i => reader.Read<uint16>(indexSubTablePosition + 12 + (UInt32)(Marshal.SizeOf(typeof(GlyphIdOffsetPair)) * i)),
                OffsetFunc = i => reader.Read<uint16>(indexSubTablePosition + 12 + (UInt32)(Marshal.SizeOf(typeof(GlyphIdOffsetPair)) * i) + 2),
            };
        }
    }


    class IndexSubTable_5 : IIndexSubTable
    {
        public (UInt16, UInt16) GlyphIndexRange => (FirstGlyphIndex, LastGlyphIndex);
        public Int32 ImageCount => (Int32)NumGlyphs;
        public UInt16 ImageFormat => InnerData.ImageFormat;
        public UInt32 GetOffset(Int32 index) => InnerData.GetOffset(index);
        public UInt32 GetLength(Int32 index) => InnerData.ImageSize;
        public UInt16 GetGlyphId(Int32 index) => InnerData.GlyphIdFunc(index);

        UInt16 FirstGlyphIndex;
        UInt16 LastGlyphIndex;
        UInt32 NumGlyphs;

        InnerDataClass _InnerData;
        InnerDataClass InnerData => _InnerData ?? (_InnerData = CreateInnerData());

        class InnerDataClass
        {
            public UInt16 ImageFormat;
            public UInt32 ImageDataOffset;
            public UInt32 ImageSize;

            public UInt32 GetOffset(Int32 index) => ImageDataOffset + ImageSize * (UInt32)index;
            public Func<Int32, uint16> GlyphIdFunc;
        }

        Func<InnerDataClass> CreateInnerData;

        public IndexSubTable_5(PrimitiveReader reader, UInt16 firstGlyphIndex, UInt16 lastGlyphIndex, UInt32 indexSubTablePosition)
        {
            FirstGlyphIndex = firstGlyphIndex;
            LastGlyphIndex = lastGlyphIndex;
            NumGlyphs = reader.Read<uint32>(indexSubTablePosition + 12);

            CreateInnerData = () => DoCreateInnerData(reader, indexSubTablePosition);
        }

        static InnerDataClass DoCreateInnerData(PrimitiveReader reader, UInt32 indexSubTablePosition)
        {
            return new InnerDataClass
            {
                ImageFormat = reader.Read<uint16>(indexSubTablePosition + 2),
                ImageDataOffset = reader.Read<uint32>(indexSubTablePosition + 4),
                ImageSize = reader.Read<uint32>(indexSubTablePosition + 8),
                GlyphIdFunc = i => reader.Read<uint16>(indexSubTablePosition + 16 + (UInt32)i * 2),
            };
        }
    }
}
