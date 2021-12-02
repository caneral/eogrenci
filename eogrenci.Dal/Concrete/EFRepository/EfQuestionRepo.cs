using System;
using Core.DataAccess;
using eogrenci.Dal.Abstract.EFIRepository;
using eogrenci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace eogrenci.Dal.Concrete.EFRepository
{
    public class EfQuestionRepo : EFBaseRepo<Question>, IQuestionRepo
    {
        public EfQuestionRepo(DbContext dbContext) : base(dbContext)
        {

        }


    }
}
