using System.Collections.Generic;
using System.Linq;

namespace SolidJson.Impl.TypeHandler
{
    public class TypeHandlerArray<TArr, TElem> : TypeHandlerBase<TArr>
    {
        public TypeHandlerArray(IJsonTypeHandler<TElem> elementTypeHandler)
        {
            ElementTypeHandler = elementTypeHandler;
        }

        private IJsonTypeHandler<TElem> ElementTypeHandler { get; }

        public override IJsonStruct CreateJsonStruct(IJsonFactory factory, object data)
        {
            var jsonArr = new JsonArray(factory);
            if (data != null)
            {
                foreach (var e in ((IEnumerable<TElem>)data))
                {
                    jsonArr.Add(ElementTypeHandler.CreateJsonStruct(factory, e));
                }
            }
            return jsonArr;
        }

        public override TArr CreateType(IJsonStruct jsonStruct)
        {
            return (TArr)(object)new ArrayProxy<TElem>((IJsonArray)jsonStruct);
        }

        public override IJsonStruct CreateNewJsonStruct(IJsonFactory factory)
        {
            return new JsonArray(factory);
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null || !((IEnumerable<TElem>)value).Any();
        }
    }
}
