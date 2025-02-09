﻿using AuctionHouse.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuctionHouse.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="configuration">The configuration.</param>
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model) {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="model">The login model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model) {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password)) {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256));

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Adds a new role.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role) {
            if (!await roleManager.RoleExistsAsync(role)) {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded) {
                    return Ok(new { message = "Role added successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Role already exists");
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="model">The user role model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model) {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null) {
                return BadRequest("User not found");
            }

            var result = await userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded) {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }
    }

}
   

