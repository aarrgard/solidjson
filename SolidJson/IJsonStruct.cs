using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson
{
    /// <summary>
    /// base class for all Json strutures
    /// </summary>
    public interface IJsonStruct
    {
        /// <summary>
        /// Returns the parent structure.
        /// </summary>
        IJsonStruct Parent { get; }

        /// <summary>
        /// The proxies that wrap the raw data implements
        /// the IJsonStruct interface. This property returns
        /// the underlying IJsonStruct.
        /// </summary>
        IJsonStruct RawStruct { get; }

        /// <summary>
        /// Returns the first parent of specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetParent<T>();

        /// <summary>
        /// Converts this struct to specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T As<T>();

        /// <summary>
        /// Converts this struct to specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object As(Type type);

        /// <summary>
        /// Emits the structure in json format.
        /// </summary>
        /// <param name="pretty"></param>
        string AsJson(bool pretty = false);

        /// <summary>
        /// Writes this structure to supplied writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken = default(CancellationToken));
    }
}
