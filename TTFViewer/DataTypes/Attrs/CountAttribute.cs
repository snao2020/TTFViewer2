using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class CountAttribute : Attribute, IAxisAttribute, IValueAttribute, IMethodAttribute
    {
        public Int32 Axis { get; }

        public AttributeValue Value { get; }

        // Int32 method(IAttributeService service);
        public string MethodName { get; }


        public CountAttribute(Int32 axis, FieldValueKind valueKind, string valueParameter, string valueOption)
        {
            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, valueOption);
        }

        public CountAttribute(Int32 axis, FieldValueKind valueKind, string valueParameter)
        {
            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }

        public CountAttribute(Int32 axis, string methodName)
        {
            Axis = axis;
            MethodName = methodName;
        }
    }
}
