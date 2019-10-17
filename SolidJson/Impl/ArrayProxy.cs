using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidJson.Impl
{
    /// <summary>
    /// Implements a proxy for the JsonArray.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayProxy<T> : IList<T>
    {

        public ArrayProxy(IJsonArray jsonArray)
        {
            JsonArray = jsonArray ?? throw new ArgumentNullException(nameof(jsonArray));
        }
        private IJsonArray JsonArray { get; }

        private IJsonStruct CreateItem(T item)
        {
            return JsonArray.Factory.CreateJsonStruct(typeof(T), item);
        }

        T IList<T>.this[int index] {
            get
            {
                var e = JsonArray[index];
                return e == null ? default(T) : e.As<T>();
            }
            set
            {
                JsonArray[index] = CreateItem(value);
            }
        }


        int ICollection<T>.Count => JsonArray.Count;

        bool ICollection<T>.IsReadOnly => JsonArray.IsReadOnly;

        void ICollection<T>.Add(T item)
        {
            JsonArray.Add(CreateItem(item));
        }

        void ICollection<T>.Clear()
        {
            JsonArray.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return JsonArray.Contains(CreateItem(item));
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            int idx = arrayIndex;
            foreach(var e in JsonArray)
            {
                if(idx >= array.Length)
                {
                    return;
                }
                array[idx] = e.As<T>();
                idx++;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return JsonArray.Select(o => o.As<T>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        int IList<T>.IndexOf(T item)
        {
            return JsonArray.IndexOf(CreateItem(item));
        }

        void IList<T>.Insert(int index, T item)
        {
            JsonArray.Insert(index, CreateItem(item));
        }

        bool ICollection<T>.Remove(T item)
        {
            return JsonArray.Remove(CreateItem(item));
        }

        void IList<T>.RemoveAt(int index)
        {
            JsonArray.RemoveAt(index);
        }
    }
}
