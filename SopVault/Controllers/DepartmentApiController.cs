using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SopVault.Models;
using SopVault.Repository;

namespace SopVault.Controllers
{
    public class DepartmentApiController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentVersionRepository _documentVersionRepository;

        public DepartmentApiController(IDepartmentRepository departmentRepository, IDocumentRepository documentRepository, IDocumentVersionRepository documentVersionRepository)
        {
            _departmentRepository = departmentRepository;
            _documentRepository = documentRepository;
            _documentVersionRepository = documentVersionRepository;
        }

        [HttpGet]
        public IActionResult GetByDepartmentId(long id)
        {
            return Json(_documentRepository.GetAllByDepartmentId(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            return Json(await _departmentRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _departmentRepository.Upsert(department);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _departmentRepository.Upsert(department);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}