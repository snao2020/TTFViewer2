using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TTFViewer.DataTypes;
using TTFViewer.Tables;

namespace TTFViewer.ViewModel
{
    class ItemValueProvider
    {
        IItemValueService IVS;
        public Int32 Axis { get; }
        FieldInfo FieldInfo;


        public ItemValueProvider(IItemValueService ivp)
        {
            IVS = ivp;
            var ret = GetAncestorInfo(ivp);
            Axis = ret.Item3;
            var ancestorIVP = ret.Item1;
            if (ancestorIVP != null)
            {
                var declaringType = ancestorIVP.ValueType;
                var fieldName = ret.Item2;
                if (fieldName != null)
                {
                    FieldInfo = declaringType.GetField(fieldName);
                }
                else if (TypeHelper.IsArrayOrIListT(declaringType))
                {
                    var ret2 = GetAncestorInfo(ancestorIVP.Parent);
                    var valueType = declaringType;
                    var offsetDeclaringType = ret2.Item1.ValueType;
                    var offsetFieldName = ret2.Item2;
                    FieldInfo = ProxyAttribute.FindTarget(offsetDeclaringType, offsetFieldName, valueType);
                }
                else
                {
                    //ClassType = declaringType;
                }
            }
        }


        public bool IsInvalid()
        {
            bool result = ReadInvalidAttribute();
            return result;
        }


        bool ReadInvalidAttribute()
        {
            bool result = false;

            var attr = AttributeHelper.GetAttribute<InvalidAttribute>(FieldInfo, Axis)
                    ?? AttributeHelper.GetAttribute<InvalidAttribute>(IVS.ValueType);

            if (attr != null)
            {
                if (attr.MethodName != null)
                {
                    if (AttributeHelper.GetMethod(GetDeclaringType(), attr.MethodName, typeof(IsInvalidMethod)) is IsInvalidMethod method)
                        result = method(IVS);
                }
                else
                    result = true;
            }
            return result;
        }

        public Type GetLeftType()
        {
            var leftType = FieldInfo?.FieldType ?? IVS.ValueType;
            return leftType;
        }

        public String GetTypeName()
        {
            var name = DoGetTypeName(searchInternal: false);
            if (name == String.Empty)
                name = null;
            return name;
        }

        // Get LeftValue type name by fieldinfo
        String DoGetTypeName(bool searchInternal)
        {
            var leftType = FieldInfo?.FieldType ?? IVS.ValueType;
            var elementTypeRank = TypeHelper.GetElementTypeRank(leftType);

            // first try: get name from attribute of fieldInfo
            var name = ReadTypeNameAttribute(FieldInfo);

            // second try: get name from attribute of element type of fieldinfo
            if (name == null || searchInternal && name == String.Empty)
                name = ReadTypeNameAttribute(elementTypeRank.Item1);

            // third try: get name of element type of fieldInfo
            if (name == null || searchInternal && name == String.Empty)
                name = elementTypeRank.Item1.Name;

            var result = FormatText(name);

            if (!String.IsNullOrEmpty(result))
            {
                Int32 dim = Math.Max(0, elementTypeRank.Item2 - Axis);
                result += String.Concat(Enumerable.Repeat("[]", dim).ToArray());
            }
            return result;
        }


        // Get RightValue type name by IVS.ValueType
        String GetRightValueTypeName()
        {
            var name = ReadTypeNameAttribute(IVS.ValueType);
            if (name == null)
                name = IVS.ValueType?.Name;
            return FormatText(name);
        }


        String ReadTypeNameAttribute(MemberInfo mi)
        {
            String name = null;

            var attr = AttributeHelper.GetAttribute<TypeNameAttribute>(mi);
            if (attr != null)
            {
                switch (attr.TypeNameKind)
                {
                    case TypeNameKind.Text:
                        name = attr.Name;
                        break;
                    case TypeNameKind.Method:
                        if (mi is FieldInfo)
                        {
                            if (AttributeHelper.GetMethod(GetDeclaringType(), attr.Name, typeof(IVPMethod)) is IVPMethod method)
                                name = method(IVS);
                        }
                        else if (mi is Type t)
                        {
                            if (AttributeHelper.GetMethod(t, attr.Name, typeof(IVPMethod)) is IVPMethod method)
                                name = method(IVS);
                        }
                        break;
                }
            }
            return name;
        }


