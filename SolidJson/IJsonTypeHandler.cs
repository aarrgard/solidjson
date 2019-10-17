using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson
{
    /// <summary>
    /// Interface that all the type handlers must conform to.
    /// </summary>
    public interface IJsonTypeHandler
    {
        /// <summary>
        /// Returns the type handled.
        /// </summary>
        Type TypeHandled { get; }

        /// <summary>
        /// Creates a default json struct for this type(not null)
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        IJsonStruct CreateNewJsonStruct(IJsonFactory factory);

        /// <summary>
        /// Creates a JsonStruct for supplied data
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        IJsonStruct CreateJsonStruct(IJsonFactory factory, object data);

        /// <summary>
        /// Creates a type from supplied jsonStruct
        /// </summary>
        /// <param name="jsonStruct"></param>
        /// <returns></returns>
        object CreateType(IJsonStruct jsonStruct);

        /// <summary>
        /// Determines if the supplied type is a default type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsDefaultValue(object value);
    }

    /// <summary>
    /// Interface that all the type handlers must conform to.
    /// </summary>
    public interface IJsonTypeHandler<T> : IJsonTypeHandler
    {
        /// <summary>
        /// Creates a type from supplied json structure
        /// </summary>
        /// <param name="jsonStruct"></param>
        /// <returns></returns>
        new T CreateType(IJsonStruct jsonStruct);
    }
}
