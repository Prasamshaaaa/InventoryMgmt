﻿using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        string role;

        private readonly IConfiguration _configuration;
        private readonly IValidation _validation;
        public SecurityController(
            IConfiguration configuration, 
            IValidation validation)
        {
            _configuration = configuration;
            _validation = validation;
        }

        ApplicationDbContext db = new ApplicationDbContext();

        // GET api/<SecurityController>/5  for user registration
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterUserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is required to Login!");
            }
            UserModel u= new UserModel();
            u.username = user.username;
            u.fullName= user.fullName;
            u.password= user.password;
            u.role = user.role;
            u.isActive = true;

            string uname = user.username;
            string fullName = user.fullName;
            string pwd= user.password;
            string role = user.role;
            var userFromServer = db.users
                                   .Where(u => u.username == uname).FirstOrDefault();
            if (userFromServer is null)
            {
                string error =_validation.RegisterUserValidation(user);
                if (error.Length is not 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, error);
                }
                    //call entity framework to save the data
                    db.users.Add(u);
                    db.SaveChanges();
                    return Ok("User Added Successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Username already exist. Use another username");
            }

        }
        // POST api/<SecurityController> for user Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is required to Login!");
            }
            var userFromServer = db.users
                                   .Where(u => u.username == user.username && u.password==user.password).FirstOrDefault();

            //UserModel u = (from temp in db.users where temp.username == uname && temp.password == pwd select temp).FirstOrDefault();
            if (userFromServer is null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Wrong Credentials, Please try again");
            }
            role = userFromServer.role;
            JwtToken token = new JwtToken(_configuration);
            String t= token.GeneratedToken(userFromServer.username, role);
            
            
            return Ok(t);

        }
    }
}
