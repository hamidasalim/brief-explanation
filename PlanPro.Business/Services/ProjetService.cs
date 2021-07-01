using Microsoft.EntityFrameworkCore;
using PlanPro.Business.Interfaces;
using PlanPro.Business.IServices;
using PlanPro.Entities;
using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Services
{
    public class ProjetService : IProjetService
    {
        private IRepository<Projet> _projetRepository;
        private IRepository<Tache> _tasksRepository;
        private PlanProDbContext _planProDbContext;

        public ProjetService(PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _projetRepository = new Repository<Projet>(_planProDbContext);
            _tasksRepository = new Repository<Tache>(_planProDbContext);
        }

        public async Task<List<Projet>> GetAllProjets()
        {
            return await _projetRepository.GetAllAsync();
            // await _planProDbContext.Projets.ToListAsync();
        }
        public async Task<List<Projet>> GetMyProjects(string myId)
        {
            List<Projet> projectList = await _projetRepository.GetAllAsync();
            Boolean yes = false;
            List<Projet> myProjectList = new List<Projet>();
            foreach (Projet projet in projectList )
            {
                if (projet.ChefProjetID !=null)
                {
                    if (projet.ChefProjetID.Equals(myId))
                    {
                        myProjectList.Add(projet);
                    }
                    else
                    {
                        if (projet.IdParticipants != null)
                        {
                            foreach (string id in projet.IdParticipants)
                            {
                                if (id.Equals(myId))
                                {
                                    yes = true;
                                    break;
                                }
                            }
                            if (yes == true)
                                myProjectList.Add(projet);
                        }
                    }
                }
                
            }

            return myProjectList;
         
        }

        public async Task<Projet> GetProject(int idProjet)
        {
            Projet projet= await _projetRepository.GetByIdAsync(idProjet);
            projet.Tasks = await GetProjectTaches(projet.ID);
            return projet;
        }

        private async Task<List<Tache>> GetProjectTaches(int idProjet)
        {
            List<Tache> tasksList = await _tasksRepository.GetAllAsync();
            List<Tache> projectTaskList = new List<Tache>();
            foreach (Tache tache in tasksList)
            {
                if (tache.ProjetID.Equals(idProjet))
                {
                    projectTaskList.Add(tache);
                }
            }
            return projectTaskList;
        }

        public async Task<Projet> AddProjet(Projet projetToSave)
        {
            await _projetRepository.AddAsync(projetToSave);
            await _projetRepository.CommitAsync();
            return projetToSave;
        }

        public async Task<Projet> UpdateProjet(Projet projetToUpdate)
        {
            Projet updatedProjet = _projetRepository.Update(projetToUpdate);
            await _projetRepository.CommitAsync();
            return updatedProjet;
        }

        public async Task DelteProjet(int idProjet)
        {
            Projet projetToDelete = await GetProject(idProjet);
            if (projetToDelete == null)
            {
                throw new Exception($"Projet with id={idProjet} not found");
            }
            foreach( Tache task in projetToDelete.Tasks)
            {
                _tasksRepository.Remove(task);
            }
            _projetRepository.Remove(projetToDelete);
            await _projetRepository.CommitAsync();
        }

        
        

        
    }
}
