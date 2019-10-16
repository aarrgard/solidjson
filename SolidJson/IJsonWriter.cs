using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidRpc.Json
{
    /// <summary>
    /// Interface to emit json objects to.
    /// </summary>
    public interface IJsonWriter : IDisposable
    {
        /// <summary>
        /// Writes a string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="cancellationToken"></param>
        Task WriteValueAsync(string s, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes a boolean value
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="cancellationToken"></param>
        Task WriteValueAsync(bool boolean, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writer an integer value
        /// </summary>
        /// <param name="i"></param>
        /// <param name="cancellationToken"></param>
        Task WriteValueAsync(int i, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes a float value
        /// </summary>
        /// <param name="f"></param>
        /// <param name="cancellationToken"></param>
        Task WriteValueAsync(double f, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes a start object
        /// </summary>
        Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes a start property. Emits "\"&lt;key>&gt;":" or  ",\"&lt;key&gt;\":" based on last written token.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <summary>
        /// Writes an end object
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Writes start of an array
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes the start of a new array element. Emits "," if last written token is not start of array.
        /// </summary>
        Task WriteStartArrayElementAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes end of an array.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes a json structure. Delegates writing to supplied value if not null.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        Task WriteJsonStructAsync(IJsonStruct value, CancellationToken cancellationToken = default(CancellationToken));
    }
}
