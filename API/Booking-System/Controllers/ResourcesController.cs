using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Booking_System.Controllers
{
    public class ResourcesController : BaseApiController
    {
        private readonly IResourcesRepository repository;
        private readonly UserManager<AppUser> userManager;

        public ResourcesController(IResourcesRepository repository, UserManager<AppUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }
        [HttpGet]
        public IEnumerable<Resource> getAll()
        {
            return this.repository.getAll();
        }
        [HttpPut("book")]
        public void bookResource(ResourceModel model)
        {
            //var mymodel = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            //mymodel[2] = Convert.ToDateTime(mymodel[2]).ToString();
            this.repository.bookResource(model);

        }

    }
}
