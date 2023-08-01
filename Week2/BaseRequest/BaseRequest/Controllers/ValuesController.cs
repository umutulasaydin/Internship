using BaseRequest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<BaseResponse<string>> Testo(XRequest request)
        {
            //await Service.callMethod(request);
            return new BaseResponse<string>();

        }
    }
}
