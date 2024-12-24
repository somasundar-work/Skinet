using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Skinet.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    [DefaultValue(false)]
    public bool IsDeleted { get; set; }

    [DefaultValue(true)]
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }

    [DefaultValue("System")]
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
}
