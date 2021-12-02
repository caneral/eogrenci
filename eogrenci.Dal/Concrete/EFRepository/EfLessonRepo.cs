using System;
using Core.DataAccess;
using eogrenci.Dal.Abstract.EFIRepository;
using eogrenci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace eogrenci.Dal.Concrete.EFRepository
{
    public class EfLessonRepo : EFBaseRepo<Lesson>, ILessonRepo
    {
        public EfLessonRepo(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
