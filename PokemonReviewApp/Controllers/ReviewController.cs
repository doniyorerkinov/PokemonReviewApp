using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet("GetList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var review = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        // Id bo'yicha Country olib keladi.
        [HttpGet("Get/{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int id)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_reviewRepository.ReviewExists(id))
            {
                return NotFound(new { message = "Review not found with the provided ID." });
            }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));
            return Ok(review);
        }

        [HttpGet("CheckExist")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult CheckExist(int reviewId)
        {
            var result = _reviewRepository.ReviewExists(reviewId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetReviewsOfAPokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsOfAPokemon(int pokemonId)
        {
            var result = _mapper.Map<List<ReviewDto>>(
                _reviewRepository.GetReviewsOfAPokemon(pokemonId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetReviewsByReviewer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var result = _mapper.Map<List<ReviewDto>>(
                _reviewRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }
    }
}
