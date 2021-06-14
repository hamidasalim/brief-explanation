using PlanPro.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Interfaces
{
    public interface IProjetService
    {
        Task<List<Projet>> GetAllProjets();
        Task<Projet> GetProject(int idProjet);
        Task<Projet> AddProjet(Projet projetToSave);
        Task<Projet> UpdateProjet(Projet projetToUpdate);
        Task DelteProjet(int idProjet);
        Task<List<Projet>> GetMyProjects(int myId);
    }
}
