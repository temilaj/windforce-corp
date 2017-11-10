using System;
using System.ComponentModel.DataAnnotations;

namespace windforce_corp.Models
{
  public class Employee
  {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string FullName 
        {
            get 
            {   
                return  $"{FirstName} {LastName}";
            }
        }
        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set; }
        public Double Salary { get; set; }
  }
    
}