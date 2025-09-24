using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    public class CategoryCantroller : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryCantroller(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Barcha Categoriyalarni olib keladi.
        [HttpGet("GetList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        // Id bo'yicha category olib keladi.
        [HttpGet("Get/{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_categoryRepository.CategoryExist(id))
            {
                return NotFound(new { message = "Category not found with the provided ID." });
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));
            return Ok(category);
        }

        // Categoryni id bo'yicha qidirib bor yo'qligini bilib keladi
        [HttpGet("CheckExist")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult CheckExist(int categoryId)
        {
            var result = _categoryRepository.CategoryExist(categoryId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        // Pokemonni id bo'yicha qidirib bor yo'qligini bilib keladi
        [HttpGet("GetPokemonsByCategoryId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviews(int categoryId)
        {
            var result = _mapper.Map<List<PokemonDto>>(
                _categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }
    }
}
