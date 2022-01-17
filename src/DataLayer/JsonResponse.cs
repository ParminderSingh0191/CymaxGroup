using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BestDeal.FakeResponse
{
    public static class JsonResponse
    {
        public static string ConvertResponse(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
