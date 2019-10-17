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

        public override IJsonStruct CreateJsonStruct(object parent, object data)
        {
            var jsonArr = new JsonArray((IJsonStruct)parent);
            if (data != null)
            {
                foreach (var e in ((IEnumerable<TElem>)data))
                {
                    jsonArr.Add(ElementTypeHandler.CreateJsonStruct(jsonArr, e));
                }
            }
            return jsonArr;
        }

        public override TArr CreateType(IJsonStruct jsonStruct)
        {
            var elements = new List<TElem>();
            if(jsonStruct != null)
            {
                if(jsonStruct is IJsonArray jsonArray)
                {
                    jsonArray.ToList().ForEach(o => elements.Add(ElementTypeHandler.CreateType(o)));
                }
            }
            return (TArr)(object)elements;
        }

        public override IJsonStruct CreateNewJsonStruct(object parent)
        {
            return new JsonArray((IJsonStruct)parent);
        }

        public override bool IsDefaultValue(object value)
        {
            return value == null || !((IEnumerable<TElem>)value).Any();
        }
    }
}
