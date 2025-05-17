using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class ElementListAttribute : Attribute, IAxisAttribute, IValueAttribute, IMethodAttribute
    {
        public Type ElementListType { get; }

        public Int32 Axis { get; }

        public AttributeValue Value { get; }

        // IElementList method(IAttributeService service);
        public string MethodName { get; }

        public ElementListAttribute(Int32 axis, Type elementListType, FieldValueKind valueKind, String valueParameter, String option)
        {
            ElementListType = elementListType;

            Axis = axis;

            Value = new AttributeValue(valueKind, valueParameter, option);
        }

        
        public ElementListAttribute(Int32 axis, string methodName)
        {
            Axis = axis;

            MethodName = methodName;
        }
    }
}
