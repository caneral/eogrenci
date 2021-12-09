using System;
using eogrenci.Dtos.DepartmentDtos;
using FluentValidation;

namespace eogrenci.BL.ValidationRules.Department
{
    public class DepartmentAddDtoValidator : AbstractValidator<DepartmentAddDto>
    {
        public DepartmentAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen bölüm adını giriniz.");
        }
    }
}
