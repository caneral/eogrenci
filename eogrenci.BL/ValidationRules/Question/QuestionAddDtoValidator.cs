﻿using System;
using eogrenci.Dtos.QuestionDtos;
using FluentValidation;

namespace eogrenci.BL.ValidationRules.Question
{
    public class QuestionAddDtoValidator : AbstractValidator<QuestionAddDto>
    {
        public QuestionAddDtoValidator()
        {
            //Question Text boş olmamalıdır. Custom bir kural koymak istiyorsak must kullanırız. Koşul koymak istersek When kullanırız.  
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Lütfen soruyu doldurunuz.");    
        }

       
    }
}
