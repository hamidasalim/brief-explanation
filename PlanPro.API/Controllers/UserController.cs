using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanPro.Entities.Models;
using PlanPro.Entities.UserConstant;
using PlanPro.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                IList<string> userRoles = await userManager.GetRolesAsync(user);

                //A vérifier
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                //foreach (var userRole in userRoles)
                //{
                authClaims.Add(new Claim(ClaimTypes.Role, userRoles[0]));
                //}

                /****************/
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userByNameExists = await userManager.FindByNameAsync(model.Username);
            var userByEmailExists = await userManager.FindByEmailAsync(model.Email);
            if (userByNameExists != null || userByEmailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Employe))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Employe));
                await AddClaimsToRoleAsync(UserRoles.Employe);
            }


            if (await roleManager.RoleExistsAsync(UserRoles.Employe))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Employe);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                await AddClaimsToRoleAsync(UserRoles.Admin);
            }

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> ChangeRoles([FromBody] UpdateRoleModel UpdateRoleModel)
        {
            var user = await userManager.FindByNameAsync(UpdateRoleModel.Username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            if (UpdateRoleModel.RoleName.ToLower() == UserRoles.Admin.ToLower())
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Unauthorized to execute: Cannot make other administrator !" });

            if (!await roleManager.RoleExistsAsync(UpdateRoleModel.RoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(UpdateRoleModel.RoleName));
                await AddClaimsToRoleAsync(UpdateRoleModel.RoleName);
            }

            if (await roleManager.RoleExistsAsync(UpdateRoleModel.RoleName))
            {
                await userManager.AddToRoleAsync(user, UpdateRoleModel.RoleName);
            }

            return Ok(new Response { Status = "Success", Message = "Role updated successfully!" });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.ChefProjet)]
        public async Task<IActionResult> ChangeRoleByChefProjet([FromBody] UpdateRoleModel UpdateRoleModel)
        {
            var user = await userManager.FindByNameAsync(UpdateRoleModel.Username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            IList<string> userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Contains(UserRoles.Admin))
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Unauthorized to execute: Cannot change role Admin !" });

            if (userRoles.Contains(UserRoles.ChefProjet))
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Unauthorized to execute: Cannot change role Chef_Projet !" });

            if (UpdateRoleModel.RoleName.ToLower() == UserRoles.ChefProjet.ToLower())
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Unauthorized to execute : Cannot grant role Chef_Projet !" });

            if (!await roleManager.RoleExistsAsync(UpdateRoleModel.RoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(UpdateRoleModel.RoleName));
                await AddClaimsToRoleAsync(UpdateRoleModel.RoleName);
            }


            if (await roleManager.RoleExistsAsync(UpdateRoleModel.RoleName))
            {
                await userManager.AddToRoleAsync(user, UpdateRoleModel.RoleName);
            }

            return Ok(new Response { Status = "Success", Message = "Role updated successfully!" });
        }

        private async Task AddClaimsToRoleAsync(string RoleName)
        {
            IdentityRole role = roleManager.FindByNameAsync(RoleName).Result;
            //tester si role null
            List<Claim> claims = new List<Claim>();
            switch (RoleName)
            {
                case UserRoles.Admin:
                    claims = UserClaims.AdminClaims();
                    foreach (Claim claim in claims)
                    {
                        await roleManager.AddClaimAsync(role, claim);
                    }
                    break;
                case UserRoles.ChefProjet:
                    claims = UserClaims.ChefProjetClaims();
                    foreach (Claim claim in claims)
                    {
                        await roleManager.AddClaimAsync(role, claim);
                    }
                    break;
                case UserRoles.ChefEquipe:
                    claims = UserClaims.ChefEquipeClaims();
                    foreach (Claim claim in claims)
                    {
                        await roleManager.AddClaimAsync(role, claim);
                    }
                    break;
                case UserRoles.Employe:
                    claims = UserClaims.EmployeClaims();
                    foreach (Claim claim in claims)
                    {
                        await roleManager.AddClaimAsync(role, claim);
                    }
                    break;
            }
        }

        /*public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<ApplicationUser> users = await ApplicationUser.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, null);
                return BadRequest(ex.Message);
            }
        }*/
    }
}