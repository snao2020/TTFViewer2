using System;
using System.Collections.Generic;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Tables;

namespace TTFViewer.Loaders
{
    class LengthAttributeReader
    {
        TableLoader TableLoader;

        public LengthAttributeReader(TableLoader tableLoader)
        {
            TableLoader = tableLoader;
        }

        
        public object GetContainer(LengthAttribute lengthAttribute, FieldInfo fieldInfo, Type leftElementType, Int32 axis)
        {
            var length = GetLength(lengthAttribute, fieldInfo.DeclaringType);

            object result = null;

            if (TableLoadHelper.GetUniformTypeSize(TableLoader, fieldInfo, leftElementType, axis + 1) is ValueTuple<Type, UInt32> uniformTypeSize)
            {
                Int32 count = (Int32)(length / uniformTypeSize.Item2);
                result = UniformElementListHelper.Create(leftElementType, TableLoader.TableModel, TableLoader.GetFullPath(), TableLoader.GetFilePosition(), fieldInfo.DeclaringType, count, uniformTypeSize);
            }
            else
            {
                result = VariableContainerHelper.Create(leftElementType, TableLoader.TableModel, TableLoader.GetFullPath(), TableLoader.GetFilePosition(), fieldInfo.DeclaringType, length);
            }

            return result;
        }
        

        public UInt32 GetLength(LengthAttribute attr, Type declaringType)
        {
            UInt32 result = 0;

            if (attr != null)
            {
                if (attr.CFFDICTOperator is CFFDICTOperators cffDICTOperator)
                {
                    result = GetCFFDICTLength(attr, declaringType, cffDICTOperator, attr.OperandIndex);
                }
                else if (attr.CFF2DICTOperator is CFF2DICTOperators cff2DICTOperator)
                {
                    result = GetCFF2DICTLength(attr, declaringType, cff2DICTOperator, attr.OperandIndex);
                }
                else if (attr.MethodName != null)
                {
                    result = InvokeMethod(attr, declaringType);
                }
                else
                {
                    result = GetValue(attr, declaringType);
                }

            }
            return result;
        }


        UInt32 GetCFFDICTLength(LengthAttribute attr, Type declaringType, CFFDICTOperators op, Int32 operandIndex)
        {
            UInt32 result = 0;

            var reader = new FieldValueReader(TableLoader, declaringType);
            var values = reader.GetValues(attr.Value);
            if (values != null && values.Length == 1 && values[0] is IList<DICTData> dictDataList)
            {
                var operand = DICTHelper.GetOperand(dictDataList, op, operandIndex);
                if (operand != null)
                    result = (UInt32)DICTHelper.GetOperandInteger(operand);
                else // if oparator not found, return 0
                    result = 0;
            }
            else
                throw new ArgumentException();

            return result;
        }


        UInt32 GetCFF2DICTLength(LengthAttribute attr, Type declaringType, CFF2DICTOperators op, Int32 operandIndex)
        {
            UInt32 result = 0;
            
            var reader = new FieldValueReader(TableLoader, declaringType);
            var values = reader.GetValues(attr.Value);
            if (values != null && values.Length == 1 && values[0] is IList<DICTData> dictDataList)
            {
                var operand = DICTHelper.GetOperand(dictDataList, op, operandIndex);
                if (operand != null)
                    result = (UInt32)DICTHelper.GetOperandInteger(operand);
                else // if oparator not found, return 0
                    result = 0;
            }
            else
                throw new ArgumentException();

            return result;
        }


        UInt32 InvokeMethod(LengthAttribute attr, Type declaringType)
        {
            UInt32 result = 0;
            var reader = new FieldValueReader(TableLoader, declaringType);
            result = AttributeHelper2.GetValueT2<UInt32>(reader, declaringType, attr);
            return result;
        }


        UInt32 GetValue(LengthAttribute attr, Type declaringType)
        {
            UInt32 result = 0;

            var reader = new FieldValueReader(TableLoader, declaringType);
            var value = AttributeHelper2.GetValueT2<UInt32>(reader, declaringType, attr);
            if (value is UInt32 u32)
            {
                result = u32; // values[0].ToNumber4();
            }

            return result;
        }
    }
}
