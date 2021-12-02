using System;
using Core.Entities;

namespace eogrenci.Entities.Concrete
{
    public class Student :BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
    }
}
