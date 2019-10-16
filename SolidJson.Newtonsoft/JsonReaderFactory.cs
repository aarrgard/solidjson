using Newtonsoft.Json;
using System.IO;

namespace SolidRpc.Json.Newtonsoft
{
    /// <summary>
    /// Factory that creates json reader using the newtonsoft reader.
    /// </summary>
    public class JsonReaderFactory : IJsonReaderFactory
    {
        /// <summary>
        /// Creates the reader
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public IJsonReader CreateReader(TextReader tr)
        {
            return new JsonReader(new JsonTextReader(tr));
        }
    }
}
