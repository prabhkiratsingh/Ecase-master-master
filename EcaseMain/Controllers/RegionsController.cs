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
    public class RegionsController : ApiController
    {
        ecaseEntities ecaseEntity = null;

        public RegionsController()
        {
            ecaseEntity = new ecaseEntities();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<region> regionList = new List<region>();
            regionList = ecaseEntity.regions.ToList();
            return Ok(regionList);
        }

        [HttpGet]
        public IHttpActionResult GetById(int regionId)
        {
            var region = ecaseEntity.regions.First(x => x.id == regionId);
            return Ok(region);
        }

        [HttpPost]
        public IHttpActionResult Create(RegionsDto regionsDto)
        {
            region region = new region
            {
                name = regionsDto.name,
                createdon = DateTime.Now,
                updatedon = DateTime.Now
            };

            ecaseEntity.regions.Add(region);
            ecaseEntity.SaveChanges();
            return Ok(region);
        }

        [HttpPost]
        public IHttpActionResult Delete(int regionId)
        {
            var region = ecaseEntity.regions.First(x => x.id == regionId);
            ecaseEntity.regions.Remove(region);
            ecaseEntity.SaveChanges();
            return Ok(region);
        }

        [HttpPost]
        public IHttpActionResult Update(RegionsDto regionsDto)
        {
            var region = ecaseEntity.regions.First(x => x.id == regionsDto.id);
            region.name = regionsDto.name;
            region.updatedon = DateTime.Now;
            ecaseEntity.SaveChanges();
            return Ok(region);
        }


    }
}
