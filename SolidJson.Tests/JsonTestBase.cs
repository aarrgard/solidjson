using SolidJson;
using SolidJson.Impl;
using SolidJson.Newtonsoft;

namespace SolidRpc.Tests.Json
{
    /// <summary>
    /// Base class for the Json tests
    /// </summary>
    public class JsonTestBase : TestBase
    {
        /// <summary>
        /// The json reader factory.
        /// </summary>
        public IJsonReaderFactory JsonReaderFactory => new JsonReaderFactory();
        /// <summary>
        /// The json writer factory.
        /// </summary>
        public IJsonWriterFactory JsonWriterFactory => new JsonWriterFactory();
        /// <summary>
        /// Returns the Json parser
        /// </summary>
        public IJsonFactory Factory => new JsonFactory(JsonReaderFactory, JsonWriterFactory);
        /// <summary>
        /// Returns the Json parser
        /// </summary>
        public IJsonParser Parser => new JsonParser(Factory);
    }
}