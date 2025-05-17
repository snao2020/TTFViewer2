using System;


namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class LengthAttribute : Attribute, IAxisAttribute, IValueAttribute, IMethodAttribute
    {
        // CFF,CFF2
        public CFFDICTOperators? CFFDICTOperator { get; }
        public CFF2DICTOperators? CFF2DICTOperator { get; }
        public Int32 OperandIndex { get; }

        public Int32 Axis { get; }

        public AttributeValue Value { get; }

        // UInt32 method(IAttributeService service);
        public string MethodName { get; }


        public LengthAttribute(Int32 axis, FieldValueKind valueKind, string valueParameter, string valueOption)
        {
            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, valueOption);
        }


        public LengthAttribute(Int32 axis, string methodName)
        {
            Axis = axis;

            MethodName = methodName;
        }


        public LengthAttribute(Int32 axis, FieldValueKind valueKind, string valueParameter, CFFDICTOperators op, Int32 operandIndex)
        {
            CFFDICTOperator = op;
            OperandIndex = operandIndex;

            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }


        public LengthAttribute(Int32 axis, FieldValueKind valueKind, string valueParameter, CFF2DICTOperators op, Int32 operandIndex)
        {
            CFF2DICTOperator = op;
            OperandIndex = operandIndex;

            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }
    }
}
