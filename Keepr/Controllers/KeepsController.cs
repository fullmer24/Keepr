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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        public KeepsController(KeepsService keepsService)
        {
            _keepsService = keepsService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keeps>> Create([FromBody] Keeps newKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newKeep.creatorId = userInfo.Id;
                Keeps keeps = _keepsService.Create(newKeep);
                keeps.Creator = userInfo;
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Keeps>>> GetAll()
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<Keeps> keeps = _keepsService.GetAll(user?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Keeps> GetById(int id)
        {
            try
            {
                return Ok(_keepsService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Keeps> Update(int id, [FromBody] Keeps update)
        {
            try
            {
                update.Id = id;
                return Ok(_keepsService.Update(update));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}