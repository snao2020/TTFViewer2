using System;
using TTFViewer.DataTypes;

namespace TTFViewer.Model
{
    public class ChildParam
    {
        public string Path; // parentModel to Offset32/Offset!6
        public UInt32 FilePosition;
        public UInt32? FileLength;  // if null, sum all field sizes
        public Int32? ElementCount;
        public Type BaseType;
        public CreateModelFlags CreateModelFlags;
    }
}
