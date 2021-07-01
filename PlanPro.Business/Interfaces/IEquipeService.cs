using PlanPro.Entities;
using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Interfaces
{
    public interface IEquipeService
    {
        Task<List<Equipe>> GetAllEquipes();
        Task<Equipe> GetEquipe(int idEquipe);
        Task<List<Equipe>> GetMyEquipe(string myId);
        Task<List<ApplicationUser>> GetEquipeMembers(int idEquipe);
        Task<Equipe> AddEquipe(Equipe equipeToSave);
        Task<Equipe> UpdateEquipe(Equipe equipeToUpdate);
        //Task<Equipe> RemoveMemberEquipe(Equipe equipeToUpdate);
        Task DelteEquipe(int idEquipe);
    }
}
