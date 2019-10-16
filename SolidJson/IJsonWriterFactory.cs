using System.IO;

namespace SolidRpc.Json
{
    /// <summary>
    /// The factory that creates json writers
    /// </summary>
    public interface IJsonWriterFactory
    {
        /// <summary>
        /// Creates a json writer from supplied text writer.
        /// </summary>
        /// <param name="tw"></param>
        /// <param name="pretty"></param>
        /// <returns></returns>
        IJsonWriter CreateWriter(TextWriter tw, bool pretty = false);
    }
}
