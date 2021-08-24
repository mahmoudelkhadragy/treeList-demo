using TreeListDemo.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TreeListDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentController>
        [HttpGet]
        public List<Student> Get()
        {
            return  new List<Student>
         {
             new Student{ID=1,Name="Ahmed",Department="SD"},
             new Student{ID=2,Name="Mohamed",Department="UI"},
             new Student{ID=3,Name="Mostafa",Department="SD"},
             new Student{ID=4,Name="Zyad",Department="OS"}
         };
        }

    }
}
