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
    public class FurnitureTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FurnitureTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            
            IEnumerable<FurnitureType> furnitureTypes = await _unitOfWork.FurnitureTypeRepository.GetAsync(cancellationToken);

            if (furnitureTypes.Count() < 1)
            {
                apiResponse.Code = NotFound().StatusCode;
                apiResponse.ErrorMessage = "List Data is Empty";

                return NotFound(apiResponse);
            }

            IEnumerable<IdNamePairDto> result = _mapper.Map<IEnumerable<IdNamePairDto>>(furnitureTypes);

            return Ok(result);
        }
    }
}