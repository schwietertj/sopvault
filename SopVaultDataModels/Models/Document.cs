using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SopVaultDataModels.Models
{
    public class Document : BaseEntity
    {
        public long DepartmentId { get; set; }
        [Required, MaxLength(255)]
        public string DocumentNumber { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public IEnumerable<DocumentVersion> DocumentVersions { get; set; }
    }
}
