using System;
namespace eogrenci.Dtos.QuestionDtos
{
    public class QuestionAddDto
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
    }
}
