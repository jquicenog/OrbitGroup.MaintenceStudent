using OrbitGroup.MaintenceStudent.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrbitGroup.MaintenceStudent.Core.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> AddStudent(Student newStudent);
        Task<Student> UpdateStudent(Student student);
        Task<Student> RemoveStudent(int id);
    }
}
