using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }
        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> SearchPokemon(string name)
        {
            // p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) string to'liq mos kelmasa ham, uni ichida harf mos bo'lsa ham olib keladi
            return _context.Pokemons.Where(p => p.Name.Contains(name)).ToList();
        }

        public bool ExistPokemon(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }


        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id != pokeId);

            if(reviews.Count() <= 0)
            {
                return 0;
            }
            return Math.Round(((decimal)reviews.Sum(r => r.Rating) / reviews.Count()), 2);
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }
    }
}
