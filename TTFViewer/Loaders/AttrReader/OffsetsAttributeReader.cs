using System;
using System.Collections.Generic;
using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.Loaders
{
    class OffsetsAttributeReader
    {
        TableLoader TableLoader;

        public OffsetsAttributeReader(TableLoader tableLoader)
        {
            TableLoader = tableLoader;
        }


        public object GetContainer(OffsetsAttribute attr, FieldInfo fi, Type leftElementType, Int32 axis)
        {
            object result = null;

            UInt32? containerLength = null;
            var containerLengthAttr = fi != null ? DataTypes.AttributeHelper.GetAttribute<LengthAttribute>(fi, axis) : null;
            if (containerLengthAttr != null)
            {
                var lengthReader = new LengthAttributeReader(TableLoader);
                containerLength = lengthReader.GetLength(containerLengthAttr, fi.DeclaringType);
            }

            var reader = new FieldValueReader(TableLoader, fi.DeclaringType);

            if (attr.Value != null)
            {
                var value = reader.GetValues(attr.Value).SingleValue(0);
                if (value != null)
                {
                    UInt32 basePosition = TableLoader.GetBasePosition(attr.BasePosition);
                    result = OffsetsContainerHelper.Create(leftElementType, TableLoader.TableModel, TableLoader.GetFullPath(), fi.DeclaringType, value, basePosition, attr.Value.ValueOption, containerLength);
                }
            }
            else if (attr.MethodName != null)
            {
                if (AttributeHelper.GetMethod(fi.DeclaringType, attr.MethodName, typeof(AttributeMethodT<IList<UInt32>>)) is AttributeMethodT<IList<UInt32>> method)
                {
                    var service = reader.GetAttributeService();
                    Func<IList<UInt32>> func = () => (IList<UInt32>)method(service);
                    result = OffsetsContainerHelper.Create(leftElementType, TableLoader.TableModel, TableLoader.GetFullPath(), fi.DeclaringType, func, containerLength);
                }
            }
            return result;
        }
    }
}
