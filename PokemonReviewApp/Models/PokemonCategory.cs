namespace PokemonReviewApp.Models
{
    // Join Tables
    public class PokemonCategory
    {
        public int PokemonId { get; set; }
        public int CategoryId { get; set; }

        public Pokemon Pokemon { get; set; }

        public Category Category { get; set; }
    }
}
