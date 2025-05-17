using System;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class ContainerReader
    {
        TableLoader TableLoader;
        FieldInfo FieldInfo;

        public ContainerReader(TableModel tableModel, LoadItem2 loadItem, FieldInfo fieldInfo)
        {
            TableLoader = tableModel.GetTableLoader(loadItem);
            FieldInfo = fieldInfo;
        }

        public object GetContainer(Type leftElementType, Int32 axis)
        {
            Object result = null;

            var valuesAttribute = AttributeHelper.GetAttribute<ValuesAttribute>(FieldInfo, axis);
            if (valuesAttribute != null)
            {
                result = ValuesAttributeToContainer(valuesAttribute, axis, leftElementType);
                return result;
            }

            var elementListAttribute = DataTypes.AttributeHelper.GetAttribute<ElementListAttribute>(FieldInfo, axis);
            if (elementListAttribute != null)
            {
                result = ElementListAttributeToContainer(elementListAttribute);
                return result;
            }

            var offsetsAttribute = DataTypes.AttributeHelper.GetAttribute<OffsetsAttribute>(FieldInfo, axis);
            if (offsetsAttribute != null)
            {
                result = OffsetsAttributeToContainer(offsetsAttribute, leftElementType, axis);
                return result;
            }

            var countAttribute = AttributeHelper.GetAttribute<CountAttribute>(FieldInfo, axis);
            if (countAttribute != null)
            {
                result = CountAttributeToContainer(countAttribute, leftElementType, axis);
                return result;
            }

            // FieldInfo does not contain CountAttribute
            var lengthAttribute = DataTypes.AttributeHelper.GetAttribute<LengthAttribute>(FieldInfo, axis);
            if (lengthAttribute != null)
            {
                result = LengthAttributeToContainer(lengthAttribute, leftElementType, axis);
                return result;
            }

            return result;
        }


        Object ValuesAttributeToContainer(ValuesAttribute attr, Int32 axis, Type elementType)
        {
            var reader = new ValuesAttributeReader(TableLoader);
            var result = reader.GetContainer(attr, FieldInfo, axis, elementType);
            return result;
        }


        object ElementListAttributeToContainer(ElementListAttribute attr)
        {
            object result = null;

            if (attr.ElementListType != null)
            {
                var reader = new FieldValueReader(TableLoader, FieldInfo.DeclaringType);
                object[] values = reader.GetValues(attr.Value);
                var args = new Object[] { TableLoader.TableModel, TableLoader.GetFullPath(), TableLoader.GetFilePosition(), FieldInfo.DeclaringType, values };
                result = Activator.CreateInstance(attr.ElementListType, args);
            }
            else if(attr.MethodName != null)
            {
                var reader = new FieldValueReader(TableLoader, FieldInfo.DeclaringType);
                result = AttributeHelper2.GetValueT2<IElementList>(reader, FieldInfo.DeclaringType, attr);
            }
            return result;
        }


        object OffsetsAttributeToContainer(OffsetsAttribute attr, Type leftElementType, Int32 axis)
        {
            var reader = new OffsetsAttributeReader(TableLoader);//, LoadItem, FieldInfo);
            var result = reader.GetContainer(attr, FieldInfo, leftElementType, axis);
            return result;
        }


        object CountAttributeToContainer(CountAttribute countAttribute, Type leftElementType, Int32 axis)
        {
            var countAttributeReader = new CountAttributeReader(TableLoader);
            var result = countAttributeReader.GetContainer(countAttribute, FieldInfo, leftElementType, axis);
            return result;
        }


        object LengthAttributeToContainer(LengthAttribute lengthAttribute, Type leftElementType, Int32 axis)
        {
            var lengthAttributeReader = new LengthAttributeReader(TableLoader);
            var result = lengthAttributeReader.GetContainer(lengthAttribute, FieldInfo, leftElementType, axis);
            return result;
        }
    }
}
