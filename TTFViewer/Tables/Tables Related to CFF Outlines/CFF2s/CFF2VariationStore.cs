// ver1.9.1
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("VariationStore")]
    [BaseName("VariationStore")]
    class CFF2VariationStore
    {
        public uint16 length;

        //uint8 data[length] ItemVariationStore data
        public ItemVariationStore itemVariationStore;
    }

#pragma warning restore IDE1006
}
