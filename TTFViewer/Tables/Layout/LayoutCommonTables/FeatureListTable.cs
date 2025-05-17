// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("FeatureList table")]
    class FeatureList
    {
        [Description(0, "Number of FeatureRecords in this table")]
        public uint16 featureCount;

        [Count(0, FieldValueKind.Path, "featureCount")]
        [Description(0, "Array of FeatureRecords — zero-based (first feature has FeatureIndex = 0), listed alphabetically by feature tag")]
        public IList<FeatureRecord> featureRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class FeatureRecord
    {
        [Description(0, "4-byte feature identification tag")]
        public Tag featureTag;

        [TableType(typeof(Feature))]
        [Description(0, "Offset to Feature table, from beginning of FeatureList")]
        public Offset16 featureOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Feature
    {
        //[TableType(typeof(FeatureParamsTable))]
        //[TablePosition(TablePositionKind.OwnerRelative, Flags = TablePositionFlag.MayBeNULL)]
        [TypeName("Offset16")]
        [ValueFormat(0, ValueFormatKind.Hex, Option = ValueFormatOption.RawType)]
        [Description(0, "Offset from start of Feature table to FeatureParams table, if defined for the feature and present, else NULL")]
        public uint16 featureParamsOffset;

        [Description(0, "Number of LookupList indices for this feature")]
        public uint16 lookupIndexCount;

        [Count(0, FieldValueKind.Path, "lookupIndexCount")]
        [Description(0, "Array of indices into the LookupList — zero-based (first lookup is LookupListIndex = 0)")]
        public IList<uint16> lookupListIndices;
    }
#pragma warning restore IDE1006
}
