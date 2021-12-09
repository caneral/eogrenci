using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.Dtos.QuestionDtos;
namespace eogrenci.BL.Abstract
{
    public interface IQuestionService 
    {
        Task<IResponse<List<QuestionListDto>>> GetAll();
        Task<IResponse<QuestionAddDto>> Add(QuestionAddDto questionAddDto);
        Task<IResponse<QuestionListDto>> GetById(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<QuestionUpdateDto>> Update(QuestionUpdateDto questionUpdateDto);
    } 
}
