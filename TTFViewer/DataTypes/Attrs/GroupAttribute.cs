using System;

namespace TTFViewer.DataTypes
{
    delegate Int32 GroupCountMethod(IItemValueService ivs);
    delegate Int32 GroupItemCountMethod(IItemValueService ivs, Int32 itemIndex);
    delegate string GroupTextMethod(IItemValueService ivs);


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class GroupCountMethodAttribute : Attribute, IAxisAttribute
    {
        //delegate Int32 GroupCountMethod(IItemValueService ivs);
        public String MethodName { get; }

        public Int32 Axis { get; }

        public GroupCountMethodAttribute(Int32 axis, String methodName)
        {
            MethodName = methodName;
            Axis = axis;
        }
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class GroupItemCountMethodAttribute : Attribute, IAxisAttribute
    {
        //delegate Int32 GroupItemCountMethod(IItemValueService ivs, Int32 itemIndex);
        public String MethodName { get; }

        public Int32 Axis { get; }

        public GroupItemCountMethodAttribute(Int32 axis, String methodName)
        {
            MethodName = methodName;
            Axis = axis;
        }
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class GroupTextMethodAttribute : Attribute, IAxisAttribute
    {
        //delegate string GroupTextMethod(IItemValueService ivs);
        public String MethodName { get; }

        public Int32 Axis { get; }


        public GroupTextMethodAttribute(Int32 axis, String methodName)
        {
            MethodName = methodName;
            Axis = axis;
        }
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class GroupDescriptionAttribute : Attribute, IAxisAttribute
    {
        public String Text { get; }

        public Int32 Axis { get; }


        public GroupDescriptionAttribute(Int32 axis, String text)
        {
            Text = text;
            Axis = axis;
        }
    }

    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class GroupDescriptionMethodAttribute : Attribute, IAxisAttribute
    {
        //delegate string GroupTextMethod(IItemValueService ivs);
        public String MethodName { get; }

        public Int32 Axis { get; }


        public GroupDescriptionMethodAttribute(Int32 axis, String methodName)
        {
            MethodName = methodName;
            Axis = axis;
        }
    }
}
