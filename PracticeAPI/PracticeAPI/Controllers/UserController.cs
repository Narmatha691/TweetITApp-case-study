﻿using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.DTO;
using PracticeAPI.Entities;
using PracticeAPI.Model;
using PracticeAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        //private readonly ILog _logger;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            this.userService = userService;
            this._mapper = mapper;
            this.configuration = configuration;
            //this._logger = logger;
        }


        [HttpPost, Route("Register")]
        //[AllowAnonymous]
        public IActionResult AddUser(UserDTO userdto)
        {
            try
            {
                User user = _mapper.Map<User>(userdto);
                var result=userService.AddUser(user);
                if (result.Success)
                {
                    //_logger.Info("User added successfully");
                    return StatusCode(200, user);
                }
                else
                {
                    //_logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }


        }
        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = "1")]
        //
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                //List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        //
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.UserName, login.Password);
                AuthResponse authReponse = new AuthResponse();
                if (user != null)
                {
                    authReponse.UserId = user.UserId;
                    authReponse.IsAdmin = user.IsAdmin;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, user.IsAdmin.ToString()),
                        new Claim(ClaimTypes.Email,user.UserEmail),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
