using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SopVault.Repository;

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


        
    }
}