using System;

namespace TTFViewer.DataTypes
{
    /*
    <any>
    <null>
    [LAST] 
        [TypeSelectAttribute(FieldValueKind.Path, "ComponentGlyphTables\\[LAST]\\flags", "Mask:0x0100")] // 0x0100=WE_HAVE_INSTRUCTIONS
        [TypeConditionAttribute(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.EEnum, "TTFViewer.Tables.CompositeGlyphFlags.WE_HAVE_INSTRUCTIONS")]
        public uint16? instructionLength;
    */

    /*
    Option
        delimiter ','
        // Binary   op:num 
        "Mask",
        "Add",            // ContainerKind.Length OptionParam = int string(it can include 0x,0b,_)
        "Sub",            // ContainerKind.Length OptionParam = int string(it can include 0x,0b,_)
        "Mul",            // ContainerKind.Length OptionParam = int string(it can include 0x,0b,_)
        "Div",            // ContainerKind.Length OptionParam = int string(it can include 0x,0b,_)
        "AddIfNonZero",   // ContainerKind.Length OptionParam = int string(it can include 0x,0b,_)
        "SubIfNonZero ",   // ContainerKind.Length OptionParam = int string(it can include 0x,0b,_)
    }
    */

    public enum ClassValueKind
    {
        None = 0,
        Unsigned = 1,
        Enum = 2,
        Tag = 3,
        Type = 4,

        TableValueType = 5, // param ancectorLevel(0:current,1:parent,2...)
        FontTableType = 6, // if(param  == null) GetCurrentFontTable, else param:tag
        FontTableValue = 7, // param = tag\\pathstring --> return TableClass

        FieldPath = 8,
    }


    public enum FieldValueKind
    {
        None = 0,
        Unsigned = 1,
        Enum = 2,
        Tag = 3,
        Type = 4,

        TableValueType  = 5, // param ancectorLevel(0:current,1:parent,2...)
        FontTableType = 6,  // if(param  == null) GetCurrentFontTable, else param:tag
        FontTableValue = 7,      // param = tag\\pathstring --> return TableClass

        PeekValue = 8,
        OffsetSource = 9,  // param = n;pathstring(n;ancestorLevel) or pathstring(ancestorLevel=1)

        Path = 10,           // param = pathstring (pathstring can contain [LAST]) --> return object
        ElementList = 11, // return owner elementList
        FilePositionNull = 12,// return UInt32? if value==null return null
        ParentConstraint = 13,
    }


    enum AttributeConditionKind
    {
        Default = 0,
        Equal = 1,
        NotEqual = 2,
        HasFlag = 3,
    }


    struct ValueKind
    {
        public Object Value
        {
            get;
            private set;
        }

        public static implicit operator ValueKind(FieldValueKind valueKind)
        {
            var result = default(ValueKind);
            result.Value = valueKind;
            return result;
        }

        public static implicit operator ValueKind(ClassValueKind valueKind)
        {
            var result = default(ValueKind);
            result.Value = valueKind;
            return result; ;
        }
    }


    class AttributeValue
    {
        public ValueKind ValueKind { get; }
        public string ValueParameter { get; }
        public string ValueOption { get; }

        public AttributeValue(FieldValueKind valueKind, string valueParameter, string valueOption)
        {
            ValueKind = valueKind;
            ValueParameter = valueParameter;
            ValueOption = valueOption;
        }

        public AttributeValue(ClassValueKind valueKind, string valueParameter, string valueOption)
        {
            ValueKind = valueKind;
            ValueParameter = valueParameter;
            ValueOption = valueOption;
        }
    }



    interface IAxisAttribute
    {
        Int32 Axis { get; }
    }

    interface ITagAttribute
    {
        Tag Tag { get; }
    }


    interface IValueAttribute
    {
        AttributeValue Value { get; }
    }


    interface IMethodAttribute
    {
        string MethodName { get; }
    }


    interface IConditionAttribute
    {
        AttributeConditionKind ConditionKind { get; }
    }
}
