using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;
using PropertyManagement.API.Dtos;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController: ControllerBase
    {
        private readonly IBuyerRepository _repo;

        public BuyersController(IBuyerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuyers()
        {
            var buyers = await _repo.GetBuyers();

            return Ok(buyers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuyerApartments(int id)
        {
            var apartmentsForBuyer = await _repo.GetBuyerApartments(id);

            return Ok(apartmentsForBuyer);
        }

        [HttpPost("createBuyer")]
        public async Task<IActionResult> CreateBuyer(BuyerToCreateDto buyertocreateDto)
        {
            if(await _repo.BuyerFullnameExists(buyertocreateDto.FullName))
            {
                return BadRequest("Buyer's full name already exist, choose another one");
            }
            var buyerToCreate = new Buyer();

            var createdBuyer = await _repo.CreateBuyer(buyerToCreate, buyertocreateDto.FullName, buyertocreateDto.Credit);
            
            return StatusCode(201);
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy(BuyerToBuyDto buyertobuyDto)
        {
            if(!await _repo.BuyerExists(buyertobuyDto.BuyerId))
            {
                return BadRequest("Buyer doesn't exist");
            }

            if(!await _repo.ApartmentExists(buyertobuyDto.ApartmentId))
            {
                return BadRequest("Apartment doesn't exist");
            }

            if(await _repo.VerifyPurchased(buyertobuyDto.ApartmentId))
            {
                return BadRequest("This apartment has already been bought");
            }

            if(!await _repo.VerifyCredit(buyertobuyDto.BuyerId, buyertobuyDto.ApartmentId))
            {
                return BadRequest("Cannot buy, not enough credit");
            }

            _repo.Buy(buyertobuyDto.BuyerId, buyertobuyDto.ApartmentId);
            return StatusCode(201);
        }
    }
}