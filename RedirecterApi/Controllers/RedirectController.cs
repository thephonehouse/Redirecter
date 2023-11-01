using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedirecterApi.Helpers;
using RedirecterCore;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Redirecter.Controllers
{
    [Route("/")]
    [ApiController]
    public partial class RedirectController : ControllerBase
    {
        private readonly ILogger<RedirectController> _logger;

        private readonly IRedirectService _redirectService;

        public RedirectController(ILogger<RedirectController> logger, IRedirectService redirectService)
        {
            _logger = logger;
            _redirectService = redirectService;
        }

        [HttpGet]
        public IActionResult GetRedirect(Guid id)
        {
            try
            { 
                var model = _redirectService.GetById(id);

                if (model is null)
                {
                    _logger.LogWarning("Id {id} was not found!", id);
                    return StatusCodePage.NotFound404();
                }

                return Redirect(model.Url);
            } 
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while redirecting an id");
                return StatusCodePage.InternalServerError500();
            }
        }

        [GeneratedRegex("^[a-zA-Z0-9]+$", RegexOptions.Compiled)]
        private static partial Regex NameRegex();

        [HttpGet("{name}")]
        public IActionResult GetRedirect(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || NameRegex().IsMatch(name) is false)
                {
                    _logger.LogWarning("Someone searched for an invalid name: {name}", name);
                    return StatusCodePage.BadRequest400();
                }

                var model = _redirectService.GetByName(name);

                if (model is null)
                {
                    _logger.LogWarning("Name {name} was not found!", name);
                    return StatusCodePage.NotFound404();
                }

                return Redirect(model.Url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while redirecting an id");
                return StatusCodePage.InternalServerError500();
            }
        }

        
    }
}
