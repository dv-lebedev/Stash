using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utils
{
    public class AdapterUtils<T>
    {
        private readonly Dictionary<string, PropertyInfo> properties;

        public AdapterUtils()
        {
            properties = typeof(T).GetProperties().ToDictionary(i => i.Name);
        }

        public void CopyProperties(T source, T destination)
        {
            foreach(var p in properties)
            {
                p.Value.SetValue(destination, p.Value.GetValue(source));
            }
        }
    }
}
