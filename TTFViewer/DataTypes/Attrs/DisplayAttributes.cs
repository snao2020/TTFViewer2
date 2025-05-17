using System;

namespace TTFViewer.DataTypes
{
    /*
    HiddenIfNullAttribute Field Inherited  noparams
    BaseNameAttribute     Class Inherited  Name
    InvalidAttribute      Class|Field Axis MethodName
    TypeNameAttribute     Class|Field|Enum Name TypeNameKind{Text, Method}
    FieldNameAttribute    Field            Axis,Text FieldNameKind{Text,Method}
    ValueFormatAttribute  Field            Axis,MethodName Option(RawType) ValueFormatKind{Default,Decimal,Hex,Method,NoValue}
    DescriptionAttribute  Class|Field      Axis,Text DescriptionKind{Text,Method,Enum,Instruction,EmRelative,AsciiText,UnicodeText,ValueType}
    */
    delegate string IVPMethod(IItemValueService ivp);
    delegate bool IsInvalidMethod(IItemValueService ivp);


    [AttributeUsage(AttributeTargets.Field)]
    class HiddenIfNullAttribute : Attribute
    {
        public HiddenIfNullAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class BaseNameAttribute : Attribute
    {
        private string Name;

        public BaseNameAttribute(string name)
            => Name = name;

        public static string GetName(Type type)
        {
            var attr = AttributeHelper.GetAttribute<BaseNameAttribute>(type);
            return attr?.Name ?? type.Name;
        }
    }

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Field, Inherited = false)]
    public class InvalidAttribute : Attribute, IAxisAttribute
    {                                                                           
        public Int32 Axis { get; }

        // delegate bool IsInvalidMethod(IItemValueProvider ivp);
        public string MethodName { get; }

        public InvalidAttribute()
        {
        }

        public InvalidAttribute(Int32 axis, string methodName)
        {
            Axis = axis;
            MethodName = methodName;
        }
    }


    enum TypeNameKind
    {
        Default = 0,
        Text = 1,
        Method = 2,
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Enum,  Inherited = false)]
    class TypeNameAttribute : Attribute
    {
        // if return null, try next step
        public TypeNameKind TypeNameKind { get; }
        public string Name { get; }

        public TypeNameAttribute(string name)
        {
            TypeNameKind = TypeNameKind.Text;
            Name = name;
        }

        public TypeNameAttribute(TypeNameKind typeNameKind, string name)
        {
            TypeNameKind = typeNameKind;
            Name = name;
        }
    }


    enum FieldNameKind
    {
        Text = 0,
        Method = 1,
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple=true, Inherited = false)]
    class FieldNameAttribute : Attribute, IAxisAttribute
    {
        public FieldNameKind FieldNameKind { get; }
        public String Text { get; }
        public Int32 Axis { get; }


        public FieldNameAttribute(Int32 axis, String text)
        {
            FieldNameKind = FieldNameKind.Text;
            Text = text;
            Axis = axis;
        }
        

        public FieldNameAttribute(Int32 axis, FieldNameKind fieldNameKind, String text)
        {
            FieldNameKind = fieldNameKind;
            Text = text;
            Axis = axis;
        }
    }


    enum ValueFormatKind
    {
        Default = 0,
        Decimal = 1,
        Hex = 2,
        Method = 3,
        NoValue = 4,
    }

    [Flags]
    enum ValueFormatOption
    { 
        None = 0,
        RawType = 0x0001,
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class ValueFormatAttribute : Attribute, IAxisAttribute
    {
        public ValueFormatKind ValueKind { get; }
        public String MethodName { get; }

        public ValueFormatOption Option { get; set; }

        public Int32 Axis { get; }


        public ValueFormatAttribute(Int32 axis, ValueFormatKind fieldValueKind)
        {
            Axis = axis;
            ValueKind = fieldValueKind;
        }

        public ValueFormatAttribute(Int32 axis, String methodName)
        {
            Axis = axis;
            ValueKind = ValueFormatKind.Method;
            MethodName = methodName;
        }
    }


    enum DescriptionKind
    {
        Text = 0,
        Method = 1,
        Enum = 2,
        Instruction = 3,
        EmRelative = 4,
        AsciiText = 5,
        UnicodeText = 6,
        ValueType = 7,
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class DescriptionAttribute : Attribute, IAxisAttribute
    {
        public DescriptionKind DescriptionKind { get; }
        public string Text { get; }

        public Int32 Axis { get; }


        public DescriptionAttribute(Int32 axis, string text)
        {
            DescriptionKind = DescriptionKind.Text;
            Text = text;
            Axis = axis;
        }


        public DescriptionAttribute(Int32 axis, DescriptionKind descrKind, string text)
        {
            DescriptionKind = descrKind;
            Text = text;
            Axis = axis;
        }


        public DescriptionAttribute(Int32 axis, Type enumType)
        {
            if (enumType.IsEnum)
            {
                DescriptionKind = DescriptionKind.Enum;
                Text = enumType.ToString();
                Axis = axis;
            }
            else
                throw new ArgumentException("DescriptionAttribute enumType is not enum");
        }
    }
}
