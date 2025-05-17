// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("loca")]    
    [ClassTypeSelect(ClassValueKind.FontTableValue, "maxp\\numGlyphs, head\\indexToLocFormat", null)]
    [ClassTypeCondition(typeof(locaTableShortFormat), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "<any>,0")]
    [ClassTypeCondition(typeof(locaTableLongFormat), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "<any>, 1")]
    [Invalid]
    [TypeName("loca — Index to Location")]
    [BaseName("loca")]
    class locaTable
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("loca — Index to Location: Short format")]
    [BaseName("loca")]
    class locaTableShortFormat
    { 
        [Count(0, FieldValueKind.FontTableValue, "maxp\\numGlyphs", "AddIfNonZero:1")]
        [TypeName("Offset16")]
        [ValueFormat(1, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        public IList<uint16> offsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("loca — Index to Location: Long format")]
    [BaseName("loca")]
    class locaTableLongFormat
    {
        [Count(0, FieldValueKind.FontTableValue, "maxp\\numGlyphs", "AddIfNonZero:1")]
        [TypeName("Offset32")]
        [ValueFormat(1, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        public IList<uint32> offsets;
    }
#pragma warning restore IDE1006
}
