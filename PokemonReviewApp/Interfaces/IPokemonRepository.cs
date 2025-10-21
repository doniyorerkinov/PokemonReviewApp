using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        ICollection<Pokemon> SearchPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool ExistPokemon(int id);
        bool CreatePokemon(PokemonDto pokemon);
        bool DeletePokemon(int id);
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool Save();
    }
}
