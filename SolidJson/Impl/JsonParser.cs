using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a json parser.
    /// </summary>
    public class JsonParser : IJsonParser
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        public JsonParser(IJsonFactory jsonFactory)
        {
            JsonFactory = jsonFactory;
        }
        private IJsonFactory JsonFactory { get; }

        /// <summary>
        /// Parses the supplied text as json.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IJsonStruct> ParseAsync(string text, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var tr = new StringReader(text))
            {
                return await ParseAsync(tr, cancellationToken);
            }
        }

        /// <summary>
        /// Parses the supplied text as json.
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IJsonStruct> ParseAsync(TextReader tr, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var jr = JsonFactory.JsonReaderFactory.CreateReader(tr))
            {
                return await ParseAsync(jr, cancellationToken);
            }
        }

        /// <summary>
        /// Parses the content in the reader
        /// </summary>
        /// <param name="jr"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IJsonStruct> ParseAsync(IJsonReader jr, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(!(await jr.ReadAsync(cancellationToken)))
            {
                return null;
            }
            return await ParseAsync(jr, JsonFactory, cancellationToken);
        }

        private async Task<IJsonStruct> ParseAsync(IJsonReader jr, object parent, CancellationToken cancellationToken)
        {
            IJsonStruct s;
            switch(jr.TokenType)
            {
                case JsonToken.Null:
                case JsonToken.Boolean:
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.String:
                    s = JsonFactory.CreateJsonStruct(parent, jr.Value?.GetType(), jr.Value);
                    break;
                case JsonToken.StartArray:
                    s = await ParseArrayAsync(jr, parent, cancellationToken);
                    break;
                case JsonToken.StartObject:
                    s = await ParseObjectAsync(jr, parent, cancellationToken);
                    break;
                default:
                    throw new Exception("Cannot handle token type:" + jr.TokenType);
            }
            return s;
        }

        private async Task<JsonObject> ParseObjectAsync(IJsonReader jr, object parent, CancellationToken cancellationToken)
        {
            if (jr.TokenType != JsonToken.StartObject) throw new Exception("Not an object");
            var obj = new JsonObject(parent);
            if (!(await jr.ReadAsync(cancellationToken))) throw new Exception("Failed to read object element.");
            while (jr.TokenType != JsonToken.EndObject)
            {
                if(jr.TokenType != JsonToken.PropertyName) throw new Exception("Failed to read object element.");
                var name = (string)jr.Value;
                if (!(await jr.ReadAsync(cancellationToken))) throw new Exception("Failed to read object element.");
                obj[name] = await ParseAsync(jr, obj, cancellationToken);
                if (!(await jr.ReadAsync(cancellationToken))) throw new Exception("Failed to read object element.");
            }
            return obj;
        }

        private async Task<JsonArray> ParseArrayAsync(IJsonReader jr, object parent, CancellationToken cancellationToken)
        {
            if (jr.TokenType != JsonToken.StartArray) throw new Exception("Not an array");
            var arr = new JsonArray(parent);
            if (!(await jr.ReadAsync(cancellationToken))) throw new Exception("Failed to read array element.");
            while (jr.TokenType != JsonToken.EndArray)
            {
                arr.Add(await ParseAsync(jr, arr, cancellationToken));
                if (!(await jr.ReadAsync(cancellationToken))) throw new Exception("Failed to read array element.");
            }
            return arr;
        }
    }
}
