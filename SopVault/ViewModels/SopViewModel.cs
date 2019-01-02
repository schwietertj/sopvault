using System.Threading.Tasks;
using SopVault.Repository;
using SopVaultDataModels.Models;

namespace SopVault.ViewModels
{
    public class SopViewModel
    {
        public Department Department { get; set; } = new Department();
        public Document Document { get; set; } = new Document();
        public DocumentVersion DocumentVersion { get; set; } = new DocumentVersion();

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentVersionRepository _documentVersionRepository;

        public SopViewModel() { }

        public SopViewModel(IDepartmentRepository departmentRepository, IDocumentRepository documentRepository, IDocumentVersionRepository documentVersionRepository)
        {
            _departmentRepository = departmentRepository;
            _documentRepository = documentRepository;
            _documentVersionRepository = documentVersionRepository;
        }

        public async Task<SopViewModel> GetNew(long departmentId)
        {
            return new SopViewModel
            {
                Department = await _departmentRepository.GetById(departmentId),
                Document = new Document { DepartmentId = departmentId },
                DocumentVersion = new DocumentVersion { }
            };
        }
    }
}
