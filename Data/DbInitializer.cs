using System;
using windforce_corp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using GenFu;
using System.Linq;

namespace windforce_corp.Data
{
    public static class DbInitializer
    {
        public async static void Initialize(ApplicationDbContext datacontext, UserManager<ApplicationUser> _userManager)
        {
            if (datacontext.Users.Any())
            {
                return;
            }

            var users = new ApplicationUser[]
            {
                new ApplicationUser()
                {
                    UserName = "jundoe",
                    FirstName = "Jun",
                    LastName = "Doe",
                    Email = "jundoe@email.com"
                },
                new ApplicationUser()
                {
                    UserName = "adewaleokocha@email.com",
                    FirstName = "Adewale",
                    LastName = "Okocha",
                    Email = "adewaleokoca@email.com",
                },
                new ApplicationUser()
                {
                    UserName = "tolusmith@email.com",
                    FirstName = "Tolu",
                    LastName = "Smith",
                    Email = "tolusmith@email.com",
                },
            };

            foreach(var user in users)
            {
                await _userManager.CreateAsync(user, "Jundoe");
            }

            
            var salaries = new List<Double>() 
            {
                45000.00, 
                40000.00, 
                35505.12, 
                61222.01 
            };

            var roles = new String[] 
            {
                "CEO", "Sales Manager", "Sales Rep", "IT Manager", "IT Engineer"
            };

            A.Configure<Employee>()
                .Fill(x => x.Salary).WithRandom(salaries)
                .Fill(x => x.Role).WithRandom(roles)
                .Fill(x => x.AvatarUrl).AsPlaceholderImage(200, 200)
                .Fill(x => x.Id, () => 0);

            IEnumerable<Employee> employees = A.ListOf<Employee>(50);

            datacontext.Employees.AddRange(employees);
            datacontext.SaveChanges();

        }
    }
}