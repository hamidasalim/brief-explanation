using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanPro.Business.Interfaces;
using PlanPro.Entities;
using PlanPro.Entities.Models;
using PlanPro.Entities.UserConstant;
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
        private readonly UserManager<ApplicationUser> _manager;


        public ProjetController(IProjetService projetService, ILogger<ProjetController> logger, UserManager<ApplicationUser> manager)
        {
            _projetService = projetService;
            _manager = manager;
            _logger = logger;
        }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _manager.FindByNameAsync(HttpContext.User.Identity.Name);

            // Console.WriteLine(HttpContext.User);
        }
        /* private async Task<ApplicationUser> GetCurrentProject()
         {

         }*/
        [HttpGet()]
        //[Authorize(Policy = Policies.VIEW_ALL_PROJECTS)]
        [Authorize(Roles = UserRoles.Admin)]
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
     //   [Authorize(Roles = UserRoles.Admin + "," + UserRoles.ChefProjet + "," + UserRoles.ChefEquipe) ]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0)
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
       // [Authorize(Roles = UserRoles.Admin + "," + UserRoles.ChefProjet)]
        public async Task<IActionResult> Add([FromBody] Projet projet)
        {
            try
            {
                ApplicationUser user = GetCurrentUser().Result;
                if (user.Id.Equals(0))
                {
                    return BadRequest("ID User Cannot be empty");
                }
                if (projet == null)
                {
                    return BadRequest("Projet Cannot be null");
                }
                projet.ChefProjetID = user.Id;
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
      //  [Authorize(Roles = UserRoles.Admin + "," + UserRoles.ChefProjet + "," + UserRoles.ChefEquipe)]
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
        //[Authorize(Roles = UserRoles.Admin + "," + UserRoles.ChefProjet )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID Projet Cannot be empty");
                }
                //ApplicationUser user = GetCurrentUser().Result;
               /* if (user.Id != projet.ChefProjetID)
                { return BadRequest("Only The creator can delete the project"); }*/
                await _projetService.DelteProjet(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet()]
       // [Authorize(Roles =  UserRoles.ChefProjet )]
        public async Task<IActionResult> GetMy()
        {
            try
            {
                ApplicationUser user = GetCurrentUser().Result;
                if (user.Id.Equals(0))
                {
                    return BadRequest("ID UserCannot be empty");
                }
                List<Projet> projets = await _projetService.GetMyProjects((user.Id).ToString());
                
                return Ok(projets);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }

    }
}
