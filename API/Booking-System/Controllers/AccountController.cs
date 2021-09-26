using AutoMapper;
using DAL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
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
    
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,
                                    ITokenService tokenService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                if (await this.userManager.FindByNameAsync(registerDto.Username) != null) return BadRequest("Username already exists");

                if (!await this.roleManager.RoleExistsAsync("Member")) return BadRequest("Member role doesn't exist");

                var user = this.mapper.Map<AppUser>(registerDto);

                user.UserName = registerDto.Username.ToLower();

                var result = await this.userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded) return BadRequest(result.Errors);

                var roleResult = await this.userManager.AddToRoleAsync(user, "Member");

                if (!roleResult.Succeeded) return BadRequest(result.Errors);

                return new UserDto
                {
                    Username = user.UserName,
                    Token = await this.tokenService.CreateToken(user)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            var result = await signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid password");

            return new UserDto
            {
                Username = user.UserName,
                Token = await tokenService.CreateToken(user),
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public IEnumerable<AppUser> GetUsers()
        {
            return this.userManager.Users.ToList();
        }
    }
}
