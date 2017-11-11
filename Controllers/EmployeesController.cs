using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using windforce_corp.Data;
using windforce_corp.Models;

namespace windforce_corp.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _datacontext;

        public EmployeesController(ApplicationDbContext datacontext)
        {
            _datacontext = datacontext;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _datacontext.Employees.ToArray();
            return Ok(employees);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = _datacontext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) 
            {
                return NotFound();
            }
            return Ok(employee);
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
