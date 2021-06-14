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
    public class TacheController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITacheService _tacheService;

        public TacheController(ITacheService tacheService, ILogger<TacheController> logger)
        {
            _tacheService = tacheService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Tache> taches = await _tacheService.GetAllTaches();
                return Ok(taches);
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
                    return BadRequest("ID Tache Cannot be empty");
                }
                Tache tache = await _tacheService.GetTache(id);
                return Ok(tache);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] Tache tache)
        {
            try
            {
                if (tache == null)
                {
                    return BadRequest("Tache Cannot be null");
                }
                Tache savedTache = await _tacheService.AddTache(tache);
                return Ok(savedTache);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Update([FromBody] Tache tacheToUpdate)
        {
            try
            {
                if (tacheToUpdate == null)
                {
                    return BadRequest("Tache Cannot be null");
                }
                Tache updatedTache = await _tacheService.UpdateTache(tacheToUpdate);
                return Ok(updatedTache);
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
                    return BadRequest("ID Tache Cannot be empty");
                }
                await _tacheService.DelteTache(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{idProjet}")]
        public async Task<IActionResult> GetProjectTask(int idProjet)
        {
            try
            {
                List<Tache> taches = await _tacheService.GetProjectTaches(idProjet);
                return Ok(taches);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{myId}")]
        public async Task<IActionResult> GetMyTask(int myId)
        {
            try
            {
                List<Tache> taches = await _tacheService.GetMyTaches(myId);
                return Ok(taches);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

    }
}
