using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.CustomModels;
using PropertyAPI.Data;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.DtoModels.City;
using PropertyAPI.Models;
//using PropertyAPI.Models;

namespace PropertyAPI.Controllers
{
    // [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
             ApiResponse apiResponse = new ApiResponse();
             
            // throw new Exception("hahahah");
            IEnumerable<City> cities = await _unitOfWork.CityReporistory.GetAsync(cancellationToken);

            if (cities.Count() < 1)
            {
                apiResponse.Code = NotFound().StatusCode;
                apiResponse.ErrorMessage = "List Data is Empty";

                return NotFound(apiResponse);
            }

            IEnumerable<CityDto> result = _mapper.Map<IEnumerable<CityDto>>(cities);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CityDto cityDto, CancellationToken cancellationToken)
        {
            City city = _mapper.Map<City>(cityDto);

            _unitOfWork.CityReporistory.PostAsync(city, cancellationToken);
            await _unitOfWork.SaveAsync();

            return StatusCode(201, cityDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();

            City? city = await _unitOfWork.CityReporistory.GetByIdAsync(id, cancellationToken);

            if (city == null)
            {
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.ErrorMessage = "Check your data request. City's Data not found";

                return BadRequest(apiResponse);
            }
                
            _unitOfWork.CityReporistory.Delete(city);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]CityUpdateDto cityDto, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            
            City? city = await _unitOfWork.CityReporistory.GetByIdAsync(id, cancellationToken);

            if (city == null)
            {
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.ErrorMessage = "Check your data request. City's Data not found";

                return BadRequest(apiResponse);
            }

            _mapper.Map(cityDto, city);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}