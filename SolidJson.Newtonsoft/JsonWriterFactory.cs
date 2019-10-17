using Newtonsoft.Json;
using System.IO;

namespace SolidJson.Newtonsoft
{
    /// <summary>
    /// Factory that creates json writer using the newtonsoft writer.
    /// </summary>
    public class JsonWriterFactory : IJsonWriterFactory
    {
        /// <summary>
        /// Creates the writer
        /// </summary>
        /// <param name="tw"></param>
        /// <param name="pretty"></param>
        /// <returns></returns>
        public IJsonWriter CreateWriter(TextWriter tw, bool pretty)
        {
            var formatting = pretty ? Formatting.Indented : Formatting.None;
            return new JsonWriter(new JsonTextWriter(tw) { Formatting = formatting });
        }
    }
}
