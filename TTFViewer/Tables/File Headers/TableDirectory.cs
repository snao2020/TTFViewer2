// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [StartupTable()]
    [ClassTypeSelect(ClassValueKind.FieldPath, "sfntVersion", null)]
    [ClassTypeCondition(typeof(TableDirectory_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x00010000")]
    [ClassTypeCondition(typeof(TableDirectory_Version10), AttributeConditionKind.Equal, ClassValueKind.Tag, "OTTO")]
    [Invalid]
    [BaseName("TableDirectory")]
    class TableDirectory
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "0x00010000 or 0x4F54544F ('OTTO')")]
        public uint32 sfntVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("TableDirectory")]
    [BaseName("TableDirectory")]
    class TableDirectory_Version10
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "0x00010000 or 0x4F54544F('OTTO')")]
        public uint32 sfntVersion;  
        
        [Description(0, "Number of tables")]
        public uint16 numTables;
        
        [Description(0, "Maximum power of 2 less than or equal to numTables, times 16 ((2**floor(log2(numTables))) * 16, where “**” is an exponentiation operator).")]
        public uint16 searchRange;
        
        [Description(0, "Log2 of the maximum power of 2 less than or equal to numTables(log2(searchRange/16), which is equal to floor(log2(numTables)))")]
        public uint16 entrySelector;

        [Description(0, "numTables times 16, minus searchRange((numTables* 16) - searchRange)")]
        public uint16 rangeShift;

        [Count(0, FieldValueKind.Path, "numTables")]
        [Description(0, "Table records array—one for each top-level table in the font")]
        [Description(1, DescriptionKind.Method, "tableRecordsDescription")]
        public TableRecord[] tableRecords;

        static string tableRecordsDescription(IItemValueService ivp)
        {
            if (ivp.Value is TableRecord tr)
                return $"Tag='{tr.tableTag}'";
            else
                return null;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class TableRecord
    {
        [Description(0, "Table identifier")]
        public Tag tableTag;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Checksum for this table")]
        public uint32 checksum; 

        [TableType(TableKey.FontTable, FieldValueKind.Path, "tableTag")]
        [TablePosition(null)]
        [TableLength(TableLengthKind.FileLength, FieldValueKind.Path, "length")]
        [Description(0, "Offset from beginning of font file")]
        public Offset32 offset;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Length of this table")]
        public uint32 length;           
    }
#pragma warning restore IDE1006
}
