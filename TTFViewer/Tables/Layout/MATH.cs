//ver1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class MathValueRecord
    {
        [Description(0, "The X or Y value in design units")]
        public FWORD value;

        [TableType(typeof(Device))]
        [TablePosition("", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to the device table — from the beginning of parent table.May be NULL. Suggested format for device table is 1")]
        public Offset16 deviceOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("MATH")]
    [TypeName("MATH - The mathematical typesetting table")]
    class MATHTable
    {
        [FieldName(0, null)]
        public MATHHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(MATHHeader_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    [TypeName("MATH Header")]
    class MATHHeader
    {
        [Description(0, "Major version of the MATH table, = 1")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the MATH table, = 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("MATH Header")]
    class MATHHeader_Version10 : MATHHeader
    {
        //public uint16 majorVersion; // Major version of the MATH table, = 1.
        //public uint16 minorVersion; // Minor version of the MATH table, = 0.

        [TableType(typeof(MathConstants))]
        [Description(0, "Offset to MathConstants table - from the beginning of MATH table")]
        public Offset16 mathConstantsOffset;

        [TableType(typeof(MathGlyphInfo))]
        [Description(0, "Offset to MathGlyphInfo table - from the beginning of MATH table")]
        public Offset16 mathGlyphInfoOffset;

        [TableType(typeof(MathVariants))]
        [Description(0, "Offset to MathVariants table - from the beginning of MATH table")]
        public Offset16 mathVariantsOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathConstants
    {
        [Description(0, "Percentage of scaling down for level 1 superscripts and subscripts.Suggested value: 80%")]
        public int16 scriptPercentScaleDown;

        [Description(0, "Percentage of scaling down for level 2 (scriptScript) superscripts and subscripts.Suggested value: 60%")]
        public int16 scriptScriptPercentScaleDown;

        [Description(0, "Minimum height required for a delimited expression (contained within parentheses, etc.) to be treated as a sub-formula.Suggested value: normal line height × 1.5")]
        public UFWORD delimitedSubFormulaMinHeight;

        [Description(0, "Minimum height of n-ary operators(such as integral and summation) for formulas in display mode(that is, appearing as standalone page elements, not embedded inline within text)")]
        public UFWORD displayOperatorMinHeight;

        [Description(0, "White space to be left between math formulas to ensure proper line spacing.For example, for applications that treat line gap as a part of line ascender, formulas with ink going above(os2.sTypoAscender + os2.sTypoLineGap - MathLeading) or with ink going below os2.sTypoDescender will result in increasing line height")]
        public MathValueRecord mathLeading;

        [Description(0, "Axis height of the font")]
        public MathValueRecord axisHeight;

        [Description(0, "Maximum(ink) height of accent base that does not require raising the accents.Suggested: x‑height of the font(os2.sxHeight) plus any possible overshots")]
        public MathValueRecord accentBaseHeight;

        [Description(0, "Maximum(ink) height of accent base that does not require flattening the accents.Suggested: cap height of the font(os2.sCapHeight)")]
        public MathValueRecord flattenedAccentBaseHeight;

        [Description(0, "The standard shift down applied to subscript elements.Positive for moving in the downward direction.Suggested: os2.ySubscriptYOffset")]
        public MathValueRecord subscriptShiftDown;

        [Description(0, "Maximum allowed height of the(ink) top of subscripts that does not require moving subscripts further down.Suggested: 4/5 x- height")]
        public MathValueRecord subscriptTopMax;

        [Description(0, "Minimum allowed drop of the baseline of subscripts relative to the(ink) bottom of the base. Checked for bases that are treated as a box or extended shape.Positive for subscript baseline dropped below the base bottom")]
        public MathValueRecord subscriptBaselineDropMin;

        [Description(0, "Standard shift up applied to superscript elements.Suggested: os2.ySuperscriptYOffset")]
        public MathValueRecord superscriptShiftUp;

        [Description(0, "Standard shift of superscripts relative to the base, in cramped style")]
        public MathValueRecord superscriptShiftUpCramped;

        [Description(0, "Minimum allowed height of the(ink) bottom of superscripts that does not require moving subscripts further up.Suggested: ¼ x-height")]
        public MathValueRecord superscriptBottomMin;

        [Description(0, "Maximum allowed drop of the baseline of superscripts relative to the(ink) top of the base. Checked for bases that are treated as a box or extended shape.Positive for superscript baseline below the base top")]
        public MathValueRecord superscriptBaselineDropMax;

        [Description(0, "Minimum gap between the superscript and subscript ink.Suggested: 4 × default rule thickness")]
        public MathValueRecord subSuperscriptGapMin;

        [Description(0, "The maximum level to which the(ink) bottom of superscript can be pushed to increase the gap between superscript and subscript, before subscript starts being moved down.Suggested: 4/5 x-height")]
        public MathValueRecord superscriptBottomMaxWithSubscript;

        [Description(0, "Extra white space to be added after each subscript and superscript.Suggested: 0.5 pt for a 12 pt font")]
        public MathValueRecord spaceAfterScript;

        [Description(0, "Minimum gap between the(ink) bottom of the upper limit, and the(ink) top of the base operator")]
        public MathValueRecord upperLimitGapMin;

        [Description(0, "Minimum distance between baseline of upper limit and(ink) top of the base operator")]
        public MathValueRecord upperLimitBaselineRiseMin;

        [Description(0, "Minimum gap between(ink) top of the lower limit, and(ink) bottom of the base operator")]
        public MathValueRecord lowerLimitGapMin;

        [Description(0, "Minimum distance between baseline of the lower limit and(ink) bottom of the base operator")]
        public MathValueRecord lowerLimitBaselineDropMin;

        [Description(0, "Standard shift up applied to the top element of a stack")]
        public MathValueRecord stackTopShiftUp;

        [Description(0, "Standard shift up applied to the top element of a stack in display style")]
        public MathValueRecord stackTopDisplayStyleShiftUp;

        [Description(0, "Standard shift down applied to the bottom element of a stack.Positive for moving in the downward direction")]
        public MathValueRecord stackBottomShiftDown;

        [Description(0, "Standard shift down applied to the bottom element of a stack in display style.Positive for moving in the downward direction")]
        public MathValueRecord stackBottomDisplayStyleShiftDown;

        [Description(0, "Minimum gap between(ink) bottom of the top element of a stack, and the(ink) top of the bottom element.Suggested: 3 × default rule thickness")]
        public MathValueRecord stackGapMin;

        [Description(0, "Minimum gap between(ink) bottom of the top element of a stack, and the(ink) top of the bottom element in display style.Suggested: 7 × default rule thickness")]
        public MathValueRecord stackDisplayStyleGapMin;

        [Description(0, "Standard shift up applied to the top element of the stretch stack")]
        public MathValueRecord stretchStackTopShiftUp;

        [Description(0, "Standard shift down applied to the bottom element of the stretch stack.Positive for moving in the downward direction")]
        public MathValueRecord stretchStackBottomShiftDown;

        [Description(0, "Minimum gap between the ink of the stretched element, and the(ink) bottom of the element above.Suggested: same value as upperLimitGapMin")]
        public MathValueRecord stretchStackGapAboveMin;

        [Description(0, "Minimum gap between the ink of the stretched element, and the(ink) top of the element below.Suggested: same value as lowerLimitGapMin")]
        public MathValueRecord stretchStackGapBelowMin;

        [Description(0, "Standard shift up applied to the numerator")]
        public MathValueRecord fractionNumeratorShiftUp;

        [Description(0, "Standard shift up applied to the numerator in display style.Suggested: same value as stackTopDisplayStyleShiftUp")]
        public MathValueRecord fractionNumeratorDisplayStyleShiftUp;

        [Description(0, "Standard shift down applied to the denominator.Positive for moving in the downward direction")]
        public MathValueRecord fractionDenominatorShiftDown;

        [Description(0, "Standard shift down applied to the denominator in display style.Positive for moving in the downward direction.Suggested: same value as stackBottomDisplayStyleShiftDown")]
        public MathValueRecord fractionDenominatorDisplayStyleShiftDown;

        [Description(0, "Minimum tolerated gap between the(ink) bottom of the numerator and the ink of the fraction bar.Suggested: default rule thickness")]
        public MathValueRecord fractionNumeratorGapMin;

        [Description(0, "Minimum tolerated gap between the(ink) bottom of the numerator and the ink of the fraction bar in display style.Suggested: 3 × default rule thickness")]
        public MathValueRecord fractionNumDisplayStyleGapMin;

        [Description(0, "Thickness of the fraction bar.Suggested: default rule thickness")]
        public MathValueRecord fractionRuleThickness;

        [Description(0, "Minimum tolerated gap between the(ink) top of the denominator and the ink of the fraction bar.Suggested: default rule thickness")]
        public MathValueRecord fractionDenominatorGapMin;

        [Description(0, "Minimum tolerated gap between the(ink) top of the denominator and the ink of the fraction bar in display style.Suggested: 3 × default rule thickness")]
        public MathValueRecord fractionDenomDisplayStyleGapMin;

        [Description(0, "Horizontal distance between the top and bottom elements of a skewed fraction")]
        public MathValueRecord skewedFractionHorizontalGap;

        [Description(0, "Vertical distance between the ink of the top and bottom elements of a skewed fraction")]
        public MathValueRecord skewedFractionVerticalGap;

        [Description(0, "Distance between the overbar and the(ink) top of he base. Suggested: 3 × default rule thickness")]
        public MathValueRecord overbarVerticalGap;

        [Description(0, "Thickness of overbar.Suggested: default rule thickness")]
        public MathValueRecord overbarRuleThickness;

        [Description(0, "Extra white space reserved above the overbar.Suggested: default rule thickness")]
        public MathValueRecord overbarExtraAscender;

        [Description(0, "Distance between underbar and(ink) bottom of the base. Suggested: 3 × default rule thickness")]
        public MathValueRecord underbarVerticalGap;

        [Description(0, "Thickness of underbar.Suggested: default rule thickness")]
        public MathValueRecord underbarRuleThickness;

        [Description(0, "Extra white space reserved below the underbar.Always positive.Suggested: default rule thickness")]
        public MathValueRecord underbarExtraDescender;

        [Description(0, "Space between the(ink) top of the expression and the bar over it.Suggested: 1¼ default rule thickness")]
        public MathValueRecord radicalVerticalGap;

        [Description(0, "Space between the(ink) top of the expression and the bar over it.Suggested: default rule thickness + ¼ x-height")]
        public MathValueRecord radicalDisplayStyleVerticalGap;

        [Description(0, "Thickness of the radical rule.This is the thickness of the rule in designed or constructed radical signs.Suggested: default rule thickness")]
        public MathValueRecord radicalRuleThickness;

        [Description(0, "Extra white space reserved above the radical.Suggested: same value as radicalRuleThickness")]
        public MathValueRecord radicalExtraAscender;

        [Description(0, "Extra horizontal kern before the degree of a radical, if such is present")]
        public MathValueRecord radicalKernBeforeDegree;

        [Description(0, "Negative kern after the degree of a radical, if such is present.Suggested: −10/18 of em")]
        public MathValueRecord radicalKernAfterDegree;

        [Description(0, "Height of the bottom of the radical degree, if such is present, in proportion to the ascender of the radical sign.Suggested: 60%")]
        public int16 radicalDegreeBottomRaisePercent;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathGlyphInfo
    {
        [TableType(typeof(MathItalicsCorrectionInfo))]
        [Description(0, "Offset to MathItalicsCorrectionInfo table, from the beginning of the MathGlyphInfo table")]
        public Offset16 mathItalicsCorrectionInfoOffset;

        [TableType(typeof(MathTopAccentAttachment))]
        [Description(0, "Offset to MathTopAccentAttachment table, from the beginning of the MathGlyphInfo table")]
        public Offset16 mathTopAccentAttachmentOffset;

        [TableType(typeof(Coverage))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to ExtendedShapes coverage table, from the beginning of the MathGlyphInfo table.When the glyph to the left or right of a box is an extended shape variant, the (ink) box should be used for vertical positioning purposes, not the default position defined by values in MathConstants table. May be NULL")]
        public Offset16 extendedShapeCoverageOffset;

        [TableType(typeof(MathKernInfo))]
        [Description(0, "Offset to MathKernInfo table, from the beginning of the MathGlyphInfo table")]
        public Offset16 mathKernInfoOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathItalicsCorrectionInfo
    {
        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table - from the beginning of MathItalicsCorrectionInfo table")]
        public Offset16 italicsCorrectionCoverageOffset;

        [Description(0, "Number of italics correction values. Should coincide with the number of covered glyphs")]
        public uint16 italicsCorrectionCount;

        [Count(0, FieldValueKind.Path, "italicsCorrectionCount")]
        [Description(0, "Array of MathValueRecords defining italics correction values for each covered glyph")]
        public IList<MathValueRecord> italicsCorrection;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathTopAccentAttachment
    {
        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from the beginning of the MathTopAccentAttachment table")]
        public Offset16 topAccentCoverageOffset;

        [Description(0, "Number of top accent attachment point values.Must be the same as the number of glyph IDs referenced in the Coverage table")]
        public uint16 topAccentAttachmentCount;

        [Count(0, FieldValueKind.Path, "topAccentAttachmentCount")]
        [Description(0, "Array of MathValueRecords defining top accent attachment points for each covered glyph")]
        public IList<MathValueRecord> topAccentAttachment;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("MathKernInfo Table")]
    class MathKernInfo
    {
        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from the beginning of the MathKernInfo table")]
        public Offset16 mathKernCoverageOffset;

        [Description(0, "Number of MathKernInfoRecords.Must be the same as the number of glyph IDs referenced in the Coverage table")]
        public uint16 mathKernCount;

        [Count(0, FieldValueKind.Path, "mathKernCount")]
        [Description(0, "Array of MathKernInfoRecords, one for each covered glyph")]
        public IList<MathKernInfoRecord> mathKernInfoRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class MathKernInfoRecord
    {
        [TableType(typeof(MathKern))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to MathKern table for top right corner, from the beginning of the MathKernInfo table.May be NULL")]
        public Offset16 topRightMathKernOffset;

        [TableType(typeof(MathKern))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to MathKern table for the top left corner, from the beginning of the MathKernInfo table.May be NULL")]
        public Offset16 topLeftMathKernOffset;

        [TableType(typeof(MathKern))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to MathKern table for bottom right corner, from the beginning of the MathKernInfo table.May be NULL")]
        public Offset16 bottomRightMathKernOffset;

        [TableType(typeof(MathKern))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to MathKern table for bottom left corner, from the beginning of the MathKernInfo table.May be NULL")]
        public Offset16 bottomLeftMathKernOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathKern
    {
        [Description(0, "Number of heights at which the kern value changes")]
        public uint16 heightCount;

        [Count(0, FieldValueKind.Path, "heightCount")]
        [Description(0, "Array of correction heights, in design units, sorted from lowest to highest")]
        public IList<MathValueRecord> correctionHeight;

        [Count(0, FieldValueKind.Path, "heightCount", "Add:1")]
        [Description(0, "Array of kerning values for different height ranges.Negative values are used to move glyphs closer to each other")]
        public IList<MathValueRecord> kernValues;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathVariants
    {
        [Description(0, "Minimum overlap of connecting glyphs during glyph construction, in design units")]
        public UFWORD minConnectorOverlap;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from the beginning of the MathVariants table")]
        public Offset16 vertGlyphCoverageOffset;

        [TableType(typeof(Coverage))]
        [Description(0, "Offset to Coverage table, from the beginning of the MathVariants table")]
        public Offset16 horizGlyphCoverageOffset;

        [Description(0, "Number of glyphs for which information is provided for vertically growing variants.Must be the same as the number of glyph IDs referenced in the vertical Coverage table")]
        public uint16 vertGlyphCount;

        [Description(0, "Number of glyphs for which information is provided for horizontally growing variants.Must be the same as the number of glyph IDs referenced in the horizontal Coverage table")]
        public uint16 horizGlyphCount;

        [Count(0, FieldValueKind.Path, "vertGlyphCount")]
        [TableType(typeof(MathGlyphConstruction))]
        [Description(0, "Array of offsets to MathGlyphConstruction tables, from the beginning of the MathVariants table, for shapes growing in the vertical direction")]
        public IList<Offset16> vertGlyphConstructionOffsets;

        [Count(0, FieldValueKind.Path, "horizGlyphCount")]
        [TableType(typeof(MathGlyphConstruction))]
        [Description(0, "Array of offsets to MathGlyphConstruction tables, from the beginning of the MathVariants table, for shapes growing in the horizontal direction")]
        public IList<Offset16> horizGlyphConstructionOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MathGlyphConstruction
    {
        [TableType(typeof(GlyphAssembly))]
        [TablePosition("\\", Flags = TablePositionFlag.MayBeNULL)]
        [Description(0, "Offset to the GlyphAssembly table for this shape, from the beginning of the MathGlyphConstruction table.May be NULL")]
        public Offset16 glyphAssemblyOffset;

        [Description(0, "Count of glyph growing variants for this glyph")]
        public uint16 variantCount;

        [Count(0, FieldValueKind.Path, "variantCount")]
        [Description(0, "MathGlyphVariantRecords for alternative variants of the glyphs")]
        public IList<MathGlyphVariantRecord> mathGlyphVariantRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class MathGlyphVariantRecord
    {
        [Description(0, "Glyph ID for the variant")]
        public uint16 variantGlyph;

        [Description(0, "Advance width/height, in design units, of the variant, in the direction of requested glyph extension")]
        public UFWORD advanceMeasurement;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class GlyphAssembly
    {
        [Description(0, "Italics correction of this GlyphAssembly.Should not depend on the assembly size")]
        public MathValueRecord italicsCorrection;

        [Description(0, "Number of parts in this assembly")]
        public uint16 partCount;

        [Count(0, FieldValueKind.Path, "partCount")]
        [Description(0, "Array of part records, from left to right (for assemblies that extend horizontally) or bottom to top (for assemblies that extend vertically)")]
        public IList<GlyphPart> partRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class GlyphPart
    {
        [Description(0, "Glyph ID for the part")]
        public uint16 glyphID;

        [Description(0, "Advance width/ height, in design units, of the straight bar connector material at the start of the glyph in the direction of the extension (the left end for horizontal extension, the bottom end for vertical extension)")]
        public UFWORD startConnectorLength;

        [Description(0, "Advance width/ height, in design units, of the straight bar connector material at the end of the glyph in the direction of the extension(the right end for horizontal extension, the top end for vertical extension)")]
        public UFWORD endConnectorLength;

        [Description(0, "Full advance width/height for this part in the direction of the extension, in design units")]
        public UFWORD fullAdvance;

        [Description(0, "Part qualifiers. PartFlags enumeration currently uses only one bit")]
        public uint16 partFlags;
                                    // 0x0001 fExtender If set, the part can be skipped or repeated.
                                    // 0xFFFE Reserved.
    }

    /*
    OpenType Layout tags for math processing
    OpenType Layout tags used with the MATH Table
    Tag Description
    'math' 	Script tag to be used for features in math layout.The only language system supported with this tag is the default language system.
    'ssty' 	Script Style
            This feature provides glyph variants adjusted to be more suitable for use in subscripts and superscripts.
            These script style forms should not be scaled or moved in the font; scaling and moving them is done by the math-layout engine.Instead, the 'ssty' feature should provide glyph forms that result in shapes that look good as superscripts and subscripts when scaled and positioned by the math engine.When designing the script forms, the font developer may assume that the scriptPercentScaleDown and scriptScriptPercentScaleDown values in the MathConstants table will be scaling factors applied to the size of the alternate glyphs by the math engine.
            This feature can have a parameter indicating the script level: 1 for simple subscripts and superscripts, 2 for second level subscripts and superscripts (that is, scripts on scripts), and so on. (Currently, only the first two alternates are used).
            For glyphs that are not covered by this feature, the original glyph is used in subscripts and superscripts.
            Recommended format: Alternate Substitution table (Single Substitution if there are no second level forms). There should be no context.
    'flac' 	Flattened Accents over Capitals
            This feature provides flattened forms of accents to be used over high-rise bases such as capitals.
            This feature should only change the shape of the accent and should not move it in the vertical or horizontal direction. Moving of the accents is done by the math-layout engine.
            Accents are flattened by the math engine if their base is higher than the flattenedAccentBaseHeight value in the MathConstants table.
            Recommended format: Single Substitution table. There should be no context.
    'dtls' 	Dotless Forms
            This feature provides dotless forms for Math Alphanumeric characters, such as U+1D422 MATHEMATICAL BOLD SMALL I, U+1D423 MATHEMATICAL BOLD SMALL J, U+1D456 U+MATHEMATICAL ITALIC SMALL I, U+1D457 MATHEMATICAL ITALIC SMALL J, and so on.
            The dotless forms are to be used as base forms for placing mathematical accents over them.
            Recommended format: Single Substitution table. There should be no context.
    */
#pragma warning restore IDE1006
}
