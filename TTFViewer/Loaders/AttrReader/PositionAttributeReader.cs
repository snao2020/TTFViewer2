using System;
using System.Collections.Generic;
using TTFViewer.DataTypes;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.Loaders
{
    class PositionAttributeReader
    {
        public TableLoader TableLoader;

        public PositionAttributeReader(TableModel tableModel, LoadItem2 loadItem)
        {
            TableLoader = tableModel.BinaryLoader.GetTableLoader(tableModel, loadItem);
        }


        // if Operator not found, return null
        public UInt32? GetFilePosition(Type declaringType, PositionAttribute attr)
        {
            UInt32 result = 0;

            var reader = new FieldValueReader(TableLoader, declaringType);

            Int32? offs = null;
            if (attr.MethodName != null)
            {
                if (AttributeHelper.GetMethod(declaringType, attr.MethodName, typeof(PositionAttributeMethod)) is PositionAttributeMethod method)
                {
                    var service = new FieldAttributeService(TableLoader, declaringType);
                    return method(service);
                }
            }
            else
            {
                object[] values = AttributeHelper2.GetValueT2<object[]>(reader, declaringType, attr);
                offs = GetOffset(declaringType, attr, values);
            }

            if (offs == null)
                return null; // if Operator not found return null

            if (attr.BasePosition == null)
                result = (UInt32)offs;
            else
            {
                var s = attr.BasePosition.Trim();
                if (s.Contains("[]"))
                {
                    var indexes = TablePathHelper.GetIndexes(TableLoader.GetFullPath());
                    s = TablePathHelper.MergePathIndexes(s, indexes);
                }
                if(TableLoader.GetFilePositionNull(s) is UInt32 u32)
                    result = (UInt32)(u32 + offs);
            }
            return result;
        }


        public static Int32? GetOffset(Type declaringType, PositionAttribute attr, object[] values)
        {
            Int32? offs = null;

            if (values != null && values.Length == 1)
            {
                if (values[0] is IList<DICTData> dictDataList2)
                {
                    if (attr.CFFDICTOperator is CFFDICTOperators cffDICTOperator)
                    {
                        var operand = DICTHelper.GetOperand(dictDataList2, cffDICTOperator, attr.OperandIndex);
                        if (operand != null)
                        {
                            Int32 i32 = DICTHelper.GetOperandInteger(operand);
                            if (CanCreate(declaringType, attr, i32))
                            {
                                offs = i32;
                            }
                        }
                        else // if Operator not found return null;
                            return offs;
                    }
                    else if (attr.CFF2DICTOperator is CFF2DICTOperators cff2DICTOperator)
                    {
                        var operand = DICTHelper.GetOperand(dictDataList2, cff2DICTOperator, attr.OperandIndex);
                        if (operand != null)
                        {
                            Int32 i32 = DICTHelper.GetOperandInteger(operand);
                            if (CanCreate(declaringType, attr, i32))
                            {
                                offs = i32;
                            }
                        }
                        else // if Operator not found return null;
                            return offs;
                    }
                    else
                        throw new ArgumentException("PositionAttributeReader.GetOffset Operator type error");
                }
                else if(values[0] != null)
                {
                    offs = (Int32)values[0].ToNumber4();
                }
            }
            else
                throw new ArgumentException("PositionAttributeReader GetOffset value");

            return offs;
        }


        static bool CanCreate(Type declaringType, PositionAttribute attr, Int32? operand)
        {
            if (attr.CanCreate != null && operand is Int32 i32)
            {
                if (AttributeHelper.GetMethod(declaringType, attr.CanCreate, typeof(PositionAttribute.CanCreateMethod)) is PositionAttribute.CanCreateMethod method)
                {
                    bool result = method(i32);
                    return result;
                }

            }
            return true;
        }
    }
}