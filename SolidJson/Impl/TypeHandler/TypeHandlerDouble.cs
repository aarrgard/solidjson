using System;
using System.Collections.Generic;
using System.Text;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerDouble : TypeHandlerBase<double>
    {
        public override IJsonStruct CreateJsonStruct(IJsonFactory factory, object data)
        {
            if (data == null) return null;
            return new JsonFloat(factory) { Float = (double)data };

        }

        public override IJsonStruct CreateNewJsonStruct(IJsonFactory factory)
        {
            return new JsonFloat(factory);
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
