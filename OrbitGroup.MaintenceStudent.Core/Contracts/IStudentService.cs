using OrbitGroup.MaintenceStudent.Core.Dto;
using OrbitGroup.MaintenceStudent.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrbitGroup.MaintenceStudent.Core.Contracts
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetStudents();
        Task<StudentDto> GetStudentById(int id);
        Task<StudentDto> AddStudent(StudentDto newStudent);
        Task<StudentDto> UpdateStudent(StudentDto student);
        Task<StudentDto> RemoveStudent(int id);
    }
}
