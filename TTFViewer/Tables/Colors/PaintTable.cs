// ver 1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(PaintTable_Format1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [ClassTypeCondition(typeof(PaintTable_Format2), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "2")]
    [ClassTypeCondition(typeof(PaintTable_Format3), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "3")]
    [ClassTypeCondition(typeof(PaintTable_Format4), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "4")]
    [ClassTypeCondition(typeof(PaintTable_Format5), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "5")]
    [ClassTypeCondition(typeof(PaintTable_Format6), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "6")]
    [ClassTypeCondition(typeof(PaintTable_Format7), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "7")]
    [ClassTypeCondition(typeof(PaintTable_Format8), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "8")]
    [ClassTypeCondition(typeof(PaintTable_Format9), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "9")]
    [ClassTypeCondition(typeof(PaintTable_Format10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "10")]
    [ClassTypeCondition(typeof(PaintTable_Format11), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "11")]
    [ClassTypeCondition(typeof(PaintTable_Format12), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "12")]
    [ClassTypeCondition(typeof(PaintTable_Format13), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "13")]
    [ClassTypeCondition(typeof(PaintTable_Format14), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "14")]
    [ClassTypeCondition(typeof(PaintTable_Format15), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "15")]
    [ClassTypeCondition(typeof(PaintTable_Format16), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "16")]
    [ClassTypeCondition(typeof(PaintTable_Format17), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "17")]
    [ClassTypeCondition(typeof(PaintTable_Format18), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "18")]
    [ClassTypeCondition(typeof(PaintTable_Format19), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "19")]
    [ClassTypeCondition(typeof(PaintTable_Format20), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "20")]
    [ClassTypeCondition(typeof(PaintTable_Format21), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "21")]
    [ClassTypeCondition(typeof(PaintTable_Format22), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "22")]
    [ClassTypeCondition(typeof(PaintTable_Format23), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "23")]
    [ClassTypeCondition(typeof(PaintTable_Format24), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "24")]
    [ClassTypeCondition(typeof(PaintTable_Format25), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "25")]
    [ClassTypeCondition(typeof(PaintTable_Format26), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "26")]
    [ClassTypeCondition(typeof(PaintTable_Format27), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "27")]
    [ClassTypeCondition(typeof(PaintTable_Format28), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "28")]
    [ClassTypeCondition(typeof(PaintTable_Format29), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "29")]
    [ClassTypeCondition(typeof(PaintTable_Format30), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "30")]
    [ClassTypeCondition(typeof(PaintTable_Format31), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "31")]
    [ClassTypeCondition(typeof(PaintTable_Format32), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "32")]
    [Invalid]
    [TypeName("Paint Table")]
    [BaseName("PaintTable")]
    class PaintTable
    {
        public uint8 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format1:PaintColrLayers")]
    [BaseName("PaintTable")]
    class PaintTable_Format1
    {
        [Description(0, "Set to 1")]
        public uint8 format;

        [Description(0, "Number of offsets to paint tables to read from LayerList")]
        public uint8 numLayers;

        [Description(0, "Index (base 0) into the LayerList")]
        public uint32 firstLayerIndex;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format2:PaintSolid")]
    [BaseName("PaintTable")]
    class PaintTable_Format2
    {
        [Description(0, "Set to 2")]
        public uint8 format;

        [Description(0, "Index for a CPAL palette entry")]
        public uint16 paletteIndex;

        [Description(0, "Alpha value")]
        public F2DOT14 alpha;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format3:PaintVarSolid")]
    [BaseName("PaintTable")]
    class PaintTable_Format3
    {
        [Description(0, "Set to 3")]
        public uint8 format;

        [Description(0, "Index for a CPAL palette entry")]
        public uint16 paletteIndex;

        [Description(0, "Alpha value.For variation, use varIndexBase + 0")]
        public F2DOT14 alpha;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format4:PaintLinearGradient")]
    [BaseName("PaintTable")]
    class PaintTable_Format4
    {
        [Description(0, "Set to 4")]
        public uint8 format;

        [TableType(typeof(ColorLine))]
        [Description(0, "Offset to ColorLine table")]
        public Offset24 colorLineOffset;

        [Description(0, "Start point (p₀) x coordinate")]
        public FWORD x0;

        [Description(0, "Start point (p₀) y coordinate")]
        public FWORD y0;

        [Description(0, "End point (p₁) x coordinate")]
        public FWORD x1;

        [Description(0, "End point (p₁) y coordinate")]
        public FWORD y1;

        [Description(0, "Rotation point (p₂) x coordinate")]
        public FWORD x2;

        [Description(0, "Rotation point (p₂) y coordinate")]
        public FWORD y2;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format5:PaintVarLinearGradient")]
    [BaseName("PaintTable")]
    class PaintTable_Format5
    {
        [Description(0, "Set to 5")]
        public uint8 format;

        [TableType(typeof(VarColorLine))]
        [Description(0, "Offset to VarColorLine table")]
        public Offset24 colorLineOffset;

        [Description(0, "Start point (p₀) x coordinate.For variation, use varIndexBase + 0")]
        public FWORD x0;

        [Description(0, "Start point (p₀) y coordinate.For variation, use varIndexBase + 1")]
        public FWORD y0;

        [Description(0, "End point (p₁) x coordinate.For variation, use varIndexBase + 2")]
        public FWORD x1;

        [Description(0, "End point (p₁) y coordinate.For variation, use varIndexBase + 3")]
        public FWORD y1;

        [Description(0, "Rotation point (p₂) x coordinate.For variation, use varIndexBase + 4")]
        public FWORD x2;

        [Description(0, "Rotation point (p₂) y coordinate.For variation, use varIndexBase + 5")]
        public FWORD y2;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format6:PaintRadialGradient")]
    [BaseName("PaintTable")]
    class PaintTable_Format6
    {
        [Description(0, "Set to 6")]
        public uint8 format;

        [TableType(typeof(ColorLine))]
        [Description(0, "Offset to ColorLine table")]
        public Offset24 colorLineOffset;

        [Description(0, "Start circle center x coordinate")]
        public FWORD x0;

        [Description(0, "Start circle center y coordinate")]
        public FWORD y0;

        [Description(0, "Start circle radius")]
        public UFWORD radius0;

        [Description(0, "End circle center x coordinate")]
        public FWORD x1;

        [Description(0, "End circle center y coordinate")]
        public FWORD y1;

        [Description(0, "End circle radius")]
        public UFWORD radius1;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format7:PaintVarRadialGradient")]
    [BaseName("PaintTable")]
    class PaintTable_Format7
    {
        [Description(0, "Set to 7")]
        public uint8 format;

        [TableType(typeof(VarColorLine))]
        [Description(0, "Offset to VarColorLine table")]
        public Offset24 colorLineOffset;

        [Description(0, "Start circle center x coordinate. For variation, use varIndexBase + 0")]
        public FWORD x0;

        [Description(0, "Start circle center y coordinate.For variation, use varIndexBase + 1")]
        public FWORD y0;

        [Description(0, "Start circle radius.For variation, use varIndexBase + 2")]
        public UFWORD radius0;

        [Description(0, "End circle center x coordinate.For variation, use varIndexBase + 3")]
        public FWORD x1;

        [Description(0, "End circle center y coordinate.For variation, use varIndexBase + 4")]
        public FWORD y1;

        [Description(0, "End circle radius.For variation, use varIndexBase + 5")]
        public UFWORD radius1;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Formats8:PaintSweepGradient")]
    [BaseName("PaintTable")]
    class PaintTable_Format8
    {
        [Description(0, "Set to 8")]
        public uint8 format;

        [TableType(typeof(ColorLine))]
        [Description(0, "Offset to ColorLine table")]
        public Offset24 colorLineOffset;

        [Description(0, "Center x coordinate")]
        public FWORD centerX;

        [Description(0, "Center y coordinate")]
        public FWORD centerY;

        [Description(0, "Start of the angular range of the gradient, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 startAngle;

        [Description(0, "End of the angular range of the gradient, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 endAngle;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format9:PaintVarSweepGradient")]
    [BaseName("PaintTable")]
    class PaintTable_Format9
    {
        [Description(0, "Set to 9")]
        public uint8 format;

        [TableType(typeof(VarColorLine))]
        [Description(0, "Offset to VarColorLine table")]
        public Offset24 colorLineOffset;

        [Description(0, "Center x coordinate. For variation, use varIndexBase + 0")]
        public FWORD centerX;

        [Description(0, "Center y coordinate.For variation, use varIndexBase + 1")]
        public FWORD centerY;

        [Description(0, "Start of the angular range of the gradient, 180° in counter-clockwise degrees per 1.0 of value. For variation, use varIndexBase + 2")]
        public F2DOT14 startAngle;

        [Description(0, "End of the angular range of the gradient, 180° in counter-clockwise degrees per 1.0 of value. For variation, use varIndexBase + 3")]
        public F2DOT14 endAngle;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format10:PaintGlyph")]
    [BaseName("PaintTable")]
    class PaintTable_Format10
    {
        [Description(0, "Set to 10")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint table")]
        public Offset24 paintOffset;

        [Description(0, "Glyph ID for the source outline")]
        public uint16 glyphID;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format11:PaintColrGlyph")]
    [BaseName("PaintTable")]
    class PaintTable_Format11
    {
        [Description(0, "Set to 11")]
        public uint8 format;

        [Description(0, "Glyph ID for a BaseGlyphList base glyph")]
        public uint16 glyphID;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format12:PaintTransform")]
    [BaseName("PaintTable")]
    class PaintTable_Format12
    {
        [Description(0, "Set to 12")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [TableType(typeof(Affine2x3Table))]
        [Description(0, "Offset to an Affine2x3 table")]
        public Offset24 transformOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format13:PaintVarTransform")]
    [BaseName("PaintTable")]
    class PaintTable_Format13
    {
        [Description(0, "Set to 13")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [TableType(typeof(VarAffine2x3Table))]
        [Description(0, "Offset to a VarAffine2x3 table")]
        public Offset24 transformOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("Affine2x3 table")]
    class Affine2x3Table
    {
        [Description(0, "x-component of transformed x-basis vector")]
        public Fixed xx;

        [Description(0, "y-component of transformed x-basis vector")]
        public Fixed yx;

        [Description(0, "x-component of transformed y-basis vector")]
        public Fixed xy;

        [Description(0, "y-component of transformed y-basis vector")]
        public Fixed yy;

        [Description(0, "Translation in x direction")]
        public Fixed dx;

        [Description(0, "Translation in y direction")]
        public Fixed dy;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    [TypeName("VarAffine2x3 table")]
    class VarAffine2x3Table
    {
        [Description(0, "x-component of transformed x-basis vector. For variation, use varIndexBase + 0")]
        public Fixed xx;

        [Description(0, "y-component of transformed x-basis vector. For variation, use varIndexBase + 1")]
        public Fixed yx;

        [Description(0, "x-component of transformed y-basis vector. For variation, use varIndexBase + 2")]
        public Fixed xy;

        [Description(0, "y-component of transformed y-basis vector. For variation, use varIndexBase + 3")]
        public Fixed yy;

        [Description(0, "Translation in x direction. For variation, use varIndexBase + 4")]
        public Fixed dx;

        [Description(0, "Translation in y direction. For variation, use varIndexBase + 5")]
        public Fixed dy;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format14:PaintTranslate")]
    [BaseName("PaintTable")]
    class PaintTable_Format14
    {
        [Description(0, "Set to 14")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Translation in x direction")]
        public FWORD dx;

        [Description(0, "Translation in y direction")]
        public FWORD dy;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format15:PaintVarTranslate")]
    [BaseName("PaintTable")]
    class PaintTable_Format15
    {
        [Description(0, "Set to 15")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Translation in x direction.For variation, use varIndexBase + 0")]
        public FWORD dx;

        [Description(0, "Translation in y direction. For variation, use varIndexBase + 1")]
        public FWORD dy;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format16:PaintScale")]
    [BaseName("PaintTable")]
    class PaintTable_Format16
    {
        [Description(0, "Set to 16")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x direction")]
        public F2DOT14 scaleX;

        [Description(0, "Scale factor in y direction")]
        public F2DOT14 scaleY;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format17:PaintVarScale")]
    [BaseName("PaintTable")]
    class PaintTable_Format17
    {
        [Description(0, "Set to 17")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x direction.For variation, use varIndexBase + 0")]
        public F2DOT14 scaleX;

        [Description(0, "Scale factor in y direction. For variation, use varIndexBase + 1")]
        public F2DOT14 scaleY;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format18:PaintScaleAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format18
    {
        [Description(0, "Set to 18")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x direction")]
        public F2DOT14 scaleX;

        [Description(0, "Scale factor in y direction")]
        public F2DOT14 scaleY;

        [Description(0, "x coordinate for the center of scaling")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of scaling")]
        public FWORD centerY;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format19:PaintVarScaleAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format19
    {
        [Description(0, "Set to 19")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x direction.For variation, use varIndexBase + 0")]
        public F2DOT14 scaleX;

        [Description(0, "Scale factor in y direction. For variation, use varIndexBase + 1")]
        public F2DOT14 scaleY;

        [Description(0, "x coordinate for the center of scaling. For variation, use varIndexBase + 2")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of scaling. For variation, use varIndexBase + 3")]
        public FWORD centerY;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format20:PaintScaleUniform")]
    [BaseName("PaintTable")]
    class PaintTable_Format20
    {
        [Description(0, "Set to 20")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x and y directions")]
        public F2DOT14 scale;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format21:PaintVarScaleUniform")]
    [BaseName("PaintTable")]
    class PaintTable_Format21
    {
        [Description(0, "Set to 21")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x and y directions.For variation, use varIndexBase + 0")]
        public F2DOT14 scale;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format22:PaintScaleUniformAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format22
    {
        [Description(0, "Set to 22")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x and y directions")]
        public F2DOT14 scale;

        [Description(0, "x coordinate for the center of scaling")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of scaling")]
        public FWORD centerY;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format23:PaintVarScaleUniformAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format23
    {
        [Description(0, "Set to 23")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Scale factor in x and y directions.For variation, use varIndexBase + 0")]
        public F2DOT14 scale;

        [Description(0, "x coordinate for the center of scaling. For variation, use varIndexBase + 1")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of scaling. For variation, use varIndexBase + 2")]
        public FWORD centerY;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format24:PaintRotate")]
    [BaseName("PaintTable")]
    class PaintTable_Format24
    {
        [Description(0, "Set to 24")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Rotation angle, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 angle;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format25:PaintVarRotate")]
    [BaseName("PaintTable")]
    class PaintTable_Format25
    {
        [Description(0, "Set to 25")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Rotation angle, 180° in counter-clockwise degrees per 1.0 of value.For variation, use varIndexBase + 0")]
        public F2DOT14 angle;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format26:PaintRotateAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format26
    {
        [Description(0, "Set to 26")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Rotation angle, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 angle;

        [Description(0, "x coordinate for the center of rotation")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of rotation")]
        public FWORD centerY;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format27:PaintVarRotateAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format27
    {
        [Description(0, "Set to 27")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Rotation angle, 180° in counter-clockwise degrees per 1.0 of value.For variation, use varIndexBase + 0")]
        public F2DOT14 angle;

        [Description(0, "x coordinate for the center of rotation. For variation, use varIndexBase + 1")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of rotation. For variation, use varIndexBase + 2")]
        public FWORD centerY;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format28:PaintSkew")]
    [BaseName("PaintTable")]
    class PaintTable_Format28
    {
        [Description(0, "Set to 28")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Angle of skew in the direction of the x-axis, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 xSkewAngle;

        [Description(0, "Angle of skew in the direction of the y-axis, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 ySkewAngle;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format29:PaintVarSkew")]
    [BaseName("PaintTable")]
    class PaintTable_Format29
    {
        [Description(0, "Set to 29")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Angle of skew in the direction of the x-axis, 180° in counter-clockwise degrees per 1.0 of value.For variation, use varIndexBase + 0")]
        public F2DOT14 xSkewAngle;

        [Description(0, "Angle of skew in the direction of the y-axis, 180° in counter-clockwise degrees per 1.0 of value. For variation, use varIndexBase + 1")]
        public F2DOT14 ySkewAngle;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format30:PaintSkewAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format30
    {
        [Description(0, "Set to 30")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Angle of skew in the direction of the x-axis, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 xSkewAngle;

        [Description(0, "Angle of skew in the direction of the y-axis, 180° in counter-clockwise degrees per 1.0 of value")]
        public F2DOT14 ySkewAngle;

        [Description(0, "x coordinate for the center of rotation")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of rotation")]
        public FWORD centerY;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format31:PaintVarSkewAroundCenter")]
    [BaseName("PaintTable")]
    class PaintTable_Format31
    {
        [Description(0, "Set to 31")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a Paint subtable")]
        public Offset24 paintOffset;

        [Description(0, "Angle of skew in the direction of the x-axis, 180° in counter-clockwise degrees per 1.0 of value.For variation, use varIndexBase + 0")]
        public F2DOT14 xSkewAngle;

        [Description(0, "Angle of skew in the direction of the y-axis, 180° in counter-clockwise degrees per 1.0 of value. For variation, use varIndexBase + 1")]
        public F2DOT14 ySkewAngle;

        [Description(0, "x coordinate for the center of rotation. For variation, use varIndexBase + 2")]
        public FWORD centerX;

        [Description(0, "y coordinate for the center of rotation. For variation, use varIndexBase + 3")]
        public FWORD centerY;

        [Description(0, "Base index into DeltaSetIndexMap")]
        public uint32 varIndexBase;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Format32:PaintComposite")]
    [BaseName("PaintTable")]
    class PaintTable_Format32
    {
        [Description(0, "Set to 32")]
        public uint8 format;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a source Paint table")]
        public Offset24 sourcePaintOffset;

        [Description(0, "A CompositeMode enumeration value")]
        public uint8 compositeMode;

        [TableType(typeof(PaintTable))]
        [Description(0, "Offset to a backdrop Paint table")]
        public Offset24 backdropPaintOffset;
    }


    [TypeName("CompositeMode enumeration")]
    enum CompositeModeEnumeration
    {
        //Porter-Duff modes
        COMPOSITE_CLEAR = 0, //Clear
        COMPOSITE_SRC = 1, // Source (“Copy” in Composition & Blending Level 1)
        COMPOSITE_DEST = 2, // Destination
        COMPOSITE_SRC_OVER = 3, //Source Over
        COMPOSITE_DEST_OVER = 4, // Destination Over
        COMPOSITE_SRC_IN = 5, // Source In
        COMPOSITE_DEST_IN = 6, // Destination In
        COMPOSITE_SRC_OUT = 7, // Source Out
        COMPOSITE_DEST_OUT = 8, // Destination Ou
        COMPOSITE_SRC_ATOP = 9, //Source Atop
        COMPOSITE_DEST_ATOP = 10, // Destination Atop
        COMPOSITE_XOR = 11, // XOR
        COMPOSITE_PLUS = 12, // Plus(“Lighter” in Composition & Blending Level 1)

        //Separable color blend modes:
        COMPOSITE_SCREEN = 13, // screen
        COMPOSITE_OVERLAY = 14, // overlay
        COMPOSITE_DARKEN = 15, // darken
        COMPOSITE_LIGHTEN = 16, //lighten
        COMPOSITE_COLOR_DODGE = 17, // color-dodge
        COMPOSITE_COLOR_BURN = 18, //color-burn
        COMPOSITE_HARD_LIGHT = 19, // hard-light
        COMPOSITE_SOFT_LIGHT = 20, //soft-light
        COMPOSITE_DIFFERENCE = 21, // difference
        COMPOSITE_EXCLUSION = 22, //exclusion
        COMPOSITE_MULTIPLY = 23, //multiply

        //Non-separable color blend modes:
        COMPOSITE_HSL_HUE = 24, // hue
        COMPOSITE_HSL_SATURATION = 25, // saturation
        COMPOSITE_HSL_COLOR = 26, // color
        COMPOSITE_HSL_LUMINOSITY = 26, // luminosity
    }
#pragma warning restore IDE1006
}
