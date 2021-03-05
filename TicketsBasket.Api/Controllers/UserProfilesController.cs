using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Services;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Request;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase {

        private readonly IUserProfilesService _userProfilesService;
        public UserProfilesController(IUserProfilesService userProfilesService) {
            _userProfilesService = userProfilesService;
        }

        [ProducesResponseType(200 , Type = typeof(OperationResponse<UserProfileDetail>))]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> Get() {

            var result = await _userProfilesService.GetProfilebyUserIdAsync();
            if (result.IsSuccess) {
                return Ok(result);
            } else {
                return NotFound();
            }

        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfileDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfileDetail>))]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateProfileRequest model) {
            var result = await _userProfilesService.CreateProfileAsync(model);
            if (result.IsSuccess) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [ProducesResponseType(200 , Type = typeof(OperationResponse<UserProfileDetail>))]
        [ProducesResponseType(400 , Type = typeof(OperationResponse<UserProfileDetail>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] IFormFile file) {
            var result = await _userProfilesService.UpdateProfilePictureAsync(file);
            if (result.IsSuccess) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

    }
}
