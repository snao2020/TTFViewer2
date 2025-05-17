using System;
using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.ViewModel
{
    [AttributeUsage(AttributeTargets.Field)]
    class ProxyAttribute : Attribute
    {
        public Type OffsetDeclaringType { get; }
        public string OffsetFieldName { get; }

        public ProxyAttribute(Type offsetDeclaringType, string offsetFieldName)
        {
            OffsetDeclaringType = offsetDeclaringType;
            OffsetFieldName = offsetFieldName;
        }


        public static FieldInfo FindTarget(Type offsetDeclaringType, string offsetFieldName, Type valueType)
        {
            FieldInfo result = null;

            if (valueType != null)
            {
                var fis = typeof(ProxyTable).GetFields();
                foreach (var fi in fis)
                {
                    if (fi.FieldType == valueType)
                    {
                        var attr = AttributeHelper.GetAttribute<ProxyAttribute>(fi);
                        if (attr != null
                            && attr.OffsetDeclaringType == offsetDeclaringType
                            && attr.OffsetFieldName == offsetFieldName)
                        {
                            return fi;
                        }
                    }
                }
            }
            return result;
        }
    }
}
