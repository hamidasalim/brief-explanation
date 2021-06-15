﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Projet>> GetMyProjects(int myId)
        {
            List<Projet> projectList = await _projetRepository.GetAllAsync();
            Boolean yes = false;
            List<Projet> myProjectList = null;
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

        /*public  Task<List<ApplicationUser>> GetProjectUsers(Projet project)
        {
            List<ApplicationUser> userList = null;
            foreach (ApplicationUser user in project.Participants)
            {
                if (user != null)
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public Task<Projet> AddTeamToProject(Equipe team, Projet project)
        {
            foreach(ApplicationUser teamMember in team.Members)
            {
                if ( teamMember != null)
                {
                    project.Participants.Add(teamMember);
                    if(teamMember.Id != null)
                    {
                        project.IdParticipants.Add(teamMember.Id);
                    }
                }
            }
            return project;
        }

        public Task<Projet> AddUserToProject(ApplicationUser user, Projet project)
        {
            
                if (user != null)
                {
                    project.Participants.Add(user);
                    if (user.Id != null)
                    {
                        project.IdParticipants.Add(user.Id);
                    }
                }
            
            return project;
        }*/
    }
}
