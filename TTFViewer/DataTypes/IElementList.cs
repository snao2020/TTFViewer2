using System;

namespace TTFViewer.DataTypes
{
    public class ElementListItemGenerator<T> : VirtualizingList<T>.IItemGenerator
    {
        IElementList IEL;

        public ElementListItemGenerator(IElementList iel)
        {
            IEL = iel;
        }


        public int GetCount()
        {
            return IEL.GetCount();
        }


        public T GetItem(int index)
        {
            T result = (T)IEL.GetItem(index);            
            return result;
        }
    }


    public interface IElementList
    {
        string BasePath { get; }
        Type DeclaringType { get; }

        UInt32 GetFileLength();

        int GetCount();
        UInt32 GetElementPosition(Int32 index);
        UInt32 GetElementLength(Int32 index);
        Type GetElementType(Int32 index);
        object GetItem(int index);
    }
}
