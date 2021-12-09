using System;
using System.Collections.Generic;
using Core.Entities;

namespace eogrenci.Entities.Concrete
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Lesson> Lessons { get; set; }

    }
}
