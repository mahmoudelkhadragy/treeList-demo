using TreeListDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeListDemo.BL;

namespace TreeListDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConclusionColumnsController : ControllerBase
    {
        List<string> ColumnName = new List<string>
       {
           "id","name","department"
       };


        private readonly Generator _generator;

        public ConclusionColumnsController(Generator generator)
        {
            _generator = generator;
        }




        //    public List<string> Get()
        //{
        //    return ColumnName;
        //}
        public List<Student> Get()
        {
           // _generator.ConcateReportGeneratorQuery(new List<string> { "Name", "DeptName" }, 0);

            return new List<Student>
         {
             new Student{ID=1,Name="Ahmed",Department="SD"},
             new Student{ID=2,Name="Mohamed",Department="UI"},
             new Student{ID=3,Name="Mostafa",Department="SD"},
             new Student{ID=4,Name="Zyad",Department="OS"}
         };

        }

    }
}
