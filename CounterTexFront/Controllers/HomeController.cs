using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CounterTexFront.Models;
using Newtonsoft.Json;
using System.Web.Security;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using CounterTexFront.Services;

namespace CounterTexFront.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(Registro model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string endpoint = "Registro/PostRegistro";
            var response = await Microservices.PostWithoutToken(model, endpoint);
            return RedirectToAction("Welcome", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login", "Home");

            string endpoint = "Auth/Login";
            var token = (Token)await Microservices.PostWithoutToken(model, endpoint);
            SessionHelper.BearerToken = token.token;
            CookieUpdate(model);
            return RedirectToAction("Index", "Home");
        }

        private void CookieUpdate(LoginViewModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(SessionHelper.BearerToken) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var expUnix = jsonToken.Payload["exp"];

                if (expUnix != null)
                {
                    var unixTimestamp = Convert.ToInt64(expUnix);
                    var expDateTime = UnixTimeStampToDateTime(unixTimestamp);

                    var ticket = new FormsAuthenticationTicket(2,
                        usuario.UserName,
                        DateTime.Now,
                        expDateTime,
                        false,
                        JsonConvert.SerializeObject(usuario));

                    SessionHelper.UserName = usuario.UserName;
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
                    {
                        Expires = expDateTime
                    };

                    Response.AppendCookie(cookie);
                }
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1).AddSeconds(unixTimeStamp);
            return dateTime.ToLocalTime();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Session.Remove("BearerToken");
            return RedirectToAction("Welcome", "Home");
        }
    }
}