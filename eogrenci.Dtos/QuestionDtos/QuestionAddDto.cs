using System;
using Core.Entities;

namespace eogrenci.Dtos.QuestionDtos
{
    public class QuestionAddDto : IDTO
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
    }
}
