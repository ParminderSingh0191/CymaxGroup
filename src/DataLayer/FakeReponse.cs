using BestDeal.FakeResponse.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BestDeal.FakeResponse
{
    public static class FakeReponse
    {
        public static string GetAPI1Response()
        {
            return JsonResponse.ConvertResponse(new {
                Total = 10
            });
        }

        public static string GetAPI2Response()
        {
            return JsonResponse.ConvertResponse(new {
                Amount = 20
            });
        }

        public static string GetAPI3Response()
        {
            var response = new API3Response {
                Quote = 2
            };

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                CheckCharacters = false
            };

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                var serializer = new XmlSerializer(response.GetType());
                serializer.Serialize(writer, response, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                return stream.ToString();
            }
        }
    }
}
