using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class TypeConditionAttribute : Attribute, IValueAttribute, IConditionAttribute
    {
        public Type Type { get; }

        public AttributeValue Value { get; }

        public AttributeConditionKind ConditionKind { get; }


        public TypeConditionAttribute(Type type, AttributeConditionKind conditionKind, FieldValueKind valueKind, string valueParameter)
        {
            Type = type;

            Value = new AttributeValue(valueKind, valueParameter, null);

            ConditionKind = conditionKind;
        }
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    class TypeSelectAttribute : Attribute, IValueAttribute, IMethodAttribute
    {
        public AttributeValue Value { get; }

        // object[] method(IAttributeService service);
        public string MethodName { get; }


        public TypeSelectAttribute(FieldValueKind valueKind, string valueParameter, string valueOption)
        {
            Value = new AttributeValue(valueKind, valueParameter, valueOption);
        }


        public TypeSelectAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
