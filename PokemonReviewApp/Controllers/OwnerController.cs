using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet("GetList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owner = _mapper.Map<List<Owner>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        // Id bo'yicha Country olib keladi.
        [HttpGet("Get/{id}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_ownerRepository.OwnerExists(id))
            {
                return NotFound(new { message = "Owner not found with the provided ID." });
            }
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));
            return Ok(owner);
        }

        [HttpGet("CheckExist")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult CheckExist(int ownerId)
        {
            var result = _ownerRepository.OwnerExists(ownerId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetPokemonByOwner")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            var result = _mapper.Map<List<PokemonDto>>(
                _ownerRepository.GetPokemonByOwner(ownerId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetOwnersFromAPokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnersFromACountry(int pokemonId)
        {
            var result = _mapper.Map<List<OwnerDto>>(
                _ownerRepository.GetOwnersOfAPokemon(pokemonId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }
    }
}
