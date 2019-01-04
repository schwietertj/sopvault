using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SopVaultDataModels.Models
{
    public class Link : BaseEntity
    {
        public long DocumentVersionId { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Url { get; set; }
        
        [MaxLength(1024)]
        public string Description { get; set; }

        [ForeignKey("DocumentVersionId")]
        public DocumentVersion DocumentVersion { get; set; }
    }
}
