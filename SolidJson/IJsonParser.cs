using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SolidRpc.Json
{
    /// <summary>
    /// The parser parses strings and streams into Json structures
    /// </summary>
    public interface IJsonParser
    {
        /// <summary>
        /// Parses the supplied text.
        /// </summary>
        Task<IJsonStruct> ParseAsync(string text, CancellationToken cancellation = default(CancellationToken));

        /// <summary>
        /// Parses the supplied text.
        /// </summary>
        Task<IJsonStruct> ParseAsync(TextReader textReader, CancellationToken cancellation = default(CancellationToken));
    }
}
