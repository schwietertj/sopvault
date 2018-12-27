using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SopVault.Models
{
    public class DocumentVersion : BaseEntity
    {
        public long DocumentId { get; set; }
        [Required, MaxLength(1024)]
        public string Title { get; set; }
        public string Purpose { get; set; }
        public string Definitions { get; set; }
        public string Procedure { get; set; }
        public int Version { get; set; }
        [MaxLength(512)]
        public string Notes { get; set; }
        public bool Approved { get; set; }
        public bool Draft { get; set; }

        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
    }
}
