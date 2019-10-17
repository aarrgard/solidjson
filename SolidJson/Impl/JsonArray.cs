using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a Json array.
    /// </summary>
    public class JsonArray : JsonStruct, IJsonArray
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="parent"></param>
        public JsonArray(object parent) : base(parent)
        {
            Elements = new List<IJsonStruct>();
        }

        private IList<IJsonStruct> Elements { get; }

        /// <summary>
        /// The number of elements
        /// </summary>
        public int Count => Elements.Count;

        /// <summary>
        /// Is the array read only
        /// </summary>
        public bool IsReadOnly => Elements.IsReadOnly;

        /// <summary>
        /// Returns the element at supplied index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IJsonStruct this[int index] { get => Elements[index]; set => Elements[index] = value; }

        /// <summary>
        /// Returns the enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IJsonStruct> GetEnumerator()
        {
            return Elements.OfType<IJsonStruct>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
     
        /// <summary>
        /// Returns the index of supplied item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(IJsonStruct item)
        {
            return Elements.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, IJsonStruct item)
        {
            Elements.Insert(index, item);
        }

        /// <summary>
        /// Removes an item
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            Elements.RemoveAt(index);
        }

        /// <summary>
        /// Adds an item
        /// </summary>
        /// <param name="item"></param>
        public void Add(IJsonStruct item)
        {
            Elements.Add(item);
        }

        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            Elements.Clear();
        }

        /// <summary>
        /// Returns true if list contains the item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(IJsonStruct item)
        {
            return Elements.Contains(item);
        }

        /// <summary>
        /// Copies the elements
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IJsonStruct[] array, int arrayIndex)
        {
            Array.Copy(Elements.ToArray(), 0, array, 0, Elements.Count);
        }

        /// <summary>
        /// Removes suppied element.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IJsonStruct item)
        {
            return Elements.Remove(item);
        }

        /// <summary>
        /// Writes the array.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        public override async Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken)
        {
            await writer.WriteStartArrayAsync(cancellationToken);
            foreach(var e in Elements)
            {
                await writer.WriteStartArrayElementAsync(cancellationToken);
                await writer.WriteJsonStructAsync(e, cancellationToken);
            }
            await writer.WriteEndArrayAsync(cancellationToken);
        }
    }
}
