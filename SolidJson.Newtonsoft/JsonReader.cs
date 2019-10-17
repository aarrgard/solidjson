using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Newtonsoft
{
    public class JsonReader : IJsonReader
    {
        public JsonReader(JsonTextReader jsonTextReader)
        {
            JsonTextReader = jsonTextReader;
        }

        public JsonTextReader JsonTextReader { get; }

        public void Dispose()
        {
            ((IDisposable)JsonTextReader).Dispose();
        }

        public Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextReader.ReadAsync(cancellationToken);
        }

        public JsonToken TokenType {
            get 
            {
                return (JsonToken)JsonTextReader.TokenType;
            }
        }

        public object Value => JsonTextReader.Value;
    }
}
