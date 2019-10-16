using System;
using System.Collections.Generic;
using System.Text;

namespace SolidRpc.Json.Impl.TypeHandler
{
    public class TypeHandlerLong : TypeHandlerBase<long>
    {
        public override IJsonStruct CreateJsonStruct(object parent, object data)
        {
            if (data == null) return null;
            return new JsonInteger(parent) { Integer = (long)data };

        }

        public override IJsonStruct CreateNewJsonStruct(object parent)
        {
            return new JsonInteger(parent);
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
