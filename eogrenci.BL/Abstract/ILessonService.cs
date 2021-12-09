using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.Dtos.LessonDtos;

namespace eogrenci.BL.Abstract
{
    public interface ILessonService
    {
        Task<IResponse<List<LessonListDto>>> GetAll();
        Task<IResponse<LessonAddDto>> Add(LessonAddDto lessonAddDto);
        Task<IResponse<LessonListDto>> GetById(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<LessonUpdateDto>> Update(LessonUpdateDto lessonUpdateDto);

    }
}
