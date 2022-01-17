using BestDeal.Web.Api.Library.Interface;
using BestDeal.Web.Api.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeastDeal.Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BestDealController : ControllerBase
    {
        private readonly ILogger<BestDealController> _logger;

        private readonly IBestDealService _bestDealService;

        public BestDealController(ILogger<BestDealController> logger,
                                  IBestDealService bestDealService)
        {
            _logger = logger;
            _bestDealService = bestDealService;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<BestDealResponse> GetBestDeal()
        {
            _logger.LogInformation("Received a request to get best deal");

            var response = _bestDealService.GetBestDeal();

            return Ok(response);

        }
    }
}
