using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson
{
    /// <summary>
    /// Represents a Json string
    /// </summary>
    public interface IJsonInteger : IJsonStruct
    {
        /// <summary>
        /// Gets/sets the integer value
        /// </summary>
        long Integer { get; set; }
    }
}
