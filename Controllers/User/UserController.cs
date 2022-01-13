using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BlogAnywhereNET.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using BlogAnywhereNET.Services.PostService;

namespace BlogAnywhereNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _config = config;
            _userService = userService;
        }

        // Edit Post
        [HttpPut("{id}")]
        [Authorize]
        public async Task<object> UpdatePost(UserForEdit userForEdit)
        {
            var update = await _userService.EditUser(userForEdit);

            return Ok(new { updatedUser = update });
        }
    }
}

