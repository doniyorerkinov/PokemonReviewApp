namespace PokemonReviewApp.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }
        // 1 to many
        public ICollection<Owner> Owners { get; set; }
    }
}
