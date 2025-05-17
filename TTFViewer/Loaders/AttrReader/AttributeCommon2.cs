using System;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    delegate object AttributeMethod(IAttributeService service);
    delegate T AttributeMethodT<T>(IAttributeService service);
    delegate UInt32? PositionAttributeMethod(IAttributeService service);
    delegate IElementList method(IAttributeService service);


    interface IAttributeService
    {
        TableModel TableModel { get; }
        string Path { get; }
        UInt32 FilePosition { get; }
        Type DeclaringType { get; }
        PrimitiveReader PrimitiveReader { get; }
        object[] GetValues(ValueKind valueKind, string valueParameter);
    }


    interface IAttributeReader
    {
        object[] GetValues(AttributeValue value);
        IAttributeService GetAttributeService();
    }
}
