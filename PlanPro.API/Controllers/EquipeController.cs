using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanPro.Business.Interfaces;
using PlanPro.Entities;
using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanPro.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEquipeService _equipeService;
        private readonly UserManager<ApplicationUser> _manager;

        public EquipeController(IEquipeService equipeService, ILogger<EquipeController> logger)
        {
            _equipeService = equipeService;
            _logger = logger;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _manager.FindByNameAsync(HttpContext.User.Identity.Name);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Equipe> equipes = await _equipeService.GetAllEquipes();
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        /*public async Task<IActionResult> GetMy(int myId)
        {
            try
            {
                ApplicationUser user = GetCurrentUser().Result;
                if (user.Id.Equals(0))
                {
                    return BadRequest("User not found ");
                }
                List<Equipe> equipes = await _equipeService.GetMyEquipe(Int16.Parse(user.Id));
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }*/

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("ID Equipe Cannot be empty");
                }
                Equipe equipe = await _equipeService.GetEquipe(id);
                return Ok(equipe);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]

        public async Task<IActionResult> Add([FromBody] Equipe equipe)
        {
            try
            {
                if (equipe == null)
                {
                    return BadRequest("Equipe Cannot be null");
                }
                /*ApplicationUser user = GetCurrentUser().Result;
                if (user.Id.Equals(0))
                {
                    return BadRequest(" ID USER Cannot be empty");
                }
                equipe.IDChef = user.Id;*/
                Equipe savedEquipe = await _equipeService.AddEquipe(equipe);
                return Ok(savedEquipe);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Update([FromBody] Equipe equipeToUpdate)
        {
            try
            {
                if (equipeToUpdate == null)
                {
                    return BadRequest("Equipe Cannot be null");
                }
                Equipe updatedEquipe = await _equipeService.UpdateEquipe(equipeToUpdate);

                return Ok(updatedEquipe);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID Equipe Cannot be empty");
                }
                await _equipeService.DelteEquipe(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

    }
}
