using System;

namespace SolidRpc.Json.Impl.TypeHandler
{
    public class TypeHandlerObject : TypeHandlerObject<object>
    {

    }
    public class TypeHandlerObject<T> : TypeHandlerBase<T>
    {
        public override IJsonStruct CreateJsonStruct(object parent, object data)
        {
            if (data == null)
            {
                return null;
            }
            else if (data is IJsonObject jo)
            {
                return jo.RawStruct;
            }
            return new JsonObject((IJsonStruct)parent) {};

        }

        public override IJsonStruct CreateNewJsonStruct(object parent)
        {
            return new JsonObject((IJsonStruct)parent);
        }

        public override T CreateType(IJsonStruct jsonStruct)
        {
            if (jsonStruct == null)
            {
                return default(T);
            }
            else if (typeof(T).IsAssignableFrom(jsonStruct.GetType()))
            {
                return (T)jsonStruct;
            }
            else if (jsonStruct is IJsonString js)
            {
                return (T)Convert.ChangeType(js.String, typeof(T));
            }
            else if (jsonStruct is IJsonObject jo)
            {
                return jo.GetProxy<T>();
            }
            throw new Exception($"Cannot convert {jsonStruct.GetType().FullName} to a string.");
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null;
        }
    }
}
