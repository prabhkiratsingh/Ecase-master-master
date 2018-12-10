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
    public class SpecialitiesController : ApiController
    {
        ecaseEntities ecaseEntity = null;

        public SpecialitiesController()
        {
            ecaseEntity = new ecaseEntities();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<speciality> specialityList = new List<speciality>();
            specialityList = ecaseEntity.specialities.ToList();
            return Ok(specialityList);
        }

        [HttpGet]
        public IHttpActionResult GetById(int specialityId)
        {
            var speciality = ecaseEntity.specialities.First(x => x.id == specialityId);
            return Ok(speciality);
        }

        [HttpGet]
        public IHttpActionResult GetByIds([FromUri]  int[] specialityId)
        {
            var specialitiesList = ecaseEntity.specialities.Where(x => specialityId.Contains(x.id));
            return Ok(specialitiesList);
        }

        [HttpPost]
        public IHttpActionResult Create(SpecialitiesDto specialitiesDto)
        {
            speciality speciality = new speciality
            {
                name = specialitiesDto.name,
                createdon = DateTime.Now,
                updatedon = DateTime.Now
            };

            ecaseEntity.specialities.Add(speciality);
            ecaseEntity.SaveChanges();
            return Ok(speciality);
        }

        [HttpPost]
        public IHttpActionResult Delete(int specialityId)
        {
            var speciality = ecaseEntity.specialities.First(x => x.id == specialityId);
            ecaseEntity.specialities.Remove(speciality);
            ecaseEntity.SaveChanges();
            return Ok(speciality);
        }

        [HttpPost]
        public IHttpActionResult Update(SpecialitiesDto specialitiesDto)
        {
            var speciality = ecaseEntity.specialities.First(x => x.id == specialitiesDto.id);
            speciality.name = specialitiesDto.name;
            speciality.updatedon = DateTime.Now;
            ecaseEntity.SaveChanges();
            return Ok(speciality);
        }


    }
}
