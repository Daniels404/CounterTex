﻿using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CounterTexFront.Controllers
{
    public class LoginController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            string returnUrl = Url.Action("Index", "Home");
            Tokens token = new Tokens();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Auth/Login", content);

                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<Tokens>(res);
                    CookieUpdate(model);
                    Session["BearerToken"] = token.TokenValue;

                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void CookieUpdate(LoginViewModel usuario)
        {
            var ticket = new FormsAuthenticationTicket(2,
                usuario.UserName,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                JsonConvert.SerializeObject(usuario)
            );
            Session["Username"] = usuario.UserName;
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { };
            Response.AppendCookie(cookie);
        }
    }
}