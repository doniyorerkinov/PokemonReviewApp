using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet("GetList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var review = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        // Id bo'yicha Country olib keladi.
        [HttpGet("Get/{id}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int id)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_reviewerRepository.ReviewerExists(id))
            {
                return NotFound(new { message = "Review not found with the provided ID." });
            }
            var reviewer = _mapper.Map<ReviewDto>(_reviewerRepository.GetReviewer(id));
            return Ok(reviewer);
        }

        [HttpGet("CheckExist")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(404)]
        public IActionResult CheckExist(int reviewerId)
        {
            var result = _reviewerRepository.ReviewerExists(reviewerId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }

        [HttpGet("GetReviewsByReviewer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var result = _mapper.Map<List<ReviewDto>>(
                _reviewerRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(result);
        }
    }
}
