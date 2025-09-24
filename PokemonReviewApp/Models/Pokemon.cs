namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        // 1 to many
        public ICollection<Review> Reviews { get; set; }
        // Many-to-Many
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        // Many-to-Many
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
