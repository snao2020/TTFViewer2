#define COLLAPSEDTREE
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TTFViewer.DataTypes;
using TTFViewer.Model;

namespace TTFViewer.ViewModel
{
#if COLLAPSEDTREE
    class CollapsedTree
    {
        public bool Collapsed;
        public List<CollapsedTree> Children;
    }
#endif

    class ValueProvider : IItemValueService
    {
        ItemViewModel ItemViewModel;

        public ValueProvider(ItemViewModel ivm)
            => ItemViewModel = ivm;

        public object Value => ItemViewModel.Value;

        public UInt32 FilePosition => ItemViewModel.FilePosition;

        public UInt32 FileLength => ItemViewModel.FileLength;

        public IItemValueService Parent
        {
            get
            {
                var parent = ItemViewModel.Parent;
                while(parent is GroupItemViewModel)
                    parent = parent.Parent;
                return parent?.ItemValueProvider;
            }
        }

        public IGroupContainer GroupContainer
        {
            get
            {
                if(ItemViewModel.Parent is GroupItemViewModel group)
                {
                    return group.Value as IGroupContainer;
                }
                return null;
            }
        }

        public string Name => ItemViewModel.Name;

        public Type ValueType => ItemViewModel.ValueType;

        public bool IsTableModel => ItemViewModel is TTFItemViewModel;

        public object GetFontTableValue(Tag tag, string path)
        {
            return ItemViewModel.GetFontTableValue(tag, path);
        }
        
        public object LoadValue(UInt32 filePosition, Type type)
        {
            return ItemViewModel.LoadValue(filePosition, type);
        }        
    }


    abstract class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        protected static readonly ItemViewModel CollapsedItemViewModel = new CollapsedItemViewModel(null);

#if COLLAPSEDTREE
        List<CollapsedTree> CollapsedTree;
        
        public bool IsExpanded
        {
            get { return CollapsedTree == null; }
            set
            {
                if (IsLeaf())
                    return;
                if (value != (CollapsedTree == null))
                {
                    if (value)
                    {
                        LoadFromCollapsedTree();
                    }
                    else
                    {
                        SaveToCollapsedTree();
                    }
                    RaisePropertyChanged("IsExpanded");
                }
            }
        }
#else
        bool _IsExpanded;
        public bool IsExpanded
        {
            get => _IsExpanded;
            set
            {
                if (IsLeaf())
                    return;
                if(value != _IsExpanded)
                {
                    _IsExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                    Children = null; // reload
                }
            }
        }

#endif

        public ItemViewModel Parent { get; }
        public UInt32 FilePosition { get; }
        public abstract UInt32 FileLength { get; }
        public abstract string Name { get; }
        protected virtual TableModel Model => Parent?.Model;

        public IItemValueService ItemValueProvider { get; }

        public abstract Type ValueType { get; } // no throw
        public abstract object Value { get; }

        public abstract string Text { get; }
        public abstract bool IsValid { get; }
        public abstract string Description { get; }

        public abstract IList<ItemViewModel> ChildList { get; }


        IList<ItemViewModel> _Children;
        public IList<ItemViewModel> Children
        {
            get
            {
                IList<ItemViewModel> result = null;
                if (!IsLeaf())
                {
                    if (_Children == null)
                    {
                        if (IsExpanded)
                            _Children = ChildList;
                        else
                            _Children = new List<ItemViewModel>() { CollapsedItemViewModel };
                    }
                    result = _Children;
                }
                return result;
            }

            protected set
            {
                if (!IsLeaf())
                {
                    if (value != _Children)
                    {
                        _Children = value;
                        RaisePropertyChanged("Children");
                    }
                }
            }
        }


        public string GetPathString()
        {
            string result = "";
            if(!(Parent is TTFItemViewModel))
                result = Parent.GetPathString();

            if (!string.IsNullOrEmpty(result))
                result += $"\\{Name}";
            else
                result = Name;
            return result;
        }

