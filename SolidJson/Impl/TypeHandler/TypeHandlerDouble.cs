using System;
using System.Collections.Generic;
using System.Text;

namespace SolidRpc.Json.Impl.TypeHandler
{
    public class TypeHandlerDouble : TypeHandlerBase<double>
    {
        public override IJsonStruct CreateJsonStruct(object parent, object data)
        {
            if (data == null) return null;
            return new JsonFloat(parent) { Float = (double)data };

        }

        public override IJsonStruct CreateNewJsonStruct(object parent)
        {
            return new JsonFloat(parent);
        }

        public override double CreateType(IJsonStruct jsonStruct)
        {
            if (jsonStruct == null)
            {
                return default(double);
            }
            else if (jsonStruct is IJsonFloat jf)
            {
                return jf.Float;
            }
            throw new Exception($"Cannot convert {jsonStruct.GetType().FullName} to a double.");
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null || value == (object)0d;
        }
    }
}
