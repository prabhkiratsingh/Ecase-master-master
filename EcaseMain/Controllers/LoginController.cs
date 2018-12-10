using EcaseMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EcaseMain.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        ecaseEntities ecaseEntity = null;

        public LoginController()
        {
            ecaseEntity = new ecaseEntities();
        }

        [HttpPost]
        public IHttpActionResult Login(RegistrationDto registrationDto)
        {
            registration loginStatus = null;
            try
            {
                loginStatus = ecaseEntity.registrations.First(x => x.email == registrationDto.email && x.password == registrationDto.password);

                if(loginStatus.status == false)
                {
                    return Ok("Inactive");
                }
                return Ok(loginStatus);
            }
            catch(Exception ex)
            {
                return Ok(loginStatus);
            }
        }

        [HttpPost]
        public IHttpActionResult ForgotPassword(string emailId)
        {
            registration loginStatus = null;
            try
            {
                loginStatus = ecaseEntity.registrations.First(x => x.email == emailId);
                string password = loginStatus.password;

                SendMessageAsync(emailId, password).Wait();
                return Ok("Done");
            }
            catch (Exception ex)
            {
                return Ok(loginStatus);
            }
        }


        [HttpPost]
        public IHttpActionResult AdminLogin(RegistrationDto registrationDto)
        {
            registration loginStatus = null;
            try
            {
                loginStatus = ecaseEntity.registrations.First(x => x.email == registrationDto.email && x.password == registrationDto.password && x.accounttype == "0");
                return Ok(loginStatus);
            }
            catch (Exception ex)
            {
                return Ok(loginStatus);
            }
        }

        private async Task SendMessageAsync(string toEmailId, string password)
        {
            Response response = null;
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress("baljeetcodeapp@gmail.com");
                var to = new EmailAddress(toEmailId);
                string link = "Your current password is "+ password;
                var msg = MailHelper.CreateSingleEmail(from, to,
                    "Reset Password", "Reset Password", link);
                response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var statusCode = response?.StatusCode.ToString() ?? "None";
            }
        }

    }
}
