using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanPro.Business.Interfaces;
using PlanPro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanPro.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjetController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProjetService _projetService;

        public ProjetController(IProjetService projetService, ILogger<ProjetController> logger)
        {
            _projetService = projetService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Projet> projets = await _projetService.GetAllProjets();
                return Ok(projets);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("ID Projet Cannot be empty");
                }
                Projet projet = await _projetService.GetProject(id);
                return Ok(projet);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] Projet projet)
        {
            try
            {
                if (projet == null)
                {
                    return BadRequest("Projet Cannot be null");
                }
                Projet savedProjet = await _projetService.AddProjet(projet);
                return Ok(savedProjet);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Update([FromBody] Projet projetToUpdate)
        {
            try
            {
                if (projetToUpdate == null)
                {
                    return BadRequest("Projet Cannot be null");
                }
                Projet updatedProjet = await _projetService.UpdateProjet(projetToUpdate);
                return Ok(updatedProjet);
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
                    return BadRequest("ID Projet Cannot be empty");
                }
                await _projetService.DelteProjet(id);
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
