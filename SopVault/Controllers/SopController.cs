using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SopVault.Repository;
using SopVault.ViewModels;
using SopVaultDataModels.Models;

namespace SopVault.Controllers
{
    [Authorize]
    public class SopController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentVersionRepository _documentVersionRepository;

        public SopController(IDepartmentRepository departmentRepository, IDocumentRepository documentRepository, IDocumentVersionRepository documentVersionRepository)
        {
            _departmentRepository = departmentRepository;
            _documentRepository = documentRepository;
            _documentVersionRepository = documentVersionRepository;
        }

        public IActionResult Index()
        {
            return View(_departmentRepository.GetAll());
        }

        public IActionResult CreateDepartment()
        {
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _departmentRepository.Upsert(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Department(long id)
        {
            var result = await _departmentRepository.GetDetail(id);

            if (result is null)
                return RedirectToAction("Index");

            result.Documents = _documentRepository.GetAllByDepartmentId(id);

            return View(result);
        }

        public async Task<IActionResult> CreateSop(long departmentId)
        {
            var result = new SopViewModel
            {
                Department = await _departmentRepository.GetById(departmentId),
                Document = new Document {DepartmentId = departmentId},
                DocumentVersion = new DocumentVersion { }
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSop(long departmentId, SopViewModel model)
        {
            model.Document = await _documentRepository.Upsert(model.Document);
            model.DocumentVersion.DocumentId = model.Document.Id;

            await _documentVersionRepository.Upsert(model.DocumentVersion);

            return RedirectToAction("Department", new { departmentId });
        }
    }
}