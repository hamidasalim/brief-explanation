using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PlanPro.Security.UserConstant
{
    public static class UserClaims
    {
        /********* Role Employee**********/
        public static List<Claim> EmployeClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, "VIEW_TASKS"),
                    new Claim(ClaimTypes.Actor, "STATUS_TASK"),
                    //Project claims
                    new Claim(ClaimTypes.Actor, "VIEW_PROJECT"),
                };
        }

        /********* Role Chef_Equipe**********/
        public static List<Claim> ChefEquipeClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, "VIEW_TASKS"),
                    new Claim(ClaimTypes.Actor, "STATUS_TASK"),
                    new Claim(ClaimTypes.Actor, "CREATE_TASK"),
                    new Claim(ClaimTypes.Actor, "MANAGE_TASK"),
                    new Claim(ClaimTypes.Actor, "DELETE_TASK"),
                    new Claim(ClaimTypes.Actor, "AFFECT_TASK"),
                    //Project claims
                    new Claim(ClaimTypes.Actor, "VIEW_PROJECT"),
                    new Claim(ClaimTypes.Actor, "MANAGE_PROJECT"),
                    //Equipe Claims
                    new Claim(ClaimTypes.Actor, "MANAGE_EQUIPE"),
                    new Claim(ClaimTypes.Actor, "vIEW_EQUIPE"),
                };
        }
        /********* Role Chef_Projet**********/
        public static List<Claim> ChefProjetClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, "VIEW_TASKS"),
                    new Claim(ClaimTypes.Actor, "STATUS_TASK"),
                    new Claim(ClaimTypes.Actor, "CREATE_TASK"),
                    new Claim(ClaimTypes.Actor, "MANAGE_TASK"),
                    new Claim(ClaimTypes.Actor, "DELETE_TASK"),
                    new Claim(ClaimTypes.Actor, "AFFECT_TASK"),
                    //Project claims
                    new Claim(ClaimTypes.Actor, "VIEW_PROJECT"),
                    new Claim(ClaimTypes.Actor, "MANAGE_PROJECT"),
                    new Claim(ClaimTypes.Actor, "MANAGE_PROJECT_CHEF_EQUIPE"),
                    new Claim(ClaimTypes.Actor, "MANAGE_CHEF_EQUIPE"),
                    //Equipe Claims
                    new Claim(ClaimTypes.Actor, "MANAGE_EQUIPE"),
                    new Claim(ClaimTypes.Actor, "vIEW_EQUIPE"),
                };
        }
        /********* Role Admin**********/
        public static List<Claim> AdminClaims()
        {
            return new List<Claim>
                {
                    //Task claims
                    new Claim(ClaimTypes.Actor, "VIEW_TASKS"),
                    new Claim(ClaimTypes.Actor, "STATUS_TASK"),
                    new Claim(ClaimTypes.Actor, "CREATE_TASK"),
                    new Claim(ClaimTypes.Actor, "MANAGE_TASK"),
                    new Claim(ClaimTypes.Actor, "DELETE_TASK"),
                    new Claim(ClaimTypes.Actor, "AFFECT_TASK"),
                    //Project claims
                    new Claim(ClaimTypes.Actor, "VIEW_PROJECT"),
                    new Claim(ClaimTypes.Actor, "MANAGE_PROJECT"),
                    new Claim(ClaimTypes.Actor, "MANAGE_CHEF_EQUIPE"),
                    //Equipe Claims
                    new Claim(ClaimTypes.Actor, "VIEW_USERS"),
                    new Claim(ClaimTypes.Actor, "MANAGE_USERS"),
                 };
        }
    }
}
