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
            var salaries = new List<Double>() 
            {
                45000.00, 
                40000.00, 
                35505.12, 
                61222.01 
            };

            A.Configure<Employee>()
                .Fill(x => x.Salary).WithRandom(salaries);

            var employees = A.ListOf<Employee>(50);
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
