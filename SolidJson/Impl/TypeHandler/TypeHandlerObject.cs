using System;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerObject : TypeHandlerObject<object>
    {

    }
    public class TypeHandlerObject<T> : TypeHandlerBase<T>
    {
        public override IJsonStruct CreateJsonStruct(IJsonFactory factory, object data)
        {
            if (data == null)
            {
                return null;
            }
            else if (data is IJsonObject jo)
            {
                return jo.RawStruct;
            }
            return new JsonObject(factory) {};

        }

        public override IJsonStruct CreateNewJsonStruct(IJsonFactory factory)
        {
            return new JsonObject(factory);
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
