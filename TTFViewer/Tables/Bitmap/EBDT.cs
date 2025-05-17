// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("EBDT")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "header\\majorVersion, header\\minorVersion", null)]
    [ClassTypeCondition(typeof(EBDTTable_Version20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0002, 0x0000")]
    [Invalid]
    [TypeName("EBDT — Embedded Bitmap Data Table")]
    [BaseName("EBDT")]
    class EBDTTable
    {
        [FieldName(0, null)]
        public EBDTHeader header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("EBDT — Embedded Bitmap Data Table")]
    [BaseName("EBDT")]
    class EBDTTable_Version20
    {
        [FieldName(0, null)]
        public EBDTHeader header;

        [ElementList(0, typeof(BitmapDataElementList), FieldValueKind.None, null, null)]
        [FieldName(0, null)]
        [ValueFormat(0, "bitmapDataText")]

        [Position(1, "bitmapDataPosition")]
        [TypeSelectAttribute("bitmapDataSelectMethod")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format1), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format2), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "2")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format3), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "3")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format4), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "4")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format5), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "5")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format6), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "6")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format7), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "7")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "8")]
        [TypeConditionAttribute(typeof(GlyphBitmapData_Format9), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "9")]
        [FieldName(1, FieldNameKind.Method, "bitmapDataItemName")]
        
        [GroupCountMethod(0, "BitmapSizeCount")]
        [GroupItemCountMethod(0, "BitmapSizeItemCount")]
        [GroupTextMethod(0, "BitmapSizeText")]
        [GroupDescription(0, "corresponding to EBLC.Header.BitmapSize")]
        
        [GroupCountMethod(1, "IndexSubTableCount")]
        [GroupItemCountMethod(1, "IndexSubTableItemCount")]
        [GroupTextMethod(1, "IndexSubTableText")]
        [GroupDescription(1, "corresponding to IndexSubTableArray")]
        
        public IList<GlyphBitmapData> bitmapData;

        static string bitmapDataText(IItemValueService ivs)
        {
            string result = null;
            if (ivs.Value is BitmapDataElementList el)
            {
                if(AttributeHelper.GetAttribute<GroupCountMethodAttribute>(typeof(EBDTTable_Version20).GetField("bitmapData"), 0) != null)
                    result = $"(EBLC Header.numSizes={el.GlyphBitmapDataManager.GetBitmapSizeCount()})";
            }
            return result;
        }


        static UInt32? bitmapDataPosition(IAttributeService service)
        {
            UInt32 result = 0;
            if (service.GetValues(FieldValueKind.Path, "..\\").SingleValue(0) is BitmapDataElementList el)
            {
                var index = TablePathHelper.GetLastIndex(service.Path);
                var bitmapManager = el.GlyphBitmapDataManager;
                if (bitmapManager.DivideBitmapDataIndex(index) is ValueTuple<Int32, Int32, Int32> indexes)
                {
                    var subTable = bitmapManager.GetSubTable(indexes.Item1, indexes.Item2);
                    var offset = subTable.GetOffset(indexes.Item3);
                    result = service.TableModel.FilePosition + offset;
                }
            }
            return result;
        }


        static object[] bitmapDataSelectMethod(IAttributeService service)
        {
            object[] result = null;
            if (service.GetValues(FieldValueKind.ElementList, null).SingleValue(0) is BitmapDataElementList el)
            {
                var bitmapManager = el.GlyphBitmapDataManager;
                var index = TablePathHelper.GetLastIndex(service.Path);
                if (bitmapManager.DivideBitmapDataIndex(index) is ValueTuple<Int32, Int32, Int32> indexes)
                {
                    var subTable = bitmapManager.GetSubTable(indexes.Item1, indexes.Item2);
                    result = new[] { (object)subTable.ImageFormat };
                }
            }
            return result;            
        }


        static string bitmapDataItemName(IItemValueService ivs)
        {
            string result = null;

            if (ivs.Parent.Value is BitmapDataElementList parent)
            {
                var bitmapManager = parent.GlyphBitmapDataManager;
                var firstIndex = ItemValueHelper.GetFirstIndex(ivs.GroupContainer);
                var index = firstIndex + TablePathHelper.GetLastIndex(ivs.Name);
                if (bitmapManager.DivideBitmapDataIndex(index) is ValueTuple<Int32, Int32, Int32> indexes)
                {
                    var subTable = bitmapManager.GetSubTable(indexes.Item1, indexes.Item2);
                    var glyphId = subTable.GetGlyphId(indexes.Item3);
                    result = $"[{index}](GlyphIndex={glyphId})";
                }
            }
            else
                result = $"{ivs.Name}";

            return result;
        }


        static Int32 BitmapSizeCount(IItemValueService parentIVS)
        {
            Int32 result = 0;
            if(parentIVS.Value is BitmapDataElementList el)
            {
                var bitmapManager = el.GlyphBitmapDataManager;
                result = bitmapManager.GetBitmapSizeCount();
            }
            return result;
        }


        static Int32 BitmapSizeItemCount(IItemValueService parentIVS, Int32 firstBitmapDataIndexInBitmapSize)
        {
            Int32 result = 0;

            if (parentIVS.Value is BitmapDataElementList el)
            {
                var bitmapManager = el.GlyphBitmapDataManager;
                if (bitmapManager.GetBitmapSizeIndex(firstBitmapDataIndexInBitmapSize) is Int32 bitmapSizeIndex)
                {
                    var bitmapDataRange = bitmapManager.GetBitmapDataRange(bitmapSizeIndex);
                    result = bitmapDataRange.Item2 - bitmapDataRange.Item1 + 1;
                }
            }
            return result;
        }


        static String BitmapSizeText(IItemValueService ivs)
        {
            String result = null;

            if (ivs.Parent.Value is BitmapDataElementList el)
            {
                Int32 bitmapSizeIndex = TablePathHelper.GetLastIndex(ivs.Name);
                var bitmapManager = el.GlyphBitmapDataManager;
                var bitmapDataRange = bitmapManager.GetBitmapDataRange(bitmapSizeIndex);
                var subTableCount = bitmapManager.GetSubTableCount(bitmapSizeIndex);
                var bitmapSizeInfo = bitmapManager.GetBitmapSize(bitmapSizeIndex);
                result = $"BitmapSize#{bitmapSizeIndex} GlyphIndex=[{bitmapSizeInfo.StartGlyphIndex},{bitmapSizeInfo.EndGlyphIndex}] imageSize={bitmapSizeInfo.PPEMX}x{bitmapSizeInfo.PPEMY} depth={bitmapSizeInfo.BitDepth} (BitmapDataRange=[{bitmapDataRange.Item1},{bitmapDataRange.Item2}],IndexSubTableCount={subTableCount})";
            }
            return result;
        }


        static Int32 IndexSubTableCount(IItemValueService parentIVS)
        {
            Int32 result = 0;
            if (parentIVS.Parent.Value is BitmapDataElementList el)
            {
                var bitmapManager = el.GlyphBitmapDataManager;
                var firstIndex = ItemValueHelper.GetFirstIndex(parentIVS.Value as IGroupContainer);
                if (bitmapManager.GetBitmapSizeIndex(firstIndex) is Int32 bitmapSizeIndex)
                {
                    result = bitmapManager.GetSubTableCount(bitmapSizeIndex);
                }
            }
            return result;
        }


        static Int32 IndexSubTableItemCount(IItemValueService parentIVS, Int32 imageIndex)
        {
            Int32 result = 0;

            if (parentIVS.Parent.Value is BitmapDataElementList el)
            {
                var bitmapManager = el.GlyphBitmapDataManager;
                var firstIndex = ItemValueHelper.GetFirstIndex(parentIVS.Value as IGroupContainer);
                if (bitmapManager.DivideBitmapDataIndex(firstIndex + imageIndex) is ValueTuple<Int32, Int32, Int32> indexes)
                {
                    var range = bitmapManager.GetBitmapDataRange(indexes.Item1, indexes.Item2);
                    result = range.Item2 - range.Item1 + 1;
                }
            }
            return result;
        }


        static String IndexSubTableText(IItemValueService ivs)
        {
            String result = null;

            if (ivs.Parent.Value is BitmapDataElementList el)
            {
                var bitmapManager = el.GlyphBitmapDataManager;
                var firstIndex = ItemValueHelper.GetFirstIndex(ivs.Value as IGroupContainer);
                if (bitmapManager.DivideBitmapDataIndex(firstIndex) is ValueTuple<Int32, Int32, Int32> indexes)
                {
                    var subTable = bitmapManager.GetSubTable(indexes.Item1, indexes.Item2);
                    var range = subTable.GlyphIndexRange;
                    result = $"IndexSubTable#{indexes.Item2} GlyphIndex=[{range.Item1},{range.Item2}] (BitmapDataRange=[{firstIndex},{firstIndex + subTable.ImageCount - 1}] Count={subTable.ImageCount})";
                }
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("EBDT Header")]
    [BaseName("EBDTHeader")]
    class EBDTHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the EBDT table, = 2.")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the EBDT table, = 0.")]
        public uint16 minorVersion;
    }
#pragma warning restore IDE1006
}