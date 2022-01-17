using System.Xml;
using System.Xml.Serialization;

namespace BestDeal.FakeResponse.Models
{
    public class API3Response
    {
        [XmlElement("quote")]
        public double Quote { get; set; }
    }
}
