using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using Demo.PL.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using Demo.PL.Helper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
           
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetEmployeeByNameAsync(SearchValue);
            }
            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);
        }

            [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeView)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    employeeView.ImageName = DocumentSettings.UploadFile(employeeView.Image, "Images");
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeView);
                    await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return BadRequest();
            }

            return View(employeeView);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }


            
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            ViewBag.Department = await _unitOfWork.DepartmentRepository.GetByIdAsync(employee.DepartmentId);
            if (employee is null)
            {
                return BadRequest();
            }

            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployee);


        }

        public async Task<IActionResult> Update(Employee employee)
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return await Details(employee.Id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel employeeView, [FromRoute] int? id)
        {
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeView);
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index");
                }

            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employeeView);
        }

        public async Task<IActionResult> Delete(Employee employee)
        {
            return await Details(employee.Id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeView, [FromRoute] int? id)
        {
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeView);
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(employeeView);
        }
    }
}
