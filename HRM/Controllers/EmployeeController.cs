using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entity;
using HRM.Models;
using HRM.Services;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employee;
        private readonly HostingEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeService employee, HostingEnvironment hostingEnvironment)
        {
            _employee = employee;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var employees = _employee.GetAll().Select(emp => new EmployeeIndexViewModel
            {
                Id = emp.Id,
                EmployeeNo = emp.EmployeeNo,
                FullName = emp.FullName,
                ImageUrl = emp.ImageUrl,
                Gender = emp.Gender,
                DateJoined = emp.DateJoined,
                Designation = emp.Designation,
                City = emp.City,
                Address= emp.Address

            }).ToList();
            return View(employees);
        }
        //Create emplpyee
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevent cross-site  Request Forgery Attacks
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                  Id = model.Id,
                  EmployeeNo = model.EmployeeNo,
                  FirstName = model.FirstName,
                  MiddleName = model.MiddleName,
                  LastNeme = model.LastNeme,
                  FullName = model.FullName,
                  Gender = model.Gender,
                  Designation = model.Designation,
                  Email = model.Email,
                  DOB = model.DOB,
                  DateJoined = model.DateJoined,
                  NationalInsuranceNo = model.NationalInsuranceNo,
                  PayementMethod = model.PayementMethod,
                  StudentLoan = model.StudentLoan,
                  UnionMember = model.UnionMember,
                  Address = model.Address,
                  City= model.City,
                  Phone = model.Phone,
                  PostCode=model.PostCode
                };

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRoot = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRoot,uploadDir,fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }

               await _employee.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastNeme = employee.LastNeme,
                Gender = employee.Gender,
                Designation = employee.Designation,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PayementMethod = employee.PayementMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                Phone = employee.Phone,
                PostCode = employee.PostCode
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employee.GetById(model.Id);
                if (employee == null)
                {
                    return NotFound();
                }
                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastNeme = model.LastNeme;
                employee.MiddleName = model.MiddleName;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DOB = model.DOB;
                employee.DateJoined = model.DateJoined;
                employee.Phone = model.Phone;
                employee.Designation = model.Designation;
                employee.PayementMethod = model.PayementMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMember = model.UnionMember;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.PostCode = model.PostCode;

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRoot = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRoot, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }

                await _employee.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Detail(int id)
        {
            var employee = _employee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeDetailViewModel model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Phone = employee.Phone,
                Designation = employee.Designation,
                Email = employee.Email,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PayementMethod = employee.PayementMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                PostCode = employee.PostCode,
                ImageUrl = employee.ImageUrl
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeDeleteViewModel
            {
                Id = employee.Id,
                FullName = employee.FullName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeEditViewModel model)
        {
            await _employee.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}