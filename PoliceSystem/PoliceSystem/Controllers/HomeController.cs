using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoliceSystem.Models;

namespace PoliceSystem.Controllers
{
    public class HomeController : ApiController
    {
        [Route("api/PoliceSystem/CreateAccount")]
        [HttpGet]
        public bool CreateAccount(Account createAccount)
        {

            if (createAccount != null)
            {
                using (PoliceSystemEntities db = new PoliceSystemEntities())
                {
                    db.Accounts.Add(createAccount);
                    db.SaveChanges();
                    return true;
                }
            }

            else
            {
                return false;
            }
        }


        [Route("api/PoliceSystem/Login")]
        [HttpGet]
        public IHttpActionResult Login(Account login)
        {

            using (PoliceSystemEntities db = new PoliceSystemEntities())
            {
                var userlogin = db.Accounts.FirstOrDefault(a => a.Name == login.Email && a.Pass == login.Pass);

                if (userlogin != null)
                {


                    System.Web.HttpContext.Current.Session["Id"] = userlogin.Id.ToString();
                    System.Web.HttpContext.Current.Session["Email"] = userlogin.Email.ToString();

                    return CheckUser();
                }

                return InternalServerError();
            }
        }



        [Route("api/PoliceSystem/CheckUser")]
        [HttpGet]
        private IHttpActionResult CheckUser()
        {
            if (System.Web.HttpContext.Current.Session["Id"] == null)
            {
                return InternalServerError();
            }

            else
            {
                return Ok("Welcome");
            }

        }


        [Route("api/PoliceSystem/AddCriminal")]
        [HttpGet]
        public bool AddCriminal(Criminal addCriminal)
        {
            using (PoliceSystemEntities db = new PoliceSystemEntities())
            {
                if (addCriminal != null)
                {
                    db.Criminals.Add(addCriminal);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }


        [Route("api/PoliceSystem/RemoveCriminal")]
        [HttpGet]
        public bool RemoveCriminal(Criminal removeCriminal)
        {
            if (removeCriminal != null)
            {
                using (PoliceSystemEntities db = new PoliceSystemEntities())
                {
                    var RemoveCriminalsList = db.Criminals.Where(a => a.Id == removeCriminal.Id);
                    foreach (var DeleteCriminal in RemoveCriminalsList)
                    {
                        db.Criminals.Remove(DeleteCriminal);
                        db.SaveChanges();
                    }
                    return true;

                }

            }
            else
            {
                return false;
            }

        }

        [Route("api/PoliceSystem/SearchCriminal")]
        [HttpGet]
        public List<Criminal> SearchCriminal(Criminal CriminalSearch)
        {
            using (PoliceSystemEntities db = new PoliceSystemEntities())
            {
                if (CriminalSearch != null)
                {
                    var searchcriminal = db.Criminals.Where(a => a.Cnic == CriminalSearch.Cnic || a.Name == CriminalSearch.Name).ToList();
                    return searchcriminal;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}