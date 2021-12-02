using System;
using eogrenci.Dtos.LessonDtos;
using FluentValidation;

namespace eogrenci.BL.ValidationRules.Lesson
{
    public class LessonAddDtoValidator : AbstractValidator<LessonAddDto>
    {
        public LessonAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen ders adını giriniz.");
        }
    }
}
