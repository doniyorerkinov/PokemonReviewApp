using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
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
            var pokemon = _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
            pokemon.PokemonOwners = _context.PokemonOwners.Where(po => po.PokemonId == pokemon.Id).ToList();
            pokemon.PokemonCategories = _context.PokemonCategories.Where(po => po.PokemonId == pokemon.Id).ToList();
            return pokemon;
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
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

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

        public bool CreatePokemon(PokemonDto pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == pokemon.OwnerId).FirstOrDefault();
            var pokemonCategoryEntity = _context.Categories.Where(c => c.Id == pokemon.CategoryId).FirstOrDefault();

            Pokemon newPokemon = new Pokemon()
            {
                Name = pokemon.Name,
                BirthDate = pokemon.BirthDate
            };

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = newPokemon
            };
            _context.Add(pokemonOwner);
            var pokemonCategory = new PokemonCategory()
            {
                Category = pokemonCategoryEntity,
                Pokemon = newPokemon
            };

            _context.Add(pokemonCategory);

            _context.Add(newPokemon);
            
            return Save();
        }

        public bool DeletePokemon(int id)
        {
            var pokemon = GetPokemon(id);
            _context.Remove(pokemon);
            return Save();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var pokemonCategoryEntity = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var existingPokemonOwner = _context.PokemonOwners.Where(po => po.Pokemon.Id == pokemon.Id).FirstOrDefault();
            if (existingPokemonOwner != null)
            {
                _context.Remove(existingPokemonOwner); // ✅ Delete old relationship
            }
            var newPokemonOwner = new PokemonOwner()
            {
                PokemonId = pokemon.Id,
                OwnerId = ownerId, // ⚠️ Set IDs directly, not entities (avoids lazy loading issues)
                                  // If you need navigation properties, you can assign them too:
                                  // Pokemon = pokemon,
                                  // Owner = pokemonOwnerEntity
            };
            _context.Add(newPokemonOwner);


            var existingPokemonCategory = _context.PokemonCategories
                .Where(pc => pc.PokemonId == pokemon.Id)
                .FirstOrDefault();

            if (existingPokemonCategory != null)
            {
                _context.Remove(existingPokemonCategory); // ✅ Delete old relationship
            }

            // Always add new relationship (even if none existed)
            var newPokemonCategory = new PokemonCategory()
            {
                PokemonId = pokemon.Id,
                CategoryId = categoryId, // ⚠️ Set IDs directly, not entities (avoids lazy loading issues)
                                         // If you need navigation properties, you can assign them too:
                                         // Pokemon = pokemon,
                                         // Category = pokemonCategoryEntity
            };
            _context.Add(newPokemonCategory);
            _context.Update(pokemon);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
