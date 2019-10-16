using System;
using System.Collections.Generic;
using System.Text;

namespace SolidRpc.Json
{
    /// <summary>
    /// Represents a Json string
    /// </summary>
    public interface IJsonBoolean : IJsonStruct
    {
        /// <summary>
        /// Gets/sets the boolean value
        /// </summary>
        bool Boolean { get; set; }
    }
}
