using System;
using eogrenci.Dtos.QuestionDtos;
using FluentValidation;

namespace eogrenci.BL.ValidationRules.Question
{
    public class QuestionUpdateDtoValidator : AbstractValidator<QuestionUpdateDto>
    {
        public QuestionUpdateDtoValidator()
        {
            //Question Text boş olmamalıdır. Custom bir kural koymak istiyorsak must kullanırız. Koşul koymak istersek When kullanırız.  
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Lütfen soruyu doldurunuz.");    
        }

       
    }
}
