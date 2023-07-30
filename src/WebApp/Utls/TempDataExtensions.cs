using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureSample.WebApp
{
    public static class TempDataExtensions
    {
        public static void SaveData(this ITempDataDictionary tempData, string key, object value)
        {
            string str = JsonConvert.SerializeObject(value);
            tempData[key] = str;
        }
        public static bool TryGetData<T>(this ITempDataDictionary tempData, string key, out T value)
        {
            if (tempData.TryGetValue(key, out var obj))
            {
                value = obj == null ? default(T) : JsonConvert.DeserializeObject<T>(obj as string);
                return true;
            }
            value = default(T);
            return false;
        }
        public static T GetData<T>(this ITempDataDictionary tempData, string key)
        {
            var obj = tempData[key];
            return obj == null ? default(T) : JsonConvert.DeserializeObject<T>(obj as string);
        }
    }
}
