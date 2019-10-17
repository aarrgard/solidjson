using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Newtonsoft
{
    public class JsonWriter : IJsonWriter
    {

        public JsonWriter(JsonTextWriter jsonTextWriter)
        {
            JsonTextWriter = jsonTextWriter;
        }

        private JsonTextWriter JsonTextWriter { get; }

        public void Dispose()
        {
            ((IDisposable)JsonTextWriter).Dispose();
        }

        public Task WriteValueAsync(string s, CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteValueAsync(s, cancellationToken);
        }

        public Task WriteValueAsync(bool boolean, CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteValueAsync(boolean, cancellationToken);
        }

        public Task WriteValueAsync(int i, CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteValueAsync(i, cancellationToken);
        }

        public Task WriteValueAsync(double f, CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteValueAsync(f, cancellationToken);
        }

        public Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteStartObjectAsync(cancellationToken);
        }

        public Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WritePropertyNameAsync(name, cancellationToken);
        }

        public Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteEndObjectAsync(cancellationToken);
        }

        public Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteStartArrayAsync(cancellationToken);
        }

        public Task WriteStartArrayElementAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return JsonTextWriter.WriteEndArrayAsync(cancellationToken);
        }

        public Task WriteJsonStructAsync(IJsonStruct value, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(value == null)
            {
                return JsonTextWriter.WriteNullAsync(cancellationToken);
            }
            return value.WriteAsync(this, cancellationToken);
        }
    }
}
