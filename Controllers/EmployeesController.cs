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
    public class EmployeesController : ControllerBase
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
        public IActionResult Create([FromBody]Employee employee)
        {
            if (ModelState.IsValid) 
            {
                _datacontext.Employees.Add(employee);
                _datacontext.SaveChanges();
                return Created("Employees", employee );
            }
            // return BadRequest("Couldn't create a new employee");
            return BadRequest(new { message = "Couldn't create a new employee ", errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(x => x.ErrorMessage) });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Employee viewModel)
        {
            Employee employee = _datacontext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Address = viewModel.Address;
            employee.FirstName = viewModel.FirstName;
            employee.LastName = viewModel.LastName;
            _datacontext.Employees.Update(employee);
            _datacontext.SaveChanges();
            

            return Ok(new { employee });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid) 
            {
                Employee employee = _datacontext.Employees.FirstOrDefault(e => e.Id == id);
                if (employee == null ) 
                {
                    return NotFound();
                }
                _datacontext.Employees.Remove(employee);
                _datacontext.SaveChanges();                
                return NoContent();
            }
            return BadRequest();
        }
    }
}
