using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        public ProfilesController(ProfilesService profilesService)
        {
            _profilesService = profilesService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetById(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Profile profile = _profilesService.GetById(id, user?.Id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}