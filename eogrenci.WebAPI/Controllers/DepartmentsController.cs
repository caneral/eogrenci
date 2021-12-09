using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResponseObjects;
using eogrenci.BL.Abstract;
using eogrenci.Dtos.DepartmentDtos;
using eogrenci.Dtos.LessonDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eogrenci.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Tüm soruları getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<DepartmentListDto>>> GetDepartmentList()
        {
            try
            {
                var response = await _departmentService.GetAll();
                return response.Data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/lessons")]
        public async Task<ActionResult<List<LessonListDto>>> GetWithLessons(int id)
        {
            try
            {
                var response = await _departmentService.GetWithLessons(id);
                return response.Data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentListDto>> GetDepartmentWithId(int id)
        {
            try
            {
                var response = await _departmentService.GetById(id);
                return response.Data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<string>> AddDepartment(DepartmentAddDto departmentAddDto)
        {
            var list = new List<string>();

            if (departmentAddDto == null)
            {
                list.Add("Eklenecek soru bilgisi bulunamadı.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }


            try
            {
                var response = await _departmentService.Add(departmentAddDto);
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
        public async Task<ActionResult<string>> DeleteDepartment(int id)
        {

            try
            {
                var response = await _departmentService.Remove(id);
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
        public async Task<ActionResult<string>> UpdateDepartment(DepartmentUpdateDto departmentUpdateDto)
        {
            var list = new List<string>();
            var response = await _departmentService.Update(departmentUpdateDto);
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