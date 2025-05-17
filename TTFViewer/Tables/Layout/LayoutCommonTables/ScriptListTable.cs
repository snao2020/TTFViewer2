// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;



namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ScriptList
    {
        [Description(0, "Number of ScriptRecords")]
        public uint16 scriptCount;

        [Count(0, FieldValueKind.Path, "scriptCount")]
        [Description(0, "Array of ScriptRecords, listed alphabetically by script tag")]
        public IList<ScriptRecord> scriptRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class ScriptRecord
    {
        [Description(0, "4-byte script tag identifier")]
        public Tag scriptTag;

        [TableType(typeof(Script))]
        [Description(0, "Offset to Script table, from beginning of ScriptList")]
        public Offset16 scriptOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Script
    {
        [TableType(typeof(LangSys))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to default LangSys table, from beginning of Script table — may be NULL")]
        public Offset16 defaultLangSysOffset;

        [Description(0, "Number of LangSysRecords for this script — excluding the default LangSys")]
        public uint16 langSysCount;

        [Count(0, FieldValueKind.Path, "langSysCount")]
        [Description(0, "Array of LangSysRecords, listed alphabetically by LangSys tag")]
        public IList<LangSysRecord> langSysRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class LangSysRecord
    {
        [Description(0, "4-byte LangSysTag identifier")]
        public Tag langSysTag;

        [TableType(typeof(LangSys))]
        [Description(0, "Offset to LangSys table, from beginning of Script table")]
        public Offset16 langSysOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("LangSys table")]
    class LangSys
    {
        //[TableType(typeof(ReorderingTable), ChildParamFlags = ChildParamFlags.OffsetMayBeNULL)]
        [TypeName("Offset16")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "NULL(reserved for an offset to a reordering table)")]
        public uint16 lookupOrderOffset;

        [Description(0, "Index of a feature required for this language system; if no required features = 0xFFFF")]
        public uint16 requiredFeatureIndex;

        [Description(0, "Number of feature index values for this language system — excludes the required feature")]
        public uint16 featureIndexCount;

        [Count(0, FieldValueKind.Path, "featureIndexCount")]
        [Description(0, "Array of indices into the FeatureList, in arbitrary order")]
        public IList<uint16> featureIndices;
    }
#pragma warning restore IDE1006
}
