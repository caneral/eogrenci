using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ResponseObjects;
using eogrenci.BL.Abstract;
using eogrenci.BL.Extensions;
using eogrenci.Dal.UnitOfWork;
using eogrenci.Dtos.DepartmentDtos;
using eogrenci.Dtos.LessonDtos;
using eogrenci.Entities.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace eogrenci.BL.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DepartmentAddDto> _departmentAddDtoValidator;
        private readonly IValidator<DepartmentUpdateDto> _departmentUpdateDtoValidator;


        public DepartmentManager(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<DepartmentAddDto> departmentAddDtoValidator,
            IValidator<DepartmentUpdateDto> departmentUpdateDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _departmentAddDtoValidator = departmentAddDtoValidator;
            _departmentUpdateDtoValidator = departmentUpdateDtoValidator;
        }

        public async Task<IResponse<DepartmentAddDto>> Add(DepartmentAddDto departmentAddDto)
        {
            //Burada departmentAddDto yu Department a çevirmek için mapledik. Ekleme yapmak için Department tipinde göndermemiz gerekmektedir.

            //Validate methodu burada bize ValidationResult dönüyor.
            var validationResult = _departmentAddDtoValidator.Validate(departmentAddDto);



            if (validationResult.IsValid)
            {
                await _unitOfWork.GetRepository<Department>().Add(_mapper.Map<Department>(departmentAddDto));

                await _unitOfWork.SaveChanges();
                return new Response<DepartmentAddDto>(ResponseType.Success, departmentAddDto);
            }
            else
            {
                return new Response<DepartmentAddDto>(ResponseType.ValidationError, departmentAddDto, validationResult.ConvertToCustomValidationError());
            }

        }

        public async Task<IResponse<List<DepartmentListDto>>> GetAll()
        {
            //Burada ise Department olarak dönen değeri, DepartmentListDto ya dönüştürüyoruz. Çünkü api ye departmentListDto şeklinde sadece istediğimiz verileri döneriz.
            var data = _mapper.Map<List<DepartmentListDto>>(await _unitOfWork.GetRepository<Department>().GetAll(x => !x.IsDeleted));

            return new Response<List<DepartmentListDto>>(ResponseType.Success, data);

        }

        public async Task<IResponse<DepartmentListDto>> GetById(int id)
        {
            //Burada Department tipinde gelen veriyi, DepartmentListDto tipinde döndük.
            var data = _mapper.Map<DepartmentListDto>(await _unitOfWork.GetRepository<Department>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<DepartmentListDto>(ResponseType.NotFound, $"{id} ye ait kayıt bulunamadı.");
            }
            return new Response<DepartmentListDto>(ResponseType.Success, data);
        }

        public async Task<IResponse<List<LessonListDto>>> GetWithLessons(int id)
        {
            var data = _mapper.Map<List<LessonListDto>>(await _unitOfWork.GetRepository<Lesson>().
                GetListQueryable(x => !x.IsDeleted && x.DepartmentId == id, x => x.Department)
                .Select(p => new LessonListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                })
                .ToListAsync()

                );
            //Bu kod da çalışıyor ancak, IQueryable olmadığından sadece istediğimiz alanları almıyoruz hepsini dönüyor.
            //var data = _mapper.Map<List<LessonListDto>>(await _unitOfWork.GetRepository<Lesson>().GetAll(x => !x.IsDeleted && x.DepartmentId == id, false, x => x.Department));
            return new Response<List<LessonListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _unitOfWork.GetRepository<Department>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _unitOfWork.GetRepository<Department>().Remove(removedEntity);
                await _unitOfWork.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait kayıt bulunamadı.");
            }

        }

        public async Task<IResponse<DepartmentUpdateDto>> Update(DepartmentUpdateDto departmentUpdateDto)
        {
            var validationResult = _departmentUpdateDtoValidator.Validate(departmentUpdateDto);

            if (validationResult.IsValid)
            {
                var updatedEntity = await _unitOfWork.GetRepository<Department>().GetById(departmentUpdateDto.Id);

                if (updatedEntity != null)
                {
                    _unitOfWork.GetRepository<Department>().Update(_mapper.Map<Department>(departmentUpdateDto), updatedEntity);
                    await _unitOfWork.SaveChanges();
                    return new Response<DepartmentUpdateDto>(ResponseType.Success, departmentUpdateDto);
                }

                return new Response<DepartmentUpdateDto>(ResponseType.NotFound, $"{departmentUpdateDto.Id} ye ait kayıt bulunamadı.");


            }
            else
            {
                return new Response<DepartmentUpdateDto>(ResponseType.ValidationError, departmentUpdateDto, validationResult.ConvertToCustomValidationError());
            }



        }

    }
}
