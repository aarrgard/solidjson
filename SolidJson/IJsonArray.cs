using System;
using System.Collections.Generic;
using System.Text;

namespace SolidRpc.Json
{
    /// <summary>
    /// Represents a Json array
    /// </summary>
    public interface IJsonArray : IJsonStruct, IList<IJsonStruct>
    {

    }
}
