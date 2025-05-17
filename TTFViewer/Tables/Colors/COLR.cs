// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("COLR")]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(COLRTable_Version0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(COLRTable_Version1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    [TypeName("COLR — Color Table")]
    [BaseName("COLR")]
    class COLRTable
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("COLR version 0 — Color Table")]
    [BaseName("COLR")]
    class COLRTable_Version0
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number—set to 0")]
        public uint16 version;

        [Description(0, "Number of BaseGlyph Records")]
        public uint16 numBaseGlyphRecords;

        [TableType(typeof(IList<BaseGlyph>))]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "numBaseGlyphRecords")]
        [Description(0, "Offset to baseGlyphRecords array")]
        public Offset32 baseGlyphRecordsOffset;

        [TableType(typeof(IList<Layer>))]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "numLayerRecords")]
        [Description(0, "Offset to layerRecords array")]
        public Offset32 layerRecordsOffset;

        [Description(0, "Number of Layer records")]
        public uint16 numLayerRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("COLR version 1 — Color Table")]
    [BaseName("COLR")]
    class COLRTable_Version1
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version number—set to 1")]
        public uint16 version;

        [Description(0, "Number of BaseGlyph records; may be 0 in a version 1 table")]
        public uint16 numBaseGlyphRecords;

        [TableType(typeof(IList<BaseGlyph>))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "numBaseGlyphRecords")]
        [Description(0, "Offset to baseGlyphRecords array(may be NULL)")]
        public Offset32 baseGlyphRecordsOffset;

        [TableType(typeof(IList<Layer>))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [TableLength(TableLengthKind.ElementCount, FieldValueKind.Path, "numLayerRecords")]
        [Description(0, "Offset to layerRecords array(may be NULL)")]
        public Offset32 layerRecordsOffset;

        [Description(0, "Number of Layer records; may be 0 in a version 1 table")]
        public uint16 numLayerRecords;

        [TableType(typeof(BaseGlyphList))]
        [Description(0, "Offset to BaseGlyphList table")]
        public Offset32 baseGlyphListOffset;

        [TableType(typeof(LayerList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to LayerList table (may be NULL)")]
        public Offset32 layerListOffset;

        [TableType(typeof(ClipList))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ClipList table(may be NULL)")]
        public Offset32 clipListOffset;

        [TableType(typeof(DeltaSetIndexMap))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to DeltaSetIndexMap table(may be NULL)")]
        public Offset32 varIndexMapOffset;

        [TableType(typeof(ItemVariationStore))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ItemVariationStore(may be NULL)")]
        public Offset32 itemVariationStoreOffset;
    }


    #region BaseGlyph and Layer records

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class BaseGlyph
    {
        [Description(0, "Glyph ID of the base glyph")]
        public uint16 glyphID;

        [Description(0, "Index (base 0) into the layerRecords array")]
        public uint16 firstLayerIndex;

        [Description(0, "Number of color layers associated with this glyph")]
        public uint16 numLayers;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Layer")]
    class Layer
    {
        [Description(0, "Glyph ID of the glyph used for a given layer")]
        public uint16 glyphID;

        [Description(0, "index (base 0) for a palette entry in the CPAL table")]
        public uint16 paletteIndex;
    }

    #endregion

    #region BaseGlyphList, LayerList and ClipList

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BaseGlyphList
    {
        public uint32 numBaseGlyphPaintRecords;

        [Count(0, FieldValueKind.Path, "numBaseGlyphPaintRecords")]
        public IList<BaseGlyphPaintRecord> baseGlyphPaintRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class BaseGlyphPaintRecord
    {
        [Description(0, "Glyph ID of the base glyph")]
        public uint16 glyphID;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint table")]
        public Offset32 paintOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LayerList
    {
        public uint32 numLayers;

        [Count(0, FieldValueKind.Path, "numLayers")]
        [TableType(typeof(PaintTable))]
        [Description(0, "Offsets to Paint tables")]
        public IList<Offset32> paintOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ClipList
    {
        [Description(0, "Set to 1")]
        public uint8 format;

        [Description(0, "Number of Clip records")]
        public uint32 numClips;

        [Count(0, FieldValueKind.Path, "numClips")]
        [Description(0, "Clip records.Sorted by startGlyphID")]
        public IList<Clip> clips;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class Clip
    {
        [Description(0, "First glyph ID in the range")]
        public uint16 startGlyphID;

        [Description(0, "Last glyph ID in the range")]
        public uint16 endGlyphID;

        [TableType(typeof(ClipBox))]
        [Description(0, "Offset to a ClipBox table")]
        public Offset24 clipBoxOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(ClipBoxFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(ClipBoxFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [Invalid]
    [TypeName("ClipBox")]
    [BaseName("ClipBox")]
    class ClipBox
    {
        [Description(0, "Set to 1 or 2")]
        public uint8 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("ClipBoxFormat1 table, static clip box")]
    [BaseName("ClipBox")]
    class ClipBoxFormat1
    {
        [Description(0, "Set to 1")]
        public uint8 format;

        [Description(0, "Minimum x of clip box")]
        public FWORD xMin;

        [Description(0, "Minimum y of clip box")]
        public FWORD yMin;

        [Description(0, "Maximum x of clip box")]
        public FWORD xMax;

        [Description(0, "Maximum y of clip box")]
        public FWORD yMax;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("ClipBoxFormat2 table, variable clip box")]
    [BaseName("ClipBox")]
    class ClipBoxFormat2
    {
        [Description(0, "Set to 2")]
        public uint8 format;

        [Description(0, "Minimum x of clip box.For variation, use varIndexBase + 0")]
        public FWORD xMin;

        [Description(0, "Minimum y of clip box.For variation, use varIndexBase + 1")]
        public FWORD yMin;

        [Description(0, "Maximum x of clip box.For variation, use varIndexBase + 2")]
        public FWORD xMax;

        [Description(0, "Maximum y of clip box.For variation, use varIndexBase + 3")]
        public FWORD yMax;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }

    #endregion

    #region Color references, ColorStop and ColorLine

/* var1,9,1 deleted
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("ColorIndex record")]
    class ColorIndexRecord
    {
        [Description(0, "Index for a CPAL palette entry")]
        public uint16 paletteIndex;

        [Description(0, "Alpha value")]
        public F2DOT14 alpha;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("VarColorIndex record")]
    class VarColorIndexRecord
    {
        [Description(0, "Index for a CPAL palette entry")]
        public uint16 paletteIndex;

        [Description(0, "Alpha value. For variation, use varIndexBase + 0")]
        public F2DOT14 alpha;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }
*/
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class ColorStop
    {
        [Description(0, "Position on a color line")]
        public F2DOT14 stopOffset;

        [Description(0, "Index for a CPAL palette entry")]
        public uint16 paletteIndex;

        [Description(0, "Alpha value")]
        public F2DOT14 alpha;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class VarColorStop
    {
        [Description(0, "Position on a color line. For variation, use varIndexBase + 0")]
        public F2DOT14 stopOffset;

        [Description(0, "Index for a CPAL palette entry")]
        public uint16 paletteIndex;

        [Description(0, "Alpha value. For variation, use varIndexBase + 1")]
        public F2DOT14 alpha;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ColorLine
    {
        [Description(0, typeof(ExtendEnumeration))]
        public uint8 extend;

        [Description(0, "Number of ColorStop records")]
        public uint16 numStops;

        [Count(0, FieldValueKind.Path, "numStops")]
        public IList<ColorStop> colorStops;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class VarColorLine
    {
        [Description(0, typeof(ExtendEnumeration))]
        public uint8 extend;

        [Description(0, "Number of ColorStop records")]
        public uint16 numStops;

        [Count(0, FieldValueKind.Path, "numStops")]
        [Description(0, "Allows for variations")]
        public IList<VarColorStop> colorStops;
    }


    [TypeName("Extend")]
    enum ExtendEnumeration
    {
        EXTEND_PAD = 0, // Use nearest color stop.
        EXTEND_REPEAT = 1,// Repeat from farthest color stop.
        EXTEND_REFLECT = 2, // Mirror color line from nearest end.
    }

    #endregion

#pragma warning restore IDE1006
}
