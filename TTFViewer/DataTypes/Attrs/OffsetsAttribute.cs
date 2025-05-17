using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class OffsetsAttribute : Attribute, IAxisAttribute, IValueAttribute, IMethodAttribute
    {
        public string BasePosition { get; }

        public Int32 Axis { get; }

        public AttributeValue Value { get; }

        // IList<UInt32> method(IAttributeService service);
        public string MethodName { get; }


        public OffsetsAttribute(Int32 axis, string basePosition, FieldValueKind valueKind, string valueParameter, string valueOption)
        {
            BasePosition = basePosition;
            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, valueOption);
        }
        
        public OffsetsAttribute(Int32 axis, string methodName)
        {
            Axis = axis;
            MethodName = methodName;
        }
    }
}
