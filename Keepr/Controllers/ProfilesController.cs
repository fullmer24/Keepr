using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<Account>> GetById(string id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Account account = _profilesService.GetById(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<Keeps>>> GetKeeps(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<Keeps> keeps = _profilesService.GetKeeps(id, user?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}