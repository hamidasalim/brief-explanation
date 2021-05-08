

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
        private IRepository<Tache> _projetRepository;
        private PlanProDbContext _planProDbContext;

        public TacheService(PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _projetRepository = new Repository<Tache>(_planProDbContext);
        }

        public async Task<List<Tache>> GetAllTaches()
        {
            return await _projetRepository.GetAllAsync();
            // await _planProDbContext.Projets.ToListAsync();
        }

        public async Task<Tache> GetTache(int idTache)
        {
            return await _projetRepository.GetByIdAsync(idTache);
        }

        public async Task<Tache> AddTache(Tache tacheToSave)
        {
            await _projetRepository.AddAsync(tacheToSave);
            await _projetRepository.CommitAsync();
            return tacheToSave;
        }

        public async Task<Tache> UpdateTache(Tache tacheToUpdate)
        {
            Tache updatedTache = _projetRepository.Update(tacheToUpdate);
            await _projetRepository.CommitAsync();
            return updatedTache;
        }

        public async Task DelteTache(int idTache)
        {
            Tache tacheToDelete = await GetTache(idTache);
            if (tacheToDelete == null)
            {
                throw new Exception($"Tache with id={idTache} not found");
            }
            _projetRepository.Remove(tacheToDelete);
            await _projetRepository.CommitAsync();
        }
    }
}
