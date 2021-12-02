using System;
using Core.Entities;

namespace eogrenci.Entities.Concrete
{
    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
       
    }
}