        public String GetFieldName()
        {
            String result = null;

            var attr = AttributeHelper.GetAttribute<FieldNameAttribute>(FieldInfo, Axis);
            if (attr != null)
            {
                switch (attr.FieldNameKind)
                {
                    case FieldNameKind.Text:
                        result = attr.Text;
                        break;

                    case FieldNameKind.Method:
                        if (AttributeHelper.GetMethod(GetDeclaringType(), attr.Text, typeof(IVPMethod)) is IVPMethod method)
                            result = method(IVS);
                        break;
                }
            }
            else
            {
                result = IVS.Name;
            }

            result = FormatText(result);
            if (IVS.Value is IList list)
            {
                result += $" (Count={list.Count})";
            }
            else if (IVS.Value != null && IVS.Value.GetType().IsClass)
            {
                var leftValueTypeName = DoGetTypeName(true);
                var rightValueTypeName = GetRightValueTypeName();
                if (rightValueTypeName != leftValueTypeName)
                    result += $" ({rightValueTypeName})";
            }
            return result;
        }


        public String GetValueText()
        {
            var ret = ReadValue();
            var result = ret.name;
            var option = ret.option;

            if (!TypeHelper.IsArrayOrIListT(IVS.ValueType) && FieldInfo != null)
            {
                if (option.HasFlag(ValueFormatOption.RawType))
                {
                    var leftValueType = TypeHelper.GetElementTypeRank(FieldInfo.FieldType).Item1;
                    if (result != null && !TypeHelper.IsNullable(leftValueType) && IVS.Value is ITTFPrimitive)
                    {
                        var rightValueTypeName = GetRightValueTypeName();
                        result = $"{result} ({rightValueTypeName})";
                    }
                }
            }

            if (result == String.Empty)
                result = null;

            return result;
        }


        (String name, ValueFormatOption option) ReadValue()
        {
            (String, ValueFormatOption) result = (null, ValueFormatOption.None);

            var attr = AttributeHelper.GetAttribute<ValueFormatAttribute>(FieldInfo, Axis);
            if (attr != null)
            {
                result.Item2 = attr.Option;
                switch (attr.ValueKind)
                {
                    case ValueFormatKind.Default:
                        result.Item1 = ItemValueHelper.DefaultText(IVS);
                        break;
                    case ValueFormatKind.Decimal:
                        result.Item1 = ItemValueHelper.DecimalText(IVS);
                        break;
                    case ValueFormatKind.Hex:
                        result.Item1 = ItemValueHelper.HexText(IVS);
                        break;
                    case ValueFormatKind.Method:
                        if (AttributeHelper.GetMethod(GetDeclaringType(), attr.MethodName, typeof(IVPMethod)) is IVPMethod method)
                            result.Item1 = method(IVS);
                        break;
                    case ValueFormatKind.NoValue:
                        result.Item1 = null;
                        break;
                }
            }
            else
            {
                result.Item1 = ItemValueHelper.DefaultText(IVS);
            }
            return result;
        }


