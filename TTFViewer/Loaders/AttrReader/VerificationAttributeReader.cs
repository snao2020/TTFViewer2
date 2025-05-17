using System.Reflection;
using TTFViewer.DataTypes;

namespace TTFViewer.Loaders
{
    class VerificationAttributeReader
    {
        TableLoader TableLoader;

        public VerificationAttributeReader(TableLoader loader)
        {
            TableLoader = loader;
        }


        public bool IsValid(FieldInfo fi)
        {
            
            var axis = TablePathHelper.GetAxis(TableLoader.GetFullPath());
            var attr = AttributeHelper.GetAttribute<VerificationAttribute>(fi, axis);
            if(attr != null)
            {
                var reader = new FieldValueReader(TableLoader, fi.DeclaringType);
                var values0 = reader.GetValues(attr.Value0);
                var values1 = reader.GetValues(attr.Value1);
                if (!ValueHelper.IsSelect2(attr.Condition, values0, values1))
                    return false;
            }
            
            return true;
        }
    }
}
