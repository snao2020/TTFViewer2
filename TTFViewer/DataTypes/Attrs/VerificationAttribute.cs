using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class VerificationAttribute : Attribute, IAxisAttribute
    {
        public AttributeConditionKind Condition { get; }
        public AttributeValue Value0 { get; }
        public AttributeValue Value1 { get; }

        public Int32 Axis { get; }

        public VerificationAttribute(Int32 axis, AttributeConditionKind condition, FieldValueKind valueKind0, String valueParameter0, FieldValueKind valueKind1, String valueParameter1)
        {
            Axis = axis;
            Condition = condition;
            Value0 = new AttributeValue(valueKind0, valueParameter0, null);
            Value1 = new AttributeValue(valueKind1, valueParameter1, null);
        }

        public VerificationAttribute(Int32 axis, AttributeConditionKind condition, FieldValueKind valueKind, String valueParameter, Type type)
        {
            Axis = axis;
            Condition = condition;
            Value0 = new AttributeValue(valueKind, valueParameter, null);
            String typeString = type.ToString();
            Value1 = new AttributeValue(FieldValueKind.Type, typeString, null);
        }
    }
}
