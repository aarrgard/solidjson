using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a Json float.
    /// </summary>
    public class JsonFloat : JsonStruct, IJsonFloat
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="factory"></param>
        public JsonFloat(IJsonFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// The json float.
        /// </summary>
        public double Float { get; set; }

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
                convertedValue = Convert.ChangeType(Float, toType);
                return true;
            }
            return base.ConvertTo(toType, out convertedValue);
        }

        /// <summary>
        /// Emits a float
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        public override Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken)
        {
            return writer.WriteValueAsync(Float, cancellationToken);
        }
    }
}
