using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PropertyAPI.CustomModels;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.DtoModels.User;
using PropertyAPI.Models;

namespace PropertyAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin([FromBody]LoginRequest credential, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            
            if (credential == null) {
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.ErrorMessage = "Check your data request";

                return BadRequest(apiResponse);
            }                
            
            User? user = await _unitOfWork.UserRepository.Authenticate(
                credential.Username, credential.Password, cancellationToken);

            if (user == null)
            {
                apiResponse.Code = Unauthorized().StatusCode;
                apiResponse.ErrorMessage = "Unauthorized! Invalid Username or Password";

                return Unauthorized(apiResponse);
            }

            LoginResponse response = new LoginResponse()
            {
                Username = user.Username,
                Token = GenerateTokenJwt(user)
            };

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostRegister([FromBody]LoginRequest credential, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();

            if (string.IsNullOrEmpty(credential.Username) || string.IsNullOrEmpty(credential.Password))
            {
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.ErrorMessage = "Please input username and password";

                return BadRequest(apiResponse);
            }

            bool isUserExist = await _unitOfWork.UserRepository.CheckUserExist(credential.Username, cancellationToken);

            if (isUserExist)
            {
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.ErrorMessage = "Please try using another username. It might be used by others";

                return BadRequest(apiResponse);  
            }

            _unitOfWork.UserRepository.Register(credential.Username, credential.Password, cancellationToken);
            await _unitOfWork.SaveAsync();

            return StatusCode(201, credential);
        }
        private string GenerateTokenJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSecuritySettings:SecretKey").Value));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var signingCredential = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}