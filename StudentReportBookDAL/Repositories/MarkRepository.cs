﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;

namespace StudentReportBookDAL.Repositories
{
    public class MarkRepository
    {
        private readonly AppDbContext dbContext;

        public MarkRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Mark> GetAll()
        {
            IEnumerable<Mark> marks = dbContext.Marks
                                            .Include(st => st.Student)
                                                    .ThenInclude( st => st.Group)
                                                    .ThenInclude(g =>g.Faculty)
                                             .Include(st => st.TeachersWorkload)
                                                    .ThenInclude(tw => tw.Group)
                                              .Include(st => st.TeachersWorkload)
                                                    .ThenInclude(tw => tw.Subject).AsEnumerable();

            return marks;
        }

        public IEnumerable<Mark> Get(Expression<Func<Mark, bool>> predicate)
        {
            IEnumerable<Mark> marks = dbContext.Marks
                                            .Where(predicate)
                                        .Include(st => st.TeachersWorkload)
                                             .ThenInclude(tw => tw.Subject)
                                        .Include(st => st.TeachersWorkload)
                                             .ThenInclude(tw => tw.Teacher)
                                        .AsEnumerable();

            return marks;
        }

        public void Add(Mark mark)
        {
            dbContext.Set<Mark>().Add(mark);
        }

        public void Update(Mark mark)
        {
            dbContext.Set<Mark>().Update(mark);
        }

        public void Delete(int markId)
        {
            Mark existing = dbContext.Set<Mark>().Find(markId);
            if (existing != null) dbContext.Set<Mark>().Remove(existing);
        }
    }
}
