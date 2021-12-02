using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.ResponseObjects;
using eogrenci.BL.Abstract;
using eogrenci.BL.Extensions;
using eogrenci.Dal.UnitOfWork;
using eogrenci.Dtos.QuestionDtos;
using eogrenci.Entities.Concrete;
using FluentValidation;

namespace eogrenci.BL.Concrete
{
    public class QuestionManager : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<QuestionAddDto> _questionAddDtoValidator;
        private readonly IValidator<QuestionUpdateDto> _questionUpdateDtoValidator;



        public QuestionManager(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<QuestionAddDto> questionAddDtoValidator,
            IValidator<QuestionUpdateDto> questionUpdateDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _questionAddDtoValidator = questionAddDtoValidator;
            _questionUpdateDtoValidator = questionUpdateDtoValidator;
        }

        public async Task<IResponse<QuestionAddDto>> Add(QuestionAddDto questionAddDto)
        {
            //Burada questionAddDto yu Question a çevirmek için mapledik. Ekleme yapmak için Question tipinde göndermemiz gerekmektedir.

            //Validate methodu burada bize ValidationResult dönüyor.
            var validationResult = _questionAddDtoValidator.Validate(questionAddDto);



            if (validationResult.IsValid)
            {
                await _unitOfWork.GetRepository<Question>().Add(_mapper.Map<Question>(questionAddDto));

                await _unitOfWork.SaveChanges();
                return new Response<QuestionAddDto>(ResponseType.Success, questionAddDto);
            }
            else
            {
               return new Response<QuestionAddDto>(ResponseType.ValidationError, questionAddDto, validationResult.ConvertToCustomValidationError());
            }

        }

        public async Task<IResponse<List<QuestionListDto>>> GetAll()
        {
            //Burada ise Question olarak dönen değeri, QuestionListDto ya dönüştürüyoruz. Çünkü api ye questionListDto şeklinde sadece istediğimiz verileri döneriz.
            var data = _mapper.Map<List<QuestionListDto>>(await _unitOfWork.GetRepository<Question>().GetAll());

            return new Response<List<QuestionListDto>>(ResponseType.Success, data);

        }

        public async Task<IResponse<QuestionListDto>> GetById(int id)
        {
            //Burada Question tipinde gelen veriyi, QuestionListDto tipinde döndük.
            var data = _mapper.Map<QuestionListDto>(await _unitOfWork.GetRepository<Question>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<QuestionListDto>(ResponseType.NotFound, $"{id} ye ait kayıt bulunamadı.");
            }
            return new Response<QuestionListDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _unitOfWork.GetRepository<Question>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _unitOfWork.GetRepository<Question>().Remove(removedEntity);
                await _unitOfWork.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait kayıt bulunamadı.");
            }

        }

        public async Task<IResponse<QuestionUpdateDto>> Update(QuestionUpdateDto questionUpdateDto)
        {
            var validationResult = _questionUpdateDtoValidator.Validate(questionUpdateDto);

            if (validationResult.IsValid)
            {
                var updatedEntity = await _unitOfWork.GetRepository<Question>().GetById(questionUpdateDto.Id);

                if (updatedEntity != null)
                {
                    _unitOfWork.GetRepository<Question>().Update(_mapper.Map<Question>(questionUpdateDto), updatedEntity);
                    await _unitOfWork.SaveChanges();
                    return new Response<QuestionUpdateDto>(ResponseType.Success, questionUpdateDto);
                }

                return new Response<QuestionUpdateDto>(ResponseType.NotFound, $"{questionUpdateDto.Id} ye ait kayıt bulunamadı.");


            }
            else
            {
                return new Response<QuestionUpdateDto>(ResponseType.ValidationError, questionUpdateDto, validationResult.ConvertToCustomValidationError());
            }



        }

    }
}
