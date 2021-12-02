using System;
namespace eogrenci.Dtos.QuestionDtos
{
    public class QuestionUpdateDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
    }
}
