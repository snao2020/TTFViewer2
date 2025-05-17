// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("BASE")]
    [TypeName("BASE — Baseline table")]
    [BaseName("BASE")]
    class BASETable
    {
        [FieldName(0, null)]
        public BASEHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(BASEHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [ClassTypeCondition(typeof(BASEHeader_Version11), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0001")]
    [Invalid]
    [TypeName("BASE Header")]
    [BaseName("BASEHeader")]
    class BASEHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Major version of the BASE table = 1")]
        public uint16 majorVersion;

        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Minor version of the BASE table = 0 or 1")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("BASE Header, version 1.0")]
    class BASEHeader_Version10 : BASEHeader
    {
        //public uint16 majorVersion; // Major version of the BASE table, = 1
        //public uint16 minorVersion; // Minor version of the BASE table, = 0

        [TableType(typeof(Axis))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to horizontal Axis table, from beginning of BASE table(may be NULL)")]
        public Offset16 horizAxisOffset;

        [TableType(typeof(Axis))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to vertical Axis table, from beginning of BASE table(may be NULL)")]
        public Offset16 vertAxisOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("BASE Header, version 1.1")]
    class BASEHeader_Version11 : BASEHeader
    {
        //public uint16 majorVersion; // Major version of the BASE table, = 1
        //public uint16 minorVersion; // Minor version of the BASE table, = 1

        [TableType(typeof(Axis))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to horizontal Axis table, from beginning of BASE table(may be NULL)")]
        public Offset16 horizAxisOffset;

        [TableType(typeof(Axis))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to vertical Axis table, from beginning of BASE table(may be NULL)")]
        public Offset16 vertAxisOffset;

        [TableType(typeof(ItemVariationStore))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to Item Variation Store table, from beginning of BASE table(may be null)")]
        public Offset32 itemVarStoreOffset;
    }


    #region Sub

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Axis
    {
        [TableType(typeof(BaseTagList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to BaseTagList table, from beginning of Axis table(may be NULL)")]
        public Offset16 baseTagListOffset;

        [TableType(typeof(BaseScriptList))]
        [Description(0, "Offset to BaseScriptList table, from beginning of Axis table")]
        public Offset16 baseScriptListOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseTagList
    {
        [Description(0, "Number of baseline identification tags in this text direction — may be zero(0)")]
        public uint16 baseTagCount;

        [Count(0, FieldValueKind.Path, "baseTagCount")]
        [Description(0, "Array of 4-byte baseline identification tags — must be in alphabetical order")]
        public IList<Tag> baselineTags;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseScriptList
    {
        [Description(0, "Number of BaseScriptRecords defined")]
        public uint16 baseScriptCount;

        [Count(0, FieldValueKind.Path, "baseScriptCount")]
        [Description(0, "Array of BaseScriptRecords, in alphabetical order by baseScriptTag")]
        public IList<BaseScriptRecord> baseScriptRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class BaseScriptRecord
    {
        [Description(0, "4-byte script identification tag")]
        public Tag baseScriptTag;

        [TableType(typeof(BaseScript))]
        [Description(0, "Offset to BaseScript table, from beginning of BaseScriptList")]
        public Offset16 baseScriptOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseScript
    {
        [TableType(typeof(BaseValues))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to BaseValues table, from beginning of BaseScript table(may be NULL)")]
        public Offset16 baseValuesOffset;

        [TableType(typeof(MinMax))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to MinMax table, from beginning of BaseScript table(may be NULL)")]
        public Offset16 defaultMinMaxOffset;

        [Description(0, "Number of BaseLangSysRecords defined — may be zero(0)")]
        public uint16 baseLangSysCount;

        [Count(0, FieldValueKind.Path, "baseLangSysCount")]
        [Description(0, "Array of BaseLangSysRecords, in alphabetical order by BaseLangSysTag")]
        public IList<BaseLangSys> baseLangSysRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class BaseLangSys
    {
        [Description(0, "4-byte language system identification tag")]
        public Tag baseLangSysTag;

        [TableType(typeof(MinMax))]
        [Description(0, "Offset to MinMax table, from beginning of BaseScript table")]
        public Offset16 minMaxOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseValues
    {
        [Description(0, "Index number of default baseline for this script — equals index position of baseline tag in baselineTags array of the BaseTagList")]
        public uint16 defaultBaselineIndex;

        [Description(0, "Number of BaseCoord tables defined — should equal baseTagCount in the BaseTagList")]
        public uint16 baseCoordCount;

        [Count(0, FieldValueKind.Path, "baseCoordCount")]
        [TableType(typeof(BaseCoord))]
        [Description(0, "Array of offsets to BaseCoord tables, from beginning of BaseValues table — order matches baselineTags array in the BaseTagList")]
        public IList<Offset16> baseCoordOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MinMax
    {
        [TableType(typeof(BaseCoord))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to BaseCoord table that defines the minimum extent value, from the beginning of MinMax table(may be NULL)")]
        public Offset16 minCoordOffset;

        [TableType(typeof(BaseCoord))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to BaseCoord table that defines maximum extent value, from the beginning of MinMax table(may be NULL)")]
        public Offset16 maxCoordOffset;

        [Description(0, "Number of FeatMinMaxRecords — may be zero(0)")]
        public uint16 featMinMaxCount;

        [Count(0, FieldValueKind.Path, "featMinMaxCount")]
        [Description(0, "Array of FeatMinMaxRecords, in alphabetical order by featureTableTag")]
        public IList<FeatMinMax> featMinMaxRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class FeatMinMax
    {
        [Description(0, "4-byte feature identification tag — must match feature tag in FeatureList")]
        public Tag featureTableTag;

        [TableType(typeof(BaseCoord))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to BaseCoord table that defines the minimum extent value, from beginning of MinMax table(may be NULL)")]
        public Offset16 minCoordOffset;

        [TableType(typeof(BaseCoord))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to BaseCoord table that defines the maximum extent value, from beginning of MinMax table(may be NULL)")]
        public Offset16 maxCoordOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(BaseCoordFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(BaseCoordFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(BaseCoordFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [Invalid]
    class BaseCoord
    {
        [Description(0, "Format identifier")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [BaseName("BaseCoord")]
    class BaseCoordFormat1
    {
        [Description(0, "Format identifier — format = 1")]
        public uint16 format;

        [Description(0, "X or Y value, in design units")]
        public int16 coordinate;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [BaseName("BaseCoord")]
    class BaseCoordFormat2
    {
        [Description(0, "Format identifier — format = 2")]
        public uint16 format;

        [Description(0, "X or Y value, in design units")]
        public int16 coordinate;

        [Description(0, "Glyph ID of control glyph")]
        public uint16 referenceGlyph;

        [Description(0, "Index of contour point on the reference glyph")]
        public uint16 baseCoordPoint;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [BaseName("BaseCoord")]
    class BaseCoordFormat3
    {
        [Description(0, "Format identifier — format = 3")]
        public uint16 format;

        [Description(0, "X or Y value, in design units")]
        public int16 coordinate;

        [TableType(typeof(Device))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to Device table(non-variable font) / Variation Index table(variable font) for X or Y value, from beginning of BaseCoord table(may be NULL)")]
        public Offset16 deviceOffset;
    }

    //Item Variation Store Table

    #endregion

#pragma warning restore IDE1006
}
