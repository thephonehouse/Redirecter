using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedirecterApi.Helpers;

namespace Redirecter.Controllers
{
    [Route("/")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly ILogger<RedirectController> _logger;
        private readonly IDictionary<int, RedirectModel> _redirects;

        public RedirectController(ILogger<RedirectController> logger, IDictionary<int, RedirectModel> redirects)
        {
            this._logger = logger;
            this._redirects = redirects;
        }

        [HttpGet]
        public IActionResult GetRedirect(int id)
        {
            try
            { 
                if (_redirects.TryGetValue(id, out var model) is false)
                {
                    _logger.LogWarning("Someone searched for an invalid id {id}", id);
                    return StatusCodePage.NotFound404();
                }

                Response.Redirect(model.Url);

                return StatusCodePage.TemporaryRedirect302(model.Url);
            } 
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while redirecting an id");
                return StatusCodePage.InternalServerError500();
            }
        }
    }
}
