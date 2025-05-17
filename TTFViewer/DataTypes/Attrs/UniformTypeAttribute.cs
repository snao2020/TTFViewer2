using System;

namespace TTFViewer.DataTypes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class UniformTypeAttribute : Attribute, IAxisAttribute
    {
        public Int32 Axis { get; }

        public UniformTypeAttribute()
        {
        }

        public UniformTypeAttribute(Int32 axis)
        {
            Axis = axis;
        }
    }
}
