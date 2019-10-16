using System;
using System.Collections.Generic;
using System.Text;

namespace SolidRpc.Json.Impl.TypeHandler
{
    public class TypeHandlerInt : TypeHandlerBase<int>
    {
        public override IJsonStruct CreateJsonStruct(object parent, object data)
        {
            if (data == null) return null;
            return new JsonInteger((IJsonStruct)parent) { Integer = (long)data };

        }

        public override IJsonStruct CreateNewJsonStruct(object parent)
        {
            return new JsonInteger((IJsonStruct)parent);
        }

        public override int CreateType(IJsonStruct jsonStruct)
        {
            if (jsonStruct == null)
            {
                return default(int);
            }
            else if (jsonStruct is IJsonInteger js)
            {
                return (int)js.Integer;
            }
            throw new Exception($"Cannot convert {jsonStruct.GetType().FullName} to a string.");
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null || value == (object)0;
        }
    }
}
