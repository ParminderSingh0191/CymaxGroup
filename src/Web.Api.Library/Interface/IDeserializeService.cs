namespace BestDeal.Web.Api.Library.Interface
{
    public interface IDeserializeService
    {
        T FromJsonString<T>(string response);

        T FromXmlString<T>(string response);
    }
}
