using System;

namespace SolidJson.Impl.TypeHandler
{
    /// <summary>
    /// Base class for a type handler.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TypeHandlerBase<T> : IJsonTypeHandler<T>
    {
        /// <summary>
        /// The handled type
        /// </summary>
        public Type TypeHandled => typeof(T);

        object IJsonTypeHandler.CreateType(IJsonStruct jsonStruct)
        {
            return CreateType(jsonStruct);
        }

        /// <summary>
        /// Creates the type
        /// </summary>
        /// <param name="jsonStruct"></param>
        /// <returns></returns>
        public abstract T CreateType(IJsonStruct jsonStruct);

        /// <summary>
        /// Creates the json struct
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract IJsonStruct CreateJsonStruct(IJsonFactory factory, object data);

        /// <summary>
        /// Determines if supplied value is the default value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool IsDefaultValue(object value);

        /// <summary>
        /// Creates a new struct.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public abstract IJsonStruct CreateNewJsonStruct(IJsonFactory factory);
    }
}
