using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var country = _mapper.Map<List<Country>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        // Id bo'yicha Country olib keladi.
        [HttpGet("Get/{id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_countryRepository.CountryExist(id))
            {
                return NotFound(new { message = "Category not found with the provided ID." });
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));
            return Ok(country);
        }

        [HttpGet("CheckExist")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult CheckExist(int countryId)
        {
            var result = _countryRepository.CountryExist(countryId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetCountryByOwner")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(404)]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var result = _mapper.Map<CountryDto>(
                _countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetOwnersFromACountry")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnersFromACountry(int countryId)
        {
            var result = _mapper.Map<List<OwnerDto>>(
                _countryRepository.GetOwnersFromACountry(countryId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }
    }
}
