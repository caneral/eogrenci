using System;
using eogrenci.Dtos.DepartmentDtos;
using eogrenci.Dtos.LessonDtos;
using FluentValidation;

namespace eogrenci.BL.ValidationRules.Department
{
    public class DepartmentUpdateDtoValidator : AbstractValidator<DepartmentUpdateDto>
    {
        public DepartmentUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen bölüm adını giriniz.");
        }
    }
}
