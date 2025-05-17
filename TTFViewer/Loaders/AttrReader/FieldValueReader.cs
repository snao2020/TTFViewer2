using System;
using System.Linq;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.Loaders
{
    class FieldAttributeService : IAttributeService
    {
        public TableModel TableModel => TableLoader.TableModel;
        public string Path => TableLoader.GetFullPath();
        public UInt32 FilePosition => TableLoader.GetFilePosition();
        public Type DeclaringType { get; }
        public PrimitiveReader PrimitiveReader => TableLoader.TableModel.BinaryLoader.GetPrimitiveReader();

        TableLoader TableLoader;

        public FieldAttributeService(TableLoader tableLoader, Type declaringType)
        {
            TableLoader = tableLoader;
            DeclaringType = declaringType;
        }

        public object[] GetValues(ValueKind valueKind, string valueParameter)
        {
            if (valueKind.Value is FieldValueKind fieldValueKind)
            {
                var reader = new FieldValueReader(TableLoader, DeclaringType);
                var result = reader.GetValues(new AttributeValue(fieldValueKind, valueParameter, null));
                return result;
            }
            throw new AccessViolationException("FieldAttributeService.GetValue");
        }
    }


    class FieldValueReader : IAttributeReader
    {
        TableLoader TableLoader;
        Type DeclaringType;

        public FieldValueReader(TableLoader tableLoader, Type declaringType)
        {
            TableLoader = tableLoader;
            DeclaringType = declaringType;
        }

        public IAttributeService GetAttributeService()
        {
            var result = new FieldAttributeService(TableLoader, DeclaringType);
            return result;
        }

        public object[] GetValues(AttributeValue attributeValue)
        {
            if (attributeValue?.ValueKind.Value is FieldValueKind valueKind)// && valueKind != FieldValueKind.None)
            {
                var values = (attributeValue.ValueParameter ?? string.Empty)
                    .Split(',')
                    .Select(i => GetValue(valueKind, i))
                    .Select(i => AttributeHelper.ProcessMath(i, attributeValue.ValueOption))
                    .ToArray();
                return values;
            }
            return null;
        }

        public object GetValue(FieldValueKind valueKind, string valueParam)
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
                    case FieldValueKind.Unsigned:
                        result = AttributeHelper.GetUnsigned(s);
                        break;

                    case FieldValueKind.Enum:
                        result = AttributeHelper.GetEnum(s);
                        break;

                    case FieldValueKind.Tag:
                        result = AttributeHelper.GetTag(s);
                        break;

                    case FieldValueKind.Type:
                        result = AttributeHelper.GetType(s);
                        break;

                    case FieldValueKind.TableValueType:
                        result = AttributeHelper2.GetTableValueType(TableLoader.TableModel, s);
                        break;

                    case FieldValueKind.FontTableType:
                        result = AttributeHelper2.GetFontTableType(TableLoader.TableModel, s);
                        break;

                    case FieldValueKind.FontTableValue:
                        result = AttributeHelper2.GetFontTableValue(TableLoader.TableModel, s);
                        break;

                    case FieldValueKind.PeekValue:
                        result = AttributeHelper2.CreatePeekObject(TableLoader.TableModel.BinaryLoader, TableLoader.GetFilePosition(), s);
                        break;

                    case FieldValueKind.OffsetSource:
                        result = AttributeHelper2.GetOffsetSourceValue(TableLoader.TableModel, s);
                        break;

                    case FieldValueKind.Path:
                        result = TableLoader.GetPathValue(s);
                        break;

                    case FieldValueKind.ElementList:
                        result = TableLoader.GetElementList();
                        break;

                    case FieldValueKind.FilePositionNull:
                        result = TableLoader.GetFilePositionNull(s);
                        break;

                    case FieldValueKind.ParentConstraint:
                        result = TableLoader.GetParentConstraintLength();
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
