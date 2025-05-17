using System;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class ClassAttributeReader
    {
        TableModel TableModel;
        UInt32 FilePosition;
        Type DeclaringType;

        public ClassAttributeReader(TableModel tableModel, UInt32 filePosition, Type declaringType)
        {
            TableModel = tableModel;
            FilePosition = filePosition;
            DeclaringType = declaringType;
        }


        public UInt32? GetLength()
        {
            var reader = new ClassValueReader(TableModel, FilePosition, DeclaringType);
            var attr = AttributeHelper.GetAttribute<ClassLengthAttribute>(DeclaringType);
            var result = AttributeHelper2.GetValueT2<UInt32?>(reader, DeclaringType, attr);
            return result;
        }


        public Type GetValueType()
        {
            Type result = DeclaringType;
            var selectAttribute = AttributeHelper.GetAttribute<ClassTypeSelectAttribute>(DeclaringType);
            var reader = new ClassValueReader(TableModel, FilePosition, DeclaringType);
            var attr = AttributeHelper2.SelectAttribute4<ClassTypeSelectAttribute,ClassTypeConditionAttribute>(reader, selectAttribute, DeclaringType);
            if(attr != null)
                result = attr.Type;

            if (result != null && result.IsGenericTypeDefinition)
            {
                var types = DeclaringType.GetGenericArguments();
                result = result.MakeGenericType(types);
            }
            return result;
        }
    }
}
