// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ValueRecord
    {
        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(int16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.X_PLACEMENT")]
        [TypeName("int16")]
        [Description(0, "Horizontal adjustment for placement, in design units")]
        public int16? xPlacement;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(int16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.Y_PLACEMENT")]
        [TypeName("int16")]
        [Description(0, "Vertical adjustment for placement, in design units")]
        public int16? yPlacement;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(int16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.X_ADVANCE")]
        [TypeName("int16")]
        [Description(0, "Horizontal adjustment for advance, in design units — only used for horizontal layout")]
        public int16? xAdvance;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(int16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.Y_ADVANCE")]
        [TypeName("int16")]
        [Description(0, "Vertical adjustment for advance, in design units — only used for vertical layout")]
        public int16? yAdvance;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(Offset16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.X_PLACEMENT_DEVICE")]
        [TableSelect(FieldValueKind.FontTableValue, "GPOS\\Header\\featureVariationsOffset")]
        [TableCondition(typeof(Device),AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<null>")]
        [TableCondition(typeof(VariationIndex), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [TypeName("Offest16")]
        [Description(0, "Offset to Device table(non-variable font) / VariationIndex table(variable font) for horizontal placement, from beginning of the immediate parent table(SinglePos or PairPosFormat2 lookup subtable, PairSet table within a PairPosFormat1 lookup subtable) — may be NULL")]
        public Offset16? xPlaDeviceOffset;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(Offset16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.Y_PLACEMENT_DEVICE")]
        [TableSelect(FieldValueKind.FontTableValue, "GPOS\\Header\\featureVariationsOffset")]
        [TableCondition(typeof(Device), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<null>")]
        [TableCondition(typeof(VariationIndex), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [TypeName("Offest16")]
        [Description(0, "Offset to Device table(non-variable font) / VariationIndex table(variable font) for vertical placement, from beginning of the immediate parent table(SinglePos or PairPosFormat2 lookup subtable, PairSet table within a PairPosFormat1 lookup subtable) — may be NULL")]
        public Offset16? yPlaDeviceOffset;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(Offset16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.X_ADVANCE_DEVICE")]
        [TableSelect(FieldValueKind.FontTableValue, "GPOS\\Header\\featureVariationsOffset")]
        [TableCondition(typeof(Device), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<null>")]
        [TableCondition(typeof(VariationIndex), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [TypeName("Offest16")]
        [Description(0, "Offset to Device table(non-variable font) / VariationIndex table(variable font) for horizontal advance, from beginning of the immediate parent table(SinglePos or PairPosFormat2 lookup subtable, PairSet table within a PairPosFormat1 lookup subtable) — may be NULL")]
        public Offset16? xAdvDeviceOffset;

        [TypeSelectAttribute("GetFormat2")]
        [TypeConditionAttribute(typeof(Offset16), AttributeConditionKind.HasFlag, FieldValueKind.Enum, "TTFViewer.Tables.ValueFormat.Y_ADVANCE_DEVICE")]
        [TableSelect(FieldValueKind.FontTableValue, "GPOS\\Header\\featureVariationsOffset")]
        [TableCondition(typeof(Device), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<null>")]
        [TableCondition(typeof(VariationIndex), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [TypeName("Offest16")]
        [Description(0, "Offset to Device table(non-variable font) / VariationIndex table(variable font) for vertical advance, from beginning of the immediate parent table(SinglePos or PairPosFormat2 lookup subtable, PairSet table within a PairPosFormat1 lookup subtable) — may be NULL")]
        public Offset16? yAdvDeviceOffset;

        static object[] GetFormat2(IAttributeService service)//, object[] values)
        {
            object[] result = null;

            if (service.GetValues(FieldValueKind.TableValueType, "0").SingleValue(0) is Type valueType)
            {
                if (valueType == typeof(SinglePosFormat1)
                    || valueType == typeof(SinglePosFormat2))
                {
                    result = service.GetValues(FieldValueKind.Path, "\\valueFormat");
                }
                else if (valueType == typeof(PairSet))
                {
                    if (service.Path.Contains("valueRecord1"))
                        result = service.GetValues(FieldValueKind.OffsetSource, "1:\\valueFormat1");
                    else if (service.Path.Contains("valueRecord2"))
                        result = service.GetValues(FieldValueKind.OffsetSource, "1:\\valueFormat2");
                }
                else if (valueType == typeof(PairPosFormat2))
                {
                    if (service.Path.Contains("valueRecord1"))
                        result = service.GetValues(FieldValueKind.Path, "\\valueFormat1");
                    else if (service.Path.Contains("valueRecord2"))
                        result = service.GetValues(FieldValueKind.Path, "\\valueFormat2");
                }

            }
            return result;
        }
    }


    [Flags]
    enum ValueFormat
    {
        X_PLACEMENT = 0x0001, // Includes horizontal adjustment for placement
        Y_PLACEMENT = 0x0002, // Includes vertical adjustment for placement
        X_ADVANCE = 0x0004, // Includes horizontal adjustment for advance
        Y_ADVANCE = 0x0008, // Includes vertical adjustment for advance
        X_PLACEMENT_DEVICE = 0x0010, // DEVICE Includes Device table(non-variable font) / VariationIndex table(variable font) for horizontal placement
        Y_PLACEMENT_DEVICE = 0x0020, // Includes Device table(non-variable font) / VariationIndex table(variable font) for vertical placement
        X_ADVANCE_DEVICE = 0x0040, // Includes Device table(non-variable font) / VariationIndex table(variable font) for horizontal advance
        Y_ADVANCE_DEVICE = 0x0080, // Includes Device table(non-variable font) / VariationIndex table(variable font) for vertical advance
        Reserved = 0xFF00, // For future use(set to zero)
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "anchorFormat", null)]
    [ClassTypeCondition(typeof(AnchorFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(AnchorFormat2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(AnchorFormat3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [Invalid]
    [BaseName("Anchor")]
    class Anchor
    {
        [Description(0, "Format identifier")]
        public uint16 anchorFormat;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("Anchor")]
    class AnchorFormat1
    {
        [Description(0, "Format identifier, = 1")]
        public uint16 format;

        [Description(0, "Horizontal value, in design units")]
        public int16 xCoordinate;

        [Description(0, "Vertical value, in design units")]
        public int16 yCoordinate;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("Anchor")]
    class AnchorFormat2
    {
        [Description(0, "Format identifier, = 2")]
        public uint16 format;

        [Description(0, "Horizontal value, in design units")]
        public int16 xCoordinate;

        [Description(0, "Vertical value, in design units")]
        public int16 yCoordinate;

        [Description(0, "Index to glyph contour point")]
        public uint16 anchorPoint;
    }
    

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("Anchor")]
    class AnchorFormat3
    {
        [Description(0, "Format identifier, = 3")]
        public uint16 format;

        [Description(0, "Horizontal value, in design units")]
        public int16 xCoordinate;

        [Description(0, "Vertical value, in design units")]
        public int16 yCoordinate;

        [TableSelect(FieldValueKind.FontTableValue, "GPOS\\Header\\featureVariationsOffset")]
        [TableCondition(typeof(Device), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<null>")]
        [TableCondition(typeof(VariationIndex), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to Device table(non-variable font) / VariationIndex table(variable font) for X coordinate, from beginning of Anchor table(may be NULL)")]
        public Offset16 xDeviceOffset;

        [TableSelect(FieldValueKind.FontTableValue, "GPOS\\Header\\featureVariationsOffset")]
        [TableCondition(typeof(Device), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "<null>")]
        [TableCondition(typeof(VariationIndex), AttributeConditionKind.Default, FieldValueKind.None, null)]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to Device table(non-variable font) / VariationIndex table(variable font) for Y coordinate, from beginning of Anchor table(may be NULL)")]
        public Offset16 yDeviceOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("MarkArray")]
    class MarkArray
    {
        [Description(0, "Number of MarkRecords")]
        public uint16 markCount;

        [Count(0, FieldValueKind.Path, "markCount")]
        [Description(0, "Array of MarkRecords, ordered by corresponding glyphs in the associated mark Coverage table.")]
        public IList<MarkRecord> markRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class MarkRecord
    {
        [Description(0, "Class defined for the associated mark")]
        public uint16 markClass;

        [TableType(typeof(Anchor))]
        [Description(0, "Offset to Anchor table, from beginning of MarkArray table")]
        public Offset16 markAnchorOffset;
    }
#pragma warning restore IDE1006
}
