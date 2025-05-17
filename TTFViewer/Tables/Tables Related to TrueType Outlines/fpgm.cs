// ver 1.9.1
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("fpgm")]
    [TypeName("fpgm — Font Program")]
    [BaseName("fpgm")]
    class fpgmTable
    {
        [FieldName(0, null)]
        [Length(0, FieldValueKind.ParentConstraint, null, null)]
        [Description(0, "n is the number of uint8 items that fit in the size of the table.")]
        [Description(1, DescriptionKind.Instruction, null)]
        public IList<uint8> Instructions;
    }
#pragma warning restore IDE1006
}
