using OpenEyesBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
namespace LoginWithReacthooks.Controllers
{
    [System.Web.Http.RoutePrefix("api/user")]
    public class UserLoginController : ApiController
    {
        OpenEyesEntities1 DB = new OpenEyesEntities1();
        [System.Web.Http.Route("Login")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult userLogin(Login login)
        {
            var log = DB.UserLogins.Where(x => x.Email.Equals(login.Email) && x.Password.Equals(login.Password)).FirstOrDefault();

            if (log == null)
            {
                return Ok(new { status = 401, isSuccess = false, message = "Invalid User", });
            }
            else

                return Ok(new { status = 200, isSuccess = true, message = "User Login successfully", UserDetails = log });
        }
        [System.Web.Http.Route("InsertUser")]
        [System.Web.Http.HttpPost]
        public object InsertUser(Register Reg)
        {
            try
            {

                UserLogin UL = new UserLogin();
                if (UL.Id == 0)
                {
                    UL.UserName = Reg.UserName;
                    UL.Email = Reg.Email;
                    UL.Password = Reg.Password;
                    DB.UserLogins.Add(UL);
                    DB.SaveChanges();
                    return new Response
                    { Status = "Success", Message = "Record SuccessFully Saved." };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new Response
            { Status = "Error", Message = "Invalid Data." };
        }
    }
}