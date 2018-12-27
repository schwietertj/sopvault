using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SopVault.Models
{
    public class Department : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        public IEnumerable<Document> Documents { get; set; }
    }
}
