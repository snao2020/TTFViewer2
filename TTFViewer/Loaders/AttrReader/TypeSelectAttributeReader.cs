using System;
using System.Diagnostics;
using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.Loaders
{
    class TypeSelectAttributeReader
    {
        TableLoader TableLoader;

        public TypeSelectAttributeReader(TableLoader tableLoader)
        {
            TableLoader = tableLoader;
        }


        public Type GetValueType(int axis, FieldInfo fi, TypeSelectAttribute attr)
        {
            Type result = TypeHelper.GetElementTypeRank(fi.FieldType).Item1;
            if (attr != null)
            {
                var reader = new FieldValueReader(TableLoader, fi.DeclaringType);
                var ret = AttributeHelper2.SelectAttribute4<TypeSelectAttribute, TypeConditionAttribute>(reader, attr, fi);
                if (ret != null)
                    result = ret.Type;
                if (result == null)
                    Debug.WriteLine($"TypeSelectAttriibuteReader.GetValueType null fieldName={fi.Name}");
            }
            return result;
        }
    }
}
