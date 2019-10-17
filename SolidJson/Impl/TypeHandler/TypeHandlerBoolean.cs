using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerBoolean : TypeHandlerBase<bool>
    {
        public override IJsonStruct CreateJsonStruct(IJsonFactory factory, object data)
        {
            if (data == null) return null;
            return new JsonBoolean(factory) { Boolean = (bool)data };

        }

        public override IJsonStruct CreateNewJsonStruct(IJsonFactory factory)
        {
            return new JsonBoolean(factory);
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
