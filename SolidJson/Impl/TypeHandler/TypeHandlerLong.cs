using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerLong : TypeHandlerBase<long>
    {
        public override IJsonStruct CreateJsonStruct(IJsonFactory factory, object data)
        {
            if (data == null) return null;
            return new JsonInteger(factory) { Integer = (long)data };

        }

        public override IJsonStruct CreateNewJsonStruct(IJsonFactory factory)
        {
            return new JsonInteger(factory);
        }

        public override long CreateType(IJsonStruct jsonStruct)
        {
            if (jsonStruct == null)
            {
                return default(long);
            }
            else if (jsonStruct is IJsonInteger js)
            {
                return js.Integer;
            }
            throw new Exception($"Cannot convert {jsonStruct.GetType().FullName} to a string.");
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null || value == (object)0L;
        }
    }
}
