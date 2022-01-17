using AutoMapper;
using BestDeal.FakeResponse.Models;
using BestDeal.Web.Api.Library.Interface;
using BestDeal.Web.Api.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BestDeal.Web.Api.Library.Implementation
{
    public class BestDealService : IBestDealService
    {
        private readonly IMapper _mapper;

        private readonly IDeserializeService _deserializeService;

        private readonly IApiResponseService _apiResponseService;

        private List<BestDealResponse> bestDealResponses = new List<BestDealResponse>();

        public BestDealService(IMapper mapper, 
                               IDeserializeService deserializeService,
                               IApiResponseService apiResponseService)
        {
            _mapper = mapper;
            _deserializeService = deserializeService;
            _apiResponseService = apiResponseService;
        }

        public BestDealResponse GetBestDeal()
        {
            // deserialize each response depends on if string starts with
            // '{' --> JSON
            // otherwise XML
            DeserializeResponse<API1Response>(_apiResponseService.GetApi1Response(), "API1");
            DeserializeResponse<API2Response>(_apiResponseService.GetApi2Response(), "API2");
            DeserializeResponse<API3Response>(_apiResponseService.GetApi3Response(), "API3");

            return bestDealResponses.OrderBy(d => d.Amount)
                                    .ThenBy(d => d.APIName)
                                    .FirstOrDefault();
        }

        private void DeserializeResponse<T>(string response, string apiName)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                throw new ArgumentNullException("Response string is Invalid");
            }

            T apiResponse;

            if(response.StartsWith('{'))
            {
                apiResponse = _deserializeService.FromJsonString<T>(response);
            }
            else
            {
                apiResponse = _deserializeService.FromXmlString<T>(response);
            }

            if (apiResponse != null)
            {
                AddBestDealResponse(apiResponse, apiName);
            }
        }

        private void AddBestDealResponse<T>(T apiResponse, string apiName)
        {
            var bestDealResponse = _mapper.Map<BestDealResponse>(apiResponse);
            bestDealResponse.APIName = apiName;
            bestDealResponses.Add(bestDealResponse);
        }
    }
}
