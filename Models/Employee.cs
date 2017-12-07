using System;
using System.ComponentModel.DataAnnotations;

namespace windforce_corp.Models
{
  public class Employee
  {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]        
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
        
        [Required]
        public double Salary { get; set; }

        public string AvatarUrl { get; set; }

        [Required]
        public string Role { get; set; }
  }
    
}
