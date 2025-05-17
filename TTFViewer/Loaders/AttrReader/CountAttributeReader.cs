using System;
using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.Loaders
{
    class CountAttributeReader
    {
        TableLoader TableLoader;

        public CountAttributeReader(TableLoader tableLoader)
        {
            TableLoader = tableLoader;
        }


        public Object GetContainer(CountAttribute countAttribute, FieldInfo fieldInfo, Type leftElementType, Int32 axis)
        {
            object result = null;
            var countAttributeReader = new CountAttributeReader(TableLoader);
            if (countAttributeReader.GetCount(countAttribute, fieldInfo.DeclaringType) is Int32 elementCount && elementCount > 0)
            {
                if (IsPositionContainer(fieldInfo, axis))
                {
                    var valueType = typeof(DiscreteList<>);
                    var t = valueType.MakeGenericType(new[] { leftElementType });
                    var fp = TableLoader.GetFilePosition();
                    result = Activator.CreateInstance(t, new object[] { TableLoader.TableModel, TableLoader.GetFullPath(), fp, elementCount, fieldInfo.DeclaringType });
                }
                else if (TableLoadHelper.GetUniformTypeSize(TableLoader, fieldInfo, leftElementType, axis + 1) is ValueTuple<Type, UInt32> uniformTypeSize)
                {
                    if (uniformTypeSize.Item1 == typeof(ITTFPrimitive))
                        result = null;
                    else
                    {
                        result = UniformElementListHelper.Create(leftElementType, TableLoader.TableModel, TableLoader.GetFullPath(), TableLoader.GetFilePosition(), fieldInfo.DeclaringType, elementCount, uniformTypeSize);
                    }
                }
                else
                {
                    UInt32? containerLength = null;
                    var containerLengthAttr = fieldInfo != null ? AttributeHelper.GetAttribute<LengthAttribute>(fieldInfo, axis) : null;
                    if (containerLengthAttr != null)
                    {
                        var lengthReader = new LengthAttributeReader(TableLoader);
                        containerLength = lengthReader.GetLength(containerLengthAttr, fieldInfo.DeclaringType);
                    }
                    result = VariableContainerHelper.Create(leftElementType, TableLoader.TableModel, TableLoader.GetFullPath(), TableLoader.GetFilePosition(), fieldInfo.DeclaringType, elementCount, containerLength);
                }
            }
            return result;

        }

        bool IsPositionContainer(FieldInfo fieldInfo, Int32 axis)
        {
            if (AttributeHelper.GetAttribute<PositionAttribute>(fieldInfo, axis + 1) != null)
            {
                return true;
            }
            else if (AttributeHelper.GetAttribute<CountAttribute>(fieldInfo, axis + 1) != null)
            {
                return IsPositionContainer(fieldInfo, axis + 1);
            }
            else
            {
                return false;
            }
        }

        Int32? GetCount(CountAttribute countAttribute, Type declaringType)
        {
            var reader = new FieldValueReader(TableLoader, declaringType);
            Int32? result = AttributeHelper2.GetValueT2<Int32>(reader, declaringType, countAttribute);
            return result;
        }
    }
}
