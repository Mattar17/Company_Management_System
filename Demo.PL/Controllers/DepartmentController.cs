using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
            {
                return BadRequest();
            }

            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(Id);

            if (department is null)
            {
                return BadRequest();
            }

            return View(ViewName, department);

        }
        [HttpGet]
        public async Task<IActionResult> Update(Department department)
        {
            return await Details(department.Id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department department, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index");
                }

                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);
        }
    }
}
