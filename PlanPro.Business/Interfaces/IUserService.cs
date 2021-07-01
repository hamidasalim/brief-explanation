using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<List<ApplicationUser>> GetAllChefEquipes();
        Task<List<ApplicationUser>> GetAllChefProjet();
        Task<List<ApplicationUser>> GetAllEmployee();
        Task<List<ApplicationUser>> GetAllEmployeeAndChefEquipe();
    }
}
