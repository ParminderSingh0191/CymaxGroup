using BestDeal.FakeResponse;
using BestDeal.Web.Api.Library.Interface;

namespace BestDeal.Web.Api.Library.Implementation
{
    public class ApiResponseService : IApiResponseService
    {
        public string GetApi1Response()
        {
            return FakeReponse.GetAPI1Response();
        }

        public string GetApi2Response()
        {
            return FakeReponse.GetAPI2Response();
        }

        public string GetApi3Response()
        {
            return FakeReponse.GetAPI3Response();
        }
    }
}
