using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using OrenairTraining.Models;

namespace OrenairTraining.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] role = new string[] { };
            using (OrenairTrainingEntities _db = new OrenairTrainingEntities())
            {
                try
                {
                    // Получаем пользователя
                    user user = (from u in _db.user
                                 where u.user_name == username
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        role userRole = _db.role.Find(user.role_id);

                        if (userRole != null)
                        {
                            role = new string[] { userRole.role_name };
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            }
            return role;
        }

        public override void CreateRole(string roleName)
        {
            role newRole = new role() { role_name = roleName };
            OrenairTrainingEntities db = new OrenairTrainingEntities();
            db.role.Add(newRole);
            db.SaveChanges();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (OrenairTrainingEntities _db = new OrenairTrainingEntities())
            {
                try
                {
                    // Получаем пользователя
                    user user = (from u in _db.user
                                 where u.user_name == username
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        role userRole = _db.role.Find(user.role_id);

                        //сравниваем
                        if (userRole != null && userRole.role_name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}