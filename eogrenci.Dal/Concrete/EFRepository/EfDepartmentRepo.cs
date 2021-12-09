using System;
using Core.DataAccess;
using eogrenci.Dal.Abstract.EFIRepository;
using eogrenci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace eogrenci.Dal.Concrete.EFRepository
{
    public class EfDepartmentRepo : EFBaseRepo<Department>, IDepartmentRepo
    {
        public EfDepartmentRepo(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
