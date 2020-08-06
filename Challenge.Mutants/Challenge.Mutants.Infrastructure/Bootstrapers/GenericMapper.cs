using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Mutants.Infrastructure.Bootstrapers
{
    public static class GenericMapper
    {
        public static T MapObject<T>(object source) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));

        public static T MapObjectWithConstructor<T>(object source)
        {
            var propValues = source.GetType().GetProperties().Select(x => x.GetValue(source, null)).ToArray();
            var map = (T)Activator.CreateInstance(typeof(T), propValues);
            return map;
        }

        public static IEnumerable<T> MapObjectCollection<T>(IEnumerable<object> entities) => entities.Select(x => MapObject<T>(x));
    }
}
