using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.Loaders
{
    static class LoadItemHelper
    {
        public static FieldInfo GetFieldInfo(LoadItem2 item)
        {
            FieldInfo result = null;
            if (item == null)
                return result;

            var el = item.ElementList;
            if (el != null)
            {
                if(el.DeclaringType != null)
                {
                    var path = TablePathHelper.RemoveAllElements(el.BasePath);
                    var name = TablePathHelper.GetLastName(path);
                    if(!string.IsNullOrEmpty(name))
                    {
                        result = el.DeclaringType.GetField(name);
                    }
                }
            }
            else
            {
                var parent = item.Parent;
                while (parent != null && TypeHelper.IsArrayOrIListT(parent.LeftValueType))
                {
                    item = parent;
                    parent = item.Parent;
                }
                result = parent?.ObjectType?.GetField(item.Name);
            }
            return result;
        }
    }
}
