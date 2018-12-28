using System;

namespace SopVaultDataModels.Models
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        DateTime Modified { get; set; }
        string ModifiedBy { get; set; }
        bool Active { get; set; }
    }
}
