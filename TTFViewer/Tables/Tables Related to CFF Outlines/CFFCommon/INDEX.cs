// var 1.9.1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect("SelectClassMethod")]
    [ClassTypeCondition(typeof(INDEX_Valid<>), AttributeConditionKind.Equal, ClassValueKind.Enum, "TTFViewer.Tables.INDEXKind.Valid")]
    [ClassTypeCondition(typeof(INDEX_Error<>), AttributeConditionKind.Equal, ClassValueKind.Enum, "TTFViewer.Tables.INDEXKind.Error")]
    [ClassTypeCondition(null, AttributeConditionKind.Default, ClassValueKind.None, null)]
    [Invalid]
    [TypeName("INDEX")]
    [BaseName("INDEX")]
    abstract class INDEX<T>
    {
        [TypeSelectAttribute(FieldValueKind.FontTableType, null, null)]
        [TypeConditionAttribute(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.Type, "TTFViewer.Tables.CFFTable")]
        [TypeConditionAttribute(typeof(uint32), AttributeConditionKind.Equal, FieldValueKind.Type, "TTFViewer.Tables.CFF2Table")]
        [TypeName(TypeNameKind.Method, "CFFHelper.GetTypeName")]
        [Description(0, "Number of objects stored in INDEX")]
        public ITTFPrimitive count;
        
        [TypeSelectAttribute(FieldValueKind.Path, "count", null)]
        [TypeConditionAttribute(null,AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0")]
        [TypeConditionAttribute(typeof(uint8), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TypeName(TypeNameKind.Method, "offSizeTypeName")]
        [FieldName(0, FieldNameKind.Method, "offSizeFieldName")]
        [Description(0, "Offset array element size")]
        public uint8? offSize;

        [Count(0, FieldValueKind.Path, "count", "AddIfNonZero:1")]
        [UniformType(1)]
        [TypeSelectAttribute(FieldValueKind.Path, "offSize", null)]
        [TypeConditionAttribute(typeof(uint8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TypeConditionAttribute(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "2")]
        [TypeConditionAttribute(typeof(uint24), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "3")]
        [TypeConditionAttribute(typeof(uint32), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "4")]
        [TypeName(TypeNameKind.Method, "offsetTypeName")]
        [FieldName(0, FieldNameKind.Method, "offsetFieldName")]
        [ValueFormat(1, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "[count + 1] Offset array(from byte preceding object data)")]
        public IList<ITTFPrimitive> offset;

        static object[] SelectClassMethod(IAttributeService service)
        {
            if (service.GetValues(ClassValueKind.FontTableType, null).SingleValue(0) is Type fontTableType
                && CFFHelper.IsCFF(fontTableType) is bool isCFF)
            {
                var indexInfo = new INDEXInfo(isCFF, service.PrimitiveReader, service.FilePosition);
                if (indexInfo.Count == 0 || indexInfo.OffSize is UInt32)
                    return new object[] { INDEXKind.Valid };
                else
                    return new object[] { INDEXKind.Error };
            }
            return new object[] { INDEXKind.None };
        }

        static String offSizeTypeName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "uint8" : "OffSize";

        static String offSizeFieldName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "offsetSize" : "offSize";

        static String offsetTypeName(IItemValueService ivs)
        {
            String result = null;
            var tableType = ItemValueHelper.GetFontTableType(ivs);
            if (tableType == typeof(CFFTable))
                result = "Offset";
            else if (tableType == typeof(CFF2Table))
            {
                if (ivs.Value == null)
                {
                    result = "Offset8 or Offset16 or Offset24 or Offset32";
                }
                else
                {
                    Type type = null;

                    if (ivs.Value is UniformElementList<ITTFPrimitive> el)
                    {
                        type = el.GetElementType(0);
                    }
                    else if (ivs.Parent.Value is UniformElementList<ITTFPrimitive> el2)
                        type = el2.GetElementType(0);
                    switch (type)
                    {
                        case Type t when t == typeof(uint8): result = "Offset8"; break;
                        case Type t when t == typeof(uint16): result = "Offset16"; break;
                        case Type t when t == typeof(uint24): result = "Offset24"; break;
                        case Type t when t == typeof(uint32): result = "Offset32"; break;
                    }
                }
            }
            return result;
        }

        static String offsetFieldName(IItemValueService ivs)
            => ItemValueHelper.GetFontTableType(ivs) == typeof(CFF2Table)
            ? "offsets" : "offset";
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [TypeName("INDEX")]
    class INDEX_Error<T> : INDEX<T>
    {
        [HiddenIfNull]
        public T data;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("INDEX")]
    class INDEX_Valid<T> : INDEX<T>
    {
        [ElementList(0, "dataMethod")]
        [Length(1, FieldValueKind.ParentConstraint, null, null)]
        [TypeName(TypeNameKind.Method, "dataTypeName")]
        [Description(0, "[< varies >] Object data")]
        [Description(1, DescriptionKind.Method, "dataDescription")]
        public T data;

        static IElementList dataMethod(IAttributeService service)
        {
            IElementList result = null;

            var genericTypeArgument = service.DeclaringType.GenericTypeArguments[0];
            if (TypeHelper.GetElementType(genericTypeArgument) == typeof(Charstring))
            {
                var subrSelector = CreateSubrSelector(service);
                var values = new object[] { service.GetValues(FieldValueKind.Path, "offset").SingleValue(0), subrSelector };
                result = new CharstringElementList(service.TableModel, service.Path, service.FilePosition, service.DeclaringType, values);
            }
            else if (service.GetValues(FieldValueKind.Path, "offset").SingleValue(0) is IList<ITTFPrimitive> offset)
            {
                var elementType = TypeHelper.GetElementType(genericTypeArgument);
                UInt32 basePosition = service.FilePosition;
                result = OffsetsContainerHelper.Create(elementType, service.TableModel, service.Path, service.DeclaringType, offset, basePosition, "Sub:1", null) as IElementList;
            }
            return result;
        }

        static SubrSelector CreateSubrSelector(IAttributeService service)
        {
            SubrSelector result = null;

            if (CFFHelper.IsCFF(service.TableModel.ValueType) is bool isCFF)
            {
                FDSelect fdSelect = null;
                UInt32? globalSubrINDEXPosition = (UInt32?)service.GetValues(FieldValueKind.FilePositionNull, "\\GlobalSubrINDEX").SingleValue(0);
                UInt32?[] localSubrINDEXPositions = null;
                try  // eat all exceptions
                {
                    if (isCFF)
                    {
                        var fontIndex = TablePathHelper.GetFirstIndex(service.Path);
                        if (CFFHelper.IsCIDFont(service, fontIndex))
                        {
                            fdSelect = service.GetValues(FieldValueKind.Path, $"\\FDSelect\\[{fontIndex}]").SingleValue(0) as FDSelect;
                            var localSubrCount = (Int32)service.GetValues(FieldValueKind.Path, $"\\FontDICTINDEX\\[{fontIndex}]\\count").SingleValue(0).ToNumber4();
                            localSubrINDEXPositions = Enumerable.Range(0, localSubrCount)
                                .Select(i => (UInt32?)service.GetValues(FieldValueKind.FilePositionNull, $"\\CIDLocalSubrINDEX\\[{fontIndex}]\\[{i}]").SingleValue(0))
                                .ToArray();
                        }
                        else
                        {
                            localSubrINDEXPositions = new[] { (UInt32?)service.GetValues(FieldValueKind.FilePositionNull, $"\\LocalSubrINDEX\\[{fontIndex}]").SingleValue(0) };
                        }
                    }
                    else
                    {
                        fdSelect = service.GetValues(FieldValueKind.Path, $"\\FDSelect").SingleValue(0) as FDSelect;
                        var localSubrCount = (Int32)service.GetValues(FieldValueKind.Path, $"\\FontDICTINDEX\\count").SingleValue(0).ToNumber4();
                        localSubrINDEXPositions = Enumerable.Range(0, localSubrCount)
                            .Select(i => (UInt32?)service.GetValues(FieldValueKind.FilePositionNull, $"\\LocalSubrINDEX\\[{i}]").SingleValue(0))
                            .ToArray();
                    }
                }
                catch
                {
                    throw;
                }
                result = new SubrSelector(isCFF, fdSelect, globalSubrINDEXPosition, localSubrINDEXPositions);
            }
            return result;
        }

        static String dataTypeName(IItemValueService ivs)
            => TypeHelper.GetElementTypeRank(ivs.ValueType).Item1 == typeof(uint8)
                ? "Card8" : null;

        static String dataDescription(IItemValueService ivs)
            => TypeHelper.GetElementTypeRank(ivs.ValueType).Item1 == typeof(uint8)
            ? ItemValueHelper.AsciiDescription(ivs)
            : null;
    }

//------------------------------------------------------------------

    enum INDEXKind
    {
        None = 0,
        Error = 1,
        Valid = 2
    }
    
    class INDEXInfo
    {
        PrimitiveReader PrimitiveReader;

        public INDEXInfo(bool isCFF, PrimitiveReader reader, UInt32 indexPosition)
        {
            PrimitiveReader = reader;
            var filePosition = indexPosition;
            if(isCFF)
            {
                Count = PrimitiveReader.Read<uint16>(filePosition);
                filePosition += (UInt32)Marshal.SizeOf(typeof(uint16));
            }
            else
            {
                Count = (Int32)(UInt32)PrimitiveReader.Read<uint32>(filePosition);
                filePosition += (UInt32)Marshal.SizeOf(typeof(uint32));
            }
            if(Count > 0)
            {
                var offSize = PrimitiveReader.Read<uint8>(filePosition);
                filePosition += (UInt32)Marshal.SizeOf(typeof(uint8));

                if (offSize > 0 && offSize <= 4)
                {
                    OffSize = offSize;
                    OffsetPosition = filePosition;
                }
            }
        }

        public Int32 Count { get; }
        public UInt32? OffSize { get; }
        public UInt32? OffsetPosition { get; }

        public UInt32? GetOffset(Int32 index)
        {
            if(OffSize is UInt32 offSize && OffsetPosition is UInt32 offsetPosition)
            {
                switch(offSize)
                {
                    case 1: return PrimitiveReader.Read<uint8>(offsetPosition + (UInt32)(index * Marshal.SizeOf(typeof(uint8))));
                    case 2: return PrimitiveReader.Read<uint16>(offsetPosition + (UInt32)(index * Marshal.SizeOf(typeof(uint16))));
                    case 3: return PrimitiveReader.Read<uint16>(offsetPosition + (UInt32)(index * Marshal.SizeOf(typeof(uint24))));
                    case 4: return PrimitiveReader.Read<uint16>(offsetPosition + (UInt32)(index * Marshal.SizeOf(typeof(uint32))));
                }
            }
            return null;
        }

        public static UInt32? GetDataPosition(INDEXInfo indexInfo, Int32 index)
        {
            UInt32? result = null;
            if(indexInfo.GetOffset(index) is UInt32 offset)
            {
                result = (UInt32)indexInfo.OffsetPosition + (UInt32)((UInt32)indexInfo.OffSize * (indexInfo.Count + 1)) + offset - 1;
            }
            return result;
        }
    }

#pragma warning restore IDE1006
}
