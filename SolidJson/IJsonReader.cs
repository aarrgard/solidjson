using System;
using System.Threading;
using System.Threading.Tasks;

namespace SolidRpc.Json
{
    /// <summary>
    /// This is the reader that reads json tokens.
    /// </summary>
    public interface IJsonReader : IDisposable
    {

        /// <summary>
        /// Reads the next token
        /// </summary>
        /// <returns></returns>
        Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the token type.
        /// </summary>
        JsonToken TokenType { get; }

        /// <summary>
        /// Returns the value
        /// </summary>
        object Value { get; }
    }
}
