using AutoMapper;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO;
using BookStoreAPI.Model.DTO.BooksDTO;
using BookStoreAPI.Model.DTO.UserDTO;

namespace BookStoreAPI.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PublisherDTO, Publisher>().ReverseMap();
            CreateMap<PublisherCreateDTO, Publisher>().ReverseMap();
            CreateMap<Publisher, PublisherUpdateDTO>().ReverseMap();

            CreateMap<Books,BookDTO>().ReverseMap();
            CreateMap<Books, BookCreateDTO>().ReverseMap();
            CreateMap<BookUpdateDTO,Books>().ReverseMap();

            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();


            CreateMap<User, RegistrationDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<User,UserUpdateDTO>().ReverseMap();



            

        }
        
        }
}
