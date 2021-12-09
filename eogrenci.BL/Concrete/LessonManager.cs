using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ResponseObjects;
using eogrenci.BL.Abstract;
using eogrenci.BL.Extensions;
using eogrenci.Dal.UnitOfWork;
using eogrenci.Dtos.LessonDtos;
using eogrenci.Entities.Concrete;
using FluentValidation;

namespace eogrenci.BL.Concrete
{
    public class LessonManager :  ILessonService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<LessonAddDto> _lessonAddDtoValidator;
        private readonly IValidator<LessonUpdateDto> _lessonUpdateDtoValidator;


        public LessonManager(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<LessonAddDto> lessonAddDtoValidator,
            IValidator<LessonUpdateDto> lessonUpdateDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _lessonAddDtoValidator = lessonAddDtoValidator;
            _lessonUpdateDtoValidator = lessonUpdateDtoValidator;
        }

        public async Task<IResponse<LessonAddDto>> Add(LessonAddDto lessonAddDto)
        {
            //Burada lessonAddDto yu Lesson a çevirmek için mapledik. Ekleme yapmak için Lesson tipinde göndermemiz gerekmektedir.

            //Validate methodu burada bize ValidationResult dönüyor.
            var validationResult = _lessonAddDtoValidator.Validate(lessonAddDto);



            if (validationResult.IsValid)
            {
                await _unitOfWork.GetRepository<Lesson>().Add(_mapper.Map<Lesson>(lessonAddDto));

                await _unitOfWork.SaveChanges();
                return new Response<LessonAddDto>(ResponseType.Success, lessonAddDto);
            }
            else
            {
                return new Response<LessonAddDto>(ResponseType.ValidationError, lessonAddDto, validationResult.ConvertToCustomValidationError());
            }

        }

        public async Task<IResponse<List<LessonListDto>>> GetAll()
        {
            //Burada ise Lesson olarak dönen değeri, LessonListDto ya dönüştürüyoruz. Çünkü api ye lessonListDto şeklinde sadece istediğimiz verileri döneriz.
            var data = _mapper.Map<List<LessonListDto>>(await _unitOfWork.GetRepository<Lesson>().GetAll(x => !x.IsDeleted));

            return new Response<List<LessonListDto>>(ResponseType.Success, data);

        }

        public async Task<IResponse<LessonListDto>> GetById(int id)
        {
            //Burada Lesson tipinde gelen veriyi, LessonListDto tipinde döndük.
            var data = _mapper.Map<LessonListDto>(await _unitOfWork.GetRepository<Lesson>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<LessonListDto>(ResponseType.NotFound, $"{id} ye ait kayıt bulunamadı.");
            }
            return new Response<LessonListDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _unitOfWork.GetRepository<Lesson>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _unitOfWork.GetRepository<Lesson>().Remove(removedEntity);
                await _unitOfWork.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait kayıt bulunamadı.");
            }

        }

        public async Task<IResponse<LessonUpdateDto>> Update(LessonUpdateDto lessonUpdateDto)
        {
            var validationResult = _lessonUpdateDtoValidator.Validate(lessonUpdateDto);

            if (validationResult.IsValid)
            {
                var updatedEntity = await _unitOfWork.GetRepository<Lesson>().GetById(lessonUpdateDto.Id);

                if (updatedEntity != null)
                {
                    _unitOfWork.GetRepository<Lesson>().Update(_mapper.Map<Lesson>(lessonUpdateDto), updatedEntity);
                    await _unitOfWork.SaveChanges();
                    return new Response<LessonUpdateDto>(ResponseType.Success, lessonUpdateDto);
                }

                return new Response<LessonUpdateDto>(ResponseType.NotFound, $"{lessonUpdateDto.Id} ye ait kayıt bulunamadı.");


            }
            else
            {
                return new Response<LessonUpdateDto>(ResponseType.ValidationError, lessonUpdateDto, validationResult.ConvertToCustomValidationError());
            }



        }

    }
}
