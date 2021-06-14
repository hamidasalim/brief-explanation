using PlanPro.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Interfaces
{
    public interface ITacheService
    {
        Task<List<Tache>> GetAllTaches();
        Task<Tache> GetTache(int idTache);
        Task<Tache> AddTache(Tache tacheToSave);
        Task<Tache> UpdateTache(Tache tacheToUpdate);
        Task DelteTache(int idTache);
        Task<List<Tache>> GetProjectTaches(int idProjet);
        Task<List<Tache>> GetMyTaches(int idProjet);
    }
}
