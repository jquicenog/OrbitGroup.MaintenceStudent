using OrbitGroup.MaintenceStudent.Core.Contracts;
using OrbitGroup.MaintenceStudent.Core.Dto;
using OrbitGroup.MaintenceStudent.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrbitGroup.MaintenceStudent.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDto>> GetStudents()
        {
            var students = await _studentRepository.GetStudents();
            return students.Select(x => MapToConcreteObj(x));
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var students = await _studentRepository.GetStudents();
            var student = students.FirstOrDefault(x => x.Id == id);
            return student != null ? MapToConcreteObj(student) : null;
        }

        public async Task<StudentDto> AddStudent(StudentDto newStudent)
        {
            var studentMap = MapObj(newStudent);
            var student = await _studentRepository.AddStudent(studentMap);
            return student != null ? MapToConcreteObj(student) : null;
        }

        public async Task<StudentDto> UpdateStudent(StudentDto newStudent)
        {
            var studentMap = MapObj(newStudent);
            var student = await _studentRepository.UpdateStudent(studentMap);
            return student != null ? MapToConcreteObj(student) : null;
        }

        public async Task<StudentDto> RemoveStudent(int id)
        {
            var student = await _studentRepository.RemoveStudent(id);
            return student != null ? MapToConcreteObj(student) : null;
        }

        private StudentDto MapToConcreteObj(Student student)
        {
            StudentDto studentDto = new StudentDto
            {
                Id = student.Id,
                UserName = student.UserName,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Career = student.Career
            };

            return studentDto;
        }

        private Student MapObj(StudentDto newStudent)
        {
            Student student = new Student
            {
                Id = newStudent.Id,
                UserName = newStudent.UserName,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                Age = newStudent.Age,
                Career = newStudent.Career
            };

            return student;
        }
    }
}
