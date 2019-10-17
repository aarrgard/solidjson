using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Implements the writer logic.
    /// </summary>
    public class JsonWriter : IJsonWriter, IDisposable
    {
        private enum WrittenToken
        {
            BOF,
            Null,
            Boolean,
            Number,
            String,
            StartObject,
            PropertyName,
            EndObject,
            StartArray,
            EndArray
        }
        private WrittenToken LastWrittenToken = WrittenToken.BOF;

        /// <summary>
        /// Constucts a new instance
        /// </summary>
        /// <param name="textWriter"></param>
        public JsonWriter(TextWriter textWriter)
        {
            TextWriter = textWriter;
        }

        private TextWriter TextWriter { get; }

        /// <summary>
        /// Disposes of the writer.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Writes a boolean
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="cancellationToken"></param>
        public Task WriteValueAsync(bool boolean, CancellationToken cancellationToken = default(CancellationToken))
        {
            LastWrittenToken = WrittenToken.Boolean;
            return TextWriter.WriteAsync(boolean ? "true" : "false");
        }

        /// <summary>
        /// Writes a number
        /// </summary>
        /// <param name="i"></param>
        /// <param name="cancellationToken"></param>
        public Task WriteValueAsync(int i, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a number
        /// </summary>
        /// <param name="f"></param>
        /// <param name="cancellationToken"></param>
        public Task WriteValueAsync(double f, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes start of an object
        /// </summary>
        public Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            LastWrittenToken = WrittenToken.StartObject;
            return TextWriter.WriteAsync('{');
        }

        /// <summary>
        /// Writes end of an object
        /// </summary>
        public Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            LastWrittenToken = WrittenToken.EndObject;
            return TextWriter.WriteAsync("}");
        }

        /// <summary>
        /// Writes start of a property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        public async Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (LastWrittenToken != WrittenToken.StartObject)
            {
                await TextWriter.WriteAsync(',');
            }
            await TextWriter.WriteAsync('\"');
            await TextWriter.WriteAsync(name);
            await TextWriter.WriteAsync("\":");
            LastWrittenToken = WrittenToken.PropertyName;
        }

        /// <summary>
        /// Writes a Json string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="cancellationToken"></param>
        public async Task WriteValueAsync(string s, CancellationToken cancellationToken = default(CancellationToken))
        {
            await TextWriter.WriteAsync("\"");
            foreach(var c in s)
            {
                await WriteStringCharAync(c);
            }
            await TextWriter.WriteAsync("\"");
            LastWrittenToken = WrittenToken.String;
        }

        private async Task WriteStringCharAync(char c)
        {
            switch(c)
            {
                case '\"':
                    await TextWriter.WriteAsync("\\\"");
                    break;
                case '\\':
                    await TextWriter.WriteAsync("\\\\");
                    break;
                case '/':
                    await TextWriter.WriteAsync("\\/");
                    break;
                case '\b':
                    await TextWriter.WriteAsync("\\b");
                    break;
                case '\f':
                    await TextWriter.WriteAsync("\\f");
                    break;
                case '\n':
                    await TextWriter.WriteAsync("\\n");
                    break;
                case '\r':
                    await TextWriter.WriteAsync("\\r");
                    break;
                case '\t':
                    await TextWriter.WriteAsync("\\t");
                    break;
                default:
                    await TextWriter.WriteAsync(c);
                    break;
            }
        }

        /// <summary>
        /// Writes start of an array
        /// </summary>
        public Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            LastWrittenToken = WrittenToken.StartArray;
            return TextWriter.WriteAsync("[");
        }
        /// <summary>
        /// Writes the start of an array element
        /// </summary>
        public Task WriteStartArrayElementAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (LastWrittenToken != WrittenToken.StartArray)
            {
                return TextWriter.WriteAsync(',');
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Writes end of an array.
        /// </summary>
        public Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            LastWrittenToken = WrittenToken.EndArray;
            return TextWriter.WriteAsync("]");
        }

        /// <summary>
        /// Emits supplied value to the writer.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        public Task WriteJsonStructAsync(IJsonStruct value, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (value == null)
            {
                LastWrittenToken = WrittenToken.Null;
                return TextWriter.WriteAsync("null");
            }
            return value.WriteAsync(this, cancellationToken);
        }
    }
}
