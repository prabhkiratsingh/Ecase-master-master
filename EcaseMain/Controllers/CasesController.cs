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
    public class CasesController : ApiController
    {
        ecaseEntities ecaseEntity = null;

        public CasesController()
        {
            ecaseEntity = new ecaseEntities();
        }


        [HttpPost]
        public IHttpActionResult Create(CasesDto casesDto)
        {
            @case caseObj = new @case
            {
                title = casesDto.title,
                description = casesDto.description,
                clientid = casesDto.clientid,
                specialityid = casesDto.specialityid,
                approvedlawyerid = casesDto.approvedlawyerid
            };

            ecaseEntity.cases.Add(caseObj);
            ecaseEntity.SaveChanges();
            return Ok(caseObj);
        }


        [HttpPost]
        public IHttpActionResult HireLawyer(int caseId, int lawyerId, string emailId)
        {
            var caseRecord = ecaseEntity.cases.First(x => x.id == caseId);
            caseRecord.approvedlawyerid = lawyerId;
            ecaseEntity.SaveChanges();

            var caseTitle = caseRecord.title;

            var clientRecord = ecaseEntity.registrations.First(x => x.id == caseRecord.clientid);
            var clientName = clientRecord.firstname + " " + clientRecord.lastname;

            SendMessageAsync(emailId,caseTitle, clientName).Wait();
            return Ok(caseRecord);
        }

        private async Task SendMessageAsync(string toEmailId, string caseTitle, string clientName)
        {
            Response response = null;
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress("baljeetcodeapp@gmail.com");
                var to = new EmailAddress(toEmailId);
                var msg = MailHelper.CreateSingleEmail(from, to,
                    "Hired", "Hired", "You are hired by "+ clientName +
                    "on the case : "+ caseTitle);
                response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var statusCode = response?.StatusCode.ToString() ?? "None";
            }
        }


    }
}
