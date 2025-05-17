using System;
using System.Collections;
using System.Collections.Generic;

namespace TTFViewer.DataTypes
{
    public class VirtualizingList<T> : IList<T>, IList
    {
        public interface IItemGenerator
        {
            int GetCount();
            T GetItem(int index);
        }

        public IItemGenerator ItemGenerator { get; set; }


        public T this[int index]
        {
            get => ItemGenerator.GetItem(index);
            set =>throw new NotImplementedException();
        }

        public int Count => ItemGenerator.GetCount();

        public bool IsReadOnly => true;

        public bool IsFixedSize => true; //false;

        public object SyncRoot => this;

        public bool IsSynchronized => false;

        object IList.this[int index]
        {
            get => this[index];
            set => throw new NotImplementedException();
        }

        public void Add(T item)
            => throw new NotImplementedException();

        public void Clear()
            => throw new NotImplementedException();

        public bool Contains(T item)
            => false;

        public void CopyTo(T[] array, int arrayIndex)
        {
            int count = array.Length - arrayIndex;
            if (count >= Count)
            {
                foreach (T i in this)
                    array[arrayIndex++] = i;
            }
            else
                throw new ArgumentException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        public int IndexOf(T item)
            => -1;

        public void Insert(int index, T item)
            => throw new NotImplementedException();
        
        
        public bool Remove(T item)
            => throw new NotImplementedException();

        public void RemoveAt(int index)
            => throw new NotImplementedException();
        
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public int Add(object value)
            => throw new NotImplementedException();

        public bool Contains(object value)
            => false;

        public int IndexOf(object value)
            => -1;

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}

