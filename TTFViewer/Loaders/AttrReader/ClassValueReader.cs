using System;
using System.Linq;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class ClassAttributeService : IAttributeService
    {
        public TableModel TableModel { get; }

        public string Path => null;

        public UInt32 FilePosition { get; }

        public Type DeclaringType { get; }

        public PrimitiveReader PrimitiveReader => TableModel.BinaryLoader.GetPrimitiveReader();

        public ClassAttributeService(TableModel tableModel, UInt32 filePosition, Type declaringType)
        {
            TableModel = tableModel;
            FilePosition = filePosition;
            DeclaringType = declaringType;
        }

        public object[] GetValues(ValueKind valueKind, string valueParameter)
        {
            if (valueKind.Value is ClassValueKind classValueKind)
            {
                var reader = new ClassValueReader(TableModel, FilePosition, DeclaringType);
                var result = reader.GetValues(new AttributeValue(classValueKind, valueParameter, null));
                return result;
            }
            throw new AccessViolationException("FieldAttributeService.GetValue");
        }
    }


    class ClassValueReader : IAttributeReader
    {
        TableModel TableModel;
        UInt32 FilePosition;
        Type DeclaringType;


        public ClassValueReader(TableModel tableModel, UInt32 filePosition, Type declaringType)
        {
            TableModel = tableModel;
            FilePosition = filePosition;
            DeclaringType = declaringType;
        }


        public IAttributeService GetAttributeService()
        {
            return new ClassAttributeService(TableModel, FilePosition, DeclaringType);
        }


        public object[] GetValues(AttributeValue value)
        {
            if (value?.ValueKind.Value is ClassValueKind valueKind)
            {
                var values = (value.ValueParameter ?? string.Empty)
                    .Split(',')
                    .Select(i => GetValue(valueKind, i))
                    .Select(i => AttributeHelper.ProcessMath(i, value.ValueOption))
                    .ToArray();
                return values;
            }
            return null;
        }


        object GetValue(ClassValueKind valueKind, string valueParam)
        {
            object result;

            var s = valueParam?.Trim();
            if (s == "<null>")
                result = null;
            else if (s == "<any>")
                result = new object();
            else
            {
                switch (valueKind)
                {
                    case ClassValueKind.Unsigned:
                        result = AttributeHelper.GetUnsigned(s);
                        break;

                    case ClassValueKind.Enum:
                        result = AttributeHelper.GetEnum(s);
                        break;

                    case ClassValueKind.Tag:
                        result = AttributeHelper.GetTag(s);
                        break;

                    case ClassValueKind.Type:
                        result = AttributeHelper.GetType(s);
                        break;

                    case ClassValueKind.TableValueType:
                        result = AttributeHelper2.GetTableValueType(TableModel, s);
                        break;

                    case ClassValueKind.FontTableType:
                        result = AttributeHelper2.GetFontTableType(TableModel, s);
                        break;

                    case ClassValueKind.FontTableValue:
                        result = AttributeHelper2.GetFontTableValue(TableModel, s);
                        break;

                    //case ClassValueKind.PeekValue:
                    //    result = AttributeHelper2.CreatePeekObject(TableModel.BinaryLoader, FilePosition, s);
                    //    break;

                    case ClassValueKind.FieldPath:
                        result = AttributeHelper2.CreateFieldObject(TableModel.BinaryLoader, FilePosition, DeclaringType, s);
                        break;

                    default:
                        result = null;
                        break;
                }
            }
            return result;
        }
    }
}
