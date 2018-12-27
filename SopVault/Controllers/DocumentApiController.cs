using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SopVault.Models;
using SopVault.Repository;

namespace SopVault.Controllers
{
    [Authorize]
    public class DocumentApiController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentVersionRepository _documentVersionRepository;

        public DocumentApiController(IDepartmentRepository departmentRepository, IDocumentRepository documentRepository, IDocumentVersionRepository documentVersionRepository)
        {
            _departmentRepository = departmentRepository;
            _documentRepository = documentRepository;
            _documentVersionRepository = documentVersionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_documentRepository.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            return Json(await _documentRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Document document)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _documentRepository.Upsert(document);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Document document)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _documentRepository.Upsert(document);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}