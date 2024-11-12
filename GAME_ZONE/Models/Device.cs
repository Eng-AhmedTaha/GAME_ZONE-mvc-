using System.ComponentModel.DataAnnotations;

namespace GameZone.Models;

public class Device : BaseEntity
{
    [MaxLength(50)]
    public string Icon { get; set; } = string.Empty;
}