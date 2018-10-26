using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using BSBingoApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BSBingoApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSBingoApi.Controllers
{

    [Route("/[controller]")]
    [Authorize]
    [ApiController]
    public class UserinfoController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UserinfoController(IUsersService userService)
        {
            _userService = userService;
        }


        [HttpGet(Name = nameof(Userinfo))]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserInfoResponse>> Userinfo()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The user is not yet registrered"
                });
            }

            var userId = _userService.GetUserIdAsync(User);

            return new UserInfoResponse
            {
                Self = Link.To(nameof(Userinfo)),
                GivenName = user.FirstName,
                FamilyName = user.LastName,
                Subject = Url.Link(
                    nameof(UsersController.GetUserById),
                    new { userId })
            };

        }
    }
}
