using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.Dtos.LessonDtos;

namespace eogrenci.BL.Abstract
{
    public interface ILessonService
    {
        Task<IResponse<List<LessonListDto>>> GetAll();
        Task<IResponse<LessonListDto>> Add(LessonUpdateDto lessonAddDto);
        Task<IResponse<LessonListDto>> GetById(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<LessonListDto>> Update(LessonUpdateDto lessonUpdateDto);

    }
}
