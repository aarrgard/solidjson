using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerInt : TypeHandlerBase<int>
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
