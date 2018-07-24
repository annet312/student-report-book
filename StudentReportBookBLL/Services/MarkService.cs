using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        IEnumerable<MarkBll> IMarkService.GetAllMarks(StudentBll student)
        {
            IEnumerable<Mark> marks = db.Marks.Get(m => m.StudentId == student.Id);

            IEnumerable<MarkBll> marksbll = mapper.Map<IEnumerable<MarkBll>>(marks);

            return marksbll;
        }

        IEnumerable<MarkBll> IMarkService.GetAllMarksOfGroup(TeacherBll teacher, GroupBll group)
        {
            IEnumerable<Mark> marks = db.Marks.Get(m => (m.TeachersWorkload.TeacherId == teacher.Id ) && (m.TeachersWorkload.GroupId == group.Id));

            IEnumerable<MarkBll> marksbll = mapper.Map<IEnumerable<MarkBll>>(marks);

            return marksbll;
        }

        IEnumerable<MarkBll> IMarkService.GetAllMarksOfGroup(SubjectBll subject, GroupBll group)
        {
            throw new NotImplementedException();
        }

        IEnumerable<MarkBll> IMarkService.GetAllMarksOfGroup(TeacherBll teacher, SubjectBll subject, GroupBll group)
        {
            throw new NotImplementedException();
        }
    }
}
