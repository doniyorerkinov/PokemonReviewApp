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

            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<Review, ReviewDto>();

            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
