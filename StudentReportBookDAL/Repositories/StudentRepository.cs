//using Microsoft.EntityFrameworkCore;
//using StudentReportBookDAL.Context;
//using StudentReportBookDAL.Entities;
//using StudentReportBookDAL.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace StudentReportBookDAL.Repositories
//{
//    class StudentRepository : IRepository<Student>
//    {
//        private AppDbContext dbContext;
//        public StudentRepository(AppDbContext databaseContext)
//        {
//            dbContext = databaseContext;
//        }
//        public void Create(Student student)
//        {
//            dbContext.Students.Add(student);
//        }

//        public void Delete(int id)
//        {
//            Student student =dbContext.Students.Find(id);
//            if (student != null)
//            {
//                dbContext.Students.Remove(student);
//            }
//        }

//        public IEnumerable<Student> Find(Func<Student, bool> predicate)
//        {
//            IEnumerable<Student> result = dbContext.Students.Include(s => s.Group).Include(s => s.PersonSubjects).Where(predicate);
//            return result;
//        }

//        public Student Get(int id)
//        {
//            Student student = dbContext.Students.Find(id);
//            return student;
//        }

//        public IEnumerable<Student> GetAll()
//        {
//            IEnumerable<Student> students = dbContext.Students.Include(s => s.Group).Include(s => s.PersonSubjects);
//            return students;
//        }

//        public void Update(Student student)
//        {
//            dbContext.Entry(student).State = EntityState.Modified;
//        }
//    }
//}
