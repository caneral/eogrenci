using System;
using Core.Entities;

namespace eogrenci.Entities.Concrete
{
    public class Lesson : BaseEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
