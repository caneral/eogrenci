using System;
using AutoMapper;
using eogrenci.Dtos.LessonDtos;
using eogrenci.Entities.Concrete;

namespace eogrenci.BL.Mappings.AutoMapper
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonListDto>().ReverseMap();
            CreateMap<Lesson, LessonAddDto>().ReverseMap();
            CreateMap<Lesson, LessonUpdateDto>().ReverseMap();
        }
    }
}
