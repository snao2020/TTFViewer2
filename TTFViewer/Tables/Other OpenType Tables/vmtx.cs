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
    [FontTable("vmtx")]
    [ClassTypeSelect(ClassValueKind.FontTableValue, "vhea\\numOfLongVerMetrics, maxp\\numGlyphs", null)]
    [ClassTypeCondition(typeof(vmtxTable), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "<any>, <any>")]
    [Invalid]
    [TypeName("vmtx — Vertical Metrics Table")]
    [BaseName("vmtx")]
    class vmtxTableInvalid
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("vmtx — Vertical Metrics Table")]
    [BaseName("vmtx")]
    class vmtxTable
    {
        [Count(0, FieldValueKind.FontTableValue, "vhea\\numOfLongVerMetrics")]
        public IList<vMetrics> vMetricsArray;

        [Count(0, "topSideBearingMethod")]
        [Description(0, "The top sidebearing of the glyph.Signed integer in FUnits.")]
        public IList<FWORD> topSideBearing;

        static Int32 topSideBearingMethod(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.FontTableValue, "vhea\\numOfLongVerMetrics, maxp\\numGlyphs");
            if (values[0] is uint16 numOfLongVerMetrics
                && values[1] is uint16 numGlyphs)
            {
                return (Int32)(numGlyphs - numOfLongVerMetrics);
            }
            return 0;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class vMetrics
    {
        [Description(0, "The advance height of the glyph.Unsigned integer in FUnits")]
        public UFWORD advanceHeight;

        [Description(0, "The top sidebearing of the glyph.Signed integer in FUnits.")]
        public FWORD topSideBearing;
    }
#pragma warning restore IDE1006
}
