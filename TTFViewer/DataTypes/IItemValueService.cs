using System;

namespace TTFViewer.DataTypes
{
    interface IGroupContainer
    {
        IElementList SourceList { get; }
        Int32 FirstIndex { get; }
        Int32 ItemCount { get; }
    }


    interface IItemValueService
    {
        object Value { get; }
        UInt32 FilePosition { get; }
        UInt32 FileLength { get; }
        IItemValueService Parent { get; }
        IGroupContainer GroupContainer { get; }
        string Name { get; }
        Type ValueType { get; }
        bool IsTableModel { get; }
        object GetFontTableValue(Tag tag, string path);
        object LoadValue(UInt32 filePosition, Type type);
    }
}
