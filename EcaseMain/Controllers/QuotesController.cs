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
    public class QuotesController : ApiController
    {
        ecaseEntities ecaseEntity = null;

        public QuotesController()
        {
            ecaseEntity = new ecaseEntities();
        }

       
        [HttpGet]
        public IHttpActionResult GetQuotedCaseList(int clientId)
        {
            registration record = null;
            try
            {
                List<CasesQuotesDto> casesQuotesDtoList = new List<CasesQuotesDto>();

                var caseList = ecaseEntity.cases.Where(x => x.clientid == clientId).ToList();

                foreach(var cas in caseList)
                {
                    CasesQuotesDto casesQuotesDto = new CasesQuotesDto();
                    casesQuotesDto.title = cas.title;
                    casesQuotesDto.description = cas.description;
                    casesQuotesDto.id = cas.id;
                    casesQuotesDto.approvedLaywerId = Convert.ToInt32(cas.approvedlawyerid);
                    if(cas.approvedlawyerid != 0)
                    {
                        var lawrecord = ecaseEntity.registrations.First(x => x.id == cas.approvedlawyerid);
                        casesQuotesDto.LawyerName = lawrecord.firstname + " " + lawrecord.lastname;
                    }
                    casesQuotesDto.specialityName = ecaseEntity.specialities.First(x => x.id == cas.specialityid).name;


                    List<Quoted> quotedList = new List<Quoted>();
                    var QuoteList = ecaseEntity.quotes.Where(x => x.caseid == cas.id).ToList();

                    foreach(var quote  in QuoteList)
                    {
                        Quoted quoted = new Quoted();
                        quoted.id = quote.id;
                        quoted.description = quote.description;
                        quoted.duration = quote.duration;
                        quoted.price = quote.price;
                        quoted.lawyerid = Convert.ToInt32(quote.lawyerid);
                        var lawyer = ecaseEntity.registrations.First(x => x.id == quote.lawyerid);
                        quoted.lawyername = lawyer.firstname + " " + lawyer.lastname;
                        quoted.emailId = lawyer.email;

                        quotedList.Add(quoted);
                    }

                    casesQuotesDto.Quoteds = quotedList;
                    casesQuotesDtoList.Add(casesQuotesDto);
                }

                return Ok(casesQuotesDtoList);

            }
            catch (Exception ex)
            {
                record = null;
                return Ok(record);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(QuotesDto quotesDto)
        {
            quote quote = new quote
            {
                description = quotesDto.description,
                price = quotesDto.price,
                duration = quotesDto.duration,
                lawyerid = quotesDto.lawyerId,
                caseid = quotesDto.caseId
            };

            ecaseEntity.quotes.Add(quote);
            ecaseEntity.SaveChanges();
            return Ok(quote);
        }


    }
}
