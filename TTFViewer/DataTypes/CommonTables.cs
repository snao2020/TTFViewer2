using System.Runtime.InteropServices;

namespace TTFViewer.DataTypes
{
#pragma warning disable IDE1006

    /*
    CommonTables
        RootTable  // namespace=DateTypes

        NotImplementedFontTable
            FontTableAttribute corresponding to TableRecord.tableTag is not found

        NullTable
            TablePositionFlag.MayBeNULL is set and offset == 0
            
        ErrorTable
            TableTypeAttribute nor TableSelectAttribute is not found in field of Offset16/24/32
    */


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RootTable
    {
        [TableType(TableKey.Startup, FieldValueKind.Path, "Offset")]
        public Offset32 Offset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable]
    [TypeName("<null>")]
    [Description(0, "not implemented")]
    public class NotImplementedFontTable
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("<null>")]
    public class NullTable
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Invalid]
    [TypeName("Error")]
    public class ErrorTable
    {
    }

#pragma warning restore IDE1006
}