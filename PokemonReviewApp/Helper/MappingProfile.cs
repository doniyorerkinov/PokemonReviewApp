using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Pkemondan PokemonDto ma'lumotlarni ko'chiradi faqatgina mos kelganlarini
            // bu siz database'dagi ortiqcha ma'lumotlarni tashqariga chiqarmasligizga yordam beradi
            CreateMap<Pokemon, PokemonDto>();

            CreateMap<Category, CategoryDto>();

            CreateMap<Country, CountryDto>();

            CreateMap<Owner, OwnerDto>();
        }
    }
}
