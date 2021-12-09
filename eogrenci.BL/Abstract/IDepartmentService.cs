using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.Dtos.DepartmentDtos;
using eogrenci.Dtos.LessonDtos;

namespace eogrenci.BL.Abstract
{
    public interface IDepartmentService
    {
        Task<IResponse<List<DepartmentListDto>>> GetAll();
        Task<IResponse<List<LessonListDto>>> GetWithLessons(int id);
        Task<IResponse<DepartmentAddDto>> Add(DepartmentAddDto departmentAddDto);
        Task<IResponse<DepartmentListDto>> GetById(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<DepartmentUpdateDto>> Update(DepartmentUpdateDto departmentUpdateDto);

    }
}
