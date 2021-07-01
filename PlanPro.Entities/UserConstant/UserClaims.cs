using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PlanPro.Entities.UserConstant
{

    public class Policies
    {
        
        public const string VIEW_TASKS = "Lire les données de taches";
        public const string STATUS_TASK = "Mise à jour de l'état d'avancement";
        public const string VIEW_PROJECT = "Lire les données du projet en tant que participant";
        public const string CREATE_TASK = "Créer une tâche";
        public const string MANAGE_TASK = "Gestion d'une tache";
        public const string DELETE_TASK = "Supprimer une tache";
        public const string AFFECT_TASK = "Affecter une tache à un employé realisateur";
        public const string MANAGE_PROJECT = "Gestion du projet";
        public const string MANAGE_EQUIPE = "Gestion d'équipe";
        public const string VIEW_EQUIPE = "Voir une équipe";
        public const string CREATE_EQUIPE = "créer une équipe";
        public const string DELETE_EQUIPE = "supprimer une équipe";
        public const string MANAGE_PROJECT_CHEF_EQUIPE = "Gérer les chefs d'équipes dans un projet";
        public const string MANAGE_CHEF_EQUIPE = "Gestion des roles des chefs d'équipes";
        public const string CREATE_PROJECT = "Créer un projet";
        public const string DELETE_PROJECT = "Supprimer un projet";
        public const string MANAGE_USERS = "Gestion des Utilisateur(ADMIN ONLY)";
        public const string VIEW_ALL_TEAMS = "View all teams(ADMIN ONLY)";
        public const string VIEW_ALL_PROJECTS = "View all PROJECTS(ADMIN ONLY)";
        public const string VIEW_ALL_TASKS = "View all TASKS(ADMIN ONLY)";
    }

    public static class UserClaims
    {
        /********* Role Employee**********/
        public static List<Claim> EmployeClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, Policies.VIEW_TASKS),
                    new Claim(ClaimTypes.Actor, Policies.STATUS_TASK),
                    //Project claims
                    new Claim(ClaimTypes.Actor, Policies.VIEW_PROJECT),
                };
        }

        /********* Role Chef_Equipe**********/
        public static List<Claim> ChefEquipeClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                     new Claim(ClaimTypes.Actor, Policies.VIEW_TASKS),
                   new Claim(ClaimTypes.Actor, Policies.STATUS_TASK),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.AFFECT_TASK),
                    //Project claims
                      new Claim(ClaimTypes.Actor, Policies.VIEW_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_PROJECT),
                    //Equipe Claims
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.VIEW_EQUIPE),
                };
        }
        /********* Role Chef_Projet**********/
        public static List<Claim> ChefProjetClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, Policies.VIEW_TASKS),
                    new Claim(ClaimTypes.Actor, Policies.STATUS_TASK),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.AFFECT_TASK),
                    //Project claims
                    new Claim(ClaimTypes.Actor, Policies.VIEW_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_PROJECT_CHEF_EQUIPE),
                    //USER
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_CHEF_EQUIPE),
                    //Equipe Claims
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.VIEW_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_EQUIPE),
                };
        }
        /********* Role Admin**********/
        public static List<Claim> AdminClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, Policies.VIEW_TASKS),
                    new Claim(ClaimTypes.Actor, Policies.STATUS_TASK),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_TASK),
                    new Claim(ClaimTypes.Actor, Policies.AFFECT_TASK),
                    //Project claims
                    new Claim(ClaimTypes.Actor, Policies.VIEW_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_PROJECT),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_PROJECT_CHEF_EQUIPE),

                    //USER 
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_USERS),
                    new Claim(ClaimTypes.Actor, Policies.VIEW_ALL_TEAMS),
                    new Claim(ClaimTypes.Actor, Policies.VIEW_ALL_PROJECTS),
                    new Claim(ClaimTypes.Actor, Policies.VIEW_ALL_TASKS),
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_CHEF_EQUIPE),
                    //Equipe Claims
                    new Claim(ClaimTypes.Actor, Policies.MANAGE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.VIEW_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.CREATE_EQUIPE),
                    new Claim(ClaimTypes.Actor, Policies.DELETE_EQUIPE),
                 };
        }
    }
}
