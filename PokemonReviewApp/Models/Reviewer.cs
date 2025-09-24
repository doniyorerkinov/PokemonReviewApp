namespace PokemonReviewApp.Models
{
    public class Reviewer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        // 1 to many
        public ICollection<Review> Reviews { get; set; }
    }
}
