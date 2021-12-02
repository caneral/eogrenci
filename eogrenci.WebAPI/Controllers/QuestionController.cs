using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.BL.Abstract;
using eogrenci.Dtos.QuestionDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eogrenci.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Tüm soruları getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetQuestionList")]
        public async Task<ActionResult<List<QuestionListDto>>> GetQuestionList()
        {
            try
            {
                var response = await _questionService.GetAll();
                return response.Data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Soru ekleme.
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddQuestion")]
        public async Task<ActionResult<string>> AddQuestion(QuestionAddDto questionAddDto)
        {
            var list = new List<string>();

            if (questionAddDto == null)
            {
                list.Add("Eklenecek soru bilgisi bulunamadı.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }


            try
            {
                var response = await _questionService.Add(questionAddDto);
                if(response.ResponseType == ResponseType.ValidationError)
                {
                    foreach (var error in response.ValidationErrors)
                    {
                        list.Add(error.ErrorMessage);
                    }
                    return Ok(list);
                }
                else 
                {
                    return Ok(response.Message);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpDelete("DeleteQuestion")]
        public async Task<ActionResult<string>> DeleteQuestion(int id)
        {

            try
            {
               var response = await _questionService.Remove(id);
               if(response.ResponseType == ResponseType.NotFound)
                {
                    return NotFound(response.Message);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateQuestion")]
        public async Task<ActionResult<string>> UpdateQuestion(QuestionUpdateDto questionUpdateDto)
        {
            var list = new List<string>();
            var response = await _questionService.Update(questionUpdateDto);
            if(response.ResponseType == ResponseType.NotFound)
            {
                foreach (var error in response.ValidationErrors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(list);
                }
            }
            return Ok(response.Message);
        }


    }
}