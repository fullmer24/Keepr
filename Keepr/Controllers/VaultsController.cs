using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly VaultKeepsService _vaultKeepsService;
        public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
        {
            _vaultsService = vaultsService;
            _vaultKeepsService = vaultKeepsService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vaults>> Create([FromBody] Vaults newVault)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = userInfo?.Id;
                Vaults vaults = _vaultsService.Create(newVault);
                vaults.Creator = userInfo;
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaults>> GetById(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Vaults vaults = _vaultsService.GetById(id, user?.Id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vaults>> Edit(int id, [FromBody] Vaults vaultData)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.Id = id;
                Vaults vaults = _vaultsService.Edit(vaultData, user);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                string message = _vaultsService.Delete(id, user);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeepsVM>>> GetKeepByVaultId(int id, string userId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultKeepsVM> keeps = _vaultKeepsService.GetKeepByVaultId(id, userInfo?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}