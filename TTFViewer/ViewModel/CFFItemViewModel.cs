using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TTFViewer.DataTypes;
using TTFViewer.Model;
using TTFViewer.Tables;

namespace TTFViewer.ViewModel
{
    class CFFItemViewModel : ItemViewModel
    {
        uint _FileLength;
        public override uint FileLength => _FileLength;

        string _Name;
        public override string Name => _Name;

        public override Type ValueType => _Value?.GetType();

        object _Value;
        public override object Value => _Value;

        public override string Text
        {
            get
            {
                var provider = new ItemValueProvider(ItemValueProvider);

                String typeText = provider.GetTypeName();

                var fieldText = provider.GetFieldName();

                String valueText = null;
                valueText = provider.GetValueText();
                if (valueText == typeText)
                    valueText = null;

                if (fieldText != null && valueText != null)
                    return $"{typeText} {fieldText}={valueText}";
                else
                    return $"{typeText} {fieldText} {valueText}";
            }
        }

        public override bool IsValid
        {
            get
            {
                if (ValueType == null)
                    return true;
                else
                {
                    var provider = new ItemValueProvider(ItemValueProvider);
                    return !provider.IsInvalid();
                }
            }
        }


        public override string Description
        {
            get
            {
                var provider = new ItemValueProvider(ItemValueProvider);
                var text = provider.GetDescription();
                if (String.IsNullOrEmpty(text))
                    return $" (FilePosition=0x{FilePosition:X8})";
                else
                    return $" ({text} FilePosition=0x{FilePosition:X8})";
            }
        }

        public override IList<ItemViewModel> ChildList
        {
            get
            {
                var valueType = ValueType;
                if (typeof(IElementList).IsAssignableFrom(valueType))
                {
                    var iel = Value as IElementList;
                    return ItemViewModelHelper.CreateLazyLoadChildren(this, iel);
                }
                else
                {
                    return ItemViewModelHelper.CreateFieldChildren(this, Value);
                }
            }
        }


        public CFFItemViewModel(/*FieldInfo fi, */string name, object value, ItemViewModel parent, UInt32 filePosition)
            : base(parent, filePosition)
        {
            _Name = name;
            _Value = value;
            _FileLength = TypeHelper.CalcSize(value);
        }

        public CFFItemViewModel(FieldInfo fi, List<Int32> indexes, object value, ItemViewModel parent, UInt32 filePosition)
            : base(parent, filePosition)
        {
            _Name = fi.Name;
            _Value = value;
            _FileLength = TypeHelper.CalcSize(value);
        }
    }
}
