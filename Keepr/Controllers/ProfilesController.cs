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
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public ProfilesController(ProfilesService profilesService, VaultsService vaultsService, KeepsService keepsService)
        {
            _profilesService = profilesService;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
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
        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vaults>>> GetProfileVaults(string id)
        {
            try
            {

                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<Vaults> vaults = _vaultsService.getVaultsByProfileId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<Vaults>>> GetProfileKeeps(string id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<Keeps> keeps = _keepsService.getKeepsByProfileId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}