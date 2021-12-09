using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.BL.Abstract;
using eogrenci.Dtos.LessonDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eogrenci.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// Tüm soruları getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<LessonListDto>>> GetLessonList()
        {
            try
            {
                var response = await _lessonService.GetAll();
                return response.Data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonListDto>> GetLessonWithId(int id)
        {
            try
            {
                var response = await _lessonService.GetById(id);
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
        [HttpPost]
        public async Task<ActionResult<string>> AddLesson(LessonAddDto lessonAddDto)
        {
            var list = new List<string>();

            if (lessonAddDto == null)
            {
                list.Add("Eklenecek soru bilgisi bulunamadı.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }


            try
            {
                var response = await _lessonService.Add(lessonAddDto);
                if (response.ResponseType == ResponseType.ValidationError)
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





        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteLesson(int id)
        {

            try
            {
                var response = await _lessonService.Remove(id);
                if (response.ResponseType == ResponseType.NotFound)
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

        [HttpPut]
        public async Task<ActionResult<string>> UpdateLesson(LessonUpdateDto lessonUpdateDto)
        {
            var list = new List<string>();
            var response = await _lessonService.Update(lessonUpdateDto);
            if (response.ResponseType == ResponseType.NotFound)
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