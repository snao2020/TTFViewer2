using System;

namespace TTFViewer.DataTypes
{
    enum ValuesKind
    {
        None = 0,
        ParentValue = 1,
        Method = 2,
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class ValuesAttribute : Attribute, IAxisAttribute, IMethodAttribute
    {
        public ValuesKind ValuesKind { get; }
        public string ParentFieldName { get; }

        public Int32 Axis { get; }

        // return (UInt32 fileLength, IEnumerable<T>)?
        public string MethodName { get; }


        public ValuesAttribute(Int32 axis, ValuesKind valuesKind, string fieldName)
        {
            ValuesKind = valuesKind;
            ParentFieldName = fieldName;
            Axis = axis;
        }

        public ValuesAttribute(Int32 axis, string methodName)
        {
            ValuesKind = ValuesKind.Method;
            Axis = axis;
            MethodName = methodName;
        }
    }
}
