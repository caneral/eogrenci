using System;
namespace eogrenci.Dtos.LessonDtos
{
    public class LessonUpdateDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
