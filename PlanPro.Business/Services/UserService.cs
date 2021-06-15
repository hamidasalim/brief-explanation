﻿using Microsoft.AspNetCore.Identity;
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
        private PlanProDbContext _planProDbContext;
        public UserService(UserManager<ApplicationUser> userManager, PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }


        public async Task<List<ApplicationUser>> GetAllChefEquipes()
        {
            return await _userManager.GetUsersInRoleAsync(UserRoles.ChefEquipe) as List<ApplicationUser>;
        }


        public async Task<List<ApplicationUser>> GetAllEmployee()
        {
            return await _userManager.GetUsersInRoleAsync(UserRoles.Employe) as List<ApplicationUser>;
        }




    }
}
