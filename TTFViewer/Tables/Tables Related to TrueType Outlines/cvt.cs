// var 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("cvt ")]
    [TypeName("cvt — Control Value Table")]
    [BaseName("cvt")]
    class cvtTable
    {
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [FieldName(0, null)]
        [Description(0, "number of FWORD items that fit in the size of the table.")]
        public IList<FWORD> ControlValue;
    }

#pragma warning restore IDE1006
}
