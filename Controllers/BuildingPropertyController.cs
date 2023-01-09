using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PropertyAPI.CustomModels;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.DtoModels.BuildingProperty;
using PropertyAPI.Models;

namespace PropertyAPI.Controllers
{
    public class BuildingPropertyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BuildingPropertyController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int sellRent, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();

            IEnumerable<BuildingProperty> properties = await _unitOfWork.BuildingPropertyRepository.GetAsync(cancellationToken);

            if (sellRent != 0)
                properties = properties.Where(w => w.SellRent.Equals(sellRent)).ToList();

            if (properties.Count() < 1)
            {
                apiResponse.Code = NotFound().StatusCode;
                apiResponse.ErrorMessage = "List Data is Empty";

                return NotFound(apiResponse);
            }

            IEnumerable<BuildingPropertyDto> result = _mapper.Map<IEnumerable<BuildingPropertyDto>>(properties);

            return Ok(result);            
            
        }
    
        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();

            var property = await _unitOfWork.BuildingPropertyRepository.GetByIdAsync(id, cancellationToken);

            if (property == null)
            {
                apiResponse.Code = NotFound().StatusCode;
                apiResponse.ErrorMessage = $"Data with id {id.ToString()} was not exist";

                return NotFound(apiResponse);
            }

            BuildingPropertyDto result = _mapper.Map<BuildingPropertyDto>(property);

            return Ok(result);
        }
    }
}