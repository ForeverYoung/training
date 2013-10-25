using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(OrenairTraining.Models.LoginOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(My_Classes.MyMembership.ValidateUser(model.UserName,model.Password)) //if (Membership.ValidateUser(model.user_name, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    using (OrenairTrainingEntities _db = new OrenairTrainingEntities()) {
                        _db.log.Add(new log
                        {
                            datetime = DateTime.Now,
                            user_id = _db.user.FirstOrDefault(u => u.user_name == model.UserName).user_id,
                            ip = HttpContext.Request.UserHostAddress,
                            operation_code = "login",
                            objectcode_id = null, object_id = null
                        });
                        _db.SaveChanges();
                        //!!!!!!!!!!!!!!!!!!!
                    }                    
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);                        
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(OrenairTraining.Models.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //MembershipCreateStatus createStatus;
                //Membership.CreateUser(model.user_name, model.Password, model.Name, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
                bool success = My_Classes.MyMembership.CreateUser(model.UserName, model.Password, model.FirstName, model.LastName, isApproved: true, providerUserKey: null);
                if (success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации. Возможно такой пользователь уже существует");
                }
            }

            return View(model);
        }
    }
}