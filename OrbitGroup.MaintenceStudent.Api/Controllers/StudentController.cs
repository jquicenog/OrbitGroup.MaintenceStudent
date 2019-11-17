using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrbitGroup.MaintenceStudent.Core.Contracts;
using OrbitGroup.MaintenceStudent.Core.Dto;
using OrbitGroup.MaintenceStudent.Core.Models;

namespace OrbitGroup.MaintenceStudent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudents();
                if(!students.Any())
                    return NotFound();

                return (Ok(students));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudentsById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                if (student==null)
                    return NotFound();

                return (Ok(student));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto newStudent)
        {
            try
            {
                var student = await _studentService.AddStudent(newStudent);
                if (student == null)
                    return NotFound();

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDto newStudent)
        {
            var response = new GenericResponse<StudentDto>();
            response.Entry = newStudent;
            try
            {
                var student = await _studentService.UpdateStudent(newStudent);
                response.Entry = student;

                if (student == null)
                {
                    response.Message = new Message
                    {
                        Error = "Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Messagge = string.Format("Client with id {0} can not found in database", newStudent.Id)
                    };
                }
                else
                {
                    response.Message = new Message
                    {
                        Error = string.Empty,
                        Status = StatusCodes.Status200OK,
                        Messagge = "The client data has been updated"
                    };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = new Message
                {
                    Error = ex.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Messagge = "Error to update student, please validate if the studente exists in it database"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var response = new GenericResponse<StudentDto>();
            response.Entry = null;
            try
            {
                var student = await _studentService.RemoveStudent(id);
                response.Entry = student;

                if (student == null)
                {
                    response.Message = new Message
                    {
                        Error = "Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Messagge = string.Format("Client with id {0} can not found in database", id)
                    };
                }
                else
                {
                    response.Message = new Message
                    {
                        Error = string.Empty,
                        Status = StatusCodes.Status200OK,
                        Messagge = "The client data has been deleted"
                    };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = new Message
                {
                    Error = ex.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Messagge = "Error to delete student, please validate if the studente exists in it database"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}