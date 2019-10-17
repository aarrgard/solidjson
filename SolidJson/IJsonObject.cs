using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson
{
    /// <summary>
    /// Represents a Json object
    /// </summary>
    public interface IJsonObject : IJsonStruct
    {
        /// <summary>
        /// Returns the proxy that wraps this structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetProxy<T>();

        /// <summary>
        /// Returns the json structure for supplied name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IJsonStruct this[string name] { get; set; }

        /// <summary>
        /// Removes the name/value pair(if it exists)
        /// </summary>
        /// <param name="name"></param>
        bool Remove(string name);
    }
}
