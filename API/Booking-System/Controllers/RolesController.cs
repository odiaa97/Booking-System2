using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseApiController
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<AppRole>> CreateRole(AppRole role)
        {
            try
            {
                var existingRole = this.roleManager.Roles.FirstOrDefault(model => model.Name == role.Name);
                if (existingRole == null)
                {
                    role.NormalizedName = role.Name.ToUpper();
                    await this.roleManager.CreateAsync(role);
                }
                else
                {
                    return BadRequest($"{existingRole.Name} Role already exists");
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppRole>> GetRoles()
        {
            return this.roleManager.Roles.ToList();
        }

        [HttpPut("Assign")]
        public async Task<ActionResult<AppUserRole>> Assign(UserRoleModel userRole)
        {

            var user = this.userManager.Users.FirstOrDefault(m => m.Id == userRole.UserId);
            if (user == null)
            {
                return BadRequest($"User with Id: {userRole.UserId} doesn't exist");
            }
            else
            {
                var currentRoles = await userManager.GetRolesAsync(user);
                if (currentRoles.All(userRole.Roles.Contains))
                {
                    return BadRequest($"User '{user.UserName}' already has these roles.");
                }
                await userManager.RemoveFromRolesAsync(user, currentRoles);
                await this.userManager.AddToRolesAsync(user, userRole.Roles);
                return Ok($"Roles set to {user.UserName}");
            }
        }

        [HttpDelete("delete")]
        public ActionResult<AppRole> DeleteEvent(AppRole role)
        {
            var existingRole = this.roleManager.Roles.SingleOrDefault(model => model.Name == role.Name);
            if (existingRole != null)
            {
                this.roleManager.DeleteAsync(role);
            }
            else
            {
                return BadRequest("Role doesn't exists");
            }
            return Ok(role);
        }
    }
}
