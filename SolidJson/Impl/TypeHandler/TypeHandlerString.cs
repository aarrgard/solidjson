using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerString : TypeHandlerBase<string>
    {
        public override IJsonStruct CreateJsonStruct(IJsonFactory factory, object data)
        {
            if (data == null) return null;
            return new JsonString(factory) { String = (string)data };

        }

        public override IJsonStruct CreateNewJsonStruct(IJsonFactory factory)
        {
            return new JsonString(factory);
        }

        public override string CreateType(IJsonStruct jsonStruct)
        {
            if(jsonStruct == null)
            {
                return null;
            }
            else if(jsonStruct is IJsonString js)
            {
                return js.String;
            }
            throw new Exception($"Cannot convert {jsonStruct.GetType().FullName} to a string.");
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null;
        }
    }
}
