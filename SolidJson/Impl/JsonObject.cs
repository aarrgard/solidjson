using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents a Json object.
    /// </summary>
    public class JsonObject : JsonStruct, IJsonObject
    {
        private interface IEmptyInterface { }
        private static readonly object[] s_EmptyProxyArray = new object[0];
        private static readonly ConcurrentDictionary<Type, Func<IJsonObject, object>> s_ProxyFactories = new ConcurrentDictionary<Type, Func<IJsonObject, object>>();

        private object[] _proxies = s_EmptyProxyArray;

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="parent"></param>
        public JsonObject(object parent) : base(parent)
        {
            Properties = new Dictionary<string, IJsonStruct>();
        }

        private Dictionary<string, IJsonStruct> Properties { get; }

        /// <summary>
        /// Sets or gets the property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IJsonStruct this[string name] { 
            get 
            {
                if(Properties.TryGetValue(name, out IJsonStruct s)) {
                    return s;
                }
                return null;
            } 
            set 
            {
                Properties[name] = value;
            } 
        }

        /// <summary>
        /// Removes the property
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Remove(string name)
        {
            return Properties.Remove(name);
        }

        /// <summary>
        /// Converts the json object to specified type. - May be an interface.
        /// </summary>
        /// <param name="toType"></param>
        /// <param name="convertedValue"></param>
        /// <returns></returns>
        protected override bool ConvertTo(Type toType, out object convertedValue)
        {
            if(toType.IsInterface)
            {
                convertedValue = GetProxy(toType);
                return true;
            }
            return base.ConvertTo(toType, out convertedValue);
        }

        public T GetProxy<T>()
        {
            return (T)GetProxy(typeof(T));
        }

        private object GetProxy(Type proxyType)
        {
            var proxy  = _proxies.Where(o => proxyType.IsAssignableFrom(o.GetType())).FirstOrDefault();
            if (proxy != null)
            {
                return proxy;
            }
            lock (Properties)
            {
                proxy = _proxies.Where(o => proxyType.IsAssignableFrom(o.GetType())).FirstOrDefault();
                if (proxy == null)
                {
                    proxy = s_ProxyFactories.GetOrAdd(proxyType, CreateProxyFactory).Invoke(this);
                    _proxies = _proxies.Union(new[] { proxy }).ToArray();
                }
                return proxy;
            }
        }

        private Func<IJsonObject, object> CreateProxyFactory(Type interfaze)
        {
            return (Func<IJsonObject, object>) GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.IsGenericMethod)
                .Where(o => o.Name == nameof(CreateProxyFactory))
                .Single()
                .MakeGenericMethod(interfaze)
                .Invoke(this, null);
        }

        private Func<IJsonObject, object> CreateProxyFactory<T>()
        {
            // there seem to be an issue when proxying same interface
            // twice. Since the ObjectProxy implements the IJsonObject
            // interface we do not need to implement the proxy
            if (typeof(T) == typeof(IJsonObject))
            {
                return (jsonObject) =>
                {
                    var proxy = DispatchProxy.Create<IEmptyInterface, ObjectProxy<T>>();
                    ((ObjectProxy<T>)(object)proxy).JsonObject = jsonObject;
                    return proxy;
                };
            }
            else
            {
                return (jsonObject) =>
                {
                    var proxy = DispatchProxy.Create<T, ObjectProxy<T>>();
                    ((ObjectProxy<T>)(object)proxy).JsonObject = jsonObject;
                    return proxy;
                };
            }
        }

        /// <summary>
        /// Writes the object
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cancellationToken"></param>
        public override async Task WriteAsync(IJsonWriter writer, CancellationToken cancellationToken)
        {
            await writer.WriteStartObjectAsync(cancellationToken);
            foreach(var o in Properties) 
            {
                await writer.WritePropertyNameAsync(o.Key, cancellationToken);
                await writer.WriteJsonStructAsync(o.Value, cancellationToken);
            }
            await writer.WriteEndObjectAsync(cancellationToken);
        }
    }
}
