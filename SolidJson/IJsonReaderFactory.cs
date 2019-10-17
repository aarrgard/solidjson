using System.IO;

namespace SolidJson
{
    /// <summary>
    /// The factory that creates json readers
    /// </summary>
    public interface IJsonReaderFactory
    {
        /// <summary>
        /// Creates a json reader from supplied text reader.
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        IJsonReader CreateReader(TextReader tr);
    }
}
