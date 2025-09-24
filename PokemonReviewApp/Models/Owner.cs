namespace PokemonReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gym { get; set; }
        // 1 to many
        public Country Country { get; set; }
        // Many-to-Many
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
    }
}
