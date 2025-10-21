using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    // Pastdagi class controller ekanligini aytish uchun
    // [controller] folder nomini oladi
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonInterface, IOwnerRepository ownerRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonInterface;
            _mapper = mapper;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
        }


        // Barcha Pokemonlarni olib keladi.
        [HttpGet("GetList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }


        // Id bo'yicha pokemonni olib keladi.
        [HttpGet("Get/{id}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int id)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_pokemonRepository.ExistPokemon(id))
            {
                return NotFound(new { message = "Pokémon not found with the provided ID." });
            }
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(id));
            return Ok(pokemon);
        }
        // Pokemonni qidirib topib list qaytaradi
        [HttpGet("Search")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemon(string search)
        {
            var pokemons = _pokemonRepository.SearchPokemon(search);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (pokemons == null)
            {
                return NotFound(new { message = "Pokémon not found with the provided ID." });
            }
            return Ok(pokemons);
        }

        // Pokemonni id bo'yicha qidirib bor yo'qligini bilib keladi
        [HttpGet("CheckExist")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult CheckExist(int pokemonId)
        {
            var result = _pokemonRepository.ExistPokemon(pokemonId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        // Pokemonni id bo'yicha qidirib bor yo'qligini bilib keladi
        [HttpGet("GetReviews")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult GetReviews(int pokemonId)
        {
            var result = _pokemonRepository.GetPokemonRating(pokemonId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        // Yangi pokemon yaratadi
        [HttpPost("Create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreatePokemon([FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
            {
                return BadRequest(ModelState);
            }
            var pokemons = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (pokemons != null)
            {
                ModelState.AddModelError("", "Pokémon already exists");
                return StatusCode(422, ModelState);
            }
            if (pokemonCreate.OwnerId == 0 || !_ownerRepository.OwnerExists(pokemonCreate.OwnerId))
            {
                ModelState.AddModelError("", "Owner not found");
                return StatusCode(422, ModelState);
            }
            if (pokemonCreate.CategoryId == 0 || !_categoryRepository.CategoryExist(pokemonCreate.CategoryId))
            {
                ModelState.AddModelError("", "Category not found");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_pokemonRepository.CreatePokemon(pokemonCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("Update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePokemon([FromQuery] int pokemonId, [FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto updatedPokemon)
        {
            if (updatedPokemon == null)
            {
                return BadRequest(ModelState);
            }
            if (pokemonId != updatedPokemon.Id)
            {
                return BadRequest(ModelState);
            }
            if (ownerId == 0 || !_ownerRepository.OwnerExists(ownerId))
            {
                ModelState.AddModelError("", "Owner not found");
                return StatusCode(422, ModelState);
            }
            if (categoryId == 0 || !_categoryRepository.CategoryExist(categoryId))
            {
                ModelState.AddModelError("", "Category not found");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);
            if (!_pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon([FromQuery] int pokemonId)
        {
            var pokemon = _pokemonRepository.GetPokemon(pokemonId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (pokemon == null)
            {
                return NotFound(new { message = "Pokémon not found with the provided ID." });
            }
            if(!_pokemonRepository.DeletePokemon(pokemonId))
                return BadRequest("Something went wrong");
            return Ok();
        }
    }
}
