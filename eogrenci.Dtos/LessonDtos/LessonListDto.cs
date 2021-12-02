using System;
using Core.Entities;

namespace eogrenci.Dtos.LessonDtos
{
    public class LessonListDto : IDTO
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
