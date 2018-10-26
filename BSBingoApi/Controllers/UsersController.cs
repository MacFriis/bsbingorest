using System;
using System.Linq;
using System.Threading.Tasks;
using BSBingoApi.Model;
using BSBingoApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BSBingoApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly PagingOptions _defaultPagingOptions;
        private readonly IAuthorizationService _authzService;


        public UsersController(
            IUsersService usersService,
            IOptions<PagingOptions> defaultPagingOptions,
            IAuthorizationService authorizationService)
        {
            _userService = usersService;
            _defaultPagingOptions = defaultPagingOptions.Value;
            _authzService = authorizationService;
        }



        [Authorize]
        [HttpGet(Name = nameof(GetVisibleUsers))]
        public async Task<ActionResult<PagedCollection<User>>> GetVisibleUsers(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<User, UserEntity> sortOptions,
            [FromQuery] SearchOptions<User, UserEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var users = new PagedResults<User>();
            if (User.Identity.IsAuthenticated)
            {
                var canSeeEveryone = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryone.Succeeded)
                {

                    users = await _userService.GetUsersAsync(
                    pagingOptions, sortOptions, searchOptions);


                }
                else
                {
                    var myself = await _userService.GetUserAsync(User);
                    users.Items = new[] { myself };
                    users.TotalSize = 1;
                }
            }



            var collection = PagedCollection<User>.Create(
                Link.ToCollection(nameof(GetVisibleUsers)),
                users.Items.ToArray(),
                users.TotalSize,
                pagingOptions);

            return collection;
        }


        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [HttpGet("{userId}", Name = nameof(GetUserById))]
        public async Task<ActionResult<User>> GetUserById(Guid userId)
        {
            var currentUserId = await _userService.GetUserIdAsync(User);
            if (currentUserId == null) return NotFound();

            if (currentUserId == userId)
            {
                var myself = await _userService.GetUserAsync(User);
                return myself;
            }

            var canSeeEveryone = await _authzService.AuthorizeAsync(
                User, "ViewAllUsersPolicy");
            if (!canSeeEveryone.Succeeded) return NotFound();

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            return user;
        }

        [Authorize]
        [HttpPost(Name = nameof(RegisterUser))]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterForm form)
        {
            var (success, message) = await _userService.CreateUserAsync(form);
            if (success) return Created(Url.Link(nameof(UserinfoController.Userinfo), null),
                                        null);

            return BadRequest(new ApiError
            {
                Message = "Registration failed",
                Detail = message
            });
        }

    }
}