        protected ItemViewModel(ItemViewModel parent, UInt32 filePosition)
        {
            Parent = parent;
            FilePosition = filePosition;
#if COLLAPSEDTREE
            CollapsedTree = new List<CollapsedTree>();
#endif
            ItemValueProvider = new ValueProvider(this);
        }

        
        public object LoadValue(UInt32 filePosition, Type type)
        {
            //return Model.BinaryLoader.CreatePrimitive(type, filePosition);
            return Model.BinaryLoader.GetPrimitiveReader().Read(type, filePosition);
        }


        public object GetFontTableNoThrow(Tag tag)
        {
            object result = null;
            try
            {
                TableModel model = Model;
                if (model != null)
                    result = TableModelHelper.CreateFontTable(model, tag);
            }
            catch
            {
            }
            return result;
        }


        public object GetFontTableValue(Tag tag, string path)
        {
            object result = null;
            try
            {
                TableModel model = Model;
                if (model != null)
                    result = TableModelHelper.GetFontTableValue(model, tag, path);
            }
            catch
            {
            }
            return result;
        }


        bool IsLeaf()
        {
            bool result = false;

            TableModel model = Model;
            var valueType = ValueType;

            if (model == null)      // for MessageItemViewModel
                result = true;
            else if (valueType == typeof(object))
                return true;
            else if (model.CreateModelFlags.HasFlag(CreateModelFlags.Invalid))
                return true;

            else if (model.Parent != null) // not RootItemModel
            {
                if (valueType == null)
                    result = true;
                else if (typeof(ITTFPrimitive).IsAssignableFrom(valueType))
                {
                    if (valueType != typeof(Offset16) && valueType != typeof(Offset32) && valueType != typeof(Offset24))
                        result = true;
                }

                // TTFItemViewModelの場合、ValueType!=null && Value==nullでも表示は必要なのでleafでは無い
                // FieldViewModelの場合、ValueTypeはValueから取得しているので
                // Value==nullはValueType==nullと等しい
                // よってこの判定は不要
                //else if (Value == null)
                //    return true;

                else if (!TypeHelper.IsArrayOrIListT(valueType))
                    return valueType.GetFields(BindingFlags.Public | BindingFlags.Instance).Length == 0;
            }
            return result;
        }


#if COLLAPSEDTREE
        void SaveToCollapsedTree()
        {
            if (Children != null)
            {
                var collapsedTree = new List<CollapsedTree>();

                foreach (ItemViewModel tnv in Children)
                {
                    bool collapsed = !tnv.IsExpanded;
                    if (!collapsed)
                        tnv.SaveToCollapsedTree();

                    var item = new CollapsedTree()
                    {
                        Collapsed = collapsed,
                        Children = tnv.CollapsedTree
                    };
                    collapsedTree.Add(item);
                }

                CollapsedTree = collapsedTree;
                Children = null; // reload Children
            }
        }


