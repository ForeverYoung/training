using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrenairTraining.Models;
//using System.Web.Mvc;
//using System.Data;
//using System.Data.Entity;

namespace OrenairTraining.My_Classes
{
    public class MyMembership
    {
        //private OrenairTrainingEntities _db = new OrenairTrainingEntities();

        /// <summary>
        /// Добавляет в хранилище данных нового пользователя со значениями указанного
        ///    свойства и уникальным идентификатором, и возвращает параметр состояния, который
        ///    сообщает об успешном создании пользователя или содержит причину, по которой
        ///    не удалось создать пользователя.
        /// </summary>
        /// <param name="username">Имя для нового пользователя.</param>
        /// <param name="password">Пароль для нового пользователя.</param>
        /// <param name="email">Адрес электронной почты для нового пользователя.</param>
        /// <param name="isApproved">Логическое значение, указывающее одобрен ли вход пользователя в систему.</param>
        /// <param name="providerUserKey">Идентификатор пользователя для пользователя, который должен быть сохранен в хранилище данных участия.</param>
        /// <returns>Объект System.Web.Security.MembershipUser для вновь созданного пользователя.
        ///    Если пользователь не был создан, метод возвращает значение null.</returns>
        public static bool CreateUser(string username, string password, string firstname, string lastname, bool isApproved, object providerUserKey)
        {
            using (OrenairTrainingEntities _db = new OrenairTrainingEntities())
            {
                try
                {
                    _db.user.Add(new user { user_name = username, password = password, surname = lastname, firstname = firstname });
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception) { }                
            }
            return false;
        }

        /// <summary>
        /// Проверяет допустимость предоставленного имени пользователя и пароля.
        /// </summary>
        /// <param name="username">Возвращает имя пользователя, которое следует проверить.</param>
        /// <param name="password">Пароль заданного пользователя.</param>
        /// <returns>Значение true, если предоставленное имя пользователя и пароль допустимы; в противном случае — значение false.</returns>
        public static bool ValidateUser(string username, string password)
        {
            using (OrenairTrainingEntities _db=new OrenairTrainingEntities())
            {
                var user = (from u in _db.user where u.user_name == username select u).FirstOrDefault();
                if (user != null && user.password==password)
                {
                    return true;
                }
            }            
            return false;
        }

    }
}