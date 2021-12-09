using System;
using AutoMapper;
using eogrenci.BL.Abstract;
using eogrenci.BL.Concrete;
using eogrenci.BL.Mappings.AutoMapper;
using eogrenci.BL.ValidationRules.Department;
using eogrenci.BL.ValidationRules.Lesson;
using eogrenci.BL.ValidationRules.Question;
using eogrenci.Dal.Concrete.Context;
using eogrenci.Dal.UnitOfWork;
using eogrenci.Dtos.DepartmentDtos;
using eogrenci.Dtos.LessonDtos;
using eogrenci.Dtos.QuestionDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eogrenci.BL.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        //Burada IServiceCollection methoduna extension yapmış olduk.
        public static void AddDependencies(this IServiceCollection services)
        {

            services.AddDbContext<StudentDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySQL("Server=localhost; Port=3306; Database=EStudent;User=root;Password=123456789; ConvertZeroDateTime=True;");
                //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new QuesitonProfile());
                opt.AddProfile(new LessonProfile());
                opt.AddProfile(new DepartmentProfile());

            });
            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<ILessonService, LessonManager>();
            services.AddScoped<IDepartmentService, DepartmentManager>();
            

            services.AddTransient<IValidator<QuestionAddDto>, QuestionAddDtoValidator>();
            services.AddTransient<IValidator<QuestionUpdateDto>, QuestionUpdateDtoValidator>();

            services.AddTransient<IValidator<LessonAddDto>,LessonAddDtoValidator>();
            services.AddTransient<IValidator<LessonUpdateDto>,LessonUpdateDtoValidator>();

            services.AddTransient<IValidator<DepartmentAddDto>, DepartmentAddDtoValidator>();
            services.AddTransient<IValidator<DepartmentUpdateDto>, DepartmentUpdateDtoValidator>();

        }
    }
}
