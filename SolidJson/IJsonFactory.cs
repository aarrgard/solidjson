using System;
using SolidRpc.Json.Impl;

namespace SolidRpc.Json
{
    /// <summary>
    /// Use the factory to create new objects
    /// </summary>
    public interface IJsonFactory
    {
        /// <summary>
        /// The reader factory
        /// </summary>
        IJsonReaderFactory JsonReaderFactory { get; }

        /// <summary>
        /// The writer factory
        /// </summary>
        IJsonWriterFactory JsonWriterFactory { get; }

        /// <summary>
        /// Constructs a new JsonObject instance. The instance
        /// returned can be used as a root node or added to another
        /// document later.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        IJsonObject New(object parent = null);

        /// <summary>
        /// Constructs a new JsonObject and converts it to
        /// the specified type. Populates the underlying structure
        /// according to the "EmitDefaultValue" setting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        T New<T>(object parent = null);

        /// <summary>
        /// Constructs data from supplied struct
        /// </summary>
        /// <param name="jsonStruct"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        object CreateData(JsonStruct jsonStruct, Type dataType);

        /// <summary>
        /// Creates a IJsonStruct from supplied data.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        IJsonStruct CreateJsonStruct(object parent, Type dataType, object data);

        /// <summary>
        /// Creates a new json struct.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        IJsonStruct CreateNewJsonStruct(object parent, Type dataType);

        /// <summary>
        /// Returns true if supplied data is default value for specified type.
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool IsDefaultValue(Type propertyType, object data);
    }
}
