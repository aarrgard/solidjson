﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a Json object.
    /// </summary>
    public abstract class JsonStruct : IJsonStruct
    {
        private readonly object _parent;

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="parent"></param>
        public JsonStruct(object parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <summary>
        /// The parent structure
        /// </summary>
        public IJsonStruct Parent => _parent as IJsonStruct;

        /// <summary>
        /// Returns this instance. Proxies returns the wrapped object.
        /// </summary>
        public IJsonStruct RawStruct => this;

        /// <summary>
        /// Converts this structure to specified struct.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T As<T>()
        {
            return (T)GetParent<IJsonFactory>().CreateData(this, typeof(T));
        }

        /// <summary>
        /// Converts thisstructure to another type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object As(Type type)
        {
            return GetParent<IJsonFactory>().CreateData(this, type);
        }

        /// <summary>
        /// Emits the object as a json string
        /// </summary>
        /// <returns></returns>
        public string AsJson(bool pretty)
        {
            var jsonFactory = GetParent<IJsonFactory>();
            var sw = new StringWriter();
            using (sw)
            {
                using (var jw = jsonFactory.JsonWriterFactory.CreateWriter(sw, pretty))
                {
                    WriteAsync(jw).Wait();
                }
            }
            return sw.ToString();
        }

        /// <summary>
        /// Returns the first parent node of specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParent<T>()
        {
            if (_parent == null)
            {
                return default(T);
            }
            if (_parent is T)
            {
                return (T)_parent;
            }
            return ((IJsonStruct)_parent).GetParent<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        public abstract Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Implements the conversion logic.
        /// </summary>
        /// <param name="toType"></param>
        /// <param name="convertedValue"></param>
        /// <returns></returns>
        protected virtual bool ConvertTo(Type toType, out object convertedValue)
        {
            if(toType.IsAssignableFrom(GetType()))
            {
                convertedValue = this;
                return true;
            }
            convertedValue = null;
            return false;
        }
    }
}
