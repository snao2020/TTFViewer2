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
    [FontTable("hmtx")]
    [TypeName("hmtx — Horizontal Metrics Table")]
    [BaseName("hmtx")]
    class hmtxTable
    {
        [Count(0, FieldValueKind.FontTableValue, "hhea\\numberOfHMetrics")]
        [Description(0, "Paired advance width and left side bearing values for each glyph.Records are indexed by glyph ID.")]
        public IList<LongHorMetric> hMetrics;

        [Count(0, "leftSideBearingsMethod")]
        [Description(0, "Left side bearings for glyph IDs greater than or equal to numberOfHMetrics")]
        public IList<FWORD> leftSideBearings;

        private static Int32 leftSideBearingsMethod(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.FontTableValue, "hhea\\numberOfHMetrics, maxp\\numGlyphs");
            if (values[0] is uint16 numberOfHMetrics
                && values[1] is uint16 numGlyphs)
            {
                return numGlyphs - numberOfHMetrics;
            }
            else
                return 0;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class LongHorMetric
    {
        [Description(0, "Advance width, in font design units.")]
        public UFWORD advanceWidth;

        [Description(0, "Glyph left side bearing, in font design units")]
        public FWORD lsb;
    }
#pragma warning restore IDE1006
}
