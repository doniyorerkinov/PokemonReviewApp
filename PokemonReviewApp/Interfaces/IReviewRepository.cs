using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);

        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewExists(int reviewId);
    }
}
