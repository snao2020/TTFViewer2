using System;

namespace TTFViewer.DataTypes
{
    [Flags]
    public enum CreateModelFlags
    {
        None = 0,
        Buffered = 1,
        Debugging = 2,
        Invalid = 4,    // ValueType == null or NulTable or NotImplementedFontTable
    }
}
