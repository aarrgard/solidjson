using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a Json string.
    /// </summary>
    public class JsonString : JsonStruct, IJsonString
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="parent"></param>
        public JsonString(object parent) : base(parent)
        {
        }

        /// <summary>
        /// The json string.
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// Convertes the value
        /// </summary>
        /// <param name="toType"></param>
        /// <param name="convertedValue"></param>
        /// <returns></returns>
        protected override bool ConvertTo(Type toType, out object convertedValue)
        {
            if(toType == typeof(string))
            {
                convertedValue = String;
                return true;
            }
            if (toType.IsValueType)
            {
                convertedValue = Convert.ChangeType(String, toType);
                return true;
            }
            return base.ConvertTo(toType, out convertedValue);
        }

        /// <summary>
        /// Writes the string
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        public override Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken)
        {
            return writer.WriteValueAsync(String, cancellationToken);
        }
    }
}
