using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Models.Entities;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;

namespace StudentReportBook.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITeacherService teacherService;
        public SampleDataController(IMapper mapper, ITeacherService teacherService)
        {
            this.mapper = mapper;
            this.teacherService = teacherService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Teacher> WeatherForecasts()
        {
            IEnumerable<TeacherBll> teachersbll= teacherService.GetAllTeachers();
            IEnumerable<Teacher> teachers = mapper.Map<IEnumerable<Teacher>>(teachersbll);
            return teachers;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
