using AutoMapper;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO;

namespace BookStoreAPI.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AuthorDTO, Publisher>().ReverseMap();
            CreateMap<AuthorCreateDTO, Publisher>().ReverseMap();
           
           ;
            CreateMap<Publisher, AuthorUpdateDTO>().ReverseMap();

        }
        
        }
}
