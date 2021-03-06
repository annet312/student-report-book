﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookBLL.Services
{
    public class MarkService : IMarkService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public MarkService(IMapper mapper, IUnitOfWork db)
        {
            this.mapper = mapper;
            this.db = db;
        }

        private MarkBll AddMark(int grade, Student student, TeachersWorkload teachersWorkload, bool flSingle)
        {
            Mark mark = new Mark()
            {
                Grade = grade,
                Student = student,
                TeachersWorkload = teachersWorkload,
                Date = DateTime.Now
            };
            try
            {
                db.Marks.Add(mark);
                if (flSingle == true)
                {
                    db.Save();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            MarkBll markBll = mapper.Map<MarkBll>(mark);
            return markBll;
        }

        private MarkBll EditMark(Mark mark, int grade)
        {
            mark.Grade = grade;
            try
            {
                db.Marks.Update(mark);
                db.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
            MarkBll markBll = mapper.Map<MarkBll>(mark);

            return markBll;
        }

        public MarkBll EditMark(StudentBll student, int grade, TeachersWorkloadBll teachersWorkload)
        {
            Student stud = mapper.Map<Student>(student);
            TeachersWorkload tw = mapper.Map<TeachersWorkload>(teachersWorkload);
            Mark mark = db.Marks.Get(m => (m.Student.Id == student.Id) && (m.TeachersWorkload.Id == teachersWorkload.Id)).SingleOrDefault();
            MarkBll markBll;
            if(mark == null)
            {
                try
                {
                    markBll = AddMark(grade, stud, tw, true);
                }
                catch
                {
                    return null; 
                }
            }
            else
            {
                try
                {
                    markBll = EditMark(mark, grade);
                }
                catch 
                {
                    return null;
                }
            }
            return markBll;
        }

        public IEnumerable<MarkBll> GetAllMarksOfSubject(int teacherId, int subjectId, int groupId, int studentId)
        {
            IEnumerable<Mark> marks = db.Marks.Get(m => (m.TeachersWorkload.Subject.Id == subjectId)
                                                    && (m.Student.Group.Id == groupId)
                                                    && (m.TeachersWorkload.Teacher.Id == teacherId)
                                                    && (m.Student.Id == studentId));
            IEnumerable<MarkBll> markBlls = mapper.Map<IEnumerable<MarkBll>>(marks);

            return markBlls;
        }

        IEnumerable<MarkBll> IMarkService.GetAllMarks(StudentBll student)
        {
            IEnumerable<Mark> marks = db.Marks.Get(m => m.StudentId == student.Id);
            if (marks == null)
                return null;
            IEnumerable<MarkBll> marksbll = mapper.Map<IEnumerable<MarkBll>>(marks);
            
            return marksbll;
        }
    }
}
