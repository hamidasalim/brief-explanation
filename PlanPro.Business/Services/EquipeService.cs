using Microsoft.AspNetCore.Identity;
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
    public class EquipeService : IEquipeService
    {
        private IRepository<Equipe> _equipeRepository;
        private PlanProDbContext _planProDbContext;
        private readonly UserManager<ApplicationUser> _userManager;


        public EquipeService(UserManager<ApplicationUser> userManager, PlanProDbContext planProDbContext)
        {
            _planProDbContext = planProDbContext;
            _equipeRepository = new Repository<Equipe>(_planProDbContext);
        
            _userManager = userManager;
        }

        public async Task<List<Equipe>> GetAllEquipes()
        {
            //List<Equipe> equipes = await _equipeRepository.GetAllAsync();
            //foreach (Equipe equipe in equipes)
            //{
            //    foreach (string idMember in equipe.IdMembers)
            //    {
            //        ApplicationUser member = await _userManager.FindByIdAsync(idMember);
            //        if (member != null)
            //            equipe.Members.Add(member);
            //    }
            //}
            //return equipes;
            return await _equipeRepository.GetAllAsync();
        }

        public async Task<Equipe> GetEquipe(int idEquipe)
        {
            Equipe equipe = await _equipeRepository.GetByIdAsync(idEquipe);
            equipe.Members = new List<ApplicationUser>();
            if (equipe.IdMembers == null)
                return equipe;
            foreach (string idMember in equipe.IdMembers)
            {
                ApplicationUser member = await _userManager.FindByIdAsync(idMember);
                if (member != null)
                    equipe.Members.Add(member);
            }
            return equipe;
        }
        public async Task<List<ApplicationUser>>GetEquipeMembers(int idEquipe)
        {
            Equipe equipe = await _equipeRepository.GetByIdAsync(idEquipe);
            equipe.Members = new List<ApplicationUser>();
            if (equipe.IdMembers == null)
                return equipe.Members;
            foreach (string idMember in equipe.IdMembers)
            {
                ApplicationUser member = await _userManager.FindByIdAsync(idMember);
                if (member != null)
                    equipe.Members.Add(member);
            }
            return equipe.Members;
        }

        public async Task<List<Equipe>> GetMyEquipe(string myId)
        {
            List <Equipe> allEquipe = await _equipeRepository.GetAllAsync();
            List<Equipe> myEquipe = new List<Equipe>();

            foreach (Equipe equipe in allEquipe )
            {
                if (equipe.IDChef .Equals(myId))
                    myEquipe.Add(equipe);
            }
            return myEquipe;
        }

        public async Task<Equipe> AddEquipe(Equipe equipeToSave)
        {
            //importaaaaaaant
            //tester if role du idChed # role employee
            //******
            await _equipeRepository.AddAsync(equipeToSave);
            await _equipeRepository.CommitAsync();
            return equipeToSave;
        }

        public async Task<Equipe> UpdateEquipe(Equipe equipeToUpdate)
        {
            Equipe updatedEquipe = _equipeRepository.Update(equipeToUpdate);
            await _equipeRepository.CommitAsync();
            return updatedEquipe;
        }
        //public async Task<Equipe> RemoveMemberEquipe(Equipe equipeToUpdate)
        //{
        //    Equipe updatedEquipe = _equipeRepository.Update(equipeToUpdate);
        //    await _equipeRepository.CommitAsync();
        //    return updatedEquipe;
        //}

        public async Task DelteEquipe(int idEquipe)
        {
            Equipe EquipeToDelete = await GetEquipe(idEquipe);
            if (EquipeToDelete == null)
            {
                throw new Exception($"Equipe with id={idEquipe} not found");
            }
            _equipeRepository.Remove(EquipeToDelete);
            await _equipeRepository.CommitAsync();
        }

        /*public async Task GetTeamMembers (int idEquipe)
        {
            List<ApplicationUser> memberlist = new List<ApplicationUser>();
         
            foreach (ApplicationUser user in users)
            {
                if()
            }

        }*/
   
    }
}


