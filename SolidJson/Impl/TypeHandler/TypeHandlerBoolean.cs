using System;
using System.Collections.Generic;
using System.Text;

namespace SolidRpc.Json.Impl.TypeHandler
{
    public class TypeHandlerBoolean : TypeHandlerBase<bool>
    {
        public override IJsonStruct CreateJsonStruct(object parent, object data)
        {
            if (data == null) return null;
            return new JsonBoolean(parent) { Boolean = (bool)data };

        }

        public override IJsonStruct CreateNewJsonStruct(object parent)
        {
            return new JsonBoolean(parent);
        }

        public override bool CreateType(IJsonStruct jsonStruct)
        {
            if (jsonStruct == null)
            {
                return default(bool);
            }
            else if (jsonStruct is IJsonBoolean jb)
            {
                return jb.Boolean;
            }
            throw new Exception($"Cannot convert {jsonStruct.GetType().FullName} to a boolean.");
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null || value == (object)false;
        }
    }
}
