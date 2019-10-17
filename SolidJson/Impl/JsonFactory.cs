using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a json factory.
    /// </summary>
    public class JsonFactory : IJsonFactory
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        public JsonFactory(IJsonReaderFactory jsonReaderFactory, IJsonWriterFactory jsonWriterFactory, IServiceProvider serviceProvider = null)
        {
            ServiceProvider = serviceProvider;
            JsonReaderFactory = jsonReaderFactory;
            JsonWriterFactory = jsonWriterFactory;
            TypeHandlers = new ConcurrentDictionary<Type, IJsonTypeHandler>();
        }

        private IServiceProvider ServiceProvider { get; }
        public IJsonReaderFactory JsonReaderFactory { get; }
        public IJsonWriterFactory JsonWriterFactory { get; }
        private ConcurrentDictionary<Type, IJsonTypeHandler> TypeHandlers { get; }

        private IJsonTypeHandler CreateTypeHandler(Type type)
        {
            return (IJsonTypeHandler)GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.Name == nameof(CreateTypeHandler))
                .Where(o => o.IsGenericMethod)
                .Single()
                .MakeGenericMethod(type)
                .Invoke(this, null);
        }
        private IJsonTypeHandler CreateTypeHandler<T>()
        {
            // resolve from service provider
            if (ServiceProvider != null)
            {
                var typeHandler = (IJsonTypeHandler<T>)ServiceProvider.GetService(typeof(IJsonTypeHandler<T>));
                if (typeHandler != null)
                {
                    return typeHandler;
                }
            }

            // resolve from this assembly
            var typeHandlerType = GetType().Assembly.GetTypes().Where(o => typeof(IJsonTypeHandler<T>).IsAssignableFrom(o)).FirstOrDefault();
            if (typeHandlerType != null)
            {
                return (IJsonTypeHandler<T>)Activator.CreateInstance(typeHandlerType);
            }

            // if enumerable type - create a new array handler.
            if (GetEnumerableType(typeof(T), out Type enumType))
            {
                typeHandlerType = typeof(TypeHandler.TypeHandlerArray<,>).MakeGenericType(typeof(T), enumType);
                var elemTypeHandler = CreateTypeHandler(enumType);
                var typeHandler = (IJsonTypeHandler<T>)Activator.CreateInstance(typeHandlerType, elemTypeHandler);
                return typeHandler;
            }

            // if supplied type is an interface - used object type handler
            if (typeof(T).IsInterface)
            {
                return new TypeHandler.TypeHandlerObject<T>();
            }
            throw new Exception($"Cannot find type handler for type:{typeof(T)}");
        }

        private bool GetEnumerableType(Type type, out Type enumType)
        {
            var interfaces = type.GetInterfaces().Union(new[] { type });
            foreach (var i in interfaces)
            {
                if (!i.IsGenericType)
                {
                    continue;
                }
                if (!typeof(IEnumerable<>).IsAssignableFrom(i.GetGenericTypeDefinition()))
                {
                    continue;
                }
                enumType = i.GetGenericArguments()[0];
                return true;
            }
            enumType = null;
            return false;
        }

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <returns></returns>
        public IJsonObject New()
        {
            return new JsonObject(this);
        }

        /// <summary>
        /// Constucts a new instance. Populates the underlying structure
        /// according to the "EmitDefaultValue" setting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T New<T>()
        {

            var proxy = new JsonObject(this).As<T>();
            ObjectProxy<T>.SetDefaultValues(proxy);
            return proxy;
        }

        private Type GetPropertyType(Type dataType, object data)
        {
            if (dataType == null)
            {
                dataType = data?.GetType();
                if (dataType == null)
                {
                    dataType = typeof(object);
                }
            }
            return dataType;
        }
        bool IJsonFactory.IsDefaultValue(Type dataType, object data)
        {
            return TypeHandlers.GetOrAdd(GetPropertyType(dataType, data), CreateTypeHandler).IsDefaultValue(data);
        }

        IJsonStruct IJsonFactory.CreateJsonStruct(Type dataType, object data)
        {
            return TypeHandlers.GetOrAdd(GetPropertyType(dataType, data), CreateTypeHandler).CreateJsonStruct(this, data);
        }

        object IJsonFactory.CreateData(JsonStruct jsonStruct, Type dataType)
        {
            return TypeHandlers.GetOrAdd(GetPropertyType(dataType, null), CreateTypeHandler).CreateType(jsonStruct);
        }

        IJsonStruct IJsonFactory.CreateNewJsonStruct(Type dataType)
        {
            return TypeHandlers.GetOrAdd(GetPropertyType(dataType, null), CreateTypeHandler).CreateNewJsonStruct(this);
        }
    }
}
