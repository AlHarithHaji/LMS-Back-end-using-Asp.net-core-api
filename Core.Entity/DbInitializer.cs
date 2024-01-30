using Core.domain.Models;
using Core.Entity.LMSDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public static class DbInitializer
    {
        public static void Initialize(LMSDbDataContext context)
        {
            context.Database.EnsureCreated();

            // Check if there is already data in the database
            if (context.LoginTb.Any())
            {
                return;   // Database has been seeded
            }

            // Seed your initial data
            var loginTb = new LoginTb[]
            {
            new LoginTb { FirstName = "Admin", LastName="Admin",Email="Admin@gmail.com",IsActive=true,DepartmentId=0,SessionId=0,CourseId=0,UserType="Admin", Address="ABC",Mobile="998", Password="kVU41twDyttUL/SM7IO0vQ==" },
           
                // Add more products as needed
            };

            context.LoginTb.AddRange(loginTb);
            context.SaveChanges();

            //Deparments
            var departments = new DepartmentTb[]
          {
            new DepartmentTb { DepartmentId=1,DeparmentName="Web Development", IsPaiad=false },
            new DepartmentTb { DepartmentId=2,DeparmentName="Mobile Development", IsPaiad=true },
            new DepartmentTb { DepartmentId=3,DeparmentName="Other Development", IsPaiad=false },

              // Add more products as needed
          };
            context.DepartmentTb.AddRange(departments);
            context.SaveChanges();

            ///Active Sessions
            ///
            var Session = new SesstionTB[]
          {
            new SesstionTB { SesstionId=1,Session="DECE-2034", IsActive=true },
            

              // Add more products as needed
          };
            context.SesstionTB.AddRange(Session);
            context.SaveChanges();
        }
    }

}
