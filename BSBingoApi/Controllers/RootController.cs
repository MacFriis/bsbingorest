using System;
using BSBingoApi.Model;
using Microsoft.AspNetCore.Mvc;
using BSBingoApi.Infrastructure;

namespace BSBingoApi.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Words = Link.ToCollection(nameof(WordsController.GetAllWords)),
                Info = Link.To(nameof(InfoController.GetInfo)),
                Languages = Link.ToCollection(nameof(WordsController.GetAvailableLanguages)),
                Categories = Link.ToCollection(nameof(WordsController.GetAvailableCategories)),
                Users = Link.ToCollection(nameof(UsersController.GetVisibleUsers)),
                Token = FormMetadata.FromModel(
                    new PasswordGrantForm(),
                    Link.ToForm(nameof(TokenController.TokenExchange),
                                null, relations: Form.Relation))

            };

            return Ok(response);

        }
    }
}
