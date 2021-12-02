using System;
using eogrenci.Dtos.LessonDtos;
using FluentValidation;

namespace eogrenci.BL.ValidationRules.Lesson
{
    public class LessonUpdateDtoValidator : AbstractValidator<LessonUpdateDto>
    {
        public LessonUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen ders adını giriniz.");
        }
    }
}
