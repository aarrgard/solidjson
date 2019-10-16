using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidRpc.Json.Impl
{
    /// <summary>
    /// Represents a Json integer.
    /// </summary>
    public class JsonInteger : JsonStruct, IJsonInteger
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="parent"></param>
        public JsonInteger(object parent) : base(parent)
        {
        }

        /// <summary>
        /// The json integer.
        /// </summary>
        public long Integer { get; set; }

        /// <summary>
        /// Convertes the value
        /// </summary>
        /// <param name="toType"></param>
        /// <param name="convertedValue"></param>
        /// <returns></returns>
        protected override bool ConvertTo(Type toType, out object convertedValue)
        {
            if (toType.IsValueType)
            {
                convertedValue = Convert.ChangeType(Integer, toType);
                return true;
            }
            return base.ConvertTo(toType, out convertedValue);
        }

        /// <summary>
        /// Writes the integer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        public override Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken)
        {
            return writer.WriteValueAsync(Integer, cancellationToken);
        }
    }
}
