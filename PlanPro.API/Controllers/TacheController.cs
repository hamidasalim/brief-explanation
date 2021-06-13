﻿using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanPro.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TacheController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITacheService _tacheService;
        private readonly UserManager<ApplicationUser> _manager;

        public TacheController(ITacheService tacheService, ILogger<TacheController> logger,
            UserManager<ApplicationUser> manager)
        {
            _manager = manager;
            _tacheService = tacheService;
            _logger = logger;
        }

        // You can also just take part after return and use it in async methods.
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _manager.FindByNameAsync(HttpContext.User.Identity.Name);
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
                if (id == 0)
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
        [Authorize]
        public async Task<IActionResult> Add([FromBody] Tache tache)
        {
            try
            {
                if (tache == null)
                {
                    return BadRequest("Tache Cannot be null");
                }
                //GET Current user
                ApplicationUser user = GetCurrentUser().Result;
                tache.RealisateurID = user.Id;
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
                //GET Current user
                ApplicationUser user = GetCurrentUser().Result;
                tacheToUpdate.RealisateurID = user.Id;
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

    }
}
