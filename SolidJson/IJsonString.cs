﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson
{
    /// <summary>
    /// Represents a Json string
    /// </summary>
    public interface IJsonString : IJsonStruct
    {
        /// <summary>
        /// Gets/sets the string value
        /// </summary>
        string String { get; set; }
    }
}
