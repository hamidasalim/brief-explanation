

using PlanPro.Business.Interfaces;
using PlanPro.Business.IServices;
using PlanPro.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanPro.Business.Services
{
    public class TacheService : ITacheService
    {
        private IRepository<Tache> _tasksRepository;
        private PlanProDbContext _planProDbContext;

        public TacheService(PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _tasksRepository = new Repository<Tache>(_planProDbContext);
        }

        public async Task<List<Tache>> GetAllTaches()
        {
            return await _tasksRepository.GetAllAsync();
            // await _planProDbContext.Projets.ToListAsync();
        }  

        public async Task<List<Tache>> GetProjectTaches(int idProjet)
        {
            List <Tache> taskList = await _tasksRepository.GetAllAsync();
            List<Tache> projectTaskList = new List<Tache>();
            foreach (Tache task in taskList)
            {
                if(task.ProjetID.Equals(idProjet))
                {
                    projectTaskList.Add(task);
                }
            }
            return projectTaskList;
            // await _planProDbContext.Projets.ToListAsync();
        }  

        public async Task<List<Tache>> GetMyTaches(String myId)
        {
            List<Tache> taskList = await _tasksRepository.GetAllAsync();
            List<Tache> myTaskList = new List<Tache>(); 
            foreach (Tache task in taskList)
            {
                if (task != null)
                {
                    if (/*task.RealisateurID.Equals(myId) ||*/ task.CreatorId.Equals(myId))
                        
                        {
                        myTaskList.Add(task);
                    }
                }
            }
            return myTaskList;
            // await _planProDbContext.Projets.ToListAsync();
        }
        public async Task<List<Tache>> GetMyProjectTasks(string myId, int idProjet)
        {
            List<Tache> taskList = await _tasksRepository.GetAllAsync();
            List<Tache> myTaskList = new List<Tache>();
            List<Tache> projectTaskList = new List<Tache>();
            foreach (Tache task in taskList)
            {
                if (task.ProjetID.Equals(idProjet))
                {
                    projectTaskList.Add(task);
                }
            }
            foreach (Tache task in projectTaskList)
            {
                //if (task.RealisateurID.Equals(myId) || task.CreatorId.Equals(myId))
                    if ( task.CreatorId.Equals(myId))
                    {
                    myTaskList.Add(task);
                }
            }
            return myTaskList;
            // await _planProDbContext.Projets.ToListAsync();
        }

        public async Task<Tache> GetTache(int idTache)
        {
            return await _tasksRepository.GetByIdAsync(idTache);
        }

        public async Task<Tache> AddTache(Tache tacheToSave)
        {
            await _tasksRepository.AddAsync(tacheToSave);
            await _tasksRepository.CommitAsync();
            return tacheToSave;
        }

        public async Task<Tache> UpdateTache(Tache tacheToUpdate)
        {
            Tache updatedTache = _tasksRepository.Update(tacheToUpdate);
            await _tasksRepository.CommitAsync();
            return updatedTache;
        }

        public async Task DelteTache(int idTache)
        {
            Tache tacheToDelete = await GetTache(idTache);
            if (tacheToDelete == null)
            {
                throw new Exception($"Tache with id={idTache} not found");
            }
            _tasksRepository.Remove(tacheToDelete);
            await _tasksRepository.CommitAsync();
        }
    }
}
