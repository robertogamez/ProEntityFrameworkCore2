﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdvancedContext context;

        public HomeController(AdvancedContext ctx)
        {
            this.context = ctx;
        }

        private static Func<AdvancedContext, string, IEnumerable<Employee>>
            query = EF.CompileQuery((AdvancedContext context, string searchTerm) =>
                context.Employees.Where(e => EF.Functions.Like(e.FirstName, searchTerm)));

        public async Task<IActionResult> Index(string searchTerm)
        {
            //IQueryable<Employee> data = context.Employees;
            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    data = data.Where(e => EF.Functions.Like(e.FirstName, searchTerm));
            //}
            //HttpClient client = new HttpClient();
            //ViewBag.PageSize = (await client.GetAsync("http://apress.com"))
            //    .Content.Headers.ContentLength;

            //return View(
            //    string.IsNullOrEmpty(searchTerm) 
            //        ? await context.Employees.ToListAsync()
            //        : query(context, searchTerm)
            //);
            return View(context.Employees
                .Include(e => e.OtherIdentity)
                .OrderByDescending(e => EF.Property<DateTime>(e, "LastUpdated")));
        }

        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            return View(string.IsNullOrWhiteSpace(SSN)
            ? new Employee() : context.Employees.Include(e => e.OtherIdentity)
                .AsNoTracking()    
                .First(e => e.SSN == SSN
                            && e.FirstName == firstName
                            && e.FamilyName == familyName));
        }

        [HttpPost]
        public IActionResult Update(Employee employee, decimal originalSalary)
        {
            //Employee existing = context.Employees
            //    .AsTracking()
            //    .First(e => e.SSN == employee.SSN && e.FirstName == employee.FirstName
            //            && e.FamilyName == employee.FamilyName);
            //if (existing == null)
            //{
            //    context.Add(employee);
            //}
            //else
            //{
            //    existing.Salary = employee.Salary;
            //}

            //context.SaveChanges();
            //return RedirectToAction(nameof(Index));

            //if (context.Employees.Count(e => e.SSN == employee.SSN
            //        && e.FirstName == employee.FirstName
            //        && e.FamilyName == employee.FamilyName) == 0)
            //{
            //    context.Add(employee);
            //}
            //else
            //{
            //    context.Update(employee);
            //}
            //Employee existing = context.Employees
            //    .Find(employee.SSN, employee.FirstName, employee.FamilyName);

            //if(existing == null)
            //{
            //    //context.Entry(employee).Property("LastUpdated")
            //    //    .CurrentValue = System.DateTime.Now;
            //    context.Add(employee);
            //}
            //else
            //{
            //    existing.Salary = salary;
            //    context.Entry(existing).Property("LastUpdated")
            //        .CurrentValue = System.DateTime.Now;
            //}
            if (context.Employees.Count(e => e.SSN == employee.SSN
                && e.FirstName == employee.FirstName
                && e.FamilyName == employee.FamilyName) == 0)
            {
                context.Add(employee);
            }
            else
            {
                Employee e = new Employee
                {
                    SSN = employee.SSN,
                    FirstName = employee.FirstName,
                    FamilyName = employee.FamilyName,
                    Salary = originalSalary,
                    RowVersion = employee.RowVersion
                };
                context.Employees.Attach(e);
                e.Salary = employee.Salary;
                e.LastUpdated = DateTime.Now;
            }

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            context.Attach(employee);
            employee.SoftDeleted = true;
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}