using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using windforce_corp.Models;

namespace windforce_corp.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var employees = A.ListOf<Employee>();
            // return new string[] { "value1", "value2" };
            return Ok(employees);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}
