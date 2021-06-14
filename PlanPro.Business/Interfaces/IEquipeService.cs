using PlanPro.Entities;
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
        Task<List<Equipe>> GetMyEquipe(int myId);
        Task<Equipe> AddEquipe(Equipe equipeToSave);
        Task<Equipe> UpdateEquipe(Equipe equipeToUpdate);
        //Task<Equipe> RemoveMemberEquipe(Equipe equipeToUpdate);
        Task DelteEquipe(int idEquipe);
    }
}
