// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("SVG ")]
    [TypeName("SVG — The SVG (Scalable Vector Graphics) table")]
    [BaseName("SVG")]
    class SVGTable
    {
        [FieldName(0, null)]
        public SVGHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "version", null)]
    [ClassTypeCondition(typeof(SVGHeaderVersion0), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [Invalid]
    [TypeName("SVG Header")]
    [BaseName("SVGHeader")]
    class SVGHeader
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        [Description(0, "Table version(starting at 0). Set to 0")]
        public uint16 version;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("SVG Table Header")]
    class SVGHeaderVersion0 : SVGHeader
    {
        //public uint16 version; // Table version(starting at 0). Set to 0.

        [TableType(typeof(SVGDocumentList))]
        [Description(0, "Offset to the SVGDocumentList,from the start of the SVG table.Must be non-zero")]
        public Offset32 svgDocumentListOffset;

        [Description(0, "Set to 0")]
        public uint32 reserved;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SVGDocumentList
    {
        [Description(0, "Number of SVG document records.Must be non-zero")]
        public uint16 numEntries;

        [Count(0, FieldValueKind.Path, "numEntries")]
        [Description(0, "Array of SVG document records")]
        public IList<SVGDocumentRecord> documentRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class SVGDocumentRecord
    {
        [Description(0, "The first glyph ID for the range covered by this record")]
        public uint16 startGlyphID;

        [Description(0, "The last glyph ID for the range covered by this record")]
        public uint16 endGlyphID;

        [TableType(typeof(SVGDocument))]
        [TableLength(TableLengthKind.FileLength, FieldValueKind.Path, "svgDocLength")]
        [Description(0, "Offset from the beginning of the SVGDocumentList to an SVG document.Must be non-zero")]
        public Offset32 svgDocOffset;

        [Description(0, "Length of the SVG document data. Must be non-zero")]
        public uint32 svgDocLength;
    }

    /*
    FieldValue tags, in alphabetical order of tags:
    Tag     Mnemonic    FieldValue represented
    'cpht' 	cap height  OS/2.sCapHeight
    'gsp0' 	gaspRange[0] gasp.gaspRange[0].rangeMaxPPEM
    'gsp1' 	gaspRange[1] gasp.gaspRange[1].rangeMaxPPEM
    'gsp2' 	gaspRange[2] gasp.gaspRange[2].rangeMaxPPEM
    'gsp3' 	gaspRange[3] gasp.gaspRange[3].rangeMaxPPEM
    'gsp4' 	gaspRange[4] gasp.gaspRange[4].rangeMaxPPEM
    'gsp5' 	gaspRange[5] gasp.gaspRange[5].rangeMaxPPEM
    'gsp6' 	gaspRange[6] gasp.gaspRange[6].rangeMaxPPEM
    'gsp7' 	gaspRange[7] gasp.gaspRange[7].rangeMaxPPEM
    'gsp8' 	gaspRange[8] gasp.gaspRange[8].rangeMaxPPEM
    'gsp9' 	gaspRange[9] gasp.gaspRange[9].rangeMaxPPEM
    'hasc' 	horizontal ascender     OS/2.sTypoAscender
    'hcla' 	horizontal clipping ascent OS/2.usWinAscent
    'hcld' 	horizontal clipping descent OS/2.usWinDescent
    'hcof' 	horizontal caret offset hhea.caretOffset
    'hcrn' 	horizontal caret run hhea.caretSlopeRun
    'hcrs' 	horizontal caret rise hhea.caretSlopeRise
    'hdsc' 	horizontal descender    OS/2.sTypoDescender
    'hlgp' 	horizontal line gap OS/2.sTypoLineGap
    'sbxo' 	subscript em x offset   OS/2.ySubscriptXOffset
    'sbxs' 	subscript em x size     OS/2.ySubscriptXSize
    'sbyo' 	subscript em y offset   OS/2.ySubscriptYOffset
    'sbys' 	subscript em y size     OS/2.ySubscriptYSize
    'spxo' 	superscript em x offset     OS/2.ySuperscriptXOffset
    'spxs' 	superscript em x size   OS/2.ySuperscriptXSize
    'spyo' 	superscript em y offset     OS/2.ySuperscriptYOffset
    'spys' 	superscript em y size   OS/2.ySuperscriptYSize
    'stro' 	strikeout offset    OS/2.yStrikeoutPosition
    'strs' 	strikeout size  OS/2.yStrikeoutSize
    'undo' 	underline offset    post.underlinePosition
    'unds' 	underline size  post.underlineThickness
    'vasc' 	vertical ascender   vhea.ascent
    'vcof' 	vertical caret offset vhea.caretOffset
    'vcrn' 	vertical caret run vhea.caretSlopeRun
    'vcrs' 	vertical caret rise vhea.caretSlopeRise
    'vdsc' 	vertical descender  vhea.descent
    'vlgp' 	vertical line gap vhea.lineGap
    'xhgt' 	x height    OS/2.sxHeight
    */


//--------------------------------------------------------------------

    class SVGDocument
    {
    }

#pragma warning restore IDE1006
}
