using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanPro.Business.Interfaces;
using PlanPro.Entities.Models;
using PlanPro.Entities.UserConstant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<ApplicationUser> _manager;
        private PlanProDbContext _planProDbContext;
        public UserService(UserManager<ApplicationUser> userManager, UserManager<ApplicationUser> manager, PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _manager = manager;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

      

        /*public async Task<ApplicationUser> GetUser(int id)
        {
            return await _userManager.GetUserAsync(id);
        }*/


        public async Task<List<ApplicationUser>> GetAllChefEquipes()
        {
            return await _userManager.GetUsersInRoleAsync(UserRoles.ChefEquipe) as List<ApplicationUser>;
        }
        public async Task<List<ApplicationUser>> GetAllChefProjet()
        {
            return await _userManager.GetUsersInRoleAsync(UserRoles.ChefProjet) as List<ApplicationUser>;
        }


        public async Task<List<ApplicationUser>> GetAllEmployee()
        {
            return await _userManager.GetUsersInRoleAsync(UserRoles.Employe) as List<ApplicationUser>;
        }

        public async Task<List<ApplicationUser>> GetAllEmployeeAndChefEquipe()
        {
            List<ApplicationUser> list2= await _userManager.GetUsersInRoleAsync(UserRoles.ChefEquipe) as List<ApplicationUser>;
            List<ApplicationUser> list1= await _userManager.GetUsersInRoleAsync(UserRoles.Employe) as List<ApplicationUser>;
            foreach(ApplicationUser user in list1)
            {
                list2.Add(user);
            }
            return list2;
        }




    }
}
