using Microsoft.EntityFrameworkCore;
using OrbitGroup.MaintenceStudent.Core.Contracts;
using OrbitGroup.MaintenceStudent.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrbitGroup.MaintenceStudent.Dal
{
    public class StudentDBRepository:IStudentRepository
    {
        private readonly StudentContext context;

        public StudentDBRepository (StudentContext contextObj)
        {
            context = contextObj;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await context.Student.ToListAsync();
        }

        public async Task<Student> AddStudent(Student newStudent)
        {
            context.Student.Add(newStudent);
            var idStudent = await context.SaveChangesAsync();
            return newStudent;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            context.Student.Update(student);
            await context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> RemoveStudent(int id)
        {
            var student = await context.Student.FirstAsync(x => x.Id == id);
            context.Remove(student);
            context.SaveChanges();

            return student;
        }
    }
}
