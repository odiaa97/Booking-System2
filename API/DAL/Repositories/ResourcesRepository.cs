using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ResourcesRepository : IResourcesRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;

        public ResourcesRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IEnumerable<Resource> getAll()
        {
            return this.context.Resources.ToList();
        }

        public void bookResource(ResourceModel model)
        {
            try
            {
                var resource = this.context.Resources.FirstOrDefault(m => m.Id == model.resourceId);
                var user = this.userManager.Users.FirstOrDefault(m => m.Id == model.userId);
                resource.Available = model.available;
                resource.AppUser = user;
                context.Update(resource);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
