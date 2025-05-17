using System;


namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    class PositionAttribute : Attribute, IAxisAttribute, IValueAttribute, IMethodAttribute
    {
        public string BasePosition { get; }
        public CFFDICTOperators? CFFDICTOperator { get; }
        public CFF2DICTOperators? CFF2DICTOperator { get; }
        public Int32 OperandIndex { get; }

        public delegate bool CanCreateMethod(Int32 operandInteger);
        public string CanCreate { get; set; }

        public Int32 Axis { get; }

        public AttributeValue Value { get; }

        //public delegate UInt32? PositionAttributeMethod(IAttributeService service);
        public String MethodName { get; }


        public PositionAttribute(Int32 axis, string offsetBase, FieldValueKind valueKind, string valueParameter)
        {
            BasePosition = offsetBase;
            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }


        public PositionAttribute(Int32 axis, string offsetBase, FieldValueKind valueKind, string valueParameter, CFFDICTOperators cffDICTOperator, Int32 operandIndex)//, string canCreate)
        {
            BasePosition = offsetBase;
            CFFDICTOperator = cffDICTOperator;
            OperandIndex = operandIndex;

            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }


        public PositionAttribute(Int32 axis, string offsetBase, FieldValueKind valueKind, string valueParameter, CFF2DICTOperators cff2DICTOperator, Int32 operandIndex)//, string canCreate)
        {
            BasePosition = offsetBase;
            CFF2DICTOperator = cff2DICTOperator;
            OperandIndex = operandIndex;

            Axis = axis;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }


        public PositionAttribute(Int32 axis, string methodName)
        {
            Axis = axis;
            MethodName = methodName;
        }
    }
}
