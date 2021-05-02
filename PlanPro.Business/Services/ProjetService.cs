using Microsoft.EntityFrameworkCore;
using PlanPro.Business.Interfaces;
using PlanPro.Business.IServices;
using PlanPro.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanPro.Business.Services
{
    public class ProjetService : IProjetService
    {
        private IRepository<Projet> _projetRepository;
        private PlanProDbContext _planProDbContext;

        public ProjetService(PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _projetRepository = new Repository<Projet>(_planProDbContext);
        }

        public async Task<List<Projet>> GetAllProjets()
        {
            return await _projetRepository.GetAllAsync();
            // await _planProDbContext.Projets.ToListAsync();
        }

        public async Task<Projet> GetProject(int idProjet)
        {
            return await _projetRepository.GetByIdAsync(idProjet);
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
            _projetRepository.Remove(projetToDelete);
            await _projetRepository.CommitAsync();
        }
    }
}
