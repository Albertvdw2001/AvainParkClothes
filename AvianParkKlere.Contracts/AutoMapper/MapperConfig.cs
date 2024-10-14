using AutoMapper;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;

namespace AvianParkKlere.Contracts.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            /* Student */
            CreateMap<StudentPutDto, StudentPutDto>().ReverseMap();

            CreateMap<Student, StudentGetDto>().ReverseMap();
            CreateMap<Student, StudentPostDto>().ReverseMap();
            CreateMap<Student, StudentPutDto>().ReverseMap();

            /* Clothing */
            CreateMap<Clothing, ClothingGetDto>().ReverseMap();
            CreateMap<Clothing, ClothingPostDto>().ReverseMap();    
            CreateMap<Clothing, ClothingPutDto>().ReverseMap(); 
            
            /* StudentClothing */
            CreateMap<StudentClothing, StudentClothingGetDto>().ReverseMap();   
            CreateMap<StudentClothing, StudentClothingPostDto>().ReverseMap();

        }
    }
}
