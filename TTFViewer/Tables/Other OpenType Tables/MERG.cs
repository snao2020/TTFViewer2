// ver 1.9.1 not tested
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("MERG")]
    [TypeName("MERG — Merge Table")]
    [BaseName("MERG")]
    class MERGTable
    {
        [FieldName(0, null)]
        public MergeHeader Header;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(MergeHeaderVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [Invalid]
    [TypeName("Merge header")]
    [BaseName("MergeHeader")]
    class MergeHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Version number of the merge table")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Merge header")]
    class MergeHeaderVersion0 : MergeHeader
    {
        //public uint16 version; // Version number of the merge table — set to 0.

        [Description(0, "The number of merge classes.")]
        public uint16 mergeClassCount;
        
        [TableType(typeof(MergeEntry))]
        [Description(0, "Offset to the array of merge-entry data.")]
        public Offset16 mergeDataOffset;

        [Description(0, "The number of class definition tables.")]
        public uint16 classDefCount;

        [TableType(typeof(ClassDefOffsets))]
        [Description(0, "Offset to an array of offsets to class definition tables — in bytes from the start of the MERG table.")]
        public Offset16 offsetToClassDefOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MergeEntry
    {
        [Count(0, FieldValueKind.OffsetSource, "mergeClassCount")]
        [Description(0, "Array of merge-entry rows.")]
        public IList<MergeEntryRow> mergeEntryRows;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MergeEntryRow
    {
        [Count(0, FieldValueKind.OffsetSource, "mergeClassCount")]
        [Description(0, "Array of merge entries.")]
        [Description(1, typeof(MergeEntryFlags))]
        public IList<uint8> mergeEntries;
    }


    [TypeName("Merge entry flags")]
    enum MergeEntryFlags
    {
        MERGE_LTR = 0x01, // Merge glyphs, for LTR visual order.
        GROUP_LTR = 0x02, // Group glyphs, for LTR visual order.
        SECOND_IS_SUBORDINATE_LTR = 0x04, // Second glyph is subordinate to the first glyph, for LTR visual order.
        [FieldName(0, "Reserved")]
        Reserved0 = 0x08, // Flag reserved for future use — set to 0
        MERGE_RTL = 0x10, // Merge glyphs, for RTL visual order.
        GROUP_RTL = 0x20, // Group glyphs, for RTL visual order.
        SECOND_IS_SUBORDINATE_RTL = 0x40, // Second glyph is subordinate to the first glyph, for RTL visual order.
        [FieldName(0, "Reserved")]
        Reserved1 = 0x80, // Flag reserved for future use — set to 0
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("")]
    class ClassDefOffsets
    {
        [Count(0, FieldValueKind.OffsetSource, "classDefCount")]
        [TableType(typeof(ClassDef))]
        public Offset16[] Offsets;
    }

#pragma warning restore IDE1006
}
