using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    class ClassTypeConditionAttribute : Attribute, IValueAttribute, IConditionAttribute
    {
        public Type Type { get; }

        public AttributeValue Value { get; }

        public AttributeConditionKind ConditionKind { get; }

        
        public ClassTypeConditionAttribute(Type type, AttributeConditionKind conditionKind, ClassValueKind valueKind, string valueParameter)
        {
            Type = type;

            Value = new AttributeValue(valueKind, valueParameter, null);

            ConditionKind = conditionKind;
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class ClassTypeSelectAttribute : Attribute, IValueAttribute, IMethodAttribute
    {
        public AttributeValue Value { get; }
        
        public String MethodName { get; }

        public ClassTypeSelectAttribute(ClassValueKind valueKind, string valueParameter, string valueOption)
        {
            Value = new AttributeValue(valueKind, valueParameter, valueOption);
        }

        public ClassTypeSelectAttribute(String methodName)
        {
            MethodName = methodName;
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class ClassLengthAttribute : Attribute, IValueAttribute
    {
        public AttributeValue Value { get; }

        public ClassLengthAttribute(ClassValueKind valueKind, string valueParameter)
        {
            Value = new AttributeValue(valueKind, valueParameter, null);
        }
    }
}
