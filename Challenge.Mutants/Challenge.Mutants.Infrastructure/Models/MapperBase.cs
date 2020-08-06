using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Infrastructure.Models
{
    public abstract class MapperBase<TSource, TModel>
        where TSource : class
        where TModel : class
    {
        public virtual TModel MapEntityToModel(TSource entity) => JsonConvert.DeserializeObject<TModel>(JsonConvert.SerializeObject(entity));

        public virtual TSource MapModelToEntity(TModel model) => JsonConvert.DeserializeObject<TSource>(JsonConvert.SerializeObject(model));

        public IEnumerable<TModel> MapEntityToModelCollection(IEnumerable<TSource> entities) => entities.Select(x => MapEntityToModel(x));

        public IEnumerable<TSource> MapModelToEntityCollection(IEnumerable<TModel> models) => models.Select(x => MapModelToEntity(x));
    }
}
