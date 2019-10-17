using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SolidJson.Impl
{
    /// <summary>
    /// Represents an object proxy.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectProxy<T> : DispatchProxy, IJsonObject
    {
        private static readonly ConcurrentDictionary<MethodInfo, Func<object[], IJsonObject, object>> s_Invokers = new ConcurrentDictionary<MethodInfo, Func<object[], IJsonObject, object>>();
        private static readonly IEnumerable<Action<T>> s_DefaultValueSetters = CreateDefaultValueSetters();

        private static IEnumerable<Action<T>> CreateDefaultValueSetters()
        {

            var props = typeof(T).GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).Select(o => o);
            return props.Select<PropertyInfo, Action<T>>(o =>
            {
                var binding = o.GetCustomAttributes<DataMemberAttribute>().FirstOrDefault();
                var emitDefaultValue = binding?.EmitDefaultValue ?? true;
                var defaultValue = new object[] { GetDefaultValue(o.PropertyType) };
                if (!emitDefaultValue) return null;
                return _ =>
                {
                    ((ObjectProxy<T>)(object)_).Invoke(o.GetSetMethod(), defaultValue);
                };
            }).Where(o => o != null)
            .ToArray();
        }

        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType) return Activator.CreateInstance(type);
            return null;
        }

        /// <summary>
        /// Sets the default values on supplied proxy
        /// </summary>
        /// <param name="proxy"></param>
        public static void SetDefaultValues(T proxy)
        {
            foreach (var a in s_DefaultValueSetters)
            {
                a(proxy);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectProxy()
        {
        }

        internal IJsonObject JsonObject { get; set; }

        #region IJsonObject 
        IJsonStruct IJsonObject.this[string name] { get => JsonObject[name]; set => JsonObject[name] = value; }

        bool IJsonObject.Remove(string name)
        {
            return JsonObject.Remove(name);
        }
        IJsonFactory IJsonStruct.Factory => JsonObject.Factory;
        IJsonStruct IJsonStruct.Parent => JsonObject.Parent;

        IJsonStruct IJsonStruct.RawStruct => JsonObject.RawStruct;

        T1 IJsonStruct.GetParent<T1>()
        {
            return JsonObject.GetParent<T1>();
        }

        T1 IJsonStruct.As<T1>()
        {
            return JsonObject.As<T1>();
        }

        object IJsonStruct.As(Type type)
        {
            return JsonObject.As(type);
        }

        string IJsonStruct.AsJson(bool pretty)
        {
            return JsonObject.AsJson(pretty);
        }

        Task IJsonStruct.WriteAsync(IJsonWriter writer, CancellationToken cancellationToken)
        {
            return JsonObject.WriteAsync(writer, cancellationToken);
        }

        T1 IJsonObject.GetProxy<T1>()
        {
            return JsonObject.GetProxy<T1>();
        }

        #endregion

        /// <summary>
        /// Invokes the method
        /// </summary>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            return s_Invokers.GetOrAdd(targetMethod, CreateInvoker).Invoke(args, JsonObject);
        }

        private Func<object[], IJsonObject, object> CreateInvoker(MethodInfo targetMethod)
        {
            if (targetMethod.DeclaringType == typeof(IJsonStruct))
            {
                if (targetMethod.Name == nameof(IJsonStruct.Parent))
                {
                    return (args, jsonObject) => jsonObject.Parent;
                }
                if (targetMethod.Name == nameof(IJsonStruct.RawStruct))
                {
                    return (args, jsonObject) => jsonObject.RawStruct;
                }
            }
            if (targetMethod.DeclaringType == typeof(IJsonObject))
            {
                if (targetMethod.Name == nameof(IJsonObject.Remove))
                {
                    return (args, jsonObject) => jsonObject.Remove((string)args[0]);
                }
                if (targetMethod.Name == "get_Item")
                {
                    return (args, jsonObject) => jsonObject[(string)args[0]];
                }
                if (targetMethod.Name == "set_Item")
                {
                    return (args, jsonObject) => jsonObject[(string)args[0]] = (IJsonStruct)args[1];
                }
            }
            if (targetMethod.Name.StartsWith("get_") || targetMethod.Name.StartsWith("set_"))
            {
                var propertyName = targetMethod.Name.Substring(4);
                var property = targetMethod.DeclaringType.GetProperties().Single(o => o.Name == propertyName);
                var propertyType = property.PropertyType;
                var defaultValue = GetDefaultValue(propertyType);
                var binding = property.GetCustomAttributes<DataMemberAttribute>().FirstOrDefault();
                var jsonName = binding?.Name ?? propertyName;
                var emitDefaultValue = binding?.EmitDefaultValue ?? true;
                var action = targetMethod.Name.Substring(0, 3);
                switch (action)
                {
                    case "get":
                        if(property.GetMethod.GetParameters().Length == 0)
                        {
                            return (args, jsonObject) =>
                            {
                                var jsonStruct = jsonObject[jsonName];
                                if (jsonStruct == null)
                                {
                                    jsonStruct = jsonObject.Factory.CreateNewJsonStruct(propertyType);
                                    jsonObject[jsonName] = jsonStruct;
                                }
                                return jsonStruct.As(propertyType);
                            };
                        }
                        else
                        {
                            return (args, jsonObject) =>
                            {
                                var propName = args[0].ToString();
                                var jsonStruct = jsonObject[propName];
                                if (jsonStruct == null)
                                {
                                    jsonStruct = jsonObject.Factory.CreateNewJsonStruct(propertyType);
                                    jsonObject[propName] = jsonStruct;
                                }
                                return jsonStruct.As(propertyType);
                            };
                        }
                    case "set":
                        if(property.SetMethod.GetParameters().Length == 1)
                        {
                            return (args, jsonObject) =>
                            {
                                var jsonFactory = JsonObject.Factory;
                                if (!emitDefaultValue && jsonFactory.IsDefaultValue(propertyType, args[0]))
                                {
                                    jsonObject.Remove(jsonName);
                                }
                                else
                                {
                                    var newObject = jsonFactory.CreateJsonStruct(propertyType, args[0]);
                                    jsonObject[jsonName] = newObject;
                                }
                                return null;
                            };
                        }
                        else
                        {
                            return (args, jsonObject) =>
                            {
                                var propName = args[0].ToString();
                                var jsonFactory = JsonObject.Factory;
                                if (!emitDefaultValue && jsonFactory.IsDefaultValue(propertyType, args[1]))
                                {
                                    jsonObject.Remove(propName);
                                }
                                else
                                {
                                    var newObject = jsonFactory.CreateJsonStruct(propertyType, args[1]);
                                    jsonObject[propName] = newObject;
                                }
                                return null;
                            };
                        }
                }

            }
            throw new Exception($"Cannot handle method:{targetMethod.Name}");
        }
    }
}
