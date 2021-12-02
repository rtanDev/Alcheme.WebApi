using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alcheme.WebApi.Contracts
{
    public class SerializeContractResolver<T> : DefaultContractResolver
    {
        public static readonly SerializeContractResolver<T> Instance = new SerializeContractResolver<T>();
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType.Name == typeof(T).Name || property.PropertyType.Name == property.DeclaringType.Name)
                property.ShouldSerialize = instance => { return false; };

            if (property.PropertyType != typeof(string))
            {
                if (property.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    property.ShouldSerialize =
                        instance => (instance?.GetType().GetProperty(property.PropertyName).GetValue(instance) as IEnumerable<object>)?.Count() > 0;
            }

            return property;
        }
    }
}
