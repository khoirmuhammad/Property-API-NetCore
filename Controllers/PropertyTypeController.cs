using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertyAPI.CustomModels;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.DtoModels;
using PropertyAPI.Models;

namespace PropertyAPI.Controllers
{
    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            
            IEnumerable<PropertyType> propertyTypes = await _unitOfWork.PropertyTypeRepository.GetAsync(cancellationToken);

            if (propertyTypes.Count() < 1)
            {
                apiResponse.Code = NotFound().StatusCode;
                apiResponse.ErrorMessage = "List Data is Empty";

                return NotFound(apiResponse);
            }

            IEnumerable<IdNamePairDto> result = _mapper.Map<IEnumerable<IdNamePairDto>>(propertyTypes);

            return Ok(result);
        }
    }
}