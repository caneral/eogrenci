using System;
using AutoMapper;
using eogrenci.Dtos.QuestionDtos;
using eogrenci.Entities.Concrete;

namespace eogrenci.BL.Mappings.AutoMapper
{
    public class QuesitonProfile : Profile
    {
        public QuesitonProfile()
        {
            CreateMap<Question, QuestionListDto>().ReverseMap();
            CreateMap<Question, QuestionAddDto>().ReverseMap();
            CreateMap<Question, QuestionUpdateDto>().ReverseMap();
        }
    }
}
