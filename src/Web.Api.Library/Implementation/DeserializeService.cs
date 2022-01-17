using BestDeal.Web.Api.Library.Interface;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace BestDeal.Web.Api.Library.Implementation
{
    public class DeserializeService : IDeserializeService
    {
        public T FromJsonString<T>(string response)
        {
            var details = JsonConvert.DeserializeObject<T>(response);

            return details;
        }

        public T FromXmlString<T>(string response)
        {
            using (var reader = new StringReader(response))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
