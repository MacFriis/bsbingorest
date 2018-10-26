using System;
using BSBingoApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BSBingoApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : Controller
    {
        private readonly ApiInfo _apiInfo;
        public InfoController(IOptions<ApiInfo> apiInfoWrapper)
        {
            _apiInfo = apiInfoWrapper.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<ApiInfo> GetInfo()
        {
            //throw new NotImplementedException();
            //  _apiInfo.Href = Url.Link(nameof(GetInfo), null);

            return _apiInfo;
        }
    }
}
