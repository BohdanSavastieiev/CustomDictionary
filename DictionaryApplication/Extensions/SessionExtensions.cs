using DictionaryApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DictionaryApplication.Extensions
{
    public static class SessionExtensions
    {
        public static List<T> GetList<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<T>>(data);
        }

        public static void SetList<T>(this ISession session, string key, List<T> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }


    }
}
