using System;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;

namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    /*
    Card8 0 – 255 1-byte unsigned number
    Card16 0 – 65535 2-byte unsigned number
    Offset varies 1, 2, 3, or 4 byte offset (specified by 
    OffSize field)
    OffSize 1– 4 1-byte unsigned number specifies the 

    size of an Offset field or fields
    SID 0 – 64999 2-byte string identifier
    */

    static class CFFHelper
    {
        public static bool? IsCFF(Type fontTableType)
        {
            bool? result = null;
            switch (fontTableType)
            {
                case Type t when t == typeof(CFFTable): result = true; break;
                case Type t when t == typeof(CFF2Table): result = false; break;
            }
            return result;
        }

        public static string FontIndexText(IItemValueService ivp, String name)
        {
            name = ItemValueHelper.FormatText(name);
            var nameIndexes = ViewModelHelper.GetNameIndexes(ivp.Name);
            var indexes = nameIndexes.Item2;
            if (indexes != null)
            {
                var fontTableType = ItemValueHelper.GetFontTableType(ivp);
                if (fontTableType == typeof(CFFTable))
                {
                    if (indexes.Count == 1)
                        return $"{name} (font#{indexes[0]})";
                    else if (indexes.Count == 2)
                        return $"{name} (font#{indexes[0]}, fd#{indexes[1]})";
                }
                else if (fontTableType == typeof(CFF2Table))
                {
                    if (indexes.Count == 1)
                        return $"{name} (FontDICT#{indexes[0]})";
                }
            }
            return name;
        }


        public static String GetTypeName(IItemValueService ivs)
        {
            String result;

            if (ItemValueHelper.GetFontTableType(ivs) == typeof(CFFTable))
            {
                var type = ivs.ValueType;
                if (type == null)
                {
                    var ivp = new ItemValueProvider(ivs);
                    type = ivp.GetLeftType();
                }
                var typeRank = TypeHelper.GetElementTypeRank(type);// ivs.ValueType);
                if (typeRank.Item1 == typeof(uint8))
                    result = "Card8";
                else if (typeRank.Item1 == typeof(uint16))
                    result = "Card16";
                else
                    throw new ArgumentException($"CFFHelper.GetTypeName {ivs.ValueType.Name}");
            }
            else
            {
                var type = ivs.ValueType;
                if (type == null)
                {
                    var ivp = new ItemValueProvider(ivs);
                    type = ivp.GetLeftType();
                }
                var typeRank = TypeHelper.GetElementTypeRank(type);
                result = typeRank.Item1.Name;
            }
            return result;
        }


        public static bool IsCIDFont(IAttributeService service, Int32 fontIndex)
        {
            // The Top DICT begins with ROS operator(Adobe 5176)
            bool result = service.GetValues(FieldValueKind.Path, $"\\TopDICTINDEX\\data\\[{fontIndex}]\\Data\\[0]").SingleValue(0) is DICTData dictData
                && DICTHelper.GetCFFDICTOperator(dictData.Operator) == CFFDICTOperators.ROS;
            return result;
        }
    }

#pragma warning restore IDE1006
}
