using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;
using System;
using System.Threading.Tasks;
using PropertyManagement.API.Helpers;
using PropertyManagement.API.Dtos;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController: ControllerBase
    {
        private readonly IApartmentRepository _repo;

        public ApartmentsController(IApartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetApartments([FromQuery]ApartmentParams apartmentParams)
        {
            var apartments = await _repo.GetApartments(apartmentParams);

            Response.AddPagination(apartments.CurrentPage, apartments.PageSize, apartments.TotalCount, apartments.TotalPages);

            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartment(int id)
        {
            var apartment = await _repo.GetApartment(id);

            return Ok(apartment);
        }

        [HttpPost("createApartment")]
        public async Task<IActionResult> CreateApartment(ApartmentDto apartmentDto)
        {
            if(apartmentDto.Title == null || apartmentDto.NbOfRooms == null || apartmentDto.Address == null) {
                return BadRequest("Missing field");
            }

            var apartmentToCreate = new Apartment();

            var createdApartment = await _repo.CreateApartment(apartmentToCreate, apartmentDto.Title, 
                (int)apartmentDto.NbOfRooms, apartmentDto.Address);
            
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(int id, ApartmentForUpdateDto apartmentForUpdateDto)
        {
            var apartment = await _repo.UpdateApartment(id, apartmentForUpdateDto.Title, 
                apartmentForUpdateDto.NbOfRooms, apartmentForUpdateDto.Address);

            return Ok(apartment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartmentToDelete = await _repo.DeleteApartment(id);

            return Ok(apartmentToDelete);
        }
    }
}