        public string GetDescription()
        {
            string result = null;

            var attr = AttributeHelper.GetAttribute<DescriptionAttribute>(FieldInfo, Axis)
                    ?? AttributeHelper.GetAttribute<DescriptionAttribute>(IVS.ValueType);
            if (attr != null)
            {
                switch (attr.DescriptionKind)
                {
                    case DescriptionKind.Text:
                        return attr.Text;

                    case DescriptionKind.Method:
                        if (AttributeHelper.GetMethod(GetDeclaringType(), attr.Text, typeof(IVPMethod)) is IVPMethod method)
                            return method(IVS);
                        else
                            return null;

                    case DescriptionKind.Enum:
                        var type = Type.GetType(attr.Text);
                        return ItemValueHelper.GetEnumItemName(type, (Int32)IVS.Value.ToNumber4());

                    case DescriptionKind.EmRelative:
                        return ItemValueHelper.RelativeValueDescription(IVS);

                    case DescriptionKind.Instruction:
                        return ItemValueHelper.InstructionDescription(IVS);

                    case DescriptionKind.AsciiText:
                        return ItemValueHelper.AsciiDescription(IVS);

                    case DescriptionKind.UnicodeText:
                        return ItemValueHelper.UnicodeDescription(IVS);

                    case DescriptionKind.ValueType:
                        if (IVS.Value != null)
                        {
                            var rightValueTypeName = GetRightValueTypeName();
                            return rightValueTypeName;
                        }
                        return null;
                }
            }
            return result;
        }


        public Int32 GetGroupCount(Int32 axis)
        {
            Int32 result = 0;
            var attr = AttributeHelper.GetAttribute<GroupCountMethodAttribute>(FieldInfo, axis);
            if (attr != null)
            {
                if (AttributeHelper.GetMethod(GetDeclaringType(), attr.MethodName, typeof(GroupCountMethod)) is GroupCountMethod method)
                    result = method(IVS);
            }
            return result;
        }


        public Int32 GetGroupItemCount(Int32 axis, Int32 itemIndex)
        {
            Int32 result = 0;
            var attr = AttributeHelper.GetAttribute<GroupItemCountMethodAttribute>(FieldInfo, axis);
            if (attr != null)
            {
                if (AttributeHelper.GetMethod(GetDeclaringType(), attr.MethodName, typeof(GroupItemCountMethod)) is GroupItemCountMethod method)
                    result = method(IVS, itemIndex);
            }
            return result;
        }


        public String GetGroupText(Int32 axis)
        {
            String result = null;

            var attr = AttributeHelper.GetAttribute<GroupTextMethodAttribute>(FieldInfo, axis);
            if(attr != null)
            {
                if (AttributeHelper.GetMethod(GetDeclaringType(), attr.MethodName, typeof(GroupTextMethod)) is GroupTextMethod method)
                    result = method(IVS);
            }
            return result;
        }


        public String GetGroupDescription(Int32 axis)
        {
            var attr0 = AttributeHelper.GetAttribute<GroupDescriptionAttribute>(FieldInfo, axis);
            if (attr0 != null)
            {
                return attr0.Text;
            }
            var attr = AttributeHelper.GetAttribute<GroupDescriptionMethodAttribute>(FieldInfo, axis);
            if (attr != null)
            {
                if (AttributeHelper.GetMethod(GetDeclaringType(), attr.MethodName, typeof(GroupTextMethod)) is GroupTextMethod method)
                    return method(IVS);
            }
            return null;
        }


        Type GetDeclaringType()
        {
            var result = FieldInfo?.DeclaringType ?? IVS.ValueType;
            return result;
        }


        static (IItemValueService, string, Int32) GetAncestorInfo(IItemValueService ivp)
        {
            //var result = null;

            Int32 axis = 0;
            for (; ; )
            {
                if (ivp == null)
                    return (null, null, axis);
                else if (ivp.IsTableModel)
                {
                    return (ivp, null, axis);
                }
                else //if (ivm is FieldViewModel fvm)
                {
                    if (ivp.Name.First() == '[')
                    {
                        axis++;
                        ivp = ivp.Parent;
                    }
                    else
                    {
                        var name = ivp.Name;
                        while(name.Last() == ']')
                        {
                            axis++;
                            var pos = name.LastIndexOf('[');
                            name = name.Substring(0, pos);
                        }

                        return (ivp.Parent, name, axis);
                    }
                }
            }
        }


        static String FormatText(String text)
        {
            if (text != null)
            {
                text = text.Trim();
                if (text.Length > 0 && text[0] != '\'' && text.Contains(' '))
                    text = $"'{text}'";
            }
            return text;
        }
    }
}
