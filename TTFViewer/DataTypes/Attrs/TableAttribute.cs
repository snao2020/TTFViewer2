using System;


namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class StartupTableAttribute : Attribute, ITagAttribute
    {
        public Tag Tag { get; }

        // DefaultTable
        public StartupTableAttribute()
        {
        }

        public StartupTableAttribute(string tag)
        {
            Tag = tag;
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class FontTableAttribute : Attribute, ITagAttribute
    {
        public Tag Tag { get; }
        public CreateModelFlags Flags { get; set; }


        // NotImplementedFontTable
        public FontTableAttribute()
        {
        }

        public FontTableAttribute(string tag)
        {
            Tag = tag;
        }
    }


    enum TableKey
    {
        None = 0,
        Startup = 1,
        FontTable = 2,  // search FontTableAttribute
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    class TableTypeAttribute : Attribute, IValueAttribute//, IMethodAttribute
    {
        public Type TableType { get; }
        public TableKey TableKey { get; }
        public CreateModelFlags CreateModelFlags { get; set; }

        public AttributeValue Value { get; }

        public TableTypeAttribute(Type tableType)
        {
            TableType = tableType;
        }

        public TableTypeAttribute(TableKey tableKey, FieldValueKind valueKind, string valueParameter)
        {
            TableKey = tableKey;

            Value = new AttributeValue(valueKind, valueParameter, null);
        }
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class TableConditionAttribute : Attribute, IValueAttribute, IConditionAttribute
    {
        public Type Type { get; }

        public AttributeValue Value { get; }

        public AttributeConditionKind ConditionKind { get; }

        public TableConditionAttribute(Type type, AttributeConditionKind conditionKind, FieldValueKind valueKind, string valueParameter)
        {
            Type = type;

            Value = new AttributeValue(valueKind, valueParameter, null);
            ConditionKind = conditionKind;
        }
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    class TableSelectAttribute : Attribute, IValueAttribute, IMethodAttribute
    {
        public CreateModelFlags CreateModelFlags { get; set; }

        public AttributeValue Value { get; }

        // object[] method(IAttributeService service);
        public string MethodName { get; }


        public TableSelectAttribute(FieldValueKind valueKind, string valueParameter)
        {
            Value = new AttributeValue(valueKind, valueParameter, null);
        }

        public TableSelectAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }


    [Flags]
    enum TablePositionFlag
    {
        None = 0,
        MayBeNULL = 1,
        //OffsetMul2 = 2,         // gvar.glyphVariationDataOffsets16
        Method = 2,
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    class TablePositionAttribute : Attribute, IValueAttribute, IMethodAttribute
    {
        public string BasePosition { get; }
        public TablePositionFlag Flags { get; set; }

        public AttributeValue Value { get; }

        public String MethodName { get; }

        public TablePositionAttribute(string basePosition)
        {
            BasePosition = basePosition;
        }
        
        public TablePositionAttribute(string basePosition, FieldValueKind valueKind, string valueParameter)
        {
            BasePosition = basePosition; ;

            Value = new AttributeValue(valueKind, valueParameter, null);
        }

        public TablePositionAttribute(TablePositionFlag flags, String methodName)
        {
            if(flags.HasFlag(TablePositionFlag.Method))
            {
                Flags = Flags;
                MethodName = methodName;
            }
        }
    }


    enum TableLengthKind
    {
        None = 0,
        FileLength = 1,
        ElementCount = 2,
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    class TableLengthAttribute : Attribute, IValueAttribute, IMethodAttribute
    {
        public TableLengthKind LengthKind { get; }

        public AttributeValue Value { get; }

        public String MethodName { get; }

        public TableLengthAttribute(TableLengthKind lengthKind, FieldValueKind valueKind, string valueParameter)
        {
            LengthKind = lengthKind;
            Value = new AttributeValue(valueKind, valueParameter, null);
        }

        public TableLengthAttribute(TableLengthKind lengthKind, String methodName)
        {
            LengthKind = lengthKind;
            MethodName = methodName;
        }
    }
}