        void LoadFromCollapsedTree()
        {
            var collapsedTree = CollapsedTree;
            CollapsedTree = null;
            Children = null; // reload Children

            if (collapsedTree != null && collapsedTree.Count > 0)
            {
                var children = Children;
                for (int i = 0; i < children.Count; i++)
                {
                    var ct = collapsedTree[i];
                    children[i].CollapsedTree = ct.Children;
                    if (!ct.Collapsed)
                        children[i].IsExpanded = true;
                }
            }
        }
#endif
    }


    //-----------------------------------------------

    class TTFItemViewModel : ItemViewModel
    {
        UInt32? _FileLength;
        public override UInt32 FileLength
        {
            get
            {
                if(_FileLength == null)
                if (Model.FileLength is UInt32 u32)
                    _FileLength = u32;
                else
                    _FileLength = Model.BinaryLoader.GetTableLoader(Model).GetFileLength();
                return (UInt32)_FileLength;
            }
        }

        public override string Name
        {
            get
            {
                if (Model.Parent == null)
                    return "";
                else
                {
                    Type valueType = ValueType;
                    if (valueType != null)
                        return BaseNameAttribute.GetName(valueType);
                    else
                        return null;
                }
                    
            }
        }

        protected override TableModel Model { get; }
        public override Type ValueType => Model?.ValueType;
        public override object Value => Model?.GetValue();

        public override string Text
        {
            get
            {
                var provider = new ItemValueProvider(ItemValueProvider);
                var text = provider.GetTypeName();
                if (Model.ElementCount is Int32 count)
                    text += $" (Count={count})";
                return text;
            }
        }


        public override bool IsValid
        {
            get
            {
                if (ValueType == null)
                    return true;
                bool ret = AttributeHelper.GetAttribute<InvalidAttribute>(ValueType) != null;
                if (ret)
                    return false;
                return true;
            }
        }


        public override string Description
        {
            get
            {
                var provider = new ItemValueProvider(ItemValueProvider);
                var text = provider.GetDescription();
                if (!String.IsNullOrEmpty(text))
                    return $" ({text} FilePosition=0x{FilePosition:X8})";
                else
                    return $" (FilePosition=0x{FilePosition:X8})";
            }
        }


        public override IList<ItemViewModel> ChildList
        {
            get
            {
                if(ValueType == typeof(RootTable))
                {
                    var childModel = TableModelHelper.GetChildModel(Model, "Offset"); // pathString any
                    return new List<ItemViewModel>
                    {
                        new TTFItemViewModel(this, childModel),
                    };
                }
                else
                {
                    var valueType = ValueType;
                    object value = Value;
                    if (value is Array array)
                    {
                        var list = new List<ItemViewModel>();
                        UInt32 fp = Model.FilePosition;
                        for (int i = 0; i < array.Length; i++)
                        {
                            object elementValue = array.GetValue(i);
                            string name = $"[{i}]";
                            list.Add(new FieldViewModel(elementValue.GetType(), name, elementValue, this, fp));
                            fp += TypeHelper.CalcSize(elementValue);
                        }
                        return list;
                    }
                    else if (value is ITTFPrimitive)
                    {
                        var list = new List<ItemViewModel>
                        {   
                            new FieldViewModel(value.GetType(), "\\Item", value, this, Model.FilePosition),
                        };
                        return list;
                    }
                    else if (value is IElementList iel)
                        return ItemViewModelHelper.CreateLazyLoadChildren(this, iel);
                    else if (value != null && value.GetType().IsClass)
                    {
                        return ItemViewModelHelper.CreateModelValues(this, value, Model);
                    }
                    else
                        return null;
                }
            }
        }


        public TTFItemViewModel(ItemViewModel parent, TableModel model)
            : base(parent, model.FilePosition)
        {
            Model = model;
        }
    }


    class FieldViewModel : ItemViewModel
    {
        public override string Name { get; }

        public override string Text
        {
            get
            {
                var provider = new ItemValueProvider(ItemValueProvider);

                String typeText = provider.GetTypeName();

                var fieldText = provider.GetFieldName();

                String valueText = provider.GetValueText();
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
                if (Value != null)
                {
                    var provider = new ItemValueProvider(ItemValueProvider);
                    var text = provider.GetDescription();
                    if (String.IsNullOrEmpty(text))
                        return $" (FilePosition=0x{FilePosition:X8})";
                    else
                        return $" ({text} FilePosition=0x{FilePosition:X8})";
                }
                return null;
            }
        }


        public override IList<ItemViewModel> ChildList
        {
            get
            {
                var valueType = ValueType;
                if (valueType == typeof(Offset32) || valueType == typeof(Offset24) || valueType == typeof(Offset16))
                {
                    var childModel = TableModelHelper.GetChildModel(Model, GetPathString());
                    return new List<ItemViewModel>
                    {
                        (new TTFItemViewModel(this, childModel)),
                    };
                }
                else
                {
                    var result = ItemViewModelHelper.CreateGroup(this);
                    if (result != null)
                        return result;

                    else if (typeof(IElementList).IsAssignableFrom(valueType))
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
        }



        UInt32? _FileLength;
        public override UInt32 FileLength
        {
            get
            {
                if (ElementList != null)
                {
                    if(_FileLength == null)
                        _FileLength = ElementList.GetElementLength(Index);
                }
                return (UInt32)_FileLength;
            }
        }

        Type _ValueType;
        public override Type ValueType // => FieldValue?.GetType();
        {
            get
            {
                if (ElementList != null)
                {
                    if (_Value != null)
                        _ValueType = Value.GetType();
                    else if (_ValueType == null)
                    {
                        // ElementListからでは無く、Valueの右辺値の型を求める
                        if (Value != null)
                            _ValueType = Value.GetType();
                        else
                            _ValueType = ElementList.GetElementType(Index);
                    }

                }
                else if (_ValueType == null)
                    _ValueType = Value?.GetType();
                return _ValueType;
            }
        }
    

        object _Value;
        public override object Value
        {
            get
            {
                if (ElementList != null)
                {
                    if (_Value == null)
                    {
                        if(ElementList is IList list)
                        _Value = list[Index];
                    }
                }
                return _Value;
            }
        }


        IElementList ElementList;
        Int32 Index;


        public FieldViewModel(IElementList iel, Int32 index, ItemViewModel parent)
            : base(parent, iel.GetElementPosition(index))
        {
            Name = $"[{index}]";
            ElementList = iel;
            Index = index;
        }


        public FieldViewModel(string fieldName, object value, ItemViewModel parent, UInt32 filePosition)
            : base(parent, filePosition)
        {
            Name = fieldName;
            _Value = value;
            _FileLength = TypeHelper.CalcSize(value);
        }
        
        public FieldViewModel(Type fieldType, string fieldName, object fieldValue, ItemViewModel parent, UInt32 filePosition)
            : base(parent, filePosition)
        {
             Name = fieldName;
            _Value = fieldValue;
            _FileLength = TypeHelper.CalcSize(fieldValue);
        }
    }


    class GroupItemViewModel : ItemViewModel
    {
        public override string Text
        {
            get
            {
                Int32 axis = ItemViewModelHelper.GetGroupAxis(Parent);
                var ivp = new ItemValueProvider(ItemValueProvider);
                var text = ivp.GetGroupText(axis);
                return text;
            }
        }


        public override string Description
        {
            get
            {
                Int32 axis = ItemViewModelHelper.GetGroupAxis(Parent);
                var ivp = new ItemValueProvider(ItemValueProvider);
                var text = ivp.GetGroupDescription(axis);
                if (text != null)
                    text = $" ({text})";
                return text;
            }
        }


        public override IList<ItemViewModel> ChildList
        {
            get
            {
                var result = ItemViewModelHelper.CreateGroup(this);
                if (result != null)
                    return result;

                else if (typeof(IElementList).IsAssignableFrom(ValueType))
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


        public override UInt32 FileLength { get; }
        public override Type ValueType => Value?.GetType();
        public override object Value { get; }
        public override bool IsValid => true;
        public override string Name { get; }

        public GroupItemViewModel(ItemViewModel parent, UInt32 filePosition, UInt32 fileLength, string name, Object value)
            : base(parent, filePosition)
        {
            Name = name;
            FileLength = fileLength;
            Value = value;
        }
    }


    class CollapsedItemViewModel : ItemViewModel
    {
        public override UInt32 FileLength { get; }
        public override Type ValueType => null;
        public override object Value => null;
        public override string Text => null;
        public override bool IsValid => false;
        public override string Description => null;
        public override IList<ItemViewModel> ChildList => null;
        public override string Name => ""; // BaseNameAttribute.GetName(typeof(TTFError));


        public CollapsedItemViewModel(ItemViewModel parent)
            : base(parent, UInt32.MaxValue)
        {
        }
    }
}
